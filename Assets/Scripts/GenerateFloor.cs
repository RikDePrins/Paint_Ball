using UnityEngine;
using UnityEngine.Events;

public class GenerateFloor : MonoBehaviour
{
    public UnityEvent<Color> onTileEnterEvent = null;
    public UnityEvent<Color> onTileExitEvent = null;

    [SerializeField]
    private GameObject _tileTemplate;
    [SerializeField]
    private int _nrOfCols;
    [SerializeField]
    private int _nrOfRows;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        var basePos = transform.position;
        var tileBounds = _tileTemplate.GetComponent<BoxCollider>().size;
        for (int col = 0; col < _nrOfCols; col++)
        {
            for (int row = 0; row < _nrOfRows; row++)
            {
                var tileObject = Instantiate(
                    _tileTemplate,
                    new Vector3(basePos.x + col * tileBounds.x, basePos.y, basePos.z + row * tileBounds.z),
                    Quaternion.identity,
                    transform);
                //tileObject.GetComponent<TileBehaviour>().SetColor(Color.red);
            }
        }
    }


}
