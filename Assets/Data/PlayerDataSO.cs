using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Data/PlayerData")]
public class PlayerDataSO : ScriptableObject
{
    // player transform properties
    public Vector3 playerPosition;
    public Quaternion playerRotation;

    [Header("Player Attribute")]
    // player attributes
    public int playerHealth;
}
