using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : Singleton<InteractionManager>
{
    public Action<string> OnInteractableTargetChange;
}
