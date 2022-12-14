using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiGridRenderer : Graphic
{
    // Start is called before the first frame update
    public float thickness;
    public Vector2Int gridSize = new Vector2Int(1, 1);
    float width;
    float height;
    public float cellWidth;
    public float cellHeight;
    public float xWeight = 1;
    public float yHeight = 1;
    public int originY = 0;

    protected override void OnPopulateMesh(VertexHelper vh)
    {
        vh.Clear();
        width = rectTransform.rect.width;
        height = rectTransform.rect.height;
        cellWidth = width / (float)gridSize.x;
        cellHeight = height / (float)gridSize.y;
        int count = 0;

        for (int y = 0; y < gridSize.y; y++)
        {
            for (int x = 0; x < gridSize.x; x++)
            {
                DrawCell(x, y, count, vh);
                count++;
            }
        }
        DrawOrigin(vh);


    }
    private void DrawOrigin(VertexHelper vh)
    {
        UIVertex vertex = UIVertex.simpleVert;
        float yPos = originY * cellHeight;
        float xPos = cellWidth * gridSize.x;
        List<UIVertex> v = new List<UIVertex>();

        vertex.color = Color.red;
        vertex.position = new Vector3(0, yPos);
        v.Add(vertex);
        vertex.position = new Vector3(xPos, yPos);
        v.Add(vertex);
        vertex.position = new Vector3(0, yPos + 2);
        v.Add(vertex);
        vertex.position = new Vector3(xPos, yPos + 1);
        v.Add(vertex);
        vh.AddUIVertexQuad(v.ToArray());
        v.Clear();
        
        vertex.position = new Vector3(xPos, yPos);
        v.Add(vertex);
        vertex.position = new Vector3(0, yPos);
        v.Add(vertex);
        vertex.position = new Vector3(xPos, yPos + 1);
        v.Add(vertex);
        vertex.position = new Vector3(0, yPos + 2);
        v.Add(vertex);


        vh.AddUIVertexQuad(v.ToArray());


    }
    private void DrawCell(float x, float y, int index, VertexHelper vh)
    {
        float xPos = cellWidth * x;
        float yPos = cellHeight * y;

        UIVertex vertex = UIVertex.simpleVert;

        vertex.color = color;
        vertex.position = new Vector3(xPos, yPos);
        vh.AddVert(vertex);
        vertex.color = color;

        vertex.position = new Vector3(xPos, cellHeight + yPos);
        vh.AddVert(vertex);


        vertex.position = new Vector3(xPos + cellWidth, yPos + cellHeight);
        vh.AddVert(vertex);
        vertex.position = new Vector3(xPos + cellWidth, yPos);
        vh.AddVert(vertex);
        //  vh.AddTriangle(0,1,2);
        //vh.AddTriangle(2,3,0);

        float widthSqr = thickness * thickness;
        float distanceSqr = widthSqr / 2f;
        float distance = Mathf.Sqrt(distanceSqr);
        vertex.position = new Vector3(xPos + distance, yPos + distance);
        vh.AddVert(vertex);
        vertex.position = new Vector3(xPos + distance, yPos + (cellHeight - distance));
        vh.AddVert(vertex);
        vertex.position = new Vector3(xPos + (cellWidth - distance), yPos + (cellHeight - distance));

        vh.AddVert(vertex);
        vertex.position = new Vector3(xPos + (cellWidth - distance), yPos + distance);

        vh.AddVert(vertex);

        int offset = index * 8;
        //left Edge
        vh.AddTriangle(offset + 0, offset + 1, offset + 5);
        vh.AddTriangle(offset + 5, offset + 4, offset + 0);
        //Top Edge

        vh.AddTriangle(offset + 1, offset + 2, offset + 6);
        vh.AddTriangle(offset + 6, offset + 5, offset + 1);
        //Right Edge
        vh.AddTriangle(offset + 2, offset + 3, offset + 7);
        vh.AddTriangle(offset + 7, offset + 6, offset + 2);
        //botom Edge
        vh.AddTriangle(offset + 3, offset + 0, offset + 4);
        vh.AddTriangle(offset + 4, offset + 7, offset + 3);
    }
}
