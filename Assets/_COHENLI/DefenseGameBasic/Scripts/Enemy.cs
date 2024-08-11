using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace COHENLI.DefenseBasic
{
    public class Enemy : MonoBehaviour, IComponentChecking
    {
        public float speed;
        public float atkDistance;               // khoảng cách enemy có thể tấn công player
        private Animator m_amin;
        private Rigidbody2D m_rb;
        private Player m_player;
        private  bool m_isDead;

        private GameManager m_gm;

        private void Awake() 
        {
            m_amin = GetComponent<Animator>();
            m_rb = GetComponent<Rigidbody2D>();
            m_player = FindAnyObjectByType<Player>();
            m_gm = FindAnyObjectByType<GameManager>();
        }
        // Start is called before the first frame update
        void Start()
        {
            
        }
        public bool IsComponentsNull()
        {
            return m_amin == null || m_rb == null || m_player == null;
        }

        // Update is called once per frame
        void Update()
        {
            if(IsComponentsNull()) return;

            // tính khoảng cách giữa player và con quái
            float distToPlayer = Vector2.Distance(m_player.transform.position, transform.position);
            
            if(distToPlayer <= atkDistance)
            {
                m_amin.SetBool(Const.ATTACK_ANIM, true);    // chuyển sang trạng thái tấn công
                m_rb.velocity = Vector2.zero;   // (0,0)    dừng di chuyển con enemy lại
            }
            else
            {
                m_rb.velocity = new Vector2(-speed, m_rb.velocity.y);   // con enemy di chuyển
            }
        }
        public void Die()
        {
            // Debug.Log("Die");

            if(IsComponentsNull() || m_isDead) return;

            m_isDead = true;
            m_amin.SetTrigger(Const.DEAD_ANIM);
            m_rb.velocity = Vector2.zero;
            gameObject.layer = LayerMask.NameToLayer(Const.DEAD_ANIM);
            if(m_gm)
                m_gm.Score++;
            Destroy(gameObject,2f);
        }
    }
}

