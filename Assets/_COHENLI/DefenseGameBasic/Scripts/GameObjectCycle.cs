using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class GameObjectCycle : MonoBehaviour
{
    public Transform myTransform;
    public SpriteRenderer sp;
    public GameObjectCycle demoScript;
    public GameObject heroPrefab;
    public float timeScaleValue = 1f;
    float angle = 0;
    float score;
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
       // Debug.Log("Awake");
       sp  =  GetComponent<SpriteRenderer>();
    }
    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    void OnEnable()
    {
        // Debug.Log("OnEnable");
    }
    // Start is called before the first frame update
    void Start()
    {
        // Debug.Log("Start");
        if(sp)
        {
            sp.color = Color.red;
        }
        if(heroPrefab)
        {
            var heroClone = Instantiate(heroPrefab, new Vector3(3.5f, 1.5f, 0f), quaternion.identity);

            Destroy(heroClone, 5f);
        }

        // StartCoroutine("DemoCo");   // cách 1
        StartCoroutine(DemoCo());   // cách 2
        Invoke("Work", 3);

        // score += 10;
        // PlayerPrefs.SetFloat("score", score);
        // float scoreCopy = PlayerPrefs.GetFloat("score", 0);
        // Debug.Log(scoreCopy);

        score = PlayerPrefs.GetFloat("score",0);
        score += 10;
        PlayerPrefs.SetFloat("score", score);
        Debug.Log(score);
    }

    private IEnumerator DemoCo()
    {
        yield return new WaitForSeconds(3);
        Debug.Log("Đang xử lý công việc 1");
        yield return new WaitForSeconds(2);
        Debug.Log("Đang xử lý công việc 2");
    }
    private void Work()
    {
         Debug.Log("Công việc cần thực hiện");
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log("Update");
        // Debug.Log(Time.deltaTime);
        // var vectorDemo = new Vector2(1.3f, 4);
        // myTransform.position = new Vector3(2, 1, 0);            // vị trí
        // myTransform.localScale = new Vector3(3, 3, 0);          // độ giản nở

        Time.timeScale = timeScaleValue;
        angle += 1000 * Time.deltaTime;
        if(myTransform)
        {
            myTransform.localRotation = Quaternion.Euler(0f, 0f, angle);
        }

    }
    /// <summary>
    /// This function is called when the behaviour becomes disabled or inactive.
    /// </summary>
    void OnDisable()
    {
        // Debug.Log("OnDisable");
    }
    /// <summary>
    /// This function is called when the MonoBehaviour will be destroyed.
    /// </summary>
    void OnDestroy()
    {
        // Debug.Log("OnDestroy");
    }

    // void OnCollisionEnter2D(Collision2D collision)
    // {
    //     Debug.Log(collision.gameObject.tag);
    //     Debug.Log(collision.gameObject.GetComponent<SpriteRenderer>().color= Color.blue);
    //     Debug.Log("Đã va chạm với nhau");
    // }
    // void OnCollisionStay2D(Collision2D other)
    // {
    //     Debug.Log("2 đối tượng game đang va chạm với nhau");
    // }
    // void OnCollisionExit2D(Collision2D other)
    // {
    //     Debug.Log("2 đối tượng game ko còn va chạm với nhau");
    // }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Đã va chạm với nhau");
    }
    void OnTriggerStay2D(Collider2D other)
    {
        Debug.Log("2 đối tượng game đang va chạm với nhau");
    }
    void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("2 đối tượng game ko còn va chạm với nhau");
    }
}
