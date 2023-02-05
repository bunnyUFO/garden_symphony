using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [field: SerializeField] public Transform SpawnPoint { get; private set; }
}
