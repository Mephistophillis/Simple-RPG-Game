using System.Collections.Generic;
using UnityEngine;

public class Inventory_Player : Inventory_Base
{
  private Player player;
  public List<Inventory_EquipmentSlot> equipList;

  protected override void Awake()
  {
    base.Awake();
    player = GetComponent<Player>();
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

    // STEP 2: No empty slots? Replace first one
    var slotToReplace = matchingSlots[0];
    var itemToUnequip = slotToReplace.equipedItem;

    EquipItem(inventoryItem, slotToReplace);
    UnequipItem(itemToUnequip);
  }

  public void EquipItem(Inventory_Item itemToEquip, Inventory_EquipmentSlot slot)
  {
    float savedHelthPercent = player.health.GetHealthPercent();
    slot.equipedItem = itemToEquip;
    slot.equipedItem.AddModifiers(player.stats);

    player.health.SetHealthToPercent(savedHelthPercent);
    RemoveItem(itemToEquip);
  }

  public void UnequipItem(Inventory_Item itemToUnequip)
  {
    if (!CanAddItem())
    {
      Debug.Log("Inventory is full!");
      return;
    }

    float savedHelthPercent = player.health.GetHealthPercent();

    var slotToUnequip = equipList.Find(slot => slot.equipedItem == itemToUnequip);

    if (slotToUnequip != null)
      slotToUnequip.equipedItem = null;

    itemToUnequip.RemoveModifiers(player.stats);

    player.health.SetHealthToPercent(savedHelthPercent);
    AddItem(itemToUnequip);
  }
}
