using UnityEngine;

public class EnemyCount : MonoBehaviour
{
    GameObject[] enemyObjects;
    GameObject[] enemyObjects2;
    GameObject[] enemyObjects3;
    int enemyNum;

    public Animation[] anim;
    int animClip;

    public GameObject FireBall1;
    public GameObject FireBall2;
    public GameObject FireBall3;
    public GameObject FireBall4;

    public Animation blade1;
    public Animation blade2;

    public Animation spike1;
    public Animation spike2;
    public Animation spike3;
    public Animation spike4;

    //�@�z���g�͌�������ڎw������������͖���
    bool door1 = false;
    bool door2 = false;
    bool door3 = false;
    bool door4 = false;

    void Start()
    {
        enemyObjects = GameObject.FindGameObjectsWithTag("Enemy1");
        enemyNum = enemyObjects.Length;
        enemyObjects2 = GameObject.FindGameObjectsWithTag("Enemy2");
        enemyNum = enemyObjects2.Length;
        enemyObjects3 = GameObject.FindGameObjectsWithTag("Enemy3");
        enemyNum = enemyObjects3.Length;

        animClip = anim.Length;
    }

    private void Update()
    {
        //EnemyLengthController();

        if (enemyObjects.Length == 2 && !door1)
        {
            door1 = true;
            anim[0].Play();
            anim[1].Play();
        }
        else if (enemyObjects2.Length == 2 && !door2)
        {
            door2 = true;
            anim[2].Play();
            anim[3].Play();
        }
        else if (enemyObjects3.Length == 0 && !door3)
        {
            door3 = true;
            anim[4].Play();
            anim[5].Play();
        }
        else if (enemyObjects.Length == 0 && enemyObjects2.Length == 0 && !door4)
        {
            door4 = true;
            anim[6].Play();
        }
        enemyObjects = GameObject.FindGameObjectsWithTag("Enemy1");
        enemyNum = enemyObjects.Length;
        enemyObjects2 = GameObject.FindGameObjectsWithTag("Enemy2");
        enemyNum = enemyObjects2.Length;
        enemyObjects3 = GameObject.FindGameObjectsWithTag("Enemy3");
        enemyNum = enemyObjects3.Length;
    }

    private void EnemyLengthController()
    {
        if (enemyObjects.Length == 11)
        {

        }
        else if (enemyObjects.Length == 7)
        {

        }
        else if (enemyObjects.Length == 4)
        {

        }
        else if(enemyObjects.Length == 0)
        {

        }
    }
}
