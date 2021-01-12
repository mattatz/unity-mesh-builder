using UnityEngine;

using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace MeshBuilder {

    [ExecuteInEditMode]
    [RequireComponent (typeof(MeshFilter), typeof(MeshRenderer))]
    public abstract class DemoBase : MonoBehaviour {

        protected MeshFilter filter;
        protected Material lineMaterial;

        protected virtual void Start () {
            filter = GetComponent<MeshFilter>();
            Build(filter);
        }

        protected abstract void Build(MeshFilter filter);

        protected void OnValidate()
        {
            if (filter == null) filter = GetComponent<MeshFilter>();
            Build(filter);
        }

        protected void OnRenderObject () {
            if (filter == null || filter.sharedMesh == null) return;

            var mesh = filter.sharedMesh;
            var vertices = mesh.vertices;
            var triangles = mesh.triangles;

            CheckInit();
            if(lineMaterial != null) {
                lineMaterial.SetPass(0);
            }

            GL.PushMatrix();
            GL.MultMatrix(transform.localToWorldMatrix);

            GL.Begin(GL.LINES);

            for(int i = 0, n = triangles.Length; i < n; i += 3) {
                var a = vertices[triangles[i]];
                var b = vertices[triangles[i + 1]];
                var c = vertices[triangles[i + 2]];
                GL.Vertex(a); GL.Vertex(b);
                GL.Vertex(b); GL.Vertex(c);
                GL.Vertex(c); GL.Vertex(a);
            }

            GL.End();

            GL.PopMatrix();
        }

        protected void CheckInit () {
            if(lineMaterial == null) {
                Shader shader = Shader.Find("MeshBuilder/DebugLine");
                if (shader == null) return;
                lineMaterial = new Material(shader);
                lineMaterial.hideFlags = HideFlags.HideAndDontSave;
            }
        }

    }

}

