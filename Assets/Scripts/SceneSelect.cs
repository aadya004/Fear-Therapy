using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadNyctophobiaScene()
    {
        print("ButtonPress");
          // replace with your actual scene name
        SceneManager.LoadScene("TestDemo");

    }

    public void LoadClaustrophobiaScene()
    {
        SceneManager.LoadScene("Claustrophobia");
    }

    public void LoadAcrophobiaScene()
    {
        
        SceneManager.LoadScene("Overview");
    }

    public void LoadPeacefulScene()
    {
        SceneManager.LoadScene("PeacefulScene");
    } 
}
