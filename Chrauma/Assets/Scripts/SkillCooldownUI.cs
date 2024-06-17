using UnityEngine;
using UnityEngine.UI;

public class SkillCooldownUI : MonoBehaviour
{
    public Image skillIcon; 
    public Image cooldownOverlay;

    private float cooldownTime;
    private float cooldownRemaining;

    void Start()
    {
        cooldownOverlay.fillAmount = 0;
    }

    public void StartCooldown(float duration)
    {
        cooldownTime = duration;
        cooldownRemaining = duration;
        cooldownOverlay.fillAmount = 1;
    }

    void Update()
    {
        if (cooldownRemaining > 0)
        {
            cooldownRemaining -= Time.deltaTime;
            cooldownOverlay.fillAmount = cooldownRemaining / cooldownTime;
        }
        else if (cooldownOverlay.fillAmount > 0)
        {
            cooldownOverlay.fillAmount = 0;
        }
    }
}
