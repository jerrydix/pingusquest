using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRoute : MonoBehaviour
{
    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;
    [SerializeField] private Transform enemy;
    [SerializeField] private float speed;
    [SerializeField] private float waitTime;
    private float waitTimer;
    private Vector3 iScale;
    private bool isMovingLeft;
    [SerializeField] private Animator anim;

    // Start is called before the first frame update
    void Awake()
    {
        iScale = enemy.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (speed > 0)
        {
            if (isMovingLeft)
            {
                if (enemy.position.x >= leftEdge.position.x)
                    Move(-1);
                else
                {
                    ChangeDirection();
                }
            }
            else
            {
                if (enemy.position.x <= rightEdge.position.x)
                    Move(1);
                else
                {
                    ChangeDirection();
                }

            }
        }
    }

    private void ChangeDirection()
    {
        if (speed > 0)
        {
            anim.SetBool("moving",false);
            waitTimer += Time.deltaTime;
            if (waitTimer > waitTime)
            {
                isMovingLeft = !isMovingLeft;
            } 
        }
    }

    private void Move(int direction)
    {
        anim.SetBool("moving",true);
        waitTimer = 0;
        enemy.localScale = new Vector3(Mathf.Abs(iScale.x) * direction, iScale.y, iScale.z);
        enemy.position = new Vector3(enemy.position.x + Time.deltaTime * direction * speed, enemy.position.y, enemy.position.z);
    }

    private void OnDisable()
    {
        
    }
}
