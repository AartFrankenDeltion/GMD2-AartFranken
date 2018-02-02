using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineList : MonoBehaviour {

    public List<GameObject> lijnList = new List<GameObject>();
    public GameObject selectedLine;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetButton("Z"))
        {
            selectedLine = lijnList[1];
        }
        else if (Input.GetButton("X"))
        {
            selectedLine = lijnList[2];
        }
        else if (Input.GetButton("C"))
        {
            selectedLine = lijnList[3];
        }
        else
        {
            selectedLine = lijnList[0];
        }

    }
}
