using UnityEngine;
using UnityEngine.Rendering.Universal;

public class UI_Storage : MonoBehaviour
{
  private Inventory_Player inventory;
  private Inventory_Storage storage;

  [SerializeField] private UI_ItemSlotParent inventoryParent;
  [SerializeField] private UI_ItemSlotParent storageParent;
  [SerializeField] private UI_ItemSlotParent materialsStashParent;

  public void SetupStorage(Inventory_Storage storage)
  {
    this.storage = storage;
    inventory = storage.playerInventory;
    storage.OnInventoryChange += UpdateUI;
    UpdateUI();

    UI_StorageSlot[] slots = GetComponentsInChildren<UI_StorageSlot>();
    foreach (var slot in slots)
    {
      slot.SetStorage(storage);
    }
  }

  private void OnEnable()
  {
    UpdateUI();
  }

  private void UpdateUI()
  {
    if (storage == null) return;

    inventoryParent.UpdateSlots(inventory.itemList);
    storageParent.UpdateSlots(storage.itemList);
    materialsStashParent.UpdateSlots(storage.materialStash);
  }
}
