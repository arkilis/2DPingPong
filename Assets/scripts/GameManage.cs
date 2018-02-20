using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManage : MonoBehaviour {

	public GameObject finalResultUI;

	// Restart a game
	public void Restart() {
		SceneManager.LoadScene (SceneManager.GetActiveScene().name);
	}

	// 

	// Stop a game
	public void GameStop() {
		Time.timeScale = 0;
		finalResultUI.SetActive (true);
		ShowResults ();
	}


	public void ShowResults() {
		BallMovement ball = FindObjectOfType<BallMovement> ();
		Text finalResultText = finalResultUI.transform.Find ("Results").GetComponentInChildren<Text> ();
		finalResultText.text = string.Format ("{0} : {1}", ball.leftScore, ball.rightScore);
	}

}
