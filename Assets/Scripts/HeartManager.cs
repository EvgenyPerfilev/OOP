using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartManager : MonoBehaviour {

    public Image[] hearts;
    public Sprite fullHeart; // спрайт полного сердечка
    public Sprite halfFullHeart; // спрайт наполовину полного сердечка
    public Sprite emptyHeart; // спрайт пустого сердечка
    public FloatValue heartContainers;
    public FloatValue playerCurrentHealth; //текущее состояние здоровье игрока


    // Use this for initialization
    void Start () {
        InitHearts();
	}

    public void InitHearts()
    {
        for (int i = 0; i < heartContainers.initialValue; i++)
        {
            hearts[i].gameObject.SetActive(true);
            hearts[i].sprite = fullHeart;
        }
    }

    public void UpdateHearts()
    {
        float tempHealth = playerCurrentHealth.RuntimeValue / 2; //делим на 2, так как половину сердце считаем за 1 очко здоровья
        for (int i = 0; i < heartContainers.initialValue; i++)
        {
            if(i <= tempHealth-1)
            {
                hearts[i].sprite = fullHeart;
            }else if(i >= tempHealth)
            {
                hearts[i].sprite = emptyHeart;
            }else{
                hearts[i].sprite = halfFullHeart;
            }
        }
    }
}
