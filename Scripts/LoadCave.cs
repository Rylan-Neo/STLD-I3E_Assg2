using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadCave : Collectible
{
    public bool enteredCave = false;
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
            enteredCave = true;
            Debug.Log(enteredCave);
        }
        else
        {
            SceneManager.LoadScene(1);
            enteredCave = false;
        }
    }
    private void Start()
    {
        MenuSwap();
        Debug.Log(enteredCave);
    }
}
