using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{

    public float lifeTime;

    public bool isEnemyBullet = false;

    private Vector2 lastPosition;

    private Vector2 curPosition;

    private Vector2 playerPosition;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DeathDelay());

        if(!isEnemyBullet) {
        transform.localScale = new Vector2(0.1f, 0.1f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(isEnemyBullet) {
            
            transform.localScale = new Vector2(0.1f, 0.1f);
            curPosition = transform.position;
            transform.position = Vector2.MoveTowards(transform.position, playerPosition, 5f * Time.deltaTime);
            
            if(curPosition == lastPosition){
                Destroy(gameObject);
            }
            lastPosition = curPosition;
        }
    }

    public void GetPlayer(Transform player){
        playerPosition = player.position;
    }

    // Eliminate bullet after for seconds
    IEnumerator DeathDelay(){
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }

    // Detect bullet collision with enemy and eliminate
    void OnTriggerEnter2D(Collider2D col) {

        if(col.tag == "Enemy" && !isEnemyBullet) {
            col.gameObject.GetComponent<EnemyController>().Death();
            Destroy(gameObject);
        }

        if(col.tag == "Player" && isEnemyBullet) {
            GameController.DamagePlayer(1);
            Destroy(gameObject);
        }
    }
}
