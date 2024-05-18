using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EsciScript : MonoBehaviour
{
    public void Quit()
    {
        SceneManager.LoadScene("LoadingScreen");
    }
}