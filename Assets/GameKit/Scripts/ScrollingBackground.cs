using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
	public bool scrolling, parallax;

	public float backgroundSize;
	public float parallaxSpeed;

	private Transform cameraTransform;
	private Transform[] layers;
	private float viewZone = 10;
	private int bottomIndex;
	private int topIndex;
	private float lastCameraY;

    void Start()
    {
		cameraTransform = Camera.main.transform;
		lastCameraY = cameraTransform.position.y;	
		layers = new Transform[transform.childCount];
		for (int i = 0; i < transform.childCount; i++)
			layers[i] = transform.GetChild(i);

		bottomIndex = 0;
		topIndex = layers.Length - 1;
    }

	private void Update()
	{
		if (parallax)
		{
			float deltaY = cameraTransform.position.y - lastCameraY;
			transform.position += Vector3.up * (deltaY * parallaxSpeed);
		}

		lastCameraY = cameraTransform.position.y;

		if (scrolling)
		{
			if (cameraTransform.position.y < (layers[bottomIndex].transform.position.y + viewZone))
				ScrollBottom();
			if (cameraTransform.position.y > (layers[topIndex].transform.position.y - viewZone))
				ScrollTop();
		}
	}

	private void ScrollBottom()
	{
		// Debug.Log("Scroll Bottom!");
		layers[topIndex].position = Vector3.up * (layers[bottomIndex].position.y - backgroundSize);
		bottomIndex = topIndex;
		topIndex--;
		if (topIndex < 0)
			topIndex = layers.Length - 1;
	}

	private void ScrollTop()
	{
		// Debug.Log("Scroll Top!");
		layers[bottomIndex].position = Vector3.up * (layers[topIndex].position.y + backgroundSize);
		topIndex = bottomIndex;
		bottomIndex++;
		if (bottomIndex == layers.Length)
			bottomIndex = 0;
	}
}