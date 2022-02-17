using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    GameObject player;       //Public variable to store a reference to the player game object
    public float offsetY;


    // Use this for initialization
    void Start()
    {
        //Calculate and store the offset value by getting the distance between the player's position and camera's position.
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // LateUpdate is called after Update each frame
    void LateUpdate()
    {
        // Set the position of the camera's transform to be the same as the player's, but offset by the calculated offset distance.
        transform.position = new Vector3(transform.position.x, player.transform.position.y+offsetY, transform.position.z);
    }
}