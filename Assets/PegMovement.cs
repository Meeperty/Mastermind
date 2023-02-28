using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PegMovement : MonoBehaviour
{
	public Vector2 lastTouch;
	public bool lastFramePickedUp;
	public Game game;
	public HoleColor color;

	bool wasTouchLastFrame = false;

	void Awake()
	{
		game = FindObjectOfType<Game>();
		lastFramePickedUp = true;
	}

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
			RaycastHit hit;
			Vector3 origin = Camera.main.ScreenToWorldPoint(position);
			Vector3 direction = Camera.main.transform.forward;
			Debug.DrawLine(origin, origin + direction * 10, Color.red, 10);
			if (Physics.Raycast(origin, direction, out hit))
			{
				if (hit.transform == this.transform)
					lastFramePickedUp = true;
			}
			else
			{
				//Debug.Log("did not hit object");
			}
		}
		if (lastFramePickedUp && Input.touchCount > 0)
		{
			float moveXValue = (Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position)).x;
			float moveYValue = (Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position)).y;

			this.transform.position = new Vector3(moveXValue, moveYValue, -2f);
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
				this.transform.position = closestHole.position;
			}
			else
				Destroy(this.gameObject);
			lastFramePickedUp = false;
		}

		if (Input.touchCount != 0)
			wasTouchLastFrame = true;
		else
			wasTouchLastFrame = false;
	}
}
