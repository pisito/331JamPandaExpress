using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
namespace PandaExpress2DGame
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance; // Singleton instance

        private bool isGameOver = false; // Track game over state
        private bool isGameStarted = false; // Track game start state

        public GameObject winPanel;
        public GameObject losePanel;

        public Transform player;
        public Transform exitDoor;

        public int countMonsterToEliminated = 0;
        public string EnemyTag = "Enemy";

        public UnityEvent onGameOver;

        private void Awake()
        {
            // Ensure there is only one instance of the GameManager
            if (Instance == null)
            {
                Instance = this;
                // we don't need this, simply reload the scene to replay for this version
                //DontDestroyOnLoad(gameObject); // Persist across scenes
            }
            else
            {
                Destroy(gameObject);
            }

            // Manage UI Panel
            winPanel.SetActive(false);
            losePanel.SetActive(false);
        }

        private void Start()
        {
            GameObject playerGO = GameObject.FindGameObjectWithTag("Player");
            if (playerGO != null)
            {
                player = playerGO.transform;
            }
            StartGame();
        }

        // Function to start the game
        public void StartGame()
        {
            if (isGameStarted) return;

            Debug.Log("Game Started!");
            isGameStarted = true;
            isGameOver = false;

            exitDoor.gameObject.SetActive(false);
            DetectMonstersInMap();

            // Load the main gameplay scene
            // Not need this, too = )
            // SceneManager.LoadScene("GameScene"); // Replace "GameScene" with your scene name
        }

        // Function to trigger win condition
        public void WinGame()
        {
            if (isGameOver) return;

            Debug.Log("You Win!");
            isGameOver = true;
            // Manage UI Panel
            winPanel.SetActive(true);
            losePanel.SetActive(false);

            // Load the win screen scene or display a win message
            // Should display menu GUI instead
            // SceneManager.LoadScene("WinScene"); // Replace "WinScene" with your win screen scene name
        }

        // Function to trigger game over
        public void GameOver()
        {
            if (isGameOver) return;

            Debug.Log("Game Over!");
            isGameOver = true;
            // Manage UI Panel
            winPanel.SetActive(false);
            onGameOver?.Invoke();
            //losePanel.SetActive(true);
            // Disable Player Object
            player.gameObject.SetActive(false);

            // Load the game over scene or display a game over message\
            // Similar to Win
            // SceneManager.LoadScene("GameOverScene"); // Replace "GameOverScene" with your game over screen scene name
        }

        // Function to restart the game
        public void RestartGame()
        {
            Debug.Log("Restarting Game...");
            isGameOver = false;
            isGameStarted = false;

            // Reload the current scene
            // Yes, this is good
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        // Function to quit the game
        public void QuitGame()
        {
            Debug.Log("Quitting Game...");
            Application.Quit();
        }

        public void DetectMonstersInMap()
        {
            Object[] enemies = GameObject.FindGameObjectsWithTag(EnemyTag);
            if(enemies!=null && enemies.Length > 0)
            {
                countMonsterToEliminated = enemies.Length;
            }
        }

        public void ReportMonsterKilled()
        {
            countMonsterToEliminated -= 1;
            if(countMonsterToEliminated <= 0)
            {
                SpawnExitDoor();
            }
        }

        public void SpawnExitDoor()
        {
            exitDoor.gameObject.SetActive(true);
        }
    }
}
