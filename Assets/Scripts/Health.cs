using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private HealthUI HealthUI;
    public byte maxHealth;
    public byte currentHealth { get; private set; } 
    private Animator anim;
    public bool dead;

    [SerializeField] private float iDuration;
    [SerializeField] private int flashNum;
    private SpriteRenderer renderer;

    [SerializeField] private AudioClip damageSound;


    // Start is called before the first frame update
    void Awake()
    {
        currentHealth = maxHealth;
        anim = GetComponent<Animator>();
        renderer = GetComponent<SpriteRenderer>();
    }

    public void Damage(byte value)
    {
        currentHealth = (byte)((value > currentHealth) ? 0 : currentHealth - value);
        HealthUI.UpdateHearts();
        SoundManagement.instance.PlaySound(damageSound);

        if (currentHealth > 0)
        {
            anim.SetTrigger("hurt");
            StartCoroutine(Immunity());
            return;
        }

        if (!dead)
        {
            anim.SetTrigger("die");
            
            //for player
            if (GetComponent<PlayerMovement>() != null)
            {
                GetComponent<PlayerMovement>().enabled = false;
                GetComponent<PlayerAttack>().enabled = false;
            }

            //for enemy
            if (GetComponent<MeleeBear>() != null)
            {
                GetComponent<MeleeBear>().enabled = false;
            }
            dead = true;
        }
    }

    public void Heal(byte value)
    {
        currentHealth += value;
        currentHealth = (byte)((currentHealth > maxHealth) ? maxHealth : currentHealth);
        HealthUI.UpdateHearts();
    }

    private IEnumerator Immunity()
    {
        Physics2D.IgnoreLayerCollision(7,8,true);
        for (int i = 0; i < flashNum; i++)
        {
            renderer.color = new Color(1f, 0.47f, 0.49f, 0.5f);
            yield return new WaitForSeconds(iDuration / (flashNum * 2));
            renderer.color = Color.white;
            yield return new WaitForSeconds(iDuration / (flashNum * 2));

        }
        Physics2D.IgnoreLayerCollision(7,8,false);
    }

    public void Respawn()
    {
        dead = false;
        Heal(maxHealth);
        anim.ResetTrigger("die");
        anim.Play("idle");
        //StartCoroutine(Immunity());
        if (GetComponent<PlayerMovement>() != null)
        {
            GetComponent<PlayerMovement>().enabled = true;
            GetComponent<PlayerAttack>().enabled = true;
        }
        //for enemy
        if (GetComponent<MeleeBear>() != null)
        {
            GetComponent<MeleeBear>().enabled = true;
        }
    }
}
