using System;

namespace System.Reflection.Metadata
{
	// Token: 0x02000055 RID: 85
	internal enum HandleKind : byte
	{
		// Token: 0x04000304 RID: 772
		ModuleDefinition,
		// Token: 0x04000305 RID: 773
		TypeReference,
		// Token: 0x04000306 RID: 774
		TypeDefinition,
		// Token: 0x04000307 RID: 775
		FieldDefinition = 4,
		// Token: 0x04000308 RID: 776
		MethodDefinition = 6,
		// Token: 0x04000309 RID: 777
		Parameter = 8,
		// Token: 0x0400030A RID: 778
		InterfaceImplementation,
		// Token: 0x0400030B RID: 779
		MemberReference,
		// Token: 0x0400030C RID: 780
		Constant,
		// Token: 0x0400030D RID: 781
		CustomAttribute,
		// Token: 0x0400030E RID: 782
		DeclarativeSecurityAttribute = 14,
		// Token: 0x0400030F RID: 783
		StandaloneSignature = 17,
		// Token: 0x04000310 RID: 784
		EventDefinition = 20,
		// Token: 0x04000311 RID: 785
		PropertyDefinition = 23,
		// Token: 0x04000312 RID: 786
		MethodImplementation = 25,
		// Token: 0x04000313 RID: 787
		ModuleReference,
		// Token: 0x04000314 RID: 788
		TypeSpecification,
		// Token: 0x04000315 RID: 789
		AssemblyDefinition = 32,
		// Token: 0x04000316 RID: 790
		AssemblyFile = 38,
		// Token: 0x04000317 RID: 791
		AssemblyReference = 35,
		// Token: 0x04000318 RID: 792
		ExportedType = 39,
		// Token: 0x04000319 RID: 793
		GenericParameter = 42,
		// Token: 0x0400031A RID: 794
		MethodSpecification,
		// Token: 0x0400031B RID: 795
		GenericParameterConstraint,
		// Token: 0x0400031C RID: 796
		ManifestResource = 40,
		// Token: 0x0400031D RID: 797
		Document = 48,
		// Token: 0x0400031E RID: 798
		MethodDebugInformation,
		// Token: 0x0400031F RID: 799
		LocalScope,
		// Token: 0x04000320 RID: 800
		LocalVariable,
		// Token: 0x04000321 RID: 801
		LocalConstant,
		// Token: 0x04000322 RID: 802
		ImportScope,
		// Token: 0x04000323 RID: 803
		CustomDebugInformation = 55,
		// Token: 0x04000324 RID: 804
		NamespaceDefinition = 124,
		// Token: 0x04000325 RID: 805
		UserString = 112,
		// Token: 0x04000326 RID: 806
		String = 120,
		// Token: 0x04000327 RID: 807
		Blob = 113,
		// Token: 0x04000328 RID: 808
		Guid
	}
}
