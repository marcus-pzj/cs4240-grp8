using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* To include methods in another class:
 * declare: ScoreUpdate scoreUpdate;
 * initialize: scoreUpdate = GameObject.Find("ScoreObject").GetComponent<ScoreUpdate>();
 * call: scoreUpdate.increaseScore() / decreaseScore();
 */

public class ScoreUpdate : MonoBehaviour
{
	Text scoreText;
	private int score;

    // Start is called before the first frame update
    void Start()
    {
		score = 0;
		scoreText = GetComponentInChildren<Text>();
	}

	public void increaseScore()
	{
		score++;
		scoreText.text = score.ToString();
	}

	public void decreaseScore()
	{
		score--;
		GetComponentInChildren<Animator>().SetTrigger("Decrease");
		scoreText.text = score.ToString();
	}
}
