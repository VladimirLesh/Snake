using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeInput : MonoBehaviour
{
   private Camera _camera;
   [SerializeField] private SnakeHead _head;
   private Vector3 _min, _max;

   private void Start()
   {
      _camera = Camera.main;
      _min = _camera.ViewportToWorldPoint(new Vector3(0, 0, 0));
      _max = _camera.ViewportToWorldPoint(new Vector3(1, 1, 0));
   }

   public Vector2 GetDirectionToClick(Vector2 headPosition)
   {
      Vector3 mousePosition = Input.mousePosition;

      mousePosition = _camera.ScreenToViewportPoint(mousePosition);
      mousePosition.y = 1;
      mousePosition = _camera.ViewportToWorldPoint(mousePosition);

      if (_head.transform.position.x < _min.x && mousePosition.x < _min.x) 
         mousePosition.x = _min.x;
      else if (_head.transform.position.x > _max.x && mousePosition.x > _max.x) 
         mousePosition.x = _max.x;
      
      Vector2 direction = new Vector2(mousePosition.x - headPosition.x, mousePosition.y - headPosition.y);
      
      return direction;
   }
}
