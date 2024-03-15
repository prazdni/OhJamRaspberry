using UnityEngine;

public class RaspberryController : MonoBehaviour
{
    [SerializeField]
    string _id;
    [SerializeField]
    GameObject _raspberry;

    RaspberryManager _raspberryManager;

    void Start()
    {
        _raspberryManager = FindObjectOfType<RaspberryManager>();
        _raspberry.SetActive(_raspberryManager && !_raspberryManager.IsCollected(_id));
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!_raspberry.activeSelf)
            return;

        _raspberry.SetActive(false);
        if (_raspberryManager && other.gameObject.GetComponent<CatMovementController>())
            _raspberryManager.AddRaspberry(_id);
    }
}
