using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chamber : MonoBehaviour
{
   private GameObject bullet;

   public GameObject Bullet
   {
      get => bullet;
      set => bullet = value;
   }
}
