using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Wizards
{
    public class GameMenuView : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI _health;
        [SerializeField] TextMeshProUGUI _score;
        [SerializeField] TextMeshProUGUI _message;
        [SerializeField] GameObject _gameOver;

        public TextMeshProUGUI Health { get => _health; }
        public TextMeshProUGUI Score { get => _score; }
        public TextMeshProUGUI Message { get => _message; }
        public GameObject GameOver { get => _gameOver; }

        void Start()
        {
            if(_health == null) _health = transform.Find("Health").Find("Value").GetComponent<TextMeshProUGUI>();
            if(_score == null) _score = transform.Find("Score").Find("Value").GetComponent<TextMeshProUGUI>();
            if(_message == null) _message = transform.Find("Message").Find("Value").GetComponent<TextMeshProUGUI>();
            if (_gameOver == null) _gameOver = transform.Find("GameOver").gameObject;
        }
    }
}
