using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DeathParticleController : MonoBehaviour
{
    public GameObject enemySpawn;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void ParticleGetOld()
    {
        StartCoroutine(desactivateParticles());

    }
    public IEnumerator desactivateParticles()
    {
        yield return new WaitForSeconds(2);
        enemySpawn.GetComponent<EnemySpawnControler>().listOldParticle(gameObject);
        gameObject.SetActive(false);
    }
}