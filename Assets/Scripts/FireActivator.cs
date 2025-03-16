using System.Collections;
using UnityEngine;

public class FireActivator : MonoBehaviour
{
    [SerializeField] private ParticleSystem _fire;
    [SerializeField] private AudioSource _sourceAudFire;

    private bool _isFire;
    private BoxCollider _collider;


	private void Start()
	{
        _collider = GetComponent<BoxCollider>();
        _collider.enabled = false;
        StartCoroutine(WaitForFire());
	}

	private IEnumerator WaitForFire()
    {
        yield return new WaitForSeconds(Random.Range(10f, 24f));
        StartCoroutine(StartFire());
    }

    private IEnumerator StartFire()
    {
        _collider.enabled = true;
        _isFire = true;
        _fire.Play();
        _sourceAudFire.Play();
        yield return new WaitForSeconds(15f);
        print("you lose");
    }

    private IEnumerator StopFire()
    {
        StopCoroutine(StartFire());
        yield return new WaitForSeconds(4f);
        _fire.Stop();
        _sourceAudFire.Stop();
        StartCoroutine(WaitForFire());
    }

	private void OnTriggerEnter(Collider other)
	{
        print("1");
		if(other.gameObject.tag == "Extinguisher" && _isFire == true)
        {
            print("2");
            _collider.enabled = false;
            _isFire = false;
            StartCoroutine(StopFire());
        }
	}
}
