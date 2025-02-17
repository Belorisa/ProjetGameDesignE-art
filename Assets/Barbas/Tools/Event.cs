using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event : MonoBehaviour
{
   public static Event current;

   public void Awake()
   {
      current = this;
   }

   public event Action onLevelUp;

   public void LevelUp()
   {
      if (onLevelUp != null)
      {
         onLevelUp();
      }
   }
}
