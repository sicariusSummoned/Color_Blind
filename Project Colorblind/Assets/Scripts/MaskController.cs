using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskController : MonoBehaviour {
    public enum MASK_COLOR { Red, Green, Blue };
    public MASK_COLOR myColor;
    private string maskButton = "_CycleMask";
    private string lookHorizAxis = "_Look_Horizontal";
    private string lookVertAxis = "_Look_Vertical";
    
    private int index = 0;
    public Sprite[] sprites;
    private SpriteMask myMask;

    private Transform myTransform;
    private Vector2 targetVector;
    private Vector2 startVector = Vector2.up;

    private Quaternion myRotation;
    private Quaternion targetRotation;

    public float rotationSpeed = 150.0f;

    private bool wasDown = false;


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
                ProcessInput("P1");
                break;
            case MASK_COLOR.Green:
                ProcessInput("P2");
                break;
            case MASK_COLOR.Blue:
                ProcessInput("P3");
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

    void LerpToTarget()
    {
        float step = rotationSpeed * Time.deltaTime;

        targetRotation =  Quaternion.AngleAxis(Vector2.SignedAngle(startVector, targetVector),Vector3.forward);

        myTransform.rotation = Quaternion.RotateTowards(myTransform.rotation, targetRotation, step);
    }

    void ProcessInput(string playerString)
    {
        string buttonString = playerString + maskButton;
        string horizString = playerString + lookHorizAxis;
        string vertString = playerString + lookVertAxis;


        if (Input.GetAxis(buttonString) > 0)
        {
            if (!wasDown)
            {
                CycleActiveSprite();
                wasDown = true;
            }
        }
        else
        {
            wasDown = false;
        }

        if (Input.GetAxis(horizString) != 0 || Input.GetAxis(vertString) != 0)
        {
            targetVector = new Vector2(Input.GetAxis(horizString), Input.GetAxis(vertString));
            LerpToTarget();

        }
        else
        {
            targetVector = new Vector2(0, 0);
        }
    }
   
}
