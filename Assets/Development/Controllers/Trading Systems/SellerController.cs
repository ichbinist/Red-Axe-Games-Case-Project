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
    
    [FoldoutGroup("Seller Data")]
    [ReadOnly]
    public SellerInteractionData SellerInteractionData = new SellerInteractionData();
    
    [FoldoutGroup("References")]
    [ReadOnly]
    public CharacterSettings CharacterSettings;

    [FoldoutGroup("Car Value Settings")]
    [ReadOnly]
    public float BaseCarValue = 0f;

    [FoldoutGroup("Car Value Settings")]
    [ReadOnly]
    public float AdjustedCarValue = 0f;

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
        
        BaseCarValue = localValue;
        
        CarModifyingController.Initialize();

        localValue -= Mathf.Abs(CarModifyingController.GetDamagePercent() * FactorManager.Instance.Valuation_Damaged);
        localValue -= Mathf.Abs(CarModifyingController.GetPaintPercent() * FactorManager.Instance.Valuation_Painted);
        localValue += Mathf.Abs(CarModifyingController.GetUpgradePercent() * FactorManager.Instance.Valuation_Upgraded);

        AdjustedCarValue = localValue;

        //Filling the Data
        SellerInteractionData.Seller = this;
        SellerInteractionData.CarName = CarSettings.CarName;
        SellerInteractionData.MaxSpeed = CarSettings.CarController.maxspeed;
        SellerInteractionData.MaxTorque = CarSettings.CarController.maxEngineTorque;
        SellerInteractionData.BrakeTorque = CarSettings.CarController.brakeTorque;
        SellerInteractionData.MotorUpgradeLevel = Mathf.RoundToInt(CarModifyingController.SimplifiedCarStatistics.UpgradePercentage);
        SellerInteractionData.DamagePercent = CarModifyingController.GetDamagePercent();
        SellerInteractionData.PaintedPercent = CarModifyingController.GetPaintPercent();
        SellerInteractionData.BaseValue = BaseCarValue;
        SellerInteractionData.ModifiedValue = AdjustedCarValue;
    }

    public void SellerTradeInteraction()
    {
        SellerManager.Instance.OnSellerInteractionStarted.Invoke(SellerInteractionData);
    }
}

[System.Serializable]
public class SellerInteractionData
{
    [ReadOnly]
    public SellerController Seller;
    //-------------CAR INFO----------------
    [FoldoutGroup("General Info")]
    [ReadOnly]
    public string CarName;
    [FoldoutGroup("General Info")]
    [ReadOnly]
    public string CarProducer = "WMB Motors";

    [FoldoutGroup("Motor Info")]
    [ReadOnly]
    public float MaxSpeed;
    [FoldoutGroup("Motor Info")]
    [ReadOnly]
    public float MaxTorque;
    [FoldoutGroup("Motor Info")]
    [ReadOnly]
    public float BrakeTorque;

    [FoldoutGroup("Motor Info")]
    [ReadOnly]
    public int MotorUpgradeLevel;

    [FoldoutGroup("Damage Info")]
    [ReadOnly]
    public float DamagePercent;
    [FoldoutGroup("Damage Info")]
    [ReadOnly]
    public float PaintedPercent;
    //-------------------------------------

    //-------------VALUE INFO--------------
    [FoldoutGroup("Value Info")]
    [ReadOnly]
    public float BaseValue;
    [FoldoutGroup("Value Info")]
    [ReadOnly]
    public float ModifiedValue;
    //-------------------------------------
}