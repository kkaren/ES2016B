﻿using UnityEngine;
using System.Collections;

public class Building : MonoBehaviour, CanUpgrade, CanRepair, CanReceiveDamage
{
    private float currentLevel;
    public float baseHealth;
    private float totalHealth;
    private float currentHealth;

	public float upgradeFactor;
    public float upgradeCost;
	public float repairQuantity;
    public float repairCost;

    private float timeToWin; // TODO move this to Controller

	// Use this for initialization
	void Start ()
	{
	    this.currentLevel = 0;
		this.currentHealth = this.baseHealth;
	    this.totalHealth = this.baseHealth;

        this.timeToWin = 30.0f; // TODO move this to controller

        Debug.Log ("BUILDING CREATED with HP: " + this.baseHealth);
	}

	// Update is called once per frame
	void Update ()
	{
        // TODO move this to controller
        if (this.timeToWin <= 0.0f)
        {
            Application.LoadLevel("MainMenu"); // TODO remove obsolete method
        }
        this.timeToWin -= Time.deltaTime * 1;
        //
    }

	// To upgrade when there are enough coins
	public void Upgrade ()
	{
		this.totalHealth *= this.upgradeFactor;
		this.currentHealth *= this.upgradeFactor;
	    this.currentLevel++;
		Debug.Log ("BUILDING UPGRADED, now it has HP: " + this.totalHealth);
	}

	// Repair the building
	public void Repair ()
	{
		this.currentHealth += this.repairQuantity;
		if (this.currentHealth > this.totalHealth) {
			this.currentHealth = this.totalHealth;
		}
		Debug.Log ("BUILDING REPAIRED, HP: " + this.currentHealth);
	}

	// Receive damage from a projectile (shot by weapon)
	public bool ReceiveDamage (Projectile proj)
	{
		this.currentHealth -= proj.getDamage();
		Debug.Log ("Building's currentHealth: " + this.currentHealth);
		//Debug.Log ("BUILDING DAMAGED by HP: " + proj.getDamage());

		// TODO should be moved to Controller
		if (this.currentHealth <= 0.0)
		{
		    Destroy(this.gameObject);
			Application.LoadLevel ("MainMenu"); // TODO remove obsolete method
			return true;
		} else {
			return false;
		}
	}

    public float getMissingHealth()
    {
        return this.totalHealth - this.currentHealth;
    }

    void OnDestroy()
    {
        GameController.instance.notifyDeath(this); // Tell controller I'm dead
    }
}
