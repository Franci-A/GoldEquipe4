using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BonusType
{
    Chest,
    Hammer1,
    Hammer2,
    Hammer3,
    Thunder,
    Mountain,
    Shield1,
    Shield2
}
public class BonusTile : MonoBehaviour
{
    public BonusType bonusType;
}
