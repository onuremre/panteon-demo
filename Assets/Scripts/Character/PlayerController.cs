using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    GameManager gameManager;
    Obstacle obstacle;
    Platform platform;
    public GameObject camera;
    Rigidbody rigidbody;
    Animator animator;
    public float characterSpeed = 1, platformSizeZ, touchPosZ;
    float playerTransformX;
    void Start()
    {
        gameManager = GameManager.Instance;
        platform = gameManager.platform;
        platformSizeZ = platform.getSize().z;
        rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if(gameManager.game == true)
        {
            Move();
            if (Input.GetMouseButton(0))
            {
                touchPosZ += Input.GetAxis("Mouse X");
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(gameManager.game == true && other.tag == "RotatorObstacleRotatingStick")
        {
            animator.SetBool("Jump", true);
            obstacle = other.GetComponentInParent<Obstacle>();
            transform.position = Vector3.Lerp(transform.position,
                new Vector3(transform.position.x + (40 * obstacle.direction), transform.position.y, transform.position.z), 0.25f);
            
        }
        else if(gameManager.game == true && other.tag != "Platform" && other.tag != "Invisible")
        {
            gameManager.game = false;
            rigidbody.velocity = new Vector3(0, 0, 0);
            animator.SetBool("Fall", true);
            StartCoroutine(returnStartingPosition());
        }
    }

    void Move()
    {
        Vector3 dir = transform.forward * gameManager.gameSpeed;
        dir.y += rigidbody.velocity.y;
        rigidbody.velocity = dir;
        transform.position = new Vector3(transform.position.x, transform.position.y, (float)(touchPosZ * -0.025 * gameManager.gameSpeed));
        playerTransformX = transform.position.x;
        camera.transform.position = new Vector3(playerTransformX - 9, camera.transform.position.y, camera.transform.position.z);
        if(transform.position.z > platform.transform.position.z + (platformSizeZ / 2) || transform.position.z < platform.transform.position.z - (platformSizeZ / 2))
        {
            gameManager.game = false;
            rigidbody.velocity = new Vector3(0, 0, 0);
            animator.SetBool("Fall", true);
            touchPosZ = 0;
            StartCoroutine(returnStartingPosition());
        }
    }

    IEnumerator returnStartingPosition()
    {
        yield return new WaitForSeconds(2);
        gameManager.game = true;
        transform.position = new Vector3(0, 0, 0);
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

    public void moveCameratoPlayer()
    {
        Vector3 finishCamera = new Vector3(playerTransformX + 3, camera.transform.position.y, camera.transform.position.z);
        camera.transform.position = Vector3.Lerp(camera.transform.position, finishCamera, 1);
    }
}
