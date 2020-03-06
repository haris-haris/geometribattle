using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceBulletScript : MonoBehaviour
{
    public float speed = 10.0f;
    public Quaternion rotationQ;
    float ExistTime;
    public float LimitTime = 5;

    public Vector3 originalObject;
    public Transform reflectedObject;
    

    //[SerializeField]
    //[Tooltip("Just for debugging, adds some velocity during OnEnable")]
    //private Vector3 initialVelocity;

    //[SerializeField]
    //private float minVelocity = 10f;

    //private Vector3 lastFrameVelocity;

    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //rb.velocity = initialVelocity;
    }

    // Update is called once per frame
    void Update()
    {
        float x = speed * Time.deltaTime;
        Vector3 position = transform.rotation * (new Vector3(x, 0, 0));

        transform.position = transform.position + position;
        ExistTime += Time.deltaTime;
        
        //lastFrameVelocity = rb.velocity;

        //reflectedObject.position = Vector3.Reflect(originalObject.position, Vector3.right);

        if (ExistTime >= LimitTime)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Bounce(collision.contacts[0].normal);
        //Store new direction
        if (other.gameObject.name == "wall")
        {
            print("wall");
            Vector3 newDirection = Vector3.Reflect(transform.position,transform.position);
            //Vector3 newDirection = Vector3.Reflect(transform.position, other.contacts[0].normal);
            //Rotate bullet to new direction
            print(newDirection);
            transform.rotation = Quaternion.LookRotation(newDirection);

            //add velocity to bullet based on new forward vector
            rb.velocity = transform.forward * speed;
        }
    }

    //private void Bounce(Vector3 collisionNormal)
    //{
    //    var speed = lastFrameVelocity.magnitude;
    //    var direction = Vector3.Reflect(lastFrameVelocity.normalized, collisionNormal);

    //    Debug.Log("Out Direction: " + direction);
    //    rb.velocity = direction * Mathf.Max(speed, minVelocity);
    //}
}
