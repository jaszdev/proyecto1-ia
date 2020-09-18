using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHub : MonoBehaviour
{
    public Transform container;
    public Text genText;
    public Text healthText;
    public Text driveText;
    public LayerMask enemyLayer;
    public Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Collider2D enemyCollider = Physics2D.OverlapCircle(mouseWorldPos, 0.5f, enemyLayer);
        Enemy enemy = null;
        if (enemyCollider != null) enemy = enemyCollider.GetComponent<Enemy>();

        bool mouseOverEnemy = enemyCollider != null && enemy != null;

        container.gameObject.SetActive(mouseOverEnemy);

        if (mouseOverEnemy)
        {
            genText.text = enemy.gen.ToString();
            healthText.text = enemy.GetComponent<Damageable>().health.ToString();
            driveText.text = enemy.drive.ToString();
            transform.position = Camera.main.WorldToScreenPoint(enemy.transform.position + offset);
        }

    }
}
