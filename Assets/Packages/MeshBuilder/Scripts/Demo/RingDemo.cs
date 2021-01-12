using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MeshBuilder
{

    public class RingDemo : DemoBase
    {
        [SerializeField, Range(0f, 1f)] protected float innerRadius = 0.1f, outerRadius = 1f;
        [SerializeField, Range(2, 64)] protected int thetaSegments = 16, phiSegments = 16;
        [SerializeField, Range(0f, Mathf.PI * 2f)] protected float thetaStart = 0f, thetaLength = Mathf.PI * 2f;

        protected override void Build(MeshFilter filter)
        {
            filter.sharedMesh = RingBuilder.Build(innerRadius, outerRadius, thetaSegments, phiSegments, thetaStart, thetaLength);
        }
    }

}


