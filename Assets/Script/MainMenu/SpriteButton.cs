using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Collider2D))]
public class SpriteButton : MonoBehaviour
{
    public enum ButtonType { Start, Settings, Inventory, Kitchen, Shop, ChangeBait }
    public ButtonType buttonType;

    private void OnMouseDown()
    {
        // Handle click
        IconController.Instance.HandleSpriteButtonPress(buttonType);
    }
}
