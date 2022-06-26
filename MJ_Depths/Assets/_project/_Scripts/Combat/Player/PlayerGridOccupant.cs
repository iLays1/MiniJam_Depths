using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGridOccupant : GridOccupant
{
    public SpriteRenderer spriteRend;
    public SpriteRenderer shadowSpriteRend;
    public int moveCost = 1;

    bool canmove = true;

    [Space, Header("VFXs")]
    public GameObject pitPrefab;
    public GameObject deathParticlePrefab;
    public ParticleSystem trailParticle;
    public ParticleSystem hitParticle;

    Camera cam;
    
    public override void Awake()
    {
        base.Awake();
        cam = Camera.main;
        Player.Instance.OnValueChange.AddListener(() =>
        {
            if (Player.Instance.fuel <= 0)
                PlayDeathAnim();
        });
    }

    void Update()
    {
        if (!canmove) return;

        if (Input.GetKeyDown(KeyCode.G))
            PlayNextRoomAnim();

        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            spriteRend.flipX = true;
            MoveInDir(Vector2Int.right);
            return;
        }
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            spriteRend.flipX = false;
            MoveInDir(Vector2Int.left);
            return;
        }
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            MoveInDir(Vector2Int.up);
            return;
        }
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            MoveInDir(Vector2Int.down);
            return;
        }
    }
    
    public void TakeDamage(int damage)
    {
        Player.Instance.SpendFuel(damage);

        TextPopup.Create($"-{damage.ToString()}", Color.yellow, transform.position);

        transform.DOComplete();
        transform.DOPunchPosition(Vector3.left * 0.35f, 0.3f, 12);
        hitParticle.Play();

        cam.DOShakePosition(0.2f, 0.1f, 30);

        AudioSystem.Instance.Play("POnHit");
    }
    public override void MoveInDir(Vector2Int dir)
    {
        if (!Player.Instance.EnoughFuel(moveCost)) return;

        var finalPos = gridPos + dir;
        var set = GridManager.GetPositionSet(finalPos);
        bool blocked = false;

        if (set != null)
        {
            foreach (var o in set)
            {
                if (o.blocksTile)
                {
                    blocked = true;
                    break;
                }
            }
        }

        if (blocked)
        {
            //Cant move into blocked tile
            transform.DOComplete();
            transform.DOPunchPosition((Vector2)dir * 0.2f, 0.25f);
            return;
        }

        SetPositionInGrid(finalPos);
        
        AudioSystem.Instance.Play("Move");
        Player.Instance.SpendFuel(moveCost);

        GameEvents.OnPlayerMove.Invoke();
        GameEvents.OnPlayerAct.Invoke();
    }

    public void PlayNextRoomAnim()
    {
        canmove = false;
        GameEvents.OnLevelEnd.Invoke();

        Sequence s = DOTween.Sequence();
        var pt = spriteRend.transform;
        var oPos = pt.position;
        var oScale = pt.localScale;

        s.Append(pt.DOMove(oPos + (Vector3.up*1.5f), 1f).SetEase(Ease.OutCubic));
        s.Join(pt.DOLocalRotate(new Vector3(0,0,360), 0.6f, RotateMode.FastBeyond360));
        s.Join(pt.DOScale(oScale * 1.2f, 1f));

        s.AppendInterval(0.15f);

        s.Append(pt.DOMove(oPos, 0.06f));
        s.Join(pt.DOScale(oScale * 0.7f, 0.08f));
        s.AppendCallback(() =>
        {
            var pit = Instantiate(pitPrefab);
            pit.transform.position = transform.position + (Vector3.down*0.3f);

            cam.DOShakePosition(0.4f, 0.5f, 30);
            trailParticle.Stop(true, ParticleSystemStopBehavior.StopEmitting);

            SceneSystem.Instance.LoadNextLevel(2f);

            DestroySelf();
        });
    }

    public void PlayDeathAnim()
    {
        canmove = false;
        GameEvents.OnLevelEnd.Invoke();

        MusicSystem.Instance.PlaySong(Song.GameoverTheme);

        Sequence s = DOTween.Sequence();
        var pt = spriteRend.transform;
        var oPos = pt.position;
        var oScale = pt.localScale;

        pt.DOKill();
        transform.DOKill();

        s.Append(pt.DOLocalMove((Vector3.down * 0.03f), 0.7f));
        s.Join(pt.DOScale(oScale + (Vector3.down * 0.3f), 0.7f));
        s.Join(transform.DOPunchPosition(Vector3.left * 0.2f, 0.7f));

        s.AppendInterval(0.2f);

        s.Append(pt.DOMove(oPos + (Vector3.up*0.3f), 0.04f));
        s.Join(pt.DOScale((oScale * 1.5f), 0.06f));
        
        s.AppendCallback(() =>
        {
            var dp = Instantiate(deathParticlePrefab);
            dp.transform.position = transform.position;

            cam.DOShakePosition(0.4f, 0.5f, 40);
            trailParticle.Stop(true, ParticleSystemStopBehavior.StopEmitting);

            AudioSystem.Instance.Play("PDeath");
            DestroySelf();
        });
    }

    void DestroySelf()
    {
        Destroy(this);
        Destroy(spriteRend.gameObject);
        Destroy(shadowSpriteRend.gameObject);
    }
}
