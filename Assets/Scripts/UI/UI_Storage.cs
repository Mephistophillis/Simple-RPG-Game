using UnityEngine;
using UnityEngine.Rendering.Universal;

public class UI_Storage : MonoBehaviour
{
  private Inventory_Player inventory;
  private Inventory_Storage storage;

  [SerializeField] private UI_ItemSlotParent inventoryParent;
  [SerializeField] private UI_ItemSlotParent storageParent;

  public void SetupStorage(Inventory_Player inventory, Inventory_Storage storage)
  {
    this.inventory = inventory;
    this.storage = storage;
    storage.OnInventoryChange += UpdateUI;
    UpdateUI();

    UI_StorageSlot[] slots = GetComponentsInChildren<UI_StorageSlot>();
    foreach (var slot in slots)
    {
      slot.SetStorage(storage);
    }
  }

  private void UpdateUI()
  {
    inventoryParent.UpdateSlots(inventory.itemList);
    storageParent.UpdateSlots(storage.itemList);
  }

}
