using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x020001F2 RID: 498
	public class RadDataFormItem : Control, INamingContainer
	{
		// Token: 0x0600118E RID: 4494 RVA: 0x0004014C File Offset: 0x0003E34C
		public RadDataFormItem(RadDataFormItemType itemType, RadDataForm ownerView)
		{
			this.ItemType = itemType;
			this.OwnerDataForm = ownerView;
		}

		// Token: 0x170005DD RID: 1501
		// (get) Token: 0x0600118F RID: 4495 RVA: 0x00040162 File Offset: 0x0003E362
		// (set) Token: 0x06001190 RID: 4496 RVA: 0x0004016A File Offset: 0x0003E36A
		public RadDataFormItemType ItemType { get; protected set; }

		// Token: 0x06001191 RID: 4497 RVA: 0x00040174 File Offset: 0x0003E374
		protected override bool OnBubbleEvent(object source, EventArgs args)
		{
			CommandEventArgs commandEventArgs = args as CommandEventArgs;
			if (commandEventArgs != null && !(args is RadDataFormCommandEventArgs))
			{
				RadDataFormCommandEventArgs args2 = RadDataFormCommandEventArgsFactory.CreateCommandEventArgs(this, source, commandEventArgs);
				base.RaiseBubbleEvent(this, args2);
				return true;
			}
			return base.OnBubbleEvent(source, args);
		}

		// Token: 0x170005DE RID: 1502
		// (get) Token: 0x06001192 RID: 4498 RVA: 0x000401AE File Offset: 0x0003E3AE
		// (set) Token: 0x06001193 RID: 4499 RVA: 0x000401B6 File Offset: 0x0003E3B6
		public RadDataForm OwnerDataForm { get; protected set; }

		// Token: 0x06001194 RID: 4500 RVA: 0x000401C0 File Offset: 0x0003E3C0
		public void FireCommandEvent(string commandName, object commandArgument)
		{
			CommandEventArgs args = new CommandEventArgs(commandName, commandArgument);
			this.OnBubbleEvent(this, args);
		}

		// Token: 0x170005DF RID: 1503
		// (get) Token: 0x06001195 RID: 4501 RVA: 0x000401DE File Offset: 0x0003E3DE
		public virtual bool IsInEditMode
		{
			get
			{
				return false;
			}
		}
	}
}
