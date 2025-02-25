using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Image healthBar;
    public Image healthBarBG;
    public Gradient gradient;
    public float healthMax = 100f;
    public Image border;
    public Text generalText;

    void Start()
    {
        healthBar.gameObject.SetActive(false);
        healthBarBG.gameObject.SetActive(false);
        border.gameObject.SetActive(false);
        generalText.gameObject.SetActive(false);
        
    }
    public void takeDamage(float damage)
    {
        healthMax -= damage;
        healthBar.fillAmount = healthMax / 100f;
        UpdateFillColor();
    }
    
    public void heal(float heal)
    {
        healthMax += heal;
        healthMax = Mathf.Clamp(healthMax, 0, 100);
        healthBar.fillAmount = healthMax / 100f;
        UpdateFillColor();
    }
    
    private void UpdateFillColor()
    {
        healthBar.color = gradient.Evaluate(healthBar.fillAmount);
    }
    
    public void ShowBar() 
    {
        gameObject.SetActive(true);
        healthBar.gameObject.SetActive(true);
        healthBarBG.gameObject.SetActive(true);
        border.gameObject.SetActive(true);
        generalText.gameObject.SetActive(true);
    }
}
