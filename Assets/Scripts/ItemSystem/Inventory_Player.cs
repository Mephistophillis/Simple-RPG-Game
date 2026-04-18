using System.Collections.Generic;
using UnityEngine;

public class Inventory_Player : Inventory_Base
{
  private Entity_Stats playerStats;
  public List<Inventory_EquipmentSlot> equipList;

  protected override void Awake()
  {
    base.Awake();
    playerStats = GetComponent<Entity_Stats>();
  }

  public void TryEquipItem(Inventory_Item item)
  {
    Inventory_Item inventoryItem = FindItem(item.itemData);
    var matchingSlots = equipList.FindAll(slot => slot.slotType == item.itemData.itemType);

    // STEP 1: Try to find empty slot and equip item
    foreach (var slot in matchingSlots)
    {
      if (!slot.HasItem())
      {
        EquipItem(inventoryItem, slot);
        return;
      }
    }
  }

  public void EquipItem(Inventory_Item itemToEquip, Inventory_EquipmentSlot slot)
  {
    slot.equipmentItem = itemToEquip;
    slot.equipmentItem.AddModifiers(playerStats);

    RemoveItem(itemToEquip);
  }
}
