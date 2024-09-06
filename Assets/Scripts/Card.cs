using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public int CardIndex;
    public Sprite Face; 

    [SerializeField] private GameBehaviour _gameScript;   
    [SerializeField] private Sprite _cardBack;   
    [SerializeField] private UnityEvent _foundEvent;

    private Image _cardImage;

    [System.Obsolete]

    private void Awake()
    {
        _cardImage = gameObject.GetComponent<Image>();
    }

    private void Start()
    {
        Return();
        if (_gameScript.Sprites.Count > 0)
        {
            int randomNumber = Random.RandomRange(0, _gameScript.Indexes.Count);
            int randomImage = Random.RandomRange(0, _gameScript.Sprites.Count);
            CardIndex = _gameScript.Indexes[randomNumber];
            _gameScript.Indexes.Remove(_gameScript.Indexes[randomNumber]);
            Face = _gameScript.Sprites[randomImage];
            _gameScript.Sprites.Remove(Face);
            _gameScript.OriginalCards.Add(this);
        }
        else
        {
            int randomCard = Random.RandomRange(0, _gameScript.OriginalCards.Count);
            CardIndex = _gameScript.OriginalCards[randomCard].CardIndex;
            Face = _gameScript.OriginalCards[randomCard].Face;
            _gameScript.OriginalCards.Remove(_gameScript.OriginalCards[randomCard]);
        }

        _gameScript.AllReadyCards.Add(this);
    }

    public void Flip()
    {
        if (!_gameScript.IsActive) return;
        _cardImage.sprite = Face;
        _gameScript.Versus(this.gameObject);
    }

    public void End()
    {
        _cardImage.sprite = Face;
        gameObject.GetComponent<Button>().interactable = false;
        _gameScript.AllReadyCards.Remove(this);
    }

    public void Found()
    {
        _foundEvent?.Invoke();
    }

    public void Return()
    {
        _cardImage.sprite = _cardBack;
    }

}
