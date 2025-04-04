using UnityEngine;
using UnityEngine.SceneManagement;

public class Invaders : MonoBehaviour
{
    public Invader[] prefabs = new Invader[5];
    public int rows = 5;
    public int columns = 11;
    public AnimationCurve speed;
    private Vector3 _direction = Vector2.right;
    public int amountKilled { get; private set; }
    public int totalInvaders => this.rows * this.columns;
    public float percentKilled => (float)this.amountKilled / (float)this.totalInvaders;
    public float missileAttackRate = 1.0f;
    public int amountAlive => this.totalInvaders - this.amountKilled;
    public Projectile missilePrefab;
   

    private void Awake(){
        for (int row = 0; row < this.rows; row++)
        {
            float width = 3.0f * (this.columns - 1);
            float height = 3.0f * (this.rows - 1);
            Vector2 centering = new Vector2(-width /2, -height / 2);
            Vector3 rowPosition = new Vector3(centering.x, centering.y + (row * 3.0f), 0.0f);
            for (int col = 0; col < this.columns; col ++)
            {
                Invader invader = Instantiate(this.prefabs[row], this.transform);
                invader.killed += InvaderKilled;
                Vector3 position = rowPosition;
                position.x += col * 3.0f;
                invader.transform.localPosition = position;
            }
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvokeRepeating(nameof(MissileAttack), this.missileAttackRate, this.missileAttackRate);
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += _direction * this.speed.Evaluate(this.percentKilled) * Time.deltaTime;
        Vector3 leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
        Vector3 rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);

        foreach (Transform invader in this.transform)
        {
            if (!invader.gameObject.activeInHierarchy) {
                continue;
            }
            if(_direction == Vector3.right && invader.position.x > (rightEdge.x - 1.0f)) 
            {
                AdvanceRow();
            } else if (_direction == Vector3.left && invader.position.x < (leftEdge.x + 1.0f)){
                AdvanceRow();
            }
        }
    }
    private void AdvanceRow(){
        _direction.x *= -1.0f;

        Vector3 position = this.transform.position;
        position.y -= 1.0f;
        this.transform.position = position;
    }
    private void InvaderKilled(){
        this.amountKilled++;
//you win that currently doesn't work
        if(this.amountKilled >= this.totalInvaders){
         UnityEngine.SceneManagement.SceneManager.LoadScene("Win");
        }
    }
    private void MissileAttack(){
        foreach (Transform invader in this.transform){
            if(!invader.gameObject.activeInHierarchy){
                continue;
            }
            if (Random.value < (1.0f / (float)this.amountAlive)){
                Instantiate(this.missilePrefab, invader.position, Quaternion.identity);
                break;
            }
        }
    }
}
