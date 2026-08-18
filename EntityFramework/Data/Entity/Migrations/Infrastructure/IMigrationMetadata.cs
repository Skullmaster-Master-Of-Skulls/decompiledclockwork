using System;

namespace System.Data.Entity.Migrations.Infrastructure
{
	// Token: 0x020006FC RID: 1788
	public interface IMigrationMetadata
	{
		// Token: 0x17000AAE RID: 2734
		// (get) Token: 0x060048A0 RID: 18592
		string Id { get; }

		// Token: 0x17000AAF RID: 2735
		// (get) Token: 0x060048A1 RID: 18593
		string Source { get; }

		// Token: 0x17000AB0 RID: 2736
		// (get) Token: 0x060048A2 RID: 18594
		string Target { get; }
	}
}
