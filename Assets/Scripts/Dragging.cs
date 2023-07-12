using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragging : MonoBehaviour
{
    private int counter = 10; // I have created 10 twin objects. I want to check if all of them are destroyed.
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
                    dist = hit.transform.position.y - Camera.main.transform.position.y; // Use Y position for the distance
                    v3 = new Vector3(pos.x, pos.y, dist);
                    v3 = Camera.main.ScreenToWorldPoint(v3);
                    tempLocation = toDrag.position;
                    offset = toDrag.position - v3;
                    dragging = true;
                }
            }
        }

        if (dragging && touch.phase == TouchPhase.Moved)
        {
            if (toDrag == null)
            {
                dragging = false;
                return;
            }

            v3 = new Vector3(Screen.width - Input.mousePosition.x, Screen.height - Input.mousePosition.y, dist); // Invert the mouse input direction
            v3 = Camera.main.ScreenToWorldPoint(v3);
            toDrag.position = new Vector3(v3.x, tempLocation.y, v3.z); // Keep the Y position unchanged
        }

        if (dragging && (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled))
        {
            dragging = false;

            float distanceLeft = Vector3.Distance(toDrag.position, leftHole.position);
            float distanceRight = Vector3.Distance(toDrag.position, rightHole.position);
            Debug.Log(distanceLeft);

            if (distanceLeft <= 14 && !isLeft)
            {
                //toDrag.GetComponent<BoxCollider>().enabled = false;
                toDrag.position = new Vector3(leftHole.position.x, leftHole.position.y , leftHole.position.z);
                leftOriginalPosition = tempLocation;
                isLeft = true;
                toDragObjectLeft = toDrag.gameObject;
            }
            else if (distanceRight <= 14 && !isRight)
            {
                //toDrag.GetComponent<BoxCollider>().enabled = false;
                toDrag.position = new Vector3(rightHole.position.x, rightHole.position.y, rightHole.position.z);
                rightOriginalPosition = tempLocation;
                isRight = true;
                toDragObjectRight = toDrag.gameObject;
            }
        }

        if (isLeft && isRight)
        {
            // check the id of the objects if matches then destroy them
            if(toDragObjectLeft.GetComponent<ObjectID>().id == toDragObjectRight.GetComponent<ObjectID>().id)
            {
                Destroy(toDragObjectLeft);
                Destroy(toDragObjectRight);
                counter--;
            }
            else{
                toDragObjectLeft.transform.position = new Vector3(leftOriginalPosition.x, leftOriginalPosition.y, leftOriginalPosition.z); // Keep the Y position unchanged
                toDragObjectRight.transform.position = new Vector3(rightOriginalPosition.x, rightOriginalPosition.y, rightOriginalPosition.z); // Keep the Y position unchanged
                //toDragObjectLeft.GetComponent<BoxCollider>().enabled = true;
                //toDragObjectRight.GetComponent<BoxCollider>().enabled = true;
            }

            isLeft = false;
            isRight = false;
        }
        if (counter == 0)
        {
            Debug.Log("You Win!");
        }
    }
}
