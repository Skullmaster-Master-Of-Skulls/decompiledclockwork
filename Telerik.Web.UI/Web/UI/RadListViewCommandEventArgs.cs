using System;
using System.Diagnostics.CodeAnalysis;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02001943 RID: 6467
	public class RadListViewCommandEventArgs : CommandEventArgs, IRadListViewCommandEvent
	{
		// Token: 0x17004BA8 RID: 19368
		// (get) Token: 0x0600FA68 RID: 64104 RVA: 0x0038643C File Offset: 0x0038463C
		// (set) Token: 0x0600FA69 RID: 64105 RVA: 0x00386444 File Offset: 0x00384644
		public virtual RadListViewItem ListViewItem { get; set; }

		// Token: 0x17004BA9 RID: 19369
		// (get) Token: 0x0600FA6A RID: 64106 RVA: 0x0038644D File Offset: 0x0038464D
		// (set) Token: 0x0600FA6B RID: 64107 RVA: 0x00386455 File Offset: 0x00384655
		public object EventSource { get; set; }

		// Token: 0x0600FA6C RID: 64108 RVA: 0x0038645E File Offset: 0x0038465E
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public RadListViewCommandEventArgs(RadListViewItem listViewItem, object eventSource, CommandEventArgs args) : base(args)
		{
			this.ListViewItem = listViewItem;
			this.EventSource = eventSource;
		}

		// Token: 0x0600FA6D RID: 64109 RVA: 0x00386475 File Offset: 0x00384675
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		internal RadListViewCommandEventArgs(RadListViewItem listViewItem, object eventSource, string name, object argument) : base(name, argument)
		{
			this.ListViewItem = listViewItem;
			this.EventSource = eventSource;
		}

		// Token: 0x17004BAA RID: 19370
		// (get) Token: 0x0600FA6E RID: 64110 RVA: 0x0038648E File Offset: 0x0038468E
		// (set) Token: 0x0600FA6F RID: 64111 RVA: 0x00386496 File Offset: 0x00384696
		public virtual bool Canceled { get; set; }

		// Token: 0x0600FA70 RID: 64112 RVA: 0x003864A0 File Offset: 0x003846A0
		public virtual void ExecuteCommand(object source)
		{
			string commandName = base.CommandName;
			RadListView ownerListView = this.ListViewItem.OwnerListView;
			if (string.Compare(commandName, "Edit", true) == 0 && this.ListViewItem is RadListViewDataItem)
			{
				((RadListViewDataItem)this.ListViewItem).Edit = true;
				ownerListView.FireItemEditing(this);
				if (this.Canceled)
				{
					return;
				}
				ownerListView.Rebind();
				return;
			}
			else
			{
				if (string.Compare(commandName, "RebindListView", true) == 0)
				{
					ownerListView.Rebind();
				}
				if (string.Compare(commandName, "Update", true) == 0)
				{
					ownerListView.FireItemUpdating(this);
					if (this.Canceled)
					{
						return;
					}
					if (ownerListView.IsUsingModelBinding || ownerListView.IsBoundUsingDataSourceIDInternal)
					{
						ownerListView.PerformUpdate((RadListViewDataItem)this.ListViewItem, true);
						if (!ownerListView.IsModelValid)
						{
							ownerListView.IsModelValid = true;
							return;
						}
					}
					else
					{
						RadListViewDataItem radListViewDataItem = this.ListViewItem as RadListViewDataItem;
						if (radListViewDataItem != null)
						{
							radListViewDataItem.Edit = false;
						}
					}
					ownerListView.Rebind();
					return;
				}
				else if (string.Compare(commandName, "Cancel", true) == 0)
				{
					ownerListView.FireItemCanceling(this);
					if (this.Canceled)
					{
						return;
					}
					RadListViewDataItem radListViewDataItem2 = this.ListViewItem as RadListViewDataItem;
					if (radListViewDataItem2 != null)
					{
						radListViewDataItem2.Edit = false;
					}
					if (radListViewDataItem2 is IRadListViewInsertItem)
					{
						ownerListView.IsItemInserted = false;
					}
					ownerListView.Rebind();
					return;
				}
				else if (string.Compare(commandName, "Delete", true) == 0)
				{
					ownerListView.FireItemDeleting(this);
					if (this.Canceled)
					{
						return;
					}
					if (ownerListView.IsUsingModelBinding || ownerListView.IsBoundUsingDataSourceIDInternal)
					{
						ownerListView.PerformDelete((RadListViewDataItem)this.ListViewItem, true);
					}
					ownerListView.Rebind();
					return;
				}
				else
				{
					if (string.Compare(commandName, "InitInsert", true) != 0)
					{
						if (string.Compare(commandName, "PerformInsert", true) == 0)
						{
							ownerListView.FireItemInserting(this);
							if (this.Canceled)
							{
								return;
							}
							if (ownerListView.IsUsingModelBinding || ownerListView.IsBoundUsingDataSourceIDInternal)
							{
								ownerListView.PerformInsert();
								return;
							}
							((RadListViewDataItem)this.ListViewItem).Edit = false;
							((RadListViewDataItem)this.ListViewItem).OwnerListView.IsItemInserted = false;
							ownerListView.Rebind();
						}
						return;
					}
					if (this.Canceled)
					{
						return;
					}
					ownerListView.ShowInsertItem();
					return;
				}
			}
		}
	}
}
