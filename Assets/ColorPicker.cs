using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ColorPicker : MonoBehaviour
{
    public HoleColor color;

    bool touch;

    // Start is called before the first frame update
    void Start()
    {
        touch = Input.touchSupported;
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
			Debug.Log($"input at {position}");
            RaycastHit2D hit;
            Vector3 origin = Camera.main.ScreenToWorldPoint(position);
			Vector3 direction = Camera.main.transform.forward;
			Debug.DrawLine(origin, origin + direction * 10, Color.red, 10);
            hit = Physics2D.Raycast(origin, direction);
			if (hit.transform)
            {
                Debug.Log($"hit object {hit.transform.gameObject.name}");

            }
            else
            {
                Debug.Log("did not hit object");
            }
		}
	}
}
