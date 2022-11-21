using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wizards
{
    public static class CoordinatesModel
    {
        private static Rect field;
        public static Rect Field { get => field; }

        //public CoordinatesModel(Camera camera)
        //{
        //    Vector3 maxCoordinates = camera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        //    Vector3 minCoordinates = camera.ScreenToWorldPoint(new Vector3(0, 0, 0));
        //    field = new Rect(minCoordinates.x,minCoordinates.y,maxCoordinates.x,maxCoordinates.y);
        //}

        static CoordinatesModel()
        {
            Vector3 maxCoordinates = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
            Vector3 minCoordinates = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
            field = new Rect(minCoordinates.x, minCoordinates.y, maxCoordinates.x - minCoordinates.x, maxCoordinates.y - minCoordinates.y);
        }

        public static Vector2 GetVector2(Vector3 v3)
        {
            return new Vector2(v3.x, v3.y);
        }

        public static Vector2 ClampByField(Vector2 targetPosition)
        {
            return new Vector2(Mathf.Clamp(targetPosition.x, Field.xMin, Field.xMax), Mathf.Clamp(targetPosition.y, Field.yMin, Field.yMax));
        }
    }
}
