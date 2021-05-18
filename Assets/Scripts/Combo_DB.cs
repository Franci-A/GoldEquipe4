using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ComboData", menuName = "ComboMenu")]
public class Combo_DB : ScriptableObject
{
    public enum COMBO_TYPE
    {
        NONE,
        TRIPLE_LVL2,
        TRIPLE_LVL3,
        QUAD_LVL1,
        QUAD_LVL2,
        QUAD_LVL3,
        QUINT_LVL1,
        QUINT_LVL2,
        QUINT_LVL3,
    }

    [System.Serializable]
    public struct ComboConfig
    {
        public COMBO_TYPE _typeCombo;

        [Range(10, 100)]
        public int _value;
    }

    public List<ComboConfig> _comboConfig = new List<ComboConfig>();


}
