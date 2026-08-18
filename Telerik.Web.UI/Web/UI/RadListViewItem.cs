using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02001996 RID: 6550
	public class RadListViewItem : Control, INamingContainer
	{
		// Token: 0x0600FD8A RID: 64906 RVA: 0x0038F798 File Offset: 0x0038D998
		public RadListViewItem(RadListViewItemType itemType, RadListView ownerView)
		{
			this.ItemType = itemType;
			this.OwnerListView = ownerView;
		}

		// Token: 0x17004C87 RID: 19591
		// (get) Token: 0x0600FD8B RID: 64907 RVA: 0x0038F7AE File Offset: 0x0038D9AE
		// (set) Token: 0x0600FD8C RID: 64908 RVA: 0x0038F7B6 File Offset: 0x0038D9B6
		public RadListViewItemType ItemType { get; protected set; }

		// Token: 0x0600FD8D RID: 64909 RVA: 0x0038F7C0 File Offset: 0x0038D9C0
		protected override bool OnBubbleEvent(object source, EventArgs args)
		{
			CommandEventArgs commandEventArgs = args as CommandEventArgs;
			if (commandEventArgs != null && !(args is RadListViewCommandEventArgs))
			{
				RadListViewCommandEventArgs args2 = RadListViewCommandEventArgsFactory.CreateCommandEventArgs(this, source, commandEventArgs);
				base.RaiseBubbleEvent(this, args2);
				return true;
			}
			return base.OnBubbleEvent(source, args);
		}

		// Token: 0x17004C88 RID: 19592
		// (get) Token: 0x0600FD8E RID: 64910 RVA: 0x0038F7FA File Offset: 0x0038D9FA
		// (set) Token: 0x0600FD8F RID: 64911 RVA: 0x0038F802 File Offset: 0x0038DA02
		public RadListView OwnerListView { get; protected set; }

		// Token: 0x0600FD90 RID: 64912 RVA: 0x0038F80C File Offset: 0x0038DA0C
		public void FireCommandEvent(string commandName, object commandArgument)
		{
			CommandEventArgs args = new CommandEventArgs(commandName, commandArgument);
			this.OnBubbleEvent(this, args);
		}

		// Token: 0x17004C89 RID: 19593
		// (get) Token: 0x0600FD91 RID: 64913 RVA: 0x0038F82A File Offset: 0x0038DA2A
		public virtual bool IsInEditMode
		{
			get
			{
				return false;
			}
		}
	}
}
