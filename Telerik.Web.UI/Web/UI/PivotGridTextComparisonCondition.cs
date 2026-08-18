using System;
using Telerik.Web.UI.PivotGrid.Core.Filtering;

namespace Telerik.Web.UI
{
	// Token: 0x02000DBF RID: 3519
	[Serializable]
	public class PivotGridTextComparisonCondition : IFilterCondition, IPivotTextCondition
	{
		// Token: 0x0600835F RID: 33631 RVA: 0x001DF39C File Offset: 0x001DD59C
		Condition IFilterCondition.GetDataEngineFilterCondition()
		{
			return new TextCondition
			{
				Comparison = this.Comparison,
				Pattern = this.Pattern,
				IgnoreCase = this.IgnoreCase
			};
		}

		// Token: 0x17002983 RID: 10627
		// (get) Token: 0x06008360 RID: 33632 RVA: 0x001DF3D4 File Offset: 0x001DD5D4
		// (set) Token: 0x06008361 RID: 33633 RVA: 0x001DF3DC File Offset: 0x001DD5DC
		public bool IgnoreCase { get; set; }

		// Token: 0x17002984 RID: 10628
		// (get) Token: 0x06008362 RID: 33634 RVA: 0x001DF3E5 File Offset: 0x001DD5E5
		// (set) Token: 0x06008363 RID: 33635 RVA: 0x001DF3ED File Offset: 0x001DD5ED
		public string Pattern { get; set; }

		// Token: 0x17002985 RID: 10629
		// (get) Token: 0x06008364 RID: 33636 RVA: 0x001DF3F6 File Offset: 0x001DD5F6
		// (set) Token: 0x06008365 RID: 33637 RVA: 0x001DF3FE File Offset: 0x001DD5FE
		public TextComparison Comparison { get; set; }
	}
}
