using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using TMPro;
using TMPro.Examples;
using Unity.VisualScripting;

public class Turret : MonoBehaviour
{
	protected int _countOfShots = 0;
    
	[Header("Turret and gun rotation settings")]

	[Tooltip("Башня танка(для теста)")] public GameObject _turret;
	//[SerializeField][Tooltip("Прибор наведения")] protected GameObject _spotter;

	[SerializeField][Tooltip("Скорость по оси X")] protected float _speedX;
	[SerializeField][Tooltip("Скорость по оси Y")] protected float _speedY;

	[Tooltip("Верные координаты, по которым необходимо стрелять(X0Z)")] public float[] _correctCoordinatesX0Z;
	[Tooltip("Верные координаты, по которым необходимо стрелять(Y)")] public float[] _correctCoordinatesY;

	[SerializeField][Tooltip("Угол максимального отклонения устройства наведения")] private float _maxGuidanceDeviceAngle;

	private float _x;
	private float _y;

	[Header("Displays")]

	[SerializeField][Tooltip("Выводит необходимые координаты")] protected Text _spotterText;
	[SerializeField][Tooltip("Показывает текущие координаты поворота")] protected Text _coordinator;

	[Header("Activation settings")]

	[SerializeField] private GameObject _camera;

	[SerializeField] private string _playerTag;

	[SerializeField] private float _maxDistanceAct;

	public LayerMask _mask;

	public bool _isActive = false;

	private GameObject _player;


	void Start()
    {
		_player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
		if (_isActive)
		{
			_x += Input.GetAxisRaw("Horizontal") * _speedX * Time.deltaTime;
			_y -= Input.GetAxisRaw("Vertical") * _speedY * Time.deltaTime;

			transform.localRotation = Quaternion.Lerp(Quaternion.Euler(0f, Input.GetAxisRaw("Horizontal") * _maxGuidanceDeviceAngle, 0f), transform.localRotation, 0.7f);

			_turret.transform.rotation = Quaternion.Euler(_y, _x, 0f);
		}

		float[] _currentRotationCoord = { _y - _y % 1, (_x - _x % 1) % 360 + 180, 0 };
		float[] _corrFullCoordinates = { _correctCoordinatesY[_countOfShots] * 1.00f, _correctCoordinatesX0Z[_countOfShots] * 1.00f, 0 };

		_coordinator.text = "Aim coordinates:\nX:" + _corrFullCoordinates[0] + "\nY:" + _corrFullCoordinates[1];
		_spotterText.text = "Aim coordinates:\nX:" + _currentRotationCoord[1] + "\nY:" + -_currentRotationCoord[0];

		if (Input.GetKeyDown(KeyCode.E))
		{
			if (_isActive == true)
			{
				_camera.SetActive(false);
				_player.SetActive(true);
				_isActive = false;
			}
		}
	}

	private void Fire()
	{
		_countOfShots++;
		print("The shot is fired");
	}


	public void ActivateAim()
	{
		print("b");
		_camera.GetComponent<SeatCamera>().CameraToBasic();

		_camera.SetActive(true);
		_player.SetActive(false);
		_isActive = true;
	}
}
