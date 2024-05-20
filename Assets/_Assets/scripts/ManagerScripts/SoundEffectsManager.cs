using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectsManager : MonoBehaviour
{
    [SerializeField] AllSoundEffectSO allSoundEffectsSO;

    [SerializeField] Transform drawerInstantiatePosition;
    public static SoundEffectsManager Instance { get; private set; }


    private void Start()
    {
        SocketBehaviour.OnAnyWireSnap += SocketBehaviour_OnAnyWireSnap;
    }

    private void Awake()
    {
        Instance = this;
    }

    public void DrawerOpenCloseSound()
    { PlaySound(allSoundEffectsSO.closingDrawer, Vector3.zero, 1f); }
    public void NormalClick()
    { PlaySound(allSoundEffectsSO.normalClickUIAudio,Vector3.zero , 1.5f); }

    public void KeyPadClick()
    { PlaySound(allSoundEffectsSO.keypadClickUIAudio, Vector3.zero, .2f); }

    public void OnDestroyObjectByHand(Vector3 position)
    { PlaySound(allSoundEffectsSO.destroyComponent, position, .5f) ; }
    public void OnComponentFallOnGround(Vector3 position)
    {
        PlaySound(allSoundEffectsSO.componentFallGroundDestroyAudio, position, 2f);
    }

    public void OnComponentDropOnTable(Vector3 position)
    {
        PlaySound(allSoundEffectsSO.componentDropAudio, position, 1f);
    }

    public void OnAnyComponentInstantiated()
    {
        PlaySound(allSoundEffectsSO.componentInstantiateInDrawerAudio,drawerInstantiatePosition.position,.5f);
    }

    private void SocketBehaviour_OnAnyWireSnap(object sender, SocketBehaviour.OnAnyWireSnapEventArgs e)
    {
        PlaySound(allSoundEffectsSO.wireSnapAudio, e.position,.3f);
    }

    private void PlaySound(AudioClip audio, Vector3 position, float volume = 1)
    { AudioSource.PlayClipAtPoint(audio, position, volume); }
}
