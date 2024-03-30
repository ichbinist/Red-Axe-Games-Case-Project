using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellerController : MonoBehaviour
{
    private CharacterInteraction characterInteraction;
    public CharacterInteraction CharacterInteraction { get { return (characterInteraction == null) ? characterInteraction = GetComponentInChildren<CharacterInteraction>(): characterInteraction; } }

    private CarInteraction carInteraction;
    public CarInteraction CarInteraction { get { return (carInteraction == null) ? carInteraction = GetComponentInChildren<CarInteraction>() : carInteraction; } }

    private CarSettings carSettings;
    public CarSettings CarSettings { get { return (carSettings == null) ? carSettings = GetComponentInChildren<CarSettings>() : carSettings; } }

    private CarModifyingController carModifyingController;
    public CarModifyingController CarModifyingController { get { return (carModifyingController == null) ? carModifyingController = GetComponentInChildren<CarModifyingController>() : carModifyingController; } }

    private SellerSpawnerController sellerSpawnerController;
    public SellerSpawnerController SellerSpawnerController { get { return (sellerSpawnerController == null) ? sellerSpawnerController = GetComponent<SellerSpawnerController>() : sellerSpawnerController; } }

    [FoldoutGroup("Car Value Settings")]
    [ReadOnly]
    public float CarValue = 0f;

    [FoldoutGroup("Car Value Settings")]
    public float CarDefaultValueCalculationFactor = 1f;

    private void OnEnable()
    {
        SellerSpawnerController.OnSpawnCompleted += InitializeCarValue;
        SellerSpawnerController.Initialize();
    }

    private void OnDisable()
    {
        SellerSpawnerController.OnSpawnCompleted -= InitializeCarValue;
    }

    private void InitializeCarValue()
    {
        float localValue = (CarSettings.CarController.maxspeed * CarDefaultValueCalculationFactor) + (CarSettings.CarController.maxEngineTorque * CarDefaultValueCalculationFactor * 0.5f) + (CarSettings.CarController.brakeTorque * CarDefaultValueCalculationFactor * 0.1f);
        
        CarModifyingController.Initialize();

        Debug.Log(gameObject.name + ": " + localValue);
        Debug.Log(gameObject.name + " Damage Percent: " + CarModifyingController.GetDamagePercent());
        Debug.Log(gameObject.name + " Painted Percent: " + CarModifyingController.GetPaintPercent());
        Debug.Log(gameObject.name + " Upgrade Percent: " + CarModifyingController.GetUpgradePercent());


        localValue -= Mathf.Abs(CarModifyingController.GetDamagePercent() * FactorManager.Instance.Valuation_Damaged);
        localValue -= Mathf.Abs(CarModifyingController.GetPaintPercent() * FactorManager.Instance.Valuation_Painted);
        localValue += Mathf.Abs(CarModifyingController.GetUpgradePercent() * FactorManager.Instance.Valuation_Upgraded);

        CarValue = localValue;
    }
}