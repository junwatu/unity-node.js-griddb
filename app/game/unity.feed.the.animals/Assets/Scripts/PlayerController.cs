using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public float horizontalMove;
    public float speed = 10.0f;
    public float xRange = 10.0f;

    public GameObject[] projectilePrefabs; // Array to hold multiple projectile prefabs

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < -xRange)
        {
            transform.position = new Vector3(-xRange, transform.position.y, transform.position.z);
        }

        if (transform.position.x > xRange)
        {
            transform.position = new Vector3(xRange, transform.position.y, transform.position.z);
        }
        horizontalMove = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * Time.deltaTime * horizontalMove * speed);

        if (Input.GetKeyDown("space"))
        {
            // Pick a random index
            int randomIndex = Random.Range(0, projectilePrefabs.Length);

            // Pick a projectile prefab using the random index
            GameObject randomProjectilePrefab = projectilePrefabs[randomIndex];

            // Instantiate the random projectile
            Instantiate(randomProjectilePrefab, transform.position, randomProjectilePrefab.transform.rotation);

            GameManager.Instance.numberOfMeatThrows++;
        }
    }
}
