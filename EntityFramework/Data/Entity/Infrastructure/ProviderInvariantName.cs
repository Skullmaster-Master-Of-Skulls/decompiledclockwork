using System;

namespace System.Data.Entity.Infrastructure
{
	// Token: 0x0200028F RID: 655
	internal class ProviderInvariantName : IProviderInvariantName
	{
		// Token: 0x060016FD RID: 5885 RVA: 0x00072C16 File Offset: 0x00070E16
		public ProviderInvariantName(string name)
		{
			this.Name = name;
		}

		// Token: 0x1700029A RID: 666
		// (get) Token: 0x060016FE RID: 5886 RVA: 0x00072C25 File Offset: 0x00070E25
		// (set) Token: 0x060016FF RID: 5887 RVA: 0x00072C2D File Offset: 0x00070E2D
		public string Name { get; private set; }
	}
}
