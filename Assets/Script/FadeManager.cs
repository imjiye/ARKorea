using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FadeManager : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Image>().DOFade(0, 1).SetLoops(-5, LoopType.Yoyo);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
