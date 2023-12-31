using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemyScript : EnemyTemplate
{
    [SerializeField] GameObject bulletPrefab;
    private AudioManager audioPlayer;
    bool isAttacking = false;

    private void Awake() {
        base.Awake();
        audioPlayer = GetComponent<AudioManager>();
    }

    // Start is called before the first frame update
    void Start() {
        max_hp = 1;
        hp = max_hp;
    }

    // Update is called once per frame
    void Update() {
        if (hp <= 0) {
            Die();
            return;
        }
        if(!FoundPlayer()) {
            agent.isStopped = false;
            agent.SetDestination(player.transform.position);
        } else {
            Attack();
        }
    }

    void Attack() {
        agent.isStopped = true;
        if(!isAttacking) {
            StartCoroutine(WaitToShoot());
            isAttacking = true;
        }
    }

    IEnumerator WaitToShoot() {
        while (true) {
            GameObject bullet = Instantiate(bulletPrefab, agent.transform.position, Quaternion.identity);
            bullet.GetComponent<BulletScript>().ShootAt(player);
            audioPlayer.PlayClip(0, 0.5f);
            yield return new WaitForSeconds(4.0f);
        }
    }
}
