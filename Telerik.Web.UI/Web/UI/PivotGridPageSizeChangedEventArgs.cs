using System;
using System.Diagnostics.CodeAnalysis;

namespace Telerik.Web.UI
{
	// Token: 0x02000DD6 RID: 3542
	public class PivotGridPageSizeChangedEventArgs : PivotGridCommandEventArgs
	{
		// Token: 0x0600839F RID: 33695 RVA: 0x001DFE82 File Offset: 0x001DE082
		[SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.Convert.ToInt32(System.Object)")]
		public PivotGridPageSizeChangedEventArgs(string name, object argument) : base(name, argument)
		{
			this.NewPageSize = Convert.ToInt32(argument);
		}

		// Token: 0x17002990 RID: 10640
		// (get) Token: 0x060083A0 RID: 33696 RVA: 0x001DFE98 File Offset: 0x001DE098
		// (set) Token: 0x060083A1 RID: 33697 RVA: 0x001DFEA0 File Offset: 0x001DE0A0
		public int NewPageSize { get; internal set; }

		// Token: 0x060083A2 RID: 33698 RVA: 0x001DFEAC File Offset: 0x001DE0AC
		public override void ExecuteCommand(object source)
		{
			RadPivotGrid radPivotGrid = source as RadPivotGrid;
			if (radPivotGrid != null)
			{
				radPivotGrid.FirePageSizeChanged(this);
			}
			PivotGridItem pivotGridItem = source as PivotGridItem;
			if (pivotGridItem != null)
			{
				pivotGridItem.OwnerPivotGrid.FirePageSizeChanged(this);
				if (this.Canceled)
				{
					return;
				}
				pivotGridItem.OwnerPivotGrid.PageSize = this.NewPageSize;
				pivotGridItem.OwnerPivotGrid.CurrentPageIndex = 0;
				PivotGridRebindReason rebindReason = PivotGridRebindReason.PostBackEvent;
				pivotGridItem.OwnerPivotGrid.ObtainDataSource(rebindReason, false);
				pivotGridItem.OwnerPivotGrid.DataBind();
			}
		}
	}
}
