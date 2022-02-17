using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundUpdate : MonoBehaviour {

    private void LateUpdate()
    {
        this.transform.position = new Vector3(Camera.main.transform.position.x,Camera.main.transform.position.y,0);
    }
}
