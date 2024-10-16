using System;
using System.Diagnostics;
public class HealthSystem
{
    // Variables
    public int health;
    public string healthStatus;
    public int shield;
    public int lives;

    // Optional XP system variables
    public int xp;
    public int level;

    public HealthSystem()
    {
        ResetGame();
    }


    public string ShowHUD()
    {
        // Implement HUD display
        return $"Health: {health}, Shield: {shield}, Lives: {lives}, Status: {healthStatus}"; //XP: {xp}/100, Level: {level}";
    }

    public void TakeDamage(int damage)
    {
        // Implement damage logic
        if (shield >= damage)
        {
            shield -= damage; //All damage absorbed by the shield
        }
        else
        {
            int remainingDamage = damage - shield;
            shield = 0;
            health -= remainingDamage;

            if (health < 0)
                health = 0; //Health can't be less than 0
        }

        UpdateHealthStatus();
    }

    public void Heal(int hp)
    {
        // Implement healing logic
        health += hp;
        if (health > 100)
            health = 100;

        UpdateHealthStatus();
    }

    public void RegenerateShield(int hp)
    {
        // Implement shield regeneration logic
        shield += hp;
        if (shield > 100)
            shield = 100;
    }

    public void Revive()
    {
        // Implement revive logic
        if (lives > 0)
        {
            lives--;
            health = 100;
            shield = 100;
            UpdateHealthStatus();
        }
        else
        {
            Console.WriteLine("No lives left!");
        }
    }

    public void ResetGame()
    {
        // Reset all variables to default values
        health = 100;
        shield = 100;
        lives = 3;
        //xp = 0;
        //level = 1;
        UpdateHealthStatus();
    }

    private void UpdateHealthStatus()
    {
        if (health <= 10)
        {
            healthStatus = "Imminent Danger";
        }
        else if (health <= 50)
        {
            healthStatus = "Badly Hurt";
        }
        else if (health <= 75)
        {
            healthStatus = "Hurt";
        }
        else if (health <= 90)
        {
            healthStatus = "Healthy";
        }
        else if (health <= 100)
        {
            healthStatus = "Perfect Health";
        }
    }

    // Optional XP system methods
    public void IncreaseXP(int exp)
    {
        // Implement XP increase and level-up logic
        // xp += exp;

        // while(xp >= 100)
        // {
        //     xp -=100;
        //     if(level < 99)
        //     {
        //         level++; //Increase level to max of 99
        //         Console.WriteLine($"Level Up! You are now Level {level}.");
        //    }
        //    else
        //     {
        //         xp = 100; //keep XP at 100 if the level is maxed out at 99
        //         break;
        //     }
        // }
    }

    public void Test_TakeDamage_OnlyShield()
    {
        HealthSystem system = new HealthSystem();
        system.shield = 100;
        system.health = 100;
        system.lives = 3;

        system.TakeDamage(20);

        Debug.Assert(80 == system.shield, "Shield should reduce to 80");
        Debug.Assert(100 == system.health, "Health should remain at 100");
        Debug.Assert(3 == system.lives, "Lives should remain at 3");
    }

    public void Test_TakeDamage_ShieldAndHealth()
    {
        HealthSystem system = new HealthSystem();
        system.shield = 50;
        system.health = 100;
        system.lives = 3;

        system.TakeDamage(60);

        Debug.Assert(0 == system.shield, "Shield should reduce to 0");
        Debug.Assert(90 == system.health, "Health should reduce to 90");
        Debug.Assert(3 == system.lives, "Lives should remain at 3");
    }

    public void Test_TakeDamage_OnlyHealth()
    {
        HealthSystem system = new HealthSystem();
        system.shield = 0;
        system.health = 100;
        system.lives = 3;

        system.TakeDamage(20);

        Debug.Assert(0 == system.shield, "Shield should remain at 0");
        Debug.Assert(80 == system.health, "Health should reduce to 80");
        Debug.Assert(3 == system.lives, "Lives should remain at 3");
    }

    public void Test_TakeDamage_ReduceHealthToZero()
    {
        HealthSystem system = new HealthSystem();
        system.shield = 0;
        system.health = 10;
        system.lives = 3;

        system.TakeDamage(20);

        Debug.Assert(0 == system.shield, "Shield should remain at 0");
        Debug.Assert(0 == system.health, "Health should reduce to 0");
        Debug.Assert(3 == system.lives, "Lives should remain at 3");
    }

    public void Test_TakeDamage_ShieldAndHealthToZero()
    {
        HealthSystem system = new HealthSystem();
        system.shield = 30;
        system.health = 20;
        system.lives = 3;

        system.TakeDamage(50);

        Debug.Assert(0 == system.shield, "Shield should reduce to 0");
        Debug.Assert(0 == system.health, "Health should reduce to 0");
        Debug.Assert(3 == system.lives, "Lives should remain at 3");
    }

    public void Test_TakeDamage_NegativeInput()
    {
        HealthSystem system = new HealthSystem();
        system.shield = 100;
        system.health = 100;
        system.lives = 3;

        system.TakeDamage(-10);

        Debug.Assert(1000 == system.shield, "Shield should remain at 100");
        Debug.Assert(100 == system.health, "Health should remain at 100");
        Debug.Assert(3 == system.lives, "Lives should remain at 3");
    }



    public void Test_Heal_Normal()
    {
        HealthSystem system = new HealthSystem();
        system.health = 50;

        system.Heal(30);

        Debug.Assert(80 == system.health, "Health should increase to 80");
    }

    public void Test_Heal_NegativeInput()
    {
        HealthSystem system = new HealthSystem();
        system.health = 50;

        system.Heal(-20);

        Debug.Assert(50 == system.health, "Health should remain at 50");
    }



    public void Test_RegenerateShield_Normal()
    {
        HealthSystem system = new HealthSystem();
        system.shield = 50;

        system.RegenerateShield(30);

        Debug.Assert(80 == system.health, "Shield should increase to 80");
    }

    public void Test_RegenerateShield_AtMaxShield()
    {
        HealthSystem system = new HealthSystem();
        system.shield = 100;

        system.RegenerateShield(20);

        Debug.Assert(100 == system.health, "Shield should remain at 100");
    }

    public void Test_RegenerateShield_NegativeInput()
    {
        HealthSystem system = new HealthSystem();
        system.shield = 50;

        system.RegenerateShield(-20);

        Debug.Assert(50 == system.health, "Shield should remain at 50");
    }



    public void Test_Revive()
    {
        HealthSystem system = new HealthSystem();
        system.health = 10;
        system.shield = 0;
        system.lives = 3;

        system.Revive();

        Debug.Assert(100 == system.health, "Health should reset to 100");
        Debug.Assert(100 == system.shield, "Shield should reset to 100");
        Debug.Assert(2 == system.lives, "Lives should reduce to 2");
    }
}