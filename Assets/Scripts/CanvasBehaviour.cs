﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        // tell References I am the canvas
        References.canvas = gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
