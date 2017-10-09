using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ShipState { Idle, Prepare, Sail,Dead };

public class Ship : MonoBehaviour
{
    public static Ship ship;
    Rigidbody2D rb;
    Vector3 start_position;
    public float maxRange = 2.0f;
    public float forceCoeff = 1.0f;
    Animator anim;
    //ShipMove shipMove;
    public int Price { get; set; }
    public ShipState State { get; set; }
    //public LayerMask mask;

    void Awake()
    {
        ship = this;
    }

    void Start ()
    {
        anim = GetComponentInChildren<Animator>();
        State = ShipState.Idle;
        rb = GetComponent<Rigidbody2D>();
        start_position = transform.position;
        Price = 200;
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("DangerZone"))
        {
            //if (State != ShipState.Dead)
            {
                anim.SetTrigger("Destroy");
                State = ShipState.Dead;
            }
           

            //Destroy(gameObject);
        }

        if (coll.gameObject.CompareTag("Port"))
        {
            GameController.gc.ShipReached(true, Price);
            Destroy(gameObject);
        }
        
    }


    IEnumerator DestroyDelay()
    {
        yield return new WaitForSeconds(5.0f);
        anim.SetTrigger("Destroy");
        State = ShipState.Dead;
    }

    public void Move()
    {
        if (GameController.gc.State != GameState.Game)
            return;

        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        mousePosition.z = 0.0f;
        if ((State == ShipState.Idle))
        {
            // rb.simulated = false;
            rb.gravityScale = 0.0f;
        }
        if ((Input.GetMouseButtonDown(0)) && (State == ShipState.Idle))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

            // print(hit.collider);
            if (hit.collider != null)
            {
                if (hit.collider.gameObject.CompareTag("Ship"))
                {
                    State = ShipState.Prepare;
                    rb.bodyType = RigidbodyType2D.Kinematic;
                }

            }
        }

        if ((Input.GetMouseButton(0)) && (State == ShipState.Prepare))
        {
            RotateBackToPoint(start_position + (mousePosition - start_position).normalized * maxRange * 2.0f);
            //RotateToPoint((mousePosition - start_position).normalized * maxRange * 2.0f);
            if ((mousePosition - start_position).magnitude < maxRange)
            {
                transform.position = mousePosition;
                
            }
            else
            {
                transform.position = start_position+(mousePosition - start_position).normalized * maxRange;
               
            }
            //Debug.DrawLine(mousePosition, start_position);

        }

        if (Input.GetMouseButtonUp(0) && (State == ShipState.Prepare))
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
            // rb.simulated = true;
            rb.gravityScale = 1.0f;
            rb.AddForce((start_position - transform.position) * forceCoeff, ForceMode2D.Impulse);
            State = ShipState.Sail;
            StartCoroutine(DestroyDelay());
            
        }

        //if (State == ShipState.Sail)
        //{
        //    RotateToPoint((Vector2)transform.position + rb.velocity);
        //}

        if (State == ShipState.Dead)
        {
            rb.velocity = Vector2.zero;
        }
    }

    //public void Throw()
    //{

    //}
    public void RotateToPoint(Vector3 point)
    {
        point.z = 0;
        float angle = Vector2.Angle(Vector2.right, point - transform.position);
        //transform.rotation = Quaternion.Lerp(transform.rotation,
        //        Quaternion.AngleAxis(angle, Vector3.forward), 0.5f);
        transform.eulerAngles = new Vector3(0f, 0f, transform.position.y < point.y ? angle : -angle);
    }

    public void RotateBackToPoint(Vector3 point)
    {
        point.z = 0;
        float angle = Vector2.Angle(Vector2.right, point - transform.position);
        //transform.eulerAngles = new Vector3(0f, 0f, transform.position.y < point.y ? angle : -angle);
        transform.eulerAngles = new Vector3(0f, 0f,
            transform.position.y < point.y ? angle + 180.0f : -angle + 180.0f);
    }
    void FixedUpdate()
    {
        if (State == ShipState.Sail)
        {
            RotateToPoint((Vector2)transform.position + rb.velocity);
        }
    }
    
    void Update()
    {
        Move();
        float offset = 2.5f;
        Vector3 pos = transform.position;

        float x0 = transform.position.x;
        float y0 = transform.position.y;
        float x=0;
        float y = Edges.botEdge;

        //if ((pos.y > Edges.topEdge) || (pos.y < Edges.botEdge)
        //    || (pos.x > Edges.rightEdge) || (pos.x < Edges.leftEdge))
        //{
        //    x0 = transform.position.x;
        //    y0 = transform.position.y;
            
        //    y = Edges.botEdge;
        //    x = ((y - y0) / rb.velocity.y) * rb.velocity.x+x0;
        //    transform.position= new Vector2(x, y);
        //}


        if (pos.y > Edges.topEdge+offset)
        {
            x0 = transform.position.x;
            y0 = transform.position.y;

            y = Edges.botEdge- offset;
            x = ((y - y0) / rb.velocity.y) * rb.velocity.x + x0;
            transform.position = new Vector2(x, y);
        }

        if (pos.y < Edges.botEdge- offset)

        {
            x0 = transform.position.x;
            y0 = transform.position.y;

            y = Edges.topEdge+ offset;
            x = ((y - y0) / rb.velocity.y) * rb.velocity.x + x0;
            transform.position = new Vector2(x, y);
        }

        if (pos.x > Edges.rightEdge+ offset)
        {
            x0 = transform.position.x;
            y0 = transform.position.y;

            x = Edges.leftEdge- offset;
            y = ((x - x0) / rb.velocity.x) * rb.velocity.y + y0;
            transform.position = new Vector2(x, y);
        }

        if (pos.x < Edges.leftEdge- offset)
        {
            x0 = transform.position.x;
            y0 = transform.position.y;

            x = Edges.rightEdge+ offset;
            y = ((x - x0) / rb.velocity.x) * rb.velocity.y + y0;
            transform.position = new Vector2(x, y);
        }

        //if (pos.y > Edges.topEdge + offset)
        //    transform.position = new Vector3(pos.x, Edges.botEdge - offset, pos.z);

        //if (pos.y < Edges.botEdge - offset)
        //    transform.position = new Vector3(pos.x, Edges.topEdge + offset, pos.z);

        //if (pos.x > Edges.rightEdge + offset)
        //    transform.position = new Vector3(Edges.leftEdge - offset, pos.y, pos.z);

        //if (pos.x < Edges.leftEdge - offset)
        //    transform.position = new Vector3(Edges.rightEdge + offset, pos.y, pos.z);


    }
}
