using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceLevelController : MonoBehaviour
{
    public static ExperienceLevelController Instance { get; private set; }

    [SerializeField] private int currentExperience;
    [SerializeField] private List<int> expLevels;
    [SerializeField] private int currentLevel = 1, levelCount = 100;

    private void Awake() {
        if(Instance != null) {
            Debug.LogError("There's already an instance of ExperienceLevelController");
        }
        Instance = this;
    }

    private void Start() {
        while(expLevels.Count < levelCount) {
            expLevels.Add(Mathf.CeilToInt(expLevels[expLevels.Count - 1] * 1.1f));
        }
    }

    public void GetExp(int amountToGet) {
        currentExperience += amountToGet;

        if(currentExperience >= expLevels[currentLevel]) {
            LevelUp();
        }
    }

    public void LevelUp() {
        currentExperience -= expLevels[currentLevel];

        currentLevel++;

        if(currentLevel >= expLevels.Count) {
            currentLevel = expLevels.Count - 1;
        }
    }
}
