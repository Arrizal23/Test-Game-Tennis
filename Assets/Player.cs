using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform aimTarget; // target di mana kita bertujuan untuk mendaratkan bola yang di pukul oleh player
    float speed = 3f; // kecepatan bergeraknya bola
    float force = 13; // kekuatan memukul bola

    bool hitting; // untuk mengetahui apakah kita memukul bola atw tidak

    public Transform ball; // bola
    Animator animator;

    Vector3 aimTargetInitialPosition; // posisi awal bola 

    ShotManager shotManager; 
    Shot currentShot; // bidikan ketika bola di pukul

    private void Start()
    {
        animator = GetComponent<Animator>(); 
        aimTargetInitialPosition = aimTarget.position; 
        shotManager = GetComponent<ShotManager>(); 
        currentShot = shotManager.topSpin; 
    }

    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal"); 
        float v = Input.GetAxisRaw("Vertical"); 

        if (Input.GetKeyDown(KeyCode.F)) 
        {
            hitting = true; 
            currentShot = shotManager.topSpin; 
        }
        else if (Input.GetKeyUp(KeyCode.F))
        {
            hitting = false; 
        }                    

        if (Input.GetKeyDown(KeyCode.E))
        {
            hitting = true; 
            currentShot = shotManager.flat; 
        }
        else if (Input.GetKeyUp(KeyCode.E))
        {
            hitting = false;
        }

        if (hitting)  
        {
            aimTarget.Translate(new Vector3(h, 0, 0) * speed * 2 * Time.deltaTime); 

        if ((h != 0 || v != 0) && !hitting) 
        {
            transform.Translate(new Vector3(h, 0, v) * speed * Time.deltaTime); 
        }



    }


}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball")) 
        {
            Vector3 dir = aimTarget.position - transform.position; 
            other.GetComponent<Rigidbody>().velocity = dir.normalized * currentShot.hitForce + new Vector3(0, currentShot.upForce, 0);
            

            Vector3 ballDir = ball.position - transform.position; 
            if (ballDir.x >= 0)                                 
            {
                animator.Play("forehand");                       
            }
            else                                                  
            {
                animator.Play("backhand");
            }

            aimTarget.position = aimTargetInitialPosition;

        }
    }
}
