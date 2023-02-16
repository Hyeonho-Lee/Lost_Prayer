using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestCooltime : MonoBehaviour {

    public Image skillFilter;
    public Text coolTimeCounter;

    public float coolTime;
    private float currentCoolTime;

    private bool canSkill = true;

    void Start() {

        skillFilter.fillAmount = 0;
    }

    public void UseSkill() {

        if(canSkill) {

            Debug.Log("스킬사용");
            skillFilter.fillAmount = 1;
            StartCoroutine("CoolTime");

            currentCoolTime = coolTime;
            coolTimeCounter.text = "" + currentCoolTime;
            StartCoroutine("CoolTimeCounter");

            canSkill = false;
        }else {

            Debug.Log("아직은 못씀");
        }
    }

    IEnumerator CoolTime(){

        while(skillFilter.fillAmount > 0) {

            skillFilter.fillAmount -= 1 * Time.smoothDeltaTime / coolTime;

            yield return null;
        }

        canSkill = true;

        yield break;
    }

    IEnumerator CoolTimeCounter() {

        while(currentCoolTime > 0) {

            yield return new WaitForSeconds(1f);

            currentCoolTime -= 1f;
            coolTimeCounter.text = "" + currentCoolTime;
        }

        yield break;
    }
}
