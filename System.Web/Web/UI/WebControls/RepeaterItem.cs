using System;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	// Token: 0x0200062E RID: 1582
	[ToolboxItem(false)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class RepeaterItem : Control, IDataItemContainer, INamingContainer
	{
		// Token: 0x06004E63 RID: 20067 RVA: 0x0013D426 File Offset: 0x0013C426
		public RepeaterItem(int itemIndex, ListItemType itemType)
		{
			this.itemIndex = itemIndex;
			this.itemType = itemType;
		}

		// Token: 0x170013CC RID: 5068
		// (get) Token: 0x06004E64 RID: 20068 RVA: 0x0013D43C File Offset: 0x0013C43C
		// (set) Token: 0x06004E65 RID: 20069 RVA: 0x0013D444 File Offset: 0x0013C444
		public virtual object DataItem
		{
			get
			{
				return this.dataItem;
			}
			set
			{
				this.dataItem = value;
			}
		}

		// Token: 0x170013CD RID: 5069
		// (get) Token: 0x06004E66 RID: 20070 RVA: 0x0013D44D File Offset: 0x0013C44D
		public virtual int ItemIndex
		{
			get
			{
				return this.itemIndex;
			}
		}

		// Token: 0x170013CE RID: 5070
		// (get) Token: 0x06004E67 RID: 20071 RVA: 0x0013D455 File Offset: 0x0013C455
		public virtual ListItemType ItemType
		{
			get
			{
				return this.itemType;
			}
		}

		// Token: 0x06004E68 RID: 20072 RVA: 0x0013D460 File Offset: 0x0013C460
		protected override bool OnBubbleEvent(object source, EventArgs e)
		{
			if (e is CommandEventArgs)
			{
				RepeaterCommandEventArgs args = new RepeaterCommandEventArgs(this, source, (CommandEventArgs)e);
				base.RaiseBubbleEvent(this, args);
				return true;
			}
			return false;
		}

		// Token: 0x170013CF RID: 5071
		// (get) Token: 0x06004E69 RID: 20073 RVA: 0x0013D48E File Offset: 0x0013C48E
		int IDataItemContainer.DataItemIndex
		{
			get
			{
				return this.ItemIndex;
			}
		}

		// Token: 0x170013D0 RID: 5072
		// (get) Token: 0x06004E6A RID: 20074 RVA: 0x0013D496 File Offset: 0x0013C496
		int IDataItemContainer.DisplayIndex
		{
			get
			{
				return this.ItemIndex;
			}
		}

		// Token: 0x04002C95 RID: 11413
		private int itemIndex;

		// Token: 0x04002C96 RID: 11414
		private ListItemType itemType;

		// Token: 0x04002C97 RID: 11415
		private object dataItem;
	}
}
