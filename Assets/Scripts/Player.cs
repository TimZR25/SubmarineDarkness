using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private OxygenHandler _oxygenHandler;

    [SerializeField] private Manipulator _manipulator;

    [SerializeField] private float _speed;

    private Rigidbody2D _rigidbody;


    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _manipulator.Show();
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        float moveInputX = Input.GetAxisRaw(AxisNames.Horizontal);
        float moveInputY = Input.GetAxisRaw(AxisNames.Vertical);

        _rigidbody.velocity = new Vector2(moveInputX, moveInputY) * _speed;

        if (moveInputX == 0) return;

        transform.localScale = new Vector3(moveInputX, 1, 1);
    }

    private void Die()
    {
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        _oxygenHandler.OnDead += Die;
    }

    private void OnDisable()
    {
        _oxygenHandler.OnDead -= Die;
    }
}

public struct AxisNames
{
    public const string Vertical = "Vertical";
    public const string Horizontal = "Horizontal";
}
