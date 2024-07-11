using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TextManager : MonoBehaviour
{
    public static TextManager instance;

    public Text text1;
    public Text textFade;
    public Text text2;
    public Text textFade2;
    
    public Text text3;
    
    public Text animal;
    public Text eat;
    public Text objects;
    public Text vehicle;

    


    private void Awake()
    {
        if(instance == null)
        {
            instance = this; 
        }
        else
        {
            Debug.LogWarning("씬에 두개 이상의 매니저 존재");
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        text1.DOText("한글 공부를 조금 더 재미있고 쉽게 할 수 있도록\r\n한글 낱말 카드와 증강현실(AR : Augmented Reality) 기술을 \r\n결합한 콘텐츠 입니다.\r\n\r\n아이들 뿐 아니라 한글을 배우고자 하는 외국인들에게도\r\n유용하게 사용될 수 있기를 기대합니다.", 12f);

        textFade.DOFade(1, 2);

        text3.DOText("'AR한글공부' 앱을 사용하기 위해서는 한글 낱말 카드가 필요합니다.\r\n아직 카드가 준비되지 않았다면 다운받아 출력해 주세요.", 12f);

        animal.DOText("동물 친구들", 1f);
        eat.DOText("맛있는 음식들", 1f);
        objects.DOText("다양한 물건들", 1f);
        vehicle.DOText("멋진 탈 것들", 1f);

        
    }

    public void TextFade()
    {
        text2.DOText("1. 공부하고자 하는 카테고리를 선택합니다.\r\n2. 선택한 카테고리에 해당하는 낱말카드를 준비합니다.\r\n3. 한글을 읽고 카드를 카메라로 비춰 그림을 확인하고 발음을 들어봅니다.\r\n4. 반복해서 단어를 공부합니다.", 12f);

        textFade2.DOFade(1, 2);
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
