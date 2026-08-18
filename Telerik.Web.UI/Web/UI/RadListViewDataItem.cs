using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02001997 RID: 6551
	public class RadListViewDataItem : RadListViewItem, IDataItemContainer, INamingContainer
	{
		// Token: 0x0600FD92 RID: 64914 RVA: 0x0038F82D File Offset: 0x0038DA2D
		public RadListViewDataItem(RadListView ownerListView, int displayIndex) : this(ownerListView, displayIndex, RadListViewItemType.DataItem)
		{
		}

		// Token: 0x0600FD93 RID: 64915 RVA: 0x0038F838 File Offset: 0x0038DA38
		public RadListViewDataItem(RadListView ownerListView, int displayIndex, RadListViewItemType itemType) : base(itemType, ownerListView)
		{
			this.DisplayIndex = displayIndex;
		}

		// Token: 0x17004C8A RID: 19594
		// (get) Token: 0x0600FD94 RID: 64916 RVA: 0x0038F849 File Offset: 0x0038DA49
		// (set) Token: 0x0600FD95 RID: 64917 RVA: 0x0038F851 File Offset: 0x0038DA51
		public virtual object DataItem { get; set; }

		// Token: 0x17004C8B RID: 19595
		// (get) Token: 0x0600FD96 RID: 64918 RVA: 0x0038F85A File Offset: 0x0038DA5A
		// (set) Token: 0x0600FD97 RID: 64919 RVA: 0x0038F862 File Offset: 0x0038DA62
		public int DataItemIndex { get; internal set; }

		// Token: 0x17004C8C RID: 19596
		// (get) Token: 0x0600FD98 RID: 64920 RVA: 0x0038F86B File Offset: 0x0038DA6B
		// (set) Token: 0x0600FD99 RID: 64921 RVA: 0x0038F873 File Offset: 0x0038DA73
		public int DisplayIndex { get; protected set; }

		// Token: 0x17004C8D RID: 19597
		// (get) Token: 0x0600FD9A RID: 64922 RVA: 0x0038F87C File Offset: 0x0038DA7C
		// (set) Token: 0x0600FD9B RID: 64923 RVA: 0x0038F894 File Offset: 0x0038DA94
		public bool Selected
		{
			get
			{
				return base.OwnerListView.SelectedIndexes.Contains(this.DisplayIndex);
			}
			set
			{
				if (value)
				{
					base.OwnerListView.AddSelectedIndex(this.DisplayIndex);
					return;
				}
				base.OwnerListView.RemoveSelectedIndex(this.DisplayIndex);
			}
		}

		// Token: 0x17004C8E RID: 19598
		// (get) Token: 0x0600FD9C RID: 64924 RVA: 0x0038F8BC File Offset: 0x0038DABC
		// (set) Token: 0x0600FD9D RID: 64925 RVA: 0x0038F8D4 File Offset: 0x0038DAD4
		public bool Edit
		{
			get
			{
				return base.OwnerListView.EditIndexes.Contains(this.DisplayIndex);
			}
			set
			{
				if (value)
				{
					base.OwnerListView.AddEditIndex(this.DisplayIndex);
					return;
				}
				base.OwnerListView.RemoveEditIndex(this.DisplayIndex);
			}
		}

		// Token: 0x17004C8F RID: 19599
		// (get) Token: 0x0600FD9E RID: 64926 RVA: 0x0038F8FC File Offset: 0x0038DAFC
		public IDictionary SavedOldValues
		{
			get
			{
				object obj = this.ViewState["SavedOldValues"];
				if (obj == null)
				{
					obj = new Hashtable();
					this.ViewState["SavedOldValues"] = obj;
				}
				return (IDictionary)obj;
			}
		}

		// Token: 0x0600FD9F RID: 64927 RVA: 0x0038F93A File Offset: 0x0038DB3A
		public object GetDataKeyValue(string keyName)
		{
			return base.OwnerListView.DataKeyValues[this.DisplayIndex][keyName];
		}

		// Token: 0x0600FDA0 RID: 64928 RVA: 0x0038F958 File Offset: 0x0038DB58
		public virtual void ExtractValues(IDictionary newValues)
		{
			if (newValues == null)
			{
				throw new ArgumentNullException("newValues");
			}
			base.OwnerListView.ExtractValuesFromItem(newValues, this, true);
		}

		// Token: 0x0600FDA1 RID: 64929 RVA: 0x0038F978 File Offset: 0x0038DB78
		public virtual void UpdateValues(object objectToUpdate)
		{
			if (objectToUpdate == null)
			{
				throw new ArgumentNullException("objectToUpdate");
			}
			Hashtable hashtable = new Hashtable();
			this.ExtractValues(hashtable);
			if (hashtable.Count == 0)
			{
				return;
			}
			DataRow dataRow = objectToUpdate as DataRow;
			if (dataRow != null)
			{
				using (IDictionaryEnumerator enumerator = hashtable.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
						dataRow[dictionaryEntry.Key.ToString()] = dictionaryEntry.Value;
					}
					return;
				}
			}
			DataRowView dataRowView = objectToUpdate as DataRowView;
			if (dataRowView != null)
			{
				using (IDictionaryEnumerator enumerator2 = hashtable.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						object obj2 = enumerator2.Current;
						DictionaryEntry dictionaryEntry2 = (DictionaryEntry)obj2;
						dataRowView[dictionaryEntry2.Key.ToString()] = dictionaryEntry2.Value;
					}
					return;
				}
			}
			PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(objectToUpdate);
			foreach (object obj3 in hashtable)
			{
				DictionaryEntry dictionaryEntry3 = (DictionaryEntry)obj3;
				PropertyDescriptor propertyDescriptor = properties[dictionaryEntry3.Key.ToString()];
				if (propertyDescriptor != null)
				{
					propertyDescriptor.SetValue(objectToUpdate, propertyDescriptor.Converter.ConvertFromString((dictionaryEntry3.Value != null) ? dictionaryEntry3.Value.ToString() : string.Empty));
				}
			}
		}
	}
}
