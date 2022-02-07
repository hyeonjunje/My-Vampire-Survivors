using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPTree : MonoBehaviour
{
    [Tooltip("Select a Prop Variant.")]
    [SerializeField] private TreeSelection selection = TreeSelection.TreeApples;

    [Header("Sprites")]
    [SerializeField] private Sprite treeApples;
    [SerializeField] private Sprite treeNoApples;

    [Header("Shadows")]
    [SerializeField] private Sprite shadowTreeApples;
    [SerializeField] private Sprite shadowTreeNoApples;

    private void OnValidate()
    {
        Sprite selectedSprite = null;
        Sprite selectedShadow = null;

        switch (selection)
        {
            case TreeSelection.TreeApples:
                selectedSprite = treeApples;
                selectedShadow = shadowTreeApples;
                break;
            case TreeSelection.TreeNoApples:
                selectedSprite = treeNoApples;
                selectedShadow = shadowTreeNoApples;
                break;
        }
        GetComponent<SpriteRenderer>().sprite = selectedSprite;
        transform.Find("Shadow").GetComponent<SpriteRenderer>().sprite = selectedShadow;
    }

    private enum TreeSelection
    {
        TreeApples,
        TreeNoApples,
    }
}
