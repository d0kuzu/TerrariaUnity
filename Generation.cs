using UnityEngine;

public class Generation : MonoBehaviour
{
    public GameObject Square;
    private Vector3 Vector3;

    void Start()
    {
        Vector3.x = 1.5f;
        Vector3.y = -1;
        for (int i = 0; i < 15; i++)
        {
            Vector3.x -= 1;
            GameObject gameObject = Instantiate(Square, Vector3, Quaternion.identity);
            gameObject.name = Square.name;
        }
        Vector3.y = 0;
        for (int i = 0; i < 15; i++)
        {
            Vector3.x -= 1;
            GameObject gameObject = Instantiate(Square, Vector3, Quaternion.identity);
            gameObject.name = Square.name;
        }
        Vector3.x = 0.5f;
        Vector3.y = -2;
        for (int i = 0; i < 20; i++)
        {
            Vector3.x += 1;
            GameObject gameObject = Instantiate(Square, Vector3, Quaternion.identity);
            gameObject.name = Square.name;
        }
        Vector3.y = -1;
        for (int i = 0; i < 5; i++)
        {
            Vector3.x += 1;
            GameObject gameObject = Instantiate(Square, Vector3, Quaternion.identity);
            gameObject.name = Square.name;
        }
    }
}