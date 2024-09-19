using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BottonManager : MonoBehaviour
{
    public GameObject CanvasPause;
    private GameObject btnReStart;
    private GameObject btnContinue;
    private GameObject btnGameEnd;
    public GameObject optionbtn;

    public string thisScene;

    public GameObject t1;
    public GameObject t2;

    public void OpenMenu() //�޴���ư�� ���� ��� �޴��˾� Ȱ��ȭ
    {
        CanvasPause.SetActive(true);
        OnTogglePauseButton();
    }

    public void LoadScene(int sceneId) // �� ��ȯ
    {
        SceneManager.LoadScene(sceneId);
    }

    public void ReStart() // ����� ��ư ������ �� ���� �ٽ� �ε��ϰ� �����̰� ��
    {
        //Debug.Log("re");
        Time.timeScale = 1f;
        SceneManager.LoadSceneAsync(thisScene);
    }

    public void Continue() // ����ϱ� ��ư ������ �޴��˾� ��Ȱ��ȭ, ���� �����̰� ��
    {
        CanvasPause.SetActive(false);
        Time.timeScale = 1f;
    }

    public void GameEnd() // ������ ��ư ������ ��������
    {
        Debug.Log("end");
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void OnTogglePauseButton()
    {
        if (Time.timeScale == 0) // �����ִٸ�
        {
            Time.timeScale = 1f; // �����ϱ�
        }
        else // �����δٸ�
        {
            Time.timeScale = 0; // ���߱�
        }
    }

    public void OpenOption() // �ɼ� ��ư ������ �ɼ� �޴� Ȱ��ȭ
    {
        optionbtn.SetActive(true);
    }

    public void CloseOption()
    {
        optionbtn.SetActive(false);
    }

    public void OnClick_OpenURL()
    {
        Application.OpenURL("https://drive.google.com/drive/folders/1ilYEfJFynY3GEN5GHcw_-fdD-MEEymHI?usp=sharing"); // Ŭ���ϸ� ����Ʈ�� �̵��ϰ� �ϴ� �ڵ�
    }

    public void OpenT2()
    {
        t2.SetActive(true);
        t1.SetActive(false);
        TextManager.instance.TextFade();
    }
}
