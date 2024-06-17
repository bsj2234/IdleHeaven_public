using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseViewModel : MonoBehaviour
{
    protected void RaisePropertyChanged(string propertyName)
    {
        OnPropertyChanged?.Invoke(propertyName);
    }

    public event Action<string> OnPropertyChanged;
}
