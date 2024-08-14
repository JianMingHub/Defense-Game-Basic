using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace COHENLI.DefenseBasic
{
    public class GameManager : MonoBehaviour, IComponentChecking
    {
        public static GameManager Ins;

        public float spawnTime;         // time to spawn
        public Enemy[] enemyPrefabs;    // list of enemy
        public ShopManager shopMng;
        private Player m_curPlayer; // current player
        private bool m_isGameOver;      // check if game is over
        private int m_score;            // score of the player

        public int Score { get => m_score; set => m_score = value; }            // get set by player

        public void Awake()
        {
            // Ins = this;
            MakeSingleton();
        }
        private void MakeSingleton() 
        {
            if (Ins == null)
            {
                Ins = this;
                DontDestroyOnLoad(this);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        // Start is called before the first frame update
        void Start()
        {
            if (IsComponentsNull()) return;
            GUIManager.Ins.ShowGameGUI(false);
            GUIManager.Ins.UpdateMainCoins();
        }
        public bool IsComponentsNull()
        {
            return GUIManager.Ins == null || shopMng == null || AudioController.Ins == null;
        }
        public void PlayGame()
        {
            if(IsComponentsNull()) return;
            ActivePlayer();
            StartCoroutine(SpawnEnemy());
            GUIManager.Ins.ShowGameGUI(true);
            GUIManager.Ins.UpdateGameplayCoins();
            AudioController.Ins.PlayBgm();
        }
        public void ActivePlayer()
        {
            if (IsComponentsNull()) return;

            if (m_curPlayer)
                Destroy(m_curPlayer.gameObject);
            var shopItem = shopMng.items;
            if (shopItem == null || shopItem.Length <= 0) return;
            var newPlayerPb = shopItem[Pref.curPlayerId].playerPrefab;
            if (newPlayerPb)
                m_curPlayer = Instantiate(newPlayerPb, new Vector3(-7f, -1f, 0f), Quaternion.identity);
        }
        public void GameOver()
        {
            if (m_isGameOver) return;
            m_isGameOver = true;
            Pref.bestScore = m_score;
            GUIManager.Ins.gameoverDialog.Show(true);
            AudioController.Ins.PlaySound(AudioController.Ins.gameOver);
        }

        // Create random enemy position for the player
        IEnumerator SpawnEnemy()
        {
            while(!m_isGameOver)
            {
                if(enemyPrefabs != null && enemyPrefabs.Length > 0)
                {
                    int ranIdx = Random.Range(0, enemyPrefabs.Length);          // get random number of enemy, not max value. Ex: (0,3) 0, 1, 2
                    Enemy enemyRefab = enemyPrefabs[ranIdx];                    // get an element from the array enemyPrefabs
                    if(enemyRefab)
                    {
                        Instantiate(enemyRefab, new Vector3(8, 0, 0), Quaternion.identity);         // create a copy of it at the location
                    }
                }

                yield return new WaitForSeconds(spawnTime);                     // wait for enemy to spawn
            }
        }
    }
}
