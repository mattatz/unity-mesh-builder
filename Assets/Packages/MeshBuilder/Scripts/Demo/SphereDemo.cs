using UnityEngine;
using System.Collections;

namespace MeshBuilder {

    [ExecuteInEditMode]
    [RequireComponent (typeof(MeshFilter), typeof(MeshRenderer))]
    public class SphereDemo : DemoBase {

        [SerializeField, Range(0.5f, 10f)] float radius = 1f;
        [SerializeField, Range(8, 20)] int lonSegments = 10;
        [SerializeField, Range(8, 20)] int latSegments = 10;

        protected override void Build(MeshFilter filter)
        {
            filter.sharedMesh = SphereBuilder.Build(radius, lonSegments, latSegments);
        }

    }

}


