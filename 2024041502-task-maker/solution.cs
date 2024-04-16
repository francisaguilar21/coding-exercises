string[] solution(string[] source, int challengeId) {
    
    var resultString = new List<string>();
    var lastIndex = -1;
    
    for (int index = 0; index <= source.Length - 1; index++)
    {
        Console.WriteLine("{0}", source[index]);
        
        if (!source[index].Contains($"//DB"))
        {
            resultString.Add(source[index]);    
            lastIndex++;
        }
        
        if (source[index].Contains($"//DB "))
        {
            if (source[index].Contains($"//DB {challengeId}//"))
            {
                resultString.RemoveAt(lastIndex);
                var cleanString = source[index].Replace($"//DB {challengeId}//", "");
                resultString.Add(cleanString);
            }
        }
    }
    
    return resultString.ToArray();
}
