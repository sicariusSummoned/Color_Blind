using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCatalogue : MonoBehaviour
{
    public enum BLOCK_COLOR { Red, Green, Blue };
    public BLOCK_COLOR myColor;

    public enum BLOCK_TYPE { Platform, MovingPlatform, Spikes };
    public BLOCK_TYPE myType;
}
