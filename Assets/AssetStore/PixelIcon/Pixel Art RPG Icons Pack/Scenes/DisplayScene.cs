using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;
using System;

public class DisplayScene : MonoBehaviour
{
    [SerializeField] private Transform canvasHolder;
    [SerializeField] private SpriteAtlas atlas;
    [SerializeField] private int iconsAmount;
    [SerializeField] private GameObject iconPrefab;
    [SerializeField] private GameObject hitPlayText;
    private Sprite[] icons;
    private Func<Sprite> getSpriteFunc;
    private int nextSprite;

    private void Start() {
        if(hitPlayText != null)
            hitPlayText.SetActive(false);
            
        icons = new Sprite[atlas.spriteCount];
        atlas.GetSprites(icons);
        nextSprite = -1;

        SetRandom();
    }

    public void SetRandom(){
        getSpriteFunc = RandomSprite;
        SetIcons();
    }

    public void SetNext(){
        nextSprite++;
        if(nextSprite >= icons.Length)
            nextSprite = 0;
            
        getSpriteFunc = NextSprite;
        SetIcons();
    }

    private void SetIcons(){
        // Cleaning all icons from screen
        foreach(Transform child in canvasHolder)
            Destroy(child.gameObject);

        // Instantiating new icons
        for(int i = 0; i < iconsAmount; i++){
            Transform iconTransf = Instantiate(iconPrefab, Vector3.zero, Quaternion.identity).transform;
            iconTransf.SetParent(canvasHolder);
            iconTransf.GetComponent<Image>().sprite = getSpriteFunc();
        }
        // Reseting scale
        foreach(Transform child in canvasHolder)
            child.localScale = Vector3.one;

    }

    private Sprite RandomSprite(){
        return icons[UnityEngine.Random.Range(0,icons.Length)];
    }

    private Sprite NextSprite(){

        return icons[nextSprite];
    }
}
