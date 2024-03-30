using GuardAndGarden;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarModifyingController : MonoBehaviour
{
    private RCC_CarControllerV3 car;
    public RCC_CarControllerV3 Car { get { return (car == null) ? car = GetComponent<RCC_CarControllerV3>() : car; } }
    
    [ShowInInspector]
    [ReadOnly]
    public SimplifiedCarStatistics SimplifiedCarStatistics;

    public void Initialize()
    {
        SimplifiedCarStatistics = new SimplifiedCarStatistics();
        SimplifiedCarStatistics.MaxDamagePercentage = 100f;
        SimplifiedCarStatistics.MaxPaintedPercentage = 100f;
        SimplifiedCarStatistics.MaxUpgradedPercentage = 5f;
        CarCalculations();
    }

    public float GetDamagePercent()
    {
        return (SimplifiedCarStatistics.DamagePercentage / SimplifiedCarStatistics.MaxDamagePercentage) * 100f;
    }
    public float GetPaintPercent()
    {
        return (SimplifiedCarStatistics.PaintPercentage / SimplifiedCarStatistics.MaxPaintedPercentage) * 100f;
    }
    public float GetUpgradePercent()
    {
        return (SimplifiedCarStatistics.UpgradePercentage / SimplifiedCarStatistics.MaxUpgradedPercentage) * 100f;
    }

    [Button]
    public void Damage()
    {
        RecursiveDamage();
    }

    [Button]
    public void Paint()
    {
        RecursivePaint();
    }

    [Button]
    public void Upgrade()
    {
        RecursiveUpgrade();
    }

    private void RecursivePaint()
    {
        float percentage = Random.Range(0f, 100f);
        if(percentage < FactorManager.Instance.Probability_Painted)
        {
            int index = Random.Range(0, Car.damage.meshFilters.Length);

            MeshRenderer targetRenderer = Car.damage.meshFilters[index].GetComponent<MeshRenderer>();
            MaterialPropertyBlock materialPropertyBlock = new MaterialPropertyBlock();
            materialPropertyBlock.SetColor("_BaseColor", new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1f));
            targetRenderer.SetPropertyBlock(materialPropertyBlock);
            SimplifiedCarStatistics.PaintPercentage = Mathf.Clamp(SimplifiedCarStatistics.PaintPercentage + 10f, 0f, SimplifiedCarStatistics.MaxPaintedPercentage);
            RecursivePaint();
        }
    }

    private void RecursiveDamage()
    {
        float percentage = Random.Range(0f, 100f);
        if (percentage < FactorManager.Instance.Probability_Damaged)
        {
            int index = Random.Range(0, Car.damage.meshFilters.Length);
            RaycastHit hit = new RaycastHit();
            hit.point = Car.damage.meshFilters[index].gameObject.transform.position;
            Car.damage.Initialize(Car);
            Car.damage.OnCollisionWithRay(hit, 5000);
            Car.damage.UpdateDamage();

            SimplifiedCarStatistics.DamagePercentage = Mathf.Clamp(SimplifiedCarStatistics.DamagePercentage + 10f, 0f, SimplifiedCarStatistics.MaxDamagePercentage);

            RecursiveDamage();
        }
    }

    private void RecursiveUpgrade()
    {
        float percentage = Random.Range(0f, 100f);
        if (percentage < FactorManager.Instance.Probability_Upgraded)
        {
            Car.maxEngineTorque += Car.maxEngineTorque * 0.15f;
            SimplifiedCarStatistics.UpgradePercentage = Mathf.Clamp(SimplifiedCarStatistics.UpgradePercentage + 1f, 0f, SimplifiedCarStatistics.MaxUpgradedPercentage);
            RecursiveUpgrade();
        }
    }

    private void CarCalculations()
    {
        Damage();
        Paint();
        Upgrade();
    }

    private IEnumerator InitializationCoroutine()
    {
        yield return new WaitForSeconds(0.05f);
        CarCalculations();
    }
}

[System.Serializable]
public class SimplifiedCarStatistics
{
    [FoldoutGroup("Car Statistics Data")]
    [ReadOnly]
    public float DamagePercentage;
    [FoldoutGroup("Car Statistics Data")]
    [ReadOnly]
    public float PaintPercentage;
    [FoldoutGroup("Car Statistics Data")]
    [ReadOnly]
    public float UpgradePercentage;

    [FoldoutGroup("Car Statistics Data/Max")]
    [ReadOnly]
    public float MaxDamagePercentage;

    [FoldoutGroup("Car Statistics Data/Max")]
    [ReadOnly]
    public float MaxPaintedPercentage;

    [FoldoutGroup("Car Statistics Data/Max")]
    [ReadOnly]
    public float MaxUpgradedPercentage = 5f;
}