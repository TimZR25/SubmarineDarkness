using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Points : MonoBehaviour
{
    [Header("PointSettings")]
    [SerializeField] private int _length;
    [SerializeField] private int _wide;

    private static int[] _pointsX;
    private static int[] _pointsY;

    public static Vector3 GetRandomPoint => new Vector3(_pointsX[Random.Range(0, _pointsX.Length)], _pointsY[Random.Range(0, _pointsY.Length)], 0);

    private void Awake()
    {
        SetPoints();
    }

    private void SetPoints()
    {
        _pointsX = new int[_length];
        _pointsY = new int[_wide];

        for (int i = 0; i < _length; i++)
        {
            _pointsX[i] = -_length / 2 + i;
        }

        for (int i = 0; i < _wide; i++)
        {
            _pointsY[i] = -_wide / 2 + i;
        }
    }
}
