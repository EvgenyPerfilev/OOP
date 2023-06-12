using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Работа со значениями и памятью

[CreateAssetMenu]
public class FloatValue : ScriptableObject, ISerializationCallbackReceiver {
    public float initialValue;

    [HideInInspector] //чтобы не отображалось в инспекторе Unity
    public float RuntimeValue; //некое "кэширование"

    public void OnAfterDeserialize() {
        RuntimeValue = initialValue; // чтобы в инспекторе каждый раз здоровье брало начальное значение, а не записывала новое

    }

    public void OnBeforeSerialize() { }

}
