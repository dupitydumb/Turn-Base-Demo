using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace TurnBasedGame {
    

public class StatusPanel : MonoBehaviour
{
    [SerializeField] private Entity entity;
    public Entity Entity { get => entity; set => entity = value; }
    [SerializeField] private TMP_Text entityNameText;
    [SerializeField] private TMP_Text attackPowerText;
    [SerializeField] private TMP_Text defensePowerText;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Slider manaSlider;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        entity.onInitialized += Initialize;
        entity.onBuffChange += UpdateUI;
        entity.onTakeDamage += UpdateUI;
        entity.onManaUse += UpdateUI;
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Initialize()
    {
        entityNameText.text = entity.EntityName;
        attackPowerText.text = entity.AttackPower.ToString();
        defensePowerText.text = entity.Defense.ToString();
        healthSlider.maxValue = entity.MaxHealth;
        healthSlider.value = entity.Health;
        manaSlider.maxValue = entity.MaxMana;
        manaSlider.value = entity.Health;
        Debug.LogWarning(entity.EntityName + " has been initialized.");
    }

    void UpdateUI()
    {
        attackPowerText.text = entity.AttackPower.ToString();
        defensePowerText.text = entity.Defense.ToString();
        healthSlider.value = entity.Health;
        manaSlider.value = entity.Mana;
    }


}
}