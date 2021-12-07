using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

[RequireComponent(typeof(NavMeshAgent))]
public class CharactorMoving : MonoBehaviour
{
    [SerializeField] private UnityEvent _run;
    [SerializeField] private UnityEvent _idle;

    private MovePointsContainer _pointsContainer;
    private NavMeshAgent _agent;
    private Transform _currentPoint;
    private bool _isRunnig = false;
    private bool _isFinish = false;
    private int _currentPlatformID = 1;
    private LevelInitializator _initializator;
    private CharactorShooting _shooting;

    private void Awake()
    {
        _shooting = GetComponent<CharactorShooting>();
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (_isRunnig == true)
        {
            if (Vector3.Distance(_agent.destination, transform.position) == 0)
            {
                if (_isFinish == true)
                {
                    _initializator.ActivateFinishScene();
                }
                else
                {
                    _shooting.CanShoot(true);
                    LookForward();
                    _idle?.Invoke();
                }
                
                _isRunnig = false;
            }
        }
    }

    private void LookForward()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    private void MoveCharector()
    {
        _run?.Invoke();
        _isRunnig = true;
        _shooting.CanShoot(false);
    }

    public void Init(MovePointsContainer pointContainer, LevelInitializator initializator)
    {
        _initializator = initializator;
        _pointsContainer = pointContainer;
    }

    public void MoveToNextPoint(int platformID)
    {
        _currentPoint = _pointsContainer.GetNextMovePoint();

        if (_currentPoint == null)
        {
            _isFinish = true;
            MoveCharector();
        }
        else
        {
            if (_currentPlatformID == platformID)
            {
                MoveCharector();
                _agent.SetDestination(_currentPoint.position);
                _currentPlatformID++;
                _initializator.GetCurrentPlatform(_currentPlatformID).CheckCurrentEnemiesCount();
            }
        }
    }
}