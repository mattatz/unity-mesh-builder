using UnityEngine;
using System.Collections;

namespace MeshBuilder {

    [ExecuteInEditMode]
    [RequireComponent (typeof(MeshFilter), typeof(MeshRenderer))]
    public class CylinderDemo : DemoBase {

        [SerializeField] float radius = 1f;
        [SerializeField] float height = 4f;
        [SerializeField, Range(3, 16)] int radialSegments = 8, heightSegments = 4;
        [SerializeField] bool openEnded = false;

        protected override void Build(MeshFilter filter)
        {
            filter.sharedMesh = CylinderBuilder.Build(radius, height, radialSegments, heightSegments, openEnded);
        }

    }

}


