using UnityEngine;
using System.Collections;
using DG.Tweening;

using MuchoBestoStudio.LudumDare.Gameplay;
using MuchoBestoStudio.LudumDare.Gameplay._3C;

public class Resource : MonoBehaviour, IInteractable
{
	public bool AxeDependent { get { return _axeDependent; } }

    [Header("State")]
    [SerializeField]
    private uint _lifePoint = 1;
    public uint LifePoint => _lifePoint;
    [SerializeField]
    private uint _combustibleAmount = 0;
    public uint CombustibleAmount => _combustibleAmount;
    [SerializeField]
    private bool _axeDependent = true;

    [Header("Visual")]
    [SerializeField]
    private GameObject _basicVisual = null;
    [SerializeField]
    private GameObject _emptyVisual = null;

    public System.Action<uint> onCombustibleAmountChanged = null;

    private void Awake()
    {
        if (_basicVisual == null)
        {
            Debug.LogError("No \"_basicVisual\" set for the resource : " + gameObject.name, gameObject);
        }
    }

    public void SetCombustibleAmount(uint value)
    {
        _combustibleAmount = value;
        if (value > 1)
        {
            _basicVisual.SetActive(true);
        }
    }

    public void AddCombustible(uint value) { SetCombustibleAmount(_combustibleAmount + value); }
    public void RemoveCombustible(uint value)
    {
        if (_combustibleAmount - value <= _combustibleAmount)
        {
            SetCombustibleAmount(_combustibleAmount - value);
            onCombustibleAmountChanged?.Invoke(_combustibleAmount);
            if (_combustibleAmount == 0)
            {
                if( _emptyVisual != null)
                {
                    _emptyVisual.SetActive(true);
                    _basicVisual.transform.DORotate(Vector3.forward * 90f + Vector3.up * Random.Range(0f, 360f), 1f);
                    _basicVisual.transform.DOMoveY(-2, 1f).SetDelay(1f).OnComplete(() => _basicVisual.SetActive(false)); ;
                }
                else
                {
                    Destroy(gameObject);
                }
            }
        }
    }

    public void TakeCombustible()
    {
        if (_combustibleAmount > 0)
        {
            Inventory inventory = FindObjectOfType<Inventory>();
            if (inventory && inventory.CanAddCombustible())
            {
                if (_axeDependent)
                {
                    if (inventory.PlayerAxe == null)
                    {
                        return;
                    }
                    inventory.UseAxe();
                }
                --_lifePoint;

                if (_lifePoint == 0)
                {
                    inventory.AddCombustible(_combustibleAmount);
                    RemoveCombustible(_combustibleAmount);
                }
                else if (_basicVisual != null)
                {
                    _basicVisual.transform.DOShakeRotation(.125f, 5, 5, 5);
                }
            }
        }
    }

    public void Interact(ECharacter character)
    {
		if (character == ECharacter.PLAYER)
		{
			TakeCombustible();
		}
    }
}
