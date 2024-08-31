using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public GameObject MenuWindow; // �޴� ��ư
    public GameObject Player; // �÷��̾�
    public GameObject backgroundsound; // ������� ������Ʈ

    // �޴� ����
    public void Clock_Menu()
    {
        MenuWindow.SetActive(true);
        Time.timeScale = 0; // ������ �Ͻ����� ��Ŵ
        GameObject.Find("BackGroundSound").GetComponent<AudioSource>().Pause(); // ������� ����
    }

    // �޴� ���
    public void Clock_Cancle()
    {
        MenuWindow.SetActive(false);
        Time.timeScale = 1; // ������ ��� ��Ŵ
        GameObject.Find("BackGroundSound").GetComponent<AudioSource>().Play(); // ������� ���
    }

    // ���� �����
    public void Clock_ReStart()
    {
        MenuWindow.SetActive(false);
        GameObject.Find("Canvas").transform.Find("ReGameStart").gameObject.SetActive(true);
    }

    // ����� �� ������ ��ġ
    public void Clock_ReGameStartYes()
    {
        // ���� �� �̸� SceneManager.GetActiveScene().name
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
        GameObject.Find("BackGroundSound").GetComponent<AudioSource>().Play(); // ������� ���
        Time.timeScale = 1; // ������ ��� ��Ŵ
    }

    // ����� ���
    public void Clock_ReGameStartNo()
    {
        GameObject.Find("ReGameStart").SetActive(false);
        Time.timeScale = 1; // ������ ��� ��Ŵ
        GameObject.Find("BackGroundSound").GetComponent<AudioSource>().Play(); // ������� ���
    }

    // ���� ������ ����
    public void Clock_Quit()
    {
        MenuWindow.SetActive(false);
        GameObject.Find("Canvas").transform.Find("ReGameOff").gameObject.SetActive(true);
    }

    // ���� ������
    public void Clock_ReQuitYes()
    {
        Application.Quit(); // ���� ����
    }

    // ���� ������ ���
    public void Clock_ReQuitNo()
    {
        GameObject.Find("ReGameOff").SetActive(false);
        Time.timeScale = 1; // ������ ��� ��Ŵ
        GameObject.Find("BackGroundSound").GetComponent<AudioSource>().Play(); // ������� ���
    }
}
