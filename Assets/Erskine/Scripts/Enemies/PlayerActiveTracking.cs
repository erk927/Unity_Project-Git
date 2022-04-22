using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActiveTracking : MonoBehaviour//Current state is for a single player and enemy, need to set melee distance collision in seperate script(?)
{
    public float speed = 1f;//Enemy speed, shoud be able to modify it with a difficulty setting
    public float lerpSpeed = 0.3f;


    [HideInInspector] public float angle;
    private Animator animator;
    GameObject player;
    private float distanceToPlayer;
    [SerializeField]private float attackRange = 0.5f;
    

    // Start is called before the first frame update
    void Start(){
        player = GameObject.Find("Player");//FInd the player object
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate(){
        //Trying something simple
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        angle = AngleBetweenPoints(transform.position, player.transform.position);//Angle b/t player and enemy
        changeDirection();
    }

    //Changes character animation depending on where the mouse cursor is
    public void changeDirection(){
        distanceToPlayer = Vector2.Distance(player.transform.position, transform.position);

        if (distanceToPlayer < attackRange && angle > 150.1 && angle < 210){//attack left
            animator.Play("basic_enemy_attack_left");
        }
        else if (distanceToPlayer < attackRange && (angle > 0 && angle <30) || (angle > 331 && angle < 360)){//attack right
            animator.Play("basic_enemy_attack_right");
        }
        else if (angle > 31 && angle < 150){// up
            animator.Play("basic_enemy_back_walk");
        }
        else if (angle > 210.1 && angle < 330){//down
            animator.Play("basic_enemy_walk");
        }
        else if (angle > 150.1 && angle < 210){//left
            animator.Play("basic_enemy_left_walk");
        }
        else if ((angle > 0 && angle <30) || (angle > 331 && angle < 360)){//right
            animator.Play("basic_enemy_right_walk");
        }
        else
            animator.Play("basic_enemy_idle");//failcase
    }

    public float AngleBetweenPoints(Vector2 a, Vector2 b) {
         return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg + 180;
     }
}
