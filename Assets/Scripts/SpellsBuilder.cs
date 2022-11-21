using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions.Must;

namespace Wizards
{
    public class SpellsBuilder
    {
        public enum ColliderType { circle, box, custom}

        protected GameObject gameObject;

        public SpellsBuilder(Sprite sprite)
        {
            gameObject = new GameObject();
            gameObject.AddComponent<SpriteRenderer>().sprite = sprite;
        }


        public SpellsBuilder SetName(string name)
        {
            gameObject.name = name;
            return this;
        }

        public SpellsBuilder SetCollider2D(ColliderType type, float size = 1.0f)
        {
            if(!gameObject.GetComponent<Collider2D>())
            switch(type)
            {
                case ColliderType.circle: var component = gameObject.AddComponent<CircleCollider2D>(); component.radius = size; break;
                case ColliderType.box: gameObject.AddComponent<BoxCollider2D>(); break;
                default: gameObject.AddComponent<PolygonCollider2D>(); break;
            }
            
            return this;
        }

        public SpellsBuilder SetRigidbody2D(float mass)
        {
            Rigidbody2D component;
            if (!gameObject.TryGetComponent<Rigidbody2D>(out component)) component = gameObject.AddComponent<Rigidbody2D>();
            component.mass = mass;
            component.gravityScale = 0f;
            return this;
        }

        public SpellsBuilder SetSprite(Sprite sprite)
        {
            SpriteRenderer component;
            if(!gameObject.TryGetComponent<SpriteRenderer>(out component)) component = gameObject.AddComponent<SpriteRenderer>();
            component.sprite = sprite;
            return this;
        }

        public SpellsBuilder SetColor(Color color)
        {
            SpriteRenderer component;
            if(!gameObject.TryGetComponent<SpriteRenderer>(out component)) component = gameObject.AddComponent<SpriteRenderer>();
            component.color = color;
            return this;
        }

        public SpellsBuilder SetScale(float scale)
        {
            gameObject.transform.localScale = Vector3.one * scale;
            return this;
        }

        public SpellsBuilder SetToDefaultPosition()
        {
            return this;
        }

        public static implicit operator GameObject(SpellsBuilder builder) { return builder.gameObject; }
    }
}
