using UnityEngine;
using TMPro;

public class StatsUpdater : MonoBehaviour
{
	public TextMeshProUGUI meatText;
	public TextMeshProUGUI networkStatus;

	// Update is called once per frame
	void Update()
	{
		meatText.text = "Meat Throws: " + GameManager.Instance.numberOfMeatThrows;
		networkStatus.text = "Network Status: " + GameManager.Instance.websocketStatus;
	}
}
