using System.Runtime.CompilerServices;
using BenchmarkDotNet.Attributes;
using Benchmarks.Builders;
using Benchmarks.Models;
using Bogus;

[assembly: InternalsVisibleTo("Benchmarks.Tests")]
namespace Benchmarks.Benchmarks;

[MemoryDiagnoser]
public class SpliceArrayBenchmarks
{
    private static readonly Faker Faker = new();
    private static readonly int FirstPage = 1;
    private static readonly int LastFullPage = 83;
    
    internal readonly int PageSize = 12;
    internal readonly string[] Strings = new string[1000].Select(_ => Faker.Random.Word()).ToArray();
    internal readonly BenchmarkPerson[] Objects = BenchmarkPersonBuilder.BuildArray(1000);

    public static IEnumerable<object[]> SpliceArrayData()
    {
        yield return new object[] { FirstPage };
        yield return new object[] { 20 };
        yield return new object[] { LastFullPage };
    }

    [Benchmark]
    [ArgumentsSource(nameof(SpliceArrayData))]
    public string[] Strings_SkipTake(int pageNumber)
    {
        return Strings.Skip(PageSize * (pageNumber - 1)).Take(PageSize).ToArray();
    }

    [Benchmark]
    [ArgumentsSource(nameof(SpliceArrayData))]
    public string[] Strings_ArrayCopy(int pageNumber)
    {
        var start = Math.Max(Math.Min(PageSize * (pageNumber - 1), Strings.Length), 0);
        var count = Math.Min(PageSize, Strings.Length - start);
        var results = new string[count];
        Array.Copy(Strings, start, results, 0, count);
        return results;
    }
    
    [Benchmark]
    [ArgumentsSource(nameof(SpliceArrayData))]
    public string[] Strings_AsSpan(int pageNumber)
    {
        var start = Math.Max(Math.Min(PageSize * (pageNumber - 1), Strings.Length), 0);
        var count = Math.Min(PageSize, Strings.Length - start);
        return Strings.AsSpan().Slice(start, count).ToArray();
    }
    
    [Benchmark]
    [ArgumentsSource(nameof(SpliceArrayData))]
    public BenchmarkPerson[] Objects_SkipTake(int pageNumber)
    {
        return Objects.Skip(PageSize * (pageNumber - 1)).Take(PageSize).ToArray();
    }
    
    [Benchmark]
    [ArgumentsSource(nameof(SpliceArrayData))]
    public BenchmarkPerson[] Objects_ArrayCopy(int pageNumber)
    {
        var start = Math.Max(Math.Min(PageSize * (pageNumber - 1), Objects.Length), 0);
        var count = Math.Min(PageSize, Objects.Length - start);
        var results = new BenchmarkPerson[count];
        Array.Copy(Objects, start, results, 0, count);
        return results;
    }
    
    [Benchmark]
    [ArgumentsSource(nameof(SpliceArrayData))]
    public BenchmarkPerson[] Objects_AsSpan(int pageNumber)
    {
        var start = Math.Max(Math.Min(PageSize * (pageNumber - 1), Objects.Length), 0);
        var count = Math.Min(PageSize, Objects.Length - start);
        return Objects.AsSpan().Slice(start, count).ToArray();
    }
}