using System;
using System.Diagnostics.CodeAnalysis;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x020001DA RID: 474
	public class RadDataFormCommandEventArgs : CommandEventArgs, IRadDataFormCommandEvent
	{
		// Token: 0x170005AE RID: 1454
		// (get) Token: 0x060010F1 RID: 4337 RVA: 0x0003E590 File Offset: 0x0003C790
		// (set) Token: 0x060010F2 RID: 4338 RVA: 0x0003E598 File Offset: 0x0003C798
		public virtual RadDataFormItem DataFormItem { get; set; }

		// Token: 0x170005AF RID: 1455
		// (get) Token: 0x060010F3 RID: 4339 RVA: 0x0003E5A1 File Offset: 0x0003C7A1
		// (set) Token: 0x060010F4 RID: 4340 RVA: 0x0003E5A9 File Offset: 0x0003C7A9
		public object EventSource { get; set; }

		// Token: 0x060010F5 RID: 4341 RVA: 0x0003E5B2 File Offset: 0x0003C7B2
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public RadDataFormCommandEventArgs(RadDataFormItem dataFormItem, object eventSource, CommandEventArgs args) : base(args)
		{
			this.DataFormItem = dataFormItem;
			this.EventSource = eventSource;
		}

		// Token: 0x060010F6 RID: 4342 RVA: 0x0003E5C9 File Offset: 0x0003C7C9
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		internal RadDataFormCommandEventArgs(RadDataFormItem dataFormItem, object eventSource, string name, object argument) : base(name, argument)
		{
			this.DataFormItem = dataFormItem;
			this.EventSource = eventSource;
		}

		// Token: 0x170005B0 RID: 1456
		// (get) Token: 0x060010F7 RID: 4343 RVA: 0x0003E5E2 File Offset: 0x0003C7E2
		// (set) Token: 0x060010F8 RID: 4344 RVA: 0x0003E5EA File Offset: 0x0003C7EA
		public virtual bool Canceled { get; set; }

		// Token: 0x060010F9 RID: 4345 RVA: 0x0003E5F4 File Offset: 0x0003C7F4
		public virtual void ExecuteCommand(object source)
		{
			string commandName = base.CommandName;
			RadDataForm ownerDataForm = this.DataFormItem.OwnerDataForm;
			if (string.Compare(commandName, "Edit", true) == 0 && this.DataFormItem is RadDataFormDataItem)
			{
				((RadDataFormDataItem)this.DataFormItem).Edit = true;
				ownerDataForm.FireItemEditing(this);
				if (this.Canceled)
				{
					return;
				}
				ownerDataForm.Rebind();
				return;
			}
			else
			{
				if (string.Compare(commandName, "RebindDataForm", true) == 0)
				{
					ownerDataForm.Rebind();
				}
				if (string.Compare(commandName, "Update", true) == 0)
				{
					ownerDataForm.FireItemUpdating(this);
					if (this.Canceled)
					{
						return;
					}
					if (ownerDataForm.IsUsingModelBinding || ownerDataForm.IsBoundUsingDataSourceIDInternal)
					{
						ownerDataForm.PerformUpdate((RadDataFormDataItem)this.DataFormItem, true);
						if (!ownerDataForm.IsModelValid)
						{
							ownerDataForm.IsModelValid = true;
							return;
						}
					}
					else
					{
						RadDataFormDataItem radDataFormDataItem = this.DataFormItem as RadDataFormDataItem;
						if (radDataFormDataItem != null)
						{
							radDataFormDataItem.Edit = false;
						}
					}
					ownerDataForm.Rebind();
					return;
				}
				else if (string.Compare(commandName, "Cancel", true) == 0)
				{
					ownerDataForm.FireItemCanceling(this);
					if (this.Canceled)
					{
						return;
					}
					RadDataFormDataItem radDataFormDataItem2 = this.DataFormItem as RadDataFormDataItem;
					if (radDataFormDataItem2 != null)
					{
						radDataFormDataItem2.Edit = false;
					}
					if (radDataFormDataItem2 is IRadDataFormInsertItem)
					{
						ownerDataForm.IsItemInserted = false;
					}
					ownerDataForm.Rebind();
					return;
				}
				else if (string.Compare(commandName, "Delete", true) == 0)
				{
					ownerDataForm.FireItemDeleting(this);
					if (this.Canceled)
					{
						return;
					}
					if (ownerDataForm.IsUsingModelBinding || ownerDataForm.IsBoundUsingDataSourceIDInternal)
					{
						ownerDataForm.PerformDelete((RadDataFormDataItem)this.DataFormItem, true);
					}
					ownerDataForm.CurrentPageIndex = 0;
					ownerDataForm.Rebind();
					return;
				}
				else
				{
					if (string.Compare(commandName, "InitInsert", true) != 0)
					{
						if (string.Compare(commandName, "PerformInsert", true) == 0)
						{
							ownerDataForm.FireItemInserting(this);
							if (this.Canceled)
							{
								return;
							}
							if (ownerDataForm.IsUsingModelBinding || ownerDataForm.IsBoundUsingDataSourceIDInternal)
							{
								ownerDataForm.PerformInsert();
								return;
							}
							((RadDataFormDataItem)this.DataFormItem).Edit = false;
							((RadDataFormDataItem)this.DataFormItem).OwnerDataForm.IsItemInserted = false;
							ownerDataForm.Rebind();
						}
						return;
					}
					if (this.Canceled)
					{
						return;
					}
					ownerDataForm.ShowInsertItem();
					return;
				}
			}
		}
	}
}
