# Benchmarks

```ini

BenchmarkDotNet=v0.11.4, OS=Windows 10.0.17763.379 (1809/October2018Update/Redstone5)
Intel Xeon CPU E3-1230 v3 3.30GHz, 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview3-010431
  [Host]     : .NET Core 2.2.3 (CoreCLR 4.6.27414.05, CoreFX 4.6.27414.05), 64bit RyuJIT
  Job-ONNKIC : .NET Core 2.2.3 (CoreCLR 4.6.27414.05, CoreFX 4.6.27414.05), 64bit RyuJIT

Runtime=Core  IterationTime=500.0000 ms  MaxIterationCount=16
MaxWarmupIterationCount=7

```

| Method                           | N         |               Mean |             Error |            StdDev |    Ratio |  RatioSD |      ByteSize |  Gen 0/1k Op |  Gen 1/1k Op |  Gen 2/1k Op | Allocated Memory/Op |
| -------------------------------- | --------- | -----------------: | ----------------: | ----------------: | -------: | -------: | ------------: | -----------: | -----------: | -----------: | ------------------: |
| **MspackSerialize**              | **1**     |       **365.4 ns** |     **14.754 ns** |     **13.079 ns** | **1.00** | **0.00** |      **68 B** |   **0.0227** |        **-** |        **-** |            **96 B** |
| MspackContractlessSerialize      | 1         |           356.2 ns |          3.659 ns |          3.244 ns |     0.98 |     0.04 |          68 B |       0.0226 |            - |            - |                96 B |
| LZ4MspackSerialize               | 1         |           664.8 ns |         32.284 ns |         31.708 ns |     1.83 |     0.11 |          81 B |       0.0255 |            - |            - |               112 B |
| LZ4MspackContractlessSerialize   | 1         |           732.6 ns |         43.414 ns |         42.638 ns |     1.99 |     0.15 |          81 B |       0.0259 |            - |            - |               112 B |
| ProtoSerialize                   | 1         |         1,683.3 ns |         45.415 ns |         42.481 ns |     4.61 |     0.24 |          76 B |       0.1634 |            - |            - |               696 B |
| NewtonJsonSerialize              | 1         |         2,974.2 ns |        132.668 ns |        130.298 ns |     8.12 |     0.35 |         181 B |       0.5031 |            - |            - |              2112 B |
| SwifterJsonSerialize             | 1         |           906.9 ns |         27.063 ns |         26.579 ns |     2.49 |     0.08 |         177 B |       0.1352 |            - |            - |               568 B |
|                                  |           |                    |                   |                   |          |          |               |              |              |              |                     |
| MspackDeserialize                | 1         |           363.5 ns |         20.776 ns |         18.418 ns |     1.00 |     0.00 |            1N |       0.0377 |            - |            - |               160 B |
| MspackContractlessDeserialize    | 1         |           378.7 ns |         23.897 ns |         22.354 ns |     1.05 |     0.09 |            1N |       0.0377 |            - |            - |               160 B |
| LZ4MspackDeserialize             | 1         |           469.6 ns |         32.441 ns |         31.862 ns |     1.29 |     0.08 |            1N |       0.0378 |            - |            - |               160 B |
| LZ4MspackContractlessDeserialize | 1         |           453.2 ns |         28.910 ns |         27.043 ns |     1.25 |     0.11 |            1N |       0.0377 |            - |            - |               160 B |
| ProtoDeserialize                 | 1         |         6,411.1 ns |        117.844 ns |         98.405 ns |    17.68 |     0.93 |            1N |       0.5850 |            - |            - |              2485 B |
| NewtonJsonDeserialize            | 1         |         6,258.5 ns |        485.598 ns |        454.229 ns |    17.32 |     1.66 |            1N |       0.7705 |            - |            - |              3280 B |
| SwifterJsonDeserialize           | 1         |         2,424.8 ns |         93.531 ns |         91.860 ns |     6.67 |     0.36 |            1N |       0.1686 |            - |            - |               720 B |
|                                  |           |                    |                   |                   |          |          |               |              |              |              |                     |
| **MspackSerialize**              | **100**   |    **29,356.8 ns** |    **560.826 ns** |    **524.597 ns** | **1.00** | **0.00** |   **6.73 KB** |   **1.6389** |        **-** |        **-** |          **6912 B** |
| MspackContractlessSerialize      | 100       |        30,178.1 ns |      1,465.006 ns |      1,370.367 ns |     1.03 |     0.04 |       6.73 KB |       1.6220 |            - |            - |              6912 B |
| LZ4MspackSerialize               | 100       |        39,322.8 ns |      1,018.819 ns |      1,000.617 ns |     1.34 |     0.04 |       2.94 KB |       0.6903 |            - |            - |              3056 B |
| LZ4MspackContractlessSerialize   | 100       |        39,030.9 ns |      1,271.162 ns |      1,248.451 ns |     1.33 |     0.05 |       2.97 KB |       0.6990 |            - |            - |              3056 B |
| ProtoSerialize                   | 100       |        78,480.3 ns |      2,744.051 ns |      2,566.787 ns |     2.67 |     0.10 |        7.7 KB |       5.8570 |            - |            - |             24640 B |
| NewtonJsonSerialize              | 100       |       239,675.0 ns |      9,174.431 ns |      9,010.518 ns |     8.21 |     0.30 |      17.96 KB |      26.1072 |            - |            - |            109968 B |
| SwifterJsonSerialize             | 100       |        85,090.8 ns |      2,967.431 ns |      2,914.414 ns |     2.90 |     0.11 |       17.4 KB |      10.6645 |            - |            - |             45352 B |
|                                  |           |                    |                   |                   |          |          |               |              |              |              |                     |
| MspackDeserialize                | 100       |        32,596.7 ns |      1,391.476 ns |      1,366.615 ns |     1.00 |     0.00 |          100N |       3.3566 |            - |            - |             14352 B |
| MspackContractlessDeserialize    | 100       |        32,612.1 ns |      1,590.382 ns |      1,561.968 ns |     1.00 |     0.08 |          100N |       3.3960 |            - |            - |             14352 B |
| LZ4MspackDeserialize             | 100       |        35,388.2 ns |      1,657.545 ns |      1,627.930 ns |     1.09 |     0.06 |          100N |       3.3766 |            - |            - |             14352 B |
| LZ4MspackContractlessDeserialize | 100       |        34,951.7 ns |      1,940.666 ns |      1,905.993 ns |     1.07 |     0.07 |          100N |       3.3840 |            - |            - |             14352 B |
| ProtoDeserialize                 | 100       |       116,990.1 ns |      5,519.874 ns |      5,163.294 ns |     3.60 |     0.17 |          100N |       7.6673 |            - |            - |             32336 B |
| NewtonJsonDeserialize            | 100       |       433,308.3 ns |     26,693.220 ns |     26,216.311 ns |    13.32 |     1.03 |          100N |      10.8611 |            - |            - |             47368 B |
| SwifterJsonDeserialize           | 100       |       233,205.6 ns |      6,937.377 ns |      6,813.432 ns |     7.17 |     0.46 |          100N |      14.4864 |            - |            - |             61912 B |
|                                  |           |                    |                   |                   |          |          |               |              |              |              |                     |
| **MspackSerialize**              | **10000** | **3,502,794.0 ns** | **69,485.241 ns** | **68,243.798 ns** | **1.00** | **0.00** | **729.89 KB** | **659.5745** | **659.5745** | **659.5745** |       **2713616 B** |
| MspackContractlessSerialize      | 10000     |     3,491,478.4 ns |     73,620.953 ns |     72,305.620 ns |     1.00 |     0.02 |     729.89 KB |     662.0690 |     662.0690 |     662.0690 |           2713616 B |
| LZ4MspackSerialize               | 10000     |     4,949,137.1 ns |    137,756.378 ns |    135,295.183 ns |     1.41 |     0.05 |     313.92 KB |     727.2727 |     727.2727 |     727.2727 |           3038560 B |
| LZ4MspackContractlessSerialize   | 10000     |     5,237,152.1 ns |    334,962.007 ns |    328,977.479 ns |     1.50 |     0.10 |     314.86 KB |     725.4902 |     725.4902 |     725.4902 |           3037976 B |
| ProtoSerialize                   | 10000     |     8,499,476.0 ns |    463,102.388 ns |    410,528.205 ns |     2.43 |     0.14 |     818.02 KB |     703.1250 |     687.5000 |     671.8750 |           3050244 B |
| NewtonJsonSerialize              | 10000     |    25,553,082.7 ns |    395,682.898 ns |    350,762.583 ns |     7.30 |     0.13 |       1.81 MB |    1578.9474 |    1052.6316 |     578.9474 |           9711328 B |
| SwifterJsonSerialize             | 10000     |     9,273,767.7 ns |    115,522.107 ns |    108,059.451 ns |     2.65 |     0.06 |       1.76 MB |     629.6296 |     351.8519 |     351.8519 |           4641735 B |
|                                  |           |                    |                   |                   |          |          |               |              |              |              |                     |
| MspackDeserialize                | 10000     |     3,941,129.4 ns |    157,709.900 ns |    154,892.210 ns |     1.00 |     0.00 |        10000N |     242.1875 |     117.1875 |            - |           1511960 B |
| MspackContractlessDeserialize    | 10000     |     3,800,217.9 ns |    163,027.964 ns |    160,115.259 ns |     0.96 |     0.04 |        10000N |     236.1111 |     118.0556 |            - |           1511960 B |
| LZ4MspackDeserialize             | 10000     |     4,106,615.7 ns |     37,714.159 ns |     35,277.848 ns |     1.05 |     0.04 |        10000N |     398.4375 |     328.1250 |     171.8750 |           2259545 B |
| LZ4MspackContractlessDeserialize | 10000     |     4,988,178.7 ns |     81,402.733 ns |     76,144.167 ns |     1.27 |     0.04 |        10000N |     392.8571 |     321.4286 |     169.6429 |           2259605 B |
| ProtoDeserialize                 | 10000     |    12,706,491.1 ns |    275,106.682 ns |    243,874.908 ns |     3.22 |     0.12 |        10000N |     520.8333 |     354.1667 |     187.5000 |           2951364 B |
| NewtonJsonDeserialize            | 10000     |    44,124,208.6 ns |    630,051.173 ns |    558,523.954 ns |    11.19 |     0.53 |        10000N |     700.0000 |     200.0000 |            - |           4584232 B |
| SwifterJsonDeserialize           | 10000     |    27,533,485.6 ns |    414,125.301 ns |    387,373.063 ns |     7.01 |     0.26 |        10000N |    1055.5556 |     388.8889 |     111.1111 |           6186860 B |
