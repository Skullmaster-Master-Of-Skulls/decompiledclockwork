using System;

namespace Telerik.Web.UI
{
	// Token: 0x02000C3D RID: 3133
	public class PivotGridShowHideFieldEventArgs : PivotGridCommandEventArgs
	{
		// Token: 0x06007692 RID: 30354 RVA: 0x001B8628 File Offset: 0x001B6828
		public PivotGridShowHideFieldEventArgs(PivotGridItem item, object commandSource, object argument) : base(item, commandSource, "ShowHideField", argument)
		{
		}

		// Token: 0x1700268D RID: 9869
		// (get) Token: 0x06007693 RID: 30355 RVA: 0x001B8638 File Offset: 0x001B6838
		protected virtual RadPivotGrid OwnerPivotGrid
		{
			get
			{
				return this.Item.OwnerPivotGrid;
			}
		}

		// Token: 0x06007694 RID: 30356 RVA: 0x001B8648 File Offset: 0x001B6848
		public override void ExecuteCommand(object source)
		{
			this.OwnerPivotGrid.FireShowHideField(this);
			if (this.Canceled)
			{
				return;
			}
			string text = base.CommandArgument.ToString();
			string text2 = text.Substring(0, text.LastIndexOf(' '));
			PivotGridField fieldByUniqueName = this.OwnerPivotGrid.Fields.GetFieldByUniqueName(text2);
			if (fieldByUniqueName == null)
			{
				this.OwnerPivotGrid.PromissedFieldsForCreation.Add(text2);
			}
			else if (text.ToLower().Contains("hide"))
			{
				fieldByUniqueName.IsHidden = true;
			}
			else
			{
				fieldByUniqueName.Show();
			}
			PivotGridRebindReason rebindReason = PivotGridRebindReason.PostBackEvent;
			this.OwnerPivotGrid.ResetPivotModel();
			this.OwnerPivotGrid.ObtainDataSource(rebindReason, false);
			this.OwnerPivotGrid.DataBind();
		}
	}
}
