using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    Slider healthSlider;
    public float health = 100;
    public static int players;
    private void Awake()
    {
        
        players++;
        if (players == 1)
        {
            healthSlider = GameObject.Find("SliderFill1").GetComponent<Slider>();
        }
        else
        {
            healthSlider = GameObject.Find("SliderFill2").GetComponent<Slider>();
        }
        
        healthSlider.maxValue = health;
        healthSlider.value = health;
    }
    public void Damage(float damage)
    {
        health -= damage;
        healthSlider.value = health;
    }
    private void Update()
    {
        if (health == 0 || health < 0)
        {
            print("へ（>_<へ)");

        }

    }
}
