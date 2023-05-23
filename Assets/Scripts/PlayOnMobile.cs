using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayOnMobile : MonoBehaviour {

    void Start() {
        gameObject.SetActive(Application.isMobilePlatform);
    }
}
