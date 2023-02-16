using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillCooltime : MonoBehaviour {

    public Image skillFilter;
    public Text coolTimeCounter;

    public float coolTime;
    private float currentCoolTime;

    private float coolTimeSave;
    private float skillFilterSave;

    private bool canSkill = true;

    void Start() {

        skillFilter.fillAmount = 0;
    }

    public void UseSkill() {

        if (canSkill) {

            skillFilter.fillAmount = 1;
            StartCoroutine("CoolTime");

            currentCoolTime = coolTime;
            coolTimeCounter.text = "" + currentCoolTime;
            StartCoroutine("CoolTimeCounter");

            canSkill = false;
        }
        else {

            //Debug.Log("아직은 못씀");
        }
    }

    IEnumerator CoolTime() {

        while (skillFilter.fillAmount > 0) {

            skillFilter.fillAmount -= 1 * Time.smoothDeltaTime / coolTime;
            //skillFilterSave = skillFilter.fillAmount;

            yield return null;
        }

        canSkill = true;

        yield break;
    }

    IEnumerator CoolTimeCounter() {

        while (currentCoolTime > 0) {

            yield return new WaitForSeconds(1f);

            currentCoolTime -= 1f;
            //coolTimeSave = currentCoolTime;
            coolTimeCounter.text = "" + currentCoolTime;
        }

        yield break;
    }
}
