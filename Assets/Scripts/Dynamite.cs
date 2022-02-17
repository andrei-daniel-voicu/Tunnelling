using UnityEngine;

public class Dynamite : MonoBehaviour
{

    public float explodeTime;
    public float elapsedTime;
    bool nearWall;
    Collider2D wallCol;
    Animator anim;
    public bool toExplode;
    bool isTimer;
    bool isExploding;
    void Start()
    {
        anim = GetComponent<Animator>();
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Wall")
        {
            
            wallCol = col;
            nearWall = true;
        }
        else if(col.tag=="WallFromPlayer")
        {
            print("este in col");
        }
    }
    void OnTriggerStay2D(Collider2D col)
    {
       
        if (col.tag == "Wall")
        {
            wallCol = col;
            nearWall = true;
        }
    }
    void Update()
    {
        if (toExplode)
        {
            isTimer = true;

            anim.SetBool("isTimer", isTimer);
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= explodeTime)
            {
                isTimer = false;
                anim.SetBool("isTimer", isTimer);
                if(elapsedTime>=explodeTime+0.8f)
                {
                    Destroy(this.gameObject);
                    if (nearWall)
                    {
                        Destroy(wallCol.gameObject);
                       // nearWall = false;
                    }
                    PlayerController.used = false;
                }
            }
        }
    }
}
