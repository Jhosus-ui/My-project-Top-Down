using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private Vector3 enemyPosition;

    void Start()
    {
        if (PlayerPrefs.HasKey("EnemyPositionX"))
        {
            float x = PlayerPrefs.GetFloat("EnemyPositionX");
            float y = PlayerPrefs.GetFloat("EnemyPositionY");
            float z = PlayerPrefs.GetFloat("EnemyPositionZ");
            enemyPosition = new Vector3(x, y, z);
            transform.position = enemyPosition;
        }
    }

    void OnDisable()
    {
        PlayerPrefs.SetFloat("EnemyPositionX", transform.position.x);
        PlayerPrefs.SetFloat("EnemyPositionY", transform.position.y);
        PlayerPrefs.SetFloat("EnemyPositionZ", transform.position.z);
    }
}
