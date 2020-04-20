using UnityEngine;

namespace MuchoBestoStudio.LudumDare.Gameplay.Fire
{
	public class FireSourceVFX : MonoBehaviour
	{
		[System.Serializable]
		private class FlameParticule
		{
			public ParticleSystem _particuleSystem = null;
			public Vector2 _minMax = Vector2.up;
		}

		[System.Serializable]
		private class FlameLight
		{
			public Light _light = null;
			public Vector2 _minMax = Vector2.up;
			public Color _MinColor = Color.black;
			public Color _MaxColor = Color.white;
		}

		#region Variables

		[Header("Globals")]
		[SerializeField, Tooltip("")]
		private FireSource _source = null;
		[SerializeField, Tooltip("")]
		private float _maxCombustible = 5f;

		[Header("Particles")]
		[SerializeField]
		private FlameParticule[] _particuleFX = null;

		[Header("Light")]
		[SerializeField]
		private FlameLight[] _fireLights = null;

		#endregion

		#region MonoBehaviour's Methods

		private void OnEnable()
		{
			_source.onCombustibleAmountChanged += FireSource_OnCombustibleAmountChanged;
		}

		private void OnDisable()
		{
			_source.onCombustibleAmountChanged -= FireSource_OnCombustibleAmountChanged;
		}

		private void FireSource_OnCombustibleAmountChanged(uint amount, int _)
		{
			float ratio = (float)amount / _maxCombustible;

			foreach (FlameParticule flameParticule in _particuleFX)
			{
				SetFlameRateOverTime(flameParticule, ratio * flameParticule._minMax.y);
			}

			foreach (FlameLight flameLight in _fireLights)
			{
				SetLightIntensity(flameLight, ratio);
			}
		}

		#endregion

		#region VFX

		private void SetFlameRateOverTime(FlameParticule flameSystem, float rate)
		{
			var emission = flameSystem._particuleSystem.emission;
			emission.rateOverTime = new ParticleSystem.MinMaxCurve(Mathf.Clamp(rate, flameSystem._minMax.x, flameSystem._minMax.y));
		}

		private void SetLightIntensity(FlameLight flameLight, float ratio)
		{
			flameLight._light.intensity = Mathf.Lerp(flameLight._minMax.x, flameLight._minMax.y, ratio);
			flameLight._light.color = Color.Lerp (flameLight._MinColor,flameLight._MaxColor, ratio);
		}
		#endregion
	}
}
