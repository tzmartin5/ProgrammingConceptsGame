using UnityEngine;

/// <summary>
/// This script handles when the player clicks on the cube when it lights up 
/// </summary>
public class ButtonController : MonoBehaviour {

	//access to cube button through sprite renderer 
	private SpriteRenderer sprite;

	//the cube button's object number
	public int buttonNumber;

	private GameManager game;

	// Use this for initialization
	void Start () {

		//attach the cube button object 
		sprite = GetComponent<SpriteRenderer>();

		//reference to game manager script
		game = FindObjectOfType<GameManager>();
	}
	
	//detects when the player clicks on the object 
	private void OnMouseDown()
	{
		//change the color of the sprite object to light up 
		sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 1f);
	}

	//detects when the player releases the click on the object
	private void OnMouseUp()
	{
		//change the color of the sprite object to the original dark color 
		sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 0.5f);

		//access the color presed function in the game manager script and send the number of the color
		game.ColorPressed(buttonNumber);

	}
}
