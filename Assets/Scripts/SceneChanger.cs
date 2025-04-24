using UnityEngine;

public class SceneChanger : MonoBehaviour
{
    public void ChangeScene(int index)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(index);
    }
}
