using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ShipState { Idle,Prepare,Sail};

public class Ship : MonoBehaviour
{
    public ShipState State { get; set; }
    Rigidbody2D rb;
    Vector3 start_point;

    void Awake()
    {
        //State = InputState.Default;
        State = ShipState.Idle;
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

        if ((Input.GetMouseButtonDown(0))&&(State==ShipState.Idle))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

            print(hit.collider);
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
            transform.position = mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            State = ShipState.Idle;
        }
    }

    public void Throw()
    {

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
