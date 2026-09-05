using Unity.VisualScripting;
using UnityEngine;

public class Object_Blacksmith : Object_NPC, IInteractable
{
  private Animator anim;
  private Inventory_Player inventory;
  private Inventory_Storage storage;
  private bool isStorageOpened;

  protected override void Awake()
  {
    base.Awake();
    storage = GetComponent<Inventory_Storage>();
    anim = GetComponentInChildren<Animator>();
    anim.SetBool("isBlacksmith", true);
  }

  public void Interact()
  {
    ui.storageUI.SetupStorage(storage);
    ui.craftUI.SetupCraftUI(storage);

    ui.OpenStorageUI(true);
  }

  protected override void OnTriggerEnter2D(Collider2D collision)
  {
    base.OnTriggerEnter2D(collision);
    inventory = player.GetComponent<Inventory_Player>();
    storage.SetInventory(inventory);
  }

  protected override void OnTriggerExit2D(Collider2D collision)
  {
    base.OnTriggerExit2D(collision);
    ui.HideAllToolTips();
    ui.OpenStorageUI(false);
  }
}
