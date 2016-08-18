using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameUIManager : MonoBehaviour
{
    public GameManager gameManager;

    //=== Графические элементы экрана ===//

    // Счетчик отсчета
    public GameObject startCounterUI;
    // Кнопка вызова меню паузы
    public GameObject pauseButtonUI;
    // Счет
    public Text scoreUIText;
    // Рекорд
    public Text hightscoreUIText;
    // Баланс - монеты
    public Text balansCoinText;
    // Баланс - кристалы
    public Text balansGemText;

    //=== End Графические элементы экрана ===//
    public Text gameoverScoreUIText;
    public Text gameoverHightScoreUIText;
    public Text gameoverCoinUIText;

    public UIGameOverWindow gameOverMenu;

    public UISpecialEventWindow specialEventWindow;

    void Start()
    {

    }

    /*
    // Меню паузы
    public void PauseMenu(bool show = true)
    {
        pauseButtonUI.SetActive(!show);
        menuPause.SetActive(show);
    }

    // Меню проигрыша
    public void GameOverMenu()
    {
        menuPause.SetActive(false);
        menuGameover.SetActive(true);

        if(gameoverHightScoreUIText != null)
            gameoverHightScoreUIText.text = gameManager.playerHightScore.ToString();

        if(gameoverScoreUIText != null)
            gameoverScoreUIText.text = gameManager.playerScore.ToString();

        if(gameoverCoinUIText != null)
            gameoverCoinUIText.text = "0";

        //StartCoroutine(Score2Coin());
    }
    */
    /*private IEnumerator Score2Coin()
    {
        int _coin = 0;
        yield return new WaitForSeconds(1.0f);

        while(gameManager.playerScore > 0)
        {
            if(gameManager.playerScore >= 50)
            {
                gameManager.playerScore -= 50;
                _coin += 1;
                gameManager.soundManager.PlaySoundCoin();
            }
            else
            {
                gameManager.playerScore = 0;
            }

            if(gameoverScoreUIText != null)
                gameoverScoreUIText.text = gameManager.playerScore.ToString();

            if(gameoverCoinUIText != null)
                gameoverCoinUIText.text = _coin.ToString();

            yield return new WaitForSeconds(0.3f);
        }
        yield return null;
    }*/

    // Задать значение для очков
    public void UpdateScore()
    {
        if(scoreUIText != null)
            scoreUIText.text = gameManager.playerScore.ToString();
    }

    // Задать значение для рекорда
    public void UpdateHightScore()
    {
        if(hightscoreUIText != null)
            hightscoreUIText.text = gameManager.playerHightScore.ToString();
    }

    // Задать значение для кол-ва монет на балансе
    public void UpdateBalansCoin()
    {
        if(balansCoinText != null)
            balansCoinText.text = Utils.AddStartedNull(gameManager.playerCoin.ToString(), 4);
    }

    // Задать значение для кол-ва кристалов на балансе
    public void UpdateBalansGem()
    {
        if(balansGemText != null)
            balansGemText.text = Utils.AddStartedNull(gameManager.playerGem.ToString(), 2);
    }
}
