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
using UnityEditor.Experimental.GraphView;

public class Player : MonoBehaviour
{
    public int sceneValue = 0;
    public Menu menu;
    public HealthBar healthBar;
    public SprintBar sprintBar;
    public GameObject healthDrug;
    public TextMeshProUGUI healthDisplay;
    public static float currentHealth;
    public static float currentStamina;
    public float maxHealth = 200f;
    public float maxStamina = 150f;
    public float dashTime = .2f;
    public float dashSpeed = 1.5f;
    static bool sprintActive = false;
    bool menuPaused = false;
    private string direction;
    public GameObject interact;
    public GameObject caveTransition;
    public GameObject teleportTransition;
    public TextMeshProUGUI drugsNum;
    public int drugsAvailable = 0;
    public int crystalsAvailable = 0;
    public int goldAvailable = 0;
    public int metalAvailable = 0;
    public int coreAvailable = 0;
    public bool keyPickedUp = false;
    public GameObject crystals;
    public GameObject gold;
    public GameObject metal;
    public GameObject core;
    public GameObject key;
    public TextMeshProUGUI crystalText;
    public TextMeshProUGUI goldText;
    public TextMeshProUGUI metalText;
    public TextMeshProUGUI coreText;
    public TextMeshProUGUI keyText;

    [SerializeField]
    Transform playerCamera;
    [SerializeField]
    float interactionDistance = 5f;
    private Collectible currentCollectible;

    // Start is called before the first frame update
    void Start()
    {
        // Starts at full hp and max stamina
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        currentStamina = maxStamina;
        sprintBar.SetMaxSprint(maxStamina);

        // Setting the raytrace text and health drug image to be inactive at the beginning
        interact.gameObject.SetActive(false);
        healthDrug.gameObject.SetActive(false);
        teleportTransition.gameObject.SetActive(false);
        crystals.gameObject.SetActive(false);
        gold.gameObject.SetActive(false);
        metal.gameObject.SetActive(false);
        core.gameObject.SetActive(false);
        key.gameObject.SetActive(false);
    }

    public void SceneChange(int sceneNum)
    {
        sceneValue = sceneNum;
        print(sceneNum);
    }

    void OnPause()
    {
        // Opens up the pause menu
        menu.GetComponent<Menu>().MenuUI();
        if (menuPaused)
        {
            menuPaused = false;
        }
        else
        {
            menuPaused = true;
        }
    }
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
                if (hitInfo.transform.name == "Scene swap")
                {
                    caveTransition.gameObject.SetActive(true);
                }
                else if (hitInfo.transform.name == "Activation panel")
                {
                    teleportTransition.gameObject.SetActive(true);
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
        }
    }
    // All purpose damage function
    public void Damage(float damage)
    {
        currentHealth -= damage;

        // Player death will trigger the death screen
        if (currentHealth <= 0)
        {
            menu.DeathScreen();
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
            StaminaConsumption(2f);
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
            Damage(-30);
        }
    }
    // Calculates number of heals available and displays to screen
    public void UseDrug(int quantity)
    {
        drugsAvailable -= quantity;
        drugsNum.text = "x " + drugsAvailable.ToString();
    }
    public void AddCrystal(int quantity)
    {
        crystals.SetActive(true);
        crystalsAvailable += quantity;
        crystalText.text = crystalsAvailable.ToString();
    }
    public void AddGold(int quantity)
    {
        gold.SetActive(true);
        goldAvailable += quantity;
        goldText.text = goldAvailable.ToString();
    }
    public void AddMetal(int quantity)
    {
        metal.SetActive(true);
        metalAvailable += quantity;
        metalText.text = metalAvailable.ToString();
    }
    public void AddCore(int quantity)
    {
        core.SetActive(true);
        coreAvailable += quantity;
        coreText.text = coreAvailable.ToString();
    }
    public void KeyPickedUp()
    {
        key.SetActive(true);
        keyPickedUp = true;
        keyText.text = "1".ToString();
    }
}
