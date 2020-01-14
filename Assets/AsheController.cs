using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.AI;
using UnityEngine.UI;

public class AsheController : NetworkBehaviour
{
    // Start is called before the first frame update
    public GameObject bulletPrefab;

    public Transform bulletSpawn;
    public Transform playerSpawn;

    NavMeshAgent agent;

    private NetworkStartPosition spawnPoints;
    //Text myScore;
    public Text scoreBoard;
    //Text myScore;
    int count = 0;
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        /* myScore = GameObject.Find("Score").GetComponent<Text>();
         myScore.text = "Count : " + count.ToString();*/
    }
    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        /*var x = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
        var z = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;

        transform.Rotate(0, x, 0);
        transform.Translate(0, 0, z);*/
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
    }

    [Command]
    void CmdFire()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);

        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 30;

        NetworkServer.Spawn(bullet);

        Destroy(bullet, 2.0f);
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
        if (collision.transform.tag == "Tree")
        {
            Vector3 spawnPoint = Vector3.zero;
            spawnPoint = playerSpawn.transform.position;
            GameObject temp = GameObject.FindGameObjectWithTag("Player");
            GameObject ashe = Instantiate(temp, spawnPoint, Quaternion.identity);
            CmdSpawn(ashe);
        }
        //Destroy(collision.gameObject);
    }

}
