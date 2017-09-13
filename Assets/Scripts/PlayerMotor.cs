using UnityEngine;

[RequireComponent(typeof(Rigidbody))] //we want to always have a rigidbody with our player motor.
public class PlayerMotor : MonoBehaviour {

    [SerializeField]
    private Camera cam;


    private Vector3 velocity = Vector3.zero;
    private Vector3 rotation = Vector3.zero;
    private Vector3 cameraRotation = Vector3.zero;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    //gets a rotational vector 
    public void Move(Vector3 _velocity) {
        velocity = _velocity;
    }


    public void Rotate(Vector3 _rotation)
    {
        rotation = _rotation;
    }

    //gets the rotational vector of the camera
    public void RotateCamera(Vector3 _cameraRotation)
    {
        cameraRotation = _cameraRotation;
    }


    //Run every physics iteration = runs on a fixed time
    private void FixedUpdate()
    {
        PerformMovement();
        PerformRotation();
    }

    //Perform movement based on velocity variable
    void PerformMovement()
    {
        if(velocity != Vector3.zero) //if speed not 0
        {
            rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);  //this will move our rigidbody (our player) to the position of our player + velocity vector.
            //takes in the position that we want to move to
            //different from transform.translate, since it'll stop it from moving there if it collides with something on the way; does physics checks. mmuch easier to control than the add force method.
        }
    }

    void PerformRotation()
    {
        rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));
        if(cam!= null)
        {
            cam.transform.Rotate(-cameraRotation);
        }
    }



}
