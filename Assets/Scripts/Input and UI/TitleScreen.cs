using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleScreen : MonoBehaviour
{
    public Sprite[] sprites = new Sprite[2];
    int counter;
    int index;
    public int animFrames;

    public Image player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        counter++;
        if(counter > animFrames)
        {
            counter = 0;
            index = (index + 1) % 2;
            player.sprite = sprites[index];
        }

        if (Input.GetKey(KeyCode.C))
        {
            SceneManager.LoadScene("Tutorial");
        }
        else if (Input.GetKey(KeyCode.E))
        {
            SceneManager.LoadScene("Endless");
        }
    }
}
