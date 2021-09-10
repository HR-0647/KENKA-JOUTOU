using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public EnemyType type;
    public int attack;
    public int EnemyHP;//エネミーの体力
    public int defaultEnemyHP;
    public Slider Slider;

    private void Start()
    {
        Slider.value = 1;
        EnemyHP = defaultEnemyHP;
    }
}
public enum EnemyType
{
    Skeleton,
    
}