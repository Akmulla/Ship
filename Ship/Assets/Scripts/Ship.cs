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
    ShipMove shipMove;
    public int Price { get; set; }
    public ShipState State { get; set; }
    public LayerMask mask;

    void Awake()
    {
        ship = this;
    }

    void Start ()
    {
        anim = GetComponentInChildren<Animator>();
        State = ShipState.Idle;
        rb = GetComponent<Rigidbody2D>();
        Price = 200;
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("DangerZone"))
        {
            State=ShipState.Dead;
            anim.SetTrigger("Destroy");
            Destroy(gameObject);
        }

        if (coll.gameObject.CompareTag("Port"))
        {
            GameController.gc.ShipReached(true, Price);
            Destroy(gameObject);
        }
        
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
            RotateBackToPoint((mousePosition - start_position).normalized * maxRange * 2.0f);
            //RotateToPoint((mousePosition - start_position).normalized * maxRange * 2.0f);
            if ((mousePosition - start_position).magnitude < maxRange)
            {
                transform.position = mousePosition;
                
            }
            else
            {
                transform.position = (mousePosition - start_position).normalized * maxRange;
                
            }


        }

        if (Input.GetMouseButtonUp(0) && (State == ShipState.Prepare))
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
            // rb.simulated = true;
            rb.gravityScale = 1.0f;
            rb.AddForce((start_position - transform.position) * forceCoeff, ForceMode2D.Impulse);
            State = ShipState.Sail;
            
        }

        if (State == ShipState.Sail)
        {
            RotateToPoint((Vector2)transform.position + rb.velocity);
        }

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

    void Update()
    {
        Move();
        float offset = 2.5f;
        Vector3 pos = transform.position;


        //if ((pos.y > Edges.topEdge + offset) || (pos.y < Edges.botEdge - offset)
        //    || (pos.x > Edges.rightEdge + offset) || (pos.x < Edges.leftEdge - offset))
        //{
        //    if (pos.y > Edges.topEdge + offset)
        //    {
        //        float AB = transform.position.y;
        //        float AC=
        //    }
        //}


        //if ( (pos.y > Edges.topEdge + offset)||( pos.y < Edges.botEdge - offset)
        //    || (pos.x > Edges.rightEdge + offset)|| (pos.x < Edges.leftEdge - offset) )
        //{

        //Vector2 v;
        //Vector2 res;
        //float k;
        //float newY;
        //if (pos.y > Edges.topEdge + offset+1.0f)
        //{
        //    newY = Mathf.Abs((Edges.topEdge + offset) - (Edges.botEdge - offset));
        //    v = rb.velocity;
        //    //k = v.y / v.x;
        //    k = v.y / v.x;
        //    //res = new Vector2(v.x - k * v.x, v.y - v.y);
        //    res = new Vector2(v.x - k * v.x, v.y - newY);

        //    transform.position = res;
        //}

        //if (pos.y < Edges.botEdge - offset-1.0f)
        //{
        //    newY = Mathf.Abs((Edges.topEdge + offset) - (Edges.botEdge - offset));
        //    v = rb.velocity;
        //    //k = v.y / v.x;
        //    k = v.y / v.x;
        //    //res = new Vector2(v.x - k * v.x, v.y - v.y);
        //    res = new Vector2(v.x - k * v.x, v.y + newY);

        //    transform.position = res;
        //}

        //}
        //Vector2 v = rb.velocity;
        //float k = v.y / v.x;

        //Vector2 res = new Vector2(v.x - k * v.y, v.y - v.y);
        //print(res);
        //Debug.DrawLine(Vector2.zero, res,Color.red,0.1f);
        if (pos.y > Edges.topEdge + offset)
            transform.position = new Vector3(pos.x, Edges.botEdge - offset, pos.z);

        if (pos.y < Edges.botEdge - offset)
            transform.position = new Vector3(pos.x, Edges.topEdge + offset, pos.z);

        if (pos.x > Edges.rightEdge + offset)
            transform.position = new Vector3(Edges.leftEdge - offset, pos.y, pos.z);

        if (pos.x < Edges.leftEdge - offset)
            transform.position = new Vector3(Edges.rightEdge + offset, pos.y, pos.z);


    }
}
