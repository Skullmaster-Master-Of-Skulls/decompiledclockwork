using System;

namespace System.Data.Entity.SqlServer.SqlGen
{
	// Token: 0x0200001C RID: 28
	internal class BoolWrapper
	{
		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060001AD RID: 429 RVA: 0x000061B4 File Offset: 0x000043B4
		// (set) Token: 0x060001AE RID: 430 RVA: 0x000061BC File Offset: 0x000043BC
		internal bool Value { get; set; }

		// Token: 0x060001AF RID: 431 RVA: 0x000061C5 File Offset: 0x000043C5
		internal BoolWrapper()
		{
			this.Value = false;
		}
	}
}
