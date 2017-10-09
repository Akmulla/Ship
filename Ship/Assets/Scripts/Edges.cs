using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Edges : MonoBehaviour {
    
        public static float leftEdge, rightEdge, botEdge, topEdge;
    public RectTransform leftImg;


        void Awake()
        {
        //leftEdge = Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x;
        Vector3 pos= new Vector3(leftImg.rect.xMax,0.0f,0.0f);
        leftEdge = Camera.main.ScreenToWorldPoint(leftImg.TransformPoint(pos)).x; 
            rightEdge = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x;
            topEdge = Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)).y;
            botEdge = Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).y;
        }

}
