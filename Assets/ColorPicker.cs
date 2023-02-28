using System.Collections;
using System.Collections.Generic;
using System.IO.Compression;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class ColorPicker : MonoBehaviour
{
    public Object pegObject;
    public HoleColor color;
    public bool lastFramePickedUp;
    public Game game;
    public Vector2 lastTouch;

    bool wasTouchLastFrame = false;
    public GameObject objectInHand;

	// Update is called once per frame
	void Update()
    {
        Vector2 position = new();
        bool isInput = false;
        if (Input.touchCount > 0 && !wasTouchLastFrame)
        {
            position = Input.GetTouch(0).position;
            isInput = true;
        }
        else if (Input.GetMouseButtonDown(0))
		{
            position = Input.mousePosition;
            isInput = true;
        }
        if (isInput)
        {
            //Debug.Log($"input at {position}");
            RaycastHit2D hit;
            Vector3 origin = Camera.main.ScreenToWorldPoint(position);
            Vector3 direction = Camera.main.transform.forward;
            //Debug.DrawLine(origin, origin + direction * 10, Color.red, 10);
            hit = Physics2D.Raycast(origin, direction);
            if (hit.transform == this.transform)
            {
                //Debug.Log($"hit object {hit.transform.gameObject.name}");
                Vector3 spawnPoint = Camera.main.ScreenToWorldPoint(position);
                spawnPoint.z = -2f;

                Object newObject = Instantiate(pegObject);
                objectInHand = (GameObject)newObject;
                objectInHand.GetComponent<PegMovement>().color = color;
                objectInHand.GetComponent<MeshRenderer>().material = game.MaterialFromHoleColor(color);
            }
            else
            {
                //Debug.Log("did not hit object");
            }
        }

		if (Input.touchCount != 0)
			wasTouchLastFrame = true;
		else
			wasTouchLastFrame = false;
	}
}