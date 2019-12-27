using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	//load the player input scene
	public void PlayBtn_Handler()
	{
		SceneManager.LoadScene("PlayerInput");
	}

	//exit the game 
	public void QuitBtn_Handler()
	{
			Application.Quit();
	}

}
