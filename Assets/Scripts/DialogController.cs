using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;
using Role;

public class DialogController : MonoBehaviour
{
	// Use this for initialization
	private TMP_Text text;
	private Systems.DialogSystem dialogSystem;
    void Start()
	{
		text = GetComponent<TMP_Text>();
	}

	// Update is called once per frame
	void Update()
	{
		
	}

	public void ShowDialog(string dialog)
	{
		text.text = $"{dialog}";
	}

	public void ClearDialog()
	{
		text.text = "";
	}
}

