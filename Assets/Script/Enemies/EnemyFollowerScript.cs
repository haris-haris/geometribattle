using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollowerScript : MonoBehaviour
{
    public float speed = 3.0f;

    float health = 100.0f;

    public GameObject playerTarget;
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        playerTarget = GameObject.FindGameObjectWithTag("Player");
        GameObject gameManagerObj = GameObject.FindGameObjectWithTag("GameManager");
        gameManager = gameManagerObj.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(playerTarget.transform.position);
        float step = speed * Time.deltaTime; // calculate distance to move
        if (playerTarget != null)
            transform.position = Vector3.MoveTowards(transform.position, playerTarget.transform.position, step);
        if (health <= 0)
        {
            gameManager.score += 10;
            Destroy(this.gameObject);
            Destroy(gameObject);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        GameObject objectCollide = other.gameObject;
        if (objectCollide.tag == "bullet")
        {
            health -= 30;
            Destroy(objectCollide);
        }
        if (other.gameObject.tag == "BounceBullet")
        {
            health -= 40;
        }
    }
}
