using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Move in local axis
        Vector3 moveDirection = new Vector3(horizontal, 0f, vertical) * 5f * Time.deltaTime;
        transform.Translate(moveDirection, Space.Self);
    }
}
