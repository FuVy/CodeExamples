using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Creatures/Projectile")]
public class ProjectileData : ScriptableObject
{
    [SerializeField]
    private AnimatorOverrideController _animator;
    public AnimatorOverrideController Animator => _animator;

    [SerializeField]
    private Vector3 _spawnOffset;
    public Vector3 Spawnoffset => _spawnOffset;

    [SerializeField]
    private Vector3 _hitOffset;
    public Vector3 HitOffset => _hitOffset;

    [SerializeField]
    private Vector2 _size = new Vector2(0.3f, 0.3f);
    public Vector2 Size => _size;

    [SerializeField]
    private float _speed = 1f;
    public float Speed => _speed;
}
