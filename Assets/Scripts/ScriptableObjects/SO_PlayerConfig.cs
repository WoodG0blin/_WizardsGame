using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Wizards;

[CreateAssetMenu(fileName = "PlayerConfig", menuName = "ScriptableObjects/PlayerConfig", order = 1)]

public class SO_PlayerConfig : ScriptableObject
{
    [SerializeField] private GameObject _player;
    [SerializeField] private float _speed;
    [SerializeField] private float _acceleration;
    [SerializeField] private float _hp;
    [SerializeField] private float _damage;
    [SerializeField] private float _force;

    public GameObject Player { get => _player; }
    public float Speed { get => _speed; }
    public float Acceleration { get => _acceleration; }
    public float HP { get => _hp; }
    public float Damage { get => _damage; }
    public float Force { get => _force; }

}
