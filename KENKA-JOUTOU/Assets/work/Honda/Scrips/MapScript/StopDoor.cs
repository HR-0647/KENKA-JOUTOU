using UnityEngine;

public class StopDoor : MonoBehaviour
{
    public Animation anim;
    bool stopAnim = false;

    public void Stop()
    {
        anim.Stop();
        stopAnim = true;
    }
}
