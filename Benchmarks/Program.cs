using BenchmarkDotNet.Running;
using Benchmarks.Benchmarks;
using Bogus;

Randomizer.Seed = new Random(123456789);

// BenchmarkRunner.Run<SpliceArrayBenchmarks>();
// BenchmarkRunner.Run<MappingBenchmarks>();
// BenchmarkRunner.Run<TemporaryListBenchmarks>();
BenchmarkRunner.Run<LoopBenchmarks>();