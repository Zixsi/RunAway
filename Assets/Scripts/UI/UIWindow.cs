using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIWindow : MonoBehaviour
{
    public EasyTween tween;

    // Показать окно
    virtual public void Show()
    {
        this.gameObject.SetActive(true);
    }

    // Скрыть окно
    virtual public void Hide()
    {
        this.gameObject.SetActive(false);
    }
}
