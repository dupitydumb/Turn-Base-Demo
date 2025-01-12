using TurnBasedGame;
using UnityEngine;
using System.Collections.Generic;
using TMPro;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
namespace TurnBasedGame
{
    public class GameManager : MonoBehaviour
    {
        
        [SerializeField] private GameObject logParent;

        [SerializeField] private GameObject logPrefab;
        [SerializeField] private GameObject turninfoprefab;
        public static GameManager Instance { get; private set; }

        public TurnManager turnManager;
        [SerializeField] private GameObject gameOverPanel;
        [SerializeField] private TMP_Text gameOverText;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            
        }

        public static IEnumerator SpawnTurnInfoText(string text)
        {
            GameObject canvas = GameObject.Find("Canvas");
            GameObject turninfotext = Instantiate(Instance.turninfoprefab, Vector3.zero, Quaternion.identity);
            turninfotext.transform.SetParent(canvas.transform);
            //set react transform
            turninfotext.transform.localPosition = new Vector3(0, 0, 0);
            turninfotext.GetComponentInChildren<TMP_Text>().text = text;
            Destroy(turninfotext, 1.5f);
            yield return new WaitForSeconds(1.5f);
        }

        public void GenerateLog(string text)
        {
            GameObject log = Instantiate(logPrefab, logParent.transform);
            log.GetComponentInChildren<TMP_Text>().text = text;
        }

        public void SpawnObject(GameObject prefab, Vector3 position, Quaternion rotation, Transform parent = null)
        {
            GameObject obj = Instantiate(prefab, position, rotation, parent);
        }

        public void GameOver(Entity deadEntity)
        {
            gameOverPanel.SetActive(true);
            gameOverText.text = $"{deadEntity.EntityName} has died.";

        }

        public void RestartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
    //spawn turn info text

}

