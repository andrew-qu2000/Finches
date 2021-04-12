using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SeedControl : MonoBehaviour
{
    public const int IN_BUSH = 0;
    public const int IN_BEAK = 1;
    public const int IN_NEST = 2;
    public const int FALLING = 3;

    public string size;
    public float speed;
    public float timeToFall;
    public int state;
    private Rigidbody2D rigidbody2D;
    private GameObject finch;
    
    public void SetFinch(GameObject finch)
    {
        this.finch = finch;
        if (finch)
        {
            UpdateState(IN_BEAK, 5);
        }
        else
        {
            UpdateState(FALLING, 0);
        }
    }

    public void UpdateState(int state, int timeToFall)
    {
        this.state = state;
        if (state == IN_BUSH)
        {
            this.timeToFall = timeToFall;
            rigidbody2D.gravityScale = 0;
            rigidbody2D.velocity = Vector2.zero;
        }
        if (state == IN_BEAK)
        {
            this.timeToFall = timeToFall;
            rigidbody2D.gravityScale = 0;
            rigidbody2D.velocity = Vector2.zero;
        }
        if (state == IN_NEST)
        {
            this.timeToFall = timeToFall;
            rigidbody2D.gravityScale = 0;
            rigidbody2D.velocity = Vector2.zero;
        }
        if (state == FALLING)
        {
            rigidbody2D.gravityScale = 1;
        }
        //Debug.Log("UPDATE STATE!\nState " + state + ", timeToFall " + timeToFall);
    }

    

    // Start is called before the first frame update
    void Start()
    {
        speed = 10f;
        // QualitySettings.vSyncCount = 1;

        rigidbody2D = GetComponent<Rigidbody2D>();
        UpdateState(IN_BUSH, 3600);
        // rigidbody2D.freezePosition = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (state == IN_BEAK)
        {
            timeToFall -= Time.deltaTime;
            if (timeToFall < 0)
            {
                UpdateState(FALLING, 0);
            }

            Vector3 finchPos = finch.transform.position;
            transform.position = finchPos + FinchBeakControl.offset;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Nest")
        {
            Debug.Log("(Seed) Trigger entered with speed: " + rigidbody2D.velocity.y);
            if (rigidbody2D.velocity.y > -6)
            {
                UpdateState(IN_NEST, 100);
                Debug.Log("(Seed) caught by nest");
            }
            Debug.Log("(Seed) too fast to be caught by nest");
            //GetComponent<CapsuleCollider2D>().isTrigger = false;
        }
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
