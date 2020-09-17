using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour
{

    public float speed = 2f;
    Rigidbody2D rigidbody;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed;
    private float lastFire;
    public float fireDelay;
    private SpriteRenderer head;
    private Animator animHead;
    private SpriteRenderer body;
    private Animator animBody;
    public static int countHealth = 0;
    public static int countFireRate = 0;
    public static int countPlayerSpeed = 0;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        
        head = this.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>();
        animHead = this.gameObject.transform.GetChild(0).GetComponent<Animator>();
        body = this.gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>();
        animBody = this.gameObject.transform.GetChild(1).GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        fireDelay = GameController.FireRate;
        speed = GameController.MoveSpeed;

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        
        float shootH = Input.GetAxis("ShootHorizontal");
        float shootV = Input.GetAxis("ShootVertical");

        PlayerAnimations(horizontal, vertical, shootH, shootV);

        if((shootH != 0 || shootV != 0) && Time.time > lastFire + fireDelay) {
            Shoot(shootH, shootV);
            lastFire = Time.time;
        }

        // Move player
        rigidbody.velocity = new Vector3(horizontal * speed, vertical * speed, 0);
    }

    // Instantiate bullet 
    void Shoot(float x, float y) {

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation) as GameObject;
        bullet.AddComponent<Rigidbody2D>().gravityScale = 0;
        bullet.GetComponent<Rigidbody2D>().velocity = new Vector3 (
            (x < 0) ? Mathf.Floor(x) * bulletSpeed : Mathf.Ceil(x) * bulletSpeed,
            (y < 0) ? Mathf.Floor(y) * bulletSpeed : Mathf.Ceil(y) * bulletSpeed,
            0
        );
    }

    void PlayerAnimations(float mX, float mY, float sX, float sY) {

        if(mX !=0 || mY != 0) {

            // Horizontal movement
            if (mX > 0 ) {

                FlipAndLookRight();

                if (body.flipX == true) {
                    body.flipX = false;
                }
                animBody.SetBool("WalkX", true);



            } else if (mX < 0) {

                FlipAndLookLeft();

                if (body.flipX == false) {
                    body.flipX = true;
                }
                animBody.SetBool("WalkX", true);

            } else {

                animHead.SetBool("LookX", false);
                animBody.SetBool("WalkX", false);
            }

            // Vertical movement
            if (mY > 0) {

                animHead.SetBool("LookY", true);
                animBody.SetBool("WalkY", true);

            } else if (mY < 0) {

                animHead.SetBool("LookY", false);
                animBody.SetBool("WalkY", true);

            } else {

                animHead.SetBool("LookY", false);
                animBody.SetBool("WalkY", false);
            }

        } else {

            animHead.SetBool("LookX", false);
            animBody.SetBool("WalkX", false);
            animHead.SetBool("LookY", false);
            animBody.SetBool("WalkY", false);
        }

        // Shoot movement
        if (sX > 0) {

            FlipAndLookRight();

        } else if ( sX < 0) {

            FlipAndLookLeft();

        }

        if (sY > 0) {

            animHead.SetBool("LookY", true);

        } else if ( sY < 0) {

            animHead.SetBool("LookY", false);

        }
    }

    void FlipAndLookRight() {
        if (head.flipX == true) {
            head.flipX = false;
        }
        animHead.SetBool("LookX", true);
    }

    void FlipAndLookLeft() {
        if (head.flipX == false) {
            head.flipX = true;
        }
        animHead.SetBool("LookX", true);
    }
}
