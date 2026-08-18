using System;

namespace System.Data.Entity.Core.Objects
{
	// Token: 0x020005A1 RID: 1441
	public sealed class ObjectContextOptions
	{
		// Token: 0x06003906 RID: 14598 RVA: 0x0010FE62 File Offset: 0x0010E062
		internal ObjectContextOptions()
		{
			this.ProxyCreationEnabled = true;
			this.EnsureTransactionsForFunctionsAndCommands = true;
		}

		// Token: 0x1700089E RID: 2206
		// (get) Token: 0x06003907 RID: 14599 RVA: 0x0010FE78 File Offset: 0x0010E078
		// (set) Token: 0x06003908 RID: 14600 RVA: 0x0010FE80 File Offset: 0x0010E080
		public bool EnsureTransactionsForFunctionsAndCommands { get; set; }

		// Token: 0x1700089F RID: 2207
		// (get) Token: 0x06003909 RID: 14601 RVA: 0x0010FE89 File Offset: 0x0010E089
		// (set) Token: 0x0600390A RID: 14602 RVA: 0x0010FE91 File Offset: 0x0010E091
		public bool LazyLoadingEnabled { get; set; }

		// Token: 0x170008A0 RID: 2208
		// (get) Token: 0x0600390B RID: 14603 RVA: 0x0010FE9A File Offset: 0x0010E09A
		// (set) Token: 0x0600390C RID: 14604 RVA: 0x0010FEA2 File Offset: 0x0010E0A2
		public bool ProxyCreationEnabled { get; set; }

		// Token: 0x170008A1 RID: 2209
		// (get) Token: 0x0600390D RID: 14605 RVA: 0x0010FEAB File Offset: 0x0010E0AB
		// (set) Token: 0x0600390E RID: 14606 RVA: 0x0010FEB3 File Offset: 0x0010E0B3
		public bool UseLegacyPreserveChangesBehavior { get; set; }

		// Token: 0x170008A2 RID: 2210
		// (get) Token: 0x0600390F RID: 14607 RVA: 0x0010FEBC File Offset: 0x0010E0BC
		// (set) Token: 0x06003910 RID: 14608 RVA: 0x0010FEC4 File Offset: 0x0010E0C4
		public bool UseConsistentNullReferenceBehavior { get; set; }

		// Token: 0x170008A3 RID: 2211
		// (get) Token: 0x06003911 RID: 14609 RVA: 0x0010FECD File Offset: 0x0010E0CD
		// (set) Token: 0x06003912 RID: 14610 RVA: 0x0010FED5 File Offset: 0x0010E0D5
		public bool UseCSharpNullComparisonBehavior { get; set; }
	}
}
