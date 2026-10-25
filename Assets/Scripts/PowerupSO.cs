using System;
using UnityEngine;

[CreateAssetMenu(fileName = "PowerUp", menuName = "PowerupSO")]
public class PowerupSO : ScriptableObject
{
    [SerializeField] public string PowerupType;
    [SerializeField] public float valueChange;
    [SerializeField] public float time;

    public string GetPowerupType()
    {
        return PowerupType;
    }
    public float GetvalueChange()
    {
        return valueChange;
    }
    public float Gettime()
    {
        return time;
    }

}
