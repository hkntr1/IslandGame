using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private float _speed = 5;
    [SerializeField] private float _turnSpeed = 360;
    public FloatingJoystick joystick;
    public Vector3 _input;
    public static PlayerController Current;
    private void Start()
    {
        Current = this;
    }
    private void Update()
    {
        GatherInput();
        Look();
    }
    private void FixedUpdate()
    {
        Move();       
    }
    private void GatherInput()
    {
        _input = new Vector3(-joystick.Horizontal, 0, -joystick.Vertical);
    }

    private void Look()
    {
        if (_input == Vector3.zero) return;
        var rot = Quaternion.LookRotation(_input.ToIso(), Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, _turnSpeed * Time.deltaTime);
    }

    private void Move()
    {
        _rb.MovePosition(transform.position + transform.forward * _input.normalized.magnitude * _speed * Time.deltaTime);     
    }
}
public static class Helpers
{
    private static Matrix4x4 _isoMatrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));
    public static Vector3 ToIso(this Vector3 input) => _isoMatrix.MultiplyPoint3x4(input);
}