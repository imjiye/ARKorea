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

    public void OpenMenu() //메뉴버튼을 누른 경우 메뉴팝업 활성화
    {
        CanvasPause.SetActive(true);
        OnTogglePauseButton();
    }

    public void LoadScene(int sceneId) // 씬 변환
    {
        SceneManager.LoadScene(sceneId);
    }

    public void ReStart() // 재시작 버튼 누르면 이 씬을 다시 로드하고 움직이게 함
    {
        //Debug.Log("re");
        Time.timeScale = 1f;
        SceneManager.LoadSceneAsync(thisScene);
    }

    public void Continue() // 계속하기 버튼 누르면 메뉴팝업 비활성화, 게임 움직이게 함
    {
        CanvasPause.SetActive(false);
        Time.timeScale = 1f;
    }

    public void GameEnd() // 끝내기 버튼 누르면 게임종료
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
        if (Time.timeScale == 0) // 멈춰있다면
        {
            Time.timeScale = 1f; // 시작하기
        }
        else // 움직인다면
        {
            Time.timeScale = 0; // 멈추기
        }
    }

    public void OpenOption() // 옵션 버튼 누르면 옵션 메뉴 활성화
    {
        optionbtn.SetActive(true);
    }

    public void CloseOption()
    {
        optionbtn.SetActive(false);
    }
}
