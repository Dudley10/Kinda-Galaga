using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float speed = 5.0f;
    public Projectile laserPrefab;
    private bool _laserActive;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
     if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
     this.transform.position += Vector3.left * this.speed * Time.deltaTime;   
    } else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
        this.transform.position += Vector3.right * this.speed * Time.deltaTime;
    } else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) {
        this.transform.position += Vector3.down * this.speed * Time.deltaTime;
    } else if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) {
        this.transform.position += Vector3.up * this.speed * Time.deltaTime;
    }
    if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)){
        Shoot();
    }
    }
    private void Shoot(){
        if (!_laserActive) {
        Projectile projectile = Instantiate(this.laserPrefab, this.transform.position, Quaternion.identity);
        projectile.destroyed += LaserDestroyed;
        _laserActive = true;
        }
    }
    private void LaserDestroyed(){
        _laserActive = false;
    }
    private void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.layer == LayerMask.NameToLayer("Invader") ||
        other.gameObject.layer == LayerMask.NameToLayer("Missile")) {
            //load win scene
        }
    }
}
