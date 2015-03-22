using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour {
	private int health, stamina;
	public Text healthText, staminaText;

	public void Start() {
		health = 3;
		stamina = 3;
	}

	public void Update() {
		healthText.text = health.ToString();
		staminaText.text = stamina.ToString();
	}

	public void applyDamage(int damage) {
		health -= damage;
	}

	public bool isDead() {
		return health <= 0;
	}

	public void payStamina(int stamina) {
		this.stamina -= stamina;
	}

	public bool canPayStamina(int cost) {
		return stamina >= cost;
	}

	public void regainStamina() {
		if (stamina < 3) {
			stamina++;
		}
	}
}
