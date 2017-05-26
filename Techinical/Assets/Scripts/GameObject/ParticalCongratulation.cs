using UnityEngine;
using System.Collections;

public class ParticalCongratulation : MonoBehaviour {
	void OnEnable()
    {
        ManagerObject.Instance.DespawnObjectAfter(this.gameObject, ePoolName.ObjectPool, 1.0f);
    }
}
