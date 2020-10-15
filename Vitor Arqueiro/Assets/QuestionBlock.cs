using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionBlock : MonoBehaviour
{

    public float bounceHeight = 0.5f;
    public float bounceSpeed = 4f;
    public Sprite empytBlockSprite;

    public float coinMoveSpeed = 8f;
    public float coinMoveHeight = 3f;
    public float coinFallDistance = 2f;

    public GameObject spiningCoin;

    private Vector2 originalPosition;

    private bool canBounce = true;


    // Start is called before the first frame update
    void Start()
    {
        originalPosition = transform.localPosition;
    }
    void changeSprite()
    {
        GetComponent<Animator>().enabled = false;
        GetComponent<SpriteRenderer>().sprite = empytBlockSprite;
    }
    public void QuestionBlockBounce()
    {
        if(canBounce)
        {
            canBounce = false;
            StartCoroutine(Bounce());
        }
    }
    void PresentCoin()
    {
        //GameObject spiningCoin = (GameObject)Instantiate(Resources.Load("Prefabs/MoedasPega", typeof(GameObject)));
        GameObject temp = Instantiate(spiningCoin, transform.position, transform.localRotation);
        temp.transform.SetParent(this.transform.parent);
        temp.transform.localPosition = new Vector2(originalPosition.x, originalPosition.y+1);
        StartCoroutine(MoveCoin(temp));
    }

    // Update is called once per frame
    void Update()
    {
       // QuestionBlockBounce();
    }

    IEnumerator Bounce ()
    {
        changeSprite();
        PresentCoin();
        while (true)
        {
            transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y + bounceSpeed*Time.deltaTime);
            if (transform.localPosition.y >= originalPosition.y + bounceHeight)
            {
                break;
            }
            yield return null;
        }
        while (true)
        {
            transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y - bounceSpeed*Time.deltaTime);
            if (transform.localPosition.y <= originalPosition.y + bounceHeight)
            {
                transform.localPosition = originalPosition;
                break;
            }
            yield return null;
        }
    }
    IEnumerator MoveCoin(GameObject coin)
    {
        while (true)
        {
            coin.transform.localPosition = new Vector2(coin.transform.localPosition.x, coin.transform.localPosition.y + coinMoveSpeed*Time.deltaTime);

            if (coin.transform.localPosition.y >= originalPosition.y + coinMoveHeight + 1)
            {
                break;
            }
            yield return null;
        }
        while (true)
        {
            coin.transform.localPosition = new Vector2(coin.transform.localPosition.x, coin.transform.localPosition.y - coinMoveSpeed * Time.deltaTime);
            if (coin.transform.localPosition.y <= originalPosition.y + coinMoveHeight - 1)
            {
                Destroy(coin.gameObject);
                break;
            }
            yield return null;
        }
    }
}
