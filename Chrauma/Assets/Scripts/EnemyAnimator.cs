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
        animator = GetComponent<Animator>();
		parent = gameObject.transform.parent;
		oldPosition = parent.position;
        //rb = parent.GetComponent<Rigidbody>();
    }

    void Update()
    {
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
		animator.SetTrigger("isAttacking");
	}
}
