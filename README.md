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

## IList vs List & Yield vs NoYield

```
|  Method | Count |                Mean |             Error |            StdDev | Ratio | RatioSD | Rank | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|-------- |------ |--------------------:|------------------:|------------------:|------:|--------:|-----:|------------:|------------:|------------:|--------------------:|
|   IList |     1 |            31.71 ns |         0.6744 ns |         1.9241 ns |  1.00 |    0.00 |    2 |      0.0095 |           - |           - |                40 B |
|    List |     1 |            16.26 ns |         0.3663 ns |         0.8116 ns |  0.51 |    0.04 |    1 |           - |           - |           - |                   - |
|         |       |                     |                   |                   |       |         |      |             |             |             |                     |
|   Yield |     1 |     1,990,015.40 ns |     5,264.2317 ns |     4,666.6042 ns |  1.00 |    0.00 |    1 |           - |           - |           - |                40 B |
| NoYield |     1 |     1,991,413.52 ns |     4,474.6337 ns |     4,185.5751 ns |  1.00 |    0.00 |    1 |           - |           - |           - |                64 B |
|         |       |                     |                   |                   |       |         |      |             |             |             |                     |
|   IList |    10 |           127.18 ns |         2.5794 ns |         5.8220 ns |  1.00 |    0.00 |    2 |      0.0093 |           - |           - |                40 B |
|    List |    10 |            42.83 ns |         1.0150 ns |         2.9123 ns |  0.33 |    0.03 |    1 |           - |           - |           - |                   - |
|         |       |                     |                   |                   |       |         |      |             |             |             |                     |
|   Yield |    10 |    11,950,059.32 ns |    27,166.2902 ns |    25,411.3647 ns |  1.00 |    0.00 |    1 |           - |           - |           - |                40 B |
| NoYield |    10 |    19,887,310.05 ns |   103,146.2087 ns |    96,483.0276 ns |  1.66 |    0.01 |    2 |           - |           - |           - |                96 B |
|         |       |                     |                   |                   |       |         |      |             |             |             |                     |
|   IList |   100 |           937.40 ns |        18.5009 ns |        45.7296 ns |  1.00 |    0.00 |    2 |      0.0086 |           - |           - |                40 B |
|    List |   100 |           334.12 ns |         8.2474 ns |        24.1881 ns |  0.36 |    0.03 |    1 |           - |           - |           - |                   - |
|         |       |                     |                   |                   |       |         |      |             |             |             |                     |
|   Yield |   100 |   101,449,942.67 ns |   465,846.7274 ns |   435,753.3178 ns |  1.00 |    0.00 |    1 |           - |           - |           - |                40 B |
| NoYield |   100 |   199,264,617.78 ns |   474,979.2256 ns |   444,295.8624 ns |  1.96 |    0.01 |    2 |           - |           - |           - |               456 B |
|         |       |                     |                   |                   |       |         |      |             |             |             |                     |
|   IList |  1000 |         8,881.32 ns |       175.8008 ns |       385.8871 ns |  1.00 |    0.00 |    2 |           - |           - |           - |                40 B |
|    List |  1000 |         2,904.50 ns |        57.9653 ns |       117.0927 ns |  0.33 |    0.02 |    1 |           - |           - |           - |                   - |
|         |       |                     |                   |                   |       |         |      |             |             |             |                     |
|   Yield |  1000 |   997,205,526.67 ns | 2,155,423.1771 ns | 2,016,184.1772 ns |  1.00 |    0.00 |    1 |           - |           - |           - |                40 B |
| NoYield |  1000 | 1,994,124,633.33 ns | 1,744,351.0692 ns | 1,631,667.0724 ns |  2.00 |    0.00 |    2 |           - |           - |           - |              4056 B |
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
