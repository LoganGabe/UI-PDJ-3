using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement; // For scene management

public class GameManager : MonoBehaviour
{
    public GameObject[] targets;
    public TMP_Text remaining;
    public GameObject winPanel;
    public TMP_Text exitText;
    public float exitCooldown = 5f; // Cooldown duration in seconds

    private bool isExiting = false; // To prevent multiple coroutine calls

    void LateUpdate()
    {
        remaining.text = targets.Length.ToString() + " <color=red>Canvas</color> restantes";

        if (targets.Length == 0 && !isExiting)
        {
            winPanel.SetActive(true);
            if (remaining != null) remaining.gameObject.SetActive(false);
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            StartCoroutine(ExitLevelAfterCooldown()); // Start the cooldown coroutine
        }

        for (int i = 0; i < targets.Length; i++)
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

    private IEnumerator ExitLevelAfterCooldown()
    {
        isExiting = true; // Prevent multiple calls
        float elapsedTime = 0f;

        while (elapsedTime < exitCooldown)
        {
            elapsedTime += Time.unscaledDeltaTime; // Use unscaled time since Time.timeScale is 0
            exitText.text = $"Voltando em <color=red>{Mathf.CeilToInt(exitCooldown - elapsedTime)}</color>"; // Update exit text
            yield return null;
        }

        // Exit the level (load the next scene or quit)
        SceneManager.LoadScene(0); // Example: Load the next scene
    }
}
