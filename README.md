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
|               Method |     N |            Mean |          Error |           StdDev |          Median | Ratio | RatioSD | Rank |  ByteSize | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|--------------------- |------ |----------------:|---------------:|-----------------:|----------------:|------:|--------:|-----:|----------:|------------:|------------:|------------:|--------------------:|
|      MspackSerialize |     1 |        316.0 ns |       3.576 ns |         2.986 ns |        315.3 ns |  1.00 |    0.00 |    1 |      50 B |      0.0267 |           - |           - |               112 B |
|   LZ4MspackSerialize |     1 |        348.8 ns |       6.960 ns |        12.371 ns |        347.9 ns |  1.08 |    0.04 |    2 |      50 B |      0.0267 |           - |           - |               112 B |
|       ProtoSerialize |     1 |        871.3 ns |      16.434 ns |        14.568 ns |        873.4 ns |  2.76 |    0.06 |    3 |      53 B |      0.1602 |           - |           - |               672 B |
|        JsonSerialize |     1 |      1,478.3 ns |      29.513 ns |        67.811 ns |      1,459.6 ns |  4.80 |    0.29 |    4 |     117 B |      0.3872 |           - |           - |              1632 B |
|                      |       |                 |                |                  |                 |       |         |      |           |             |             |             |                     |
|    MspackDeserialize |     1 |        253.8 ns |       5.116 ns |         5.686 ns |        253.3 ns |  1.00 |    0.00 |    1 |         - |      0.0343 |           - |           - |               144 B |
| LZ4MspackDeserialize |     1 |        280.6 ns |       5.818 ns |        15.629 ns |        276.1 ns |  1.09 |    0.06 |    2 |         - |      0.0343 |           - |           - |               144 B |
|     ProtoDeserialize |     1 |        665.8 ns |      13.072 ns |        15.054 ns |        662.5 ns |  2.63 |    0.10 |    3 |         - |      0.0467 |           - |           - |               200 B |
|      JsonDeserialize |     1 |      2,530.3 ns |      50.253 ns |        76.741 ns |      2,518.8 ns | 10.07 |    0.41 |    4 |         - |      0.6714 |           - |           - |              2824 B |
|                      |       |                 |                |                  |                 |       |         |      |           |             |             |             |                     |
|      MspackSerialize |    10 |      3,120.2 ns |      54.164 ns |        48.015 ns |      3,106.6 ns |  1.00 |    0.00 |    1 |     493 B |      0.2136 |           - |           - |               904 B |
|   LZ4MspackSerialize |    10 |      3,312.8 ns |      56.043 ns |        49.681 ns |      3,319.8 ns |  1.06 |    0.03 |    2 |     238 B |      0.2136 |           - |           - |               904 B |
|       ProtoSerialize |    10 |      8,283.0 ns |     161.682 ns |       158.793 ns |      8,266.1 ns |  2.66 |    0.07 |    3 |     542 B |      0.7935 |           - |           - |              3337 B |
|        JsonSerialize |    10 |     14,478.1 ns |     284.177 ns |       291.829 ns |     14,460.0 ns |  4.64 |    0.11 |    4 |   1.13 KB |      3.8147 |           - |           - |             16112 B |
|                      |       |                 |                |                  |                 |       |         |      |           |             |             |             |                     |
|    MspackDeserialize |    10 |      2,689.6 ns |      51.354 ns |        54.948 ns |      2,680.0 ns |  1.00 |    0.00 |    1 |         - |      0.2899 |           - |           - |              1232 B |
| LZ4MspackDeserialize |    10 |      2,867.5 ns |      45.973 ns |        43.003 ns |      2,864.4 ns |  1.07 |    0.03 |    2 |         - |      0.2899 |           - |           - |              1232 B |
|     ProtoDeserialize |    10 |      6,174.3 ns |     110.475 ns |        92.252 ns |      6,157.2 ns |  2.29 |    0.07 |    3 |         - |      0.2670 |           - |           - |              1136 B |
|      JsonDeserialize |    10 |     24,671.7 ns |     464.718 ns |       456.415 ns |     24,600.1 ns |  9.17 |    0.29 |    4 |         - |      6.6528 |           - |           - |             28032 B |
|                      |       |                 |                |                  |                 |       |         |      |           |             |             |             |                     |
|      MspackSerialize |   100 |     31,154.0 ns |     464.224 ns |       411.522 ns |     31,030.8 ns |  1.00 |    0.00 |    1 |   4.97 KB |      2.0752 |           - |           - |              8824 B |
|   LZ4MspackSerialize |   100 |     32,412.0 ns |     510.097 ns |       452.188 ns |     32,500.8 ns |  1.04 |    0.02 |    2 |   1.75 KB |      2.0752 |           - |           - |              8824 B |
|       ProtoSerialize |   100 |     80,399.2 ns |   1,125.056 ns |       939.473 ns |     80,437.9 ns |  2.58 |    0.04 |    3 |   5.45 KB |      7.0801 |           - |           - |             29985 B |
|        JsonSerialize |   100 |    150,243.5 ns |   4,432.946 ns |     4,146.581 ns |    149,079.7 ns |  4.83 |    0.15 |    4 |  11.54 KB |     38.3301 |      0.2441 |           - |            161552 B |
|                      |       |                 |                |                  |                 |       |         |      |           |             |             |             |                     |
|    MspackDeserialize |   100 |     26,526.0 ns |     364.768 ns |       304.598 ns |     26,567.0 ns |  1.00 |    0.00 |    1 |         - |      3.0212 |           - |           - |             12752 B |
| LZ4MspackDeserialize |   100 |     28,639.1 ns |     542.804 ns |       507.740 ns |     28,542.8 ns |  1.08 |    0.01 |    2 |         - |      3.0212 |           - |           - |             12752 B |
|     ProtoDeserialize |   100 |     63,103.1 ns |     389.497 ns |       304.094 ns |     63,177.8 ns |  2.38 |    0.03 |    3 |         - |      2.4414 |           - |           - |             10496 B |
|      JsonDeserialize |   100 |    255,237.1 ns |   4,060.279 ns |     3,797.988 ns |    255,102.2 ns |  9.62 |    0.14 |    4 |         - |     66.8945 |           - |           - |            280752 B |
|                      |       |                 |                |                  |                 |       |         |      |           |             |             |             |                     |
|      MspackSerialize | 10000 |  3,467,709.2 ns |  57,618.953 ns |    51,077.701 ns |  3,474,731.4 ns |  1.00 |    0.00 |    1 | 554.11 KB |    152.3438 |     74.2188 |           - |            952032 B |
|   LZ4MspackSerialize | 10000 |  3,663,439.0 ns |  47,909.004 ns |    42,470.084 ns |  3,656,085.7 ns |  1.06 |    0.02 |    2 | 186.66 KB |    152.3438 |     74.2188 |           - |            952032 B |
|       ProtoSerialize | 10000 |  9,313,567.8 ns | 193,839.483 ns |   215,452.072 ns |  9,260,687.5 ns |  2.69 |    0.07 |    3 | 593.42 KB |    484.3750 |    234.3750 |           - |           3039617 B |
|        JsonSerialize | 10000 | 24,144,320.6 ns | 477,581.386 ns |   836,445.273 ns | 24,246,256.3 ns |  6.80 |    0.30 |    4 |   1.18 MB |   2968.7500 |    812.5000 |    375.0000 |          16271021 B |
|                      |       |                 |                |                  |                 |       |         |      |           |             |             |             |                     |
|    MspackDeserialize | 10000 |  3,574,820.8 ns |  71,281.429 ns |   169,407.859 ns |  3,512,595.4 ns |  1.00 |    0.00 |    1 |         - |    214.8438 |    105.4688 |           - |           1351960 B |
| LZ4MspackDeserialize | 10000 |  3,877,658.9 ns |  76,486.412 ns |   150,976.716 ns |  3,887,012.0 ns |  1.08 |    0.08 |    2 |         - |    214.8438 |    105.4688 |           - |           1351960 B |
|     ProtoDeserialize | 10000 |  7,327,682.6 ns | 140,374.416 ns |   172,392.389 ns |  7,329,113.3 ns |  2.06 |    0.12 |    3 |         - |    164.0625 |     78.1250 |           - |           1040104 B |
|      JsonDeserialize | 10000 | 35,317,057.7 ns | 778,511.891 ns | 1,363,500.778 ns | 35,011,200.0 ns |  9.82 |    0.67 |    4 |         - |   6142.8571 |   1214.2857 |    428.5714 |          28152499 B |
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
