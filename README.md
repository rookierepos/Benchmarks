# Benchmarks

```
  Count               : Value of the 'Count' parameter
  Mean                : Arithmetic mean of all measurements
  Error               : Half of 99.9% confidence interval
  StdDev              : Standard deviation of all measurements
  Ratio               : Mean of the ratio distribution ([Current]/[Baseline])
  RatioSD             : Standard deviation of the ratio distribution ([Current]/[Baseline])
  Rank                : Relative position of current benchmark mean among all benchmarks (Arabic style)
  ByteSize            : Custom 'ByteSize' tag column
  Gen 0/1k Op         : GC Generation 0 collects per 1k Operations
  Gen 1/1k Op         : GC Generation 1 collects per 1k Operations
  Gen 2/1k Op         : GC Generation 2 collects per 1k Operations
  Allocated Memory/Op : Allocated memory per single operation (managed only, inclusive, 1KB = 1024B)
  1 ns                : 1 Nanosecond (0.000000001 sec)

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.194 (1809/October2018Update/Redstone5)
Intel Xeon CPU E3-1230 v3 3.30GHz, 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-009812
  [Host] : .NET Core 2.2.0 (CoreCLR 4.6.27110.04, CoreFX 4.6.27110.04), 64bit RyuJIT
  Core   : .NET Core 2.2.0 (CoreCLR 4.6.27110.04, CoreFX 4.6.27110.04), 64bit RyuJIT

Job=Core  Runtime=Core
```

## IList vs List

```
| Method | Count |        Mean |       Error |      StdDev | Ratio | RatioSD | Rank | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|------- |------ |------------:|------------:|------------:|------:|--------:|-----:|------------:|------------:|------------:|--------------------:|
|  IList |     1 |    28.35 ns |   0.5766 ns |   0.8451 ns |  1.00 |    0.00 |    2 |      0.0095 |           - |           - |                40 B |
|   List |     1 |    14.03 ns |   0.3163 ns |   0.4536 ns |  0.50 |    0.02 |    1 |           - |           - |           - |                   - |
|        |       |             |             |             |       |         |      |             |             |             |                     |
|  IList |    10 |   108.75 ns |   2.0208 ns |   1.6875 ns |  1.00 |    0.00 |    2 |      0.0094 |           - |           - |                40 B |
|   List |    10 |    36.08 ns |   0.7023 ns |   0.6898 ns |  0.33 |    0.01 |    1 |           - |           - |           - |                   - |
|        |       |             |             |             |       |         |      |             |             |             |                     |
|  IList |   100 |   845.84 ns |  16.7122 ns |  32.5958 ns |  1.00 |    0.00 |    2 |      0.0086 |           - |           - |                40 B |
|   List |   100 |   285.08 ns |   5.6357 ns |   7.5235 ns |  0.34 |    0.01 |    1 |           - |           - |           - |                   - |
|        |       |             |             |             |       |         |      |             |             |             |                     |
|  IList |  1000 | 7,924.61 ns | 156.6244 ns | 252.9192 ns |  1.00 |    0.00 |    2 |           - |           - |           - |                40 B |
|   List |  1000 | 2,644.76 ns |  52.3925 ns |  80.0088 ns |  0.33 |    0.01 |    1 |           - |           - |           - |                   - |
```

## Yield vs NoYield (total:N loop:N/2)

```
|  Method | Count |         Mean |     Error |    StdDev | Ratio | Rank | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|-------- |------ |-------------:|----------:|----------:|------:|-----:|------------:|------------:|------------:|--------------------:|
|   Yield |     1 |     1.987 ms | 0.0081 ms | 0.0075 ms |  1.00 |    1 |           - |           - |           - |                40 B |
| NoYield |     1 |     1.988 ms | 0.0069 ms | 0.0064 ms |  1.00 |    1 |           - |           - |           - |                64 B |
|         |       |              |           |           |       |      |             |             |             |                     |
|   Yield |    10 |    11.912 ms | 0.0255 ms | 0.0213 ms |  1.00 |    1 |           - |           - |           - |                40 B |
| NoYield |    10 |    19.880 ms | 0.0977 ms | 0.0914 ms |  1.67 |    2 |           - |           - |           - |                96 B |
|         |       |              |           |           |       |      |             |             |             |                     |
|   Yield |   100 |   101.382 ms | 0.3552 ms | 0.3323 ms |  1.00 |    1 |           - |           - |           - |                40 B |
| NoYield |   100 |   198.480 ms | 0.6967 ms | 0.6517 ms |  1.96 |    2 |           - |           - |           - |               456 B |
|         |       |              |           |           |       |      |             |             |             |                     |
|   Yield |  1000 |   997.831 ms | 2.6684 ms | 2.4960 ms |  1.00 |    1 |           - |           - |           - |                40 B |
| NoYield |  1000 | 1,991.344 ms | 5.1368 ms | 4.8049 ms |  2.00 |    2 |           - |           - |           - |              4056 B |
```

## MessagePack-Csharp vs Protobuf-net vs Newtonsoft.Json

```
|                 Method |     N |            Mean |            Error |           StdDev | Ratio | RatioSD | Rank |  ByteSize | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|----------------------- |------ |----------------:|-----------------:|-----------------:|------:|--------:|-----:|----------:|------------:|------------:|------------:|--------------------:|
|        MspackSerialize |     1 |        318.1 ns |        24.032 ns |        22.479 ns |  1.00 |    0.00 |    2 |      50 B |      0.0261 |           - |           - |               112 B |
|     LZ4MspackSerialize |     1 |        334.4 ns |        15.696 ns |        14.682 ns |  1.06 |    0.08 |    3 |      50 B |      0.0258 |           - |           - |               112 B |
|         ProtoSerialize |     1 |        868.9 ns |        61.151 ns |        60.058 ns |  2.75 |    0.24 |    4 |      53 B |      0.1591 |           - |           - |               672 B |
|    NewtonJsonSerialize |     1 |      1,474.1 ns |       139.785 ns |       137.287 ns |  4.65 |    0.56 |    5 |     117 B |      0.3873 |           - |           - |              1632 B |
|   SwifterJsonSerialize |     1 |        295.7 ns |         5.184 ns |         4.596 ns |  0.94 |    0.06 |    1 |     113 B |      0.0792 |           - |           - |               336 B |
|                        |       |                 |                  |                  |       |         |      |           |             |             |             |                     |
|      MspackDeserialize |     1 |        248.9 ns |         9.275 ns |         8.676 ns |  1.00 |    0.00 |    1 |         - |      0.0337 |           - |           - |               144 B |
|   LZ4MspackDeserialize |     1 |        253.8 ns |         5.263 ns |         4.395 ns |  1.02 |    0.04 |    1 |         - |      0.0342 |           - |           - |               144 B |
|       ProtoDeserialize |     1 |        678.3 ns |        39.661 ns |        38.952 ns |  2.72 |    0.18 |    2 |         - |      0.0455 |           - |           - |               200 B |
|  NewtonJsonDeserialize |     1 |      2,237.3 ns |        46.472 ns |        41.196 ns |  9.02 |    0.34 |    4 |         - |      0.6664 |           - |           - |              2824 B |
| SwifterJsonDeserialize |     1 |      1,268.1 ns |        86.049 ns |        84.512 ns |  5.12 |    0.44 |    3 |         - |      0.1069 |           - |           - |               464 B |
|                        |       |                 |                  |                  |       |         |      |           |             |             |             |                     |
|        MspackSerialize |   100 |     32,256.5 ns |     3,016.065 ns |     2,962.179 ns |  1.00 |    0.00 |    1 |   4.97 KB |      2.0309 |           - |           - |              8824 B |
|     LZ4MspackSerialize |   100 |     31,841.2 ns |     1,386.643 ns |     1,229.223 ns |  1.00 |    0.09 |    1 |   1.81 KB |      2.0241 |           - |           - |              8824 B |
|         ProtoSerialize |   100 |     82,209.0 ns |     6,057.105 ns |     5,948.887 ns |  2.58 |    0.35 |    2 |   5.45 KB |      6.9124 |           - |           - |             29986 B |
|    NewtonJsonSerialize |   100 |    146,291.3 ns |    12,707.253 ns |    12,480.222 ns |  4.57 |    0.53 |    3 |  11.54 KB |     38.4615 |      0.4634 |           - |            161560 B |
|   SwifterJsonSerialize |   100 |     34,387.6 ns |     4,057.406 ns |     3,984.915 ns |  1.07 |    0.09 |    1 |  11.16 KB |      7.6159 |           - |           - |             31960 B |
|                        |       |                 |                  |                  |       |         |      |           |             |             |             |                     |
|      MspackDeserialize |   100 |     26,336.0 ns |     1,271.769 ns |     1,189.614 ns |  1.00 |    0.00 |    1 |         - |      3.0007 |           - |           - |             12752 B |
|   LZ4MspackDeserialize |   100 |     29,010.8 ns |     2,329.586 ns |     2,179.097 ns |  1.10 |    0.11 |    2 |         - |      3.0228 |           - |           - |             12752 B |
|       ProtoDeserialize |   100 |     61,124.0 ns |     1,285.471 ns |     1,202.430 ns |  2.32 |    0.10 |    3 |         - |      2.3810 |           - |           - |             10496 B |
|  NewtonJsonDeserialize |   100 |    246,071.0 ns |    15,217.777 ns |    14,234.718 ns |  9.37 |    0.81 |    5 |         - |     66.3110 |           - |           - |            280752 B |
| SwifterJsonDeserialize |   100 |    123,796.3 ns |     3,650.971 ns |     3,585.742 ns |  4.70 |    0.23 |    4 |         - |     10.3493 |           - |           - |             44744 B |
|                        |       |                 |                  |                  |       |         |      |           |             |             |             |                     |
|        MspackSerialize | 10000 |  3,543,730.5 ns |   259,393.753 ns |   254,759.350 ns |  1.00 |    0.00 |    1 | 554.11 KB |    144.4444 |     66.6667 |           - |            952032 B |
|     LZ4MspackSerialize | 10000 |  3,422,670.5 ns |   113,944.552 ns |   101,008.878 ns |  0.98 |    0.07 |    1 | 184.41 KB |    146.0674 |     67.4157 |           - |            952032 B |
|         ProtoSerialize | 10000 |  9,906,734.5 ns |   476,532.531 ns |   445,748.825 ns |  2.83 |    0.24 |    3 | 593.41 KB |    484.8485 |    242.4242 |           - |           3039648 B |
|    NewtonJsonSerialize | 10000 | 23,777,302.4 ns | 1,634,585.681 ns | 1,605,381.702 ns |  6.74 |    0.63 |    4 |   1.18 MB |   2923.0769 |    769.2308 |    307.6923 |          16271404 B |
|   SwifterJsonSerialize | 10000 |  5,209,434.8 ns |    98,484.498 ns |    96,724.945 ns |  1.48 |    0.11 |    2 |   1.15 MB |    527.2727 |    254.5455 |           - |           3315168 B |
|                        |       |                 |                  |                  |       |         |      |           |             |             |             |                     |
|      MspackDeserialize | 10000 |  3,249,786.8 ns |    73,194.862 ns |    71,887.142 ns |  1.00 |    0.00 |    1 |         - |    208.3333 |    104.1667 |           - |           1351960 B |
|   LZ4MspackDeserialize | 10000 |  3,379,326.4 ns |    67,382.526 ns |    59,732.854 ns |  1.04 |    0.03 |    2 |         - |    208.3333 |    104.1667 |           - |           1351960 B |
|       ProtoDeserialize | 10000 |  6,789,315.4 ns |   304,988.439 ns |   285,286.375 ns |  2.09 |    0.07 |    3 |         - |    166.6667 |     83.3333 |           - |           1040118 B |
|  NewtonJsonDeserialize | 10000 | 29,873,348.6 ns | 1,205,109.873 ns | 1,068,298.513 ns |  9.18 |    0.35 |    5 |         - |   5900.0000 |    900.0000 |    200.0000 |          28152245 B |
| SwifterJsonDeserialize | 10000 | 16,332,843.5 ns |   680,715.201 ns |   636,741.422 ns |  5.03 |    0.19 |    4 |         - |    736.8421 |    315.7895 |           - |           4550960 B |
```

## Other lab

```
|       Method |             Toolchain | Count |       Mean |     Error |    StdDev | Ratio | RatioSD | Rank | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|------------- |---------------------- |------ |-----------:|----------:|----------:|------:|--------:|-----:|------------:|------------:|------------:|--------------------:|
|  ListForeach |         .NET Core 2.1 |  1000 |   2.712 us | 0.0536 us | 0.0966 us |  1.00 |    0.00 |    1 |           - |           - |           - |                   - |
| IListForeach |         .NET Core 2.1 |  1000 |   8.606 us | 0.1706 us | 0.3288 us |  3.19 |    0.17 |    2 |           - |           - |           - |                40 B |
|       AsSpan |         .NET Core 2.1 |  1000 | 162.842 us | 3.5187 us | 6.8630 us | 59.81 |    3.26 |    3 |     17.0898 |           - |           - |             72048 B |
|      ToArray |         .NET Core 2.1 |  1000 | 161.478 us | 3.2254 us | 6.8035 us | 59.70 |    3.30 |    3 |     17.0898 |           - |           - |             72048 B |
|       ToList |         .NET Core 2.1 |  1000 | 163.792 us | 3.2443 us | 4.7554 us | 60.82 |    3.17 |    3 |     17.0898 |      0.2441 |           - |             72088 B |
|              |                       |       |            |           |           |       |         |      |             |             |             |                     |
|  ListForeach |         .NET Core 2.2 |  1000 |   2.745 us | 0.0542 us | 0.0795 us |  1.00 |    0.00 |    1 |           - |           - |           - |                   - |
| IListForeach |         .NET Core 2.2 |  1000 |   8.206 us | 0.2008 us | 0.2062 us |  2.97 |    0.07 |    2 |           - |           - |           - |                40 B |
|       AsSpan |         .NET Core 2.2 |  1000 | 154.738 us | 3.0812 us | 6.4992 us | 57.34 |    3.35 |    4 |     17.0898 |           - |           - |             72048 B |
|      ToArray |         .NET Core 2.2 |  1000 | 150.098 us | 3.3089 us | 4.7455 us | 54.72 |    2.69 |    3 |     17.0898 |           - |           - |             72048 B |
|       ToList |         .NET Core 2.2 |  1000 | 167.574 us | 3.3496 us | 8.0897 us | 59.57 |    4.00 |    5 |     17.0898 |      0.2441 |           - |             72088 B |
|              |                       |       |            |           |           |       |         |      |             |             |             |                     |
|  ListForeach | .NET Core 3.0 preview |  1000 |   2.847 us | 0.0568 us | 0.0608 us |  1.00 |    0.00 |    1 |           - |           - |           - |                   - |
| IListForeach | .NET Core 3.0 preview |  1000 |   8.191 us | 0.1615 us | 0.2742 us |  2.86 |    0.08 |    2 |           - |           - |           - |                40 B |
|       AsSpan | .NET Core 3.0 preview |  1000 | 163.346 us | 3.2646 us | 8.3094 us | 56.43 |    3.28 |    3 |     15.1367 |           - |           - |             64128 B |
|      ToArray | .NET Core 3.0 preview |  1000 | 159.496 us | 3.1441 us | 4.1973 us | 56.01 |    2.00 |    3 |     15.1367 |           - |           - |             64128 B |
|       ToList | .NET Core 3.0 preview |  1000 | 166.753 us | 3.3657 us | 8.6879 us | 59.12 |    2.50 |    3 |     15.1367 |           - |           - |             64168 B |
```
