using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class ThirdBattleEnemySpawner: MonoBehaviour
{
    GameObject LastEnemy;
    [SerializeField] GameManager gameManager;
    [SerializeField] GameObject[] Enemies;
    // Start is called before the first frame update

    private void Awake()
    {
        
    }
    void Start()
    {
        GameManager.ThirdBattle += StartSpawning;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        GameManager.ThirdBattle -= StartSpawning;
    }

    public void StartSpawning()
    {
        StartCoroutine(SpawnEnemies());
    }
    public IEnumerator SpawnEnemies()
    {
        for (int i = 0; i < Enemies.Length; i++) 
        {
            LastEnemy = Instantiate(Enemies[i], transform.position, Quaternion.identity);
            GameManager.enemyCount++;
            while (!(Vector3.Distance(LastEnemy.transform.position, transform.position) > 2f))
            { 
                yield return null; 
            }
        }
    }
}
