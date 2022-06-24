using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGridOccupant : GridOccupant
{
    public GameObject pitPrefab;
    public GameObject playerSprite;
    public ParticleSystem trailParticle;
    bool canmove = true;

    public override void Awake()
    {
        base.Awake();
    }

    void Update()
    {
        if (!canmove) return;

        if (Input.GetKeyDown(KeyCode.G))
        {
            PlayNextRoomAnim();
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            MoveInDir(Vector2Int.right);
            return;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            MoveInDir(Vector2Int.left);
            return;
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            MoveInDir(Vector2Int.up);
            return;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            MoveInDir(Vector2Int.down);
            return;
        }
    }

    public void MoveInDir(Vector2Int dir)
    {
        var finalPos = gridPos + dir;
        var set = GridManager.GetPositionSet(finalPos);
        bool blocked = false;

        if (set != null)
        {
            foreach (var o in set)
            {
                if(o.blocksTile)
                {
                    blocked = true;
                    break;
                }
            }
        }

        if(blocked)
        {
            //Cant move into blocked tile

            transform.DOComplete();
            transform.DOPunchPosition((Vector2)dir * 0.6f, 0.25f);
            return;
        }

        SetPositionInGrid(finalPos);
        GameEvents.OnPlayerMove.Invoke();
    }

    public void PlayNextRoomAnim()
    {
        canmove = false;

        Sequence s = DOTween.Sequence();
        var pt = playerSprite.transform;

        var oPos = pt.position;
        var oScale = pt.localScale;

        s.Append(pt.DOMove(oPos + (Vector3.up*1.5f), 1.3f).SetEase(Ease.OutCubic));
        s.Join(pt.DOLocalRotate(new Vector3(0,0,360), 1.4f, RotateMode.FastBeyond360));
        s.Join(pt.DOScale(oScale * 1.2f, 1.3f));

        s.AppendInterval(0.15f);

        s.Append(pt.DOMove(oPos, 0.06f));
        s.Join(pt.DOScale(oScale * 0.7f, 0.08f));
        s.AppendCallback(() =>
        {
            var pit = Instantiate(pitPrefab);
            pit.transform.position = transform.position + (Vector3.down*0.3f);
            Destroy(gameObject);

            trailParticle.transform.SetParent(null);
            trailParticle.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        });
    }
}
