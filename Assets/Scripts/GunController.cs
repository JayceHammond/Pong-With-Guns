using System.Collections;
using UnityEngine;

public class GunController : MonoBehaviour
{
    Vector2 mousePos = new Vector2();
    
    private Vector3 mousePosition;
    public GameObject bullet;
    public float bulletSpeed;
    public Transform barrel;
    public Sprite crosshair;
    private AudioSource audioData;

    // Start is called before the first frame update
    void Start()
    {
        audioData = GetComponent<AudioSource>();
        Vector2 center = new Vector2(0,30);
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.SetCursor(crosshair.texture, center, CursorMode.ForceSoftware);
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerController.paused == true){
            Time.timeScale = 0;
        }else if(PlayerController.paused == false){
            Time.timeScale = 1;
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 targetDir = mousePosition - transform.position;
            Vector3 perpendicular = Vector3.Cross(-targetDir, Vector3.forward);
            transform.rotation = Quaternion.LookRotation(Vector3.forward, perpendicular);

            if(Input.GetMouseButtonDown(0) == true){
                audioData.Play(0);
                GameObject shot = Instantiate(bullet, barrel.transform.position, transform.rotation);
                shot.GetComponent<Rigidbody2D>().AddForce(transform.right * bulletSpeed, ForceMode2D.Impulse);
                StartCoroutine(SelfDestruct(shot));
            }
        }


        

        
    }

    void OnGUI() {
        Event currEvent = Event.current;
        mousePos.x = currEvent.mousePosition.x;
        mousePos.y = Camera.main.pixelHeight - currEvent.mousePosition.y;
    }

    IEnumerator SelfDestruct(GameObject bullet){
        yield return new WaitForSeconds(5f);
        Destroy(bullet);
    }
}
