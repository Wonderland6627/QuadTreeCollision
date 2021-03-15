using UnityEngine;
using System.Collections.Generic;

namespace Wonderland6627
{
    [System.Serializable]
    public class Rect
    {
        public float originX;//原点坐标 (方块的左下角)
        public float originZ;

        public float width;//边长

        public Rect() { }

        public Rect(float _originX, float _originZ, float _width)
        {
            originX = _originX;
            originZ = _originZ;
            width = _width;
        }

        public bool IsInRange(Vector3 pos)
        {
            bool result = pos.x > originX && pos.x < originX + width && pos.z > originZ && pos.z < originZ + width;

            return result;
        }

        public void Visualize(Material mat = null)
        {
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.position = new Vector3(originX + width / 2, QuadTreeWorld.Instance.TreeLeavesCount, originZ + width / 2);
            cube.transform.localScale = new Vector3(width, 1, width);
            cube.transform.SetParent(QuadTreeWorld.Instance.TreePlane);

            if (mat != null)
            {
                cube.GetComponent<MeshRenderer>().material = mat;
            }
        }
    }

    [System.Serializable]
    public class QuadTree
    {
        public Rect rect;

        public QuadTree subQuadTree1;//第一象限的子树
        public QuadTree subQuadTree2;
        public QuadTree subQuadTree3;
        public QuadTree subQuadTree4;

        public List<QuadTree> subQuadTrees = new List<QuadTree>();

        public bool isDivided = false;

        public QuadTree() { }

        public QuadTree(Rect rect)
        {
            this.rect = rect;
            isDivided = false;
        }

        /// <summary>
        /// 插入
        /// </summary>
        public bool Insert(Vector3 pos)
        {
            if (!rect.IsInRange(pos))
            {
                return false;
            }

            if (!isDivided)
            {
                Divid();
            }

            for (int i = 0; i < subQuadTrees.Count; i++)
            {
                if (subQuadTrees[i].Insert(pos))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 分割
        /// </summary>
        public void Divid()
        {
            float originX = rect.originX;
            float originZ = rect.originZ;
            float width = rect.width;

            Rect rect1 = new Rect(originX + width / 2, originZ + width / 2, width / 2);
            subQuadTree1 = new QuadTree(rect1);

            Rect rect2 = new Rect(originX, originZ + width / 2, width / 2);
            subQuadTree2 = new QuadTree(rect2);

            Rect rect3 = new Rect(originX, originZ, width / 2);
            subQuadTree3 = new QuadTree(rect3);

            Rect rect4 = new Rect(originX + width / 2, originZ, width / 2);
            subQuadTree4 = new QuadTree(rect4);

            subQuadTrees.Add(subQuadTree1);
            subQuadTrees.Add(subQuadTree2);
            subQuadTrees.Add(subQuadTree3);
            subQuadTrees.Add(subQuadTree4);

            isDivided = true;
        }
    }
}