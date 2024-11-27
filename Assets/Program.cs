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

    public void TakeDamage(int damage)   //not ignoring negatives when it should - fix this
    {
        if(damage < 0)
        {
            Debug.LogWarning("Damage value cannot be negative.");
            return;
        }

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
        //check if input is negative
        if(hp < 0)
        {
            Debug.LogWarning("Cannot heal with a negative amount.");
            return;
        }
        
        health += hp;  // add healing amount to health

        //clamp health to the max value of 100
        if (health > 100)
            health = 100;

        UpdateHealthStatus();
    }

    public void RegenerateShield(int hp)  
    {
        //check if input is negatvie
        if(hp < 0)
        {
            Debug.LogWarning("Cannot regenerate shield with a negative value.");
            return;
        }     

        shield += hp; // add regeneration value to shield

        //clamp shield to the max value of 100
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

    public static void RunAllUnitTests()
    {
        Test_TakeDamage_OnlyShield();
        Test_TakeDamage_ShieldAndHealth();    
        Test_TakeDamage_OnlyHealth();    
        Test_TakeDamage_ReduceHealthToZero();    
        Test_TakeDamage_ShieldAndHealthToZero();     
        Test_TakeDamage_NegativeInput();      
        Test_Heal_Normal();      
        Test_Heal_NegativeInput();      
        Test_RegenerateShield_Normal();       
        Test_RegenerateShield_AtMaxShield();
        Test_RegenerateShield_NegativeInput();       
        Test_Revive();       
    }

    public static void Test_TakeDamage_OnlyShield()
    {
        var healthSystem = new HealthSystem();
        healthSystem.shield = 100;
        healthSystem.health = 100;
        healthSystem.lives = 3;

        healthSystem.TakeDamage(20);

        Debug.Assert(80 == healthSystem.shield, "Shield should reduce to 80");
        Debug.Assert(100 == healthSystem.health, "Health should remain at 100");
        Debug.Assert(3 == healthSystem.lives, "Lives should remain at 3");
    }

    public static void Test_TakeDamage_ShieldAndHealth()
    {
        var healthSystem = new HealthSystem();
        healthSystem.shield = 50;
        healthSystem.health = 100;
        healthSystem.lives = 3;

        healthSystem.TakeDamage(60);

        Debug.Assert(0 == healthSystem.shield, "Shield should reduce to 0");
        Debug.Assert(90 == healthSystem.health, "Health should reduce to 90");
        Debug.Assert(3 == healthSystem.lives, "Lives should remain at 3");
    }

    public static void Test_TakeDamage_OnlyHealth()
    {
        var healthSystem = new HealthSystem();
        healthSystem.shield = 0;
        healthSystem.health = 100;
        healthSystem.lives = 3;

        healthSystem.TakeDamage(20);

        Debug.Assert(0 == healthSystem.shield, "Shield should remain at 0");
        Debug.Assert(80 == healthSystem.health, "Health should reduce to 80");
        Debug.Assert(3 == healthSystem.lives, "Lives should remain at 3");
    }

    public static void Test_TakeDamage_ReduceHealthToZero()
    {
        var healthSystem = new HealthSystem();
        healthSystem.shield = 0;
        healthSystem.health = 10;
        healthSystem.lives = 3;

        healthSystem.TakeDamage(20);

        Debug.Assert(0 == healthSystem.shield, "Shield should remain at 0");
        Debug.Assert(0 == healthSystem.health, "Health should reduce to 0");
        Debug.Assert(3 == healthSystem.lives, "Lives should remain at 3");
    }

    public static void Test_TakeDamage_ShieldAndHealthToZero()
    {
        var healthSystem = new HealthSystem();
        healthSystem.shield = 30;
        healthSystem.health = 20;
        healthSystem.lives = 3;

        healthSystem.TakeDamage(50);

        Debug.Assert(0 == healthSystem.shield, "Shield should reduce to 0");
        Debug.Assert(0 == healthSystem.health, "Health should reduce to 0");
        Debug.Assert(3 == healthSystem.lives, "Lives should remain at 3");
    }

    public static void Test_TakeDamage_NegativeInput()    //doesn't work properly due to damage method not ignoring negative values
    {
        var healthSystem = new HealthSystem();
        healthSystem.shield = 100;
        healthSystem.health = 100;
        healthSystem.lives = 3;

        healthSystem.TakeDamage(-10);

        Debug.Assert(100 == healthSystem.shield, "Shield should remain at 100");
        Debug.Assert(100 == healthSystem.health, "Health should remain at 100");
        Debug.Assert(3 == healthSystem.lives, "Lives should remain at 3");
    }



    public static void Test_Heal_Normal()
    {
        var healthSystem = new HealthSystem();
        healthSystem.health = 50;

        healthSystem.Heal(30);

        Debug.Assert(80 == healthSystem.health, "Health should increase to 80");
    }

    public static void Test_Heal_NegativeInput()
    {
        var healthSystem = new HealthSystem();
        healthSystem.health = 50;

        healthSystem.Heal(-20);

        Debug.Assert(50 == healthSystem.health, "Health should remain at 50");
    }



    public static void Test_RegenerateShield_Normal()
    {
        var healthSystem = new HealthSystem();
        healthSystem.shield = 50;

        healthSystem.RegenerateShield(30);

        Debug.Assert(80 == healthSystem.health, "Shield should increase to 80");
    }

    public static void Test_RegenerateShield_AtMaxShield()
    {
        var healthSystem = new HealthSystem();
        healthSystem.shield = 100;

        healthSystem.RegenerateShield(20);

        Debug.Assert(100 == healthSystem.health, "Shield should remain at 100");
    }

    public static void Test_RegenerateShield_NegativeInput()
    {
        var healthSystem = new HealthSystem();
        healthSystem.shield = 50;

        healthSystem.RegenerateShield(-20);

        Debug.Assert(50 == healthSystem.health, "Shield should remain at 50");
    }



    public static void Test_Revive()
    {
        var healthSystem = new HealthSystem();
        healthSystem.health = 10;
        healthSystem.shield = 0;
        healthSystem.lives = 3;

        healthSystem.Revive();

        Debug.Assert(100 == healthSystem.health, "Health should reset to 100");
        Debug.Assert(100 == healthSystem.shield, "Shield should reset to 100");
        Debug.Assert(2 == healthSystem.lives, "Lives should reduce to 2");
    }
}