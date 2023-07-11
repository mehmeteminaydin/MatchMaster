using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Dragging : MonoBehaviour
{
    private float dist;
    private bool dragging = false;
    private bool isLeft = false;
    private bool isRight = false;

    private GameObject toDragObjectLeft;
    private GameObject toDragObjectRight;

    private Vector3 leftOriginalPosition;
    private Vector3 rightOriginalPosition;

    private Vector3 tempLocation;

    private Vector3 offset;
    private Transform toDrag;
    public Transform leftHole;
    public Transform rightHole;
    // Update is called once per frame
    void Update()
    {
        
        Vector3 v3;
        if (Input.touchCount != 1)
        {
            dragging = false;
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
                    toDrag = hit.transform;
                    dist = hit.transform.position.z - Camera.main.transform.position.z;
                    v3 = new Vector3(pos.x, pos.y, dist);
                    v3 = Camera.main.ScreenToWorldPoint(v3);
                    // keep the first touch position in tempLocation
                    tempLocation = toDrag.position;
                    offset = toDrag.position - v3;
                    dragging = true;
                }
            }

        }
        if (dragging && touch.phase == TouchPhase.Moved)
        {
            if(toDrag == null)
            {
                dragging = false;
                return;
            }
            v3 = new Vector3(Input.mousePosition.x, Input.mousePosition.y, dist);
            v3 = Camera.main.ScreenToWorldPoint(v3);
            toDrag.position = v3 + offset;

            

        }
        

        if (dragging && (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled))
        {
            dragging = false;

            float distanceLeft = Vector3.Distance(toDrag.position, leftHole.position);
            float distanceRight = Vector3.Distance(toDrag.position, rightHole.position);

            if (distanceLeft <= 6.0f && (isLeft == false))
            {
                toDrag.GetComponent<BoxCollider>().enabled = false;
                toDrag.position = leftHole.position;
                leftOriginalPosition = tempLocation;
                isLeft = true;
                toDragObjectLeft = toDrag.gameObject;

            }
            else if (distanceRight <= 6.0f && (isRight == false))
            {
                toDrag.GetComponent<BoxCollider>().enabled = false;
                toDrag.position = rightHole.position;
                rightOriginalPosition = tempLocation;
                isRight = true;
                toDragObjectRight = toDrag.gameObject;

            }
        }
        if( isLeft && isRight)
        {
            

                //Destroy(toDragObjectLeft);
                //Destroy(toDragObjectRight);
            toDragObjectLeft.transform.position = leftOriginalPosition;
            toDragObjectRight.transform.position = rightOriginalPosition;
            toDragObjectLeft.GetComponent<BoxCollider>().enabled = true;
            toDragObjectRight.GetComponent<BoxCollider>().enabled = true;

            isLeft = false;
            isRight = false;


        }
    }
}
