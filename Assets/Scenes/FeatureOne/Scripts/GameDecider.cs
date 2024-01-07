using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameDecider : MonoBehaviour
{
    bool didPlayerWin = false;


    public void CheckLives(int Lives, string beyblade)
    {
        if (Lives < 20)
        {
            if(beyblade == "player")
            {
                Endgameholder.status = "lost";
                SceneManager.LoadScene(2);
            }
            if (beyblade == "enemy")
            {
                Endgameholder.status = "won";
                SceneManager.LoadScene(2);
            }
        }
    }
}
