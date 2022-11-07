using UnityEngine;
using UnityEngine.UIElements;

public class Bala : MonoBehaviour
{
    public float speed = 5;
    public float damage;
    public Vector3 direction = new Vector3(0, 0, 1);
    public Vector3 actualScale = new Vector3(0.5f, 0.5f, 0.5f);

    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = actualScale;
    }


    // Update is called once per frame
    void Update()
    {
        // Movimiento Bala
        transform.position += direction * speed * Time.deltaTime;

        // Si presiona space dublica escala actual
        if (Input.GetKeyDown(KeyCode.Space))
        {
            transform.localScale = transform.localScale * 2;

        }

    }
}
