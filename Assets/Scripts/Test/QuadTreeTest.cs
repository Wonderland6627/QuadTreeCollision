using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Wonderland6627;
using Rect = Wonderland6627.Rect;

public class QuadTreeTest : MonoBehaviour
{
    public QuadTree quadTree;
    public Material[] randomMats;

    private void Start()
    {
        Rect rect = new Rect(0, 0, 50);
        quadTree = new QuadTree(rect);
        quadTree.Divid();

        ShowQuadTree();
    }

    private Material RandMat()
    {
        int length = randomMats.Length;
        int rand = Random.Range(0, length);

        return randomMats[rand];
    }

    private void ShowQuadTree()
    {
        quadTree.rect.Visualize(RandMat());
        quadTree.subQuadTree1.rect.Visualize(RandMat());
        quadTree.subQuadTree2.rect.Visualize(RandMat());
        quadTree.subQuadTree3.rect.Visualize(RandMat());
        quadTree.subQuadTree4.rect.Visualize(RandMat());
    }
}
