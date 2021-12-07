using Cinemachine;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(MovePointsContainer))]
public class LevelInitializator : MonoBehaviour
{
    [SerializeField] private Charactor _charactorPrefab;
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private Gun _gun;
    [SerializeField] private CinemachineVirtualCamera _camera;
    [SerializeField] private BulletPool _bulletPool;
    [SerializeField] private Platform[] _platforms;
    [SerializeField] private VictoryUI _victoryUI;

    private Charactor _createdCharactor;
    private MovePointsContainer _pointsContainer;
    private CharactorMoving _moving;
    private CharactorShooting _shooting;

    private void Start()
    {
        _pointsContainer = GetComponent<MovePointsContainer>();
        CreateCharactor();
        InitCharactor();
        InitPlatforms();
    }

    private void CreateCharactor()
    {
        Transform spawnPoint = _pointsContainer.GetNextMovePoint();
        _createdCharactor = Instantiate(_charactorPrefab, spawnPoint.transform.position, Quaternion.identity);
        SetCameraTarget(_createdCharactor);
    }

    private void SetCameraTarget(Charactor charactor)
    {
        _camera.Follow = charactor.transform;
        _camera.LookAt = charactor.transform;
    }


    private void InitCharactor()
    {
        _moving = _createdCharactor.GetComponent<CharactorMoving>();
        _shooting = _createdCharactor.GetComponent<CharactorShooting>();
        _moving.Init(_pointsContainer, this);
        _shooting.Init(_gun, _bulletPool);
    }

    private void InitPlatforms()
    {
        for (int i = 0; i < _platforms.Length; i++)
        {
            _platforms[i].Init(i + 1, _enemyPrefab, _moving);
        }
    }

    public Platform GetCurrentPlatform(int i)
    {
        return _platforms[i - 1];
    }

    public void ActivateFinishScene()
    {
        _createdCharactor.transform.DORotate(new Vector3(0, -180, 0), 0.5f);
        _createdCharactor.GetComponent<Animator>().SetTrigger("Win");
        _victoryUI.gameObject.SetActive(true);
    }
}