using UnityEngine;
using System.Collections;

public class SoundEffectsHelper : MonoBehaviour
{
	
	/// <summary>
	/// Singleton
	/// </summary>
	public static SoundEffectsHelper Instance;
	
	public AudioClip hackSound;
	public AudioClip jumpSound;
	public AudioClip gruntSound;
	public AudioClip laserSound;
	public AudioClip slinkSound;
	public AudioClip levelSound;
	
	
	void Awake()
	{
		// Register the singleton
		if (Instance != null)
		{
			Debug.LogError("Multiple instances of SoundEffectsHelper!");
		}
		Instance = this;
	}
	
	public void MakeHackSound()
	{
		MakeSound(hackSound);
	}
	
	public void MakeJumpSound()
	{
		MakeSound(jumpSound);
	}
	
	public void MakeGruntSound()
	{
		MakeSound(gruntSound);
	}
	
	public void MakeLaserSound()
	{
		MakeSound(laserSound);
	}
	
	public void MakeSlinkSound()
	{
		MakeSound(slinkSound);
	}
	
	public void MakeLevelSound()
	{
		MakeSound(levelSound);
	}
	
	
	/// <summary>
	/// Play a given sound
	/// </summary>
	/// <param name="originalClip"></param>
	private void MakeSound(AudioClip originalClip)
	{
		// As it is not 3D audio clip, position doesn't matter.
		AudioSource.PlayClipAtPoint(originalClip, transform.position);
	}
}
