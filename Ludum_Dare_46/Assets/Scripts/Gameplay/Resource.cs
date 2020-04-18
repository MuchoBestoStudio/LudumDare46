using UnityEngine;
using System;

using MuchoBestoStudio.LudumDare.Gameplay;
using MuchoBestoStudio.LudumDare.Gameplay._3C;

public class Resource : MonoBehaviour, IInteractable
{
    [SerializeField]
    private uint _combustibleAmount = 0;
    public uint CombustibleAmount => _combustibleAmount;

    public Action<uint> onCombustibleAmountChanged = null;

    public void SetCombustibleAmount(uint value)
    {
        _combustibleAmount = value;
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
                Destroy(gameObject);
            }
        }
    }

    public void TakeCombustible()
    {
        if (_combustibleAmount > 0)
        {
            RemoveCombustible(1);
            FindObjectOfType<Inventory>().AddCombustible(1);
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
