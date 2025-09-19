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
        UpdateHearts();
    }

    public void DrawHearts()
    {
        for (int i = 0; i < playerScript.maxHealth; i++)
        {
            GameObject newHeart = Instantiate(HeartPrefab, heartUI.transform);
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
