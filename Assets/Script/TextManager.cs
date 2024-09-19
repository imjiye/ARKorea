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
            Debug.LogWarning("���� �ΰ� �̻��� �Ŵ��� ����");
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        text1.DOText("�ѱ� ���θ� ���� �� ����ְ� ���� �� �� �ֵ���\r\n�ѱ� ���� ī��� ��������(AR : Augmented Reality) ����� \r\n������ ������ �Դϴ�.\r\n\r\n���̵� �� �ƴ϶� �ѱ��� ������ �ϴ� �ܱ��ε鿡�Ե�\r\n�����ϰ� ���� �� �ֱ⸦ ����մϴ�.", 12f);

        textFade.DOFade(1, 2);

        text3.DOText("'AR�ѱ۰���' ���� ����ϱ� ���ؼ��� �ѱ� ���� ī�尡 �ʿ��մϴ�.\r\n���� ī�尡 �غ���� �ʾҴٸ� �ٿ�޾� ����� �ּ���.", 12f);

        animal.DOText("���� ģ����", 1f);
        eat.DOText("���ִ� ���ĵ�", 1f);
        objects.DOText("�پ��� ���ǵ�", 1f);
        vehicle.DOText("���� Ż �͵�", 1f);

        
    }

    public void TextFade()
    {
        text2.DOText("1. �����ϰ��� �ϴ� ī�װ��� �����մϴ�.\r\n2. ������ ī�װ��� �ش��ϴ� ����ī�带 �غ��մϴ�.\r\n3. �ѱ��� �а� ī�带 ī�޶�� ���� �׸��� Ȯ���ϰ� ������ ���ϴ�.\r\n4. �ݺ��ؼ� �ܾ �����մϴ�.", 12f);

        textFade2.DOFade(1, 2);
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
