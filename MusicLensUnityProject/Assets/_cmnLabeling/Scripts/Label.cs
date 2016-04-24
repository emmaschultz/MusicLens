using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using CWRU.Common.Text;

namespace CWRU.Common.Labeling
{

    public enum LabelCorner
    {
        TOPRIGHT, TOPLEFT,
        BOTTOMRIGHT, BOTTOMLEFT,
        CLOSEST
    }

    [RequireComponent(typeof(MeshFilter))]
    [RequireComponent(typeof(MeshRenderer))]
    public class Label : MonoBehaviour
    {
        [Header("3d Text")]
        public string text;
        public HUITextController Text3D;

        [Header("Dimensions of the label")]
        public float width;
        public float height;
        public float borderThickness;

        public bool backFilled;

        [Header("Pointer Options")]
        public LabelCorner LineCorner;
        public Transform target;

        MeshFilter mf;

        void OnValidate()
        {
            BuildMesh();
        }

        [ExecuteInEditMode]
        void BuildMesh()
        {
            if (!mf)
            {
                mf = GetComponent<MeshFilter>();
            }

            Vector3[] vertexList = new Vector3[]{
                new Vector3(width, height, borderThickness / 2),
                new Vector3(width, height, -borderThickness / 2),
                new Vector3(width - borderThickness, height - borderThickness, -borderThickness/2),
                new Vector3(width - borderThickness, height - borderThickness, borderThickness/2),

                new Vector3(-width, height, borderThickness / 2),
                new Vector3(-width, height, -borderThickness / 2),
                new Vector3(-width + borderThickness, height - borderThickness, -borderThickness / 2),
                new Vector3(-width + borderThickness, height - borderThickness, borderThickness / 2),

                new Vector3(-width, -height, borderThickness / 2),
                new Vector3(-width, -height, -borderThickness / 2),
                new Vector3(-width + borderThickness, -height + borderThickness, -borderThickness / 2),
                new Vector3(-width + borderThickness, -height + borderThickness, borderThickness / 2),

                new Vector3(width, -height, borderThickness / 2),
                new Vector3(width, -height, -borderThickness / 2),
                new Vector3(width - borderThickness, -height + borderThickness, -borderThickness / 2),
                new Vector3(width - borderThickness, -height + borderThickness, borderThickness / 2),

                target?target.position - transform.position:new Vector3(0,0,0)
            };

            #region tris
            int anchor1=1;
            int anchor2=2;
            switch (LineCorner)
            {
                case LabelCorner.TOPRIGHT:
                    anchor1 = 1;
                    anchor2 = 2;
                    break;
                case LabelCorner.TOPLEFT:
                    anchor1 = 5;
                    anchor2 = 6;
                    break;
                case LabelCorner.BOTTOMLEFT:
                    anchor1 = 9;
                    anchor2 = 10;
                    break;
                case LabelCorner.BOTTOMRIGHT:
                    anchor1 = 13;
                    anchor2 = 14;
                    break;
                case LabelCorner.CLOSEST:
                    Vector3 tpos = target.position - transform.position;
                    if (tpos.x >= 0 && tpos.y >= 0)
                    {
                        anchor1 = 1;
                        anchor2 = 2;
                    }
                    if (tpos.x < 0 && tpos.y >= 0)
                    {
                        anchor1 = 5;
                        anchor2 = 6;
                    }
                    if (tpos.x >= 0 && tpos.y < 0)
                    {
                        anchor1 = 13;
                        anchor2 = 14;
                    }
                    if (tpos.x < 0 && tpos.y < 0)
                    {
                        anchor1 = 9;
                        anchor2 = 10;
                    }
                    break;
            }

            int[] triangles = new int[]
            {
                0,1,4,
                1,5,4,
                4,5,8,
                5,9,8,
                8,9,12,
                9,13,12,
                12,13,0,
                13,1,0,

                1,2,6,
                1,6,5,
                5,6,10,
                5,10,9,
                9,10,14,
                9,14,13,
                13,14,2,
                13,2,1,

                2,3,7,
                2,7,6,
                6,7,11,
                6,11,10,
                10,11,15,
                10,15,14,
                14,15,3,
                14,3,2,

                3,0,4,
                3,4,7,
                7,4,8,
                7,8,11,
                11,8,12,
                11,12,15,
                15,12,0,
                15,0,3,

                anchor1,anchor2,16,
                anchor2,anchor1,16
            };
            #endregion

            //Vector3[] normals = new Vector3[]
            //{
            //    new Vector3(1,1,1),
            //    new Vector3(1,1,-1),
            //    new Vector3(-1,-1,-1),
            //    new Vector3(-1,-1,1),

            //    new Vector3(-1,1,1),
            //    new Vector3(-1,1,-1),
            //    new Vector3(1,-1,-1),
            //    new Vector3(1,-1,1),

            //    new Vector3(-1,-1,1),
            //    new Vector3(-1,-1,-1),
            //    new Vector3(1,1,-1),
            //    new Vector3(1,1,1),

            //    new Vector3(1,-1,1),
            //    new Vector3(1,-1,-1),
            //    new Vector3(-1,1,-1),
            //    new Vector3(-1,1,1),

            //    new Vector3(1,1,1)
            //};

            Mesh mesh = new Mesh();
            mesh.vertices = vertexList;
            mesh.triangles = triangles;
            mf.mesh = mesh;
        }

        // Use this for initialization
        void Start()
        {
            Text3D.scaleType = ScaleType.Bounded;
            Text3D.DefaultScale = 2 * width - 6 * borderThickness;
            Text3D.MaxScale = height / 2f;
            Text3D.SetText(text);
            BuildMesh();
        }

        Vector3 lastPos = Vector3.zero;
        Vector3 targetLastPos = Vector3.zero;

        // Update is called once per frame
        void Update()
        {
            if(target.position != targetLastPos || transform.position != lastPos)
            {
                lastPos = transform.position;
                targetLastPos = target.position;
                BuildMesh();
            }
        }
    }
}
