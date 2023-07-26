using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageNumberController : MonoBehaviour
{
    public static DamageNumberController Instance { get; private set; }

    [SerializeField] private DamageNumber numberToSpawn;
    [SerializeField] private Transform numberCanvas;

    private List<DamageNumber> _numberPool = new List<DamageNumber>();

    private void Awake() {
        if(Instance != null) {
            Debug.LogError("There is already an instance of DamageNumberController");
        }

        Instance = this;
    }

    

    public void SpawnDamage(float damageAmount, Vector3 location, Color textColor) {
        //DamageNumber newDamage = Instantiate(numberToSpawn, location, Quaternion.identity, numberCanvas);

        DamageNumber newDamage = GetFromPool();

        newDamage.Setup(Mathf.RoundToInt(damageAmount));
        newDamage.gameObject.SetActive(true);
        newDamage.transform.position = location;
        newDamage.GetDamageNumberText().color = textColor;
    }

    public DamageNumber GetFromPool() {
        DamageNumber numberToOutput = null;

        if(_numberPool.Count == 0) {
            numberToOutput = Instantiate(numberToSpawn, numberCanvas);
        } else {
            numberToOutput = _numberPool[0];
            _numberPool.RemoveAt(0);
        }

        return numberToOutput;
    }

    public void PlaceInPool(DamageNumber numberToPlace) {
        numberToPlace.gameObject.SetActive(false);
        _numberPool.Add(numberToPlace);
    }
}
