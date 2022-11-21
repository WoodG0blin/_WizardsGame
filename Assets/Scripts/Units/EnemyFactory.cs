using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wizards
{
    internal class EnemyFactory
    {
        private SO_EnemiesConfig _settings;
        private WeaponFactory _weaponFactory;
        public enum EnemyType { Random = 0, Still = 1, Melee = 2, Ranged = 3 , Clonable = 4}

        internal EnemyFactory(SO_EnemiesConfig settings, WeaponFactory weaponFactory)
        {
            _settings = settings;
            _weaponFactory = weaponFactory;
        }
        internal EnemyFactory(SO_EnemiesConfig settings) : this(settings, Services.GetService<WeaponFactory>()) { }


        internal IEnemy CreateEnemy(EnemyType type)
        {
            if(type == EnemyType.Random) type = (EnemyType)Random.Range(1, 5);

            IEnemy temp;

            switch (type)
            {
                case EnemyType.Still: temp = new StillEnemy(GameObject.Instantiate(_settings.StillEnemyPrefab).GetComponent<View>(), _weaponFactory.CallForWeapon(Weapon.WeaponType.sword), 1); break;
                case EnemyType.Melee: temp =  new MeleeEnemy(GameObject.Instantiate(_settings.MeleeEnemyPrefab).GetComponent<View>(), _weaponFactory.CallForWeapon(Weapon.WeaponType.sword), 1); break;
                case EnemyType.Ranged: temp = new RangedEnemy(GameObject.Instantiate(_settings.RangedEnemyPrefab).GetComponent<View>(), _weaponFactory.CallForWeapon(Weapon.WeaponType.wand), 1); break;
                case EnemyType.Clonable: temp = new ClonableEnemy(GameObject.Instantiate(_settings.ClonableEnemyPrefab).GetComponent<View>(), _weaponFactory.CallForWeapon(Weapon.WeaponType.sword), 1, true); break;
                default: temp = new StillEnemy(GameObject.Instantiate(_settings.StillEnemyPrefab).GetComponent<View>(), _weaponFactory.CallForWeapon(Weapon.WeaponType.sword), 1); break;
            }

            temp.Position = SetRandomPosition();
            return temp;
        }

        private Vector2 SetRandomPosition()
        {
            return new Vector2(Random.Range(CoordinatesModel.Field.xMin, CoordinatesModel.Field.xMax), Random.Range(CoordinatesModel.Field.yMin, CoordinatesModel.Field.yMax));
        }
    }
}
