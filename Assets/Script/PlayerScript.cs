using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float speed = 5.0f;
    public float xPlayerMoveLimit;
    public float zPlayerMoveLimit;
    float rampUp = 0.5f;
    float deceleration = 1f;
    public bool isAutoOn, isBounceOn, isBurstOn, isTwoWayOn, isChaseOn;

    public GameObject bullet;
    public GameObject plane;

    public int life = 5;

    GameObject weaponObject;
    BounceWeaponScript bounceWeapon;
    AutomaticWeaponScript autoWeapon;
    BulletScript bulletScript;
    BurstWeaponScript burstWeapon;
    TwoWayWeapon twoWayWeapon;
    ChaseBounceWeaponScript chaseBounceWeapon;

    // Start is called before the first frame update
    void Start()
    {
        //getting scripts
        bulletScript = bullet.GetComponent<BulletScript>();
        autoWeapon = GetComponent<AutomaticWeaponScript>();
        burstWeapon = GetComponent<BurstWeaponScript>();
        bounceWeapon = GetComponent<BounceWeaponScript>();
        twoWayWeapon = GetComponent<TwoWayWeapon>();
        chaseBounceWeapon = GetComponent<ChaseBounceWeaponScript>();

        //finding plane
        GameObject obj = GameObject.FindGameObjectWithTag("Base");
        if (obj == null)
        {
            print("no object is found");
        }
        //weapon status
        isAutoOn = true;
        isBounceOn = false;
        isBurstOn = false;
        isTwoWayOn = false;
        isChaseOn = false;
    }

    // Update is called once per frame
    void Update()
    {
        //setting weapon position
        autoWeapon.playerPosition = transform.position;
        burstWeapon.playerPosition = transform.position;
        bounceWeapon.playerPosition = transform.position;
        twoWayWeapon.playerPosition = transform.position;
        chaseBounceWeapon.playerPosition = transform.position;

        //setting x & z player coordinates
        float x = transform.position.x + Time.deltaTime * Input.GetAxis("Horizontal") * speed;
        float z = transform.position.z + Time.deltaTime * Input.GetAxis("Vertical") * speed;

        //move limit
        if (x > xPlayerMoveLimit)
            x = xPlayerMoveLimit;
        else if (x < -xPlayerMoveLimit)
            x = -xPlayerMoveLimit;

        if (z > zPlayerMoveLimit)
            z = zPlayerMoveLimit;
        else if (z < -zPlayerMoveLimit)
            z = -zPlayerMoveLimit;
        
        //firing
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
                twoWayWeapon.rotationQ = Quaternion.FromToRotation(Vector3.right, direction);
                chaseBounceWeapon.rotationQ = Quaternion.FromToRotation(Vector3.right, direction);

                //checking weapon
                if (isAutoOn)
                    autoWeapon.SetFire(true);
                else
                    autoWeapon.SetFire(false);
                if (isBurstOn)
                    burstWeapon.SetFire(true);
                else
                    burstWeapon.SetFire(false);
                if (isBounceOn)
                    bounceWeapon.SetFire(true);
                else
                    bounceWeapon.SetFire(false);
                if (isTwoWayOn)
                    twoWayWeapon.SetFire(true);
                else
                    twoWayWeapon.SetFire(false);
                if (isChaseOn)
                    chaseBounceWeapon.SetFire(true);
                else
                    chaseBounceWeapon.SetFire(false);
            }
        }
        else if (Input.GetAxis("Fire1") == 0)
        {
            //stop firing
            autoWeapon.SetFire(false);
            burstWeapon.SetFire(false);
            bounceWeapon.SetFire(false);
            twoWayWeapon.SetFire(false);
            chaseBounceWeapon.SetFire(false);
        }

        //moving player
        transform.position = new Vector3(x, 1.0f, z);

        //selecting weapon
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            isAutoOn = true;
            isBounceOn = false;
            isBurstOn = false;
            isTwoWayOn = false;
            isChaseOn = false;
        }
        //if (Input.GetKeyDown(KeyCode.Alpha2))
        //{
        //    isAutoOn = false;
        //    isBounceOn = true;
        //    isBurstOn = false;
        //    isTwoWayOn = false;
        //    isChaseOn = false;
        //}
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            isAutoOn = false;
            isBounceOn = false;
            isBurstOn = true;
            isTwoWayOn = false;
            isChaseOn = false;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            isAutoOn = false;
            isBounceOn = false;
            isBurstOn = false;
            isTwoWayOn = true;
            isChaseOn = false;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            isAutoOn = false;
            isBounceOn = false;
            isBurstOn = false;
            isTwoWayOn = false;
            isChaseOn = true;
        }

        //ded
        if (life <= 0)
        {
            Destroy(gameObject);
            Destroy(this);
        }
    }
    //colliding
    private void OnTriggerEnter(Collider other)
    {
        GameObject objectCollide = other.gameObject;
        if (objectCollide.tag == "Enemy" || objectCollide.tag == "EnemyBossBullet")
        {
            life--;
            Destroy(objectCollide);
        }
        if (objectCollide.tag == "EnemyBoss")
        {
            life = 0;
        }
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
