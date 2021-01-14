using UnityEngine;

using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace MeshBuilder {

    public class CylinderBuilder : MeshBuilderBase {

        public static Mesh Build (float radius, float height, int radialSegments = 10, int heightSegments = 4, bool openEnded = true) {
            radialSegments = Mathf.Max(3, radialSegments);
            heightSegments = Mathf.Max(1, heightSegments);

            var mesh = new Mesh();

            var vertices = new List<Vector3>();
            var normals = new List<Vector3>();
            var triangles = new List<int>();
            var uvs = new List<Vector2>();

            var pi2 = Mathf.PI * 2f;
            var hh = height * 0.5f;

            var invR = 1f / radialSegments;
            var invH = 1f / heightSegments;
            var uy = 1f * height * invH;

            Action<float, float> AddWall = (float fx, float fy) =>
            {
                float rad = fx * pi2;
                var py = fy * height - hh;

                float c = Mathf.Cos(rad), s = Mathf.Sin(rad);
                var top = new Vector3(c * radius, py + uy, s * radius);
                var bottom = new Vector3(c * radius, py, s * radius);

                vertices.Add(top); uvs.Add(new Vector2(fx, fy + invH));
                vertices.Add(bottom); uvs.Add(new Vector2(fx, fy));
                normals.Add(new Vector3(c, 0f, s)); normals.Add(new Vector3(c, 0f, s));
            };

            for (int y = 0; y < heightSegments; y++)
            {
                var fy = 1f * y * invH;
                // Debug.Log(string.Format("{0} = {1}", y, fy));

                for (int x = 0; x < radialSegments; x++) {
                    float fx = 1f * x * invR;
                    int idx = vertices.Count;

                    AddWall(fx, fy);

                    int a = idx, b = idx + 1, c = idx + 2, d = idx + 3;
                    triangles.Add(a);
                    triangles.Add(c);
                    triangles.Add(b);

                    triangles.Add(c);
                    triangles.Add(d);
                    triangles.Add(b);
                }

                AddWall(1f, fy);
            }

            if(openEnded) {

                // Top
                {
                    int top = vertices.Count;
                    vertices.Add(new Vector3(0f, hh, 0f)); // top
                    normals.Add(Vector3.up);
                    uvs.Add(new Vector2(0.5f, 1f));

                    // top side
                    for (int x = 0; x <= radialSegments; x++)
                    {
                        var fx = 1f * x * invR;
                        var rad = fx * pi2;
                        var v = new Vector3(Mathf.Cos(rad) * radius, hh, Mathf.Sin(rad) * radius);
                        vertices.Add(v);
                        normals.Add(Vector3.up);
                        uvs.Add(new Vector2(fx, 1f));
                    }

                    for (int x = 0; x < radialSegments; x++)
                    {
                        triangles.Add(top);
                        triangles.Add(top + 1 + (x + 1) % radialSegments);
                        triangles.Add(top + 1 + x);
                    }
                }

                // Bottom
                {
                    int bottom = vertices.Count;
                    vertices.Add(new Vector3(0f, -hh, 0f)); // bottom
                    normals.Add(Vector3.down);
                    uvs.Add(new Vector2(0.5f, 0f));

                    // bottom side
                    for (int x = 0; x <= radialSegments; x++)
                    {
                        var fx = 1f * x * invR;
                        var rad = fx * pi2;
                        var v = new Vector3(Mathf.Cos(rad) * radius, -hh, Mathf.Sin(rad) * radius);
                        vertices.Add(v);
                        normals.Add(Vector3.down);
                        uvs.Add(new Vector2(fx, 0f));
                    }

                    for (int x = 0; x < radialSegments; x++)
                    {
                        triangles.Add(bottom);
                        triangles.Add(bottom + 1 + x);
                        triangles.Add(bottom + 1 + (x + 1) % radialSegments);
                    }
                }

            }

            mesh.SetVertices(vertices);
            mesh.SetNormals(normals);
            mesh.SetUVs(0, uvs);
            mesh.SetTriangles(triangles.ToArray(), 0);
            mesh.RecalculateBounds();

            return mesh;
        }

    }

}


