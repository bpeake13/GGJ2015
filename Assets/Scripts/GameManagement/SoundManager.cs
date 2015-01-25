using UnityEngine;
using System.Collections;

public enum SoundEffectType{
    damage,
    block,
    dodge,
    strong,
    heal,
    pickup,
    invalid_move
}

public class SoundManager : MonoBehaviour {

    //Singlton setup
    public static SoundManager Instance
    {
        get { return instance; }
    }
    private static SoundManager instance;

    void Start(){
        instance = this;//Set the singleton
    }


    public AudioClip DamageSound;
    public AudioClip BlockSound;
    public AudioClip DodgeSound;
    public AudioClip StrongSound;
    public AudioClip HealSound;
    public AudioClip PickupSound;
    public AudioClip InvalidSound;


    /// <summary>
    /// Play a specific sound whenever you want.
    /// </summary>
    /// <param name="type"></param>
	public void PlaySound(SoundEffectType type)
    {
        switch (type)
        {
            case SoundEffectType.damage:
                AudioSource.PlayClipAtPoint(DamageSound, Vector3.zero);
                break;
            case SoundEffectType.block:
                AudioSource.PlayClipAtPoint(BlockSound, Vector3.zero);
                break;
            case SoundEffectType.strong:
                AudioSource.PlayClipAtPoint(StrongSound, Vector3.zero);
                break;
            case SoundEffectType.heal:
                AudioSource.PlayClipAtPoint(HealSound, Vector3.zero);
                break;
            case SoundEffectType.pickup:
                AudioSource.PlayClipAtPoint(PickupSound, Vector3.zero);
                break;
            case SoundEffectType.invalid_move:
                AudioSource.PlayClipAtPoint(InvalidSound, Vector3.zero);
                break;
            default:
                AudioSource.PlayClipAtPoint(DodgeSound, Vector3.zero);
                break;
        }
    }
}
