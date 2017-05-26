using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Vuforia;

[ExecuteInEditMode]
[AddComponentMenu("Layout/Auto Grid Layout Group", 152)]
public class AutoGridLayout : GridLayoutGroup
{
    [SerializeField]
    private bool m_IsColumn;
    [SerializeField]
    public int m_Column = 2, m_Row = 3;
    [SerializeField]
    private float m_ratio = 468.0f/324.0f;

    public int Column
    {
        get { return m_Column; }
        set { m_Column = value; }
    }

    public int Row
    {
        get { return m_Row; }
        set { m_Row = value; }
    }

    public bool IsColumn
    {
        set { m_IsColumn = value; }
    }

    public float Ratio
    {
        get { return m_Column;}
        set { m_ratio = value; }
    }

    public override void CalculateLayoutInputHorizontal()
    {
        base.CalculateLayoutInputHorizontal();
        m_ratio = 468.0f / 324.0f;
        float iColumn = m_Column;
        float iRow = m_Row;
        float iRatio = m_ratio;
        
        float fHeight = (rectTransform.rect.height - ((iRow - 1) * (spacing.y))) - ((padding.top + padding.bottom));
        float fWidth = (rectTransform.rect.width - ((iColumn - 1) * (spacing.x))) - ( (padding.right + padding.left));
        float m = fWidth / iColumn;
        Vector2 vSize = new Vector2(fWidth / iColumn, m* m_ratio);
        cellSize = vSize;
        RectTransform rect = rectTransform;
        int row = transform.childCount / m_Column + (transform.childCount % Column > 0 ? 1 : 0);
        float y = (fHeight + rect.offsetMin.y) - (cellSize.y*row + spacing.y*row - 1);
        y = y < 0 ? y : 0;
        rect.offsetMin = new Vector2(0, y);
    }
}
