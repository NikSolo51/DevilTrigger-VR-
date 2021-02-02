using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    [CreateAssetMenu(fileName = "BulletData", menuName = "ScriptableObjects/BulletData", order = 1)]
    public class PrefabBulletData : ScriptableObject
    {
        public List<GameObject> bulletList;
    }

