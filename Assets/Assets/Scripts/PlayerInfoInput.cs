using System;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerInfoInput : MonoBehaviour
{
	//load main play screen 
	public void PlayBtn_Handler()
	{
		SceneManager.LoadScene("Main");
	}

	//go back to menu
	public void GoToMenu()
	{
		SceneManager.LoadScene("Menu");
	}

	//variables for saving player input 
	public InputField iField;
	public static string playername;
	public static string username;
	private static string DOB;
	private static string address;
	private static string diagnosis;

	//save player name
	public void saveName()
	{
		playername = iField.text;
	}

	//save player username 
	public void saveUsername()
	{
		username = iField.text;
	}

	//save player date of birth 
	public void saveDOB()
	{
		DOB = iField.text;
	}

	//save player street address 
	public void saveAddr()
	{
		address = iField.text;
	}

	//save player diagnosis 
	public void saveDiag()
	{
		diagnosis = iField.text;
	}

	//create output files with player info
	public void writeFile()
	{
		try
		{
			//create file name with player name and date played 
			string fileName = "C:/Users/tayma/OneDrive/Documents/TayStuff/CS3160 Concepts/Final_Project/OutputFiles/" + playername + "_" + DateTime.Today.ToString("MM_dd_yyyy") + ".txt";

			//Pass the filepath and filename
			StreamWriter sw = new StreamWriter(fileName);

			//Write player inputted information to file 
			sw.WriteLine("Name:" + playername);
			sw.WriteLine("UserName: " + username);
			sw.WriteLine("Date Of Birth: " + DOB);
			sw.WriteLine("Address (City, State, Country): " + address);
			sw.WriteLine("Diagnosis: " + diagnosis);

			//write date played to file 
			sw.WriteLine("Date Played: " + DateTime.Today.ToString());

			//add space in the .txt file for the square sequence data 
			sw.WriteLine(" ");
			sw.WriteLine("Game Square Sequence: ");

			//Close the file
			sw.Close();
		}
		catch (FileNotFoundException e)
		{
			Debug.Log("File could not be found: Exception: " + e);
		}
		catch (IOException e)
		{
			Debug.Log("File could not be opened: Exception: " + e);
		}
	}
}
