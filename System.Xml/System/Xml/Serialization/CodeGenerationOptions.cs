using System;

namespace System.Xml.Serialization
{
	// Token: 0x020002AE RID: 686
	[Flags]
	public enum CodeGenerationOptions
	{
		// Token: 0x04001427 RID: 5159
		[XmlIgnore]
		None = 0,
		// Token: 0x04001428 RID: 5160
		[XmlEnum("properties")]
		GenerateProperties = 1,
		// Token: 0x04001429 RID: 5161
		[XmlEnum("newAsync")]
		GenerateNewAsync = 2,
		// Token: 0x0400142A RID: 5162
		[XmlEnum("oldAsync")]
		GenerateOldAsync = 4,
		// Token: 0x0400142B RID: 5163
		[XmlEnum("order")]
		GenerateOrder = 8,
		// Token: 0x0400142C RID: 5164
		[XmlEnum("enableDataBinding")]
		EnableDataBinding = 16
	}
}
