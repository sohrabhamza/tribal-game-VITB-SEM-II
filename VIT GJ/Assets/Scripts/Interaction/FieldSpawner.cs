using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldSpawner : MonoBehaviour
{
    [SerializeField] Vector2 fieldSize = new Vector2(2, 5);
    [SerializeField] GameObject field;
    [SerializeField] Vector2 distBetField = new Vector2(3, 3);

    private void Start()
    {
        CreateField((int)fieldSize.x, (int)fieldSize.y);
    }

    void CreateField(int x, int y)
    {
        Vector3 initPos = transform.position;
        for (int i = 0; i < y; i++)
        {
            for (int j = 0; j < x; j++)
            {
                GameObject newField = Instantiate(field, initPos, Quaternion.identity);
                newField.transform.parent = this.transform;
                initPos.x += distBetField.x;
            }
            initPos.z += distBetField.y;
            initPos.x = transform.position.x;
        }
    }


}
