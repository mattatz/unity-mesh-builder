using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MeshBuilder
{

    public class PolyhedronBuilder : MeshBuilderBase
    {

        public static Mesh Build(List<Vector3> vertices, List<int> indices, float radius, int details)
        {
            var midCache = new Dictionary<int, int>();
            Func<int, int, int> MidPoint = (int a, int b) =>
            {
                var key = CalculateCantorPair(a, b);
                if (midCache.ContainsKey(key))
                    return midCache[key];

                var mid = (vertices[a] + vertices[b]) / 2;
                vertices.Add(mid.normalized);

                var idx = vertices.Count - 1;
                midCache.Add(key, idx);

                return idx;
            };

            for (int i = 0; i < details; i++)
            {
                int n = indices.Count;
                for (int k = 0; k < n; k += 3)
                {
                    var i0 = indices[k + 0];
                    var i1 = indices[k + 1];
                    var i2 = indices[k + 2];
                    var a = MidPoint(i0, i1);
                    var b = MidPoint(i1, i2);
                    var c = MidPoint(i2, i0);
                    indices.Add(i0); indices.Add(a); indices.Add(c);
                    indices.Add(a); indices.Add(i1); indices.Add(b);
                    indices.Add(c); indices.Add(b); indices.Add(i2);
                    indices.Add(a); indices.Add(b); indices.Add(c);
                }
            }

            var mesh = new Mesh();
            mesh.SetVertices(vertices.Select(v => v * radius).ToList());
            mesh.SetIndices(indices, MeshTopology.Triangles, 0);
            mesh.RecalculateNormals();
            mesh.RecalculateBounds();

            return mesh;
        }

        // https://medium.com/@PraveenMathew92/cantor-pairing-function-e213a8a89c2b
        protected static int CalculateCantorPair(int k1, int k2)
        {
            int sum = k1 + k2;
            return Mathf.FloorToInt(sum * (sum + 1) / 2) + Mathf.Min(k1, k2);
        }

    }
}


