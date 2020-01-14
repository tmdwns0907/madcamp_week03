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

    public GameObject ashe;
    public GameObject anivia;

    NavMeshAgent agent;

    Vector3[] spawnArray1 = { new Vector3(-110, 10, 110), new Vector3(-110, 10, 120) };
    Vector3[] spawnArray2 = { new Vector3(110, 10, 110), new Vector3(120, 10, 110) };
    Vector3[] spawnArray3 = { new Vector3(110, 10, -110), new Vector3(110, 10, -120) };
    Vector3[] spawnArray4 = { new Vector3(-110, 10, -110), new Vector3(-110, 10, -110) };

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
        if(!isServer)
            return;
        if (collision.transform.tag == "FirstWall")
        {
            Vector3 spawnPoint = Vector3.zero;
            spawnPoint = spawnArray2[Random.Range(0, spawnArray2.Length)];
            GameObject temp;
            int temp_num = Random.Range(0, 2);
            if (temp_num == 0)
                temp = Instantiate(ashe, spawnPoint, Quaternion.identity);
            else
                temp = Instantiate(anivia, spawnPoint, Quaternion.identity);
            temp.GetComponent<Score>().setScore(GetComponent<Score>().getScore());
            CmdSpawn(temp);
        }
        else if (collision.transform.tag == "SecondWall")
        {
            Vector3 spawnPoint = Vector3.zero;
            spawnPoint = spawnArray3[Random.Range(0, spawnArray3.Length)];
            GameObject temp;
            int temp_num = Random.Range(0, 2);
            if (temp_num == 0)
                temp = Instantiate(ashe, spawnPoint, Quaternion.identity);
            else
                temp = Instantiate(anivia, spawnPoint, Quaternion.identity);
            temp.GetComponent<Score>().setScore(GetComponent<Score>().getScore());
            CmdSpawn(temp);
        }
        else if (collision.transform.tag == "ThirdWall")
        {
            Vector3 spawnPoint = Vector3.zero;
            spawnPoint = spawnArray4[Random.Range(0, spawnArray4.Length)];
            GameObject temp;
            int temp_num = Random.Range(0, 2);
            if (temp_num == 0)
                temp = Instantiate(ashe, spawnPoint, Quaternion.identity);
            else
                temp = Instantiate(anivia, spawnPoint, Quaternion.identity);
            temp.GetComponent<Score>().setScore(GetComponent<Score>().getScore());
            CmdSpawn(temp);
        }
        else if (collision.transform.tag == "FourthWall")
        {
            gameObject.GetComponent<Score>().TakeScore();
            Vector3 spawnPoint = Vector3.zero;
            spawnPoint = spawnArray1[Random.Range(0, spawnArray1.Length)];
            GameObject temp;
            int temp_num = Random.Range(0, 2);
            if (temp_num == 0)
                temp = Instantiate(ashe, spawnPoint, Quaternion.identity);
            else
                temp = Instantiate(anivia, spawnPoint, Quaternion.identity);
            temp.GetComponent<Score>().setScore(GetComponent<Score>().getScore());
            CmdSpawn(temp);
        }
    }

}
