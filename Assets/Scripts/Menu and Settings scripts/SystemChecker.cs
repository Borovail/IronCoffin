using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SystemChecker : MonoBehaviour
{
    public Text _GPUName;
	public Text _GPUMemory;
	public Text _GPUType;

	public Text _deviceName;
    public Text _deviceModel;
    public Text _batteryLevel;
	public Text _deviceType;

	public Text _CPUCores;
	public Text _CPUFrequency;
	public Text _CPUType;

	public Text _RAM;

	public Text _RTSupport;


	void Start()
    {
		_GPUName.text = Convert.ToString(SystemInfo.graphicsDeviceName);
		_GPUMemory.text = Convert.ToString(SystemInfo.graphicsMemorySize);
		_GPUType.text = Convert.ToString(SystemInfo.graphicsDeviceType);

		_deviceName.text = Convert.ToString(SystemInfo.deviceName);
		_deviceModel.text = Convert.ToString(SystemInfo.deviceModel);
		_deviceType.text = Convert.ToString(SystemInfo.deviceType);

		_CPUCores.text = Convert.ToString(SystemInfo.processorCount);
		_CPUFrequency.text = Convert.ToString(SystemInfo.processorFrequency);
		_CPUType.text = Convert.ToString(SystemInfo.processorType);

		_RAM.text = Convert.ToString(SystemInfo.systemMemorySize);

		_RTSupport.text = Convert.ToString(SystemInfo.supportsRayTracing);
	}

    // Update is called once per frame
    void Update()
    {
        _batteryLevel.text = Convert.ToString(SystemInfo.batteryLevel * -100) + "%";
    }
}
