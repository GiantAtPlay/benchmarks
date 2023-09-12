using Benchmarks.Models;

namespace Benchmarks.Tests;

public class SpliceArrayTests
{
    private readonly SpliceArrayBenchmarks _sut;
    
    public SpliceArrayTests()
    {
        _sut = new SpliceArrayBenchmarks();
    }

    [Theory]
    [MemberData(nameof(SpliceArrayBenchmarks.SpliceArrayData), MemberType = typeof(SpliceArrayBenchmarks))]
    public void Splice_ReturnsSameResults(int pageNumber)
    {
        var stringSkipTake = _sut.Strings_SkipTake(pageNumber);
        var stringArrayCopy = _sut.Strings_ArrayCopy(pageNumber);
        var stringAsSpan = _sut.Strings_AsSpan(pageNumber);

        var objSkipTake = _sut.Objects_SkipTake(pageNumber);
        var objArrayCopy = _sut.Objects_ArrayCopy(pageNumber);
        var objAsSpan = _sut.Objects_AsSpan(pageNumber);
        
        Assert.Equal(stringSkipTake, stringArrayCopy);
        Assert.Equal(stringSkipTake, stringAsSpan);
        Assert.Equal(stringArrayCopy, stringAsSpan);
        Assert.Equal(objSkipTake, objArrayCopy);
        Assert.Equal(objSkipTake, objAsSpan);
        Assert.Equal(objArrayCopy, objAsSpan);
    }
    
    [Theory]
    [InlineData(84)]
    public void Splice_ReturnsPartialArray_WhenRequestPartiallyExceedsLength(int pageNumber)
    {
        var expectedStrings = _sut.Strings.TakeLast(4).ToArray();
        var expectedObjects = _sut.Objects.TakeLast(4).ToArray();
        
        var stringSkipTake = _sut.Strings_SkipTake(pageNumber);
        var stringArrayCopy = _sut.Strings_ArrayCopy(pageNumber);
        var stringAsSpan = _sut.Strings_AsSpan(pageNumber);

        var objSkipTake = _sut.Objects_SkipTake(pageNumber);
        var objArrayCopy = _sut.Objects_ArrayCopy(pageNumber);
        var objAsSpan = _sut.Objects_AsSpan(pageNumber);
        
        Assert.Equal(expectedStrings, stringSkipTake);
        Assert.Equal(expectedStrings, stringArrayCopy);
        Assert.Equal(expectedStrings, stringAsSpan);
        Assert.Equal(expectedObjects, objSkipTake);
        Assert.Equal(expectedObjects, objArrayCopy);
        Assert.Equal(expectedObjects, objAsSpan);
    }

    [Theory]
    [InlineData(100)]
    public void Splice_ReturnsEmptyArray_WhenRequestExceedsLength(int pageNumber)
    {
        var stringSkipTake = _sut.Strings_SkipTake(pageNumber);
        var stringArrayCopy = _sut.Strings_ArrayCopy(pageNumber);
        var stringAsSpan = _sut.Strings_AsSpan(pageNumber);

        var objSkipTake = _sut.Objects_SkipTake(pageNumber);
        var objArrayCopy = _sut.Objects_ArrayCopy(pageNumber);
        var objAsSpan = _sut.Objects_AsSpan(pageNumber);
        
        Assert.Equal(Array.Empty<string>(), stringSkipTake);
        Assert.Equal(Array.Empty<string>(), stringArrayCopy);
        Assert.Equal(Array.Empty<string>(), stringAsSpan);
        Assert.Equal(Array.Empty<BenchmarkPerson>(), objSkipTake);
        Assert.Equal(Array.Empty<BenchmarkPerson>(), objArrayCopy);
        Assert.Equal(Array.Empty<BenchmarkPerson>(), objAsSpan);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-1000)]
    public void Splice_ReturnsFirstPage_WhenRequestFallShortOfStart(int pageNumber)
    {
        var expectedStrings = _sut.Strings.Take(_sut.PageSize).ToArray();
        var expectedObjects = _sut.Objects.Take(_sut.PageSize).ToArray();

        var stringSkipTake = _sut.Strings_SkipTake(pageNumber);
        var stringArrayCopy = _sut.Strings_ArrayCopy(pageNumber);
        var stringAsSpan = _sut.Strings_AsSpan(pageNumber);

        var objSkipTake = _sut.Objects_SkipTake(pageNumber);
        var objArrayCopy = _sut.Objects_ArrayCopy(pageNumber);
        var objAsSpan = _sut.Objects_AsSpan(pageNumber);
        
        Assert.Equal(expectedStrings, stringSkipTake);
        Assert.Equal(expectedStrings, stringArrayCopy);
        Assert.Equal(expectedStrings, stringAsSpan);
        Assert.Equal(expectedObjects, objSkipTake);
        Assert.Equal(expectedObjects, objArrayCopy);
        Assert.Equal(expectedObjects, objAsSpan);
    }
}