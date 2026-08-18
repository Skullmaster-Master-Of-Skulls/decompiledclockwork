using System;
using System.Collections;

namespace System.Web.Util
{
	// Token: 0x02000210 RID: 528
	internal class AssemblySet : ObjectSet
	{
		// Token: 0x060019B2 RID: 6578 RVA: 0x00050283 File Offset: 0x0004E483
		internal AssemblySet()
		{
		}

		// Token: 0x060019B3 RID: 6579 RVA: 0x00050294 File Offset: 0x0004E494
		internal static AssemblySet Create(ICollection c)
		{
			AssemblySet assemblySet = new AssemblySet();
			assemblySet.AddCollection(c);
			return assemblySet;
		}
	}
}
