using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    
    public AudioClip clearFx;

    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnMouseDown()
    {
        if (spriteRenderer.sprite == null)
        {
            return;
        }
        ClearMatches();
    }

    private void ClearMatch(GameObject obj, Sprite target)
    {
        if (obj.GetComponent<SpriteRenderer>().sprite != target)
        {
            return;
        }
        if (obj == gameObject)
        {
            return;
        }

        obj.GetComponent<SpriteRenderer>().sprite = null;
        GameManager.instance.IncreaseScore();

        RaycastHit2D topObj = Physics2D.Raycast(obj.transform.position, Vector2.up);
        RaycastHit2D botObj = Physics2D.Raycast(obj.transform.position, Vector2.down);
        RaycastHit2D leftObj = Physics2D.Raycast(obj.transform.position, Vector2.left);
        RaycastHit2D rightObj = Physics2D.Raycast(obj.transform.position, Vector2.right);
        if (topObj.collider != null)
        {
            ClearMatch(topObj.collider.gameObject, target);
        }
        if (botObj.collider != null)
        {
            ClearMatch(botObj.collider.gameObject, target);
        }
        if (leftObj.collider != null)
        {
            ClearMatch(leftObj.collider.gameObject, target);
        }
        if (rightObj.collider != null)
        {
            ClearMatch(rightObj.collider.gameObject, target);
        }
    }

    private void ClearMatches()
    {
        bool matched = false;
        RaycastHit2D topObj = Physics2D.Raycast(transform.position, Vector2.up);
        RaycastHit2D botObj = Physics2D.Raycast(transform.position, Vector2.down);
        RaycastHit2D leftObj = Physics2D.Raycast(transform.position, Vector2.left);
        RaycastHit2D rightObj = Physics2D.Raycast(transform.position, Vector2.right);

        if (topObj.collider != null && topObj.collider.GetComponent<SpriteRenderer>().sprite == spriteRenderer.sprite)
        {
            ClearMatch(topObj.collider.gameObject, spriteRenderer.sprite);
            matched = true;
        }
        if (botObj.collider != null && botObj.collider.GetComponent<SpriteRenderer>().sprite == spriteRenderer.sprite)
        {
            ClearMatch(botObj.collider.gameObject, spriteRenderer.sprite);
            matched = true;
        }
        if (leftObj.collider != null && leftObj.collider.GetComponent<SpriteRenderer>().sprite == spriteRenderer.sprite)
        {
            ClearMatch(leftObj.collider.gameObject, spriteRenderer.sprite);
            matched = true;
        }
        if (rightObj.collider != null && rightObj.collider.GetComponent<SpriteRenderer>().sprite == spriteRenderer.sprite)
        {
            ClearMatch(rightObj.collider.gameObject, spriteRenderer.sprite);
            matched = true;
        }

        if (matched)
        {
            spriteRenderer.sprite = null;
            GameManager.instance.IncreaseScore();
            GameManager.instance.UpdateScore();

            Playfield.instance.Drop();
            Playfield.instance.LeftAlign();
            GetComponent<AudioSource>().PlayOneShot(clearFx);
            Playfield.instance.GameOver();
        }

    }

}
