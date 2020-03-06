using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBossScript : MonoBehaviour
{
    float speed = 2.0f;

    public float health = 1000.0f;
    public GameObject plane;
    public GameObject playerTarget;
    public bool isDefeated = false;
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
        float step = speed * Time.deltaTime; // calculate distance to move
        if (playerTarget != null)
            transform.LookAt(playerTarget.transform.position);
        transform.position = Vector3.MoveTowards(transform.position, playerTarget.transform.position, step);

        if (health <= 0)
        {
            gameManager.score += 100;
            isDefeated = true;
            Destroy(this.gameObject);
            Destroy(this);
        }
        if (isDefeated == true)
        {
            gameManager.win = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject objectCollide = other.gameObject;
        if (objectCollide.tag == "bullet")
        {
           // print("collide with bullet");
            health -= 30;
            Destroy(objectCollide);
        }
    }
}
