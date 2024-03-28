using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuLoadingScreenController : MonoBehaviour
{
    public Image LoadingCircle;

    private void Awake()
    {
        LoadingCircle.rectTransform.DOLocalRotate(new Vector3(0f, 0f, 2f), 100f).SetLoops(-1, LoopType.Incremental).SetSpeedBased(true).SetEase(Ease.Linear);
    }
}
