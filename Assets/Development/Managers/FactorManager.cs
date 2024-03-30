using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactorManager : Singleton<FactorManager>
{
    [FoldoutGroup("Probability Factors")]
    [MaxValue(80f)]
    public float Probability_Damaged;

    [FoldoutGroup("Probability Factors")]
    [MaxValue(80f)]
    public float Probability_Painted;

    [FoldoutGroup("Probability Factors")]
    [MaxValue(80f)] 
    public float Probability_Upgraded;

    [FoldoutGroup("Valuation Factors")]
    public float Valuation_Damaged;

    [FoldoutGroup("Valuation Factors")]
    public float Valuation_Painted;

    [FoldoutGroup("Valuation Factors")]
    public float Valuation_Upgraded;
}
