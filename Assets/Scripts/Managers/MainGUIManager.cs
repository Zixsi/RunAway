using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainGUIManager : MonoBehaviour
{
    public MainManager gameManager;

    // Загрузчик
    public UILoaderWindow loaderWindowUI;
    // Пустое окно
    public UISimpleWindow emptyWindowUI;
    // Окно подтверждения покупки
    public UIBuyWindow buyWindowUI;

    // Баланс игрока
    public Text balansUI;
    // Кнопка старта
    public Button playBtn;

    // Кнопка купить
    public Button bayButton;
    // Кнопка купить (текст)
    public Text bayButtonText;
    // Значек блокировки
    public GameObject lockImg;

    // Обновить баланс на экране
    public void UpdateBalans()
    {
        if(balansUI != null)
        {
            balansUI.text = Utils.AddStartedNull(UDSave.Get().Balans.ToString(), 4);
        }
    }
}
