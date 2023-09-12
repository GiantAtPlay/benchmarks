using AutoMapper;
using BenchmarkDotNet.Attributes;
using Benchmarks.Builders;
using Benchmarks.Mappers;
using Benchmarks.Models;
using Mapster;
using Nelibur.ObjectMapper;

namespace Benchmarks.Benchmarks;

[MemoryDiagnoser]
public class MappingBenchmarks
{
    internal readonly BenchmarkPerson Person = BenchmarkPersonBuilder.Build();
    internal readonly BenchmarkPerson[] People = BenchmarkPersonBuilder.BuildArray(1000);
    
    private readonly Mapper AutoMapper =
        new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<BenchmarkPerson, BenchmarkPersonDto>()));

    private readonly MapperlyMapper Mapperly = new();

    public MappingBenchmarks()
    {
        TinyMapper.Bind<BenchmarkPerson, BenchmarkPersonDto>();
        TinyMapper.Bind<BenchmarkPerson[], BenchmarkPersonDto[]>();
    }
    
    [Benchmark]
    public BenchmarkPersonDto Person_Automapper()
    {
        return AutoMapper.Map<BenchmarkPerson, BenchmarkPersonDto>(Person);
    }
    
    [Benchmark]
    public BenchmarkPersonDto Person_Manual()
    {
        return Person.MapManually();
    }

    [Benchmark]
    public BenchmarkPersonDto Person_Mapperly()
    {
        return Mapperly.PersonToPersonDto(Person);
    }

    [Benchmark]
    public BenchmarkPersonDto Person_Mapster()
    {
        return Person.Adapt<BenchmarkPersonDto>();
    }
    
    [Benchmark]
    public BenchmarkPersonDto Person_TinyMapper()
    {
        return TinyMapper.Map<BenchmarkPersonDto>(Person);
    }
    
    [Benchmark]
    public BenchmarkPersonDto[] People_Automapper()
    {
        return AutoMapper.Map<BenchmarkPerson[], BenchmarkPersonDto[]>(People);
    }
    
    [Benchmark]
    public BenchmarkPersonDto[] People_Manual()
    {
        return People.MapManually();
    }
    
    [Benchmark]
    public BenchmarkPersonDto[] People_Mapperly()
    {
        return Mapperly.PeopleToPeopleDto(People);
    }
    
    [Benchmark]
    public BenchmarkPersonDto[] People_Mapster()
    {
        return People.Adapt<BenchmarkPersonDto[]>();
    }
    
    [Benchmark]
    public BenchmarkPersonDto[] People_TinyMapper()
    {
        return TinyMapper.Map<BenchmarkPersonDto[]>(People);
    }
}