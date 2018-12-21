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
|               Method | Count |            Mean |          Error |         StdDev | Ratio | RatioSD | Rank |  ByteSize | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|--------------------- |------ |----------------:|---------------:|---------------:|------:|--------:|-----:|----------:|------------:|------------:|------------:|--------------------:|
|      MspackSerialize |     1 |        446.4 ns |       6.568 ns |       5.823 ns |  1.00 |    0.00 |    1 |      75 B |      0.0472 |           - |           - |               200 B |
|   LZ4MspackSerialize |     1 |        833.3 ns |      16.474 ns |      17.627 ns |  1.87 |    0.05 |    2 |      88 B |      0.0486 |           - |           - |               208 B |
|       ProtoSerialize |     1 |      1,015.7 ns |      18.792 ns |      17.578 ns |  2.27 |    0.05 |    3 |      53 B |      0.1736 |           - |           - |               736 B |
|        JsonSerialize |     1 |      1,693.3 ns |      33.862 ns |      31.675 ns |  3.79 |    0.08 |    4 |     117 B |      0.4025 |           - |           - |              1696 B |
|                      |       |                 |                |                |       |         |      |           |             |             |             |                     |
|    MspackDeserialize |     1 |        418.4 ns |       8.379 ns |       7.838 ns |  1.00 |    0.00 |    1 |         - |      0.0491 |           - |           - |               208 B |
| LZ4MspackDeserialize |     1 |        493.0 ns |       9.578 ns |       8.490 ns |  1.18 |    0.02 |    2 |         - |      0.0486 |           - |           - |               208 B |
|     ProtoDeserialize |     1 |      1,597.6 ns |      31.748 ns |      43.457 ns |  3.84 |    0.11 |    3 |         - |      0.1316 |           - |           - |               552 B |
|      JsonDeserialize |     1 |      2,936.3 ns |      58.197 ns |      75.673 ns |  7.03 |    0.19 |    4 |         - |      0.6866 |           - |           - |              2888 B |
|                      |       |                 |                |                |       |         |      |           |             |             |             |                     |
|      MspackSerialize |    10 |      4,294.9 ns |      84.352 ns |     133.791 ns |  1.00 |    0.00 |    1 |     743 B |      0.3204 |           - |           - |              1376 B |
|   LZ4MspackSerialize |    10 |      8,015.5 ns |     158.123 ns |     175.753 ns |  1.87 |    0.07 |    2 |     285 B |      0.3357 |           - |           - |              1464 B |
|       ProtoSerialize |    10 |     10,633.7 ns |     209.789 ns |     257.640 ns |  2.48 |    0.09 |    3 |     542 B |      1.6022 |           - |           - |              6736 B |
|        JsonSerialize |    10 |     16,666.2 ns |     344.352 ns |     515.410 ns |  3.88 |    0.16 |    4 |   1.13 KB |      3.8757 |           - |           - |             16344 B |
|                      |       |                 |                |                |       |         |      |           |             |             |             |                     |
|    MspackDeserialize |    10 |      4,175.7 ns |      82.900 ns |     118.893 ns |  1.00 |    0.00 |    1 |         - |      0.3433 |           - |           - |              1464 B |
| LZ4MspackDeserialize |    10 |      4,957.3 ns |      81.048 ns |      63.277 ns |  1.18 |    0.03 |    2 |         - |      0.3433 |           - |           - |              1464 B |
|     ProtoDeserialize |    10 |     16,179.1 ns |     254.393 ns |     212.430 ns |  3.84 |    0.08 |    3 |         - |      1.1597 |           - |           - |              4904 B |
|      JsonDeserialize |    10 |     32,302.5 ns |     633.020 ns |   1,004.037 ns |  7.74 |    0.32 |    4 |         - |      6.7139 |           - |           - |             28264 B |
|                      |       |                 |                |                |       |         |      |           |             |             |             |                     |
|      MspackSerialize |   100 |     41,860.1 ns |     816.421 ns |   1,271.069 ns |  1.00 |    0.00 |    1 |   7.41 KB |      2.9907 |           - |           - |             12600 B |
|   LZ4MspackSerialize |   100 |     79,169.2 ns |   1,915.639 ns |   1,791.890 ns |  1.88 |    0.08 |    2 |   2.03 KB |      3.2959 |           - |           - |             14128 B |
|       ProtoSerialize |   100 |    103,324.0 ns |   1,690.492 ns |   1,498.577 ns |  2.46 |    0.08 |    3 |   5.45 KB |     15.7471 |           - |           - |             66208 B |
|        JsonSerialize |   100 |    167,940.8 ns |   3,343.097 ns |   3,715.843 ns |  3.99 |    0.15 |    4 |  11.54 KB |     38.8184 |           - |           - |            162928 B |
|                      |       |                 |                |                |       |         |      |           |             |             |             |                     |
|    MspackDeserialize |   100 |     39,663.2 ns |   1,003.933 ns |     985.997 ns |  1.00 |    0.00 |    1 |         - |      3.3569 |           - |           - |             14128 B |
| LZ4MspackDeserialize |   100 |     49,517.0 ns |   1,064.402 ns |   1,456.965 ns |  1.25 |    0.05 |    2 |         - |      3.3569 |           - |           - |             14128 B |
|     ProtoDeserialize |   100 |    156,061.2 ns |   3,067.964 ns |   3,013.151 ns |  3.94 |    0.15 |    3 |         - |     11.4746 |           - |           - |             48528 B |
|      JsonDeserialize |   100 |    326,421.0 ns |   7,167.806 ns |   7,039.744 ns |  8.24 |    0.32 |    4 |         - |     66.8945 |           - |           - |            282128 B |
|                      |       |                 |                |                |       |         |      |           |             |             |             |                     |
|      MspackSerialize | 10000 |  7,112,536.8 ns | 138,252.302 ns | 135,782.246 ns |  1.00 |    0.00 |    1 | 798.25 KB |    226.5625 |    101.5625 |     31.2500 |           1374591 B |
|   LZ4MspackSerialize | 10000 | 11,102,937.8 ns | 205,341.040 ns | 182,029.483 ns |  1.56 |    0.03 |    2 | 205.82 KB |    234.3750 |    125.0000 |     31.2500 |           1462464 B |
|       ProtoSerialize | 10000 | 16,312,191.0 ns | 324,309.299 ns | 584,796.564 ns |  2.31 |    0.11 |    3 | 593.42 KB |   1218.7500 |    437.5000 |    156.2500 |           6738772 B |
|        JsonSerialize | 10000 | 30,613,934.2 ns | 592,885.227 ns | 554,585.209 ns |  4.30 |    0.12 |    4 |   1.18 MB |   3218.7500 |   1343.7500 |    625.0000 |          16453422 B |
|                      |       |                 |                |                |       |         |      |           |             |             |             |                     |
|    MspackDeserialize | 10000 |  7,478,228.8 ns | 120,002.323 ns | 112,250.247 ns |  1.00 |    0.00 |    1 |         - |    250.0000 |    109.3750 |     31.2500 |           1534413 B |
| LZ4MspackDeserialize | 10000 |  8,571,170.4 ns | 124,787.873 ns | 116,726.654 ns |  1.15 |    0.03 |    2 |         - |    250.0000 |    109.3750 |     31.2500 |           1534495 B |
|     ProtoDeserialize | 10000 | 23,118,418.1 ns | 457,564.133 ns | 610,834.975 ns |  3.10 |    0.11 |    3 |         - |    906.2500 |    343.7500 |    125.0000 |           4974724 B |
|      JsonDeserialize | 10000 | 42,854,476.9 ns | 565,990.210 ns | 472,627.460 ns |  5.74 |    0.12 |    4 |         - |   6153.8462 |   1846.1538 |    461.5385 |          28335056 B |
```
