using System;

namespace Telerik.Web.UI
{
	// Token: 0x02000C39 RID: 3129
	public class PivotGridInitFilterDialogueEventArgs : PivotGridCommandEventArgs
	{
		// Token: 0x06007679 RID: 30329 RVA: 0x001B832C File Offset: 0x001B652C
		public PivotGridInitFilterDialogueEventArgs(RadPivotGrid pivotGrid, PivotGridItem item, object commandSource, object argument) : base(item, commandSource, "Sort", argument)
		{
			this.ownerPivotGrid = pivotGrid;
		}

		// Token: 0x17002684 RID: 9860
		// (get) Token: 0x0600767A RID: 30330 RVA: 0x001B8344 File Offset: 0x001B6544
		public RadPivotGrid OwnerPivotGrid
		{
			get
			{
				return this.ownerPivotGrid;
			}
		}

		// Token: 0x17002685 RID: 9861
		// (get) Token: 0x0600767B RID: 30331 RVA: 0x001B834C File Offset: 0x001B654C
		// (set) Token: 0x0600767C RID: 30332 RVA: 0x001B8354 File Offset: 0x001B6554
		public string FieldName { get; set; }

		// Token: 0x0600767D RID: 30333 RVA: 0x001B8360 File Offset: 0x001B6560
		public override void ExecuteCommand(object source)
		{
			this.OwnerPivotGrid.FireInitFilterDialogue(this);
			if (this.Canceled)
			{
				return;
			}
			base.CommandArgument.ToString();
			this.OwnerPivotGrid.FilteringManager.FieldUniqueName = base.CommandArgument.ToString();
			this.OwnerPivotGrid.FilteringManager.IsInitFilterCommandInProgress = true;
			this.OwnerPivotGrid.ShouldCreateFilterWindow = true;
			if (!this.OwnerPivotGrid.FilterWindow.IsReportFilter)
			{
				this.OwnerPivotGrid.ShouldCreateFilterDialog = true;
			}
			this.OwnerPivotGrid.ResetPivotModel();
			this.OwnerPivotGrid.ObtainDataSource(PivotGridRebindReason.PostBackEvent);
			this.OwnerPivotGrid.DataBind();
		}

		// Token: 0x0400208E RID: 8334
		private readonly RadPivotGrid ownerPivotGrid;
	}
}
