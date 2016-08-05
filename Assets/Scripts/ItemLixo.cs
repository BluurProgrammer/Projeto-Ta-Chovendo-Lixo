using UnityEngine;

public class ItemLixo : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D coll)
    {
        GetComponent<AudioSource>().Stop();
        Destroy(transform.gameObject);
    }
}
