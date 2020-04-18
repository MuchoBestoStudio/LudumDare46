using UnityEngine;

using MuchoBestoStudio.LudumDare.Gameplay;
using MuchoBestoStudio.LudumDare.Gameplay._3C;

public class Resource : MonoBehaviour
{
    [SerializeField]
    private uint _combustibleAmount = 0;
    public uint CombustibleAmount => _combustibleAmount;

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

    private void OnTriggerEnter(Collider other)
    {
        PlayerController pc = FindObjectOfType<PlayerController>();
        if (pc)
        {
            pc.onInteractPerformed -= TakeCombustible;
            pc.onInteractPerformed += TakeCombustible;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        FindObjectOfType<PlayerController>().onInteractPerformed -= TakeCombustible;
    }
}
