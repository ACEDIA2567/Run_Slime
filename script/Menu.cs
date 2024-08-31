using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public GameObject MenuWindow; // 메뉴 버튼
    public GameObject Player; // 플레이어
    public GameObject backgroundsound; // 배경음악 컴포넌트

    // 메뉴 오픈
    public void Clock_Menu()
    {
        MenuWindow.SetActive(true);
        Time.timeScale = 0; // 게임을 일시정지 시킴
        GameObject.Find("BackGroundSound").GetComponent<AudioSource>().Pause(); // 배경음악 중지
    }

    // 메뉴 취소
    public void Clock_Cancle()
    {
        MenuWindow.SetActive(false);
        Time.timeScale = 1; // 게임을 재생 시킴
        GameObject.Find("BackGroundSound").GetComponent<AudioSource>().Play(); // 배경음악 재생
    }

    // 게임 재시작
    public void Clock_ReStart()
    {
        MenuWindow.SetActive(false);
        GameObject.Find("Canvas").transform.Find("ReGameStart").gameObject.SetActive(true);
    }

    // 재시작 시 리스폰 위치
    public void Clock_ReGameStartYes()
    {
        // 현재 씬 이름 SceneManager.GetActiveScene().name
        //SceneManager.LoadScene("SampleScene");
        if (backgroundsound.GetComponent<BackGroundSound>().SoundCheck == 0)
        {
            Player.transform.position = new Vector3(51.0f, 21.0f, 0);
        }
        else if (backgroundsound.GetComponent<BackGroundSound>().SoundCheck == 1)
        {
            Player.transform.position = new Vector3(0, 0.25f, 0);
        }
        else if (backgroundsound.GetComponent<BackGroundSound>().SoundCheck == 2)
        {
            Player.transform.position = new Vector3(46.5f, 57.0f, 0);
        }
        else
        {
            Player.transform.position = new Vector3(84.0f, 66.0f, 0);
        }
        GameObject.Find("Canvas").transform.Find("ReGameStart").gameObject.SetActive(false);
        GameObject.Find("BackGroundSound").GetComponent<AudioSource>().Play(); // 배경음악 재생
        Time.timeScale = 1; // 게임을 재생 시킴
    }

    // 재시작 취소
    public void Clock_ReGameStartNo()
    {
        GameObject.Find("ReGameStart").SetActive(false);
        Time.timeScale = 1; // 게임을 재생 시킴
        GameObject.Find("BackGroundSound").GetComponent<AudioSource>().Play(); // 배경음악 재생
    }

    // 게임 나가기 여부
    public void Clock_Quit()
    {
        MenuWindow.SetActive(false);
        GameObject.Find("Canvas").transform.Find("ReGameOff").gameObject.SetActive(true);
    }

    // 게임 나가기
    public void Clock_ReQuitYes()
    {
        Application.Quit(); // 게임 종료
    }

    // 게임 나가기 취소
    public void Clock_ReQuitNo()
    {
        GameObject.Find("ReGameOff").SetActive(false);
        Time.timeScale = 1; // 게임을 재생 시킴
        GameObject.Find("BackGroundSound").GetComponent<AudioSource>().Play(); // 배경음악 재생
    }
}
