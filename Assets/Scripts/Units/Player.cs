using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;

namespace Wizards
{
    internal sealed class Player : ITarget
    {
        private View _player;
        private IWeapon _leftWand;
        private SO_PlayerConfig _playerConfig;
        private Stats _playerStats;
        private Clock _clock;
        private bool _canDodge;
        private bool _accelerating;

        public Player(SO_PlayerConfig config, IWeaponPrototype wandPrototype)
        {
            _player = GameObject.Instantiate(config.Player).GetComponent<View>();
            _playerConfig = config;
            _player.OnCollision += (collider) => { };
            _player.OnCollision += OnCollision;

            _leftWand = wandPrototype.SetWeapon(_player.transform.Find("Weapon") ?? _player.transform);

            _playerStats = new Stats(_playerConfig.HP);
            _playerStats.OnDeath += Die;

            _clock = Services.GetService<Clock>();
            _canDodge = true;
        }

        public IWeapon Weapon { get => _leftWand; set => _leftWand = value; }

        public event Action OnDeath;
        public event Action<float> OnHealthChange;

        public Vector2 Position { get => new Vector2(_player.transform.position.x, _player.transform.position.y); set { } }
        public float Health { get => _playerStats.Health; }

        public void DealDamage(float damage) { _playerStats.ReceiveDamage(damage); OnHealthChange?.Invoke(_playerStats.Health); }

        public void SetInputs(InputController.Inputs inputs)
        {
            ChangeLookDirection(inputs.Rotation);
            MoveForward(inputs.Forward);
            if (inputs.Dodge != 0) Dodge(inputs.Dodge);
            if(inputs.Accelerate) _accelerating = true;
            if(inputs.Decelerate) _accelerating = false;
            if (inputs.Fire) Fire();
        }

        private void ChangeLookDirection(float delta) { _player.Rotate(delta); }

        private void MoveForward(float distance)
        {
            _player.Position += _player.Direction * distance * _playerConfig.Speed * (_accelerating ? _playerConfig.Acceleration : 1) * Time.deltaTime;
        }

        private void Dodge(float direction)
        {
            if (_canDodge)
            {
                _player.Position += Vector2.Perpendicular(_player.Direction) * (direction > 0 ? -1 : 1);
                _canDodge = false;
                _clock.SetTimer(2, () => { _canDodge = true; });
            }
        }
        private void Fire() { if(_leftWand.Ready) _leftWand.Attack(null); }


        private void Die() { OnDeath?.Invoke(); }
        private void OnCollision(Collider2D collider) { if (collider.transform.GetComponent<View>() is Bullet b) DealDamage(b.Damage); }
    }
}
