using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Transform ball;
    public float speed = 10f;
    private Vector3 MoveForward;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            MoveForward = ball.transform.forward * speed * Time.deltaTime;
            ball.transform.position += MoveForward;

        }

    }
}
