# XUnitComHost

- Build TestProject1
- Build TestComHostNet5
- Register TestComHostNet5 for COM Interop (regsvr32 BogusComHost.comhost.dll)
- Try DirectClientNet5. This works (1 test)
- Try TestComClientNet5. This does not work (0 tests)
