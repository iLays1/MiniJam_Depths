using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class EnemyGridOccupant : GridOccupant, IDamageTaker
{
    public GameObject deathParticlePrefab;
    public ParticleSystem hitParticle;
    public int hp;
    public TextMeshPro hpText;

    Camera cam;

    public override void Awake()
    {
        base.Awake();
        cam = Camera.main;
        hpText.text = hp.ToString();
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;
        hpText.text = hp.ToString();

        TextPopup.Create($"-{damage.ToString()}", Color.red, transform.position);

        transform.DOComplete();
        transform.DOPunchPosition(Vector3.left * 0.35f, 0.3f, 12);
        hitParticle.Play();

        cam.DOShakePosition(0.2f, 0.1f, 30);

        AudioSystem.Instance.Play("EOnHit");

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

        AudioSystem.Instance.Play("EDeath");

        Invoke("DestroySelf", 0.02f);
    }
    void DestroySelf()
    {
        GridManager.RemoveOccupantFromDictionary(this);
        Destroy(gameObject);
    }
}
