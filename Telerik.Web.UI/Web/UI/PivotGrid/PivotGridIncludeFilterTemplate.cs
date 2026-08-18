using System;
using System.Web.UI;
using Telerik.Web.UI.PivotGrid.Core;

namespace Telerik.Web.UI.PivotGrid
{
	// Token: 0x02000DF6 RID: 3574
	public class PivotGridIncludeFilterTemplate : ITemplate
	{
		// Token: 0x060084C3 RID: 33987 RVA: 0x001E4882 File Offset: 0x001E2A82
		public PivotGridIncludeFilterTemplate(RadPivotGrid pivotGrid)
		{
			this.ownerPivotGrid = pivotGrid;
		}

		// Token: 0x060084C4 RID: 33988 RVA: 0x001E4894 File Offset: 0x001E2A94
		public void InstantiateIn(Control container)
		{
			new RadListBox();
			this.ownerPivotGrid.PivotModel.DataProvider.Results.GetUniqueKeys(PivotAxis.Rows, 0);
			RadListBox radListBox = new RadListBox();
			radListBox.Items.Add(new RadListBoxItem
			{
				Text = "Item1",
				Value = "Item1",
				Checkable = true
			});
			container.Controls.Add(radListBox);
		}

		// Token: 0x040024FA RID: 9466
		private RadPivotGrid ownerPivotGrid;
	}
}
