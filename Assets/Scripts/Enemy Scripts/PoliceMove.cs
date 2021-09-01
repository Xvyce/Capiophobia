using UnityEngine;

public class PoliceMove : MonoBehaviour
{
    [SerializeField] GameObject[] waypoints;
    public GameObject police;

    readonly float speed = 0.75f;

    Event _event;

	private void Start()
	{
        this.gameObject.SetActive(false);
        _event = FindObjectOfType<Event>();
        police.gameObject.SetActive(true);
        police.gameObject.transform.position = new Vector3(waypoints[0].transform.position.x, waypoints[0].transform.position.y, waypoints[0].transform.position.z);
    }

	private void OnEnable()
	{
        police.gameObject.SetActive(true);
        police.gameObject.transform.position = new Vector3(waypoints[0].transform.position.x, waypoints[0].transform.position.y, waypoints[0].transform.position.z);
    }

    private void Update()
    {
        MovePolice();
    }

	private void MovePolice()
	{
        if(_event.isHoveringOverChangeScene)
		{
            float step = speed * Time.deltaTime;
            police.transform.position = Vector3.MoveTowards(police.transform.position, waypoints[1].transform.position, step);
            if (police.gameObject.transform.position == waypoints[1].transform.position)
            {
                police.gameObject.SetActive(false);
                _event.isHoveringOverChangeScene = false;
            }
        }
    }        
}
