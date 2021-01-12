﻿using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace MeshBuilder
{

    public class IcosphereBuilder : MeshBuilderBase
    {

        public static Mesh Build(float radius, int details)
        {
            var r = (1f + Mathf.Sqrt(5f)) / 2 * radius;

            var vertices = new List<Vector3>()
            {
                new Vector3(-radius, r, 0f),
                new Vector3(radius, r, 0f),
                new Vector3(-radius, -r, 0f),
                new Vector3(radius, -r, 0f),
                new Vector3(0f, -radius, r),
                new Vector3(0f, radius, r),
                new Vector3(0f, -radius, -r),
                new Vector3(0f, radius, -r),
                new Vector3(r, 0f, -radius),
                new Vector3(r, 0f, radius),
                new Vector3(-r, 0f, -radius),
                new Vector3(-r, 0f, radius)
            };

            var indices = new List<int>()
            {
              0, 11, 5, 0, 5, 1, 0, 1, 7, 0, 7, 10, 0, 10, 11,
              11, 10, 2, 5, 11, 4, 1, 5, 9, 7, 1, 8, 10, 7, 6,
              3, 9, 4, 3, 4, 2, 3, 2, 6, 3, 6, 8, 3, 8, 9,
              9, 8, 1, 4, 9, 5, 2, 4, 11, 6, 2, 10, 8, 6, 7
            };

            int v = vertices.Count;
            var midCache = new Dictionary<int, int>();

            Func<int, int, int> addMidPoint = (int a, int b) =>
            {
                var key = Mathf.FloorToInt((a + b) * (a + b + 1) / 2) + Mathf.Min(a, b); // Cantor's pairing function
                if (midCache.ContainsKey(key))
                    return midCache[key];

                var mid = (vertices[a] + vertices[b]) / 2;
                vertices.Add(mid.normalized * radius * 2f);

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
                    var a = addMidPoint(i0, i1);
                    var b = addMidPoint(i1, i2);
                    var c = addMidPoint(i2, i0);
                    indices.Add(i0); indices.Add(a); indices.Add(c);
                    indices.Add(a); indices.Add(i1); indices.Add(b);
                    indices.Add(c); indices.Add(b); indices.Add(i2);
                    indices.Add(a); indices.Add(b); indices.Add(c);
                }
            }

            var mesh = new Mesh();
            mesh.SetVertices(vertices);
            mesh.SetIndices(indices, MeshTopology.Triangles, 0);
            mesh.RecalculateNormals();
            mesh.RecalculateBounds();

            return mesh;
        }

    }

}


