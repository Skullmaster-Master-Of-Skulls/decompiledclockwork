using System;

namespace Telerik.Web.UI
{
	// Token: 0x02000C37 RID: 3127
	public class PivotGridFieldReorderEventArgs : PivotGridCommandEventArgs
	{
		// Token: 0x0600766F RID: 30319 RVA: 0x001B7E61 File Offset: 0x001B6061
		public PivotGridFieldReorderEventArgs(PivotGridItem item, object commandSource, object argument) : base(item, commandSource, "FieldReorder", argument)
		{
		}

		// Token: 0x17002680 RID: 9856
		// (get) Token: 0x06007670 RID: 30320 RVA: 0x001B7E71 File Offset: 0x001B6071
		protected virtual RadPivotGrid OwnerPivotGrid
		{
			get
			{
				return this.Item.OwnerPivotGrid;
			}
		}

		// Token: 0x06007671 RID: 30321 RVA: 0x001B7E80 File Offset: 0x001B6080
		public override void ExecuteCommand(object source)
		{
			this.OwnerPivotGrid.FireFieldReorder(this);
			if (this.Canceled)
			{
				return;
			}
			string[] array = base.CommandArgument.ToString().Split(new char[]
			{
				';'
			});
			string fieldUniqueName = array[0];
			string s = array[1];
			int num;
			if (!int.TryParse(s, out num))
			{
				return;
			}
			string s2 = array[2];
			int zoneIndex;
			if (!int.TryParse(s2, out zoneIndex))
			{
				return;
			}
			PivotGridFieldZoneType zoneType = (PivotGridFieldZoneType)num;
			if (this.OwnerPivotGrid.TryReorderField(fieldUniqueName, zoneType, zoneIndex))
			{
				PivotGridRebindReason rebindReason = PivotGridRebindReason.PostBackEvent;
				this.OwnerPivotGrid.ResetPivotModel();
				this.OwnerPivotGrid.ObtainDataSource(rebindReason, false);
				this.OwnerPivotGrid.DataBind();
			}
		}
	}
}
