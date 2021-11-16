using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBehavior : MonoBehaviour
{
    Animator myAnimator;
    System.Random random;
    float nextAnimation = 0;
    float randomTime;
    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
        random = new System.Random();
    }

    // Update is called once per frame
    void Update()
    {
        ChangeAnimation();
    }

    private void ChangeAnimation()
    {
        // if(Time.time > nextAnimation)
        // {
        //     randomTime = random.Next(5,11);
        //     myAnimator.SetTrigger("Action");
        //     nextAnimation = Time.time + randomTime;
        // }
        // StartCoroutine(ChangeRandomAnimation());
    }

    // private IEnumerator ChangeRandomAnimation()
    // {
    //     randomTime = random.Next(5,11);
    //     myAnimator.SetBool("Action1", true);
    //     Debug.Log(randomTime);
    //     yield return new WaitForSeconds(randomTime);
    //     myAnimator.SetBool("Action1",false);
    // }

}
