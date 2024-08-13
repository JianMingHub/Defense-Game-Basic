using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace COHENLI.DefenseBasic
{
    public class Player : MonoBehaviour, IComponentChecking
    {
        private Animator m_amin;
        public float atkRate;
        private float m_curAtkRate;
        private bool m_isAttacked;
        private bool m_isDead;
        private GameManager m_gm;
        private void Awake() {
            m_amin = GetComponent<Animator>();
            m_curAtkRate = atkRate;
            m_gm = FindObjectOfType<GameManager>();
        }
        // Start is called before the first frame update
        void Start()
        {
            
        }
        public bool IsComponentsNull()
        {
            return m_amin == null || m_gm == null;
        }
        // Update is called once per frame
        void Update()
        {
            if(IsComponentsNull()) return;
            // Chỉ cho Player tấn công khi người dùng click chuột trái và m_isAttacked = false
            if(Input.GetMouseButtonDown(0) && !m_isAttacked)
            {
                // Debug.Log("Player clicked mouse button");
                m_amin.SetBool(Const.ATTACK_ANIM, true);
                m_isAttacked = true;    // sau khi tấn công xong thì chuyển sang true
            }
            // kiểm tra ko cho tấn công liên tục, chỉ tấn công sau khoảng thời gian trễ nhất định
            if(m_isAttacked)
            {
                m_curAtkRate -= Time.deltaTime;
                if(m_curAtkRate <= 0)
                {
                    m_isAttacked = false;
                    m_curAtkRate = atkRate;
                }
            }
        }
        // chuyển trạng thái sau khi tấn công xong, thì ko tấn công nữa (ngoài Unity add hàm này vào animation)
        public void ResetAtkAnim()
        {
            if(IsComponentsNull()) return;
            m_amin.SetBool(Const.ATTACK_ANIM, false);
        }
        public void PlayAtkSound()
        {
            if(m_gm.auCtr)
                m_gm.auCtr.PlaySound(m_gm.auCtr.playerAtk);
        }
        // bắt va chạm giữa player với vũ khí, nếu vũ khí va chạm thì player sẽ chết
        private void OnTriggerEnter2D(Collider2D col)
        {
            if(IsComponentsNull()) return;
            if(col.CompareTag(Const.ENEMY_WEAPON_TAG) && !m_isDead)
            {
                m_amin.SetTrigger(Const.DEAD_ANIM);
                m_isDead = true;
                gameObject.layer = LayerMask.NameToLayer(Const.DEAD_LAYER);             // change player to dead
                m_gm.GameOver();
            }
        }
    }
}