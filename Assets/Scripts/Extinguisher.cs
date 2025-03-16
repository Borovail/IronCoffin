using UnityEngine;

public class Extinguisher : MonoBehaviour
{
    private GameObject _holder;
    private Rigidbody _rb;
    private BoxCollider _trigger;

    public float _force;

    void Awake()
    {
        _holder = GameObject.FindWithTag("Holder");
		_rb = GetComponent<Rigidbody>();
        _trigger = GetComponent<BoxCollider>();
	}

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            _trigger.enabled = true;
        }
        else
        {
            _trigger.enabled = false;
        }
    }

    public void HoldEstinguisher()
    {
        _rb.isKinematic = true;
        transform.parent = _holder.transform;
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }

    public void LetGoF()
    {
        _rb.isKinematic = false;
        transform.parent = null;
        _rb.AddForce(transform.forward * _force, ForceMode.VelocityChange);
    }
}
