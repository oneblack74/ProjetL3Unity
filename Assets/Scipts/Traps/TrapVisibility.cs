using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class TrapVisibility : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    [SerializeField] private float visibility;
}
