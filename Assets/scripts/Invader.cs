using UnityEngine;

public class Invader : MonoBehaviour
{
    public Sprite[] animationSprites;

    public float animationTime = 1.0f;

    private SpriteRenderer _spriteRenderer;
    private int _animationFrame;
    public System.Action killed;

    private void Awake() {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvokeRepeating(nameof(AnimateSprite), this.animationTime, this.animationTime);
    }

    private void AnimateSprite(){
        _animationFrame++;
        if (_animationFrame > this.animationSprites.Length){
            _animationFrame = 0;
        }
        _spriteRenderer.sprite = this.animationSprites[_animationFrame];
    }
    private void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.layer == LayerMask.NameToLayer("Laser")) {
            this.killed.Invoke();
            this.gameObject.SetActive(false);
        }
        Softlock(other);
    }

     private void Softlock(Collider2D invader){
        if (invader.gameObject.layer == LayerMask.NameToLayer("Border")) {
            //load lose scene
            UnityEngine.SceneManagement.SceneManager.LoadScene("Lose");
        }
     }

    // Update is called once per frame
    void Update()
    {
        
    }
}
