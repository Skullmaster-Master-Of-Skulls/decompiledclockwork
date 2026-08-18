using System;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x0200006E RID: 110
	[Flags]
	internal enum TableMask : ulong
	{
		// Token: 0x040003B9 RID: 953
		Module = 1UL,
		// Token: 0x040003BA RID: 954
		TypeRef = 2UL,
		// Token: 0x040003BB RID: 955
		TypeDef = 4UL,
		// Token: 0x040003BC RID: 956
		FieldPtr = 8UL,
		// Token: 0x040003BD RID: 957
		Field = 16UL,
		// Token: 0x040003BE RID: 958
		MethodPtr = 32UL,
		// Token: 0x040003BF RID: 959
		MethodDef = 64UL,
		// Token: 0x040003C0 RID: 960
		ParamPtr = 128UL,
		// Token: 0x040003C1 RID: 961
		Param = 256UL,
		// Token: 0x040003C2 RID: 962
		InterfaceImpl = 512UL,
		// Token: 0x040003C3 RID: 963
		MemberRef = 1024UL,
		// Token: 0x040003C4 RID: 964
		Constant = 2048UL,
		// Token: 0x040003C5 RID: 965
		CustomAttribute = 4096UL,
		// Token: 0x040003C6 RID: 966
		FieldMarshal = 8192UL,
		// Token: 0x040003C7 RID: 967
		DeclSecurity = 16384UL,
		// Token: 0x040003C8 RID: 968
		ClassLayout = 32768UL,
		// Token: 0x040003C9 RID: 969
		FieldLayout = 65536UL,
		// Token: 0x040003CA RID: 970
		StandAloneSig = 131072UL,
		// Token: 0x040003CB RID: 971
		EventMap = 262144UL,
		// Token: 0x040003CC RID: 972
		EventPtr = 524288UL,
		// Token: 0x040003CD RID: 973
		Event = 1048576UL,
		// Token: 0x040003CE RID: 974
		PropertyMap = 2097152UL,
		// Token: 0x040003CF RID: 975
		PropertyPtr = 4194304UL,
		// Token: 0x040003D0 RID: 976
		Property = 8388608UL,
		// Token: 0x040003D1 RID: 977
		MethodSemantics = 16777216UL,
		// Token: 0x040003D2 RID: 978
		MethodImpl = 33554432UL,
		// Token: 0x040003D3 RID: 979
		ModuleRef = 67108864UL,
		// Token: 0x040003D4 RID: 980
		TypeSpec = 134217728UL,
		// Token: 0x040003D5 RID: 981
		ImplMap = 268435456UL,
		// Token: 0x040003D6 RID: 982
		FieldRva = 536870912UL,
		// Token: 0x040003D7 RID: 983
		EnCLog = 1073741824UL,
		// Token: 0x040003D8 RID: 984
		EnCMap = 2147483648UL,
		// Token: 0x040003D9 RID: 985
		Assembly = 4294967296UL,
		// Token: 0x040003DA RID: 986
		AssemblyRef = 34359738368UL,
		// Token: 0x040003DB RID: 987
		File = 274877906944UL,
		// Token: 0x040003DC RID: 988
		ExportedType = 549755813888UL,
		// Token: 0x040003DD RID: 989
		ManifestResource = 1099511627776UL,
		// Token: 0x040003DE RID: 990
		NestedClass = 2199023255552UL,
		// Token: 0x040003DF RID: 991
		GenericParam = 4398046511104UL,
		// Token: 0x040003E0 RID: 992
		MethodSpec = 8796093022208UL,
		// Token: 0x040003E1 RID: 993
		GenericParamConstraint = 17592186044416UL,
		// Token: 0x040003E2 RID: 994
		Document = 281474976710656UL,
		// Token: 0x040003E3 RID: 995
		MethodDebugInformation = 562949953421312UL,
		// Token: 0x040003E4 RID: 996
		LocalScope = 1125899906842624UL,
		// Token: 0x040003E5 RID: 997
		LocalVariable = 2251799813685248UL,
		// Token: 0x040003E6 RID: 998
		LocalConstant = 4503599627370496UL,
		// Token: 0x040003E7 RID: 999
		ImportScope = 9007199254740992UL,
		// Token: 0x040003E8 RID: 1000
		StateMachineMethod = 18014398509481984UL,
		// Token: 0x040003E9 RID: 1001
		CustomDebugInformation = 36028797018963968UL,
		// Token: 0x040003EA RID: 1002
		PtrTables = 4718760UL,
		// Token: 0x040003EB RID: 1003
		EncTables = 3221225472UL,
		// Token: 0x040003EC RID: 1004
		TypeSystemTables = 34952443854847UL,
		// Token: 0x040003ED RID: 1005
		DebugTables = 71776119061217280UL,
		// Token: 0x040003EE RID: 1006
		AllTables = 71811071505072127UL,
		// Token: 0x040003EF RID: 1007
		ValidPortablePdbExternalTables = 34949217910615UL
	}
}
