using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SellerController : MonoBehaviour
{
    [TabGroup("References")]
    public List<RCC_CarControllerV3> RCC_Cars = new List<RCC_CarControllerV3>();
    [TabGroup("References")]
    public List<GameObject> Characters = new List<GameObject>();

    [TabGroup("Spawning Settings")]
    public Vector3 CarSpawnLocation;
    [TabGroup("Spawning Settings")]
    public Vector3 CharacterSpawnLocation;

    [Space(15)]

    [TabGroup("Spawning Settings")]
    public Quaternion CarRotation;
    [TabGroup("Spawning Settings")]
    public Quaternion CharacterRotation;

    [Space(15)]

    [TabGroup("Spawning Settings")]
    [Tooltip("Check True if you want to spawn your objects according to the global position instead of local position of Seller object. NOT IMPLEMENTED")]
    [ReadOnly]
    public bool IsPositionGlobal = false;

    private void OnEnable()
    {
        if (!IsPositionGlobal)
        {
            //Spawn car and Character according to the local position.
            SpawnCar();
            SpawnCharacter();
        }
        else
        {
            //Spawn car and Character According to the global position
        }
    }

    private void SpawnCar()
    {
        if (RCC_Cars.Count == 0)
        {
            Debug.LogWarning("No RCC Cars assigned to spawn.");
            return;
        }

        int randomCarIndex = Random.Range(0, RCC_Cars.Count);

        RCC_CarControllerV3 carInstance = Instantiate(RCC_Cars[randomCarIndex], transform);

        carInstance.transform.localPosition = CarSpawnLocation;
        carInstance.transform.localRotation = CarRotation;

        carInstance.SetEngine(false);
        carInstance.canControl = false;
    }

    private void SpawnCharacter()
    {
        if (Characters.Count == 0)
        {
            Debug.LogWarning("No Characters assigned to spawn.");
            return;
        }

        int randomCharacterIndex = Random.Range(0, Characters.Count);

        GameObject characterInstance = Instantiate(Characters[randomCharacterIndex], transform);

        characterInstance.transform.localPosition = CharacterSpawnLocation;
        characterInstance.transform.localRotation = CharacterRotation;

    }
}
