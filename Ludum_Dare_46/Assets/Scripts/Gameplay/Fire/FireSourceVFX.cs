using UnityEngine;

namespace MuchoBestoStudio.LudumDare.Gameplay.Fire
{
	public class FireSourceVFX : MonoBehaviour
	{
		#region Variables

		[Header("Globals")]
		[SerializeField, Tooltip("")]
		private	FireSource	_source = null;

		[Header("Particles")]
		[SerializeField, Tooltip("")]
		private	ParticleSystem	_flame = null;
		[SerializeField, Tooltip("")]
		private	float	_maxRateOverTime = 40f;
		[SerializeField, Tooltip("")]
		private	float	_maxCombustible = 5f;	

		private	ParticleSystem.EmissionModule	_flameEmission = new ParticleSystem.EmissionModule();

		#endregion

		#region MonoBehaviour's Methods

		private void Awake()
		{
			_flameEmission = _flame.emission;
		}

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
			SetFlameRateOverTime((amount / _maxCombustible) * _maxRateOverTime);
		}

		#endregion

		#region VFX

		public void SetFlameRateOverTime(float rate)
		{
			_flameEmission.rateOverTime = new ParticleSystem.MinMaxCurve(Mathf.Clamp(rate, 0f, _maxRateOverTime));
		}

		#endregion
	}
}
