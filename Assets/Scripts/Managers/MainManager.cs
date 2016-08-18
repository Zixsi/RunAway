using UnityEngine;
using System.Collections;

public class MainManager : MonoBehaviour
{
    // Менеджер интерфейса
    public MainGUIManager managerGUI;
    // Контроллер ввода
    public InputController inputController;
    // База
    public GameBase gameBase;

    void Start()
    {
        //UDSave.Reset();
        //UDSave.Get().Balans = 300;
        //UDSave.Save();
        managerGUI.UpdateBalans();
    }

    // Старт игры
    public void StartGame()
    {
        if(managerGUI.loaderWindowUI != null)
        {
            managerGUI.loaderWindowUI.sceneName = "Level";
            managerGUI.loaderWindowUI.Show();
        } 
    }

    // Окно с предупреждением об отсутствии функционала
    public void EmptyWindow(bool state = false)
    {
        if(managerGUI.emptyWindowUI != null)
        {
            if(state)
                managerGUI.emptyWindowUI.Show();
            else
                managerGUI.emptyWindowUI.Hide();
        }
    }


}
