using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    public float fallTime;
    public float reappearTime;
    bool isFalling;
    bool isDisabled;
    float elapsedTime;
    float elapsedTime1;

    Animator anim;
    SpriteRenderer sprite;
    Collider2D col;

    void Start()
    {
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
        anim.SetFloat("fallingTime", 1 / fallTime);
    }
    void Update()
    {
        anim.SetBool("isFalling", isFalling);

        if (isFalling == true)
        {
           // anim.Play("Destroy", 0);
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= fallTime)
            {
                if (anim.GetCurrentAnimatorStateInfo(0).IsName("Destroy"))
                {
                    sprite.enabled = false;
                    col.enabled = false;
                    isDisabled = true;
                    isFalling = false;
                    elapsedTime = 0;
                }
            }
        }
        if (isDisabled)
        {
            sprite.enabled = false;
            elapsedTime1 += Time.deltaTime;
            if (elapsedTime1 >= reappearTime)
            {
                sprite.enabled = true;
                col.enabled = true;
                elapsedTime1 = 0;
                isDisabled = false;
                
            }
        }

    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            isFalling = true;
        }
    }

}
