using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    GameManager gameManager;
    Obstacle obstacle;
    Platform platform;
    Rigidbody rigidbody;
    Animator animator;
    Vector3 target;
    public bool moveRightLeft = false, moveForward = true;
    int[] direction = { -1, 1 };
    int random;
    public float changePosition = 0;
    float startingPositionX, startingPositionZ;
    float platformSizeZ;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        gameManager = GameManager.Instance;
        animator = GetComponent<Animator>();
        platform = gameManager.platform;
        platformSizeZ = platform.getSize().z;
        startingPositionX = transform.position.x;
        startingPositionZ = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        if (moveForward)
        {
            Move();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "RotatorObstacleRotatingStick")
        {
            animator.SetBool("Jump", true);
            obstacle = other.GetComponentInParent<Obstacle>();
            transform.position = Vector3.Lerp(transform.position,
                new Vector3(transform.position.x + (40 * obstacle.direction), transform.position.y, transform.position.z), 0.25f);
        }
        else if (other.tag != "Platform" && other.tag != "Character" && other.tag != "Invisible")
        {
            moveForward = false;
            rigidbody.velocity = new Vector3(0, 0, 0);
            moveRightLeft = false;
            animator.SetBool("Fall", true);
            StartCoroutine(returnStartingPosition());
        }
        else if(other.tag == "Character")
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 3);
        }
    }

    void Move()
    {
        Vector3 dir = transform.forward * gameManager.gameSpeed;
        dir.y += rigidbody.velocity.y;
        rigidbody.velocity = dir;

        if (moveRightLeft == true)
        {
            random = Random.Range(0, 2);
            target = new Vector3(transform.position.x, transform.position.y, changePosition);
            transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime * 3);
            if(transform.position.z == changePosition)
            {
                changePosition = 0;
                moveRightLeft = false;
            }
        }
        if (transform.position.z > platform.transform.position.z + (platformSizeZ / 2) || transform.position.z < platform.transform.position.z - (platformSizeZ / 2))
        {
            moveForward = false;
            rigidbody.velocity = new Vector3(0, 0, 0);
            animator.SetBool("Fall", true);
            changePosition = 0;
            StartCoroutine(returnStartingPosition());
        }
    }

    IEnumerator returnStartingPosition()
    {
        yield return new WaitForSeconds(2);
        moveForward = true;
        transform.position = new Vector3(0, 0, startingPositionZ);
        transform.rotation = Quaternion.Euler(0, 90, 0);
        animator.SetBool("Fall", false);
        Move();
    }

    public void cancelJump()
    {
        animator.SetBool("Jump", false);
    }

    public void Victory()
    {
        animator.SetBool("Victory", true);
    }

    public void Defeat()
    {
        animator.SetBool("Defeat", true);
    }
}
