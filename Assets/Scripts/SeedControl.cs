using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SeedControl : MonoBehaviour
{
    public const int IN_BUSH = 0;
    public const int IN_BEAK = 1;
    public const int IN_NEST = 2;
    public const int FALLING = 3;

    public const int SMALL = 0;
    public const int MEDIUM = 1;
    public const int LARGE = 2;
    
    public float speed;
    public float timeToFall;
    public int size;
    public int state;
    private Rigidbody2D rigidbody2D;
    private GameObject finch;
    public Vector3 offset;
    
    public void SetFinch(GameObject finch, Vector3 offset)
    {
        this.offset = offset;
        this.finch = finch;
        if (finch)
        {
            int sizeDiff = this.size - finch.GetComponent<BeakSize>().size;
            float timeToFall = (Random.Range(2f, 3f) - sizeDiff) / 2;
            // sizeDiff | timeToFall
            // ---------+-----------
            // 0        | 1 - 1.5
            // 1        | .5 - 1
            // 2        | 0 - .5
            UpdateState(IN_BEAK, timeToFall);
        }
        else
        {
            UpdateState(FALLING, 0);
        }
    }

    public void UpdateState(int state, float timeToFall)
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
            this.finch.GetComponent<FinchBeakControl>().seed = null;
            this.finch = null;
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
            else
            {
                Vector3 finchPos = finch.transform.position;
                transform.position = finchPos + offset;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Nest")
        {
            Debug.Log("(Seed) Trigger entered with speed: " + rigidbody2D.velocity.y);
            //GetComponent<CapsuleCollider2D>().isTrigger = false;
        }
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
