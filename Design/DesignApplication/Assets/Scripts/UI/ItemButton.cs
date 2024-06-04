using System;
using System.Data.Common;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemButton : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI nameText;
    private Item item;
    private Button button;
    private ItemSpawner itemSpawner;
    public void SetItem(Sprite itemSprite, Item item, ItemSpawner spawner){
        nameText.text = item.itemName;
        itemSpawner = spawner;
        image.sprite = itemSprite;
        this.item = item;
        button = GetComponent<Button>();
        button.onClick.AddListener(ButtonClick);
    }

    private void ButtonClick()
    {
        itemSpawner.Build(item);
    }

    void OnDestroy(){
        button.onClick.RemoveListener(ButtonClick);
    }
    
}
