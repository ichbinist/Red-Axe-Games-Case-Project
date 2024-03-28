using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitManager : Singleton<InitManager>
{
    private void Start()
    {
        SceneManagement.Instance.LoadLevel("Main Menu");
    }
}