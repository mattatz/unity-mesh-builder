using UnityEngine;
using System.Collections;

namespace MeshBuilder {

    [ExecuteInEditMode]
    [RequireComponent (typeof(MeshFilter), typeof(MeshRenderer))]
    public class ConeDemo : DemoBase {

        [SerializeField, Range(5, 20)] int subdivision = 10;
        [SerializeField, Range(0.5f, 10f)] float radius = 1f;
        [SerializeField, Range(0.5f, 10f)] float height = 1f;

        protected override void Build(MeshFilter filter)
        {
            filter.sharedMesh = ConeBuilder.Build(subdivision, radius, height);
        }

    }

}


