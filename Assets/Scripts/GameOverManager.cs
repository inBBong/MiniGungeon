using UnityEngine;
using UnityEngine.SceneManagement;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public void OnPressPlayAgain()
    {
        SceneManager.LoadScene("GameScene");
    }
    public void OnPressMainMenu()
    {
        SceneManager.LoadScene("MainMenuScene");        
    }
}
