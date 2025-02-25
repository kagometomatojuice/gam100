using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CutsceneManager : MonoBehaviour
{
    public SpriteRenderer comic;
    public LevelManager lmScript;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            lmScript.ChangeSceneOnWin();
        }
    }
}
