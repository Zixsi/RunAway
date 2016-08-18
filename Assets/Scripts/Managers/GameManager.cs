using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    // База
    public GameBase gameBase;

    // Префаб игрока
    public GameObject playerPrefab;
    // Игрок
    [HideInInspector]
    public GameObject player;
    // Физическое тело игрока
    private Rigidbody playerRig;
    // Скрипт игрока
    private Player playerScript;

    // Камера
    public Camera cam = null;
    private CameraLook cameraLookScript;
    // Контроллер управления
    public InputController inputController;
    // Менеджер уровня
    public LevelManager levelManager;
    // Менеджер звуков
    public GameSoundManager soundManager;
    // Стартовая точка
    public Transform startPoint;
    // Менеджер интерфейса
    public GameUIManager gameUIManager;

    // Статус игры
    public GameStateList gameState = GameStateList.Play;
    public enum GameStateList {Play, Pause, GameOver};

    // Набранные очки
    [HideInInspector]
    public int playerScore = 0;
    // Рекорд
    [HideInInspector]
    public int playerHightScore = 0;
    // Монеты
    [HideInInspector]
    public int playerCoin = 0;
    // Кристалы
    [HideInInspector]
    public int playerGem = 0;

    // Управление (вкл / выкл)
    private bool _enabledControl = false;
    // Блок которым управляем
    private BlockRotator _block;

    // Время игры
    private float _timer = 0;
    // Время начисления очков
    private float _timerScore = 0;

    void Awake()
    {

    }

    void Start()
    {
        if(soundManager != null)
        {
            soundManager.gameManager = this;
        }

        if(levelManager != null)
        {
            levelManager.gameManager = this;
            levelManager.Generate();
        }

        int idCurrentChar = UDSave.Get().currentCharacter;
        if(idCurrentChar > 0)
        {
            for(int i = 0; i < gameBase.characters.Count; i++)
            {
                if(gameBase.characters[i].id == idCurrentChar)
                {
                    playerPrefab = gameBase.characters[i].model;
                    break;
                }
            }
        }

        playerHightScore = UDSave.Get().Hightscore;
        playerCoin = UDSave.Get().Balans;
        playerGem = UDSave.Get().Gems;

        if(playerPrefab != null)
        {
            player = Instantiate(playerPrefab);
            if(player != null)
            {
                player.transform.position = startPoint.position;
                player.transform.rotation = startPoint.rotation;
                playerRig = player.GetComponent<Rigidbody>() as Rigidbody;
                playerScript = player.GetComponent<Player>() as Player;
                playerScript.gameManager = this;

                cameraLookScript = cam.GetComponent<CameraLook>() as CameraLook;
                if (cameraLookScript != null)
                    cameraLookScript.target = player.transform;
            }
        }

        StartCoroutine(GameLoop());

        Messenger<InputController>.AddListener("InputOnTouchStart", OnTouchStart);
        Messenger<InputController>.AddListener("InputOnTouchEnd", OnTouchEnd);
        Messenger<InputController>.AddListener("InputOnTouchMovie", OnTouchMovie);
    }

    void OnApplicationPause(bool status)
    {
        if(status)
        {
            if(gameState == GameStateList.Play)
                GamePause();
        }
    }

    private IEnumerator GameLoop()
    {
        yield return StartCoroutine(GameStart());
        yield return StartCoroutine(GamePlaying());
        yield return StartCoroutine(GameEnd());
    }

    // Начало игры
    private IEnumerator GameStart()
    {
        gameUIManager.UpdateBalansCoin();
        gameUIManager.UpdateBalansGem();
        gameUIManager.UpdateHightScore();

        soundManager.PlayMusic();
        yield return new WaitForSeconds(1.0f);
        SetControl(true);
        playerScript.SetMovie(true);
        yield return null;
    }

    // Игра
    private IEnumerator GamePlaying()
    {
        // Пока игрок жив
        while(!playerScript.die)
        {
            levelManager.CheckBlocks(player.transform.position);

            if(gameState == GameStateList.Play)
            {
                _timer = Time.deltaTime;
                _timerScore = Time.deltaTime;

                // За каждую секунду игры прибавляем очки
                if(_timerScore >= 1.0f)
                {
                    OnGetScore(1);
                    _timerScore = 1.0f - _timerScore;
                }
            }

            yield return null;
        }
    }

    // Конец игры
    private IEnumerator GameEnd()
    {
        GameOver();
        yield return null;
    }

    // Перезагрузка уровня
    public void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Проигрыш
    public void GameOver()
    {
        gameState = GameStateList.GameOver;
        SetControl(false);
        playerScript.SetMovie(false);
        soundManager.PauseMusic();

        if(playerScore > playerHightScore)
        {
            playerHightScore = playerScore;
            UDSave.Get().Hightscore = playerScore;
        }

        /*int _coinFromScore = 0;
        _coinFromScore = Mathf.RoundToInt(playerScore / 50);
        if(_coinFromScore > 0)
        {
            playerCoin += _coinFromScore;
        }
           
        if(playerCoin > 0)
        {
            UDSave.Get().Balans = UDSave.Get().Balans + playerCoin;
        }*/

        UDSave.Save();
        gameUIManager.gameOverMenu.Run();
    }

    // Пауза
    public void GamePause()
    {
        gameState = GameStateList.Pause;
        SetControl(false);
        playerScript.SetMovie(false);
        soundManager.PauseMusic();
        //gameUIManager.PauseMenu(true);
    }

    // Продолжить игру
    public void GameResume()
    {
        gameState = GameStateList.Play;
        //gameUIManager.PauseMenu(false);
        soundManager.PlayMusic();
        SetControl(true);
        playerScript.SetMovie(true);
    }

    // Главный экран
    public void GameHome()
    {
        SceneManager.LoadScene("Main");
    }


    // Установить управление
    public void SetControl(bool val = false)
    {
        _enabledControl = val;

        if(!_enabledControl)
        {
            if(_block != null)
                _block.rotateListen = false;
        }
    }

    public void OnTouchStart(InputController input)
    {
        if(_enabledControl && _block == null)
        {
            OnTouchMovie(input);
        }
    }

    public void OnTouchEnd(InputController input)
    {
        if(_block != null)
        {
            _block.RotateEnd();
            _block.inputController = null;
            _block.rotateListen = false;
            _block = null;
        }
    }

    public void OnTouchMovie(InputController input)
    {
        if(_enabledControl)
        {
            if(_block == null && cam != null)
            {
                Ray _Ray = cam.ScreenPointToRay(Input.mousePosition);
                RaycastHit _RayHit;
                if(Physics.Raycast(_Ray, out _RayHit))
                {
                    if(_RayHit.transform.parent != null)
                    {
                        _block = _RayHit.transform.parent.GetComponent<BlockRotator>() as BlockRotator;
                        if(_block != null)
                        {
                            _block.inputController = input;
                            _block.rotateListen = true;
                        }
                    }

                }
            }
        }
    }

    // При получении очков
    public void OnGetScore(int val)
    {
        if(val > 0)
        {
            playerScore += val;
            gameUIManager.UpdateScore();
        }
    }

    public void OnGetCoin()
    {
        
    }

}
