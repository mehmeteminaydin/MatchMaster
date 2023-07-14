using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragging : MonoBehaviour
{

    public TMPro.TextMeshProUGUI TimerText;
    public float TotalTime = 300f; // Total time in seconds (5 minutes)

    public Transform LeftHole;
    public Transform RightHole;

    private int _objectCounter = 10; // I have created 10 twin objects. I want to check if all of them are destroyed.
    private float _dist;
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

    private float _currentTime;

    // Start is called before the first frame update
    void Start()
    {
        _currentTime = TotalTime;
    }
    // Update is called once per frame
    void Update()
    {

        _currentTime -= Time.deltaTime;

        // Calculate minutes and seconds
        int minutes = Mathf.FloorToInt(_currentTime / 60f);
        int seconds = Mathf.FloorToInt(_currentTime % 60f);

        //update the UI text with the current time
        TimerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        if (_currentTime <= 0)
        {
            Debug.Log("You Lose!");
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
            _toDrag.position = new Vector3(v3.x , _tempLocation.y, v3.z); // Keep the Y position unchanged
        }

        if (_dragging && (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled))
        {
            _dragging = false;

            float distanceLeft = Vector3.Distance(_toDrag.position, LeftHole.position);
            float distanceRight = Vector3.Distance(_toDrag.position, RightHole.position);

            if (distanceLeft <= 15 && !_isLeft)
            {
                _toDrag.GetComponent<Rigidbody>().detectCollisions = false;
                _toDrag.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                _toDrag.position = new Vector3(LeftHole.position.x, LeftHole.position.y , LeftHole.position.z);
                _leftOriginalPosition = _tempLocation;
                _isLeft = true;
                _toDragObjectLeft = _toDrag.gameObject;
            }
            else if (distanceRight <= 15 && !_isRight)
            {
                _toDrag.GetComponent<Rigidbody>().detectCollisions = false;
                _toDrag.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                _toDrag.position = new Vector3(RightHole.position.x, RightHole.position.y, RightHole.position.z);
                _rightOriginalPosition = _tempLocation;
                _isRight = true;
                _toDragObjectRight = _toDrag.gameObject;
            }
        }

        if (_isLeft && _isRight)
        {
            // check the id of the objects if matches then destroy them
            if(_toDragObjectLeft.GetComponent<ObjectID>().id == _toDragObjectRight.GetComponent<ObjectID>().id)
            {
                Destroy(_toDragObjectLeft);
                Destroy(_toDragObjectRight);
                _objectCounter--;
            }
            else{
                _toDragObjectLeft.GetComponent<Rigidbody>().detectCollisions = true;
                _toDragObjectLeft.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                _toDragObjectRight.GetComponent<Rigidbody>().detectCollisions = true;
                _toDragObjectRight.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                _toDragObjectLeft.transform.position = new Vector3(_leftOriginalPosition.x, _leftOriginalPosition.y, _leftOriginalPosition.z);
                _toDragObjectRight.transform.position = new Vector3(_rightOriginalPosition.x, _rightOriginalPosition.y, _rightOriginalPosition.z); 
            }

            _isLeft = false;
            _isRight = false;
        }
        if (_objectCounter == 0)
        {
            Debug.Log("You Win!");
        }
    }
}
