using UnityEngine;
using System.Collections;

public class Utils
{
    // Добавить стартовые нули
    public static string AddStartedNull(string s = "", int lenght = 3)
    {
        return (s.Length < lenght) ? Utils.CopyString("0", lenght - s.Length) + s : s;
    }

    // Создает строку из указанного кол-ва передаваемой строки
    public static string CopyString(string s = "", int count = 1)
    {
        string result = "";
        for(int i = 0; i < count; i++)
        {
            result += s;
        }

        return result;
    }
}
