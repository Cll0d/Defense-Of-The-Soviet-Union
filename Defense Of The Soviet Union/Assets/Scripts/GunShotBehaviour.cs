using System.Collections;
using UnityEngine;

public class GunShotBehaviour : MonoBehaviour
{
    [SerializeField] private float _rotSpeed;
    [SerializeField] private Transform _transform;
    [SerializeField] private float _range;
    [SerializeField] private float _damage;
    private bool _isRotForwfard = true;
    private Quaternion initialRotation;
    private Quaternion targetRotation;

    private void Start()
    {
        initialRotation = _transform.rotation;
        targetRotation = Quaternion.Euler(initialRotation.eulerAngles.x, initialRotation.eulerAngles.y + 15f, initialRotation.eulerAngles.z);
        StartCoroutine(DrawRay1());
    }

    private void Update()
    {
        Rotate();
    }

    private void Rotate()
    {
        float rotationStep = _rotSpeed * Time.deltaTime;
        _transform.rotation = Quaternion.RotateTowards(_transform.rotation, targetRotation, rotationStep);

        float angleDifference = Quaternion.Angle(_transform.rotation, targetRotation);
        if (angleDifference < 0.01f)
        {
            _isRotForwfard = !_isRotForwfard;
            targetRotation = Quaternion.Euler(initialRotation.eulerAngles.x, initialRotation.eulerAngles.y + (_isRotForwfard ? 20f : -20f), initialRotation.eulerAngles.z);
        }
    }



    private IEnumerator DrawRay1()
    {
        yield return new WaitForSeconds(0.1f);
        Ray ray = new Ray(_transform.position, -_transform.up);
        Debug.DrawRay(_transform.position, -_transform.up * _range, Color.yellow);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Enemy enemy = hit.collider.gameObject.GetComponent<Enemy>();
            if (enemy)
            {

                enemy.TakeDamage(_damage);
                Debug.Log("enemy");
            }
        }
        StartCoroutine(DrawRay1());
    }

    private void DrawRay()
    {
        Ray ray = new Ray(_transform.position, -_transform.up);
        Debug.DrawRay(_transform.position, -_transform.up * _range, Color.yellow);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Enemy enemy = hit.collider.gameObject.GetComponent<Enemy>();
            if (enemy)
            {
                enemy.TakeDamage(_damage);
                Debug.Log("enemy");
            }
        }
    }




    // [SerializeField] private int _numberOfRays;
    // [SerializeField] private float _rayDistance;
    // [SerializeField] private Transform _transform;
    // [SerializeField] private float _rotSpeed;
    // private LineRenderer _lineRenderer;

    // private void Start()
    // {
    //     _lineRenderer = GetComponent<LineRenderer>();
    //     _lineRenderer.positionCount = _numberOfRays;

    // }
    // private void Update()
    // {
    //     DrawRay();
    // }
    //private void DrawRay()
    // {
    //     Vector3 startPoint = transform.position;

    //     for(int i = 0; i < _numberOfRays; i++)
    //     {
    //         float angle = i * 360f / _numberOfRays;
    //         Quaternion rotation = Quaternion.Euler(0f, angle, 0f);

    //         Vector3 direction = rotation * transform.right;

    //         RaycastHit hit;
    //         if(Physics.Raycast(startPoint,direction, out hit, _rayDistance))
    //         {
    //             _lineRenderer.SetPosition(i, hit.point);
    //         }
    //         else
    //         {
    //             _lineRenderer.SetPosition(i, startPoint + direction * _rayDistance);
    //         }
    //     }
    // }
}
