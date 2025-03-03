using UnityEngine;
using UnityEngine.UI;

public class UIStatUpdater : MonoBehaviour
{
    Character player; // Reference to the player script

    [Header("HP UI")]
    public Image hpImage;
    public Sprite[] hpSprites; // 7  HP sprites

    [Header("Stamina UI")]
    public Image staminaImage;
    public Sprite[] staminaSprites; // 37 Stamina sprites

    void Start()
    {
        player = GameManager.instance.player.GetComponent<Character>();
    }

    void Update()
    {
        UpdateHPUI();
        UpdateStaminaUI();
    }

    void UpdateHPUI()
    {
        if (player.hp == null) return;

        int index = Mathf.RoundToInt((float)player.hp.currVal / player.hp.maxVal * (hpSprites.Length - 1));
        hpImage.sprite = hpSprites[Mathf.Clamp(index, 0, hpSprites.Length - 1)];
    }

    void UpdateStaminaUI()
    {
        if (player.stamina == null) return;

        int index = Mathf.RoundToInt((float)player.stamina.currVal / player.stamina.maxVal * (staminaSprites.Length - 1));
        staminaImage.sprite = staminaSprites[Mathf.Clamp(index, 0, staminaSprites.Length - 1)];
    }
}
