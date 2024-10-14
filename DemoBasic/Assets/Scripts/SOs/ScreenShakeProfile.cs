using UnityEngine;
// ScriptableObject giúp tạo các tệp dữ liệu nhỏ 1 cách dễ dàng
[CreateAssetMenu(menuName = "ScreenShake/New Profile")]
public class ScreenShakeProfile : ScriptableObject
{
	[Header("Impulse Source Settings")]
	public float impactTime = 0.2f;
	public float impactForce = 1f;
	public Vector3 defaultVelocity = new Vector3(0, -1f, 0);
	public AnimationCurve impulseCurve;

	[Header("Impulse Listener Settings")]
	public float listenerAmplitude = 1f;
	public float listenerFrequence = 1f;
	public float listenerDuaration = 1f;
}
