using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SNG.Save;
using SNG.Configs;
using DG.Tweening;

public class Dragging : MonoBehaviour
{
    public UIController UIController;
    public ObjectController ObjectController;

    public int ObjectCounter;
    public GameObject HoleObject;

    private float _dist;
    private bool _isGameOver = false;
    private bool _dragging = false;
    private bool _isLeft = false;
    private bool _isRight = false;

    private GameObject _toDragObjectLeft;
    private GameObject _toDragObjectRight;

    private Vector3 _leftOriginalPosition;
    private Vector3 _rightOriginalPosition;

    private Vector3 _tempLocation;
    private Transform _toDrag;

    private float _xOffset = 0;
    private float _zOffset = 0;

    private Collider _collider;

    private float _xCoor;
    private float _zCoor;

    private System.Random _random = new System.Random();


    // Start is called before the first frame update
    void Start()
    {
        
        //ObjectCounter = Configs.LevelConfig.LevelList[SaveGame.Instance.GeneralData.CurrentLevel - 1].TotalObjectCount;
        _collider = HoleObject.GetComponent<Collider>();
    }
    // Update is called once per frame
    void Update()
    {
        if(_isGameOver){
            return;
        }
        if(_isLeft){
            _toDragObjectLeft.gameObject.tag = "Untagged";
        }
        if(_isRight){
            _toDragObjectRight.gameObject.tag = "Untagged";
        }

        if (ObjectCounter == 0)
        {
            SaveGame.Instance.GeneralData.IsGameOver = true;
            _isGameOver = true;
            UIController.GameOverWon();
            return;
        }

        Vector3 v3;
        if (Input.touchCount != 1)
        {
            _dragging = false;
            return;
        }

        Touch touch = Input.touches[0];
        Vector3 pos = touch.position;

        

        if (touch.phase == TouchPhase.Began)
        {
            Ray ray = Camera.main.ScreenPointToRay(pos);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag == "cube")
                {
                    _toDrag = hit.transform;
                    _dist = hit.transform.position.y - Camera.main.transform.position.y; // Use Y position for the distance
                    v3 = new Vector3(pos.x, pos.y, _dist);
                    v3 = Camera.main.ScreenToWorldPoint(v3);
                    _tempLocation = _toDrag.position;
                    _dragging = true;
                    // calculate the offset btw touch position and the object pivot position
                    _xOffset = _toDrag.gameObject.transform.position.x - hit.point.x;
                    _zOffset = _toDrag.gameObject.transform.position.z - hit.point.z;

                }
            }
        }

        if (_dragging && touch.phase == TouchPhase.Moved)
        {
            if (_toDrag == null)
            {
                _dragging = false;
                return;
            }

            v3 = new Vector3(Screen.width - Input.mousePosition.x, Screen.height - Input.mousePosition.y, _dist); // Invert the mouse input direction
            v3 = Camera.main.ScreenToWorldPoint(v3);
            v3.x += _xOffset;
            v3.z += _zOffset;
            // check if the object is destroyed
            if(_toDrag != null){
                _toDrag.DOMove(new Vector3(v3.x, _tempLocation.y + 4, v3.z), 0.2f);
            }
        }

        if (_dragging && (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled))
        {
            _dragging = false;

            if(_collider.bounds.Intersects(_toDrag.GetComponent<Collider>().bounds))
            {
                // if Object's x position is less than 388 then the left hole is selected
                if (_toDrag.position.x < 388 && !_isLeft)
                {
                    // fine tune dropping position
                    if(_toDrag.position.x < 376)
                    {
                        _toDrag.position = new Vector3(378, _toDrag.position.y, _toDrag.position.z);
                    }
                    if(_toDrag.position.z > -1146)
                    {
                        _toDrag.position = new Vector3(_toDrag.position.x, _toDrag.position.y, -1148);
                    }
                    if(_toDrag.position.z < -1166)
                    {
                        _toDrag.position = new Vector3(_toDrag.position.x, _toDrag.position.y, -1164);
                    }
                    _toDrag.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionX;
                    _toDrag.gameObject.tag = "Untagged";
                    _leftOriginalPosition = _tempLocation;
                    _isLeft = true;
                    _toDragObjectLeft = _toDrag.gameObject;
                }
                else if (_toDrag.position.x >= 388 && !_isRight)
                {
                    // fine tune dropping position
                    if(_toDrag.position.z > -1146)
                    {
                        _toDrag.position = new Vector3(_toDrag.position.x, _toDrag.position.y, -1148);
                    }
                    if(_toDrag.position.z < -1166)
                    {
                        _toDrag.position = new Vector3(_toDrag.position.x, _toDrag.position.y, -1164);
                    }
                    if(_toDrag.position.x > 400)
                    {
                        _toDrag.position = new Vector3(398, _toDrag.position.y, _toDrag.position.z);
                    }
                    _toDrag.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionX;
                    _toDrag.gameObject.tag = "Untagged";
                    _rightOriginalPosition = _tempLocation;
                    _isRight = true;
                    _toDragObjectRight = _toDrag.gameObject;
                }
            }
        }

        if (_isLeft && _isRight)
        {
            // check the id of the objects if matches then destroy them
            if(_toDragObjectLeft.GetComponent<ObjectID>().id == _toDragObjectRight.GetComponent<ObjectID>().id)
            {
                _toDragObjectLeft.GetComponent<ObjectID>().OnSpecificEvent -= ReactToCollision;
                _toDragObjectRight.GetComponent<ObjectID>().OnSpecificEvent -= ReactToCollision;
                _toDragObjectLeft.transform.DOKill();
                Destroy(_toDragObjectLeft);
                OnObjectDestroyed(_toDragObjectLeft); // ObjectController'a haber veriyoruz
                _toDragObjectRight.transform.DOKill();
                Destroy(_toDragObjectRight);
                OnObjectDestroyed(_toDragObjectRight);
                ObjectCounter = ObjectCounter - 2;
                if(SaveGame.Instance.GeneralData.IsSoundEffectsOn)
                {    
                    AudioManager.instance.PlaySoundEffect("matched_2");
                }
            }
            else{
                StartCoroutine(WaitForSecondsAndThrowObjects());
            }

            _toDrag = null;
            _isLeft = false;
            _isRight = false;
        }
        
    }

    void GeneratePosition()
    {
        _xCoor = _random.Next(356, 420);
        _zCoor = _random.Next(-1125, -1040);
    }

    private IEnumerator WaitForSecondsAndThrowObjects()
    {
        // Wait for 2 seconds
        yield return new WaitForSeconds(0.2f);

        _toDragObjectLeft.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        _toDragObjectRight.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        _toDragObjectLeft.gameObject.tag = "cube";
        _toDragObjectRight.gameObject.tag = "cube";
        GeneratePosition();
        _toDragObjectLeft.transform.position = new Vector3(_xCoor, _leftOriginalPosition.y, _zCoor);
        GeneratePosition();
        _toDragObjectRight.transform.position = new Vector3(_xCoor, _rightOriginalPosition.y, _zCoor); 

        _toDragObjectLeft = null;
        _toDragObjectRight = null;
    }
    
    void OnObjectDestroyed(GameObject gameObject)
    {
        ObjectController.MarkObjectDestroyed(gameObject);
    }
    
    public void DecreaseObjectCounter()
    {
        ObjectCounter = ObjectCounter - 2;
    }

    public void ClearHole()
    {
        if(_isLeft){
            _toDragObjectLeft.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            _toDragObjectLeft.gameObject.tag = "cube";
            GeneratePosition();
            _toDragObjectLeft.transform.position = new Vector3(_xCoor, _leftOriginalPosition.y, _zCoor);
            _toDragObjectLeft = null;
            _isLeft = false;
        }
        if(_isRight){
            _toDragObjectRight.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            _toDragObjectRight.gameObject.tag = "cube";
            GeneratePosition();
            _toDragObjectRight.transform.position = new Vector3(_xCoor, _rightOriginalPosition.y, _zCoor); 
            _toDragObjectRight = null;
            _isRight = false;
        }

        _toDrag = null;

    }

    public void GameOver()
    {
        SaveGame.Instance.GeneralData.IsGameOver = true;
        _isGameOver = true;
    }

    public void CheckLeftHole(Transform toDrag)
    {
        if(!_isLeft)
        {
            // fine tune dropping position
            if(_toDrag.position.x < 376)
            {
                _toDrag.position = new Vector3(378, toDrag.position.y, toDrag.position.z);
            }
            if(_toDrag.position.z > -1146)
            {
                _toDrag.position = new Vector3(toDrag.position.x, toDrag.position.y, -1148);
            }
            if(_toDrag.position.z < -1166)
            {
                _toDrag.position = new Vector3(toDrag.position.x, toDrag.position.y, -1164);
            }
            toDrag.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionX;
            toDrag.gameObject.tag = "Untagged";
            _leftOriginalPosition = _tempLocation;
            _toDragObjectLeft = toDrag.gameObject;
            _isLeft = true;
        }
    }

    public void CheckRightHole(Transform toDrag)
    {
        if(!_isRight)
        {
            // fine tune dropping position
            if(_toDrag.position.z > -1146)
            {
                _toDrag.position = new Vector3(toDrag.position.x, toDrag.position.y, -1148);
            }
            if(_toDrag.position.z < -1166)
            {
                _toDrag.position = new Vector3(toDrag.position.x, toDrag.position.y, -1164);
            }
            if(_toDrag.position.x > 400)
            {
                _toDrag.position = new Vector3(398, toDrag.position.y, toDrag.position.z);
            }
            toDrag.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionX;
            toDrag.gameObject.tag = "Untagged";
            _rightOriginalPosition = _tempLocation;
            _toDragObjectRight = toDrag.gameObject;
            _isRight = true;
        }
    }
    
    public void ReactToCollision(GameObject prefabObject){
        if(prefabObject == null){
            return;
        }
        if(_toDragObjectLeft){
            if(prefabObject == _toDragObjectLeft){
                return;
            }
        }
        if(_toDragObjectRight){
            if(prefabObject == _toDragObjectRight){
                return;
            }
        }
        if(_toDrag != null && !_dragging){
            if(prefabObject == _toDrag.gameObject){
                if(prefabObject.transform.position.x < 388){
                    CheckLeftHole(prefabObject.transform);
                }
                else if (prefabObject.transform.position.x >= 388){
                    CheckRightHole(prefabObject.transform);
                }
                if (_isLeft && _isRight)
                {
                    // check the id of the objects if matches then destroy them
                    if(_toDragObjectLeft.GetComponent<ObjectID>().id == _toDragObjectRight.GetComponent<ObjectID>().id)
                    {
                        _toDragObjectLeft.GetComponent<ObjectID>().OnSpecificEvent -= ReactToCollision;
                        _toDragObjectRight.GetComponent<ObjectID>().OnSpecificEvent -= ReactToCollision;
                        _toDragObjectLeft.transform.DOKill();
                        Destroy(_toDragObjectLeft);
                        OnObjectDestroyed(_toDragObjectLeft); // ObjectController'a haber veriyoruz
                        
                        _toDragObjectRight.transform.DOKill();
                        Destroy(_toDragObjectRight);
                        OnObjectDestroyed(_toDragObjectRight);
                        ObjectCounter = ObjectCounter - 2;
                        if(SaveGame.Instance.GeneralData.IsSoundEffectsOn)
                        {    
                            AudioManager.instance.PlaySoundEffect("matched_2");
                        }
                    }
                    else{
                        StartCoroutine(WaitForSecondsAndThrowObjects());
                    }

                    _toDrag = null;
                    _isLeft = false;
                    _isRight = false;
                }
            }
        }
        
    }

}
