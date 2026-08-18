using System;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x02000069 RID: 105
	internal enum TableIndex : byte
	{
		// Token: 0x0400036A RID: 874
		Module,
		// Token: 0x0400036B RID: 875
		TypeRef,
		// Token: 0x0400036C RID: 876
		TypeDef,
		// Token: 0x0400036D RID: 877
		FieldPtr,
		// Token: 0x0400036E RID: 878
		Field,
		// Token: 0x0400036F RID: 879
		MethodPtr,
		// Token: 0x04000370 RID: 880
		MethodDef,
		// Token: 0x04000371 RID: 881
		ParamPtr,
		// Token: 0x04000372 RID: 882
		Param,
		// Token: 0x04000373 RID: 883
		InterfaceImpl,
		// Token: 0x04000374 RID: 884
		MemberRef,
		// Token: 0x04000375 RID: 885
		Constant,
		// Token: 0x04000376 RID: 886
		CustomAttribute,
		// Token: 0x04000377 RID: 887
		FieldMarshal,
		// Token: 0x04000378 RID: 888
		DeclSecurity,
		// Token: 0x04000379 RID: 889
		ClassLayout,
		// Token: 0x0400037A RID: 890
		FieldLayout,
		// Token: 0x0400037B RID: 891
		StandAloneSig,
		// Token: 0x0400037C RID: 892
		EventMap,
		// Token: 0x0400037D RID: 893
		EventPtr,
		// Token: 0x0400037E RID: 894
		Event,
		// Token: 0x0400037F RID: 895
		PropertyMap,
		// Token: 0x04000380 RID: 896
		PropertyPtr,
		// Token: 0x04000381 RID: 897
		Property,
		// Token: 0x04000382 RID: 898
		MethodSemantics,
		// Token: 0x04000383 RID: 899
		MethodImpl,
		// Token: 0x04000384 RID: 900
		ModuleRef,
		// Token: 0x04000385 RID: 901
		TypeSpec,
		// Token: 0x04000386 RID: 902
		ImplMap,
		// Token: 0x04000387 RID: 903
		FieldRva,
		// Token: 0x04000388 RID: 904
		EncLog,
		// Token: 0x04000389 RID: 905
		EncMap,
		// Token: 0x0400038A RID: 906
		Assembly,
		// Token: 0x0400038B RID: 907
		AssemblyProcessor,
		// Token: 0x0400038C RID: 908
		AssemblyOS,
		// Token: 0x0400038D RID: 909
		AssemblyRef,
		// Token: 0x0400038E RID: 910
		AssemblyRefProcessor,
		// Token: 0x0400038F RID: 911
		AssemblyRefOS,
		// Token: 0x04000390 RID: 912
		File,
		// Token: 0x04000391 RID: 913
		ExportedType,
		// Token: 0x04000392 RID: 914
		ManifestResource,
		// Token: 0x04000393 RID: 915
		NestedClass,
		// Token: 0x04000394 RID: 916
		GenericParam,
		// Token: 0x04000395 RID: 917
		MethodSpec,
		// Token: 0x04000396 RID: 918
		GenericParamConstraint,
		// Token: 0x04000397 RID: 919
		Document = 48,
		// Token: 0x04000398 RID: 920
		MethodDebugInformation,
		// Token: 0x04000399 RID: 921
		LocalScope,
		// Token: 0x0400039A RID: 922
		LocalVariable,
		// Token: 0x0400039B RID: 923
		LocalConstant,
		// Token: 0x0400039C RID: 924
		ImportScope,
		// Token: 0x0400039D RID: 925
		StateMachineMethod,
		// Token: 0x0400039E RID: 926
		CustomDebugInformation
	}
}
