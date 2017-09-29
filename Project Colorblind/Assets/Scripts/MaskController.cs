using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskController : MonoBehaviour {
    public enum MASK_COLOR { Red, Green, Blue };
    public MASK_COLOR myColor;
    public Sprite[] sprites;
    private SpriteMask myMask;
    private Transform myTransform;
    private int index = 0;


	// Use this for initialization
	void Start () {
        myMask = GetComponent<SpriteMask>();
        myTransform = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
        switch (myColor)
        {
            case MASK_COLOR.Red:
                if (Input.GetAxis("P1_CycleMask") > 0)
                {
                    CycleActiveSprite();
                }
                break;
            case MASK_COLOR.Green:
                if(Input.GetAxis("P2_CycleMask") > 0)
                {
                    CycleActiveSprite();
                }
                break;
            case MASK_COLOR.Blue:
                if(Input.GetAxis("P3_CycleMask")> 0)
                {
                    CycleActiveSprite();
                }
                break;
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
