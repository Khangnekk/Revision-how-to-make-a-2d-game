using Cinemachine;
using UnityEngine;

public class CameraShakeManager : MonoBehaviour
{
	public static CameraShakeManager instance;
	[SerializeField] private float globalShakeForce = 1f;
	[SerializeField] protected CinemachineImpulseListener impulseListener;

	private CinemachineImpulseDefinition impulseDefinition;

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
	}

	public void CameraShake(CinemachineImpulseSource impulseSource)
	{
		impulseSource.GenerateImpulseWithForce(globalShakeForce);
	}

	public void ScreenShakeFromProfile(ScreenShakeProfile profile, CinemachineImpulseSource impulseSource)
	{
		// apply settings
		SetupScreenShakeSettings(profile, impulseSource);
		// screenShake
		impulseSource.GenerateImpulseWithForce(profile.impactForce);
	}

	private void SetupScreenShakeSettings(ScreenShakeProfile profile, CinemachineImpulseSource impulseSource)
	{
		impulseDefinition = impulseSource.m_ImpulseDefinition;

		// change the impulse setting
		impulseDefinition.m_ImpulseDuration = profile.impactTime;
		impulseSource.m_DefaultVelocity = profile.defaultVelocity;
		impulseDefinition.m_CustomImpulseShape = profile.impulseCurve;

		// change the impulse listener setting
		impulseListener.m_ReactionSettings.m_AmplitudeGain = profile.listenerAmplitude;
		impulseListener.m_ReactionSettings.m_FrequencyGain = profile.listenerFrequence;
		impulseListener.m_ReactionSettings.m_Duration = profile.listenerDuaration;
	}
}
