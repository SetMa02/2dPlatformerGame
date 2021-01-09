using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed;
    public float Speed
    {
        get { return speed; }
        set
        {
            if (value > 0.5 && value < 15)
                speed = value;
        }
    }
    [SerializeField] private float jump;
    public float Jump
    {
        get { return jump; }
        set
        {
            if (value > 0.5 && value < 20)
                jump = value;
        }
    }
    [SerializeField] private Rigidbody2D RB;
    [SerializeField] private GroundDetection groundDetection;
    private Vector3 direction;
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer Sprite;
    private bool isJumping;
    [SerializeField] private Arrow arrow;
    [SerializeField] private Transform arrowSpawnPoint;
    [SerializeField] private float shootForce;
    [SerializeField] private int arrowsCount = 3;
   [SerializeField] private Health health;
    [SerializeField] private BuffReciver buffReciver;
    private float bonusForce;
    private float bonusHealth;
    private float bonusDamage;

    public Health Health { get { return health; } } 

    public int coolDown;
    private bool isCoolDown;
    private Arrow currentArrow;
    private List<Arrow> arrowsPool;
    private UiCharacterController controller;


    private void Awake()
    {
        Instance = this;
    }
    #region Singlton
    public static Player Instance { get; set; }
    #endregion
    private void Start()
    {
        
        arrowsPool = new List<Arrow>();
        for(int i = 0; i<arrowsCount; i++)
        {
            var arrowTemp = Instantiate(arrow, arrowSpawnPoint);
            arrowsPool.Add(arrowTemp);
            arrowTemp.gameObject.SetActive(false);                                                                                                                                                                                                                                                                                          
        }

        buffReciver.OnBuffsChanged += ApplyBuffs;
    }

    private void ApplyBuffs()
    {
        var forceBuff = buffReciver.Buffs.Find(t => t.type == BuffType.Force);
        var damageBuff = buffReciver.Buffs.Find(t => t.type == BuffType.Damage);
        var armorBuff = buffReciver.Buffs.Find(t => t.type == BuffType.Armor);
        bonusForce = forceBuff == null ? 0 : forceBuff.additiveBonus;
        bonusHealth = armorBuff == null ? 0 : armorBuff.additiveBonus;
        health.setHealth((int)bonusHealth);
        bonusDamage = damageBuff == null ? 0 : damageBuff.additiveBonus;
    }

    private void FixedUpdate()
    {
        Move();

        animator.SetFloat("Speed", Mathf.Abs(direction.x));

    }

    private void Jump1()
    {
        if (groundDetection.isGrounded)
        {
            RB.AddForce(Vector3.up * (jump + bonusForce), ForceMode2D.Impulse);
            animator.SetTrigger("StartJump");
            isJumping = true;
        }
    }

    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Escape))
            GameManager.Instance.OnPauseClick();
        if (Input.GetKeyDown(KeyCode.Space))
            Jump1();
#endif
    }

    public void InitUiController(UiCharacterController uiController)
    {
        controller = uiController;
        controller.Jump.onClick.AddListener(Jump1);
        controller.Fire.onClick.AddListener(ChekShoot);
    }

    private void Move()
    {


        // Проверка на ПРИЗЕМЛЕННОСТЬ
        animator.SetBool("IsGrounded", groundDetection.isGrounded);
        isJumping = isJumping && !groundDetection.isGrounded;
        direction = Vector3.zero;

#if UNITY_EDITOR
        if (Input.GetKey(KeyCode.A))
            direction = Vector3.left;
        if (Input.GetKey(KeyCode.D))
            direction = Vector3.right;
#endif
        if (!isJumping && !groundDetection.isGrounded)
        {
            animator.SetTrigger("StartFall");
        }

        // Управление игроком
        if (controller.Left.IsPressed)
        {
            direction = Vector3.left;
        }
        if (controller.Right.IsPressed)
        {
            direction = Vector3.right;
        }
        direction *= speed;
        direction.y = RB.velocity.y;
        RB.velocity = direction;
       

        //Поворот спрайта по оси X
        if (direction.x > 0)
            Sprite.flipX = false;
        if (direction.x < 0)
            Sprite.flipX = true;
    }

    void ChekShoot()
    {   //При нажатии на ЛКМ, выстрел с силой shootForce 
        if (!isCoolDown)
        {
                animator.SetBool("Aim", true);
        }
    }

    private void OnCollisionStay2D(Collision2D col)
    {
        if(col.gameObject.CompareTag("Enemy"))
        {
            animator.SetBool("IsDamaged", true);
        }
        else
            animator.SetBool("IsDamaged", false);
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            animator.SetBool("IsDamaged", true);
        }
        else
            animator.SetBool("IsDamaged", false);
    }


    public void InitArrow()
    {
        currentArrow = GetArrowFromPool();
        currentArrow.SetImpulse(Vector2.right, Sprite.flipX ?
                -jump * shootForce : jump * shootForce, (int)bonusDamage, this);
    }
   void Shoot()
    {
            currentArrow.SetImpulse(Vector2.right, Sprite.flipX ?
                -jump * shootForce : jump * shootForce,(int)bonusDamage, this);
        StartCoroutine(CoolDown());
   
    }
    /*
    public void OnTriggerEnter2D(Collider2D collision)
    {
        //Сбор монеток
        if (collision.gameObject.CompareTag("Coin"))
        {
            PlayerInventory.Instance.coinsCount++;
            Debug.Log("Количество монет = " + PlayerInventory.Instance.coinsCount);
            Destroy(collision.gameObject);
        }
    }
    */
    private IEnumerator CoolDown()
    { 
        animator.SetBool("Aim", false);
        isCoolDown = true;
        yield return new WaitForSeconds(coolDown);
        isCoolDown = false;
       
    }
        
    private Arrow GetArrowFromPool()
    {
        if (arrowsPool.Count > 0)
        {
            var arrowTemp = arrowsPool[0];
            arrowsPool.Remove(arrowTemp);
            arrowTemp.gameObject.SetActive(true);
            arrowTemp.transform.parent = null;
            arrowTemp.transform.position = arrowSpawnPoint.transform.position;
            return arrowTemp;
        }
        return Instantiate(arrow, arrowSpawnPoint.position, Quaternion.identity);
    }
    public void ReturnArrowToPool(Arrow arrowTemp)
    {
        if(!arrowsPool.Contains(arrowTemp))
                arrowsPool.Add(arrowTemp);
        arrowTemp.transform.parent = arrowSpawnPoint;
        arrowTemp.transform.position = arrowSpawnPoint.transform.position;
        arrowTemp.gameObject.SetActive(false);
    }
}

