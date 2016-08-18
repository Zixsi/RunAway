using UnityEngine;
using System.Collections;

[System.Serializable]
public class Transaction
{
    public enum TypeObect {Characters}

    public int id;
    public int price;
    public TypeObect type;
}
