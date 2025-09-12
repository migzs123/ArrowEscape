using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartUI : MonoBehaviour
{
    public GameObject heartUI;
    public GameObject player;

    public GameObject HeartPrefab;

    private List<GameObject> hearts = new List<GameObject>();
    private Player playerScript;

    // Start is called before the first frame update
    void Start()
    {
        playerScript = player.GetComponent<Player>();
        DrawHearts();
    }

    public void DrawHearts()
    {
        Transform initialPositon = heartUI.transform;

        for (int i = 0; i < playerScript.maxHealth; i++)
        {
            Vector3 position = new Vector3(initialPositon.position.x + (i * 50), initialPositon.position.y, 0);
            GameObject newHeart = Instantiate(HeartPrefab, position, transform.rotation, heartUI.transform);
            hearts.Add(newHeart);
        }
    }

    public void UpdateHearts()
    {
        for (int i = 0; i < hearts.Count; i++)
        {
            if (i < playerScript.currHealth)
            {
                hearts[i].transform.Find("Heart").gameObject.SetActive(true);
            }
            else
            {
                hearts[i].transform.Find("Heart").gameObject.SetActive(false);
            }
        }
    }
}
