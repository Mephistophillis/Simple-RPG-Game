using UnityEngine;
using UnityEngine.EventSystems;

public class UI_StorageSlot : UI_ItemSlot
{
  private Inventory_Storage storage;

  public enum StorageSlotType { StorageSlot, PlayerInventorySlot }
  public StorageSlotType slotType;
  public void SetStorage(Inventory_Storage storage) => this.storage = storage;

  public override void OnPointerDown(PointerEventData eventData)
  {
    if (itemInSlot == null) return;

    bool transformFullStack = Input.GetKey(KeyCode.LeftControl);

    if (slotType == StorageSlotType.StorageSlot)
      storage.FromStorageToPlayer(itemInSlot, transformFullStack);

    if (slotType == StorageSlotType.PlayerInventorySlot)
      storage.FromPlayerToStorage(itemInSlot, transformFullStack);

    ui.itemToolTip.ShowToolTip(false, null);
  }
}
