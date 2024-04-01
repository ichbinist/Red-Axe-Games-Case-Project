using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellerManager : Singleton<SellerManager>
{
    public Action<SellerInteractionData> OnSellerInteractionStarted;
    public Action<SellerInteractionData> OnSellerDataUpdate;
    public Action<SellerInteractionData> OnSellerInteractionFinished;

    public Action<float> OnHaggle;
    public Action<float> OnAccept;
    public Action OnRefuse;
    
}