using UnityEngine;

public class Health : MonoBehaviour
{
    public float health = 100;
    public void Damage(float damage)
    {
        health -= damage;
        print(health);
    }
    private void Update()
    {
        if (health == 0 || health < 0)
        {
            print("へ（>_<へ)");
        }
    }
}
