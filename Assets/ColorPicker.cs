using System.Collections;
using System.Collections.Generic;
using System.IO.Compression;
using UnityEngine;
using UnityEngine.UIElements;

public class ColorPicker : MonoBehaviour
{
    public Object pegObject;
    public HoleColor color;
    public bool lastFramePickedUp;
    public Game game;
    public Vector2 lastTouch;

    bool touch;
    public GameObject objectInHand;
    // Start is called before the first frame update
    void Start()
    {
        touch = Input.touchSupported;
    }


	private void FixedUpdate()
	{
        
        if (Input.touchCount > 0)
        {
            Debug.Log(Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position));
        }
        
	}

	// Update is called once per frame
	void Update()
    {
        Vector2 position = new();
        bool isInput = false;
        if (touch)
        {
            if (Input.touchCount > 0)
            {
                position = Input.GetTouch(0).position;
                isInput = true;
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                position = Input.mousePosition;
                isInput = true;
            }
        }
        if (isInput)
        {
            //Debug.Log($"input at {position}");
            RaycastHit2D hit;
            Vector3 origin = Camera.main.ScreenToWorldPoint(position);
            Vector3 direction = Camera.main.transform.forward;
            Debug.DrawLine(origin, origin + direction * 10, Color.red, 10);
            hit = Physics2D.Raycast(origin, direction);
            if (hit.transform == this.transform)
            {
                //Debug.Log($"hit object {hit.transform.gameObject.name}");
                Vector3 spawnPoint = Camera.main.ScreenToWorldPoint(position);
                spawnPoint.z = -2f;

                Object newObject = Instantiate(pegObject);
                objectInHand = (GameObject)newObject;
            }
            else
            {
                //Debug.Log("did not hit object");
            }
        }
        if (Input.touchCount > 0)
        {
            float moveXValue = (Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position)).x;
            float moveYValue = (Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position)).y;

            objectInHand.transform.position = new Vector3(moveXValue, moveYValue, -2f);
            lastFramePickedUp = true;
            lastTouch = Input.GetTouch(0).position;

        }
        if (lastFramePickedUp && Input.touchCount == 0)
        {
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(lastTouch);
            Hole closestHole = game.GetClosestHole(worldPosition);
			Vector2 diference = (Vector2)worldPosition - (Vector2)closestHole.position;
			float distance = diference.magnitude;
			if (distance < 1)
            {
                objectInHand.transform.position = closestHole.position;
            }
            lastFramePickedUp = false;
        }
    }
}