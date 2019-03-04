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

## MessagePack-Csharp vs Protobuf-net vs Newtonsoft.Json vs SwifterJson

```
|                           Method |     N |            Mean |            Error |           StdDev | Ratio | RatioSD | Rank |  ByteSize | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|--------------------------------- |------ |----------------:|-----------------:|-----------------:|------:|--------:|-----:|----------:|------------:|------------:|------------:|--------------------:|
|                  **MspackSerialize** |     **1** |        **315.0 ns** |        **10.187 ns** |        **10.005 ns** |  **1.00** |    **0.00** |    **1** |      **50 B** |      **0.0264** |           **-** |           **-** |               **112 B** |
|      MspackContractlessSerialize |     1 |        311.8 ns |         6.750 ns |         5.984 ns |  0.99 |    0.04 |    1 |      50 B |      0.0265 |           - |           - |               112 B |
|               LZ4MspackSerialize |     1 |        330.2 ns |         4.757 ns |         3.972 ns |  1.04 |    0.03 |    2 |      50 B |      0.0262 |           - |           - |               112 B |
|   LZ4MspackContractlessSerialize |     1 |        335.8 ns |         7.733 ns |         7.233 ns |  1.06 |    0.04 |    2 |      50 B |      0.0266 |           - |           - |               112 B |
|                   ProtoSerialize |     1 |        838.1 ns |        12.306 ns |        10.909 ns |  2.65 |    0.09 |    3 |      53 B |      0.1598 |           - |           - |               672 B |
|              NewtonJsonSerialize |     1 |      1,390.8 ns |        24.937 ns |        22.106 ns |  4.40 |    0.13 |    4 |     117 B |      0.3871 |           - |           - |              1632 B |
|             SwifterJsonSerialize |     1 |        329.8 ns |         7.830 ns |         7.324 ns |  1.05 |    0.05 |    2 |     113 B |      0.0796 |           - |           - |               336 B |
|                                  |       |                 |                  |                  |       |         |      |           |             |             |             |                     |
|                MspackDeserialize |     1 |        245.8 ns |         8.035 ns |         7.891 ns |  1.00 |    0.00 |    1 |         - |      0.0340 |           - |           - |               144 B |
|    MspackContractlessDeserialize |     1 |        243.6 ns |         5.953 ns |         5.846 ns |  0.99 |    0.04 |    1 |         - |      0.0338 |           - |           - |               144 B |
|             LZ4MspackDeserialize |     1 |        261.6 ns |         8.300 ns |         8.152 ns |  1.07 |    0.04 |    2 |         - |      0.0339 |           - |           - |               144 B |
| LZ4MspackContractlessDeserialize |     1 |        248.9 ns |         4.359 ns |         4.078 ns |  1.01 |    0.03 |    1 |         - |      0.0340 |           - |           - |               144 B |
|                 ProtoDeserialize |     1 |        669.6 ns |        15.998 ns |        15.713 ns |  2.73 |    0.09 |    3 |         - |      0.0464 |           - |           - |               200 B |
|            NewtonJsonDeserialize |     1 |      2,405.0 ns |        59.459 ns |        58.397 ns |  9.79 |    0.21 |    5 |         - |      0.6690 |           - |           - |              2824 B |
|           SwifterJsonDeserialize |     1 |      1,200.0 ns |        17.528 ns |        16.396 ns |  4.89 |    0.19 |    4 |         - |      0.1092 |           - |           - |               464 B |
|                                  |       |                 |                  |                  |       |         |      |           |             |             |             |                     |
|                  **MspackSerialize** |   **100** |     **30,175.4 ns** |       **618.364 ns** |       **578.418 ns** |  **1.00** |    **0.00** |    **1** |   **4.97 KB** |      **2.0973** |           **-** |           **-** |              **8824 B** |
|      MspackContractlessSerialize |   100 |     30,322.1 ns |       595.145 ns |       556.699 ns |  1.01 |    0.03 |    1 |   4.97 KB |      2.0755 |           - |           - |              8824 B |
|               LZ4MspackSerialize |   100 |     31,516.7 ns |       504.499 ns |       421.280 ns |  1.05 |    0.02 |    2 |   1.79 KB |      2.0767 |           - |           - |              8824 B |
|   LZ4MspackContractlessSerialize |   100 |     32,613.4 ns |       732.276 ns |       719.193 ns |  1.08 |    0.04 |    3 |   1.77 KB |      2.0601 |           - |           - |              8824 B |
|                   ProtoSerialize |   100 |     76,954.9 ns |     1,391.677 ns |     1,301.775 ns |  2.55 |    0.07 |    4 |   5.45 KB |      6.9983 |           - |           - |             29986 B |
|              NewtonJsonSerialize |   100 |    143,168.4 ns |     2,698.070 ns |     2,523.777 ns |  4.75 |    0.11 |    5 |  11.54 KB |     38.1050 |      0.5149 |           - |            161560 B |
|             SwifterJsonSerialize |   100 |     33,304.2 ns |       676.261 ns |       664.179 ns |  1.10 |    0.04 |    3 |  11.16 KB |      7.5213 |           - |           - |             31960 B |
|                                  |       |                 |                  |                  |       |         |      |           |             |             |             |                     |
|                MspackDeserialize |   100 |     26,154.3 ns |       575.494 ns |       538.318 ns |  1.00 |    0.00 |    1 |         - |      3.0283 |           - |           - |             12752 B |
|    MspackContractlessDeserialize |   100 |     28,696.5 ns |     5,970.179 ns |     5,584.509 ns |  1.10 |    0.22 |    1 |         - |      2.9641 |           - |           - |             12752 B |
|             LZ4MspackDeserialize |   100 |     52,712.3 ns |     5,895.888 ns |     5,790.551 ns |  2.01 |    0.21 |    3 |         - |      2.9279 |           - |           - |             12752 B |
| LZ4MspackContractlessDeserialize |   100 |     38,405.4 ns |     4,142.626 ns |     4,068.613 ns |  1.48 |    0.16 |    2 |         - |      3.0140 |           - |           - |             12752 B |
|                 ProtoDeserialize |   100 |     80,123.7 ns |     8,810.618 ns |     8,653.205 ns |  3.09 |    0.34 |    4 |         - |      2.4900 |           - |           - |             10496 B |
|            NewtonJsonDeserialize |   100 |    299,095.2 ns |    28,194.222 ns |    27,690.496 ns | 11.30 |    0.87 |    6 |         - |     66.4794 |           - |           - |            280752 B |
|           SwifterJsonDeserialize |   100 |    159,603.0 ns |    17,644.301 ns |    17,329.063 ns |  6.09 |    0.69 |    5 |         - |     10.6040 |           - |           - |             44744 B |
|                                  |       |                 |                  |                  |       |         |      |           |             |             |             |                     |
|                  **MspackSerialize** | **10000** |  **4,040,426.8 ns** |   **294,649.666 ns** |   **289,385.371 ns** |  **1.00** |    **0.00** |    **2** | **554.11 KB** |    **142.8571** |     **71.4286** |           **-** |            **952032 B** |
|      MspackContractlessSerialize | 10000 |  3,833,145.8 ns |   175,673.282 ns |   164,324.897 ns |  0.96 |    0.07 |    1 | 554.11 KB |    151.8987 |     75.9494 |           - |            952032 B |
|               LZ4MspackSerialize | 10000 |  4,166,945.2 ns |   301,946.448 ns |   296,551.786 ns |  1.04 |    0.09 |    2 | 185.12 KB |    154.9296 |     70.4225 |           - |            952032 B |
|   LZ4MspackContractlessSerialize | 10000 |  4,015,303.4 ns |   201,896.755 ns |   188,854.350 ns |  1.00 |    0.08 |    2 | 183.92 KB |    154.9296 |     70.4225 |           - |            952032 B |
|                   ProtoSerialize | 10000 | 10,785,922.0 ns |   906,150.387 ns |   889,960.843 ns |  2.69 |    0.33 |    4 | 593.42 KB |    482.7586 |    241.3793 |           - |           3039656 B |
|              NewtonJsonSerialize | 10000 | 24,526,419.3 ns | 1,020,953.691 ns | 1,002,713.038 ns |  6.10 |    0.49 |    5 |   1.18 MB |   2916.6667 |    750.0000 |    333.3333 |          16271209 B |
|             SwifterJsonSerialize | 10000 |  6,740,396.7 ns |   358,395.462 ns |   335,243.337 ns |  1.68 |    0.13 |    3 |   1.15 MB |    525.0000 |    250.0000 |           - |           3315168 B |
|                                  |       |                 |                  |                  |       |         |      |           |             |             |             |                     |
|                MspackDeserialize | 10000 |  4,148,571.0 ns |   264,819.313 ns |   260,087.975 ns |  1.00 |    0.00 |    1 |         - |    212.5000 |    100.0000 |           - |           1351960 B |
|    MspackContractlessDeserialize | 10000 |  4,056,341.1 ns |   243,450.941 ns |   215,812.918 ns |  0.97 |    0.06 |    1 |         - |    212.5000 |    100.0000 |           - |           1351960 B |
|             LZ4MspackDeserialize | 10000 |  4,468,625.5 ns |   231,336.879 ns |   227,203.748 ns |  1.08 |    0.09 |    2 |         - |    212.5000 |    100.0000 |           - |           1351960 B |
| LZ4MspackContractlessDeserialize | 10000 |  4,169,612.3 ns |   205,447.922 ns |   201,777.330 ns |  1.01 |    0.08 |    1 |         - |    212.5000 |    100.0000 |           - |           1351960 B |
|                 ProtoDeserialize | 10000 |  7,963,447.3 ns |   396,946.996 ns |   389,855.027 ns |  1.93 |    0.16 |    3 |         - |    166.6667 |     83.3333 |           - |           1040118 B |
|            NewtonJsonDeserialize | 10000 | 36,797,795.2 ns | 1,358,726.344 ns | 1,270,953.465 ns |  8.91 |    0.55 |    5 |         - |   5571.4286 |    571.4286 |           - |          28151960 B |
|           SwifterJsonDeserialize | 10000 | 18,759,580.0 ns | 3,044,302.575 ns | 2,847,642.518 ns |  4.52 |    0.58 |    4 |         - |   1000.0000 |           - |           - |           4551104 B |
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
