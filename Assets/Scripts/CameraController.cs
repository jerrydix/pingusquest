using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float ahead;
    [SerializeField] private float speed;
    private float lookAhead;
    private float delay;
    void Start()
    {
        
    }
    
    void Update()
    {
        transform.position = new Vector3(player.position.x , player.position.y + lookAhead, transform.position.z);
        lookAhead = Mathf.Lerp(lookAhead, (ahead * player.localScale.y), Time.deltaTime * speed);
        
    }
}
