using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer _renderer;
    public SpriteRenderer Renderer => _renderer;

    [SerializeField]
    private Animator _animator;
    public Animator Animator => _animator;

    private ProjectileData _data;
    public ProjectileData Data => _data;

    public void Init(ProjectileData data)
    {
        _data = data;
        _renderer.size = data.Size;
        _animator.runtimeAnimatorController = data.Animator;
    }

    public void Hit()
    {
        var clips = _animator.runtimeAnimatorController.animationClips;
        float clipTime = 0f;
        var clip = clips.Find(x => x.name == "Hit");
        if (clip != null)
        {
            clipTime = clip.length;
        }
        var newPosition = transform.position;
        newPosition += _data.HitOffset;
        transform.position = newPosition;
        StartCoroutine(HandleHit(clipTime));
    }

    private IEnumerator HandleHit(float time)
    {
        _animator.Play("Hit");
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
