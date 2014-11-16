using UnityEngine;
using System.Collections;

public class HeroController : MonoBehaviour
{
	public int PlayerNumber;

	public float HorizontalMovementAxis
	{
		get
		{
			return Input.GetAxis (string.Format ("Horizontal[{0}]", this.PlayerNumber));
		}
	}

	public bool Jump
	{
		get
		{
			return Input.GetButtonDown(string.Format ("Jump[{0}]", this.PlayerNumber));
		}
	}

	public bool Shooting
	{
		get
		{
			return Input.GetButton(string.Format ("Fire[{0}]", this.PlayerNumber));
		}
	}
}
