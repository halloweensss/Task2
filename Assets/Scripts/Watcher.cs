using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Watcher : MonoBehaviour
{
    [SerializeField] private float _minDistance;
    [SerializeField] private float _smoothMovementSpeed;
    [SerializeField] private float _smoothRotationSpeed;
    [SerializeField] private Vector3 _basePosition;
    [SerializeField] private Vector3 _baseRotation;

    private Vector3 _mousePosition;
    private Camera _camera;
    private Transform _transform;
    private bool _isWatching;
    private Stack<ILookable> _lookables;

    public event Action OnClickTable;
    public event Action OnClickContainerWithout;
    public event Action<IContainer> OnClickContainer;
    public event Action<IModel> OnClickModel;
    public event Action OnClickBack;

    private void Start()
    {
        _isWatching = false;
        _camera = this.GetComponent<Camera>();
        _transform = this.transform;
        _lookables = new Stack<ILookable>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CheckLook();
        }

        if (Input.GetMouseButton(0) && _lookables.Count > 0)
        {
            RotationAround(_lookables.Peek().GetTransform().position);
        }

        if (_isWatching)
        {
            Transform target = _lookables.Peek().GetTransform();
            transform.LookAt(target);
            SmoothMovement(target.position, _minDistance);
        }
        else
        {
            transform.LookAt(null);
            SmoothMovement(_basePosition, 0);
            SmoothRotation(_baseRotation);
        }

        if (Input.GetMouseButtonDown(1))
        {
            Back();
        }
    }

    private void CheckLook()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        { 
            ILookable hitLookable = hit.transform.GetComponent<ILookable>();
            if(hitLookable != null)
                LookTo(hitLookable);
        }
    }

    public void LookTo(ILookable target)
    {
        if (_lookables.Count > 0)
        {

            IModel model = _lookables.Peek().GetTransform().GetComponent<IModel>();
            if (model != null)
            {
                _lookables.Peek().Hide();
                _lookables.Pop();
            }
        }
        else
        {
            if (OnClickTable != null) 
                OnClickTable.Invoke();
        }

        IContainer container = target.GetTransform().GetComponent<IContainer>();
        
        if (container != null)
        {
            while (_lookables.Count > 0)
            {
                _lookables.Peek().Hide();
                _lookables.Pop();
            }

            if (OnClickContainerWithout != null) 
                OnClickContainerWithout.Invoke();
            
            if (OnClickContainer != null) 
                OnClickContainer.Invoke(container);
        }

        IModel model_ = target.GetTransform().GetComponent<IModel>();
        if (model_ != null)
        {
            if (OnClickModel != null) 
                OnClickModel.Invoke(model_);
        }

        _lookables.Push(target);
        _isWatching = true;
        target.Show();
    }

    public void Back()
    {
        if (_lookables.Count > 0)
        {
            while (_lookables.Count > 0)
            {
                ILookable prevLookable = _lookables.Pop();
                prevLookable.Hide();
            }

            _isWatching = false;
            
            if (OnClickBack != null) 
                OnClickBack.Invoke();
        }
    }

    private void SmoothMovement(Vector3 target, float minDistance)
    {
        float distance = Vector3.Distance(_transform.position, target);
        if (Mathf.Abs(distance - minDistance) > 0)
        {
            Vector3 direction = (_transform.position - target).normalized;
            _transform.position = Vector3.MoveTowards(_transform.position, target + direction * minDistance, _smoothMovementSpeed * Time.deltaTime);
        } 
    }

    private void SmoothRotation(Vector3 target)
    {
        float distance = Vector3.Distance(_transform.rotation.eulerAngles, target);
        if (distance > 0)
        {
            _transform.rotation = Quaternion.RotateTowards(_transform.rotation, Quaternion.Euler(target), _smoothRotationSpeed * Time.deltaTime);
        }
    }

    private void RotationAround(Vector3 target)
    {
        transform.RotateAround(target, _transform.right, Input.GetAxis("Mouse Y") * _smoothMovementSpeed);
        transform.RotateAround(target, _transform.up, -Input.GetAxis("Mouse X") * _smoothMovementSpeed);
        
    }
}
