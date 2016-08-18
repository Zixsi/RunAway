using UnityEngine;
using System.Collections;

public class UIBuyWindow : UIWindow
{
    [HideInInspector]
    public Transaction transaction;
    public Callback<Transaction> target;

    public override void Show()
    {
        base.Show();
    }

    public void Accept()
    {
        target(transaction);
        base.Hide();
        transaction = null;
    }
}
