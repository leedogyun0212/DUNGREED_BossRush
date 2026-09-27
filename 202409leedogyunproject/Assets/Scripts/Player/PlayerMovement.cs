using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour, IMovement
{
    public Vector2 lookdirection { get; set; }

    [Range(1.0f, 100.0f)] public float _MoveSpeed = 2.0f;
    [Range(1.0f, 100.0f)] public float _JumpPower = 8;

    // Rigidbody2D 
    public Rigidbody2D _rigid { get; private set; }

    public bool JumpTry { get; set; } = false;
    private bool MoveTry = false;

    private CharacterManager characterManager = null;

    private BoxCollider2D _BoxCollider2D = null;

    private float diry = 0f;

    public bool JumpButton { get; set; }

    private RaycastHit2D raycastHit;

    private void Awake()
    {
        characterManager = GameManager.GetManagerClass<CharacterManager>();
        _rigid = GetComponent<Rigidbody2D>();
        _BoxCollider2D = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        Moverotation();
        if (characterManager.inputVector.x != 0&& characterManager.inputVector.y < 0.7)
        {
            MoveTry = true;
        }
        if (JumpTry && JumpBtn())
        {
            Jump();
            characterManager.Player.playerAnim._Jumping = true;
        }
        if (MoveTry && JumpTry)
        {
            ;
            MoveTry = false;
            (this as IMovement).Movement();
        }
    }

    private void FixedUpdate()
    {
        Jumping();
        if (characterManager.inputVector.x != 0 && characterManager.inputVector.y < 0.7)
        {
            MoveTry = true;
        }
    }

    void IMovement.Movement()
    {
        if (characterManager.inputVector.y < 0)
            diry = characterManager.inputVector.y;
        lookdirection = new Vector2(characterManager.inputVector.x,-_rigid.gravityScale/2) * _MoveSpeed;
        _rigid.velocity = lookdirection;
    }


    private void Moverotation()
    {
        if (characterManager.inputVector.x < 0)
        {
            this.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if (characterManager.inputVector.x > 0)
            this.transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    private void Jumping()
    {
        LayerMask layerMask = LayerMask.GetMask("Ground") | LayerMask.GetMask("Monster");
        raycastHit = Physics2D.BoxCast(_BoxCollider2D.bounds.center, _BoxCollider2D.bounds.size, 0f, Vector2.down, 0.02f, layerMask);
        if (raycastHit.collider != null)
        {
            JumpTry = true;
            characterManager.Player.playerAnim._Jumping = false;
        }
        else
        {
            JumpTry = false;
        }
    }

    private bool JumpBtn()
    {
        if (characterManager.inputVector.y >= 0.7|| JumpButton)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void Jump()
    {
        _rigid.velocity = new Vector2(characterManager.inputVector.x*0.8f,1) * _JumpPower;
    }
}
