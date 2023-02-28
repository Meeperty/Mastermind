using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    public Hole[,] holes = new Hole[10, 4];
    public Hole[] code = new Hole[4];

    public Material white;
    public Material yellow;
    public Material orange;
    public Material green;
    public Material pink;
    public Material purple;

    readonly Vector3 topLeft = new Vector3(2.588f, 8.127f, -1.709f);
    public float hDistanceBlender = 1.325f;
    public float holeDistance;

    // Start is called before the first frame update
    void Start()
    {
		holeDistance = 0.63f * hDistanceBlender;
		for (int i = 0; i < 10; i++)
		{
			for (int j = 0; j < 4; j++)
			{
                holes[i,j] = new Hole();
				float xValue = topLeft.x + (j * holeDistance);
				float yValue = topLeft.y + (i * -holeDistance);
				float zValue = topLeft.z;
                holes[i, j].position = new Vector3(xValue, yValue, zValue);
                holes[i, j].color = HoleColor.Empty;
                holes[i, j].enabled = true;
			}
		}
	}

	// Update is called once per frame
	void Update()
    {
        
    }

    public void OnCheckButtonClick()
    {
        //Debug.Log("Button Clicked");
    }

    public Hole GetClosestHole(Vector2 position)
    {
        Vector2Int closestHole = new Vector2Int(0, 0);
        float closestHoleDist = float.PositiveInfinity;
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if (holes[i, j].enabled && holes[i, j].color == HoleColor.Empty)
                {
                    Vector2 diference = position - (Vector2)holes[i, j].position;
                    float distance = diference.magnitude;
                    if(distance < closestHoleDist)
                    {     
                        closestHole = new Vector2Int(i, j); 
                        closestHoleDist = distance;

				    }   
                }
            }
        }
        Debug.Log($"closest hole was {closestHole} at {holes[closestHole.x, closestHole.y].position}");
        return holes[closestHole.x, closestHole.y];
    }




}