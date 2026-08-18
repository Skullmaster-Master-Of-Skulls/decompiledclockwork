using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x020001F3 RID: 499
	public class RadDataFormDataItem : RadDataFormItem, IDataItemContainer, INamingContainer
	{
		// Token: 0x06001196 RID: 4502 RVA: 0x000401E1 File Offset: 0x0003E3E1
		public RadDataFormDataItem(RadDataForm ownerDataForm, int displayIndex) : this(ownerDataForm, displayIndex, RadDataFormItemType.DataItem)
		{
		}

		// Token: 0x06001197 RID: 4503 RVA: 0x000401EC File Offset: 0x0003E3EC
		public RadDataFormDataItem(RadDataForm ownerDataForm, int displayIndex, RadDataFormItemType itemType) : base(itemType, ownerDataForm)
		{
			this.DisplayIndex = displayIndex;
		}

		// Token: 0x170005E0 RID: 1504
		// (get) Token: 0x06001198 RID: 4504 RVA: 0x000401FD File Offset: 0x0003E3FD
		// (set) Token: 0x06001199 RID: 4505 RVA: 0x00040205 File Offset: 0x0003E405
		public virtual object DataItem { get; set; }

		// Token: 0x170005E1 RID: 1505
		// (get) Token: 0x0600119A RID: 4506 RVA: 0x0004020E File Offset: 0x0003E40E
		// (set) Token: 0x0600119B RID: 4507 RVA: 0x00040216 File Offset: 0x0003E416
		public int DataItemIndex { get; internal set; }

		// Token: 0x170005E2 RID: 1506
		// (get) Token: 0x0600119C RID: 4508 RVA: 0x0004021F File Offset: 0x0003E41F
		// (set) Token: 0x0600119D RID: 4509 RVA: 0x00040227 File Offset: 0x0003E427
		public int DisplayIndex { get; protected set; }

		// Token: 0x170005E3 RID: 1507
		// (get) Token: 0x0600119E RID: 4510 RVA: 0x00040230 File Offset: 0x0003E430
		// (set) Token: 0x0600119F RID: 4511 RVA: 0x00040245 File Offset: 0x0003E445
		public bool Edit
		{
			get
			{
				return base.OwnerDataForm.EditIndex == this.DisplayIndex;
			}
			set
			{
				if (value)
				{
					base.OwnerDataForm.EditIndex = this.DisplayIndex;
					return;
				}
				if (base.OwnerDataForm.EditIndex == this.DisplayIndex)
				{
					base.OwnerDataForm.EditIndex = -1;
				}
			}
		}

		// Token: 0x170005E4 RID: 1508
		// (get) Token: 0x060011A0 RID: 4512 RVA: 0x0004027C File Offset: 0x0003E47C
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

		// Token: 0x060011A1 RID: 4513 RVA: 0x000402BA File Offset: 0x0003E4BA
		public object GetDataKeyValue(string keyName)
		{
			return base.OwnerDataForm.DataKeyValues[this.DisplayIndex][keyName];
		}

		// Token: 0x060011A2 RID: 4514 RVA: 0x000402D8 File Offset: 0x0003E4D8
		public virtual void ExtractValues(IDictionary newValues)
		{
			if (newValues == null)
			{
				throw new ArgumentNullException("newValues");
			}
			base.OwnerDataForm.ExtractValuesFromItem(newValues, this, true);
		}

		// Token: 0x060011A3 RID: 4515 RVA: 0x000402F8 File Offset: 0x0003E4F8
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
