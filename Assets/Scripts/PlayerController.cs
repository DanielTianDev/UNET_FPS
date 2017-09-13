using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour {

    [SerializeField]
    private float speed = 5.0f;

    [SerializeField]
    private float lookSensitivity = 3.0f;

    private PlayerMotor motor;

	void Start () {
        motor = GetComponent<PlayerMotor>(); //no need to check, since its a required component.
	}
	
	void Update () {

        //Calculate movement velocity as a 3D vector

        float _xMov = Input.GetAxisRaw("Horizontal"); //getaxisraw because we want lerping on our own. its good to be in complete control of how we do that, and if you use getaxis then unity will perform processing on it and you won't be in full control.
        float _zMov = Input.GetAxisRaw("Vertical");

        Vector3 _movHorizontal = transform.right * _xMov; //take into account our rotation local? (1,0,0)
        Vector3 _movVertical = transform.forward * _zMov; // (0,0,1)

        //final movement vector
        Vector3 _velocity = (_movHorizontal + _movVertical).normalized * speed; //normalized = total length vector is 1. we are only using the horizontal and vertical vectors as a direction, not speed

        //Apply movement
        motor.Move(_velocity);


        //Now calculate rotation as a 3d vector (turning around)
        float _yRot = Input.GetAxisRaw("Mouse X");

        Vector3 _rotation = new Vector3(0f, _yRot, 0f) * lookSensitivity;

        //Apply rotation
        motor.Rotate(_rotation);

        //Calculating camera rotation as a 3D vector
        float _xRot = Input.GetAxisRaw("Mouse Y");

        Vector3 _cameraRotation = new Vector3(_xRot, 0f, 0f) * lookSensitivity;

        //Apply camera rotation
        motor.RotateCamera(_cameraRotation);
    }
}
