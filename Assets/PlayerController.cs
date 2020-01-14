using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.AI;
using UnityEngine.UI;

public class PlayerController : NetworkBehaviour
{
    // Start is called before the first frame update
    public GameObject bulletPrefab;
    public GameObject arrow;

    public Transform bulletSpawn;
    public Transform arrowSpawn;

    public GameObject ashe;
    public GameObject anivia;

    public GameObject[] champArray;
    NavMeshAgent agent;

    Vector3[] spawnArray = { new Vector3(110, 10, 110), new Vector3(120, 10, 110) };
    //Text myScore;
    int count = 0;
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    // Update is called once per frame
    void Update()
    {

        if(!isLocalPlayer)
        {
            return;
        }

        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                agent.SetDestination(hit.point);
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            CmdFire();
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            CmdArrowFire();
        }
    }

    [Command]
    void CmdFire()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);

        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 30;

        NetworkServer.Spawn(bullet);

        Destroy(bullet, 2.0f);
    }
    void CmdArrowFire()
    {
        GameObject bullet2 = Instantiate(arrow, arrowSpawn.position, arrowSpawn.rotation);
        
        bullet2.GetComponent<Rigidbody>().velocity = bullet2.transform.forward * 30;

        NetworkServer.Spawn(bullet2);

        Destroy(bullet2, 2.0f);
    }

    [Command]
    void CmdSpawn(GameObject go)
    {
        NetworkServer.Spawn(go);
        NetworkServer.ReplacePlayerForConnection(connectionToClient, go, playerControllerId);
        Destroy(gameObject);
    }
    private void OnCollisionEnter(Collision collision)
    {

        Debug.Log("Collision");
        if (collision.transform.tag == "FirstWall")
        {
            if (!isServer)
                return;
            Vector3 spawnPoint = Vector3.zero;
            spawnPoint = spawnArray[Random.Range(0, spawnArray.Length)];
            GameObject temp;
            int temp_num = Random.Range(0, 2);
            if(temp_num==0)
                temp = Instantiate(ashe, spawnPoint, Quaternion.identity);
            else
                temp = Instantiate(anivia, spawnPoint, Quaternion.identity);
            temp.GetComponent<Score>().setScore(GetComponent<Score>().getScore());
            CmdSpawn(temp);
        }
        //Destroy(collision.gameObject);
    }

}
