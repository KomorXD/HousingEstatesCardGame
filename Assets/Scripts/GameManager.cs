using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject _cardPrefab;
    [SerializeField] private List<GameObject> _playerCards;

    private GameObject _env;
    
    void Start()
    {
        _env = GameObject.FindGameObjectWithTag("EnvTag");

        _playerCards = new List<GameObject>();
        _playerCards.Add(Instantiate(_cardPrefab, _env.transform));
        _playerCards.Add(Instantiate(_cardPrefab, _env.transform));
        _playerCards.Add(Instantiate(_cardPrefab, _env.transform));
        _playerCards.Add(Instantiate(_cardPrefab, _env.transform));
    }
    
    void Update()
    {
        
    }
}
