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

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCheckButtonClick()
    {
        Debug.Log("Button Clicked");
    }
}
