using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Bottom_UI : MonoBehaviour
{
    [SerializeField]
    private Slider _healthGlobe;
    [SerializeField]
    private GameObject _manaBar;
    [SerializeField]
    private GameObject _staminaBar;

    private Player_Controller _player;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindWithTag("Player").GetComponent<Player_Controller>();
    }

    // Update is called once per frame
    void Update()
    {
        // Health Globe
        float _hpPercentage = (float)_player.GetHealth() / (float)_player.GetMaxHealth();

        _healthGlobe.value = _hpPercentage;

        // Health Globe
        float _manaPercentage = (float)_player.GetMana() / (float)_player.GetMaxMana();

        _manaBar.transform.localScale = new Vector3(_manaPercentage, 1, 1);

        // Health Globe
        float _staminaPercentage = (float)_player.GetStamina() / (float)_player.GetMaxStamina();

        _staminaBar.transform.localScale = new Vector3(_staminaPercentage, 1, 1);
    }
}
