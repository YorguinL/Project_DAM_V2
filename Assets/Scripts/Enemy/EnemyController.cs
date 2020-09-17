using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState{
        Wander,
        Follow,
        Attack,
        Die,
};

public enum EnemyType {
    Melee,
    Ranged
};

public class EnemyController : MonoBehaviour
{
    GameObject player;
    private SpriteRenderer spr;
    private Animator anim;
    public EnemyState currState = EnemyState.Wander;
    public EnemyType enemyType;
    public float range;
    public float speed;
    public float attackRange;
    public float bulletSpeed;
    public float coolDown;
    private bool chooseDirection = false;
    private bool coolDownAttack = false;
    private int randomDirection;
    public GameObject bulletPrefab;
    public Transform firePoint;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        spr =  transform.GetComponent<SpriteRenderer>();
        anim = transform.GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        switch(currState){

            case(EnemyState.Wander):
                Wander();
            break;

            case(EnemyState.Follow):
                Follow();
            break;

            case(EnemyState.Attack):
                Attack();
            break;

            case(EnemyState.Die):
            break;
        }

        // Check range
        if(IsPlayerInRange(range) && currState != EnemyState.Die) {
            currState = EnemyState.Follow;
        } else if(!IsPlayerInRange(range) && currState != EnemyState.Die) {
            currState = EnemyState.Wander;
        }
        
        if(Vector3.Distance(transform.position, player.transform.position) <= attackRange) {
            currState = EnemyState.Attack;
        }
    }

    private bool IsPlayerInRange(float range) {
        return Vector3.Distance(transform.position, player.transform.position) <= range;
    }

    private IEnumerator ChooseDirection() {
        chooseDirection = true;
        yield return new WaitForSeconds(5);
        randomDirection = Random.Range(0, 4);
        chooseDirection = false;
    }

    void Wander() {

        if(!chooseDirection) {
            StartCoroutine(ChooseDirection());
        }

        EnemyAnimations(randomDirection);
        // transform.position += -transform.right * speed * Time.deltaTime;

        if(IsPlayerInRange(range)){
            currState = EnemyState.Follow;
        }
    }

    void Follow() {

        float auxPosX = transform.position.x;
        float auxPosY = transform.position.y;

        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);

        if (transform.position.x > auxPosX) {
            FlipAndRight();
        } else {
            FlipAndLeft();
        }

        if ((transform.position.y > auxPosY)) {
            anim.SetBool("WalkY", true);
        } else {
            anim.SetBool("WalkY", false);
        }
    }

    void Attack() {
        if(!coolDownAttack) {

            switch(enemyType){

                case(EnemyType.Melee):
                    GameController.DamagePlayer(1); 
                    StartCoroutine(CoolDown());
                break;

                case(EnemyType.Ranged):
                    GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity) as GameObject;
                    bullet.GetComponent<BulletController>().GetPlayer(player.transform);
                    bullet.AddComponent<Rigidbody2D>().gravityScale = 0;
                    bullet.GetComponent<BulletController>().isEnemyBullet = true;
                    StartCoroutine(CoolDown());
                break;

            }
        }
    }

    private IEnumerator CoolDown() {
        coolDownAttack = true;
        yield return new WaitForSeconds(coolDown);
        coolDownAttack = false;

    }

    public void Death() {
        GameController.countKilledEnemies++;
        Destroy(gameObject);
    }

    void EnemyAnimations(int rndDir){

        switch(rndDir) {
            case(0):
                anim.SetBool("WalkX", false);
                anim.SetBool("WalkY", true);
                transform.Translate(Vector2.up * speed * Time.deltaTime);
            break;

            case(1):
                anim.SetBool("WalkX", false);
                anim.SetBool("WalkY", false);
                transform.Translate(-Vector2.up * speed * Time.deltaTime);
            break;

            case(2):
                FlipAndRight();
                transform.Translate(Vector2.right * speed * Time.deltaTime);
            break;

            case(3):
                FlipAndLeft();
                transform.Translate(-Vector2.right * speed * Time.deltaTime);
            break;
        }
    }

    void FlipAndRight(){
        anim.SetBool("WalkY", false);
        if (spr.flipX == true) {
            spr.flipX = false;
        }
        anim.SetBool("WalkX", true);
    }

    void FlipAndLeft(){
        anim.SetBool("WalkY", false);
        if (spr.flipX == false) {
            spr.flipX = true;
        }
        anim.SetBool("WalkX", true);
    }
}
