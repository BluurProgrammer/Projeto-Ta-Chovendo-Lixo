using UnityEngine;

public class Destruir : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D coll)
    {
        coll.gameObject.GetComponent<AudioSource>().Stop();
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        Destroy(coll.gameObject);
    }
}
