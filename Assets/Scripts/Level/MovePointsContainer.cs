using System;
using UnityEngine;

public class MovePointsContainer : MonoBehaviour
{
    private MovePoint[] _movePoints;
    private int _currentPointId = -1;
    

    private void Awake()
    {
        _movePoints = GetComponentsInChildren<MovePoint>();

        if (_movePoints.Length < 2)
            throw new ArgumentOutOfRangeException();
    }

    public Transform GetNextMovePoint()
    {
        if (_currentPointId == _movePoints.Length - 1)
        {
            return null;
        }

        _currentPointId++;
        Transform nextPoint = _movePoints[_currentPointId].transform;

        return nextPoint;
    }
}