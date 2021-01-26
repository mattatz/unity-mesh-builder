using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

namespace MeshBuilder
{

    public class IcosahedronBuilder : MeshBuilderBase
    {
        public static Mesh Build(float radius, int details)
        {
            var r = ((1f + Mathf.Sqrt(5f)) / 2);

            var vertices = new List<Vector3>()
            {
                new Vector3(-1, r, 0f),
                new Vector3(1, r, 0f),
                new Vector3(-1, -r, 0f),
                new Vector3(1, -r, 0f),
                new Vector3(0f, -1, r),
                new Vector3(0f, 1, r),
                new Vector3(0f, -1, -r),
                new Vector3(0f, 1, -r),
                new Vector3(r, 0f, -1),
                new Vector3(r, 0f, 1),
                new Vector3(-r, 0f, -1),
                new Vector3(-r, 0f, 1)
            }.Select((v) => v.normalized).ToList();

            var indices = new List<int>()
            {
              0, 11, 5, 0, 5, 1, 0, 1, 7, 0, 7, 10, 0, 10, 11,
              11, 10, 2, 5, 11, 4, 1, 5, 9, 7, 1, 8, 10, 7, 6,
              3, 9, 4, 3, 4, 2, 3, 2, 6, 3, 6, 8, 3, 8, 9,
              9, 8, 1, 4, 9, 5, 2, 4, 11, 6, 2, 10, 8, 6, 7
            };
            return PolyhedronBuilder.Build(vertices, indices, radius, details);
        }
    }

}


