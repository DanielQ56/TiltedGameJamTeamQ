using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

using UnityEngine;

public class RestartB : MonoBehaviour
{
    private void OnMouseDown()
    {
        SceneManager.LoadScene("TitleScreen");
    }
}
