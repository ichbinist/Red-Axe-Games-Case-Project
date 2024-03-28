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

    [FoldoutGroup("Car Statistics Factors")]
    public float DamagedCarPossibility = 10f;
    [FoldoutGroup("Car Statistics Factors")]
    public float PaintedCarPossibility = 10f;
    [FoldoutGroup("Car Statistics Factors")]
    public float UpgradedCarPossibility = 10f;

    private void OnEnable()
    {
        SimplifiedCarStatistics = new SimplifiedCarStatistics();
        StartCoroutine(InitializationCoroutine());
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
        if(percentage < PaintedCarPossibility)
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
        if (percentage < DamagedCarPossibility)
        {
            int index = Random.Range(0, Car.damage.meshFilters.Length);
            RaycastHit hit = new RaycastHit();
            hit.point = Car.damage.meshFilters[index].gameObject.transform.position;
            Car.damage.Initialize(Car);
            Car.damage.OnCollisionWithRay(hit, 50000);
            Car.damage.UpdateDamage();

            SimplifiedCarStatistics.DamagePercentage = Mathf.Clamp(SimplifiedCarStatistics.DamagePercentage + 7.5f, 0f, SimplifiedCarStatistics.MaxDamagePercentage);

            RecursiveDamage();
        }
    }

    private void RecursiveUpgrade()
    {
        float percentage = Random.Range(0f, 100f);
        if (percentage < UpgradedCarPossibility)
        {
            Car.maxEngineTorque += Car.maxEngineTorque * 0.15f;
            SimplifiedCarStatistics.UpgradePercentage = Mathf.Clamp(SimplifiedCarStatistics.UpgradePercentage + 1f, 0f, SimplifiedCarStatistics.MaxUpgradedPercentage);
            RecursiveUpgrade();
        }
    }

    private IEnumerator InitializationCoroutine()
    {
        yield return new WaitForSeconds(0.05f);

        SimplifiedCarStatistics.MaxDamagePercentage = Car.damage.meshFilters.Length * 10f;
        SimplifiedCarStatistics.MaxPaintedPercentage = Car.damage.meshFilters.Length * 10f;
        SimplifiedCarStatistics.MaxUpgradedPercentage = 5f;
        Damage();
        Paint();
        Upgrade();
    }
}

[System.Serializable]
public class SimplifiedCarStatistics
{
    public float DamagePercentage;
    public float PaintPercentage;
    public float UpgradePercentage;

    //Upgrade Values

    public int MotorUpgrade;

    [FoldoutGroup("Car Statistics Data")]
    [ReadOnly]
    public float MaxDamagePercentage;

    [FoldoutGroup("Car Statistics Data")]
    [ReadOnly]
    public float MaxPaintedPercentage;

    [FoldoutGroup("Car Statistics Data")]
    [ReadOnly]
    public float MaxUpgradedPercentage = 5f;
}