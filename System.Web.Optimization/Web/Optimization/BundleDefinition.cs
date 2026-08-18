using System;
using System.Collections.Generic;

namespace System.Web.Optimization
{
	// Token: 0x02000014 RID: 20
	public sealed class BundleDefinition
	{
		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000C1 RID: 193 RVA: 0x00003E98 File Offset: 0x00002098
		// (set) Token: 0x060000C2 RID: 194 RVA: 0x00003EA0 File Offset: 0x000020A0
		public string Path { get; set; }

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000C3 RID: 195 RVA: 0x00003EA9 File Offset: 0x000020A9
		// (set) Token: 0x060000C4 RID: 196 RVA: 0x00003EB1 File Offset: 0x000020B1
		public string CdnPath { get; set; }

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000C5 RID: 197 RVA: 0x00003EBA File Offset: 0x000020BA
		// (set) Token: 0x060000C6 RID: 198 RVA: 0x00003EC2 File Offset: 0x000020C2
		public string CdnFallbackExpression { get; set; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x00003ECB File Offset: 0x000020CB
		// (set) Token: 0x060000C8 RID: 200 RVA: 0x00003ED3 File Offset: 0x000020D3
		public IList<string> Includes { get; internal set; }
	}
}
