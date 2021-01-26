using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

namespace MeshBuilder
{

    public class OctahedronBuilder : MeshBuilderBase
    {
        public static Mesh Build(float radius, int details)
        {
            var vertices = new List<Vector3>(){
                new Vector3(1, 0, 0), new Vector3(-1, 0, 0), new Vector3(0, 1, 0),
                new Vector3(0, -1, 0), new Vector3(0, 0, 1), new Vector3(0, 0, -1)
            };
            var indices = new List<int>() {
                0, 2, 4, 0, 4, 3, 0, 3, 5,
                0, 5, 2, 1, 2, 5, 1, 5, 3,
                1, 3, 4, 1, 4, 2
            };
            return PolyhedronBuilder.Build(vertices, indices, radius, details);
        }
    }

}


