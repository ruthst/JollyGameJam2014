using UnityEngine;
using System.Collections;
using Jolly;

public class CameraFollow : MonoBehaviour
{
	public GameObject[] HeroesToFollow;
	public float SeparationZoomFactor;
	public float FollowLerpFactor = 5.0f;
	public Vector3 OffsetFromAverageLocation;
	public float MinimumY;
	
	private float InitialOrthographicSize;
	private Vector3 TargetCameraPosition;
	private float TargetCameraOrthographicSize;

	void Start ()
	{
		this.InitialOrthographicSize = this.camera.orthographicSize;
		this.TargetCameraPosition = this.transform.position;
	}
	
	void Update ()
	{
		Vector3 focalPoint = this.HeroesAverageLocation();
		float zoom = this.HeroesSeparation() * this.SeparationZoomFactor;
		this.SetCamera(focalPoint, zoom);
//		print (focalPoint);
	}

	void OnPreCull ()
	{
		float lerpFactor = Time.deltaTime * this.FollowLerpFactor;
		this.camera.transform.position = Vector3.Lerp(this.camera.transform.position, this.TargetCameraPosition, lerpFactor);
		this.camera.orthographicSize = Mathf.Lerp (this.camera.orthographicSize, this.TargetCameraOrthographicSize, lerpFactor);
	}

	private Vector3 HeroesAverageLocation()
	{
		Vector3 average = Vector3.zero;
		foreach (GameObject go in this.HeroesToFollow)
		{
			average += go.transform.position;
		}
		average /= this.HeroesToFollow.Length;
		return average;
	}

	private float HeroesSeparation()
	{
		return Mathf.Abs (this.LeftmostHero().transform.position.x - this.RightmostHero().transform.position.x);
	}

	private GameObject LeftmostHero ()
	{
		GameObject leftmostHero = null;
		float leftmostX = Mathf.Infinity;
		foreach (GameObject go in this.HeroesToFollow)
		{
			float x = go.transform.position.x;
			if (x < leftmostX)
			{
				leftmostX = x;
				leftmostHero = go;
			}
		}
		return leftmostHero;
	}

	private GameObject RightmostHero ()
	{
		GameObject rightmostHero = null;
		float rightmostX = Mathf.NegativeInfinity;
		foreach (GameObject go in this.HeroesToFollow)
		{
			float x = go.transform.position.x;
			if (x > rightmostX)
			{
				rightmostX = x;
				rightmostHero = go;
			}
		}
		return rightmostHero;
	}

	void SetCamera(Vector3 focalPoint, float zoom)
	{
		focalPoint += this.OffsetFromAverageLocation;
		float clampedY = Mathf.Max(focalPoint.y, this.MinimumY);
		this.TargetCameraPosition = this.TargetCameraPosition.SetXY (focalPoint.x, clampedY);
		this.TargetCameraOrthographicSize = InitialOrthographicSize * (1.0f+zoom);
	}
}
