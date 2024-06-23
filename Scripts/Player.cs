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
    public Menu menu;
    public HealthBar healthBar;
    public SprintBar sprintBar;
    public TextMeshProUGUI healthDisplay;
    public static float currentHealth;
    public static float currentStamina;
    public float maxHealth = 200f;
    public float maxStamina = 150f;
    public float dashTime = .2f;
    public float dashSpeed = 3f;
    static bool sprintActive = false;
    private string direction;
    public GameObject interact;
    public GameObject caveTransition;

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

        interact.gameObject.SetActive(false);
    }
    void OnPause()
    {
        menu.GetComponent<Menu>().MenuUI();
    }
    // Update is called once per frame
    void Update()
    {
        if (sprintActive == false && currentStamina < maxStamina)
        {
            StaminaConsumption(-0.1f);
        }

        interact.gameObject.SetActive(false);
        caveTransition.gameObject.SetActive(false);
        Debug.DrawLine(playerCamera.position, playerCamera.position + (playerCamera.forward * interactionDistance), Color.red);
        RaycastHit hitInfo;
        if (Physics.Raycast(playerCamera.position, playerCamera.forward, out hitInfo, interactionDistance))
        {
            //Print the name of what the raycast is looking at
            Debug.Log(hitInfo.transform.name);
            if (hitInfo.transform.name == "Scene swap")
            {
                caveTransition.gameObject.SetActive(true);
            }
            else
            {
                interact.gameObject.SetActive(true);
            }
            if (hitInfo.transform.TryGetComponent<Collectible>(out currentCollectible))
            {
                Debug.Log("Collectible within range: " + hitInfo.transform.name);
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
        if (currentCollectible != null)
        {
            currentCollectible.Collected(this);
        }
    }
    // All purpose damage function
    void Damage(float damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        if (currentHealth <= 0)
        {
            menu.GetComponent<Menu>();
        }
    }
    // All purpose stamina consumption function
    void StaminaConsumption(float energy)
    {
        currentStamina -= energy;
        sprintBar.SetSprint(currentStamina);
    }
    void PlayerDeath()
    {

    }
    // Damage test + health bar
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Damage(10);
            Debug.Log(currentHealth);
            healthDisplay.text = currentHealth.ToString();
        }
    }
    void SprintBoolChange()
    {
        sprintActive = false;
        Debug.Log("Sprint should be recharging");
    }
    void OnDash()
    {
        StartCoroutine(Dash());
        sprintActive = true;
        Debug.Log("Consuming energy");
        Invoke("SprintBoolChange", 0.5f);

    }
    IEnumerator Dash()
    {
        float startTime = Time.time;
        while ((Time.time < startTime + dashTime) && (currentStamina >= 25f))
        {
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
            StaminaConsumption(2f);
            yield return null;
        }
    }
}
