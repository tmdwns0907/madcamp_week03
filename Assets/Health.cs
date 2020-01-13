using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
public class Health : NetworkBehaviour
{
    // Start is called before the first frame update
    public const int maxHealth = 100;

    // Update is called once per frame
    [SyncVar(hook ="OnChangeHealth")]

    public int currentHealth = maxHealth;

    public Slider healthSlider;

    private NetworkStartPosition[] spawnPoints;

    private void Start()
    {
        if(isLocalPlayer)
        {
            spawnPoints = FindObjectsOfType<NetworkStartPosition>();
        }
    }
    public void TakeDamage(int amount)
    {
        if(!isServer)
        {
            return;
        }

        currentHealth -= amount;

        if(currentHealth<=0)
        {
            currentHealth = maxHealth;
            //RPC: 서버에서 발동하면 모든 클라이언트들에서 자동으로 발동
            RpcRespawn();
        }
    }

    void OnChangeHealth(int health)
    {
        healthSlider.value = health;
    }

    [ClientRpc]

    void RpcRespawn()
    {
        if(isLocalPlayer)
        {
            Vector3 spawnPoint = Vector3.zero;

            if(spawnPoints!=null && spawnPoints.Length>0)
            {
                spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position;
            }
            transform.position = spawnPoint;
        }
    }
}
