using System.Collections.Generic;
using BenchmarkDotNet.Attributes;

namespace Dotnetos.AsyncExpert.Homework.Module01.Benchmark
{
    [DisassemblyDiagnoser(exportCombinedDisassemblyReport: true)]
    [MemoryDiagnoser]
    public class FibonacciCalc
    {
        // HOMEWORK:
        // 1. Write implementations for RecursiveWithMemoization and Iterative solutions
        // 2. Add MemoryDiagnoser to the benchmark
        // 3. Run with release configuration and compare results
        // 4. Open disassembler report and compare machine code
        // 
        // You can use the discussion panel to compare your results with other students

        private ulong[] _cache;

        [Benchmark(Baseline = true)]
        [ArgumentsSource(nameof(Data))]
        public ulong Recursive(ulong n)
        {
            if (n == 1 || n == 2) return 1;
            return Recursive(n - 2) + Recursive(n - 1);
        }

        [Benchmark]
        [ArgumentsSource(nameof(Data))]
        public ulong RecursiveWithMemoization(ulong n)
        {
            if (_cache == null) _cache = new ulong[n];

            if (n == 1 || n == 2)
            {
                _cache[n - 1] = 1;
                return 1;
            }

            if (_cache[n - 1] != 0) return _cache[n - 1];

            _cache[n-1] = RecursiveWithMemoization(n - 2) + RecursiveWithMemoization(n - 1);
            return _cache[n - 1];
        }
        
        [Benchmark]
        [ArgumentsSource(nameof(Data))]
        public ulong Iterative(ulong n)
        {
            var (r, a, b) = (0UL, 1UL, 1UL);

            for (ulong i = 2; i < n; ++i)
            {
                //results[i] = results[i - 2] + results[i - 1];
                r = b + a;
                a = b;
                b = r;
            }

            return r;
        }

        public IEnumerable<ulong> Data()
        {
            yield return 15;
            yield return 35;
        }
    }
}
