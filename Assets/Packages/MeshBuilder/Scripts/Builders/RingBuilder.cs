using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MeshBuilder
{

    // Source: https://github.com/mrdoob/three.js/blob/master/src/geometries/RingBufferGeometry.js
    public class RingBuilder : MeshBuilderBase
    {

        public static Mesh Build(float innerRadius, float outerRadius, int thetaSegments, int phiSegments, float thetaStart = 0f, float thetaLength = Mathf.PI * 2f)
        {
            thetaSegments = Mathf.Max(3, thetaSegments);
            phiSegments = Mathf.Max(1, phiSegments);

            var vertices = new List<Vector3>();
            var normals = new List<Vector3>();
            var uvs = new List<Vector2>();
            var indices = new List<int>();

            var radiusStep = ((outerRadius - innerRadius) / phiSegments);
            var radius = innerRadius;
            for (int j = 0; j <= phiSegments; j++)
            {
                for (int i = 0; i <= thetaSegments; i++)
                {
                    var segment = thetaStart + 1f * i / thetaSegments * thetaLength;
                    var vertex = new Vector3(
                        radius * Mathf.Cos(segment),
                        radius * Mathf.Sin(segment),
                        0f
                    );
                    vertices.Add(vertex);
                    normals.Add(new Vector3(0f, 0f, 1f));
                    uvs.Add(new Vector2((vertex.x / outerRadius + 1) / 2, (vertex.y / outerRadius + 1) / 2));
                }
                radius += radiusStep;
            }

            for (int j = 0; j < phiSegments; j++)
            {
                var thetaSegmentLevel = j * (thetaSegments + 1);
                for (int i = 0; i < thetaSegments; i++)
                {
                    var segment = i + thetaSegmentLevel;
                    var a = segment;
                    var b = segment + thetaSegments + 1;
                    var c = segment + thetaSegments + 2;
                    var d = segment + 1;
                    indices.Add(a); indices.Add(b); indices.Add(d);
                    indices.Add(b); indices.Add(c); indices.Add(d);
                }
            }

            var mesh = new Mesh();
            mesh.SetVertices(vertices);
            mesh.SetNormals(normals);
            mesh.SetUVs(0, uvs);
            mesh.SetIndices(indices, MeshTopology.Triangles, 0);
            mesh.RecalculateBounds();
            return mesh;
        }

    }
}


