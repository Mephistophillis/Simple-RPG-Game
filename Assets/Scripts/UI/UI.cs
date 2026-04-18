using UnityEngine;

public class UI : MonoBehaviour
{
    public UI_SkillTooltip skillToolTip;
    public UI_ItemToolTip itemToolTip;

    public UI_SkillTree skillTree;
    private bool skillTreeEnabled;

    private void Awake()
    {
        skillToolTip = GetComponentInChildren<UI_SkillTooltip>();
        itemToolTip = GetComponentInChildren<UI_ItemToolTip>();
        skillTree = GetComponentInChildren<UI_SkillTree>(true);
    }

    public void ToggleSkillTreeUI()
    {
        skillTreeEnabled = !skillTreeEnabled;
        skillTree.gameObject.SetActive(skillTreeEnabled);
        skillToolTip.ShowToolTip(false, null);
    }
}
