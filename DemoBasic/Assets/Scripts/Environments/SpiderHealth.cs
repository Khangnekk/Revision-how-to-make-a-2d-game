using Cinemachine;
using UnityEngine;

public class SpiderHealth : MonoBehaviour, IDamageable
{
	[SerializeField] private float maxHealth = 1f;
	[SerializeField] private ScreenShakeProfile profile;
	private float currentHealth;
	public bool HasTakenDamage { get; set; }
	private CinemachineImpulseSource impulseSource;
	private void Start()
	{
		currentHealth = maxHealth;
		impulseSource = GetComponent<CinemachineImpulseSource>();
	}

	public void Damage(float damageAmount)
	{
		//CameraShakeManager.instance.CameraShake(impulseSource);
		CameraShakeManager.instance.ScreenShakeFromProfile(profile, impulseSource);

		HasTakenDamage = true;
		currentHealth -= damageAmount;
		if (currentHealth <= 0)
		{
			Die();
		}
	}

	private void Die()
	{
		Destroy(gameObject);
	}
}
