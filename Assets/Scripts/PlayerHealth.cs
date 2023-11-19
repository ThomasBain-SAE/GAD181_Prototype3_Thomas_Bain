using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxCollisions = 5; // Adjust this value to set the maximum allowed collisions
    private int collisionCount = 0;

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collision is with something other than the player
        if (collision.gameObject.tag != "Player")
        {
            // Increment the collision count
            collisionCount++;

            // Check if the player has exceeded the maximum allowed collisions
            if (collisionCount >= maxCollisions)
            {
                // Call a method to handle player destruction or any other desired action
                DestroyPlayer();
            }
        }
    }

    void DestroyPlayer()
    {
        // You can add any additional logic here before destroying the player
        // For example, you might want to play a death animation or trigger a game over state.

        // Destroy the player GameObject
        Destroy(gameObject);
    }
}
