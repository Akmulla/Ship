﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Edges : MonoBehaviour {
    
        public static float leftEdge, rightEdge, botEdge, topEdge;

        void Awake()
        {
            leftEdge = Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x;
            rightEdge = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x;
            topEdge = Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)).y;
            botEdge = Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).y;
        }

}