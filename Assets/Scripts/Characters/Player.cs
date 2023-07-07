using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [SerializeField] EntityData playerData;

    private void Start()
    {
        SetupCharacter(playerData);
    }
}
