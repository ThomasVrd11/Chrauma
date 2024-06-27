using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ClawAttack : MonoBehaviour
{

	[SerializeField] int damage;
	private bool damageCD = false;


	private void Start()
	{
	}
	private void OnTriggerEnter(Collider other) {
		if (other.name == "Player" && !damageCD) {
			PlayerStats.instance.TakeDamage(damage);
			damageCD = true;
			StartCoroutine(CooldownAttack());
		}
	}
	private IEnumerator CooldownAttack()
	{
		yield return new WaitForSeconds(2);
		damageCD = false;
	}
}
