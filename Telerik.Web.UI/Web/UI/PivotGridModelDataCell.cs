using System;

namespace Telerik.Web.UI
{
	// Token: 0x02000E07 RID: 3591
	[Serializable]
	internal class PivotGridModelDataCell : PivotGridModelCellBase
	{
		// Token: 0x17002A20 RID: 10784
		// (get) Token: 0x0600852C RID: 34092 RVA: 0x001E6453 File Offset: 0x001E4653
		// (set) Token: 0x0600852D RID: 34093 RVA: 0x001E645B File Offset: 0x001E465B
		public PivotGridDataCellType CellType { get; set; }

		// Token: 0x17002A21 RID: 10785
		// (get) Token: 0x0600852E RID: 34094 RVA: 0x001E6464 File Offset: 0x001E4664
		// (set) Token: 0x0600852F RID: 34095 RVA: 0x001E646C File Offset: 0x001E466C
		public bool DisplayValueAsKpi { get; set; }

		// Token: 0x17002A22 RID: 10786
		// (get) Token: 0x06008530 RID: 34096 RVA: 0x001E6475 File Offset: 0x001E4675
		// (set) Token: 0x06008531 RID: 34097 RVA: 0x001E647D File Offset: 0x001E467D
		public PivotGridKpiValue KpiIndicator { get; set; }

		// Token: 0x17002A23 RID: 10787
		// (get) Token: 0x06008532 RID: 34098 RVA: 0x001E6486 File Offset: 0x001E4686
		// (set) Token: 0x06008533 RID: 34099 RVA: 0x001E648E File Offset: 0x001E468E
		public PivotGridKpiType KpiType { get; set; }

		// Token: 0x17002A24 RID: 10788
		// (get) Token: 0x06008534 RID: 34100 RVA: 0x001E6497 File Offset: 0x001E4697
		// (set) Token: 0x06008535 RID: 34101 RVA: 0x001E649F File Offset: 0x001E469F
		public string FormattedValue
		{
			get
			{
				return this.formattedValue;
			}
			set
			{
				this.formattedValue = value;
			}
		}

		// Token: 0x0400252F RID: 9519
		[NonSerialized]
		private string formattedValue;
	}
}
