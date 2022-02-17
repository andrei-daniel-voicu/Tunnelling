using UnityEngine;

public class MovingPlatform : MonoBehaviour
{

    public float speed;
    public Vector2 movement;
    Vector2 defaultPos;
    bool back;
    // Use this for initialization
    void Start()
    {
        defaultPos = transform.position;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag=="Player")
        {
            col.gameObject.transform.parent = this.gameObject.transform;
        }
    }
    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            col.gameObject.transform.parent = null;
        }
    }
    // Update is called once per frame
    void Update()
    {

        if (movement.x != 0)
        {
            if (back == false)
            {
                if (transform.position.x < (defaultPos.x + movement.x))
                {
                    transform.position = new Vector3(transform.position.x + speed*Time.deltaTime, transform.position.y, transform.position.z);
                }
                else
                {

                    back = true;
                }
            }
            else
            {
                if (transform.position.x > defaultPos.x)
                {
                    transform.position = new Vector3(transform.position.x - speed*Time.deltaTime, transform.position.y, transform.position.z);
                }
                else
                {
                    back = false;
                }
            }

        }
        else
        {
            if (back == false)
            {
                if (transform.position.y < (defaultPos.y + movement.y))
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y + speed*Time.deltaTime, transform.position.z);
                }
                else
                {

                    back = true;
                }
            }
            else
            {
                if (transform.position.y > defaultPos.y)
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y - speed*Time.deltaTime, transform.position.z);
                }
                else
                {
                    back = false;
                }
            }

        }

    }
}
