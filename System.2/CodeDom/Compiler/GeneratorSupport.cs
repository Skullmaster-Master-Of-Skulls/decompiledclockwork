using System;

namespace System.CodeDom.Compiler
{
	// Token: 0x0200067D RID: 1661
	[Flags]
	[Serializable]
	public enum GeneratorSupport
	{
		// Token: 0x04002CA3 RID: 11427
		ArraysOfArrays = 1,
		// Token: 0x04002CA4 RID: 11428
		EntryPointMethod = 2,
		// Token: 0x04002CA5 RID: 11429
		GotoStatements = 4,
		// Token: 0x04002CA6 RID: 11430
		MultidimensionalArrays = 8,
		// Token: 0x04002CA7 RID: 11431
		StaticConstructors = 16,
		// Token: 0x04002CA8 RID: 11432
		TryCatchStatements = 32,
		// Token: 0x04002CA9 RID: 11433
		ReturnTypeAttributes = 64,
		// Token: 0x04002CAA RID: 11434
		DeclareValueTypes = 128,
		// Token: 0x04002CAB RID: 11435
		DeclareEnums = 256,
		// Token: 0x04002CAC RID: 11436
		DeclareDelegates = 512,
		// Token: 0x04002CAD RID: 11437
		DeclareInterfaces = 1024,
		// Token: 0x04002CAE RID: 11438
		DeclareEvents = 2048,
		// Token: 0x04002CAF RID: 11439
		AssemblyAttributes = 4096,
		// Token: 0x04002CB0 RID: 11440
		ParameterAttributes = 8192,
		// Token: 0x04002CB1 RID: 11441
		ReferenceParameters = 16384,
		// Token: 0x04002CB2 RID: 11442
		ChainedConstructorArguments = 32768,
		// Token: 0x04002CB3 RID: 11443
		NestedTypes = 65536,
		// Token: 0x04002CB4 RID: 11444
		MultipleInterfaceMembers = 131072,
		// Token: 0x04002CB5 RID: 11445
		PublicStaticMembers = 262144,
		// Token: 0x04002CB6 RID: 11446
		ComplexExpressions = 524288,
		// Token: 0x04002CB7 RID: 11447
		Win32Resources = 1048576,
		// Token: 0x04002CB8 RID: 11448
		Resources = 2097152,
		// Token: 0x04002CB9 RID: 11449
		PartialTypes = 4194304,
		// Token: 0x04002CBA RID: 11450
		GenericTypeReference = 8388608,
		// Token: 0x04002CBB RID: 11451
		GenericTypeDeclaration = 16777216,
		// Token: 0x04002CBC RID: 11452
		DeclareIndexerProperties = 33554432
	}
}
