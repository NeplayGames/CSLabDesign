using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ItemButton : MonoBehaviour
{
    [SerializeField] private Image image;

    private GameObject item;
    private Button button;

    private Button Button {
        get {
            if(button == null)
                button = GetComponent<Button>(); 
            return button; }
    }
    private ItemSpawner itemSpawner;
    public void SetItem(Sprite itemSprite, GameObject item, ItemSpawner spawner){
        itemSpawner = spawner;
        image.sprite = itemSprite;
        this.item = item;
        Button.onClick.AddListener(OnButtonClicked);
    }

    private void OnButtonClicked()
    {
        itemSpawner.Build(item);
    }

    private void OnDestroy(){
         Button.onClick.RemoveListener(OnButtonClicked);
    }
}
