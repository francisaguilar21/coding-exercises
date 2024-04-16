double solution(int[][] trainingData) {
    var list = new List<int>();
    
    foreach (var row in trainingData)
    {
        if (row[1] == 1)
        {
            list.Add(row[0]);
        }
    }
    
    if (list.Count() == 0)
    {
        return 0;
    }
    
    return list.Average();
}