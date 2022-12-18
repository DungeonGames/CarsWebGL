using UnityEngine;

public class PauseGame : MonoBehaviour
{
    public void ResumeGame()
    {
        Time.timeScale = 1;
    }

    public void Pause()
    {
        Time.timeScale = 0;
    }
}
