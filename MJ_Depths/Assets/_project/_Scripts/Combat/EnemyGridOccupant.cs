using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyGridOccupant : GridOccupant, IDamageTaker
{
    public GameObject deathParticlePrefab;
    public ParticleSystem hitParticle;
    public int hp;

    Camera cam;

    public override void Awake()
    {
        base.Awake();
        cam = Camera.main;
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;

        TextPopup.Create(damage.ToString(), Color.red, transform.position);

        transform.DOComplete();
        transform.DOPunchPosition(Vector3.left * 0.35f, 0.3f, 12);
        hitParticle.Play();

        cam.DOShakePosition(0.2f, 0.1f, 30);

        AudioManager.Instance.Play("EOnHit");

        if (hp <= 0)
        {
            hp = 0;
            Death();
        }
    }

    public void Death()
    {
        var dp = Instantiate(deathParticlePrefab);
        dp.transform.position = transform.position;

        AudioManager.Instance.Play("EDeath");

        Invoke("DestroySelf", 0.02f);
    }
    void DestroySelf()
    {
        GridManager.RemoveOccupantFromDictionary(this);
        Destroy(gameObject);
    }
}
