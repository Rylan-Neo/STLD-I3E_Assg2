using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadCave : Collectible
{
    public static bool enteredCave = false;
    public void MenuSwap()
    {
        enteredCave = false;
    }
    public override void Collected(Player player)
    {
        Debug.Log("Scene swapping... ");
        base.Collected(player);
        if (enteredCave == false)
        {
            SceneManager.LoadScene(2);
            enteredCave = false;
        }
        else
        {
            SceneManager.LoadScene(0);
            enteredCave = true;
        }
    }
}
