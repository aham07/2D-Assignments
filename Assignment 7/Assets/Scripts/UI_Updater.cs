using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UI_Updater : MonoBehaviour {

	public Text score;
	
	// Update is called once per frame
	void Update () {
		score.text = StatsManager.acorns.ToString();
	
	}
}
