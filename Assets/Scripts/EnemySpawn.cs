using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;


public class EnemySpawn : MonoBehaviour
{
    public GameObject Prefab;
   public GameObject Spawn(Vector3Int floorPosition)
    {
        return Instantiate<GameObject>(Prefab, floorPosition, Quaternion.identity); // create enemy sprite     
    }
}
