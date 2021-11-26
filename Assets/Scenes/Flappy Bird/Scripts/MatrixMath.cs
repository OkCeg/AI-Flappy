using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//complete
public class MatrixMath : MonoBehaviour
{
    public static float[,] MatrixMultiplication(float[,] first, float[,] second)
    {
        float[,] matrix = new float[0, 0];
        if (first.GetLength(1) == second.GetLength(0))
        {
            matrix = new float[first.GetLength(0), second.GetLength(1)];
        }
        else
        {
            Debug.Log("Impossible matrix multiplication.");
            return matrix;
        }
        for (int i = 0; i < first.GetLength(0); i++)
        {
            for (int j = 0; j < second.GetLength(1); j++)
            {
                for (int x = 0; x < first.GetLength(1); x++)
                {
                    matrix[i, j] += first[i, x] * second[x, j];
                }
            }
        }
        return matrix;
    }

    public static float[,] MatrixAddition(float[,] first, float[,] second)
    {
        float[,] matrix = new float[0, 0];
        if (first.GetLength(0) == second.GetLength(0) && first.GetLength(1) == second.GetLength(1))
        {
            matrix = new float[first.GetLength(0), first.GetLength(1)];
        }
        else
        {
            Debug.Log("Impossible matrix addition.");
            return matrix;
        }
        for (int i = 0; i < first.GetLength(0); i++)
        {
            for (int j = 0; j < first.GetLength(1); j++)
            {
                matrix[i, j] = first[i, j] + second[i, j];
            }
        }
        return matrix;
    }

    public static float[,] MatrixSubtraction(float[,] first, float[,] second)
    {
        float[,] matrix = new float[0, 0];
        if (first.GetLength(0) == second.GetLength(0) && first.GetLength(1) == second.GetLength(1))
        {
            matrix = new float[first.GetLength(0), first.GetLength(1)];
        }
        else
        {
            Debug.Log("Impossible matrix subtraction.");
            return matrix;
        }
        for (int i = 0; i < first.GetLength(0); i++)
        {
            for (int j = 0; j < first.GetLength(1); j++)
            {
                matrix[i, j] = first[i, j] - second[i, j];
            }
        }
        return matrix;
    }

    //debug purposes
    public static void ReadMatrix(float[,] matrix)
    {
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                Debug.Log(matrix[i, j]);
            }
        }
    }
}
