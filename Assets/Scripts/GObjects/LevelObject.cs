using UnityEngine;

abstract public class LevelObject : MonoBehaviour, ILevelObject
{
    public GameManager gameManager;
    public Transform t;

    public GameManager FindGameManager()
    {
        return GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }
}
