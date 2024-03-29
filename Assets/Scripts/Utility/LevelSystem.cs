using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

public class LevelSystem : MonoBehaviour
{
    [SerializeField]
    private int currentPlayerLevel;
    [SerializeField]
    private float currentXP;

    private Spawner spawner;
    [SerializeField]
    private float requiredXP;

    [SerializeField]
    private int playerMaxLevel;

    private bool isMaxLevel;

    private GameObject player;

    [SerializeField]
    private Image xpBar;

    [SerializeField]
    private Text currentLevel;

    public int CurrentPlayerLevel { get => currentPlayerLevel; }

    public static event Action levelUp;


    private void Start()
    {
        currentPlayerLevel = 1;
        currentLevel.text = currentPlayerLevel.ToString();
        currentXP = 0;
        requiredXP = SolveRequiredXP();
    }

    private void OnEnable()
    {
        player = GetComponent<GameManager>().PlayerInstance;
        Enemy.onPlayerKillEvent += GainXp;
    }
    public void GainXp(float xp)
    {
        if (player != null)
        {
            if (!isMaxLevel) currentXP += xp * (1f + player.GetComponent<PlayerController>().XpBonus);
            else player.GetComponent<PlayerController>().ChangeMoney((int)xp/5, false);
            UpdateXPUI();
        }
    }
    private void Update()
    {
        if (currentXP >= requiredXP&&!isMaxLevel)
        {
            LevelUp();
        }

    }

    private void LevelUp()
    {
        currentPlayerLevel++;
        currentLevel.text = currentPlayerLevel.ToString();

        currentXP = Mathf.RoundToInt(currentXP - requiredXP);

        requiredXP = SolveRequiredXP();
        UpdateXPUI();
        levelUp?.Invoke();
    }

    private int SolveRequiredXP()
    {

        int solveForRequiredXp = 0;
        for (int levelCycle = 1; levelCycle <= currentPlayerLevel; levelCycle++)
        {

            solveForRequiredXp += (int)Mathf.Floor(levelCycle + 300f * Mathf.Pow(2f, levelCycle / 7f));
        }

        return solveForRequiredXp / 4;
    }

    private void UpdateXPUI() {

        xpBar.fillAmount = currentXP/requiredXP;
    }
}
