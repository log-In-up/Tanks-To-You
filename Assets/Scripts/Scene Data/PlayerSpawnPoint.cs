using UnityEditor;
using UnityEngine;

namespace SceneData
{
#if UNITY_EDITOR
    [DisallowMultipleComponent]
#endif
    public sealed class PlayerSpawnPoint : MonoBehaviour
    {
        [SerializeField] private Vector3 Size;

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            bool selected = Selection.Contains(gameObject);

            Gizmos.color = selected ? Color.green : Color.cyan;

            Vector3[] gridPoints = new Vector3[]
            {
            new Vector3(-Size.x * .5f, -Size.y * .5f, Size.z * .5f),
            new Vector3(Size.x * .5f, -Size.y * .5f, Size.z * .5f),
            new Vector3(Size.x * .5f, -Size.y * .5f, -Size.z * .5f),
            new Vector3(-Size.x * .5f, -Size.y * .5f, -Size.z * .5f),
            new Vector3(-Size.x * .5f, Size.y * .5f, Size.z * .5f),
            new Vector3(Size.x * .5f, Size.y * .5f, Size.z * .5f),
            new Vector3(Size.x * .5f, Size.y * .5f, -Size.z * .5f),
            new Vector3(-Size.x * .5f, Size.y * .5f, -Size.z * .5f)
            };

            Vector3[] vertices = new Vector3[]
            {
            gridPoints[0], gridPoints[1], gridPoints[2], gridPoints[3],
            gridPoints[7], gridPoints[4], gridPoints[0], gridPoints[3],
            gridPoints[4], gridPoints[5], gridPoints[1], gridPoints[0],
            gridPoints[6], gridPoints[7], gridPoints[3], gridPoints[2],
            gridPoints[5], gridPoints[6], gridPoints[2], gridPoints[1],
            gridPoints[7], gridPoints[6], gridPoints[5], gridPoints[4]
            };

            Vector3 up = Vector3.up;
            Vector3 down = Vector3.down;
            Vector3 forward = Vector3.forward;
            Vector3 back = Vector3.back;
            Vector3 left = Vector3.left;
            Vector3 right = Vector3.right;

            Vector3[] normals = new Vector3[]
            {
            down, down, down, down,
            left, left, left, left,
            forward, forward, forward, forward,
            back, back, back, back,
            right, right, right, right,
            up, up, up, up
            };

            Vector2 uv00 = new Vector2(0f, 0f);
            Vector2 uv10 = new Vector2(1f, 0f);
            Vector2 uv01 = new Vector2(0f, 1f);
            Vector2 uv11 = new Vector2(1f, 1f);

            Vector2[] uvs = new Vector2[]
            {
            uv11, uv01, uv00, uv10,
            uv11, uv01, uv00, uv10,
            uv11, uv01, uv00, uv10,
            uv11, uv01, uv00, uv10,
            uv11, uv01, uv00, uv10,
            uv11, uv01, uv00, uv10
            };

            int[] triangles = new int[]
            {
            3, 1, 0, 3, 2, 1,
            7, 5, 4, 7, 6, 5,
            11, 9, 8, 11, 10, 9,
            15, 13, 12, 15, 14, 13,
            19, 17, 16, 19, 18, 17,
            23, 21, 20, 23, 22, 21,
            };

            Mesh mesh = new Mesh();
            mesh.Clear();
            mesh.vertices = vertices;
            mesh.triangles = triangles;
            mesh.normals = normals;
            mesh.uv = uvs;
            mesh.Optimize();

            Gizmos.DrawWireMesh(mesh, transform.position + new Vector3(0.0f, Size.y / 2, 0.0f), transform.rotation, transform.localScale);
        }
#endif
    }
}