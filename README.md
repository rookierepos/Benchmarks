# Benchmarks

```ini

BenchmarkDotNet=v0.12.0, OS=Windows 10.0.18363
Intel Core i9-9900KS CPU 4.00GHz, 1 CPU, 16 logical and 8 physical cores
.NET Core SDK=3.1.101
  [Host]     : .NET Core 3.1.1 (CoreCLR 4.700.19.60701, CoreFX 4.700.19.60801), X64 RyuJIT
  Job-SLQJKT : .NET Core 3.1.1 (CoreCLR 4.700.19.60701, CoreFX 4.700.19.60801), X64 RyuJIT

Runtime=.NET Core 3.1  IterationTime=500.0000 ms  MaxIterationCount=16
MaxWarmupIterationCount=7

```

| Method                           | N         |               Mean |            Error |           StdDev |    Ratio |  RatioSD |      ByteSize |        Gen 0 |        Gen 1 |        Gen 2 |    Allocated |
| -------------------------------- | --------- | -----------------: | ---------------: | ---------------: | -------: | -------: | ------------: | -----------: | -----------: | -----------: | -----------: |
| **MspackSerialize**              | **1**     |       **239.6 ns** |      **2.85 ns** |      **2.66 ns** | **1.00** | **0.00** |      **68 B** |   **0.0114** |        **-** |        **-** |     **96 B** |
| MspackContractlessSerialize      | 1         |           235.7 ns |          2.30 ns |          2.15 ns |     0.98 |     0.02 |          68 B |       0.0112 |            - |            - |         96 B |
| LZ4MspackSerialize               | 1         |           643.0 ns |          8.23 ns |          7.70 ns |     2.68 |     0.04 |          79 B |       0.0113 |            - |            - |        104 B |
| LZ4MspackContractlessSerialize   | 1         |           635.5 ns |          6.22 ns |          5.82 ns |     2.65 |     0.04 |          79 B |       0.0114 |            - |            - |        104 B |
| ProtoSerialize                   | 1         |           912.3 ns |         12.12 ns |         11.34 ns |     3.81 |     0.06 |          76 B |       0.0825 |            - |            - |        696 B |
| NewtonJsonSerialize              | 1         |         1,547.7 ns |         16.93 ns |         15.83 ns |     6.46 |     0.08 |         181 B |       0.2480 |            - |            - |       2096 B |
| SwifterJsonSerialize             | 1         |           368.9 ns |          4.54 ns |          4.03 ns |     1.54 |     0.02 |         176 B |       0.0715 |            - |            - |        600 B |
|                                  |           |                    |                  |                  |          |          |               |              |              |              |              |
| MspackDeserialize                | 1         |           257.7 ns |          2.50 ns |          2.34 ns |     1.00 |     0.00 |            1N |       0.0188 |            - |            - |        160 B |
| MspackContractlessDeserialize    | 1         |           255.3 ns |          2.47 ns |          2.31 ns |     0.99 |     0.01 |            1N |       0.0187 |            - |            - |        160 B |
| LZ4MspackDeserialize             | 1         |           689.7 ns |          5.58 ns |          5.22 ns |     2.68 |     0.03 |            1N |       0.0189 |            - |            - |        160 B |
| LZ4MspackContractlessDeserialize | 1         |           685.0 ns |          6.69 ns |          6.25 ns |     2.66 |     0.04 |            1N |       0.0178 |            - |            - |        160 B |
| ProtoDeserialize                 | 1         |         3,630.1 ns |         59.78 ns |         55.92 ns |    14.09 |     0.23 |            1N |       0.2923 |            - |            - |       2465 B |
| NewtonJsonDeserialize            | 1         |         3,843.6 ns |         64.08 ns |         56.80 ns |    14.90 |     0.28 |            1N |       0.3848 |            - |            - |       3264 B |
| SwifterJsonDeserialize           | 1         |         1,304.1 ns |         11.06 ns |          9.80 ns |     5.06 |     0.07 |            1N |       0.0496 |            - |            - |        432 B |
|                                  |           |                    |                  |                  |          |          |               |              |              |              |              |
| **MspackSerialize**              | **100**   |    **15,054.8 ns** |    **141.59 ns** |    **132.44 ns** | **1.00** | **0.00** |   **6.73 KB** |   **0.8036** |        **-** |        **-** |   **6912 B** |
| MspackContractlessSerialize      | 100       |        15,062.8 ns |        165.61 ns |        154.92 ns |     1.00 |     0.01 |       6.73 KB |       0.8208 |            - |            - |       6912 B |
| LZ4MspackSerialize               | 100       |        21,735.6 ns |        245.47 ns |        229.61 ns |     1.44 |     0.02 |       3.09 KB |       0.3405 |            - |            - |       3168 B |
| LZ4MspackContractlessSerialize   | 100       |        21,586.0 ns |        252.10 ns |        235.82 ns |     1.43 |     0.02 |       3.07 KB |       0.3395 |            - |            - |       3168 B |
| ProtoSerialize                   | 100       |        40,404.1 ns |        361.77 ns |        338.40 ns |     2.68 |     0.04 |        7.7 KB |       2.8752 |       0.1597 |            - |      24641 B |
| NewtonJsonSerialize              | 100       |       119,809.2 ns |      1,725.47 ns |      1,614.00 ns |     7.96 |     0.11 |      17.91 KB |      12.9564 |       2.5913 |            - |     109640 B |
| SwifterJsonSerialize             | 100       |        31,204.9 ns |        442.28 ns |        413.71 ns |     2.07 |     0.04 |       17.4 KB |       5.3994 |       0.5522 |            - |      45384 B |
|                                  |           |                    |                  |                  |          |          |               |              |              |              |              |
| MspackDeserialize                | 100       |        18,515.8 ns |        207.82 ns |        194.40 ns |     1.00 |     0.00 |          100N |       1.6129 |       0.1466 |            - |      13632 B |
| MspackContractlessDeserialize    | 100       |        18,374.0 ns |        187.25 ns |        175.15 ns |     0.99 |     0.01 |          100N |       1.5951 |       0.1088 |            - |      13632 B |
| LZ4MspackDeserialize             | 100       |        20,974.5 ns |        211.40 ns |        197.74 ns |     1.13 |     0.02 |          100N |       1.6026 |       0.1233 |            - |      13632 B |
| LZ4MspackContractlessDeserialize | 100       |        21,129.8 ns |        346.30 ns |        323.93 ns |     1.14 |     0.03 |          100N |       1.5908 |       0.1256 |            - |      13632 B |
| ProtoDeserialize                 | 100       |        55,073.9 ns |        875.05 ns |        818.52 ns |     2.97 |     0.05 |          100N |       3.7781 |       0.3238 |            - |      31653 B |
| NewtonJsonDeserialize            | 100       |       270,198.8 ns |      3,845.22 ns |      3,596.83 ns |    14.59 |     0.25 |          100N |       5.3879 |       0.5388 |            - |      46497 B |
| SwifterJsonDeserialize           | 100       |       124,687.4 ns |      1,229.75 ns |      1,150.31 ns |     6.73 |     0.09 |          100N |       3.7000 |       0.2467 |            - |      32120 B |
|                                  |           |                    |                  |                  |          |          |               |              |              |              |              |
| **MspackSerialize**              | **10000** | **1,625,092.4 ns** | **11,296.58 ns** | **10,014.12 ns** | **1.00** | **0.00** | **729.89 KB** | **165.5844** | **165.5844** | **165.5844** | **747465 B** |
| MspackContractlessSerialize      | 10000     |     1,633,326.3 ns |      5,259.76 ns |      4,919.98 ns |     1.01 |     0.01 |     729.89 KB |     165.5844 |     165.5844 |     165.5844 |     747439 B |
| LZ4MspackSerialize               | 10000     |     2,332,847.0 ns |     22,281.63 ns |     20,842.25 ns |     1.43 |     0.02 |     316.43 KB |      97.6744 |      97.6744 |      97.6744 |     324016 B |
| LZ4MspackContractlessSerialize   | 10000     |     2,336,700.5 ns |     17,538.54 ns |     16,405.56 ns |     1.44 |     0.01 |     315.51 KB |      96.3303 |      96.3303 |      96.3303 |     323584 B |
| ProtoSerialize                   | 10000     |     4,189,450.7 ns |     10,573.95 ns |      9,373.53 ns |     2.58 |     0.02 |     818.03 KB |     677.9661 |     677.9661 |     669.4915 |    3064386 B |
| NewtonJsonSerialize              | 10000     |    13,312,181.0 ns |     50,327.49 ns |     44,614.01 ns |     8.19 |     0.05 |       1.81 MB |    1184.2105 |     842.1053 |     473.6842 |    9671830 B |
| SwifterJsonSerialize             | 10000     |     3,819,571.5 ns |     25,986.71 ns |     21,700.08 ns |     2.35 |     0.01 |       1.76 MB |     363.6364 |     223.7762 |     223.7762 |    4641639 B |
|                                  |           |                    |                  |                  |          |          |               |              |              |              |              |
| MspackDeserialize                | 10000     |     2,206,979.6 ns |     19,862.68 ns |     18,579.57 ns |     1.00 |     0.00 |        10000N |     170.8333 |      83.3333 |            - |    1439245 B |
| MspackContractlessDeserialize    | 10000     |     2,177,121.7 ns |     25,202.24 ns |     23,574.20 ns |     0.99 |     0.01 |        10000N |     170.8333 |      83.3333 |            - |    1439245 B |
| LZ4MspackDeserialize             | 10000     |     2,345,971.8 ns |     29,229.16 ns |     27,340.97 ns |     1.06 |     0.01 |        10000N |     169.6429 |      84.8214 |            - |    1439242 B |
| LZ4MspackContractlessDeserialize | 10000     |     2,366,440.4 ns |     15,942.16 ns |     14,912.31 ns |     1.07 |     0.01 |        10000N |     169.6429 |      84.8214 |            - |    1439246 B |
| ProtoDeserialize                 | 10000     |     6,329,052.6 ns |     42,389.60 ns |     39,651.26 ns |     2.87 |     0.03 |        10000N |     450.0000 |     362.5000 |     200.0000 |    2876985 B |
| NewtonJsonDeserialize            | 10000     |    31,428,004.9 ns |    215,212.69 ns |    190,780.45 ns |    14.25 |     0.19 |        10000N |     562.5000 |     187.5000 |      62.5000 |    4499366 B |
| SwifterJsonDeserialize           | 10000     |    12,168,666.0 ns |    122,145.60 ns |    114,255.07 ns |     5.51 |     0.05 |        10000N |     365.8537 |     170.7317 |            - |    3065511 B |
