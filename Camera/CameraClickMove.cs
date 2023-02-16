using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;

public class CameraClickMove : MonoBehaviour {

    public Camera camera;
    public NavMeshAgent agent;
    public Animator animator;
    public Transform Target;
	public GameObject PosEffectPrefab;

    public bool isMainCharacter = false;

    public Vector3 HitPos;
    private float distance;
    private int PlayerCount = 0;
    private float CheckArea = 5f;

	private Character character;

	void Start() {
	
		character = GameObject.FindObjectOfType<Character>();
	}

    void Update() {
        
        if(isMainCharacter == true) {

            Target = null;
            CheckArea = 0f;

            if (Input.GetMouseButtonDown(0)) {

                Ray ray = camera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                animator.SetBool("isRunning", true);

                if (Physics.Raycast(ray, out hit)) {

					if(hit.collider.gameObject.tag == "Enemy") {
				
						character.target = hit.collider.transform;
						character.Attack();
					}else {
						
						if(hit.collider.gameObject.tag == "ground") {
							
							character.target = null;
							agent.SetDestination(hit.point);
							HitPos = hit.point;
						}

						if(character.target != null) {

							character.AttackStop();
						}
					}

					GameObject effect_0 = (GameObject)Instantiate(PosEffectPrefab, HitPos, transform.rotation);
					Destroy(effect_0, 2f);

					Debug.DrawRay(ray.origin, ray.direction * 10f, Color.green, 5f);
                }
            }

            if (Input.GetKey(KeyCode.O)) {

                StopAllCoroutines();
                StartCoroutine(Attacking());
            }

            if (Input.GetKey(KeyCode.P)) {

                StopAllCoroutines();
                StartCoroutine(Reloading());
            }

            distance = Vector3.Distance(HitPos, this.transform.position);

            if (distance <= 1.5f){

                animator.SetBool("isRunning", false);
            }

        }else {

            CheckPlayer();
        }
    }

    void CheckPlayer() {

        Collider[] colliders = Physics.OverlapSphere(this.transform.position, CheckArea);

        foreach (Collider hit in colliders) {

            if (hit.tag == "Player") {

                PlayerCount++;
            }
        }

        if (PlayerCount >= 2) {

            agent.SetDestination(this.transform.position);
			animator.SetBool("isRunning", false);

            PlayerCount = 0;
        }else{

            agent.SetDestination(Target.position);
			animator.SetBool("isRunning", true);
			if(agent.speed == 0) {

				animator.SetBool("isRunning", false);
			}

            PlayerCount = 0;
        }
    }

    public void ChangePlayer() {

        if(isMainCharacter == true) {

            isMainCharacter = false;
        }else {

            isMainCharacter = true;
        }
    }

    IEnumerator Attacking() {

        animator.SetBool("isAttacking", true);
        yield return new WaitForSeconds(.5f);
        animator.SetBool("isAttacking", false);
    }

    IEnumerator Reloading() {

        animator.SetBool("isReloading", true);
        yield return new WaitForSeconds(.5f);
        animator.SetBool("isReloading", false);
    }
}
