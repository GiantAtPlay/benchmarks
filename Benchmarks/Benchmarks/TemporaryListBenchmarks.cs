using System.Buffers;
using BenchmarkDotNet.Attributes;
using Benchmarks.Builders;
using Benchmarks.Models;

namespace Benchmarks.Benchmarks;

[MemoryDiagnoser]
public class TemporaryListBenchmarks
{
    private readonly BenchmarkPerson Person = BenchmarkPersonBuilder.Build();
    public static IEnumerable<object[]> TemporaryListData()
    {
        yield return new object[] { 10 };
        yield return new object[] { 1000 };
        yield return new object[] { 1000000 };
    }
    
    [Benchmark]
    [ArgumentsSource(nameof(TemporaryListData))]
    public void List(int count)
    {
        var list = new List<int>();
        for (int i = 0; i < count; i++)
        {
            list.Add(i);
        }
    }
    
    [Benchmark]
    [ArgumentsSource(nameof(TemporaryListData))]
    public void Array(int count)
    {
        var array = new int[count];
        for (int i = 0; i < count; i++)
        {
            array[i] = i;
        }
    }
    
    [Benchmark]
    [ArgumentsSource(nameof(TemporaryListData))]
    public void SharedArray_Clear(int count)
    {
        var array = ArrayPool<int>.Shared.Rent(count);

        for (int i = 0; i < count; i++)
        {
            array[i] = i;
        }
        
        ArrayPool<int>.Shared.Return(array, true);
    }

    [Benchmark]
    [ArgumentsSource(nameof(TemporaryListData))]
    public void SharedArray_NoClear(int count)
    {
        var array = ArrayPool<int>.Shared.Rent(count);

        for (int i = 0; i < count; i++)
        {
            array[i] = i;
        }
        
        ArrayPool<int>.Shared.Return(array, false);
    }
    
    [Benchmark]
    [ArgumentsSource(nameof(TemporaryListData))]
    public void List_Person(int count)
    {
        var list = new List<BenchmarkPerson>();
        for (int i = 0; i < count; i++)
        {
            list.Add(Person);
        }
    }
    
    [Benchmark]
    [ArgumentsSource(nameof(TemporaryListData))]
    public void Array_Person(int count)
    {
        var array = new BenchmarkPerson[count];
        for (int i = 0; i < count; i++)
        {
            array[i] = Person;
        }
    }
    
    [Benchmark]
    [ArgumentsSource(nameof(TemporaryListData))]
    public void SharedArray_Clear_Person(int count)
    {
        var array = ArrayPool<BenchmarkPerson>.Shared.Rent(count);

        for (int i = 0; i < count; i++)
        {
            array[i] = Person;
        }
        
        ArrayPool<BenchmarkPerson>.Shared.Return(array, true);
    }

    [Benchmark]
    [ArgumentsSource(nameof(TemporaryListData))]
    public void SharedArray_NoClear_Person(int count)
    {
        var array = ArrayPool<BenchmarkPerson>.Shared.Rent(count);

        for (int i = 0; i < count; i++)
        {
            array[i] = Person;
        }
        
        ArrayPool<BenchmarkPerson>.Shared.Return(array, false);
    }
}