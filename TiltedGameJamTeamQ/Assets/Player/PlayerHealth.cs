using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int startingHealth;
    [SerializeField] float maxInvulTime = 4f;
    [SerializeField] GameObject mcSprite;
    [SerializeField] float timeBetweenSpriteFlash = 0.25f;
    [SerializeField] Image mcHealth;

    [SerializeField] GameObject deathFx;

    int currentHealth;

    bool invulnerable = false;

    private void Start()
    {
        currentHealth = startingHealth;
    }

    private void Update()
    {
    }

    IEnumerator Invulnerability()
    {
        invulnerable = true;
        float timer = 0f;
        while(timer <= maxInvulTime)
        {
            mcSprite.SetActive(false);
            yield return new WaitForSeconds(timeBetweenSpriteFlash);
            timer += timeBetweenSpriteFlash;
            mcSprite.SetActive(true);
            yield return new WaitForSeconds(timeBetweenSpriteFlash);
            timer += timeBetweenSpriteFlash;
        }
        invulnerable = false;
    }

    public void EndInvulnerability()
    {
        invulnerable = true;
    }

    public void AllowDamage()
    {
        invulnerable = false;
    }



    public void TakeDamage()
    {
        if(!invulnerable)
        {
            currentHealth -= 1;
            mcHealth.fillAmount -= 0.33f;
            if (currentHealth <= 0)
            {
                Destroy(this.gameObject);
                Instantiate(deathFx, this.transform.position, Quaternion.identity);
                GameDetails.instance.GameOver();
            }
            else
            {
                StartCoroutine(Invulnerability());
            }
        }
    }
}
