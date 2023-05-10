using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;
using Role;

public class InputController : MonoBehaviour
{
    // Use this for initialization
    private TMP_InputField text;
    private Systems.AISystem aISystem = Systems.AISystem.Instance;
    private Systems.DialogSystem dialogSystem = Systems.DialogSystem.Instance;
    void Start()
    {
        text = GetComponent<TMP_InputField>();
        Debug.Log($"what is {text}");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Debug.Log($"Catch user Input {text.text}");
            aISystem.EnqueueMessage(text.text);

            dialogSystem.Speak("user", text.text);
            text.text = "";
        }
    }

}
