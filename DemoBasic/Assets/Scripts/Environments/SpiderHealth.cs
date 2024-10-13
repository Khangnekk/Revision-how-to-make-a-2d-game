using UnityEngine;

public class SpiderHealth : MonoBehaviour, IDamageable
{
	[SerializeField] private float maxHealth = 1f;
	private float currentHealth;
	public bool HasTakenDamage { get; set; }
	private void Start()
	{
		currentHealth = maxHealth;
	}

	public void Damage(float damageAmount)
	{
		HasTakenDamage = true;
		currentHealth -= damageAmount;
		if (currentHealth < 0)
		{
			Die();
		}
	}

	private void Die()
	{
		Destroy(gameObject);
	}
}
