using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;

    #region  Properties
    [Header("Inventory Attributes")]
    [SerializeField] GameObject itemSlotPrefab;
    [SerializeField] List<GameObject> slotList;
    public Sprite selectedSlotSprite;
    public Sprite defaultSlotSprite;


    [Header("Item Attributes")]
    [SerializeField] List<ItemScriptableObject> itemValues;
    [SerializeField] TMP_Text itemNameText;

    /// <summary>
    /// Cache variable with filled up slots
    /// </summary>
    private List<GameObject> filledSlots = new List<GameObject>();

    /// <summary>
    /// Cache of last instantiated slot for get Image component in GenerateItem() call.
    /// Instantiated here a single time instead to instantiate it in the function foreach call.
    /// </summary>
    private GameObject lastSlotInstantiated;

    private GameObject lastSelectedSlot, newSelectedSlot;

    #endregion

    #region Methods

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        GenerateItem();
    }

    void Update()
    {
        if (InputManager.instance.eventSystem.currentSelectedGameObject?.transform.childCount > 0)
        {
            HoverItemName(InputManager.instance.eventSystem.currentSelectedGameObject.transform.GetChild(0).gameObject);
        }
        else
        {
            HoverItemName();
        }
    }

    public void ItemManagement()
    {
        if (lastSelectedSlot == null)
        {
            AddItem();
        }
        else
        {
            RemoveItem(lastSelectedSlot);
        }
    }

    public void MoveItemEvent()
    {
        if (lastSelectedSlot != null && !InputManager.instance.eventSystem.alreadySelecting)
        {
            MoveItem(lastSelectedSlot, InputManager.instance.eventSystem.currentSelectedGameObject);
            UpdateSelectSlot();
        }
        else
        {
            UpdateSelectSlot();
        }
    }

    public void HoverItemName(GameObject item = null)
    {
        if (item != null)
        {
            itemNameText.text = item.name;
        }
        else
        {
            itemNameText.text = string.Empty;
        }
    }

    void GenerateItem()
    {
        for (int i = 0; i < 5; i++)
        {
            int rndItem = Random.Range(0, itemValues.Count);
            int rndSlot = Random.Range(0, slotList.Count);

            lastSlotInstantiated = Instantiate(itemSlotPrefab, parent: slotList[rndSlot].transform);
            lastSlotInstantiated.GetComponent<Image>().sprite = itemValues[rndItem].Image;
            lastSlotInstantiated.name = itemValues[rndItem].Name;
            filledSlots.Add(slotList[rndSlot]);
            slotList.Remove(slotList[rndSlot]);
        }
    }

    void MoveItem(GameObject oldSlot, GameObject newSlot)
    {
        if (oldSlot.transform.childCount == 0 && newSlot.transform.childCount == 0)
        {
            Debug.Log($"Empty Slot");
            return;
        }
        else if (oldSlot.transform.childCount == 1 && newSlot.transform.childCount == 1)
        {
            oldSlot.transform.GetChild(0).SetParent(newSlot.transform, false);
            newSlot.transform.GetChild(0).SetParent(oldSlot.transform, false);

        }
        else
        {
            oldSlot.transform.GetChild(0).SetParent(newSlot.transform, false);
            filledSlots.Add(newSlot);
            slotList.Remove(newSlot);

            slotList.Add(oldSlot);
            filledSlots.Remove(oldSlot);
        }
        newSelectedSlot = newSlot;

    }

    void UpdateSelectSlot()
    {
        if (lastSelectedSlot != null)
        {
            lastSelectedSlot.GetComponent<Image>().sprite = defaultSlotSprite;
            if (newSelectedSlot != null)
            {
                newSelectedSlot.transform.GetChild(0).gameObject.GetComponent<Animator>().SetBool("ItemClick", false);
                newSelectedSlot = null;
            }
            else
            {
                lastSelectedSlot.transform.GetChild(0).gameObject.GetComponent<Animator>().SetBool("ItemClick", false);
            }
            lastSelectedSlot = null;
        }
        else
        {
            if (InputManager.instance.eventSystem.currentSelectedGameObject != null
                && InputManager.instance.eventSystem.currentSelectedGameObject.transform.childCount > 0)
            {
                lastSelectedSlot = InputManager.instance.eventSystem.currentSelectedGameObject;
                lastSelectedSlot.GetComponent<Image>().sprite = selectedSlotSprite;
                lastSelectedSlot.transform.GetChild(0).gameObject.GetComponent<Animator>().SetBool("ItemClick", true);
            }
        }
    }

    void AddItem()
    {
        ClearInventory();
    }

    void RemoveItem(GameObject item)
    {
        if (item.transform.childCount > 0)
        {
            slotList.Add(item);
            filledSlots.Remove(item);
            UpdateSelectSlot();
            DestroyImmediate(item.transform.GetChild(0).gameObject);
        }
    }
    void ClearInventory()
    {
        foreach (GameObject filledSlot in filledSlots)
        {
            slotList.Add(filledSlot);
            DestroyImmediate(filledSlot.transform.GetChild(0).gameObject);
        }
        filledSlots.Clear();

        GenerateItem();
    }

    #endregion
}
