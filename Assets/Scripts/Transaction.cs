using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Transaction : MonoBehaviour
{
    // Start is called before the first frame update
    public InputField newAddress;
    private int nextScene;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Login()
    {
        Debug.Log(newAddress.text);

        if (newAddress.text == "1")
        {
            Debug.Log("lock");
            nextScene = SceneManager.GetActiveScene().buildIndex + 1;
            SceneManager.LoadScene(nextScene);
        }
        else if (newAddress.text == "0")
        {
            Debug.Log("unlock");
        }
    }

    public void trans_address()
    {
        Debug.Log(newAddress.text);

        if (newAddress.text == "1")
        {
            Debug.Log("lock");
            Canvas c = transform.root.GetComponentInChildren<Canvas>();
            c.gameObject.SetActive(false);
        }
        else if (newAddress.text == "0")
        {
            Debug.Log("unlock");
        }
    }
}
