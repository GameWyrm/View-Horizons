using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityEngine;

// List of Module data containers from New Horizons
// These should probably all be in there own files, but since I just need them for the data containers I'm not going to bother at the moment
// If anyone else works on this and wants them split, I guess I'll do it
// Comes from https://github.com/Outer-Wilds-New-Horizons/new-horizons/tree/main/NewHorizons/External/Modules
namespace ViewHorizons
{
        public class CuriosityColorInfo
        {
            /// <summary>
            /// The color to apply to entries with this curiosity.
            /// </summary>
            public MColor color;

            /// <summary>
            /// The color to apply to highlighted entries with this curiosity.
            /// </summary>
            public MColor highlightColor;

            /// <summary>
            /// The ID of the curiosity to apply the color to.
            /// </summary>
            public string id;
        }

        public class EntryPositionInfo
        {
            /// <summary>
            /// The name of the entry to apply the position to.
            /// </summary>
            public string id;

            /// <summary>
            /// Position of the entry
            /// </summary>
            public MVector2 position;
        }

    public class AmbientLightModule
    {
        /// <summary>
        /// The range of the light. Defaults to surfaceSize * 2.
        /// </summary>
        [Range(0, double.MaxValue)] public float? outerRadius;

        /// <summary>
        /// The lower radius where the light is brightest, fading in from outerRadius. Defaults to surfaceSize.
        /// </summary>
        [Range(0, double.MaxValue)] public float? innerRadius;

        /// <summary>
        /// The brightness of the light. For reference, Timber Hearth is `1.4`, and Giant's Deep is `0.8`.
        /// </summary>
        [Range(0, double.MaxValue)][DefaultValue(1f)] public float intensity = 1f;

        /// <summary>
        /// The tint of the light
        /// </summary>
        public MColor tint;

        /// <summary>
        /// If true, the light will work as a shell between inner and outer radius.
        /// </summary>
        [DefaultValue(false)] public bool isShell = false;

        /// <summary>
        /// The position of the light
        /// </summary>
        public MVector3 position;
    }

    #region NH Serializable Data
    public class MColor
        {
            public MColor(int r, int g, int b, int a = 255)
            {
                this.r = r;
                this.g = g;
                this.b = b;
                this.a = a;
            }

            public static MColor FromColor(Color color)
            {
                return new MColor((int)(color.r * 255), (int)(color.g * 255), (int)(color.b * 255), (int)(color.a * 255));
            }

            /// <summary>
            /// The red component of this colour from 0-255, higher values will make the colour glow if applicable.
            /// </summary>
            [System.ComponentModel.DataAnnotations.Range(0, int.MaxValue)]
            public int r;

            /// <summary>
            /// The green component of this colour from 0-255, higher values will make the colour glow if applicable.
            /// </summary>
            [System.ComponentModel.DataAnnotations.Range(0, int.MaxValue)]
            public int g;

            /// <summary>
            /// The blue component of this colour from 0-255, higher values will make the colour glow if applicable.
            /// </summary>
            [System.ComponentModel.DataAnnotations.Range(0, int.MaxValue)]
            public int b;

            /// <summary>
            /// The alpha (opacity) component of this colour
            /// </summary>
            [System.ComponentModel.DataAnnotations.Range(0, 255)]
            [DefaultValue(255)]
            public int a = 255;

            public Color ToColor() => new Color(r / 255f, g / 255f, b / 255f, a / 255f);

            public static MColor red => new MColor(255, 0, 0);

            public static MColor green => new MColor(0, 255, 0);

            public static MColor blue => new MColor(0, 0, 255);

            public static MColor white => new MColor(255, 255, 255);

            public static MColor black => new MColor(0, 0, 0);

            public static MColor yellow => new MColor(255, 235, 4);

            public static MColor cyan => new MColor(0, 255, 255);

            public static MColor magenta => new MColor(255, 0, 255);

            public static MColor gray => new MColor(127, 127, 127);

            public static MColor grey => new MColor(127, 127, 127);

            public static MColor clear => new MColor(0, 0, 0, 0);
        }
        
        public class MGradient
        {
            public MGradient(float time, MColor tint)
            {
                this.time = time;
                this.tint = tint;
            }

            public float time;
            public MColor tint;
        }

        public class MMesh
        {
            public MMesh(MVector3[] vertices, int[] triangles, MVector3[] normals, MVector2[] uv, MVector2[] uv2)
            {
                this.vertices = vertices;
                this.triangles = triangles;
                this.normals = normals;
                this.uv = uv;
                this.uv2 = uv2;
            }

            public MVector3[] vertices;
            public int[] triangles;
            public MVector3[] normals;
            public MVector2[] uv;
            public MVector2[] uv2;

            public static implicit operator MMesh(Mesh mesh)
            {
                return new MMesh
                (
                    mesh.vertices.Select(v => (MVector3)v).ToArray(),
                    mesh.triangles,
                    mesh.normals.Select(v => (MVector3)v).ToArray(),
                    mesh.uv.Select(v => (MVector2)v).ToArray(),
                    mesh.uv2.Select(v => (MVector2)v).ToArray()
                );
            }

            public static implicit operator Mesh(MMesh mmesh)
            {
                var mesh = new Mesh();

                mesh.vertices = mmesh.vertices.Select(mv => (Vector3)mv).ToArray();
                mesh.triangles = mmesh.triangles;
                mesh.normals = mmesh.normals.Select(mv => (Vector3)mv).ToArray();
                mesh.uv = mmesh.uv.Select(mv => (Vector2)mv).ToArray();
                mesh.uv2 = mmesh.uv2.Select(mv => (Vector2)mv).ToArray();
                mesh.RecalculateBounds();

                return mesh;
            }
        }

        public class MVector2
        {
            public MVector2(float x, float y)
            {
                this.x = x;
                this.y = y;
            }

            public float x;
            public float y;

            public static implicit operator MVector2(Vector2 vec)
            {
                return new MVector2(vec.x, vec.y);
            }

            public static implicit operator Vector2(MVector2 vec)
            {
                return new Vector2(vec.x, vec.y);
            }
        }

        public class MVector3
        {
            public MVector3(float x, float y, float z)
            {
                this.x = x;
                this.y = y;
                this.z = z;
            }

            public float x;
            public float y;
            public float z;

            public static implicit operator MVector3(Vector3 vec)
            {
                return new MVector3(vec.x, vec.y, vec.z);
            }

            public static implicit operator Vector3(MVector3 vec)
            {
                return new Vector3(vec.x, vec.y, vec.z);
            }

            public override string ToString() => $"{x}, {y}, {z}";
        }
#endregion
}