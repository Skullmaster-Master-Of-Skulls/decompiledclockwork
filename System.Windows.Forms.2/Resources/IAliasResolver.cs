using System;
using System.Reflection;

namespace System.Resources
{
	// Token: 0x020000F4 RID: 244
	internal interface IAliasResolver
	{
		// Token: 0x060003BC RID: 956
		AssemblyName ResolveAlias(string alias);

		// Token: 0x060003BD RID: 957
		void PushAlias(string alias, AssemblyName name);
	}
}
