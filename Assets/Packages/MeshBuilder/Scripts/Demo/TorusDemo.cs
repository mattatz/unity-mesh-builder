using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MeshBuilder
{

    public class TorusDemo : DemoBase
    {

        [SerializeField, Range(0.1f, 1f)] protected float radius = 0.5f;
        [SerializeField, Range(0.05f, 0.5f)] protected float thickness = 0.1f;
        [SerializeField, Range(2, 64)] protected int radialSegments = 16;
        [SerializeField, Range(3, 64)] protected int thetaSegments = 8;
        [SerializeField, Range(0f, Mathf.PI * 2f)] protected float thetaStart = 0f;
        [SerializeField, Range(0f, Mathf.PI * 2f)] protected float thetaEnd = Mathf.PI * 2f;

        protected override void Build(MeshFilter filter)
        {
            filter.sharedMesh = TorusBuilder.Build(radius, thickness, radialSegments, thetaSegments, thetaStart, thetaEnd);
        }

    }
}


