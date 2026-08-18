using System;
using System.Collections.Generic;

namespace TechnoPro.Common.DynamicCompiler
{
	// Token: 0x02000004 RID: 4
	public class CustomCSharpCode
	{
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000020 RID: 32 RVA: 0x00002B80 File Offset: 0x00000D80
		// (set) Token: 0x06000021 RID: 33 RVA: 0x00002B88 File Offset: 0x00000D88
		public string Code { get; set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000022 RID: 34 RVA: 0x00002B91 File Offset: 0x00000D91
		// (set) Token: 0x06000023 RID: 35 RVA: 0x00002B99 File Offset: 0x00000D99
		public IList<string> Imports { get; set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000024 RID: 36 RVA: 0x00002BA2 File Offset: 0x00000DA2
		// (set) Token: 0x06000025 RID: 37 RVA: 0x00002BAA File Offset: 0x00000DAA
		public string BinPath { get; set; }
	}
}
