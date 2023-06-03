using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingElevator : MonoBehaviour
{
    public bool canMove;

    [SerializeField] float speed;
    [SerializeField] int startPoint;
    [SerializeField] Transform [] points;

    public Transform activator;
    private Vector3 initialActivatorPos;

    int i;
    bool reverse;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = points[startPoint].position;
        i = startPoint;
        initialActivatorPos = activator.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position, points[i].position) < 0.01f)
        {
           canMove = false;

           if(i == points.Length - 1)
           {
            reverse = true;
            i--;
            activator.position = initialActivatorPos;
            return;
           } 
           else if(i == 0)
           {
                reverse = true;
                i++;
                activator.position = Vector3.Lerp(activator.position, initialActivatorPos, Time.deltaTime);
                return;
           }

           if(reverse)
           {
            i--;
           }
           else
           {
                i++;
           }
        }
        if(canMove)
        {
            transform.position = Vector3.MoveTowards(transform.position, points[i].position, speed * Time.deltaTime);
            activator.position = Vector3.Lerp(activator.position, new Vector3(activator.position.x, 0f, activator.position.z), Time.deltaTime);
        }
    }
}
