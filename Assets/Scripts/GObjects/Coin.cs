using UnityEngine;
using System.Collections;

public class Coin : LevelObject
{
    //public MeshRenderer mRenderer;
    private bool _animate = false;

    void Start()
    {
        t = this.transform;
        if(gameManager == null)
        {
            gameManager = FindGameManager();
        }
    }

    void OnEnable()
    {
        _animate = true;
    }

    void OnDisable()
    {
        _animate = false;
    }

    void Update()
    {
        if(_animate)
            t.rotation = Quaternion.Lerp(t.rotation, (t.rotation * Quaternion.Euler(0, 5.0f, 0)), Time.deltaTime * 100.0f);
    }

    void OnTriggerEnter(Collider col)
    {
        if(gameManager != null)
        {
            gameManager.soundManager.PlaySoundCoin();
            gameManager.OnGetScore(10);
            gameManager.OnGetCoin();
        }
        t.gameObject.SetActive(false);
    }
}
