using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Boss : Enemy {

    public Text EndText;
    public GameObject Canvas;

    private void LateUpdate()
    {
        foreach (GameObject player in players)
        {
            float distansToPlayer = Vector3.Distance(player.transform.position, transform.position);
            if (distansToPlayer < AttackRange + 2)
            {
                return;
            }
        }
        CmdEngGame("YOU LOSE");
    }

    public override void CmdTakeDamage(float damage)
    {
        this.currentHealth = Mathf.Clamp(currentHealth - damage, 0, MaxHealth);
        if (currentHealth <= 0)
        {
            CmdEngGame("YOU WON");   
        }
    }

    [Command]
    void CmdEngGame(string EndGameMsg)
    {
        EndText.text = EndGameMsg;
        Canvas.SetActive(true);
        Time.timeScale = 0;
        RpcEngGame(EndGameMsg);
        Destroy(gameObject);
    }

    [ClientRpc]
    void RpcEngGame(string EndGameMsg)
    {
        if (!isServer)
        {
            EndText.text = EndGameMsg;
            Canvas.SetActive(true);
            Time.timeScale = 0;
            Destroy(gameObject);
        }
    }
}
