using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightMovement : MonoBehaviour {

    public SpriteRenderer spriteRenderer;

    public Vector3 leftPoz;
    public Vector3 rotationLeft;
    public Vector3 rightPoz;
    public Vector3 rotationRight;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(spriteRenderer.flipX == false)
        {
            transform.localPosition = leftPoz;
            transform.localRotation = Quaternion.Euler(rotationLeft);
        }
        else
        {
            transform.localPosition = rightPoz;
            transform.localRotation = Quaternion.Euler(rotationRight);
        }
	}
}
