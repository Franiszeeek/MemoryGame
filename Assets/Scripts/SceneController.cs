using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour {

	public const int gridRows = 3;
	public const int gridCols = 6;
	public const float offsetX = 3f;
	public const float offsetY = 3f;

	[SerializeField] private MainCard orginalCard;
	[SerializeField] private Sprite[] images;

	private void Start()
	{
		Vector3 startPos = orginalCard.transform.position;
		int[] numbers = { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7, 8, 8 };
		numbers = ShuffleArray (numbers);

		for (int i = 0; i < gridCols; i++) {
			for (int j = 0; j < gridRows; j++) {
				MainCard card;
				if (i == 0 && j == 0) {
					card = orginalCard;
				} else {
					card = Instantiate (orginalCard) as MainCard;
				}
				int index = j * gridCols + i;
				int id = numbers [index];
				card.ChangeSprite (id, images[id]);

				float posX = (offsetX * i) + startPos.x;
				float posY = (offsetY * j) + startPos.y;
				card.transform.position = new Vector3 (posX, posY, startPos.z);
			}
		}
	}
	private int[] ShuffleArray(int[] numbers)
	{
		int[] newArray = numbers.Clone () as int[];
		for (int i = 0; i < newArray.Length; i++) {
			int tmp = newArray [i];
			int r = Random.Range (i, newArray.Length);
			newArray [i] = newArray [r];
			newArray [r] = tmp;
		}
		return newArray;
	}
//------------------------------------------------------------------------------------
	private MainCard _firstRevealed;
	private MainCard _secondRevealed;

	protected int score = 0;
	private int pair = 0;

	[SerializeField] private TextMesh scoreLabel;

	public bool canReveal
	{
		get{ return _secondRevealed == null;}
	}

	public void CardRevealed(MainCard card)
	{
		if (_firstRevealed == null) 
		{
			_firstRevealed = card;
		} 
		else 
		{
			_secondRevealed = card;
			StartCoroutine(CheckMatch());
		}
	}
		
	private IEnumerator CheckMatch()
	{
		if (_firstRevealed.id == _secondRevealed.id) 
		{
			pair++;
			if (pair == 9) {
				SceneManager.LoadScene ("Scene_002");
			}
		} 
		else 
		{
			score++;
			yield return new WaitForSeconds (0.5f);
			_firstRevealed.Unreveal ();
			_secondRevealed.Unreveal ();
			scoreLabel.text = "Liczba ruchów:\n" + score;
		}
		_firstRevealed = null;
		_secondRevealed = null;
	}

	public void Restart()
	{
		SceneManager.LoadScene("Scene_001");
	}

	public void Back()
	{
		SceneManager.LoadScene("Scene_000");
	}
}
