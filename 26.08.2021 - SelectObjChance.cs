using UnityEngine;

public class SelectObjChance : MonoBehaviour
{
    [SerializeField] private int _countToLeave = 1;

    private void Start() 
    {
        while (transform.childCount > _countToLeave)
        {
            Transform childToDestroy = transform.GetChild(Random.Range(0, transform.childCount)); //уничтожает внутри себя все дочерние объекты, кроме заданного.
            DestroyImmediate(childToDestroy.gameObject);
        }
    }
}
