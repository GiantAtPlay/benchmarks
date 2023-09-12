using Benchmarks.Models;

namespace Benchmarks.Mappers;

[Riok.Mapperly.Abstractions.MapperAttribute]
public partial class MapperlyMapper
{
    public partial BenchmarkPersonDto PersonToPersonDto(BenchmarkPerson person);
    public partial BenchmarkPersonDto[] PeopleToPeopleDto(BenchmarkPerson[] people);
}
