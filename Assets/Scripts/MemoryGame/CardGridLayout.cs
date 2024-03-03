using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class CardGridLayout : LayoutGroup
{
    public int rows;
    public int columns;
    public Vector2 cellSize;
    public Vector2 spacing;
    public float preferredPadding;
    public float bottonPadding;
    public float leftPadding;
    public float sizeX, sizeY;

    public override void CalculateLayoutInputHorizontal()
    {
        base.CalculateLayoutInputHorizontal();
        if (rows == 0 || columns == 0)
        {
            rows = 4;
            columns = 5;
        }

        float parentWidth = rectTransform.rect.width;
        float parentHeight = rectTransform.rect.height;

        float cellHeight = (parentHeight - 2 * preferredPadding - (rows - 1) * spacing.y) / rows;
        float cellWidth = cellHeight;

        cellSize.x = cellWidth * sizeX;
        cellSize.y = cellHeight * sizeY;

        padding.left = Mathf.FloorToInt((parentWidth - columns * cellHeight * leftPadding) / 2);
        padding.top = Mathf.FloorToInt((parentHeight - rows * cellWidth * bottonPadding) / 2);
        padding.bottom = padding.top;

        int columnCount = 0;
        int rowCount = 0;

        for (int i = 0; i < rectChildren.Count; i++)
        {
            rowCount = i / columns;
            columnCount = i % columns;

            var item = rectChildren[i];

            var xPos = padding.left + (cellSize.x * columnCount) + (spacing.x * (columnCount - 1));
            var yPos = padding.top + (cellSize.y * rowCount) + (spacing.y * (rowCount - 1));

            SetChildAlongAxis(item, 0, xPos, cellSize.x);
            SetChildAlongAxis(item, 1, yPos, cellSize.y);
        }
		
    }

    public RectTransform GetRectTransform()
    {
        return rectTransform;
    }

    public override void CalculateLayoutInputVertical()
    {
    }

    public override void SetLayoutHorizontal()
    {
    }

    public override void SetLayoutVertical()
    {
    }
}
