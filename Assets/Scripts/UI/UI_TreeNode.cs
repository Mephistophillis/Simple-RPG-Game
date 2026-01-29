using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_TreeNode : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    private UI ui;
    private RectTransform rect;
    private UI_SkillTree skillTree;
    private UI_TreeConnectHandler connectHandler;

    [Header("Unlock details")]
    public UI_TreeNode[] neededNodes;
    public UI_TreeNode[] conflictNodes;
    public bool isUnlocked;
    public bool isLocked;

    [Header("Skill details")]
    [SerializeField] private string skillName;
    [SerializeField] private Image skillIcon;
    public Skill_DataSO skillData;
    [SerializeField] private string lockedColorHex = "#676767";
    [SerializeField] private int skillCost;
    private Color lastColor;

    private void Awake()
    {
        ui = GetComponentInParent<UI>();
        rect = GetComponent<RectTransform>();
        skillTree = GetComponentInParent<UI_SkillTree>();
        connectHandler = GetComponent<UI_TreeConnectHandler>();

        UpdateIconColor(GetColorByHex(lockedColorHex));
    }

    public void Refund()
    {
        isUnlocked = false;
        isLocked = false;
        UpdateIconColor(GetColorByHex(lockedColorHex));

        skillTree.AddSkillPoints(skillData.cost);
        connectHandler.UnlockConnectionImage(false);

        // skill manager and reset skill
    }

    private void Unlock()
    {
        isUnlocked = true;
        UpdateIconColor(Color.white);
        LockConflictNodes();

        skillTree.RemoveSkillPoints(skillData.cost);
        connectHandler.UnlockConnectionImage(true);

        skillTree.skillManager
            .GetSkillByType(skillData.skillType)
            .SetSkillUpgrade(skillData.upgradeData);
    }

    private bool CanBeUlocked()
    {
        if (isLocked || isUnlocked) return false;

        if (!skillTree.EnoughtSkillPoints(skillData.cost))
            return false;

        foreach (var node in neededNodes)
        {
            if (!node.isUnlocked) return false;
        }

        foreach (var node in conflictNodes)
        {
            if (node.isUnlocked) return false;
        }

        return true;
    }

    private void LockConflictNodes()
    {
        foreach (var node in conflictNodes)
            node.isLocked = true;
    }

    private void UpdateIconColor(Color color)
    {
        if (skillIcon == null) return;

        lastColor = skillIcon.color;
        skillIcon.color = color;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (CanBeUlocked()) Unlock();
        else if (isLocked)
            ui.skillToolTip.LockedSkillEffect();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ui.skillToolTip.ShowToolTip(true, rect, this);

        if (!isUnlocked || !isLocked)
            return;

        ToggleNodeHightlight(true);
    }

    private void ToggleNodeHightlight(bool higlight)
    {
        Color hightlihtColor = Color.white * .9f; hightlihtColor.a = 1;
        Color colorToApply = higlight ? hightlihtColor : lastColor;

        UpdateIconColor(colorToApply);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ui.skillToolTip.ShowToolTip(false, rect);

        if (!isUnlocked || !isLocked)
            return;

        ToggleNodeHightlight(false);
    }

    private Color GetColorByHex(string hexNumber)
    {
        ColorUtility.TryParseHtmlString(hexNumber, out Color color);

        return color;
    }

    private void OnDisable()
    {
        if (isLocked)
            UpdateIconColor(GetColorByHex(lockedColorHex));

        if (isUnlocked)
            UpdateIconColor(Color.white);
    }

    private void OnValidate()
    {
        if (skillData == null) return;

        skillName = skillData.displayName;
        skillIcon.sprite = skillData.icon;
        skillCost = skillData.cost;
        gameObject.name = "UI_TreeNode - " + skillData.displayName;
    }

}
