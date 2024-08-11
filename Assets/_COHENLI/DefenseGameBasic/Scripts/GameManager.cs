using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace COHENLI.DefenseBasic
{
    public class GameManager : MonoBehaviour
    {
        public float spawnTime;
        public Enemy[] enemyPrefabs;
        private bool m_isGameOver;
        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(SpawnEnemy());
        }

        // Update is called once per frame
        void Update()
        {
            
        }
        IEnumerator SpawnEnemy()
        {
            while(!m_isGameOver)
            {
                if(enemyPrefabs != null && enemyPrefabs.Length >0)
                {
                    int ranIdx = Random.Range(0, enemyPrefabs.Length);          // lấy ngẫu nhiên các chỉ số trong mảng, ko quá trị tối đa
                    Enemy enemyRefab = enemyPrefabs[ranIdx];
                    if(enemyRefab)
                    {
                        Instantiate(enemyRefab, new Vector3(8, 0, 0), Quaternion.identity);
                    }
                }

                yield return new WaitForSeconds(spawnTime);
            }
            
        }
    }
}
