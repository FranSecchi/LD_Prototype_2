using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSController : MonoBehaviour
{

    public float m_speed;
    float m_yaw;
    float m_pitch;

    public Transform m_PitchController;

    public float m_mouseSensitivityX;
    public float m_mouseSensitivityY;

    public bool flag;

    public float m_angleVisionY;

    public KeyCode jump;
    public float m_jumpImpulse;

    public KeyCode run;
    //public float m_incrementalSpeed;
    private bool m_sprinting = false;
    private Camera m_camera;

    private CharacterController m_cc;
    private Vector3 m_front;
    private Vector3 m_right;
    private Vector3 m_movement;

    private float m_verticalVelocity;
    private bool m_OnGrounded;
    //private const float m_waterGravity = -2f;
    private const float m_surfaceGravity = -9.8f;
    //public bool m_underWater;

    // Start is called before the first frame update
    void Start()
    {
        m_yaw = transform.rotation.x;
        m_pitch = transform.rotation.y;
        m_cc = GetComponent<CharacterController>();
        m_camera = GetComponentInChildren<Camera>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        InputRotation();
        InputMovement();

        if (Input.GetKeyDown(jump))
        {
            //if (m_underWater)
            //{
            //    m_verticalVelocity = m_jumpImpulse/3;
            //}
            //else
            //{
            //    m_verticalVelocity = m_jumpImpulse;
            //}
            m_verticalVelocity = m_jumpImpulse;
        }
        //if (Input.GetKeyDown(run))
        //{
        //    m_sprinting = true;
        //    m_camera.fieldOfView = 40.0f;
        //}
        //if (Input.GetKeyUp(run))
        //{
        //    m_sprinting = false;
        //    m_camera.fieldOfView = 60.0f;
        //}
    }

    private void InputMovement()
    {
        m_front = transform.forward * Input.GetAxis("Vertical");
        m_right = transform.right * Input.GetAxis("Horizontal");

        m_movement = m_front + m_right;
        //if (m_underWater)
        //{
        //    m_verticalVelocity += m_OnGrounded ? 0 : m_waterGravity * Time.deltaTime;
        //}
        //else
        //{
        //    m_verticalVelocity += m_OnGrounded ? 0 : m_waterGravity * Time.deltaTime;
        //}
        m_verticalVelocity += m_OnGrounded ? 0 : m_surfaceGravity * Time.deltaTime;
        m_movement.y = m_verticalVelocity;
        //float m_multiplier = m_sprinting ? m_incrementalSpeed : 1;
        CollisionFlags collision = m_cc.Move(m_movement * m_speed * Time.deltaTime);
        if (collision.Equals(CollisionFlags.Below))
        {
            m_OnGrounded = true;
            m_verticalVelocity = 0f;
        }
        else
        {
            m_OnGrounded = false;
        }
    }

    private void InputRotation()
    {
        float mousePositionX = Input.GetAxis("Mouse X") * m_mouseSensitivityX;
        float mousePositionY = Input.GetAxis("Mouse Y") * m_mouseSensitivityY;

        m_pitch -= mousePositionY * Time.deltaTime;
        m_pitch = Mathf.Clamp(m_pitch, -m_angleVisionY / 2, m_angleVisionY / 2); // Ensure pitch stays within desired range

        m_yaw += mousePositionX * Time.deltaTime;

        transform.rotation = Quaternion.Euler(0.0f, m_yaw, 0.0f);
        m_PitchController.localRotation = Quaternion.Euler(m_pitch, 0.0f, 0.0f);
    }
}
