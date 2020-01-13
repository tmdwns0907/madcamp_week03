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

    public Transform bulletSpawn;

    NavMeshAgent agent;
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

        if(!isLocalPlayer)
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

    /*private void OnTriggerEnter(Collider other)
    {
        if(other.Equals(bulletPrefab))
        {
            count++;
            myScore.text = "Count : " + count.ToString();
        }
    }*/
    /*private void OnCollisionEnter(Collision collision)
    {
        //
        if (collision.transform.tag=="Bullet")
        {
            count++;
            myScore.text = "Count : " + count.ToString();
        }
        //Destroy(collision.gameObject);
    }*/
}
