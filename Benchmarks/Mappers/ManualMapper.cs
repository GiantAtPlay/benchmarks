using Benchmarks.Models;

namespace Benchmarks.Mappers;

public static class ManualMapper
{
    public static BenchmarkPersonDto MapManually(this BenchmarkPerson person)
    {
        return new BenchmarkPersonDto
        {
            FirstName = person.FirstName,
            LastName = person.LastName,
            DateOfBirth = person.DateOfBirth
        };
    }

    public static BenchmarkPersonDto[] MapManually(this BenchmarkPerson[] people)
    {
        var results = new BenchmarkPersonDto[people.Length];
        for (var i = 0; i < people.Length; i++)
        {
            var dto = people[i].MapManually();
            results[i] = dto;
        }
        return results;
    }
}