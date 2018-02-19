using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallMovement : MonoBehaviour {

	public float speed = 30;
	public int leftScore = 0;
	public int rightScore = 0;
	public Text scoreText;

	void Start() {
		// Initial Velocity
		GetComponent<Rigidbody2D>().velocity = Vector2.right * speed;
		leftScore = 0;
		rightScore = 0;
	}

	float hitFactor(Vector2 ballPos, Vector2 racketPos,
		float racketHeight) {
		// ascii art:
		// ||  1 <- at the top of the racket
		// ||
		// ||  0 <- at the middle of the racket
		// ||
		// || -1 <- at the bottom of the racket
		return (ballPos.y - racketPos.y) / racketHeight;
	}

	void OnCollisionEnter2D(Collision2D col) {
		// Note: 'col' holds the collision information. If the
		// Ball collided with a racket, then:
		//   col.gameObject is the racket
		//   col.transform.position is the racket's position
		//   col.collider is the racket's collider

		// Hit the left Racket?
		if (col.gameObject.name == "RacketLeft") {
			// Play sound
			FindObjectOfType<AudioManager>().Play("pingpong");

			// Calculate hit Factor
			float y = hitFactor(transform.position, col.transform.position, col.collider.bounds.size.y);

			// Calculate direction, make length=1 via .normalized
			Vector2 dir = new Vector2(1, y).normalized;

			// Set Velocity with dir * speed
			GetComponent<Rigidbody2D>().velocity = dir * speed;
			leftScore++;
		}

		// Hit the right Racket?
		if (col.gameObject.name == "RacketRight") {

			FindObjectOfType<AudioManager>().Play("pingpong");
			// Calculate hit Factor
			float y = hitFactor(transform.position, col.transform.position, col.collider.bounds.size.y);

			// Calculate direction, make length=1 via .normalized
			Vector2 dir = new Vector2(-1, y).normalized;

			// Set Velocity with dir * speed
			GetComponent<Rigidbody2D>().velocity = dir * speed;
			rightScore++;
		}

		// when hit left wall => fail
		if (col.gameObject.name == "WallLeft") {
			FindObjectOfType<GameManage> ().GameStop ();
		}

		// when hit right wall => fail
		if (col.gameObject.name == "WallRight") {
			FindObjectOfType<GameManage> ().GameStop ();
		}

		// Update the score
		UpdateScore(leftScore, rightScore);
	}

	void UpdateScore(int left_score, int right_score) {
		scoreText.text = string.Format ("{0} : {1}", leftScore, rightScore);
	}
}
