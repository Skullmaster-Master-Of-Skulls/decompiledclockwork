using System;

namespace System.Xml.Serialization
{
	// Token: 0x0200017D RID: 381
	internal enum TypeFlags
	{
		// Token: 0x04000B7B RID: 2939
		None,
		// Token: 0x04000B7C RID: 2940
		Abstract,
		// Token: 0x04000B7D RID: 2941
		Reference,
		// Token: 0x04000B7E RID: 2942
		Special = 4,
		// Token: 0x04000B7F RID: 2943
		CanBeAttributeValue = 8,
		// Token: 0x04000B80 RID: 2944
		CanBeTextValue = 16,
		// Token: 0x04000B81 RID: 2945
		CanBeElementValue = 32,
		// Token: 0x04000B82 RID: 2946
		HasCustomFormatter = 64,
		// Token: 0x04000B83 RID: 2947
		AmbiguousDataType = 128,
		// Token: 0x04000B84 RID: 2948
		IgnoreDefault = 512,
		// Token: 0x04000B85 RID: 2949
		HasIsEmpty = 1024,
		// Token: 0x04000B86 RID: 2950
		HasDefaultConstructor = 2048,
		// Token: 0x04000B87 RID: 2951
		XmlEncodingNotRequired = 4096,
		// Token: 0x04000B88 RID: 2952
		UseReflection = 16384,
		// Token: 0x04000B89 RID: 2953
		CollapseWhitespace = 32768,
		// Token: 0x04000B8A RID: 2954
		OptionalValue = 65536,
		// Token: 0x04000B8B RID: 2955
		CtorInaccessible = 131072,
		// Token: 0x04000B8C RID: 2956
		UsePrivateImplementation = 262144,
		// Token: 0x04000B8D RID: 2957
		GenericInterface = 524288,
		// Token: 0x04000B8E RID: 2958
		Unsupported = 1048576
	}
}
