using System;

namespace System.Data.Objects
{
	// Token: 0x0200013D RID: 317
	internal sealed class IntBox
	{
		// Token: 0x060016E7 RID: 5863 RVA: 0x0004C49C File Offset: 0x0004A69C
		internal IntBox(int val)
		{
			this.val = val;
		}

		// Token: 0x170004A9 RID: 1193
		// (get) Token: 0x060016E8 RID: 5864 RVA: 0x0004C4AB File Offset: 0x0004A6AB
		// (set) Token: 0x060016E9 RID: 5865 RVA: 0x0004C4B3 File Offset: 0x0004A6B3
		internal int Value
		{
			get
			{
				return this.val;
			}
			set
			{
				this.val = value;
			}
		}

		// Token: 0x04000A6C RID: 2668
		private int val;
	}
}
