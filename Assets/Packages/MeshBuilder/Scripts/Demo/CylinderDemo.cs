using UnityEngine;
using System.Collections;

namespace MeshBuilder {

    [ExecuteInEditMode]
    [RequireComponent (typeof(MeshFilter), typeof(MeshRenderer))]
    public class CylinderDemo : DemoBase {

        [SerializeField] float radius = 1f;
        [SerializeField] float height = 4f;
        [SerializeField] int segments = 8;
        [SerializeField] bool openEnded = false;

        protected override void Start () {
            base.Start();
            filter.mesh = CylinderBuilder.Build(radius, height, segments, openEnded);
        }

    }

}


