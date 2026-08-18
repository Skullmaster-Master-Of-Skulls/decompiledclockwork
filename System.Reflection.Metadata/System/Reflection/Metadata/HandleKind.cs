using System;

namespace System.Reflection.Metadata
{
	// Token: 0x02000054 RID: 84
	public enum HandleKind : byte
	{
		// Token: 0x040002C9 RID: 713
		ModuleDefinition,
		// Token: 0x040002CA RID: 714
		TypeReference,
		// Token: 0x040002CB RID: 715
		TypeDefinition,
		// Token: 0x040002CC RID: 716
		FieldDefinition = 4,
		// Token: 0x040002CD RID: 717
		MethodDefinition = 6,
		// Token: 0x040002CE RID: 718
		Parameter = 8,
		// Token: 0x040002CF RID: 719
		InterfaceImplementation,
		// Token: 0x040002D0 RID: 720
		MemberReference,
		// Token: 0x040002D1 RID: 721
		Constant,
		// Token: 0x040002D2 RID: 722
		CustomAttribute,
		// Token: 0x040002D3 RID: 723
		DeclarativeSecurityAttribute = 14,
		// Token: 0x040002D4 RID: 724
		StandaloneSignature = 17,
		// Token: 0x040002D5 RID: 725
		EventDefinition = 20,
		// Token: 0x040002D6 RID: 726
		PropertyDefinition = 23,
		// Token: 0x040002D7 RID: 727
		MethodImplementation = 25,
		// Token: 0x040002D8 RID: 728
		ModuleReference,
		// Token: 0x040002D9 RID: 729
		TypeSpecification,
		// Token: 0x040002DA RID: 730
		AssemblyDefinition = 32,
		// Token: 0x040002DB RID: 731
		AssemblyFile = 38,
		// Token: 0x040002DC RID: 732
		AssemblyReference = 35,
		// Token: 0x040002DD RID: 733
		ExportedType = 39,
		// Token: 0x040002DE RID: 734
		GenericParameter = 42,
		// Token: 0x040002DF RID: 735
		MethodSpecification,
		// Token: 0x040002E0 RID: 736
		GenericParameterConstraint,
		// Token: 0x040002E1 RID: 737
		ManifestResource = 40,
		// Token: 0x040002E2 RID: 738
		Document = 48,
		// Token: 0x040002E3 RID: 739
		MethodDebugInformation,
		// Token: 0x040002E4 RID: 740
		LocalScope,
		// Token: 0x040002E5 RID: 741
		LocalVariable,
		// Token: 0x040002E6 RID: 742
		LocalConstant,
		// Token: 0x040002E7 RID: 743
		ImportScope,
		// Token: 0x040002E8 RID: 744
		CustomDebugInformation = 55,
		// Token: 0x040002E9 RID: 745
		NamespaceDefinition = 124,
		// Token: 0x040002EA RID: 746
		UserString = 112,
		// Token: 0x040002EB RID: 747
		String = 120,
		// Token: 0x040002EC RID: 748
		Blob = 113,
		// Token: 0x040002ED RID: 749
		Guid
	}
}
