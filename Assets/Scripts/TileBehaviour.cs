using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TileBehaviour : MonoBehaviour
{
    private Material _material;
    [SerializeField]
    private float _intensity = 2f;
    private Color _CurrentColor= Color.black;
    private UnityEvent<Color> _onTileEnterEvent = null;
    private UnityEvent<Color> _onTileExitEvent = null;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        Renderer ren = GetComponentInChildren<Renderer>();
        if (ren) _material = ren.material;
        _material.EnableKeyword("_EMISSION");
        _onTileEnterEvent = GetComponentInParent<GenerateFloor>().onTileEnterEvent;
        _onTileExitEvent = GetComponentInParent<GenerateFloor>().onTileExitEvent;
    }
    private void Start()
    {
        SetColor(Color.black);
    }
    private void OnDestroy()
    {
        if(_material) Destroy(_material);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if(collision.gameObject.GetComponentInChildren<Renderer>() != null)
            SetColor(collision.gameObject.GetComponentInChildren<Renderer>().material.color);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
       if(other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<BallController>().AddTileInRadius(this.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<BallController>().RemoveTileInRadius(this.gameObject);
        }
    }

    public void SetColor(Color color)
    {
        if (_CurrentColor != Color.black)
        {
            _onTileExitEvent?.Invoke(_CurrentColor);
        }

        _CurrentColor = color;
        _onTileEnterEvent?.Invoke(_CurrentColor);
        //_material.DisableKeyword("_EMISSION");
        _material.color = _CurrentColor;
        //_material.EnableKeyword("_EMISSION");
        _material.SetColor("_EmissionColor", _CurrentColor * _intensity);
    }

    public void SetColorHitByOtherPlayer()
    {
        
    }
}
