using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;

namespace Wizards
{
    public class GameMenu
    {
        GameMenuView _view;
        public GameMenu(GameMenuView view)
        {
            _view = view;
        }

        public void SetHealthValue(float value)
        {
            _view.Health.text = $"{(int)value}";
        }

        public void SetScoreValue(int value) { _view.Score.text = $"{value}"; }
        public void SetMessage(string value) { _view.Message.text = value; }
        public void GameOver() { _view.GameOver.SetActive(true); }
    }
}
