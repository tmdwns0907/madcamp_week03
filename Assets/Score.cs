using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Score : NetworkBehaviour
{
    public const int initial_score = 0;


    // Update is called once per frame
    [SyncVar(hook = "OnChangeScore")]
    public int score = initial_score;


    public Text scoreBoard;
    public int passed;

    private void Start()
    {
        if (isLocalPlayer)
        {
            scoreBoard.text = score.ToString();
        }
    }
    public void setScore(int s)
    {
        score = s;
    }
    public int getScore()
    {
        return score;
    }
    public void TakeScore()
    {
        if (!isServer)
        {
            return;
        }
        Debug.Log("Score took");
        score++;
        //RPC: 서버에서 발동하면 모든 클라이언트들에서 자동으로 발동
    }

    void OnChangeScore(int s)
    {
        Debug.Log("score changed");
        Debug.Log(s);
        scoreBoard.text = s.ToString();
    }

}
