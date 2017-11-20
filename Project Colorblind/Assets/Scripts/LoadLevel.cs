using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LoadLevel : MonoBehaviour {

	public GameObject loadingScreen; 
	public Slider slider; 
	public Text progressText;

	//used for loading in levels. Name is the name of the level
	public void LoadInLevel(string levelName){
		StartCoroutine (loadAsync (levelName));
	}

	IEnumerator loadAsync(string levelName){
		AsyncOperation op =	SceneManager.LoadSceneAsync (levelName);
		loadingScreen.SetActive (true);
		while (op.isDone == false) {
			float progress = Mathf.Clamp01 (op.progress / .9f);
			slider.value = progress;
			progressText.text = progress * 100f + "%";

			yield return null;

		}

	}


}
