using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.UIElements.UxmlAttributeDescription;
//using UnityEngine.tvOS;

/* This is the Script that will randomly Spawn Prefabs in the Scene.
 * 
 * In my case, I will use it to randomly spawn coin Prefabs in the scene.
 * 
 * Source of most of this script: Alex Dev from Udemy from This tutorial: 
 * https://www.udemy.com/course/free-part2-alexdev/learn/lecture/39376574#overview . That comes from Alex Dev's 
 * "Basics of C# and Unity for Complete Beginners - Part 2" Udemy course.
 * 
 *  Instead of creating an array of sprites in the unity editor (which asks the user to put from a "formset" to put multiple sprites in an array), 
 *  I had to modify my script so that it asks for an array of Prefabs. I had to remove the "Prefab to spawn" variable, since the only thing that 
 *  I want is an array of prefabs, and I want to randomly spawn the selected prefab from that array of prefabs.
 *  
 *  Here’s a modified version of your `PrefabSpawner` script to work with an array of prefabs instead of sprites. This version removes the `prefabToSpawn` variable and replaces the `prefabSprite` array with a `prefabs` array. It randomly spawns a prefab from this array.
 *  
    ### Key Changes:
    1. * *Removed `Sprite[] prefabSprite` and `prefabToSpawn`:**
       -Replaced them with `GameObject[] prefabs` to allow multiple prefab options.

    2. **Random Prefab Selection:**
       -A random prefab is chosen from the `prefabs` array.

    3. **Spawn Area Defined by Collider:**
       -Used `BoxCollider2D` for defining the area where prefabs can spawn.

    4. **Error Handling:**
       -Added a warning if the `prefabs` array is empty.

    ### How to Use:
    1. **Attach Script:**Add this script to an empty GameObject in your scene.
    2. **Assign Prefabs:**
       -In the Inspector, add the prefabs you want to spawn into the `Prefabs` array.
    3. **Set Spawn Area:**
       -Assign a `BoxCollider2D` to the `Spawn Area` field to define where the prefabs should spawn.
    4. **Adjust Cooldown:**Set the `Cooldown` value for the time interval between spawns.
 *  
 *  
 *  
 */

public class PrefabSpawner : MonoBehaviour
{

    //[SerializeField] private Sprite[] prefabSprite;

    [SerializeField] private GameObject[] prefabs; // Array of prefabs to spawn

    // This gives a visual representation of the spawn area in the scene. This is where the prefabs will spawn in the scene.
    [SerializeField] private BoxCollider2D colliderObject;

    //[SerializeField] private GameObject prefabToSpawn;  // The Prefab to Spawn
    
    [SerializeField] private float cooldown;
    public float timer;

    // Update is called once per frame

    /* Update() function. This executes 60 times per second.
     * 
     * This will spawn the Prefab in the scene after a set amount of seconds have passed.
     * 
     * 
     */
    void Update()
    {
        // This executes the timer. Each prefab will be rendered after the timer reaches 0.
        timer -= Time.deltaTime;

        // If the timer reaches 0
        if (timer < 0)
        {
            // I will reset the timer to the cooldown time. This will prevent 60 prefabs to be spawned each second.
            timer = cooldown;

            // Spawn a random prefab
            SpawnPrefab();

            //// This will spawn the prefab in the scene.
            //GameObject newPrefab = Instantiate(prefabToSpawn);

            //float randomX = Random.Range(colliderObject.bounds.min.x, colliderObject.bounds.max.x);

            //// This will set the position of the newly created prefab to a random X position
            //newPrefab.transform.position = new Vector2(randomX, transform.position.y);

            //int randomIndex = Random.Range(0, prefabSprite.Length);

            //// This will set the sprite of the prefab to a random sprite from the prefab Sprite array.
            //newPrefab.GetComponent<SpriteRenderer>().sprite = prefabSprite[randomIndex];

            //Debug.Log("A new prefab has been spawned");
        }
    }

    // Function to spawn a random prefab
    private void SpawnPrefab()
    {
        if (prefabs.Length == 0)
        {
            Debug.LogWarning("No prefabs assigned to the spawner!");
            return;
        }

        // Select a random prefab from the array
        int randomIndex = Random.Range(0, prefabs.Length);
        GameObject selectedPrefab = prefabs[randomIndex];

        // Instantiate the prefab
        GameObject newPrefab = Instantiate(selectedPrefab);

        // Randomize the X position within the spawn area
        float randomX = Random.Range(colliderObject.bounds.min.x, colliderObject.bounds.max.x);

        // Set the prefab's position
        newPrefab.transform.position = new Vector2(randomX, transform.position.y);
    }
}
