using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour {

    public void NextStage_0(){

		//SceneManager.LoadScene(SceneManager.GetActiveScene(Stage_0));
		//Application.LoadLevel(stage_0);
		Time.timeScale = 1;
		SceneManager.LoadScene("stage_0");
    }

    public void NextResult_0(){

		Time.timeScale = 1;
		SceneManager.LoadScene("Result_0");
        //Application.LoadLevel("Result_0");
    }

    public void NextLobby_0(){

		Time.timeScale = 1;
		//SceneManager.LoadScene("Lobby_0");
		//Application.LoadLevel(lobby_0);
		SceneManager.LoadScene("lobby_0");
    }

	public void NextStage_1(){
		
		Time.timeScale = 1;
		SceneManager.LoadScene("Stage_1");
		//Application.LoadLevel("Stage_1");
	}

	public void NextResult_1(){

		Time.timeScale = 1;
		SceneManager.LoadScene("Result_1");
		//Application.LoadLevel("Result_1");
	}

	public void DieResult(){

		Time.timeScale = 1;
		SceneManager.LoadScene("DieResult");
		//Application.LoadLevel("DieResult");
	}

	public void NextLobby_1(){

		Time.timeScale = 1;
		SceneManager.LoadScene("Lobby_1");
		//Application.LoadLevel("Lobby_1");
	}

    public void PauseGame(){

        Time.timeScale = 0;
    }

	public void ResumeGame(){

        Time.timeScale = 1;
    }

	public void QuitGame() {
		
		Application.Quit ();
	}
}
