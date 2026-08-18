using System;

namespace System.Data.Entity.Core.Objects
{
	// Token: 0x02000571 RID: 1393
	internal sealed class IntBox
	{
		// Token: 0x06003654 RID: 13908 RVA: 0x001030F9 File Offset: 0x001012F9
		internal IntBox(int val)
		{
			this.Value = val;
		}

		// Token: 0x1700081F RID: 2079
		// (get) Token: 0x06003655 RID: 13909 RVA: 0x00103108 File Offset: 0x00101308
		// (set) Token: 0x06003656 RID: 13910 RVA: 0x00103110 File Offset: 0x00101310
		internal int Value { get; set; }
	}
}
