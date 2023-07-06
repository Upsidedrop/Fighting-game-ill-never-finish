using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField]
    Slider healthSlider;
    public float health = 100;
    private void Awake()
    {
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
