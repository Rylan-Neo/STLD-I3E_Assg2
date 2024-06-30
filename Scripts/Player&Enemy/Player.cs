/*
* Author: Rylan Neo
* Date of creation: 12th June 2024
* Description: All code controlling player statistics, movement and death.
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.EventSystems;
using System.Data;

public class Player : MonoBehaviour
{
    // Scene index that the player is in
    public int sceneValue = 0;

    // All of these link player data to menu
    public Menu menu;
    public GameObject taskMenu;
    public TextMeshProUGUI taskRequirements;
    public HealthBar healthBar;
    public SprintBar sprintBar;
    public GameObject healthDrug;
    public TextMeshProUGUI healthDisplay;

    // Audio manager
    public AudioManager audioManager;

    // The stats of the player are stored here
    public float maxHealth = 200f;
    public float maxStamina = 150f;
    public static float currentHealth;
    public static float currentStamina;
    public float dashTime = .2f;
    public float dashSpeed = 1.5f;
    static bool sprintActive = false;

    // For the sprint, because when menu is paused it needs to stop recharging or else people can abuse it
    bool menuPaused = false;

    // Used to store player input and alter the dash direction. Not the best but it somewhat works
    private string direction;

    // Raytrace texts
    public GameObject interact;
    public GameObject caveTransition;
    public GameObject teleportTransition;
    public GameObject placeKey;

    // Stores the data of what the player has collected so far and its quantity
    public int drugsAvailable = 0;
    public int crystalsAvailable = 0;
    public int goldAvailable = 0;
    public int metalAvailable = 0;
    public int coreAvailable = 0;
    public int shipCore = 0;
    public bool keyPickedUp = false;

    // To enable the inventory of the player and the images
    public GameObject crystals;
    public GameObject gold;
    public GameObject metal;
    public GameObject core;
    public GameObject key;

    // Update the number text
    public TextMeshProUGUI drugsNum;
    public TextMeshProUGUI crystalText;
    public TextMeshProUGUI goldText;
    public TextMeshProUGUI metalText;
    public TextMeshProUGUI coreText;
    public TextMeshProUGUI keyText;


    // Player camera. These are used in raytrace
    [SerializeField]
    Transform playerCamera;
    [SerializeField]
    float interactionDistance = 5f;
    private Collectible currentCollectible;

    // Start is called before the first frame update
    void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

        // Starts at full hp and max stamina
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        currentStamina = maxStamina;
        sprintBar.SetMaxSprint(maxStamina);

        // Setting the raytrace text and health drug image to be inactive at the beginning, along with oher stuff like inventory
        interact.gameObject.SetActive(false);
        healthDrug.gameObject.SetActive(false);
        teleportTransition.gameObject.SetActive(false);
        crystals.gameObject.SetActive(false);
        gold.gameObject.SetActive(false);
        metal.gameObject.SetActive(false);
        core.gameObject.SetActive(false);
        key.gameObject.SetActive(false);
        taskMenu.gameObject.SetActive(false);
    }

    /// <summary>
    /// Updates what scene index the player is in
    /// </summary>
    /// <param name="sceneNum"></param>
    public void SceneChange(int sceneNum)
    {
        sceneValue = sceneNum;
        print(sceneNum);
    }

    // This function is enabled using the Q key input
    void OnPause()
    {
        // Opens up the pause menu
        menu.GetComponent<Menu>().MenuUI();
        if (menuPaused)
        {
            //WHY U NO WORK WHEN I PRESS Q AGAIN HUH
            menuPaused = false;
        }
        else
        {
            menuPaused = true;
        }
    }

    // An extra measure that I cannot remember if i used
    public void UpdatePause()
    {
        menuPaused = false;
    }
    // Update is called once per frame
    void Update()
    {
        // Stamina regen taking place when stamina not at max
        if (sprintActive == false && currentStamina < maxStamina)
        {
            if (menuPaused == false)
            StaminaConsumption(-0.2f);
        }

        // Making sure raytrace text stays inactive after raytrace does not detect object anymore
        interact.gameObject.SetActive(false);
        caveTransition.gameObject.SetActive(false);
        teleportTransition.gameObject.SetActive(false);
        placeKey.gameObject.SetActive(false);

        // Raycast code
        Debug.DrawLine(playerCamera.position, playerCamera.position + (playerCamera.forward * interactionDistance), Color.red);
        RaycastHit hitInfo;
        if (Physics.Raycast(playerCamera.position, playerCamera.forward, out hitInfo, interactionDistance))
        {
            //Print the name of what the raycast is looking at
            Debug.Log(hitInfo.transform.name);

            // Cave interaction text differs from normal collectibles.
            if (hitInfo.transform.TryGetComponent<Collectible>(out currentCollectible))
            {
                Debug.Log("Collectible within range: " + hitInfo.transform.name);

                // Different interactions require different text
                if (hitInfo.transform.name == "Scene swap")
                {
                    caveTransition.gameObject.SetActive(true);
                }
                else if (hitInfo.transform.name == "Activation panel")
                {
                    teleportTransition.gameObject.SetActive(true);
                }
                else if (hitInfo.transform.name == "Altar")
                {
                    placeKey.gameObject.SetActive(true);
                }
                else
                {
                    interact.gameObject.SetActive(true);
                }
            }
            else
            {
                currentCollectible = null;
            }
        }
        else
        {
            currentCollectible = null;
        }

        // Storing the movement Key imputs of the player used for dashing
        if (Input.GetKeyDown(KeyCode.A))
        {
            direction = "left";
        }
        else if (Input.GetKeyDown(KeyCode.D)) 
        { 
            direction = "right"; 
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            direction = "forward";
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            direction = "back";
        }
    }
    void OnCollect()
    {
        // Collect the item in front of the player once detected
        if (currentCollectible != null)
        {
            currentCollectible.Collected(this);
            UpdateTask();
        }
    }
    // All purpose damage function
    public void Damage(float damage)
    {
        float beforeHealth = currentHealth;
        currentHealth -= damage;

        // Only plays when the player is injured
        if (currentHealth < beforeHealth) 
        {
            audioManager.PlaySFX(audioManager.playerHurt);
        }

        // Player death will trigger the death screen
        if (currentHealth <= 0)
        {
            menu.DeathScreen();
            audioManager.PlaySFX(audioManager.playerDead);
            Debug.Log("Player has been crabbed");
        }

        // Restricts player from going above max health by abusing the health crystal drugs
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        // Set the health and display the amount
        healthBar.SetHealth(currentHealth);
        healthDisplay.text = currentHealth.ToString();
        Debug.Log("Player hp:" + currentHealth);
    }

    // All purpose stamina consumption function
    void StaminaConsumption(float energy)
    {
        currentStamina -= energy;
        sprintBar.SetSprint(currentStamina);
    }

    // Dash CoolDown, so it cannot constantly be used
    void SprintBoolChange()
    {
        sprintActive = false;
        Debug.Log("Sprint should be recharging");
    }

    // Dash function
    void OnDash()
    {
        if (!sprintActive)
        {
            StartCoroutine(Dash());
            sprintActive = true;
            Debug.Log("Consuming energy");
            Invoke("SprintBoolChange", 0.5f);
        }
    }

    // To calculate dash math
    IEnumerator Dash()
    {
        float startTime = Time.time;
        while ((Time.time < startTime + dashTime) && (currentStamina >= 25f))
        {
            // Checks what was the previous movement input of the player and uses that as the basis for the dash direction
            if (direction == "forward")
            {
                transform.Translate(Vector3.forward * dashSpeed);
            }
            else if (direction == "back")
            {
                transform.Translate(Vector3.back * dashSpeed);
            }
            else if (direction == "left")
            {
                transform.Translate(Vector3.left * dashSpeed);
            }
            else if (direction == "right")
            {
                transform.Translate(Vector3.right * dashSpeed);
            }

            // Consume stamina
            StaminaConsumption(2.8f);
            yield return null;
        }
    }

    // Healing is enabled once the crystal is picked up
    public void EnableHealing()
    {
        healthDrug.gameObject.SetActive(true);
    }

    // Function for using heals
    void OnHeal()
    {
        if (currentHealth < maxHealth && drugsAvailable > 0)
        {
            UseDrug(1);
            Damage(-40);
            audioManager.PlaySFX(audioManager.consumeDrug);
        }
    }

    // Calculates number of heals available and displays to screen
    public void UseDrug(int quantity)
    {
        drugsAvailable -= quantity;
        drugsNum.text = "x " + drugsAvailable.ToString();
    }

    // Adds crystal, display to screen
    public void AddCrystal(int quantity)
    {
        crystals.SetActive(true);
        crystalsAvailable += quantity;
        crystalText.text = crystalsAvailable.ToString();
    }

    // Adds gold, display to screen
    public void AddGold(int quantity)
    {
        gold.SetActive(true);
        goldAvailable += quantity;
        goldText.text = goldAvailable.ToString();
    }
    // Adds metal, display to screen
    public void AddMetal(int quantity)
    {
        metal.SetActive(true);
        metalAvailable += quantity;
        metalText.text = metalAvailable.ToString();
    }

    // Adds core, display to screen
    public void AddCore(int quantity)
    {
        core.SetActive(true);
        coreAvailable += quantity;
        coreText.text = coreAvailable.ToString();
    }

    // Adds key, display to screen
    public void KeyPickedUp()
    {
        key.SetActive(true);
        keyPickedUp = true;
        keyText.text = "1".ToString();
    }

    // Display the task menu once the dialogue mentions it
    public void TaskMenu()
    {
        taskMenu.SetActive(!taskMenu.activeSelf);
    }

    // Updates the task list each time a collectible is collected
    public void UpdateTask()
    {
        taskRequirements.text = "To reconstruct engine core:\r\n---------------------------------------\r\nAccuire:\r\n\t- Crystals:  " + crystalsAvailable.ToString() +
            " / 40\r\n\t- Gold:  " + goldAvailable.ToString() + " / 20 \r\n\t- Metal:  " + metalAvailable.ToString() + 
            " / 5\r\n\t- Core:  " + coreAvailable.ToString() + " / 1\r\n---------------------------------------";

        // Changes to the final task once the requirements are met
        if (crystalsAvailable >= 40 && goldAvailable >= 20 )
        {
            if (metalAvailable >= 5 && coreAvailable >=1)
            {
                // Adds a ship core to player inventory although i didn't ahve time to make a visual cue.
                shipCore += 1;
                taskRequirements.text = "Engine core reconstructed:\r\n---------------------------------------\r\nReturn to the ship. Place the newly made core in the ship's altar.\r\n---------------------------------------\r\n";
            }
        }
    }
}
