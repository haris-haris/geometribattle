using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float speed = 5.0f;
    public float speed_acc = 0;
    public float minspeed_acc = 0;
    public float maxspeed_acc = 2.0f;
    public float xPlayerMoveLimit;
    public float zPlayerMoveLimit;
    float rampUp = 0.5f;
    float deceleration = 1f;

    public GameObject bullet;
    public GameObject plane;

    GameObject weaponObject;
    BounceWeaponScript bounceWeapon;
    AutomaticWeaponScript autoWeapon;
    BulletScript bulletScript;
    BurstWeaponScript burstWeapon;

    // Start is called before the first frame update
    void Start()
    {
        bulletScript = bullet.GetComponent<BulletScript>();
        autoWeapon = GetComponent<AutomaticWeaponScript>();
        burstWeapon = GetComponent<BurstWeaponScript>();
        bounceWeapon = GetComponent<BounceWeaponScript>();

        GameObject obj = GameObject.FindGameObjectWithTag("Base");
        if (obj == null)
        {
            print("no object is found");
        }
    }

    // Update is called once per frame
    void Update()
    {
        autoWeapon.playerPosition = transform.position;
        burstWeapon.playerPosition = transform.position;
        bounceWeapon.playerPosition = transform.position;

        //float inputdeltahorizontal = input.getaxis("horizontal");
        //float inputdeltavertical = input.getaxis("vertical");

        //if (inputDeltaHorizontal == 0)
        //{
        //    speed_acc = Mathf.MoveTowards(speed_acc, 0f, deceleration * Time.deltaTime);
        //}
        //else
        //{
        //    speed_acc += inputDeltaHorizontal * rampUp * Time.deltaTime;
        //}

        //if (inputDeltaVertical == 0)
        //{
        //    speed_acc = Mathf.MoveTowards(speed_acc, 0f, deceleration * Time.deltaTime);
        //}
        //else
        //{
        //    speed_acc += inputDeltaVertical * rampUp * Time.deltaTime;
        //}

        float x = transform.position.x + Time.deltaTime * Input.GetAxis("Horizontal") * speed;
        float z = transform.position.z + Time.deltaTime * Input.GetAxis("Vertical") * speed;

        if (x > xPlayerMoveLimit)
            x = xPlayerMoveLimit;
        else if (x < -xPlayerMoveLimit)
            x = -xPlayerMoveLimit;

        if (z > zPlayerMoveLimit)
            z = zPlayerMoveLimit;
        else if (z < -zPlayerMoveLimit)
            z = -zPlayerMoveLimit;

        if (Input.GetAxis("Fire1") == 1)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            Collider collider = plane.GetComponent<Collider>();
            RaycastHit hit;
            if (collider.Raycast(ray, out hit, 1000.0f))
            {
                Vector3 point = hit.point;

                Vector3 direction = point - transform.position;

                // we are working on a 2D plane, no need to find y direction so the bullet wont move downward
                // (remember that object is a little bit up-ed, so the direction will be a little bit skewed downward)
                direction.y = 0;

                // FromToRotation need an angle-0-vector as its base. So the first parameter would right
                // since if angle = 0, the direction is right
                autoWeapon.rotationQ = Quaternion.FromToRotation(Vector3.right, direction);
                burstWeapon.rotationQ = Quaternion.FromToRotation(Vector3.right, direction);
                bounceWeapon.rotationQ = Quaternion.FromToRotation(Vector3.right, direction);

                bounceWeapon.SetFire(true);
                //autoWeapon.SetFire(true);
                //burstWeapon.SetFire(true);
            }
        }
        else if (Input.GetAxis("Fire1") == 0)
        {
                bounceWeapon.SetFire(false);
                //autoWeapon.SetFire(false);
                //burstWeapon.SetFire(false);
        }
        transform.position = new Vector3(x, 1.0f, z);
    }
}
/*
         float rampUp = 0.1f;
          float deceleration = 0.5f;
       if (inputDeltaHorizontal == 0)
       {
           speed_acc = Mathf.MoveTowards(speed_acc, 0f, deceleration * Time.deltaTime);
       }
       else
       {
           speed_acc += inputDeltaHorizontal * rampUp * Time.deltaTime;
       }

       if (inputDeltaVertical == 0)
       {
           speed_acc = Mathf.MoveTowards(speed_acc, 0f, deceleration * Time.deltaTime);
       }
       else
       {
           speed_acc += inputDeltaVertical * rampUp * Time.deltaTime;
       }
       */
/*
if(Input.GetAxis("Horizontal") >= 1 || Input.GetAxis("Vertical") >= 1)
 {
     if (speed_acc >= maxspeed_acc)
         speed_acc = maxspeed_acc;
     else
         speed_acc += 1f * Time.deltaTime;
     x = transform.position.x + Time.deltaTime * Input.GetAxis("Horizontal") * (speed + speed_acc);
     z = transform.position.z + Time.deltaTime * Input.GetAxis("Vertical") * (speed + speed_acc);
 }
 else if (Input.GetAxis("Horizontal") <= -1 || Input.GetAxis("Vertical") <= -1)
 {
     if (speed_acc <= -maxspeed_acc)
         speed_acc = -maxspeed_acc;
     else
         speed_acc -= 1f * Time.deltaTime;
     x = transform.position.x + Time.deltaTime * Input.GetAxis("Horizontal") * (speed + -speed_acc);
     z = transform.position.z + Time.deltaTime * Input.GetAxis("Vertical") * (speed + -speed_acc);
 }
 else
 {
     if (speed_acc != 0)
     {
         if (speed_acc >= minspeed_acc)
             speed_acc -= 0.5f * Time.deltaTime;
         else if (speed_acc <= minspeed_acc)
             speed_acc += 0.5f * Time.deltaTime;
         x = transform.position.x + Time.deltaTime * Input.GetAxis("Horizontal") * (speed + speed_acc);
         z = transform.position.z + Time.deltaTime * Input.GetAxis("Vertical") * (speed + speed_acc);
     }
 }   
*/
/*
 * if(Input.GetAxis("Horizontal") >= 1 || Input.GetAxis("Vertical") >= 1)
        {
            if (speed_acc >= maxspeed_acc)
                speed_acc = maxspeed_acc;
            else
                speed_acc += 0.1f * Time.deltaTime;
        }
        else if (Input.GetAxis("Horizontal") <= -1 || Input.GetAxis("Vertical") <= -1)
        {
            if (speed_acc <= -maxspeed_acc)
                speed_acc = -maxspeed_acc;
            else
                speed_acc -= 0.1f * Time.deltaTime;
        }
        else
        {
            if (speed_acc != 0)
            {
                if (speed_acc >= minspeed_acc)
                    speed_acc -= 0.1f * Time.deltaTime;
                else if (speed_acc <= minspeed_acc)
                    speed_acc += 0.1f * Time.deltaTime;
            }
        }

 */
