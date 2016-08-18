using UnityEngine;
using System.Collections;

[System.Serializable]
public class Character
{
    // Идентификатор
    public int id;
    // Название персонажа
    public string name;
    // Модель персонажа
    public GameObject model;
    // Стоимость
    public int price = 0;
}
