using BenchmarkDotNet.Attributes;

namespace Benchmarks.Benchmarks;

[MemoryDiagnoser]
public class LoopBenchmarks
{
    private List<int> _list;
    
    [Params(10, 1000, 10000, 1000000)]
    public int N;
    
    [GlobalSetup]
    public void Setup()
    {
        var array = new int[N];
        Array.Fill<int>(array, 1);
        _list = array.ToList();
    }
    
    [Benchmark]
    public void Linq()
    {
        int counter = 0;
        _list.ForEach(x => counter++ );
    }
    
    [Benchmark]
    public void ForEachLoop()
    {
        int counter = 0;
        foreach(var i in _list) 
        {
            counter++;
        }
    }
    
    [Benchmark]
    public void ForLoop()
    {
        int counter = 0;
        for (int i = 0; i < _list.Count; i++)
        {
            counter++;
        }
    }
    
    [Benchmark]
    public void While()
    {
        int counter = 0;
        while (counter < _list.Count)
        {
            counter++;
        }
    }

    [Benchmark]
    public void Do()
    {
        int counter = 0;
        do
        { 
            counter++;
        } 
        while (counter < N);
    }
}