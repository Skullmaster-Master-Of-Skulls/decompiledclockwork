using System;

namespace System.IdentityModel
{
	// Token: 0x02000047 RID: 71
	internal interface IPrefixGenerator
	{
		// Token: 0x060002CA RID: 714
		string GetPrefix(string namespaceUri, int depth, bool isForAttribute);
	}
}
