using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMask : MonoBehaviour {
    public Sprite[] sprites;
    private SpriteMask myMask;
    private int index = 0;


	// Use this for initialization
	void Start () {
        myMask = GetComponent<SpriteMask>();
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKeyUp(KeyCode.Space))
        {
            CycleActiveSprite();
        }else if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            SetActiveSprite(0);
        }else if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            SetActiveSprite(1);
        }else if (Input.GetKeyUp(KeyCode.Alpha3))
        {
            SetActiveSprite(2);
        }
	}

    void CycleActiveSprite()
    {
        if(index == sprites.Length -1)
        {
            index = 0;
        }
        else
        {
            index++;
        }

        myMask.sprite = sprites[index];
    }

    void SetActiveSprite(int spriteIndex)
    {
        if(spriteIndex > -1 && spriteIndex < sprites.Length)
        {
            index = spriteIndex;
            myMask.sprite = sprites[index];
        }
        else
        {
            Debug.LogError("Index out of bounds");
        }
    }
}
