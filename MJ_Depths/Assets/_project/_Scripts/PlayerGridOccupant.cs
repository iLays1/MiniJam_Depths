using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGridOccupant : GridOccupant
{
    public GameObject pitPrefab;
    public ParticleSystem trailParticle;
    bool canmove = true;

    public override void Awake()
    {
        base.Awake();
    }

    void Update()
    {
        if (!canmove) return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            PlayNextRoomAnim();
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            MoveInDir(Vector2Int.right);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            MoveInDir(Vector2Int.left);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            MoveInDir(Vector2Int.up);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            MoveInDir(Vector2Int.down);
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
            transform.DOPunchPosition((Vector2)dir * 0.3f, 0.2f);
            return;
        }

        SetPositionInGrid(finalPos);
        GameEvents.OnPlayerMove.Invoke();
    }

    public void PlayNextRoomAnim()
    {
        canmove = false;

        Sequence s = DOTween.Sequence();

        var oPos = transform.position;

        s.Append(transform.DOMove(oPos + Vector3.up, 1.4f).SetEase(Ease.OutCubic));
        s.Join(transform.DOLocalRotate(new Vector3(0,0,720), 1.5f, RotateMode.FastBeyond360));

        s.AppendInterval(0.1f);

        s.Append(transform.DOMove(oPos, 0.05f));
        s.AppendCallback(() =>
        {
            var pit = Instantiate(pitPrefab);
            pit.transform.position = transform.position;
            Destroy(gameObject);

            trailParticle.transform.SetParent(null);
            trailParticle.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        });
    }
}
