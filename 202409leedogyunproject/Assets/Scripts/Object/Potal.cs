using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Potal : MonoBehaviour
{
    private CapsuleCollider2D NextPotal = null;

    public GameObject MainCam = null;

    private CharacterManager _CharacterManager = null;


    private bool Cooldown = false;

    private Vector3 PotalPos = Vector3.zero;

    private Vector3 CamPos = Vector3.zero;

    public Vector2 minBounds;  // 포탈이 변경할 카메라 최소값
    public Vector2 maxBounds;  // 포탈이 변경할 카메라 최대값

    private MoveCamera moveCamera;

    private void Awake()
    {
        _CharacterManager = GameManager.GetManagerClass<CharacterManager>();
        NextPotal = GetComponent<CapsuleCollider2D>();

    }

    private void Start()
    {
        moveCamera = FindObjectOfType<MoveCamera>();
    }

    private void Update()
    {
        StartCoroutine(PotalCooldown());
    }

    IEnumerator PotalCooldown()
    {
        yield return new WaitUntil(() => Cooldown && PotalPos.x != _CharacterManager.Player.transform.position.x);
        Cooldown = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Potal"))
        {
            PotalPos = collision.transform.position;
            PosSet();
            Destroy(NextPotal);
        }
        if(collision.CompareTag("Player")&&!Cooldown)
        {
            if (Cooldown) return;
            Cooldown = true;
            _CharacterManager.Player.transform.position = PotalPos;
            CamMove();
            moveCamera.SetBounds(minBounds, maxBounds);

        }
    }

    private void PosSet()
    {
        if(transform.position.x > PotalPos.x)
        {
            PotalPos.x -= 2.0f;
        }
        else if (transform.position.x < PotalPos.x)
        {
            PotalPos.x += 2.0f;
        }
    }

    private void CamMove()
    {
        if (PotalPos.y + transform.position.y > -3)
        {
            CamMoveX();
        }
        else
            CamMoveY();
    }

    private void CamMoveX()
    {
        CamPos = MainCam.transform.position;

        float MoveOffset = 7.39f; // 장소의 절반 크기 (조정 가능)

        if (PotalPos.x - transform.position.x > 0)  // 오른쪽 포탈로 이동
        {
            CamPos.x = PotalPos.x + MoveOffset;
        }
        else  // 왼쪽 포탈로 이동
        {
            CamPos.x = PotalPos.x - MoveOffset;
        }

        MainCam.transform.position = CamPos;
    }

    private void CamMoveY()
    {
        CamPos = MainCam.transform.position;
        float moveDistance = Mathf.Abs(PotalPos.y - transform.position.y);
        if (PotalPos.y > transform.position.y)
        {
            CamPos.y += moveDistance;
        }
        else
        {
            CamPos.y -= moveDistance;
        }
        MainCam.transform.position = CamPos;
    }
}
