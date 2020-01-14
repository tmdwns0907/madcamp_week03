using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnCollisionEnter(Collision collision)
    {
        //
        var hit = collision.gameObject;
        var health = hit.GetComponent<Health>();

        if (health != null)
        {
            health.TakeDamage(10);
        }
        var score = hit.GetComponent<Score>();

        if (score != null)
        {
            Debug.Log("it's not null");
            score.TakeScore();
        }
        else
            Debug.Log("It's null");

        Destroy(gameObject);
    }
}
