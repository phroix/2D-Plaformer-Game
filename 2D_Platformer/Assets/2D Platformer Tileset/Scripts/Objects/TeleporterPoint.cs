using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleporterPoint : MonoBehaviour
{
    bool playerDetected = false;
    [SerializeField]
    int pointId;

    Teleporter teleporter;
    // Start is called before the first frame update
    void Start()
    {
        teleporter  = transform.parent.GetComponent<Teleporter>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(playerDetected);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name != "Player") return;
        //Debug.Log(gameObject.name);
        teleporter.SetPointId(pointId);
        playerDetected = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name != "Player") return;
        playerDetected = false;

        StartCoroutine(WaitForTeleport());
    }

    private IEnumerator WaitForTeleport()
    {
        yield return new WaitForSeconds(2);
        playerDetected = false;
    }

    public bool GetPlayerDetected()
    {
        return playerDetected;
    }
}
