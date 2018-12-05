using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BossTrigger : NetworkBehaviour {

    public GameObject BossBody;

    public GameObject Barriers;
    public Transform BarriersPos;

    private int PlayerCount = 0;
    private int NumberOfPlayers;

    private bool BossSpownd = false;

    private void LateUpdate()
    {
        NumberOfPlayers = GameObject.FindGameObjectsWithTag("Player").Length;
        if (NumberOfPlayers > 0)
        {
            if (PlayerCount == NumberOfPlayers && !BossSpownd)
            {
                CmdSpownBoss();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag.Equals("Player"))
        {
            PlayerCount++;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            PlayerCount--;
        }
    }

    private void SpownBoss()
    {
        BossBody.SetActive(true);
        BossBody.GetComponentInParent<Enemy>().enabled = true;
        BossBody.GetComponentInParent<CapsuleCollider>().enabled = true;
        Instantiate(Barriers, BarriersPos.position, Quaternion.identity);
        this.gameObject.SetActive(false);
    }

    [Command]
    private void CmdSpownBoss()
    {
        Debug.Log("got here");
        SpownBoss();
        BossSpownd = true;
        RpcSpownBoss();
    }

    [ClientRpc]
    private void RpcSpownBoss()
    {
        if(!isServer)
        {
            SpownBoss();
            BossSpownd = true;
        }
    }
}
