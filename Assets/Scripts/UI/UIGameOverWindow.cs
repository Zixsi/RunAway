using UnityEngine;
using System.Collections;

[AddComponentMenu("Game/UI/GameOverWindow")]
public class UIGameOverWindow : UIWindow
{
    public void Run()
    {
        tween.OpenCloseObjectAnimation();
    }
}
