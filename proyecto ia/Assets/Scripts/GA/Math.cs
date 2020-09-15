using System;
using UnityEngine;

public static class GAMath
{
    public static float EvaluatePolynomial(float[] coefficients, float x)
    {
        float result = 0.0f;
        for (int i = 0; i < coefficients.Length; ++i)
        {
            result += coefficients[i] * Mathf.Pow(x, coefficients.Length - i - 1);
        }

        return result;
    }

    public static float SigmoidFunction(float x)
    {
        if (x < -45.0f)
            return 0.0f;
        else if (x > 45.0f)
            return 1.0f;
        else
            return 1.0f / (1.0f + Mathf.Exp(-x));

    }
}
