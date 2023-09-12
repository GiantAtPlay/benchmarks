using System.Buffers;
using Benchmarks.Models;
using Bogus;

namespace Benchmarks.Builders;

public static class BenchmarkPersonBuilder
{
    private static readonly Faker Faker = new();
    
    public static BenchmarkPerson[] BuildArray(int count)
    {
        return new BenchmarkPerson[count].Select(_ => Build()).ToArray();
    }

    public static BenchmarkPerson Build()
    {
        return new BenchmarkPerson()
        {
            FirstName = Faker.Name.FirstName(),
            LastName = Faker.Name.LastName(),
            DateOfBirth = Faker.Date.PastDateOnly(95)
        };
    }
}