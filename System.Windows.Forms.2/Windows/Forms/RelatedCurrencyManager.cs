using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace System.Windows.Forms
{
	// Token: 0x02000340 RID: 832
	internal class RelatedCurrencyManager : CurrencyManager
	{
		// Token: 0x060035CB RID: 13771 RVA: 0x000F3464 File Offset: 0x000F1664
		internal RelatedCurrencyManager(BindingManagerBase parentManager, string dataField) : base(null)
		{
			this.Bind(parentManager, dataField);
		}

		// Token: 0x060035CC RID: 13772 RVA: 0x000F3478 File Offset: 0x000F1678
		internal void Bind(BindingManagerBase parentManager, string dataField)
		{
			this.UnwireParentManager(this.parentManager);
			this.parentManager = parentManager;
			this.dataField = dataField;
			this.fieldInfo = parentManager.GetItemProperties().Find(dataField, true);
			if (this.fieldInfo == null || !typeof(IList).IsAssignableFrom(this.fieldInfo.PropertyType))
			{
				throw new ArgumentException(SR.GetString("RelatedListManagerChild", new object[]
				{
					dataField
				}));
			}
			this.finalType = this.fieldInfo.PropertyType;
			this.WireParentManager(this.parentManager);
			this.ParentManager_CurrentItemChanged(parentManager, EventArgs.Empty);
		}

		// Token: 0x060035CD RID: 13773 RVA: 0x000F3519 File Offset: 0x000F1719
		private void UnwireParentManager(BindingManagerBase bmb)
		{
			if (bmb != null)
			{
				bmb.CurrentItemChanged -= this.ParentManager_CurrentItemChanged;
				if (bmb is CurrencyManager)
				{
					(bmb as CurrencyManager).MetaDataChanged -= this.ParentManager_MetaDataChanged;
				}
			}
		}

		// Token: 0x060035CE RID: 13774 RVA: 0x000F354F File Offset: 0x000F174F
		private void WireParentManager(BindingManagerBase bmb)
		{
			if (bmb != null)
			{
				bmb.CurrentItemChanged += this.ParentManager_CurrentItemChanged;
				if (bmb is CurrencyManager)
				{
					(bmb as CurrencyManager).MetaDataChanged += this.ParentManager_MetaDataChanged;
				}
			}
		}

		// Token: 0x060035CF RID: 13775 RVA: 0x000F3588 File Offset: 0x000F1788
		internal override PropertyDescriptorCollection GetItemProperties(PropertyDescriptor[] listAccessors)
		{
			PropertyDescriptor[] array;
			if (listAccessors != null && listAccessors.Length != 0)
			{
				array = new PropertyDescriptor[listAccessors.Length + 1];
				listAccessors.CopyTo(array, 1);
			}
			else
			{
				array = new PropertyDescriptor[1];
			}
			array[0] = this.fieldInfo;
			return this.parentManager.GetItemProperties(array);
		}

		// Token: 0x060035D0 RID: 13776 RVA: 0x0001FDAB File Offset: 0x0001DFAB
		public override PropertyDescriptorCollection GetItemProperties()
		{
			return this.GetItemProperties(null);
		}

		// Token: 0x060035D1 RID: 13777 RVA: 0x000F35D0 File Offset: 0x000F17D0
		internal override string GetListName()
		{
			string listName = this.GetListName(new ArrayList());
			if (listName.Length > 0)
			{
				return listName;
			}
			return base.GetListName();
		}

		// Token: 0x060035D2 RID: 13778 RVA: 0x000F35FA File Offset: 0x000F17FA
		protected internal override string GetListName(ArrayList listAccessors)
		{
			listAccessors.Insert(0, this.fieldInfo);
			return this.parentManager.GetListName(listAccessors);
		}

		// Token: 0x060035D3 RID: 13779 RVA: 0x000F3615 File Offset: 0x000F1815
		private void ParentManager_MetaDataChanged(object sender, EventArgs e)
		{
			base.OnMetaDataChanged(e);
		}

		// Token: 0x060035D4 RID: 13780 RVA: 0x000F3620 File Offset: 0x000F1820
		private void ParentManager_CurrentItemChanged(object sender, EventArgs e)
		{
			if (RelatedCurrencyManager.IgnoreItemChangedTable.Contains(this.parentManager))
			{
				return;
			}
			int listposition = this.listposition;
			try
			{
				base.PullData();
			}
			catch (Exception e2)
			{
				base.OnDataError(e2);
			}
			if (this.parentManager is CurrencyManager)
			{
				CurrencyManager currencyManager = (CurrencyManager)this.parentManager;
				if (currencyManager.Count > 0)
				{
					this.SetDataSource(this.fieldInfo.GetValue(currencyManager.Current));
					this.listposition = ((this.Count > 0) ? 0 : -1);
					goto IL_DC;
				}
				currencyManager.AddNew();
				try
				{
					RelatedCurrencyManager.IgnoreItemChangedTable.Add(currencyManager);
					currencyManager.CancelCurrentEdit();
					goto IL_DC;
				}
				finally
				{
					if (RelatedCurrencyManager.IgnoreItemChangedTable.Contains(currencyManager))
					{
						RelatedCurrencyManager.IgnoreItemChangedTable.Remove(currencyManager);
					}
				}
			}
			this.SetDataSource(this.fieldInfo.GetValue(this.parentManager.Current));
			this.listposition = ((this.Count > 0) ? 0 : -1);
			IL_DC:
			if (listposition != this.listposition)
			{
				this.OnPositionChanged(EventArgs.Empty);
			}
			this.OnCurrentChanged(EventArgs.Empty);
			this.OnCurrentItemChanged(EventArgs.Empty);
		}

		// Token: 0x04001F6D RID: 8045
		private BindingManagerBase parentManager;

		// Token: 0x04001F6E RID: 8046
		private string dataField;

		// Token: 0x04001F6F RID: 8047
		private PropertyDescriptor fieldInfo;

		// Token: 0x04001F70 RID: 8048
		private static List<BindingManagerBase> IgnoreItemChangedTable = new List<BindingManagerBase>();
	}
}
