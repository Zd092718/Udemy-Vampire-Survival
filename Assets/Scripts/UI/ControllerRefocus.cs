using UnityEngine;
using UnityEngine.EventSystems;

// If there is no selected item, set the selected item to the event system's first selected item
public class ControllerRefocus : MonoBehaviour {
    private GameObject _lastSelect;

    void Start() {
        _lastSelect = new GameObject();
    }

    // Update is called once per frame
    void Update() {
        if (EventSystem.current.currentSelectedGameObject == null) {
            EventSystem.current.SetSelectedGameObject(_lastSelect);
        } else {
            _lastSelect = EventSystem.current.currentSelectedGameObject;
        }
    }

}