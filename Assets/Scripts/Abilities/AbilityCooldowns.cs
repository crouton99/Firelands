using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Serialization;

public class AbilityCooldowns : MonoBehaviour
{
    [SerializeField] private Image imageCooldown;
    [SerializeField] private TMP_Text textCooldown;
    [SerializeField] private float cooldownTimer;
    [SerializeField] private float durationTimer; 

    private float cdTimeFromScript;
    private float durFromScript;
    
    private bool isCooldown = false;
    private bool isActive = false; 
    
    
    void Start()
    {
        //Makes the cooldown stuff invisible at first.
        textCooldown.gameObject.SetActive(false);
        imageCooldown.fillAmount = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (isCooldown) //Prevents spamming
        {
           applyCooldown();
        }

        if (isActive)
        {
            applyActive();
        }
    }

    void applyActive()
    {
        durationTimer -= Time.deltaTime;

        if (durationTimer <= 0.0f)
        {
            isActive = false;
            textCooldown.gameObject.SetActive(false);
            imageCooldown.fillAmount = 100.0f;
        }
        else
        {
            textCooldown.text = Mathf.RoundToInt(durationTimer).ToString();
            imageCooldown.fillAmount = 100.0f;
        }
    }
    
    void applyCooldown()
    {
        cooldownTimer -= Time.deltaTime;

        if (cooldownTimer <= 0.0f)
        {
            isCooldown = false; 
            textCooldown.gameObject.SetActive(false);
            imageCooldown.fillAmount = 0.0f;
        }
        else
        {
            textCooldown.text = Mathf.RoundToInt(cooldownTimer).ToString();
            imageCooldown.fillAmount = cooldownTimer / cdTimeFromScript;
        }
    }

    public void useAbility(float cooldownTime)
    {
        if (isCooldown)
        {
            
            //Add ability wind-down SFX here
        }
        else
        {
            isCooldown = true;
            textCooldown.gameObject.SetActive(true);
            cdTimeFromScript = cooldownTime;
            cooldownTimer = cdTimeFromScript; //Pass through the ability cooldown times.
        }
    }

    public void inUse(float dur)
    {
        durationTimer = durFromScript;
        if (isActive)
        {
            //Add ability startup SFX
        }
        else
        {
            isActive = true;
            textCooldown.gameObject.SetActive(true);
            durFromScript = dur;
            durationTimer = durFromScript; //Pass through the ability cooldown times.
        }
    }
}
