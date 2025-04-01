using UnityEngine;
using UnityEngine.Events;

public class OrbManager : MonoBehaviour
{
    public UnityEvent<Color> onOrbEnterEvent = null;
    public UnityEvent<Color> onOrbExitEvent = null;
}