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
| **MspackSerialize**              | **1**     |       **237.7 ns** |      **2.84 ns** |      **2.22 ns** | **1.00** | **0.00** |      **68 B** |   **0.0112** |        **-** |        **-** |     **96 B** |
| MspackContractlessSerialize      | 1         |           234.6 ns |          1.12 ns |          1.05 ns |     0.99 |     0.01 |          68 B |       0.0112 |            - |            - |         96 B |
| LZ4MspackSerialize               | 1         |           630.0 ns |          5.11 ns |          4.78 ns |     2.65 |     0.03 |          79 B |       0.0114 |            - |            - |        104 B |
| LZ4MspackContractlessSerialize   | 1         |           631.2 ns |          4.85 ns |          4.54 ns |     2.66 |     0.04 |          79 B |       0.0116 |            - |            - |        104 B |
| ProtoSerialize                   | 1         |           932.4 ns |          6.50 ns |          6.08 ns |     3.93 |     0.05 |          76 B |       0.0824 |            - |            - |        696 B |
| NewtonJsonSerialize              | 1         |         1,549.8 ns |         37.92 ns |         37.24 ns |     6.54 |     0.18 |         182 B |       0.2492 |            - |            - |       2096 B |
| SwifterJsonSerialize             | 1         |           384.1 ns |          6.49 ns |          6.07 ns |     1.62 |     0.03 |         177 B |       0.0715 |            - |            - |        600 B |
|                                  |           |                    |                  |                  |          |          |               |              |              |              |              |
| MspackDeserialize                | 1         |           265.9 ns |          1.20 ns |          1.00 ns |     1.00 |     0.00 |            1N |       0.0186 |            - |            - |        160 B |
| MspackContractlessDeserialize    | 1         |           270.2 ns |          7.41 ns |          7.28 ns |     1.02 |     0.03 |            1N |       0.0191 |            - |            - |        160 B |
| LZ4MspackDeserialize             | 1         |           697.8 ns |         13.11 ns |         11.62 ns |     2.63 |     0.05 |            1N |       0.0178 |            - |            - |        160 B |
| LZ4MspackContractlessDeserialize | 1         |           676.1 ns |          5.46 ns |          5.11 ns |     2.54 |     0.02 |            1N |       0.0189 |            - |            - |        160 B |
| ProtoDeserialize                 | 1         |         3,738.2 ns |         31.68 ns |         29.63 ns |    14.07 |     0.13 |            1N |       0.2871 |            - |            - |       2465 B |
| NewtonJsonDeserialize            | 1         |         3,770.7 ns |         81.92 ns |         76.63 ns |    14.20 |     0.33 |            1N |       0.3878 |            - |            - |       3264 B |
| SwifterJsonDeserialize           | 1         |         1,292.8 ns |          8.40 ns |          7.86 ns |     4.87 |     0.03 |            1N |       0.0512 |            - |            - |        432 B |
|                                  |           |                    |                  |                  |          |          |               |              |              |              |              |
| **MspackSerialize**              | **100**   |    **15,379.3 ns** |    **224.24 ns** |    **209.75 ns** | **1.00** | **0.00** |   **6.73 KB** |   **0.8190** |        **-** |        **-** |   **6912 B** |
| MspackContractlessSerialize      | 100       |        15,324.8 ns |        146.08 ns |        136.64 ns |     1.00 |     0.02 |       6.73 KB |       0.8136 |            - |            - |       6912 B |
| LZ4MspackSerialize               | 100       |        21,687.2 ns |        193.15 ns |        180.67 ns |     1.41 |     0.02 |       3.08 KB |       0.3433 |            - |            - |       3168 B |
| LZ4MspackContractlessSerialize   | 100       |        21,569.5 ns |        195.88 ns |        173.64 ns |     1.40 |     0.03 |       3.09 KB |       0.3442 |            - |            - |       3176 B |
| ProtoSerialize                   | 100       |        40,926.8 ns |        339.09 ns |        300.59 ns |     2.66 |     0.04 |        7.7 KB |       2.8866 |       0.1649 |            - |      24641 B |
| NewtonJsonSerialize              | 100       |       120,824.6 ns |        693.40 ns |        648.61 ns |     7.86 |     0.13 |      17.92 KB |      12.8954 |       2.4331 |            - |     109666 B |
| SwifterJsonSerialize             | 100       |        30,970.2 ns |        174.19 ns |        162.93 ns |     2.01 |     0.03 |       17.4 KB |       5.3459 |            - |            - |      45384 B |
|                                  |           |                    |                  |                  |          |          |               |              |              |              |              |
| MspackDeserialize                | 100       |        18,897.3 ns |        350.19 ns |        292.42 ns |     1.00 |     0.00 |          100N |       1.6196 |       0.1104 |            - |      13632 B |
| MspackContractlessDeserialize    | 100       |        18,453.6 ns |        120.01 ns |        112.26 ns |     0.98 |     0.02 |          100N |       1.6138 |       0.1100 |            - |      13632 B |
| LZ4MspackDeserialize             | 100       |        20,771.6 ns |        200.76 ns |        177.97 ns |     1.10 |     0.02 |          100N |       1.6110 |       0.1239 |            - |      13632 B |
| LZ4MspackContractlessDeserialize | 100       |        20,746.0 ns |        130.13 ns |        121.73 ns |     1.10 |     0.02 |          100N |       1.6110 |       0.1239 |            - |      13632 B |
| ProtoDeserialize                 | 100       |        55,479.4 ns |        356.69 ns |        333.64 ns |     2.94 |     0.05 |          100N |       3.7811 |       0.3336 |            - |      31663 B |
| NewtonJsonDeserialize            | 100       |       269,043.9 ns |      1,091.18 ns |        851.92 ns |    14.23 |     0.23 |          100N |       5.4348 |       0.5435 |            - |      46488 B |
| SwifterJsonDeserialize           | 100       |       122,656.9 ns |        590.58 ns |        552.43 ns |     6.49 |     0.10 |          100N |       3.6675 |       0.2445 |            - |      32032 B |
|                                  |           |                    |                  |                  |          |          |               |              |              |              |              |
| **MspackSerialize**              | **10000** | **1,593,960.0 ns** | **12,975.76 ns** | **12,137.53 ns** | **1.00** | **0.00** | **729.89 KB** | **163.9871** | **163.9871** | **163.9871** | **747440 B** |
| MspackContractlessSerialize      | 10000     |     1,627,699.8 ns |      6,636.49 ns |      6,207.78 ns |     1.02 |     0.01 |     729.89 KB |     164.4737 |     164.4737 |     164.4737 |     747440 B |
| LZ4MspackSerialize               | 10000     |     2,313,814.1 ns |     22,580.97 ns |     18,856.13 ns |     1.45 |     0.02 |     314.71 KB |      95.8904 |      95.8904 |      95.8904 |     324030 B |
| LZ4MspackContractlessSerialize   | 10000     |     2,294,907.2 ns |      9,120.70 ns |      8,531.50 ns |     1.44 |     0.01 |     313.98 KB |      95.8904 |      95.8904 |      95.8904 |     324088 B |
| ProtoSerialize                   | 10000     |     4,210,625.4 ns |     20,793.77 ns |     18,433.14 ns |     2.64 |     0.02 |     818.03 KB |     675.0000 |     675.0000 |     666.6667 |    3064273 B |
| NewtonJsonSerialize              | 10000     |    12,826,228.0 ns |     63,730.76 ns |     56,495.66 ns |     8.04 |     0.07 |       1.81 MB |    1179.4872 |     846.1538 |     461.5385 |    9671328 B |
| SwifterJsonSerialize             | 10000     |     3,961,717.2 ns |     36,591.35 ns |     34,227.57 ns |     2.49 |     0.03 |       1.76 MB |     355.0725 |     217.3913 |     217.3913 |    4641439 B |
|                                  |           |                    |                  |                  |          |          |               |              |              |              |              |
| MspackDeserialize                | 10000     |     2,217,928.4 ns |     33,862.37 ns |     30,018.11 ns |     1.00 |     0.00 |        10000N |     170.8333 |      83.3333 |            - |    1439245 B |
| MspackContractlessDeserialize    | 10000     |     2,181,534.6 ns |      9,021.77 ns |      8,438.97 ns |     0.98 |     0.01 |        10000N |     170.8333 |      83.3333 |            - |    1439245 B |
| LZ4MspackDeserialize             | 10000     |     2,383,529.7 ns |     13,413.13 ns |     12,546.65 ns |     1.08 |     0.01 |        10000N |     169.6429 |      84.8214 |            - |    1439246 B |
| LZ4MspackContractlessDeserialize | 10000     |     2,385,981.8 ns |     18,738.79 ns |     16,611.45 ns |     1.08 |     0.01 |        10000N |     169.6429 |      84.8214 |            - |    1439240 B |
| ProtoDeserialize                 | 10000     |     6,520,227.1 ns |     87,350.21 ns |     81,707.45 ns |     2.94 |     0.06 |        10000N |     450.0000 |     362.5000 |     200.0000 |    2877047 B |
| NewtonJsonDeserialize            | 10000     |    30,769,317.5 ns |    241,037.79 ns |    225,466.90 ns |    13.88 |     0.19 |        10000N |     562.5000 |     187.5000 |      62.5000 |    4498608 B |
| SwifterJsonDeserialize           | 10000     |    12,304,225.5 ns |     83,034.25 ns |     77,670.29 ns |     5.55 |     0.08 |        10000N |     341.4634 |     170.7317 |            - |    3023600 B |
