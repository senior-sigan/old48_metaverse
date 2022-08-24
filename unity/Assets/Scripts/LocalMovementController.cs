using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalMovementController : MonoBehaviour
{
    [SerializeField] private float lookSensitivity;
    [SerializeField] private float moveSpeed;
    private Camera cam;
    private float xRotation = 0;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        cam = Camera.main;
    }

    private Vector3 GetAxisVec()
    {
        var yRot = Input.GetAxisRaw("Mouse X");
        var xRot = Input.GetAxisRaw("Mouse Y");

        return new Vector3(-xRot, yRot, 0);
    }

    private void Move()
    {
        var x = Input.GetAxis("Horizontal");
        var z = Input.GetAxis("Vertical");

        var dir = transform.right * x + transform.forward * z;
        var motion = Vector3.MoveTowards(
            transform.position,
            transform.position + dir,
            moveSpeed * Time.deltaTime
        );
        transform.position = motion;
    }

    private void Rotate()
    {
        var yRot = Input.GetAxisRaw("Mouse X") * lookSensitivity * Time.deltaTime;
        var xRot = Input.GetAxisRaw("Mouse Y") * lookSensitivity * Time.deltaTime;

        xRotation -= xRot;
        xRotation = Mathf.Clamp(xRotation, -70f, 70f);
        cam.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        
        transform.Rotate(Vector3.up, yRot);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Rotate();
    }
}