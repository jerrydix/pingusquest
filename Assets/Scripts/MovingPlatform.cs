using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private int startPoint;
    [SerializeField] private Transform[] points;
    private int currentPoint;
    [SerializeField] private Transform player;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = points[startPoint].position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, points[currentPoint].position) < 0.2f)
        {
            currentPoint++;
            currentPoint = (currentPoint == points.Length ? 0 : currentPoint);
        }

        transform.position =
            Vector2.MoveTowards(transform.position, points[currentPoint].position, speed * Time.deltaTime);
        if (Input.GetButtonDown("Jump"))
        {
            player.SetParent(null);
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.position.y > transform.position.y + 0.7f)
        {
            col.transform.parent = transform;
            if (col.gameObject.CompareTag("Player"))
            {
                col.rigidbody.interpolation = RigidbodyInterpolation2D.Extrapolate;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        other.transform.parent = null;
        other.rigidbody.interpolation = RigidbodyInterpolation2D.Interpolate;
    }

    /*private void OnCollisionStay2D(Collision2D collision)
    {
        if (Math.Abs(Input.GetAxisRaw("Horizontal")) > 0.25f || Input.GetButtonDown("Jump"))
        {
            collision.transform.SetParent(null);
        }
        else
        {
            collision.transform.parent = transform;
        }
    }*/
}
