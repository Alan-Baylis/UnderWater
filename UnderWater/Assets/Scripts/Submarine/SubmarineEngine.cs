using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmarineEngine : MonoBehaviour {
    private float _motorControll;
    private int _motorPower;
    private float _playerlift;

    private Rigidbody2D _rigidbody2D;

    private void OnEnable()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _motorPower = 1000;
    }

    private void FixedUpdate()
    {
        var lift = 800 + 800 * _playerlift;
        Debug.Log("Lift:" + lift);
        _rigidbody2D.AddForce(Vector2.up * lift);

        _rigidbody2D.AddForce(Vector2.right * _motorControll * _motorPower);
    }

    private void Update()
    {
        _motorControll = Input.GetAxis("Horizontal");
        _playerlift = Input.GetAxis("Vertical");
        Debug.Log(_motorControll + "/" + _playerlift);
    }
}
