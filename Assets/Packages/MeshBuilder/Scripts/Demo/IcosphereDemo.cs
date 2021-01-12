using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MeshBuilder
{

    [ExecuteInEditMode]
    [RequireComponent (typeof(MeshFilter), typeof(MeshRenderer))]
    public class IcosphereDemo : DemoBase
    {
        [SerializeField, Range(0.5f, 10f)] float radius = 1f;
        [SerializeField, Range(0, 5)] int details = 1;

        protected override void Start()
        {
            base.Start();
            filter.mesh = IcosphereBuilder.Build(radius, details);
        }

    }

}


