using System.Data.Common;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemButton : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI numberText;
    private Item item;
    private int buttonNumber;
   
    private ItemSpawner itemSpawner;
    public void SetItem(Sprite itemSprite, Item item, ItemSpawner spawner, int number){
        numberText.text = $"{number}";
        buttonNumber = number;
        itemSpawner = spawner;
        image.sprite = itemSprite;
        this.item = item;
    }
    void Update(){
        if(CheckPressed()){
            itemSpawner.Build(item);
        }
    }

    private bool CheckPressed(){
        return Input.GetKeyDown(keyCode());
    }

    private KeyCode keyCode(){
        return buttonNumber switch{
            0 => KeyCode.Alpha0,
            1 => KeyCode.Alpha1,
            2 => KeyCode.Alpha2,
            3 => KeyCode.Alpha3,
            4 => KeyCode.Alpha4,
            _=> KeyCode.Alpha0,
        };
    }
}
