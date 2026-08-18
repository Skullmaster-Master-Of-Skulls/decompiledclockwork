using System;

namespace System.Xml.Serialization
{
	// Token: 0x0200012F RID: 303
	[Flags]
	public enum CodeGenerationOptions
	{
		// Token: 0x04000A5B RID: 2651
		[XmlIgnore]
		None = 0,
		// Token: 0x04000A5C RID: 2652
		[XmlEnum("properties")]
		GenerateProperties = 1,
		// Token: 0x04000A5D RID: 2653
		[XmlEnum("newAsync")]
		GenerateNewAsync = 2,
		// Token: 0x04000A5E RID: 2654
		[XmlEnum("oldAsync")]
		GenerateOldAsync = 4,
		// Token: 0x04000A5F RID: 2655
		[XmlEnum("order")]
		GenerateOrder = 8,
		// Token: 0x04000A60 RID: 2656
		[XmlEnum("enableDataBinding")]
		EnableDataBinding = 16
	}
}
