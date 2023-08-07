
const int i = -1;
const int nodeCount = 6;
float[,] nodeValues = new float[nodeCount, nodeCount]
  { { 0,2,i,8,i,i },
    { 2,0,i,5,6,i },
    { i,i,0,i,9,3 },
    { 8,5,i,0,3,2 },
    { i,6,9,3,0,1 },
    { i,i,3,2,1,0 }};

var targetIndex = 2;
var startIndex = 0;

List<ValRow> rows = new List<ValRow>();
for (int j = 0; j < nodeCount; j++)
{
    rows.Add(new ValRow(j, j == startIndex ? 0 : float.PositiveInfinity, null));
}

while (rows.Any(t => t.Discovered == false))
{
    var currentRow = rows.Where(t => t.Discovered == false).OrderBy(t => t.Value).First();
    var connectedIndexes = new List<int>();

    for (int j = 0; j < nodeCount; j++)
    {
        if (nodeValues[currentRow.Index, j] > 0)
            connectedIndexes.Add(j);
    }

    foreach (var item in connectedIndexes)
    {
        var itemWeight = nodeValues[currentRow.Index, item];
        var minVal = Math.Min(rows[item].Value, currentRow.Value + itemWeight);
        if (minVal == currentRow.Value + itemWeight)
        {
            rows[item].Value = minVal;
            rows[item].ParentIndex = currentRow.Index;
        }
    }
    currentRow.Discovered = true;
};

List<int> path = new List<int>();
path.Add(targetIndex);
var nextIndex = rows.FirstOrDefault(t => t.Index == targetIndex).ParentIndex;
path.Add(nextIndex.Value);

while (nextIndex != null)
{
    var currentRow = rows.FirstOrDefault(t => t.Index == nextIndex);
    nextIndex = currentRow.ParentIndex;
    if (nextIndex != null)
    {
        path.Add(nextIndex.Value);
    }
};

path.Reverse();

foreach (var item in path)
{
    Console.Write(item + " -> ");
}





class ValRow
{
    public ValRow(int index, float value, int? parentIndex)
    {
        Index = index;
        Value = value;
        ParentIndex = parentIndex;

    }
    public int Index { get; set; }
    public float Value { get; set; }
    public int? ParentIndex { get; set; }
    public bool Discovered { get; set; }

    public override string ToString()
    {
        return $"{Index} - {Value} - {ParentIndex.GetValueOrDefault()} - {Discovered}";
    }
}