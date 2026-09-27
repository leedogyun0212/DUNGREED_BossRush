using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    private CharacterManager _CharacterManager = null;

    public float dashSpeed = 20f;          
    private float dashDuration = 0.2f;      
    public float dashCooldown = 1f;        
    private bool canDash = true;       
    private bool canghost = true;    

    private Rigidbody2D Dashrb;
    private Vector2 dashDirection;

    public bool DashBtn { get; set; } = false;

    // 잔상

    public GameObject ghostPrefab; // 잔상 프리팹
    public float ghostDelay = 0.1f; // 잔상 생성 간격
    private float lastGhostTime = 0f;

    void Start()
    {
        _CharacterManager = GameManager.GetManagerClass<CharacterManager>();
        Dashrb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Dirset();
        HandleDashInput();
        DushGhost();
    }

    public void HandleDashInput()
    {
        if (DashBtn && canDash)
        {
            StartCoroutine(Dash());
        }
    }

    private void Dirset()
    {
        if (_CharacterManager.inputVector.magnitude > 0.1)
        {
            dashDirection = _CharacterManager.inputVector;
        }
        else
            return;
    }

    IEnumerator Dash()
    {
        canDash = false;
        canghost = false;
        float originalGravity = Dashrb.gravityScale;
        Dashrb.gravityScale = 0;             
        Dashrb.velocity = dashDirection * dashSpeed;
        
        yield return new WaitForSeconds(dashDuration);
        canghost = true;
        Dashrb.velocity = Vector2.zero;        
        Dashrb.gravityScale = originalGravity;

        yield return new WaitForSeconds(dashCooldown);
        DashBtn = false;
        canDash = true;
    }

    private void DushGhost()
    {
        if (!canghost)
            CreateGhost();
    }

    public void CreateGhost()
    {
        if (Time.time - lastGhostTime < ghostDelay) return; 
        lastGhostTime = Time.time;

        GameObject ghost = Instantiate(ghostPrefab, transform.position, transform.rotation);
        SpriteRenderer ghostSR = ghost.GetComponent<SpriteRenderer>();

        ghostSR.color = new Color(1, 1, 1, 0.5f); 

        Destroy(ghost, 0.3f);
    }
}
