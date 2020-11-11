using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CylinderDefault : MonoBehaviour , ICylinder
{
    private int magazineSize = 6;
    
     public int GetWeaponMagazineSize()
     {
         return magazineSize;
     }
}
