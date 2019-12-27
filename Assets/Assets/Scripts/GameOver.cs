using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using System.Linq;

public class GameOver : MonoBehaviour {

	//load main screen so player can play again 
	public void PlayAgain()
	{
		SceneManager.LoadScene("Menu");
	}

	//exit the game
	public void QuitBtn_Handler()
	{
		Application.Quit();
	}

	//play game over sound 
	public AudioSource gameOver;

	public void PlaySound()
	{
		gameOver.Play();
	}

	//encrypt and/or decrypt the file 
	public void ChangeFile(){
		EncryptTest.EncryptAndDecrypt.Main();
	}

	//used for displaying scores 
	public Text highScore;
	public Text personalScore;


	void Start()
	{
		//display high score and personal best scores on screen 
		WriteScore("C:/Users/tayma/OneDrive/Documents/TayStuff/CS3160 Concepts/Final_Project/OutputFiles/HighScores.txt", "HIGH SCORE", highScore);
		WriteScore("C:/Users/tayma/OneDrive/Documents/TayStuff/CS3160 Concepts/Final_Project/OutputFiles/" + PlayerInfoInput.username + "PersonalBest.txt", "PERSONAL BEST", personalScore);
	}

	//finds the best 5 scores from the personal best and high score files 
	public void WriteScore(string filename, string placeholder, Text score)
	{
		try
		{
			//read in the lines from the file 
			string[] lines = File.ReadAllLines(filename);

			//convert the string array to int array 
			int[] numbers = lines.Select(s => int.Parse(s)).ToArray();

			//find the 5 highest scores in the file 
			int i;
			int first = 000;
			int second = 000;
			int third = 000;
			int fourth = 000;
			int fifth = 000;

			for (i = 0; i < numbers.Length; i++)
			{

				if (numbers[i] > first)
				{
					fifth = fourth;
					fourth = third;
					third = second;
					second = first;
					first = numbers[i];
				}
				else if (numbers[i] > second)
				{
					fifth = fourth;
					fourth = third;
					third = second;
					second = numbers[i];
				}
				else if (numbers[i] > third)
				{
					fifth = fourth;
					fourth = third;
					third = numbers[i];
				}
				else if (numbers[i] > fourth)
				{
					fifth = fourth;
					fourth = numbers[i];
				}
				else if (numbers[i] > fifth)
				{
					fifth = numbers[i];
				}

			}

			//print scores on the screen
			score.text = placeholder + "\n******************* " +
				"\nFirst Place: " + first +
				"\nSecond Place:  " + second +
				"\nThird Place: " + third +
				"\nFourth Place: " + fourth +
				"\nFifth Place: " + fifth;

		}
		catch (FileNotFoundException e)
		{
			Debug.Log("File could not be found: Exception: " + e);
		}
		catch (IOException e)
		{
			Debug.Log("File count not be opened: Exception: " + e);
		}
	}
}
	

