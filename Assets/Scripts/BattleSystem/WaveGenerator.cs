using System;
using System.Collections;
using UnityEngine;

public class WaveGenerator : MonoBehaviour {
    public GameObject enemy;
    public float waitInterval;
    public Vector2 startingPos;

    private IEnumerator Start() {
        yield return new WaitForSeconds(waitInterval);
        
        GameObject enemyNew = Instantiate(enemy, transform.root);
        enemyNew.SetActive(true);
        enemyNew.transform.position = startingPos;
        
        StartCoroutine(Start());
    }
}