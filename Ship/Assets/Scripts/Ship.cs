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
    public float length;
    //ShipMove shipMove;
    bool prepared = false;
    public GameObject anchor;

    int price;
    public ShipState State { get; set; }
 
    public LayerMask mask;
    public LayerMask boundary;

    void Awake()
    {
        ship = this;
        length = GetComponentInChildren<SpriteRenderer>().sprite.bounds.size.y*transform.localScale.x;
    }

    public int Price {
        get
        {
            return 200 + ScoreManager.sm.GetSausage() * 50;
        }
        set
        {
            price = value;
        }
    }
    
    void Start ()
    {
        anim = GetComponentInChildren<Animator>();
        State = ShipState.Idle;
        rb = GetComponent<Rigidbody2D>();
        start_position = transform.position;
        ScoreManager.sm.ResetSausage();
        //Price = 200;
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
        //if (coll.gameObject.CompareTag("Boundary"))
        //{
        //    RaycastHit2D hit = Physics2D.Raycast(transform.position, -transform.right, 100.0f, boundary);
        //    //print(hit.collider);
        //    if (hit.collider != null)
        //    {
        //        transform.position = hit.point + rb.velocity.normalized * length;
        //    }
        //}
        
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
        //print(mousePosition);
        if ((State == ShipState.Idle))
        {
            // rb.simulated = false;
            rb.gravityScale = 0.0f;
           // return;
        }
        if ((Input.GetMouseButtonDown(0)) && (State == ShipState.Idle))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction,20.0f,mask);

             //print(hit.collider);
            if (hit.collider != null)
            {
                if (hit.collider.gameObject.CompareTag("Ship"))
                {
                    State = ShipState.Prepare;
                    rb.bodyType = RigidbodyType2D.Kinematic;
                    SoundManager.sm.SingleSound(SoundSample.ShipPicked);
                    Instantiate(anchor, start_position, Quaternion.identity);
                    return;
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
                if(!prepared)
                {
                    SoundManager.sm.SingleSound(SoundSample.Prepare);
                    prepared = true;
                }
                
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
            SoundManager.sm.SingleSound(SoundSample.StartShip);
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


        //if ((pos.y>Edges.edges.topEdge)|| (pos.y < Edges.edges.botEdge)||
        //        (pos.x > Edges.edges.rightEdge)||(pos.x< Edges.edges.leftEdge))
        //{

        //    RaycastHit2D hit = Physics2D.Raycast(transform.position, -transform.right, 100.0f, boundary);
        //    print(hit.collider);
        //    if (hit.collider != null)
        //    {

        //        transform.position = hit.point+rb.velocity.normalized*length;
        //    }
        //}

        //Ray ray=Physics2D.Raycast(transform.position, (Vector2)transform.position + rb.velocity)


        //float x0 = transform.position.x;
        //float y0 = transform.position.y;
        //float x=0;
        //float y = Edges.edges.botEdge;




        //if (pos.y > Edges.edges.topEdge+offset)
        //{
        //    x0 = transform.position.x;
        //    y0 = transform.position.y;

        //    y = Edges.edges.botEdge- offset;
        //    if (rb.velocity.x != 0)
        //        x = ((y - y0) / rb.velocity.y) * rb.velocity.x + x0;
        //    else
        //        x = transform.position.x;
        //    transform.position = new Vector2(x, y+0.1f);
        //    return;
        //}

        //if (pos.y < Edges.edges.botEdge- offset)

        //{
        //    x0 = transform.position.x;
        //    y0 = transform.position.y;

        //    y = Edges.edges.topEdge+ offset;
        //    if (rb.velocity.x != 0)
        //        x = ((y - y0) / rb.velocity.y) * rb.velocity.x + x0;
        //    else
        //        x = transform.position.x;
        //    transform.position = new Vector2(x, y-0.1f);
        //    return;
        //}

        //if (pos.x > Edges.edges.rightEdge+ offset)
        //{
        //    x0 = transform.position.x;
        //    y0 = transform.position.y;

        //    x = Edges.edges.leftEdge- offset;
        //    if (rb.velocity.y != 0)
        //        y = ((x - x0) / rb.velocity.x) * rb.velocity.y + y0;
        //    else
        //        y = transform.position.y;
        //    transform.position = new Vector2(x+0.1f, y);

        //    return;
        //}

        //if (pos.x < Edges.edges.leftEdge- offset)
        //{
        //    x0 = transform.position.x;
        //    y0 = transform.position.y;

        //    x = Edges.edges.rightEdge+ offset;
        //    if (rb.velocity.y != 0)
        //        y = ((x - x0) / rb.velocity.x) * rb.velocity.y + y0;
        //    else
        //        y = transform.position.y;
        //    transform.position = new Vector2(x-0.1f, y);
        //    return;
        //}

        if (pos.y > Edges.edges.topEdge)
            transform.position = new Vector3(pos.x, Edges.edges.botEdge, pos.z);

        if (pos.y < Edges.edges.botEdge)
            transform.position = new Vector3(pos.x, Edges.edges.topEdge, pos.z);

        if (pos.x > Edges.edges.rightEdge)
            transform.position = new Vector3(Edges.edges.leftEdge, pos.y, pos.z);

        if (pos.x < Edges.edges.leftEdge)
            transform.position = new Vector3(Edges.edges.rightEdge, pos.y, pos.z);


    }
}
