using System;

namespace Telerik.Web.UI
{
	// Token: 0x02000C34 RID: 3124
	public class PivotGridExpandCollapseLevelEventArgs : PivotGridCommandEventArgs
	{
		// Token: 0x06007666 RID: 30310 RVA: 0x001B7C54 File Offset: 0x001B5E54
		public PivotGridExpandCollapseLevelEventArgs(PivotGridItem item, object commandSource, object argument) : base(item, commandSource, "ExpandCollapseLevel", argument)
		{
			string[] array = base.CommandArgument.ToString().Split(new char[]
			{
				';'
			});
			this.OperationType = (PivotGridExpandCollapseOperationType)Enum.Parse(typeof(PivotGridExpandCollapseOperationType), array[0]);
			this.GroupType = (PivotGridGroupType)Enum.Parse(typeof(PivotGridGroupType), array[1]);
			this.Level = int.Parse(array[2]);
		}

		// Token: 0x1700267C RID: 9852
		// (get) Token: 0x06007667 RID: 30311 RVA: 0x001B7CD5 File Offset: 0x001B5ED5
		protected virtual RadPivotGrid OwnerPivotGrid
		{
			get
			{
				return this.Item.OwnerPivotGrid;
			}
		}

		// Token: 0x1700267D RID: 9853
		// (get) Token: 0x06007668 RID: 30312 RVA: 0x001B7CE2 File Offset: 0x001B5EE2
		// (set) Token: 0x06007669 RID: 30313 RVA: 0x001B7CEA File Offset: 0x001B5EEA
		public PivotGridGroupType GroupType { get; set; }

		// Token: 0x1700267E RID: 9854
		// (get) Token: 0x0600766A RID: 30314 RVA: 0x001B7CF3 File Offset: 0x001B5EF3
		// (set) Token: 0x0600766B RID: 30315 RVA: 0x001B7CFB File Offset: 0x001B5EFB
		public PivotGridExpandCollapseOperationType OperationType { get; set; }

		// Token: 0x1700267F RID: 9855
		// (get) Token: 0x0600766C RID: 30316 RVA: 0x001B7D04 File Offset: 0x001B5F04
		// (set) Token: 0x0600766D RID: 30317 RVA: 0x001B7D0C File Offset: 0x001B5F0C
		public int Level { get; set; }

		// Token: 0x0600766E RID: 30318 RVA: 0x001B7D18 File Offset: 0x001B5F18
		public override void ExecuteCommand(object source)
		{
			base.ExecuteCommand(source);
			this.OwnerPivotGrid.FireExpandCollapseLevel(this);
			if (this.Canceled)
			{
				return;
			}
			switch (this.GroupType)
			{
			case PivotGridGroupType.Columns:
				switch (this.OperationType)
				{
				case PivotGridExpandCollapseOperationType.Expand:
					if (this.Level == -1)
					{
						this.OwnerPivotGrid.ExpandAllColumnGroups(false);
					}
					else
					{
						this.OwnerPivotGrid.ExpandAllColumnGroups(this.Level, false);
					}
					break;
				case PivotGridExpandCollapseOperationType.Collapse:
					if (this.Level == -1)
					{
						this.OwnerPivotGrid.CollapseAllColumnGroups(false);
					}
					else
					{
						this.OwnerPivotGrid.CollapseAllColumnGroups(this.Level, false);
					}
					break;
				}
				break;
			case PivotGridGroupType.Rows:
				switch (this.OperationType)
				{
				case PivotGridExpandCollapseOperationType.Expand:
					if (this.Level == -1)
					{
						this.OwnerPivotGrid.ExpandAllRowGroups(false);
					}
					else
					{
						this.OwnerPivotGrid.ExpandAllRowGroups(this.Level, false);
					}
					break;
				case PivotGridExpandCollapseOperationType.Collapse:
					if (this.Level == -1)
					{
						this.OwnerPivotGrid.CollapseAllRowGroups(false);
					}
					else
					{
						this.OwnerPivotGrid.CollapseAllRowGroups(this.Level, false);
					}
					break;
				}
				break;
			}
			PivotGridRebindReason rebindReason = PivotGridRebindReason.PostBackEvent;
			this.OwnerPivotGrid.ResetPivotModel();
			this.OwnerPivotGrid.ObtainDataSource(rebindReason, false);
			this.OwnerPivotGrid.DataBind();
		}
	}
}
