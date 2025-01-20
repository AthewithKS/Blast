using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="BoxListConfig",menuName ="Box/Config")]
public class BoxListConfig : ScriptableObject
{
    
    public List<GridRow> BoxRow;
}
[Serializable]
public class GridRow
{
    public List<int> Columns;
}
