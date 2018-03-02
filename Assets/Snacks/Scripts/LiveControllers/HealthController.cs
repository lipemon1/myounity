using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour {

    [Header("Cur Status")]
    [SerializeField] private int _curHealth;

	public int GetCurHealth() { return _curHealth; }

    public void SetCurHealth(int amountToAdd)
    {
        _curHealth += amountToAdd;

        if (_curHealth < 0)
            _curHealth = 0;

        OnHealthChange();
    }

    private void OnHealthChange()
    {
        //TODO implement call to the UI
    }
}
