using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackingTransition : Transition
{
    [SerializeField] GameObject _unitArmy;
    private bool _isCheck;
    public bool IsCheck
    {
        get => _isCheck; set => _isCheck = value;
    }
    private void Update()
    {
        if(_isCheck = false)
        {
            NeedSwitch = true;
        }
        IsChekTarget();
    }

    private void IsChekTarget()
    {
        _isCheck = true;
    }

}
