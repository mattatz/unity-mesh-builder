using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MeshBuilder
{

    public class TorusBuilder : MeshBuilderBase
    {

        public static Mesh Build(float radius, float thickness, int radialSegments = 8, int tubularSegments = 8, float arc = Mathf.PI * 2f)
        {
            radialSegments = Mathf.Max(2, radialSegments);
            tubularSegments = Mathf.Max(3, tubularSegments);

            var vertices = new List<Vector3>();
            var normals = new List<Vector3>();
            var uvs = new List<Vector2>();
            var indices = new List<int>();

            for (int y = 0; y <= radialSegments; y++)
            {
                var v = 1f * y / radialSegments * Mathf.PI * 2;
                for (int x = 0; x <= tubularSegments; x++)
                {
                    var u = 1f * x / tubularSegments * arc;

                    var vertex = new Vector3(
                        (radius + thickness * Mathf.Cos(v)) * Mathf.Cos(u),
                        (radius + thickness * Mathf.Cos(v)) * Mathf.Sin(u),
                        thickness * Mathf.Sin(v)
                    );
                    vertices.Add(vertex);

                    var center = new Vector3(
                        radius * Mathf.Cos(u),
                        radius * Mathf.Sin(u),
                        0f
                    );
                    normals.Add((vertex - center).normalized);
                    uvs.Add(new Vector2(1f * x / tubularSegments, 1f * y / radialSegments));
                }
            }

            for (int y = 1; y <= radialSegments; y++)
            {
                for (int x = 1; x <= tubularSegments; x++)
                {
                    var a = (tubularSegments + 1) * y + x - 1;
                    var b = (tubularSegments + 1) * (y - 1) + x - 1;
                    var c = (tubularSegments + 1) * (y - 1) + x;
                    var d = (tubularSegments + 1) * y + x;
                    indices.Add(a); indices.Add(b); indices.Add(d);
                    indices.Add(b); indices.Add(c); indices.Add(d);
                }
            }

            var mesh = new Mesh();
            mesh.SetVertices(vertices);
            mesh.SetNormals(normals);
            mesh.SetUVs(0, uvs);
            mesh.SetIndices(indices, MeshTopology.Triangles, 0);
            mesh.RecalculateBounds();
            return mesh;
        }

    }

}

