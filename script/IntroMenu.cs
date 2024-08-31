using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class IntroMenu : MonoBehaviour
{
    public AudioClip StartSound;
    private AudioSource GameSound;

    private void Awake()
    {
        GameSound = GetComponent<AudioSource>();
    }

    public void Clock_Quit()
    {
        GameObject.Find("Canvas").transform.Find("ReGameOff").gameObject.SetActive(true);
    }

    public void Clock_ReQuitYes()
    {
        Application.Quit(); // ���� ����
    }

    public void Clock_ReQuitNo()
    {
        GameObject.Find("ReGameOff").SetActive(false);
    }

    public void Clock_GameStart()
    {
        GameSound.clip = StartSound;
        GameSound.Play();
        Invoke("GameStarter", 1.0f);
    }

    private void GameStarter()
    {
        SceneManager.LoadScene("SampleScene"); // ���� ������ �̵�
    }
}
