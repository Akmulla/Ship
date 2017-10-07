using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public enum ShipState { Idle,Prepare,Sail};

public class ShipMove : MonoBehaviour
{
    public ShipState State { get; set; }
    
    Rigidbody2D rb;
    Vector3 start_position;
    public float maxRange=2.0f;
    public float forceCoeff=1.0f;

    void Awake()
    {
        //State = InputState.Default;
        State = ShipState.Idle;
        start_position = transform.position;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }

    void Update()
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
        if ((Input.GetMouseButtonDown(0))&&(State==ShipState.Idle))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

           // print(hit.collider);
            if (hit.collider!=null)
            {
                if (hit.collider.gameObject.CompareTag("Ship"))
                {
                    State = ShipState.Prepare;
                }

            }
        }

        if ( (Input.GetMouseButton(0)) && (State == ShipState.Prepare) )
        {
            RotateBackToPoint((mousePosition - start_position).normalized * maxRange*2.0f);
            //RotateToPoint((mousePosition - start_position).normalized * maxRange * 2.0f);
            if ((mousePosition-start_position).magnitude<maxRange)
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
            // rb.simulated = true;
            rb.gravityScale = 1.0f;
            rb.AddForce((start_position - transform.position) * forceCoeff,ForceMode2D.Impulse);
            State = ShipState.Sail;

        }

        if (State == ShipState.Sail)
        {
            RotateToPoint((Vector2)transform.position + rb.velocity);
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
}
