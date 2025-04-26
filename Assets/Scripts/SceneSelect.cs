using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
     // Reference to the first teleport target      
    public void LoadNyctophobiaScene()
    {
        SceneManager.LoadScene("Nycto"); // replace with your actual scene name
    }

    public void LoadClaustrophobiaScene()
    {
        SceneManager.LoadScene("Claustrophobia");
    }

    public void LoadAcrophobiaScene()
    {
        SceneManager.LoadScene("Acro");
    }
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("mainmenu");
    }
}
