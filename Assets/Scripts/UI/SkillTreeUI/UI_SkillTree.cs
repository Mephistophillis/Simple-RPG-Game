using TMPro;
using UnityEngine;

public class UI_SkillTree : MonoBehaviour
{
  [SerializeField] public int skillPoints;
  [SerializeField] private TextMeshProUGUI skillPointsText;
  [SerializeField] private UI_TreeConnectHandler[] parentNodes;
  private UI_TreeNode[] allTreeNodes;

  public Player_SkillManager skillManager { get; private set; }

  private void Start()
  {
    UpdateAllConnections();
    UpdateSkillPointsUI();
  }

  private void UpdateSkillPointsUI()
  {
    skillPointsText.text = skillPoints.ToString();
  }

  public void UnlockDefaultSkills()
  {
    allTreeNodes = GetComponentsInChildren<UI_TreeNode>(true);
    skillManager = FindAnyObjectByType<Player_SkillManager>();

    foreach (var node in allTreeNodes)
      node.UnlockDefaultSkills();
  }

  public bool EnoughtSkillPoints(int cost) => skillPoints >= cost;
  public void RemoveSkillPoints(int cost)
  {
    skillPoints -= cost;
    UpdateSkillPointsUI();
  }
  public void AddSkillPoints(int points)
  {
    skillPoints += points;
    UpdateSkillPointsUI();
  }

  [ContextMenu("Reset skill Tree")]
  public void RefundAlSkills()
  {
    UI_TreeNode[] skillNodes = GetComponentsInChildren<UI_TreeNode>();

    foreach (var node in skillNodes)
    {
      node.Refund();
    }
  }

  [ContextMenu("Update All Connections")]
  public void UpdateAllConnections()
  {
    foreach (var node in parentNodes)
    {
      node?.UpdateAllConnections();
    }
  }
}
