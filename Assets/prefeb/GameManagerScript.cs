using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerScript: MonoBehaviour
{

    public GameObject obj;
    private int i;
    private int j;
    public Vector3 mousePosition;
    private Camera cam;
    public GameObject obj_side;

    public GameObject objFX;
    public GameObject objEffect;
    private GameObject target;

    int randomNum;
    int randomNum1;
    int count = 0;
    public int score = 50;

    
    public AudioSource audioSource;
    public Text scoreText;

    // Start is called before the first frame update
    void Start()
    {
        randomNum = Random.Range(1, 10);
        randomNum1 = Random.Range(1, 10);
        while (randomNum1 == randomNum)
        {
            randomNum1 = Random.Range(1, 10);
            if (randomNum1 != randomNum)
                break;
        }
        
        Debug.Log(randomNum);
        float dis = 3.15f;

        for(i=0; i<3; i++)
        {
            for(j=0; j<3; j++)
            {
                count += 1;
                if(randomNum == count || randomNum1 == count)  
                {
                    Instantiate(obj_side, new Vector3(i * dis - dis, j * dis - dis, 0), Quaternion.identity);

                }
                else
                {

                    Instantiate(obj, new Vector3(i * dis - dis, j * dis - dis, 0), Quaternion.identity);
                }
            }
        }
        cam = Camera.main;

        

    }

    // Update is called once per frame
    void Update()
    {
        mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);

        if(Input.GetMouseButtonDown(0))
        {
            CastRay();
            Debug.Log("Click");
            Debug.Log("Position X :" + mousePosition.x);
            Debug.Log("Position Y :" + mousePosition.y);
        }


        
    }




    void CastRay()
    {
        target = null;
        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero, 0f);
        
        if(hit.collider !=null)
        {
            Debug.Log(hit.collider.name);//히트 된 게임 오브젝트를 타겟으로 지정


            target = hit.collider.gameObject;//타겟으로 지정

            Destroy(target); //타겟 삭제
            Instantiate(objEffect, new Vector3(pos.x,pos.y, 0), Quaternion.identity);

            if (hit.collider.name == "hammer(Clone)")
            {
                Debug.Log("클릭성공!");
                score += 100;


                //Instantiate(objFX, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
            }
            else
            {
                Debug.Log("잘못 클릭했습니다");
                score -= 10;
            }

            scoreText.text = score.ToString();

            Debug.Log(score);
        }
    }
}
