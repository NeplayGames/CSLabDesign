using System.Data.Common;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemButton : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI numberText;
    [SerializeField] private TextMeshProUGUI nameText;
    private Item item;
    private int buttonNumber;
   
    private ItemSpawner itemSpawner;
    public void SetItem(Sprite itemSprite, Item item, ItemSpawner spawner, int number){
        numberText.text = $"{number}";
        nameText.text = item.itemName;
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
            5 => KeyCode.Alpha5,
            6 => KeyCode.Alpha6,
            7 => KeyCode.Alpha7,
            8 => KeyCode.Alpha8,
            9 => KeyCode.Alpha9,
            _=> KeyCode.Alpha0,
        };
    }
}
