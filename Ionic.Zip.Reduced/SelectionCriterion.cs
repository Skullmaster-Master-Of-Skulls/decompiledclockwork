using System;
using System.Diagnostics;
using Ionic.Zip;

namespace Ionic
{
	// Token: 0x02000019 RID: 25
	internal abstract class SelectionCriterion
	{
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000060 RID: 96 RVA: 0x000025EA File Offset: 0x000007EA
		// (set) Token: 0x06000061 RID: 97 RVA: 0x000025F2 File Offset: 0x000007F2
		internal virtual bool Verbose { get; set; }

		// Token: 0x06000062 RID: 98
		internal abstract bool Evaluate(string filename);

		// Token: 0x06000063 RID: 99 RVA: 0x000025FB File Offset: 0x000007FB
		[Conditional("SelectorTrace")]
		protected static void CriterionTrace(string format, params object[] args)
		{
		}

		// Token: 0x06000064 RID: 100
		internal abstract bool Evaluate(ZipEntry entry);
	}
}
