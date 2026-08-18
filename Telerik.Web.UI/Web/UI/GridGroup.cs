using System;
using System.Collections;

namespace Telerik.Web.UI
{
	// Token: 0x02000B7A RID: 2938
	internal class GridGroup
	{
		// Token: 0x17002468 RID: 9320
		// (get) Token: 0x06006EF2 RID: 28402 RVA: 0x0019BAC6 File Offset: 0x00199CC6
		// (set) Token: 0x06006EF3 RID: 28403 RVA: 0x0019BACE File Offset: 0x00199CCE
		public string FieldName { get; set; }

		// Token: 0x17002469 RID: 9321
		// (get) Token: 0x06006EF4 RID: 28404 RVA: 0x0019BAD7 File Offset: 0x00199CD7
		// (set) Token: 0x06006EF5 RID: 28405 RVA: 0x0019BADF File Offset: 0x00199CDF
		public IEnumerable Items { get; set; }

		// Token: 0x1700246A RID: 9322
		// (get) Token: 0x06006EF6 RID: 28406 RVA: 0x0019BAE8 File Offset: 0x00199CE8
		// (set) Token: 0x06006EF7 RID: 28407 RVA: 0x0019BAF0 File Offset: 0x00199CF0
		public object Key { get; set; }

		// Token: 0x1700246B RID: 9323
		// (get) Token: 0x06006EF8 RID: 28408 RVA: 0x0019BAF9 File Offset: 0x00199CF9
		// (set) Token: 0x06006EF9 RID: 28409 RVA: 0x0019BB01 File Offset: 0x00199D01
		internal GridGroup ParentGroup { get; set; }

		// Token: 0x1700246C RID: 9324
		// (get) Token: 0x06006EFA RID: 28410 RVA: 0x0019BB0A File Offset: 0x00199D0A
		// (set) Token: 0x06006EFB RID: 28411 RVA: 0x0019BB12 File Offset: 0x00199D12
		public int Level { get; set; }
	}
}
