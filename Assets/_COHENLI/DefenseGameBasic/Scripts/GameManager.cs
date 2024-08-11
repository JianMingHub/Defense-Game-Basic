using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace COHENLI.DefenseBasic
{
    public class GameManager : MonoBehaviour
    {
        public float spawnTime;         // time to spawn
        public Enemy[] enemyPrefabs;    // list of enemy
        private bool m_isGameOver;      // check if game is over
        private int m_score;            // score of the player

        public int Score { get => m_score; set => m_score = value; }            // get set by player

        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(SpawnEnemy());
        }

        // Update is called once per frame
        void Update()
        {
            
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
