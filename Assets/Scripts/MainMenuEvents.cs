using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenuEvents : MonoBehaviour
{
    private UIDocument uiDocument;
    private Button button;
    private List<Button> menuButtons = new List<Button>();
    private AudioSource audioSource;
    public AudioClip hoverSound;
    public AudioClip clickSound;

    private VisualElement fadeOverlay; // Reference to the fade overlay
    public float fadeDuration = 1f; // Duration of the fade effect

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        uiDocument = GetComponent<UIDocument>();

        button = uiDocument.rootVisualElement.Q("PlayButton") as Button;
        button.RegisterCallback<ClickEvent>(OnPlayGameButtonClick);

        menuButtons = uiDocument.rootVisualElement.Query<Button>().ToList();

        foreach (var menuButton in menuButtons)
        {
            menuButton.RegisterCallback<ClickEvent>(OnMenuButtonClick);
            menuButton.RegisterCallback<MouseEnterEvent>(OnMenuButtonHover);
        }

        // Get the fade overlay element
        fadeOverlay = uiDocument.rootVisualElement.Q("FadeOverlay");

        // Deactivate the fade overlay at the start
        fadeOverlay.style.opacity = 0; // Set initial opacity to 0 (fully transparent)
        fadeOverlay.pickingMode = PickingMode.Ignore; // Disable pointer events
    }

    private void OnPlayGameButtonClick(ClickEvent evt)
    {
        StartCoroutine(FadeAndLoadScene(1)); // Start fade effect before loading the scene
    }

    private IEnumerator FadeAndLoadScene(int sceneIndex)
    {
        // Activate the fade overlay
        fadeOverlay.pickingMode = PickingMode.Position; // Enable pointer events

        // Fade to black
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsedTime / fadeDuration);
            fadeOverlay.style.opacity = alpha;
            Debug.Log($"Alpha: {alpha}"); // Properly assign opacity as a StyleFloat
            yield return null;
        }

        // Load the scene
        SceneManager.LoadScene(sceneIndex);
    }

    private void OnDestroy()
    {
        button.UnregisterCallback<ClickEvent>(OnPlayGameButtonClick);

        foreach (var menuButton in menuButtons)
        {
            if (menuButton.name != "PlayButton")
            {
                menuButton.UnregisterCallback<ClickEvent>(OnMenuButtonClick);
            }
        }
    }

    private void OnMenuButtonClick(ClickEvent evt)
    {
        audioSource.PlayOneShot(clickSound);
    }

    private void OnMenuButtonHover(MouseEnterEvent evt)
    {
        Debug.Log("Hovering over button: ");
        audioSource.PlayOneShot(hoverSound);
    }
}
