using UnityEngine;
using UnityEngine.UI;

public class testscript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public Button button;
    void Start()
    {
        // Print the button reference
        print(button);

        // Count how many functions are attached to the button's OnClick event
        int listenerCount = button.onClick.GetPersistentEventCount();

        // Print the number of listeners
        print("Number of functions attached to the button: " + listenerCount);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ButtonPress(){
        print("ButtonPress");
    }
}
