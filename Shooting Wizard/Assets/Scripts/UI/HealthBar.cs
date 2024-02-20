using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    float max_health;
    float current_health;
    PlayerStateManager player;
    public Slider slider;
    // Start is called before the first frame update

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStateManager>();
    }
    void Start()
    {
        slider.maxValue = player.maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = player.Health;
    }
}
