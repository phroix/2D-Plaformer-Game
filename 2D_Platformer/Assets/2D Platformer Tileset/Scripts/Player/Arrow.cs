using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    Rigidbody2D myRigidbody2D;
    BoxCollider2D myBoxCollider2D;
    // Start is called before the first frame update
    void Start()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        myBoxCollider2D = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float angle = Mathf.Atan2(myRigidbody2D.velocity.y, myRigidbody2D.velocity.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (myBoxCollider2D.IsTouchingLayers(LayerMask.GetMask("Foreground")))
        {
            Debug.Log("Arrow Foreground");
            myRigidbody2D.bodyType = RigidbodyType2D.Static;
            StartCoroutine(WaitUntilDestroy(2f));
        }
    }

    private IEnumerator WaitUntilDestroy(float v)
    {
        yield return new WaitForSeconds(v);
        Destroy(gameObject);
    }
}
