using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;

public class Character : MonoBehaviour {
	
	public Animator animator;

	//public GameObject weapon;
	public Transform target;
	public GameObject bulletPrefab;
	public GameObject TargetEffectPrefab;
	private GameObject TargetEffect;

	public float fireRate = 5f;
    public float fireCountdown = 0f;

	public bool isAttacking = false;
	private bool isAttack = false;

	public Transform firePoint;
    public Vector3 RandomizeIntensity = new Vector3(3.5f, 0, 0);
    public Vector3 Offset = new Vector3(0, 2, 0);

	void Start() {
		
		TargetEffect = (GameObject)Instantiate(TargetEffectPrefab, transform.position, Quaternion.Euler(90f, 0f, 0f)) as GameObject;
		target = null;
	}

	void Update() {
		
		Delay();

		if(target != null) {

			StartCoroutine(Attacking());
			animator.SetBool("isAttacking", true);
			transform.LookAt(target);
			//InvokeRepeating("Delay", 0f, 0.1f);
			TargetEffect.transform.position = target.transform.position;
		}else {

			isAttacking = false;
			animator.SetBool("isAttacking", false);
			//animator.SetBool("isAttacking", false);
			//CancelInvoke("Delay");
			Destroy(TargetEffect.gameObject);
		}
	}

	public void Attack() {
		
		TargetEffect = (GameObject)Instantiate(TargetEffectPrefab, target.position, Quaternion.Euler(90f, 0f, 0f)) as GameObject;
	}

	public void AttackStop() {
		
		target = null;
	}

	void Delay() {

        if (fireCountdown <= 0f) {
			
			isAttack = true;

			if(isAttacking == true && isAttack == true) {
				StartCoroutine(Shoot());
				isAttack = false;
				fireCountdown = 1f / fireRate;
			}
            //SoundController.Soundinstance.ShootSound();
        }

		if(isAttack != true) {
			fireCountdown -= Time.deltaTime;
		}
    }

	IEnumerator Shoot() {

		//AudioManager.instance.PlaySound("Tower_0 Shoot Sound", transform.position);
        firePoint.position += Offset;
        firePoint.position += new Vector3(Random.Range(-RandomizeIntensity.x, RandomizeIntensity.x),
                                Random.Range(-RandomizeIntensity.y, RandomizeIntensity.y),
                                Random.Range(-RandomizeIntensity.y, RandomizeIntensity.z));
        //AttackEffect();
        GameObject bulletShoot = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullets bullet = bulletShoot.GetComponent<Bullets>();

        if (bullet != null) {

			bullet.Seek(target);
		}

		yield return null;
    }

	IEnumerator Attacking() {
		
        yield return new WaitForSeconds(fireRate);
		isAttacking = true;
       //animator.SetBool("isAttacking", false);
    }

    IEnumerator Reloading() {

        animator.SetBool("isReloading", true);
        yield return new WaitForSeconds(fireRate);
        animator.SetBool("isReloading", false);
    }
}
