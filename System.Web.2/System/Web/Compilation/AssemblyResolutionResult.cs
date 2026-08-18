using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Build.Framework;

namespace System.Web.Compilation
{
	// Token: 0x020007F7 RID: 2039
	internal class AssemblyResolutionResult
	{
		// Token: 0x17001B9F RID: 7071
		// (get) Token: 0x0600613A RID: 24890 RVA: 0x001503D6 File Offset: 0x0014E5D6
		// (set) Token: 0x0600613B RID: 24891 RVA: 0x001503DE File Offset: 0x0014E5DE
		internal ICollection<string> ResolvedFiles { get; set; }

		// Token: 0x17001BA0 RID: 7072
		// (get) Token: 0x0600613C RID: 24892 RVA: 0x001503E7 File Offset: 0x0014E5E7
		// (set) Token: 0x0600613D RID: 24893 RVA: 0x001503EF File Offset: 0x0014E5EF
		internal ICollection<string> ResolvedFilesWithWarnings { get; set; }

		// Token: 0x17001BA1 RID: 7073
		// (get) Token: 0x0600613E RID: 24894 RVA: 0x001503F8 File Offset: 0x0014E5F8
		// (set) Token: 0x0600613F RID: 24895 RVA: 0x00150400 File Offset: 0x0014E600
		internal ICollection<Assembly> UnresolvedAssemblies { get; set; }

		// Token: 0x17001BA2 RID: 7074
		// (get) Token: 0x06006140 RID: 24896 RVA: 0x00150409 File Offset: 0x0014E609
		// (set) Token: 0x06006141 RID: 24897 RVA: 0x00150411 File Offset: 0x0014E611
		internal ICollection<BuildErrorEventArgs> Errors { get; set; }

		// Token: 0x17001BA3 RID: 7075
		// (get) Token: 0x06006142 RID: 24898 RVA: 0x0015041A File Offset: 0x0014E61A
		// (set) Token: 0x06006143 RID: 24899 RVA: 0x00150422 File Offset: 0x0014E622
		internal ICollection<BuildWarningEventArgs> Warnings { get; set; }
	}
}
