using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] targets;
    public TMP_Text remaining;
    public GameObject winPanel;

    void LateUpdate()
    {
        remaining.text = targets.Length.ToString() + " <color=red>Canvas</color> restantes";

        if (targets.Length == 0)
        {
            winPanel.SetActive(true);
            remaining.gameObject.SetActive(false);
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        for(int i = 0; i < targets.Length; i++)
        {
            if (targets[i] == null)
            {
                GameObject[] newTargets = new GameObject[targets.Length - 1];
                for (int j = 0; j < targets.Length; j++)
                {
                    if (j < i)
                    {
                        newTargets[j] = targets[j];
                    }
                    else if (j > i)
                    {
                        newTargets[j - 1] = targets[j];
                    }
                }
                targets = newTargets;
                break;
            }
        }
    }
}
