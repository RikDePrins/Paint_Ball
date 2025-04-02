using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.VFX;

public class LandMineBehaviour : MonoBehaviour
{
   
    [SerializeField]
    private float _force = 1;
    [SerializeField]
    private GameObject _ExplosionPartile = null;

    private Material _OwningPlayerMat = null;
    private Material _MineMat = null;
    private GameObject _OwningPlayer = null;
    private GameObject _OtherPlayer = null;
    public GameObject OwningPlayer 
    {
        get { return _OwningPlayer; } 
        set 
        { 
            _OwningPlayerMat = value.GetComponentInChildren<Renderer>().material;
            _OwningPlayer = value;
            _MineMat.color = _OwningPlayerMat.color * 5;
            var players = FindObjectsByType<BallController>(FindObjectsSortMode.None);

            if (players[0].gameObject == OwningPlayer) _OtherPlayer = players[1].gameObject;
            else _OtherPlayer = players[0].gameObject;
        } 
    }

    private List<TileBehaviour> _TilesInCollider = new List<TileBehaviour>();

    private void Awake()
    {
        _MineMat = GetComponentInChildren<Renderer>().material;
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Tile"))
        {
            _TilesInCollider.Add(other.gameObject.GetComponent<TileBehaviour>());
        }
       
    }
    private void Update()
    {
        if(Vector3.Distance(_OtherPlayer.transform.position, transform.position) < 1f)
        {
            Explode(_OtherPlayer);
        }
        
    }

    private void Explode(GameObject otherPlayer)
    {
        otherPlayer.GetComponent<Rigidbody>().AddForce
            (
            (otherPlayer.transform.position - transform.position).normalized * _force,
            ForceMode.Impulse
            );
        foreach (var tile in _TilesInCollider)
        {
            tile.SetColor(_OwningPlayerMat.color);
        }

        var particle = Instantiate(_ExplosionPartile, transform);
        particle.transform.parent = null;
        particle.GetComponent<VisualEffect>().SetVector4("Color", _OwningPlayerMat.color);
        
        Destroy(particle, 2f);

        Destroy(gameObject);
    }
        
}
