using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField] float sizeX = 1f;
    [SerializeField] float sizeY = 1f;
    [SerializeField] float sizeZ = 1f;
    [SerializeField] int visLength = 40;
    [SerializeField] int visWidth = 40;
    public bool showGrid = true;

    public Vector3 GetPointOnGrid(Vector3 pos)
    {
        pos -= transform.position;
        int xNum = Mathf.RoundToInt(pos.x / sizeX);
        int yNum = Mathf.RoundToInt(pos.y / sizeY);
        int zNum = Mathf.RoundToInt(pos.z / sizeZ);

        Vector3 result = new Vector3(
                (float)xNum * sizeX,
                (float)yNum * sizeY,
                (float)zNum * sizeZ);

        result += transform.position;

        return result;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        if (showGrid)
        {
            for (float x = 0; x < visLength; x += sizeX)
            {
                for (float z = 0; z < visWidth; z += sizeZ)
                {
                    var point = GetPointOnGrid(new Vector3(x, 0f, z));
                    Gizmos.DrawSphere(point, 0.1f);
                }
            }
        }
    }
}
