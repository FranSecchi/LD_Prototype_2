using System;
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
    public float m_rotationSpeed;

    public bool flag;

    public float m_angleVisionY;

    public KeyCode jump;
    public float m_jumpImpulse;

    public KeyCode sprint;
    public float m_sprintSpeed;
    public float m_stamina;
    public float runCost;
    public float regainStaminaSpeed;
    public float m_sprintFoV;


    private CharacterController m_cc;
    private Vector3 m_front;
    private Vector3 m_right;
    private Vector3 m_movement;

    private float m_verticalVelocity;
    private float currentStamina;
    private float slowFactor = 1f;
    private bool m_OnGrounded;
    private bool isDead = false;
    private bool isSprinting = false;
    private bool hasEaten = false;
    private bool canMove = true;
    private const float m_surfaceGravity = -9.8f;
    private float fov;
    public float CurrentStamina { get => currentStamina; set => currentStamina = value; }
    public bool IsDead { get => isDead; set => isDead = value; }


    public bool HasEaten { get => hasEaten; set => hasEaten = value; }

    // Start is called before the first frame update
    void Start()
    {
        currentStamina = m_stamina;
        m_yaw = transform.rotation.x;
        m_pitch = transform.rotation.y;
        m_cc = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        fov = Camera.main.fieldOfView;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead) return;
        InputRotation();
        if(canMove)
            InputMovement();
    }


    private void InputMovement()
    {
        if (Input.GetKeyDown(jump))
        {
            m_verticalVelocity = m_jumpImpulse;
        }
        Sprinting();
        m_front = transform.forward * Input.GetAxis("Vertical");
        m_right = transform.right * Input.GetAxis("Horizontal");

        m_movement = m_front + m_right;
        m_verticalVelocity += m_OnGrounded ? 0 : m_surfaceGravity * Time.deltaTime;
        m_movement.y = m_verticalVelocity;
        m_movement *= isSprinting ? m_sprintSpeed : m_speed;
        CollisionFlags collision = m_cc.Move(m_movement * slowFactor * Time.deltaTime);
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
        m_pitch -= mousePositionY;
        m_pitch = Mathf.Clamp(m_pitch, -m_angleVisionY / 2, m_angleVisionY / 2); // Ensure pitch stays within desired range

        //m_yaw += mousePositionX;
        m_PitchController.localRotation = Quaternion.Euler(m_pitch, 0.0f, 0.0f);
        transform.rotation *= Quaternion.Euler(0.0f, mousePositionX, 0.0f);
    }
    private void Sprinting()
    {
        if (Input.GetKey(sprint))
        {
            isSprinting = true;
            if (currentStamina <= 0) isSprinting = false;
            else if (!hasEaten) currentStamina -= Time.deltaTime * runCost;
            else if (currentStamina < m_stamina) currentStamina = Mathf.Min(m_stamina, currentStamina + Time.deltaTime * regainStaminaSpeed);

        }
        else
        {
            isSprinting = false;
            if (currentStamina < m_stamina) currentStamina = Mathf.Min(m_stamina, currentStamina + Time.deltaTime * regainStaminaSpeed);
        }
        float f = isSprinting ? m_sprintFoV : fov;
        Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, f, 0.15f) ;
    }
    internal void Die()
    {
        isDead = true;
        StartCoroutine(Respawn());
    }
    internal void CantMove(bool b)
    {
        canMove = b;
    }
    private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(0.5f);
        isDead = false;
    }
    internal void Slow()
    {
        StartCoroutine(DoSlow());
    }
    private IEnumerator DoSlow()
    {
        slowFactor = 0.3f;
        yield return new WaitForSeconds(2f);
        slowFactor = 1f;
    }
}
