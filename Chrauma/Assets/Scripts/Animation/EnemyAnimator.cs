using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
private Animator animator;
private Rigidbody rb;
private Transform parent;
private Vector3 oldPosition;
private bool isWalking = false;

    void Start()
    {
        /* assign objects to variables */
        animator = GetComponent<Animator>();
		parent = gameObject.transform.parent;
		oldPosition = parent.position;
    }

    void Update()
    {
		/* check if gameobject is moving to activate walking animation */
		if(parent.position != oldPosition)
		{
			isWalking = true;
			oldPosition = parent.position;
		}
		else{
			isWalking = false;
		}

        animator.SetBool("isWalking", isWalking);
        animator.SetBool("isIdle", !isWalking);
    }
	public void startAttackAnimation(){
		/* launch the attack animation */
		animator.SetTrigger("isAttacking");
	}
}
