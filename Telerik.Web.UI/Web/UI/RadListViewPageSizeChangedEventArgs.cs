using System;

namespace Telerik.Web.UI
{
	// Token: 0x0200194C RID: 6476
	public class RadListViewPageSizeChangedEventArgs : RadListViewCommandEventArgs
	{
		// Token: 0x0600FA95 RID: 64149 RVA: 0x00386AA0 File Offset: 0x00384CA0
		public RadListViewPageSizeChangedEventArgs(RadListViewItem item, object commandSource, object argument) : base(item, commandSource, "ChangePageSize", argument)
		{
			int newPageSize;
			if (argument != null && int.TryParse(argument.ToString(), out newPageSize))
			{
				this.NewPageSize = newPageSize;
			}
		}

		// Token: 0x17004BB7 RID: 19383
		// (get) Token: 0x0600FA96 RID: 64150 RVA: 0x00386AD4 File Offset: 0x00384CD4
		// (set) Token: 0x0600FA97 RID: 64151 RVA: 0x00386ADC File Offset: 0x00384CDC
		public int NewPageSize { get; set; }

		// Token: 0x0600FA98 RID: 64152 RVA: 0x00386AE8 File Offset: 0x00384CE8
		public override void ExecuteCommand(object source)
		{
			RadListView radListView = source as RadListView;
			if (radListView != null)
			{
				radListView.FirePageSizeChanged(this);
			}
		}
	}
}
