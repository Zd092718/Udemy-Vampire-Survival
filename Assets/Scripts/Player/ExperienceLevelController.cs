using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceLevelController : MonoBehaviour
{
    public static ExperienceLevelController Instance { get; private set; }

    [SerializeField] private int currentExperience;

    private void Awake() {
        if(Instance != null) {
            Debug.LogError("There's already an instance of ExperienceLevelController");
        }
        Instance = this;
    }

    public void GetExp(int amountToGet) {
        currentExperience += amountToGet;
    }
}
