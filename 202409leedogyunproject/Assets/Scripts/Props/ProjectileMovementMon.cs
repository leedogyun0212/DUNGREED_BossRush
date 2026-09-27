using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovementMon : MonoBehaviour, IMovement
{
    //  초기 속도
    [SerializeField] private float _InitialSpeed = 500.0f;

    // 최대 속도
    [SerializeField] private float _MaxSpeed = 600.0f;

    // 방향
    public Vector2 lookdirection { get; set; } = Vector2.zero;

    // 현재 속도
    public float currentSpeed { get; set; } = 0.0f;

    private CharacterManager characterManager = null;

    private MonsterManager monsterManager = null;

    private void Awake()
    {
        characterManager = GameManager.GetManagerClass<CharacterManager>();
        monsterManager = GameManager.GetManagerClass<MonsterManager>();
        currentSpeed = _InitialSpeed;
        currentSpeed = _InitialSpeed;
    }

    private void OnEnable()
    {
        directionSet();
    }

    private void Update()
    {
        (this as IMovement).Movement();
        if (!Mathf.Approximately(currentSpeed, _MaxSpeed))
            currentSpeed = Mathf.Lerp(currentSpeed, _MaxSpeed, 0.2f * Time.deltaTime);
    }

    private void directionSet()
    {
        if (characterManager.Player.transform.rotation.y == 0)
            lookdirection = Vector2.right;
        else if (characterManager.Player.transform.rotation.y == -1)
            lookdirection = Vector2.left;
    }

    void IMovement.Movement()
    {
        transform.Translate(lookdirection * currentSpeed * Time.deltaTime, Space.Self);
    }

    // 초기화
    public void Initialize()
    { Initialize(Quaternion.Euler(0, 0, 0)); }

    public void Initialize(Quaternion rotation)
    {
        transform.rotation = rotation;
        currentSpeed = _InitialSpeed;
    }

    public void Initialize(Vector2 direction)
    {
        currentSpeed = _InitialSpeed;
        gameObject.transform.rotation = Quaternion.LookRotation(direction);
    }
}
