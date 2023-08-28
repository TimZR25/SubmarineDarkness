using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private ItemSpawner _seaMineSpawner;

    [SerializeField] private OxygenHandler _oxygenHandler;

    [SerializeField] private Manipulator _manipulator;

    [SerializeField] private AudioSource _audioSonar;

    [SerializeField] private float _speed;

    [SerializeField] private float _radiusSonar;

    private Rigidbody2D _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();

        InvokeRepeating(nameof(SearchMines), 0f, 0.01f);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _manipulator.Show();
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        float moveInputX = Input.GetAxisRaw(AxisNames.Horizontal);
        float moveInputY = Input.GetAxisRaw(AxisNames.Vertical);

        _rigidbody.velocity = new Vector2(moveInputX, moveInputY) * _speed;

        if (moveInputX == 0) return;

        transform.localScale = new Vector3(moveInputX, 1, 1);
    }

    private void Die()
    {
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        _oxygenHandler.OnDead += Die;
    }

    private void OnDisable()
    {
        _oxygenHandler.OnDead -= Die;
    }

    private void SearchMines()
    {
        foreach (Item seaMine in _seaMineSpawner.Items)
        {
            float distanceToMine = Vector3.Distance(transform.position, seaMine.transform.position);

            if (distanceToMine <= _radiusSonar)
            {
                _audioSonar.pitch = 1.5f;

                if (distanceToMine <= 1f)
                {
                    _oxygenHandler.Expode(seaMine);
                    seaMine.RemoveItem();
                }

                break;
            }

            _audioSonar.pitch = 1f;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _radiusSonar);
    }
}

public struct AxisNames
{
    public const string Vertical = "Vertical";
    public const string Horizontal = "Horizontal";
}
