using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keyboardInput : MonoBehaviour
{
    [SerializeField] private PhysicsMovement _movement;
    
    private void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        if(Input.GetKey(KeyCode.W))
        {
            _movement.Move(new Vector3(-vertical, 0, horizontal));
        }
    }
}
