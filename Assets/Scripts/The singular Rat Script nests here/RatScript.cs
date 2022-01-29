using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatScript : MonoBehaviour
{
    public void TriggerDeathAnimation()
    {
        GetComponent<SimplestAnimatorPossible>().enabled = false;
        GetComponent<AlmostSimplestAnimator>().enabled = true;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        foreach (var item in GetComponents<Collider2D>())
        {
            item.enabled = false;
        }
        GetComponent<RoTaTeToWaRdSvElOcItY>().enabled = false;
        RaTgOeSrOtAtE();
        GetComponent<AlmostSimplestAnimator>().onCompleted.AddListener(delegate { Destroy(gameObject); });
        GetComponent<AlmostSimplestAnimator>().onCompleted.AddListener(delegate { UnityEngine.SceneManagement.SceneManager.LoadScene(0, UnityEngine.SceneManagement.LoadSceneMode.Single); });
    }

    void RaTgOeSrOtAtE()
    {
        transform.rotation = Quaternion.identity;
    }
}
