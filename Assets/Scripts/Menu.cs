using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Menu : MonoBehaviour {

	public void Play()
	{
		SceneManager.LoadScene("Scene_001");
	}
	public void End()
	{
		//SceneManager.LoadScene("GameRoom");
	}
}
