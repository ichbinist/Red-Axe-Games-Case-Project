using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuInfoController : MonoBehaviour
{
    public Image ButtonSprite;
    public Image HelperInfoHeader;
    public TextMeshProUGUI InfoText;
    public List<string> Infos = new List<string>();

    private int infoIndex = 0;

    private void Start()
    {
        HelperInfoHeader.rectTransform.DOLocalMoveX(700, 0.85f).From()
            .OnComplete(() => { 
            ButtonSprite.gameObject.SetActive(true);
        })
            .SetDelay(0.65f).OnStart(() =>{
            InfoText.SetText(Infos[GetIndex()]);
        });
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.B)) 
        {
            InfoText.SetText(Infos[GetIndex()]);
            ButtonSprite.rectTransform.DOPunchScale(Vector3.one * 0.75f, 0.4f, 8, 0.25f);
        }
    }

    private int GetIndex()
    {
        int index = Random.Range(0, Infos.Count);
        if(Infos.Count > 1)
        {
            if(index != infoIndex)
            {
                infoIndex = index;
                return index;
            }
            else
            {
                return GetIndex();
            }
        }
        else
        {
            infoIndex = index;
            return index;
        }
    }
}
