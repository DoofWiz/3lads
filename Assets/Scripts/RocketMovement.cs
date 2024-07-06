using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEditor.Callbacks;
using UnityEngine;

public class RocketMovement : MonoBehaviour
{
    //
    Rigidbody m_Rigidbody;
    [SerializeField]float m_Thrust = 20f;
    [SerializeField]float rotationSpeed = 5f;
    // Start is called before the first frame update
    void Start()
    {
        // Fetch the Rigidbody once and store it in m_Rigidbody. This means every frame/input the code isn't fetching an expensive resources, saving resources.
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float xRotate = Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;
        transform.Rotate(0,0,xRotate);
    }
    void FixedUpdate()
    {
        if(Input.GetKey(KeyCode.W))
        {
            UnityEngine.Vector3 antiGravityForce =  -Physics.gravity * m_Rigidbody.mass;
            m_Rigidbody.AddForce(antiGravityForce);
            m_Rigidbody.AddForce(transform.up * m_Thrust);
        }
    }
}
