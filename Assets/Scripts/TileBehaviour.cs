
using UnityEngine;

public class TileBehaviour : MonoBehaviour
{
    private Material _material;
    [SerializeField]
    private float _intensity = 2f;
    private Color _CurrentColor= Color.black;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        Renderer ren = GetComponentInChildren<Renderer>();
        if (ren) _material = ren.material;
        _material.EnableKeyword("_EMISSION");

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
            SetColor(collision.gameObject.GetComponentInChildren<Renderer>().material.color);
        }
    }
    // Update is called once per frame
    void Update()
    {
        SetColor(_CurrentColor);
    }

    public void SetColor(Color color)
    {
        _CurrentColor = color;
        //_material.DisableKeyword("_EMISSION");
        _material.color = _CurrentColor;
        //_material.EnableKeyword("_EMISSION");
        _material.SetColor("_EmissionColor", _CurrentColor * _intensity);
    }

}
