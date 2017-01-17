using UnityEngine;
using System.Collections;

namespace mattatz.MeshBuilderSystem {

    [ExecuteInEditMode]
    [RequireComponent (typeof(MeshFilter), typeof(MeshRenderer))]
    public class ConeDemo : Demo {

        [SerializeField, Range(5, 20)] int subdivision = 10;
        [SerializeField, Range(0.5f, 10f)] float radius = 1f;
        [SerializeField, Range(0.5f, 10f)] float height = 1f;

        protected override void Start () {
            base.Start();
            filter.mesh = ConeBuilder.Build(subdivision, radius, height);
        }

    }

}


