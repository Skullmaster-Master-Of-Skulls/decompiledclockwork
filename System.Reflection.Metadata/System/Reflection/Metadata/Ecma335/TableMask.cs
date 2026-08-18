using System;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x020000D4 RID: 212
	[Flags]
	internal enum TableMask : ulong
	{
		// Token: 0x040005D3 RID: 1491
		Module = 1UL,
		// Token: 0x040005D4 RID: 1492
		TypeRef = 2UL,
		// Token: 0x040005D5 RID: 1493
		TypeDef = 4UL,
		// Token: 0x040005D6 RID: 1494
		FieldPtr = 8UL,
		// Token: 0x040005D7 RID: 1495
		Field = 16UL,
		// Token: 0x040005D8 RID: 1496
		MethodPtr = 32UL,
		// Token: 0x040005D9 RID: 1497
		MethodDef = 64UL,
		// Token: 0x040005DA RID: 1498
		ParamPtr = 128UL,
		// Token: 0x040005DB RID: 1499
		Param = 256UL,
		// Token: 0x040005DC RID: 1500
		InterfaceImpl = 512UL,
		// Token: 0x040005DD RID: 1501
		MemberRef = 1024UL,
		// Token: 0x040005DE RID: 1502
		Constant = 2048UL,
		// Token: 0x040005DF RID: 1503
		CustomAttribute = 4096UL,
		// Token: 0x040005E0 RID: 1504
		FieldMarshal = 8192UL,
		// Token: 0x040005E1 RID: 1505
		DeclSecurity = 16384UL,
		// Token: 0x040005E2 RID: 1506
		ClassLayout = 32768UL,
		// Token: 0x040005E3 RID: 1507
		FieldLayout = 65536UL,
		// Token: 0x040005E4 RID: 1508
		StandAloneSig = 131072UL,
		// Token: 0x040005E5 RID: 1509
		EventMap = 262144UL,
		// Token: 0x040005E6 RID: 1510
		EventPtr = 524288UL,
		// Token: 0x040005E7 RID: 1511
		Event = 1048576UL,
		// Token: 0x040005E8 RID: 1512
		PropertyMap = 2097152UL,
		// Token: 0x040005E9 RID: 1513
		PropertyPtr = 4194304UL,
		// Token: 0x040005EA RID: 1514
		Property = 8388608UL,
		// Token: 0x040005EB RID: 1515
		MethodSemantics = 16777216UL,
		// Token: 0x040005EC RID: 1516
		MethodImpl = 33554432UL,
		// Token: 0x040005ED RID: 1517
		ModuleRef = 67108864UL,
		// Token: 0x040005EE RID: 1518
		TypeSpec = 134217728UL,
		// Token: 0x040005EF RID: 1519
		ImplMap = 268435456UL,
		// Token: 0x040005F0 RID: 1520
		FieldRva = 536870912UL,
		// Token: 0x040005F1 RID: 1521
		EnCLog = 1073741824UL,
		// Token: 0x040005F2 RID: 1522
		EnCMap = 2147483648UL,
		// Token: 0x040005F3 RID: 1523
		Assembly = 4294967296UL,
		// Token: 0x040005F4 RID: 1524
		AssemblyRef = 34359738368UL,
		// Token: 0x040005F5 RID: 1525
		File = 274877906944UL,
		// Token: 0x040005F6 RID: 1526
		ExportedType = 549755813888UL,
		// Token: 0x040005F7 RID: 1527
		ManifestResource = 1099511627776UL,
		// Token: 0x040005F8 RID: 1528
		NestedClass = 2199023255552UL,
		// Token: 0x040005F9 RID: 1529
		GenericParam = 4398046511104UL,
		// Token: 0x040005FA RID: 1530
		MethodSpec = 8796093022208UL,
		// Token: 0x040005FB RID: 1531
		GenericParamConstraint = 17592186044416UL,
		// Token: 0x040005FC RID: 1532
		Document = 281474976710656UL,
		// Token: 0x040005FD RID: 1533
		MethodDebugInformation = 562949953421312UL,
		// Token: 0x040005FE RID: 1534
		LocalScope = 1125899906842624UL,
		// Token: 0x040005FF RID: 1535
		LocalVariable = 2251799813685248UL,
		// Token: 0x04000600 RID: 1536
		LocalConstant = 4503599627370496UL,
		// Token: 0x04000601 RID: 1537
		ImportScope = 9007199254740992UL,
		// Token: 0x04000602 RID: 1538
		StateMachineMethod = 18014398509481984UL,
		// Token: 0x04000603 RID: 1539
		CustomDebugInformation = 36028797018963968UL,
		// Token: 0x04000604 RID: 1540
		PtrTables = 4718760UL,
		// Token: 0x04000605 RID: 1541
		V2_0_TablesMask = 34952443854847UL,
		// Token: 0x04000606 RID: 1542
		PortablePdb_TablesMask = 71776119061217280UL,
		// Token: 0x04000607 RID: 1543
		V3_0_TablesMask = 71811071505072127UL
	}
}
