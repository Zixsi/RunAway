using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class CharactersSlider : MonoBehaviour
{
    public MainManager gameManager;
    private Transform t;

    // Персонажи
    public List<Character> characters;
    // Расстояние между персонажами
    public float distance = 1.0f;
    // Индекс текущего персонажа
    [HideInInspector]
    public int index = 0;
    // Анимируем
    private bool _animate = false;
    // Следующая позиция
    private float _nextPositonX = 0;

    void Start()
    {
        t = transform;
        if(gameManager.gameBase.characters != null)
        {
            characters = gameManager.gameBase.characters;
        }
            

        Messenger<InputController>.AddListener("InputOnTouchEnd", OnTouchEnd);

        if(characters.Count > 0)
        {
            Init();
        }
    }

    private void OnTouchEnd(InputController input)
    {
        if(input.TouchLength() > 50.0f)
        {
            if(input.TouchVector().x > 0)
            {
                Prev();
            }
            else
            {
                Next();
            }
        }
    }

    // Инициализация слайдера
    private void Init()
    {
        for(int i = 0; i < characters.Count; i++)
        {
            if(characters[i].id == UDSave.Get().currentCharacter)
                index = i;

            GameObject character = Instantiate(characters[i].model);
            character.transform.parent = t;
            character.transform.localPosition = new Vector3(i * distance, 0, 0);
            character.transform.localRotation = Quaternion.Euler(0, 35.0f, 0);
        }

        _nextPositonX = index * -distance;
        if(index != 0)
            _animate = true;

        SetButtonPrice();
    }

    // Следующий персонаж
    public void Next()
    {
        if(!_animate)
        {
            index++;
            if(index < characters.Count)
                _nextPositonX = index * -distance;
            else
                index = characters.Count - 1;

            SetButtonPrice();
            _animate = true;
        }
    }

    // Предыдущий персонаж
    public void Prev()
    {
        if(!_animate)
        {
            index--;
            if(index >= 0)
                _nextPositonX = index * -distance;
            else
                index = 0;

            SetButtonPrice();
            _animate = true;
        }
    }

    // Установить цену на кнопке
    private void SetButtonPrice()
    {
        if(gameManager.managerGUI.bayButton != null)
        {
            
            if(!UDSave.Get().payedCharacters.Contains(characters[index].id) && characters[index].price > 0)
            {
                StartCoroutine(ShowButton());
                Button _button = gameManager.managerGUI.bayButton;
                if(gameManager.managerGUI.bayButtonText != null)
                {
                    gameManager.managerGUI.bayButtonText.text = "" + characters[index].price;
                }

                if(UDSave.Get().Balans < characters[index].price && _button.IsInteractable())
                {
                    _button.interactable = false;
                }
                else if(UDSave.Get().Balans >= characters[index].price && !_button.IsInteractable())
                {
                    _button.interactable = true;
                }

                gameManager.managerGUI.lockImg.SetActive(true);
                gameManager.managerGUI.playBtn.interactable = false;
            }
            else
            {
                UDSave.Get().currentCharacter = characters[index].id;

                gameManager.managerGUI.lockImg.SetActive(false);
                gameManager.managerGUI.playBtn.interactable = true;
                StartCoroutine(HideButton());
            }
        }
    }

    // Показать кнопку
    IEnumerator ShowButton()
    {
        if(gameManager.managerGUI.bayButton != null)
        {
            CanvasRenderer[] canvasRenderers = gameManager.managerGUI.bayButton.GetComponentsInChildren<CanvasRenderer>();
            foreach(CanvasRenderer cr in canvasRenderers)
            {
                cr.SetAlpha(1.0f);
            }
            /*float _alpha = 0;
            while(_alpha < 1.0f)
            {
                foreach(CanvasRenderer cr in canvasRenderers)
                {
                    cr.SetAlpha(_alpha);

                }
                _alpha = Mathf.MoveTowards(_alpha, 1.0f, Time.deltaTime * 3.0f);
                yield return null;
            }*/
        }
        yield return null;
    }

    // Скрыть кнопку
    IEnumerator HideButton()
    {
        if(gameManager.managerGUI.bayButton != null)
        {
            CanvasRenderer[] canvasRenderers = gameManager.managerGUI.bayButton.GetComponentsInChildren<CanvasRenderer>();
            foreach(CanvasRenderer cr in canvasRenderers)
            {
                cr.SetAlpha(0.0f);
            }
        }
        yield return null;
    }

    void Update()
    {
        // Анимирование слайда
        if(_animate)
        {
            float _distance = t.position.x - _nextPositonX;
            if(_distance > -0.2f && _distance < 0.2f)
            {
                t.position = new Vector3(_nextPositonX, t.position.y, t.position.z);
                _animate = false;
            }
            else
            {
                t.position = new Vector3(Mathf.MoveTowards(t.position.x, _nextPositonX, Time.deltaTime * 20.0f), t.position.y, t.position.z);
            }
        }
    }

    // Покупка персонажа
    public void BayCharacter()
    {
        Transaction _transaction = new Transaction();
        _transaction.id = characters[index].id;
        _transaction.price = characters[index].price;
        _transaction.type = Transaction.TypeObect.Characters;

        gameManager.managerGUI.buyWindowUI.transaction = _transaction;
        gameManager.managerGUI.buyWindowUI.target = Accept;
        gameManager.managerGUI.buyWindowUI.Show();
    }

    // Подтверждение покупки
    public void Accept(Transaction transaction)
    {
        if(transaction.type == Transaction.TypeObect.Characters)
        {
            if(UDSave.Get().Balans >= transaction.price)
            {
                UDSave.Get().Balans = (UDSave.Get().Balans - transaction.price);
                UDSave.Get().payedCharacters.Add(transaction.id);
                UDSave.Save();
            }

            gameManager.managerGUI.UpdateBalans();
            SetButtonPrice();
        }
    }

}
