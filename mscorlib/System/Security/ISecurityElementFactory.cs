using System;

namespace System.Security
{
	// Token: 0x02000611 RID: 1553
	internal interface ISecurityElementFactory
	{
		// Token: 0x06003820 RID: 14368
		SecurityElement CreateSecurityElement();

		// Token: 0x06003821 RID: 14369
		object Copy();

		// Token: 0x06003822 RID: 14370
		string GetTag();

		// Token: 0x06003823 RID: 14371
		string Attribute(string attributeName);
	}
}
