using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.IO;

public class GameManager : MonoBehaviour
{
	//array of buttons 
	public SpriteRenderer[] colors;

	//number of square in the array 0-8
	private int colorSelect;

	//keeps track of how long to light up each individual color 
	public float stayLit;
	float stayLitCounter;

	//creates pause between each square light up
	public float waitLights;
	float waitCounter;

	//control if the button is already lit or if it is dark
	bool lightOn;
	bool darkOn;

	//create a list of the color sequence that has been played 
	public List<int> activeSequence;
	int positionInSequence;

	//allows the player to actually press a button in the game 
	bool gameActive;

	//verifies if player is click the right button based on the sequence
	int inputInSequence;

	//creates win sound
	public AudioSource win;

	//creates score text
	public Text scoreText;

	// Use this for initialization
	void Start()
	{
		//set the high score 
		if (!PlayerPrefs.HasKey("HiScore"))
		{
			PlayerPrefs.SetInt("HiScore", 0);
		}

		scoreText.text = "Score: 0 - High Score: " + PlayerPrefs.GetInt("HiScore");
	}

	// Update is called once per frame
	void Update()
	{
		//keeps the cube lit up for a bit 
		if (lightOn)
		{
			stayLitCounter -= Time.deltaTime;

			//else change the color to back to the original color
			if (stayLitCounter < 0)
			{
				colors[activeSequence[positionInSequence]].color = new Color(colors[activeSequence[positionInSequence]].color.r, colors[activeSequence[positionInSequence]].color.g, colors[activeSequence[positionInSequence]].color.b, 0.5f);

				//make sure the cube should be dark and not lit
				lightOn = false;
				darkOn = true;

				//pause between the cube light up
				waitCounter = waitLights;

				//move the position forward in the list
				positionInSequence++;
			}
		}
			//if no colors are lit up, light up another cube
			if (darkOn)
			{
				//decrease the pause to nothing 
				waitCounter -= Time.deltaTime;

				//check if it is the end of the sequence
				if (positionInSequence >= activeSequence.Count)
				{
					//stop running the dark because the sequence is over
					darkOn = false;

					//allows input from the player
					gameActive = true;
				}
				else
				{
					if (waitCounter < 0)
					{
						//move onto the next light in the sequence and change the color
						colors[activeSequence[positionInSequence]].color = new Color(colors[activeSequence[positionInSequence]].color.r, colors[activeSequence[positionInSequence]].color.g, colors[activeSequence[positionInSequence]].color.b, 1f);

						stayLitCounter = stayLit;

						//make sure the cube should be lit and not dark
						lightOn = true;
						darkOn = false;
					}
				}
			}
	}

	//selects random color for game to run
	public void StartGame()
	{
		//set correct start values for start game
		activeSequence.Clear();
		positionInSequence = 0;
		inputInSequence = 0;

		//randomly selects cube color to light up 0-8
		colorSelect = UnityEngine.Random.Range(0, colors.Length);

		//add to the active sequence list the color that has been selected
		activeSequence.Add(colorSelect);

		//go into colors array, find which was randomly selected, and leave the RGB color values but change the alpha value of the color to light up the color
		colors[activeSequence[positionInSequence]].color = new Color(colors[activeSequence[positionInSequence]].color.r, colors[activeSequence[positionInSequence]].color.g, colors[activeSequence[positionInSequence]].color.b, 1f);

		//keeps track of how long to keep cube lit up 
		stayLitCounter = stayLit;

		//when game is started, the button should be lit 
		lightOn = true;

		scoreText.text = "Score: 0 - High Score: " + PlayerPrefs.GetInt("HiScore");
	}


	//handles when the color is pressed 
	public void ColorPressed(int button)
	{
		//create file name with player name and date played 
		string fileName = "C:/Users/tayma/OneDrive/Documents/TayStuff/CS3160 Concepts/Final_Project/OutputFiles/" + PlayerInfoInput.playername + "_" + DateTime.Today.ToString("MM_dd_yyy") + ".txt";
		
		if (gameActive)
		{
			//if the player presses the correct button in the sequence
			if (activeSequence[inputInSequence] == button)
			{
				
					//output button sequence info to file
					using (StreamWriter sw = File.AppendText(fileName))
					{
						sw.WriteLine("Tapped Correctly: Button " + button);
					}
				
				

				//increment so always checking if the input in sequence is correct
				inputInSequence++;

				//check if at the end of the list
				if (inputInSequence >= activeSequence.Count)
				{
					//update the high score and current score and display to the screen
					if (activeSequence.Count > PlayerPrefs.GetInt("HiScore"))
					{
						PlayerPrefs.SetInt("HiScore", activeSequence.Count);
					}
					scoreText.text = "Score: " + activeSequence.Count + " - High Score: " + PlayerPrefs.GetInt("HiScore");

					//reset sequence position to start at beginning
					positionInSequence = 0;
					inputInSequence = 0;

					//select new random color and add to the sequence 
					colorSelect = UnityEngine.Random.Range(0, colors.Length);

					activeSequence.Add(colorSelect);

					colors[activeSequence[positionInSequence]].color = new Color(colors[activeSequence[positionInSequence]].color.r, colors[activeSequence[positionInSequence]].color.g, colors[activeSequence[positionInSequence]].color.b, 1f);

					//reset counter 
					stayLitCounter = stayLit;
					lightOn = true;

					//make sure player cannot press button when not active
					gameActive = false;

					//play 'Win' sound
					win.Play();
				}
			}
			//if the incorrect button was pressed
			else
			{
				//deactivate the game 
				gameActive = false;

					//output button sequence info to file 
					using (StreamWriter sw = File.AppendText(fileName))
					{
						sw.WriteLine("Tapped Wrong: Button " + button);
					}

					//output the high score to the HighScores.txt file 
					using (StreamWriter highScoreFile = File.AppendText("C:/Users/tayma/OneDrive/Documents/TayStuff/CS3160 Concepts/Final_Project/OutputFiles/HighScores.txt"))
					{
						highScoreFile.WriteLine(activeSequence.Count);
					}

					//output the personal best score to the usernamePersonalBest.txt file
					using (StreamWriter personalBestFile = File.AppendText("C:/Users/tayma/OneDrive/Documents/TayStuff/CS3160 Concepts/Final_Project/OutputFiles/" + PlayerInfoInput.username + "PersonalBest.txt"))
					{
						personalBestFile.WriteLine(activeSequence.Count);
					}
				

				//load the game over scene 
				SceneManager.LoadScene("GameOver");
			}
		}
	}
}