using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Wonderland6627;
using Rect = Wonderland6627.Rect;

public class QuadTreeTest : MonoBehaviour
{
    public QuadTree rootTree;
    public Material[] randomMats;

    public const int MinWidth = 64;

    private void Start()
    {
        /*Rect rect = new Rect(0, 0, 50);
        rootTree = new QuadTree(rect);
        rootTree.Insert(pos);
        ShowQuadTree();*/

        /*rootTree.Divid();
        rootTree.subQuadTree2.Divid();
        rootTree.subQuadTree2.subQuadTree1.Divid();
        rootTree.subQuadTree2.subQuadTree1.subQuadTree3.Divid();
        rootTree.subQuadTree2.subQuadTree1.subQuadTree3.subQuadTree4.Divid();
        rootTree.subQuadTree2.subQuadTree1.subQuadTree3.subQuadTree4.subQuadTree2.Divid();*/

        Rect rect = new Rect(0, 0, 1024);
        rootTree = new QuadTree(rect);
        rootTree.Divid();
        CreateQuadTree(rootTree);
        ShowQuadTree(rootTree);
    }

    private Material RandMat()
    {
        int length = randomMats.Length;
        int rand = Random.Range(0, length);

        return randomMats[rand];
    }

    private void ShowQuadTree(QuadTree quadTree)
    {
        if (quadTree == null || quadTree.subQuadTrees.Count == 0)
        {
            return;
        }

        for (int i = 0; i < quadTree.subQuadTrees.Count; i++)
        {
            quadTree.subQuadTrees[i].rect.Visualize(RandMat());                  
            ShowQuadTree(quadTree.subQuadTrees[i]);
        }
    }

    private void ShowQuadTree()
    {
        foreach (var item in QuadTreeWorld.Instance.TreePlane.GetComponentsInChildren<MeshRenderer>())
        {
            item.material = RandMat();
        }
    }

    /// <summary>
    /// 创建细分 首先在方法外分割一次 然后开始细分 当边长等于最小边长时 停止细分
    /// </summary>
    /// <param name="tree"></param>
    private void CreateQuadTree(QuadTree tree)
    {
        if (tree.rect.width <= 128)
        {
            return;
        }

        for (int i = 0; i < rootTree.subQuadTrees.Count; i++)
        {
            tree.subQuadTrees[i].Divid();
            CreateQuadTree(tree.subQuadTrees[i]);
        }
    }

    public Vector3 pos;
    [ContextMenu("Test")]
    public void Test()
    {
        rootTree.Insert(pos);
        ShowQuadTree(rootTree);
    }
}
