/*
* Author: Rylan Neo
* Date of creation: 29th June 2024
* Description: Starting dialogue
*/
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StartDialogue : MonoBehaviour
{
    // Connects the script to both menu and player scripts since it uses player data and menu UI
    public Menu menu;
    public Player player;

    // Failsafe boolean
    public bool instructionRead = false;
    public GameObject instructions;

    // The three text boxes used in the dialogue box
    public TextMeshProUGUI dialogueBox;
    public TextMeshProUGUI dialogueSpeaker;
    public TextMeshProUGUI button;

    // Increases index to progress the text
    public int textIndex = 0;

    // Connect to the three triggers used in the dialogue
    public GameObject trigger1;
    public GameObject trigger2;
    public GameObject trigger3;

    // Upon start it finds these 3 important scripts
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        menu = GameObject.FindGameObjectWithTag("Menu").GetComponent<Menu>();
        instructions = GameObject.FindGameObjectWithTag("Instructions");
    }

    /// <summary>
    /// Uses the trigger to activate the dialogue
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerStay(Collider other)
    {
        // Makes sure crabs don't activate the dialogue
        if (!instructionRead && other.gameObject.tag == "Player")
        {
            InstructionsMenu();
            // Lock time and set cursor free
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0f;
        }
    }

    // Sets the instruction menu active, for some reason i needed this instead of a setActive function
    public void InstructionsMenu()
    {
        instructions.SetActive(true);
        Debug.Log("Set active ran");
    }
    /// <summary>
    /// All the dialogue that is used
    /// </summary>
    public void Dialogue()
    {
        if (textIndex == 1)
        {
            dialogueBox.text = "Look around with the mouse and move with WASD." +
                "\nSpace to jump and hold Shift to sprint.\nUse the Crtl button to do a quick dash." +
                "\nLeft mouse click to use the gun in hand.";
            dialogueSpeaker.text = "System";
        }
        else if (textIndex == 2) 
        {
            dialogueBox.text = "You can use Q to bring up the pause menu." +
                "\nThere are crystals in containers that you can pick up. " +
                "Those are healing drugs which you can use by pressing the R key.";
        }
        else if (textIndex == 3) 
        {
            dialogueBox.text = "There is a teleporter behind which was automatically set when we landed. " +
                "We should use it to get off the ship and start exploraing. I need to get my engine core back.";
            dialogueSpeaker.text = "You";
            button.text = "Okay";
        }

        // Closes the text box. Not sure why teh text still remains at the previous text after opening again.
        else if (textIndex == 4) 
        {
            instructions.SetActive(false);
            trigger1.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1f;
        }
        else if (textIndex == 5)
        {
            dialogueBox.text = "Damn. This really is their home territory. There's alien crabs swarming everywhere." +
                "\nI'll need to collect some materials from these guys. Let's cause some crab carnage!";
            button.text = "Next";
            player.TaskMenu();
        }
        else if (textIndex == 6)
        {
            dialogueBox.text = "Task bar has been set. Good luck with your mission";
            dialogueSpeaker.text = "System";
            button.text = "Okay";
        }
        else if (textIndex == 7)
        {
            instructions.SetActive(false);
            trigger2.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1f;
        }
        else if (textIndex == 8)
        {
            dialogueBox.text = "A cave huh. I bet there's more crabs inside. Let's go take a look." +
                "Let's just hope nothing too crazy appears.";
            dialogueSpeaker.text = "You";
        }
        else if (textIndex == 9)
        {
            instructionRead = true;
            instructions.SetActive(false);
            trigger3.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1f;
        }

        //Increases text index to ensure that the dialogue progresses.
        textIndex++;
    }
}
