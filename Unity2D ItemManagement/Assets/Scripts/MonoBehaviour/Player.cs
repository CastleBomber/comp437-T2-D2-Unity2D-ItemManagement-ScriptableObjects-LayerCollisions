using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
	public HealthBar healthBarPrefab;
	HealthBar healthBar;
	public Inventory inventoryPrefab;
	Inventory inventory;

	public void Start()
	{
		inventory = Instantiate(inventoryPrefab);
		hitPoints.value = startingHitPoints;
		healthBar = Instantiate(healthBarPrefab);
		healthBar.character = this;
	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("CanBePickedUp"))
		{
			Item hitObject = collision.gameObject.GetComponent<Consumable>().item;

			if(hitObject != null)
			{
				bool shouldDisappear = false;

				switch (hitObject.itemType)
				{
					case Item.ItemType.COIN:
						shouldDisappear = inventory.AddItem(hitObject);
						//shouldDisappear = true;
						break;

					case Item.ItemType.HEALTH:
						shouldDisappear = AdjustHitPoints(hitObject.quantity);
						break;
					default:
						break;
				}

				if (shouldDisappear)
				{
					collision.gameObject.SetActive(false);
				}
			}
			
		}
	}

	public bool AdjustHitPoints (int amount)
	{
		if (hitPoints.value < maxHitPoints)
		{
			hitPoints.value += amount;
			print("Adjusted hitpoints by: " + amount + ". New value: " + hitPoints.value);
		}

		return true;
	}
}
