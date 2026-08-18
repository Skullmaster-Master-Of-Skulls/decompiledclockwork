using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x0200113A RID: 4410
	public abstract class GridEditableItem : GridItem
	{
		// Token: 0x0600B3AA RID: 45994 RVA: 0x0027345B File Offset: 0x0027165B
		public GridEditableItem(GridTableView ownerTableView, int itemIndex, int dataSetIndex) : base(ownerTableView, itemIndex, dataSetIndex, GridItemType.Item)
		{
		}

		// Token: 0x0600B3AB RID: 45995 RVA: 0x00273467 File Offset: 0x00271667
		public GridEditableItem(GridTableView ownerTableView, int itemIndex, int dataSetIndex, GridItemType itemType) : base(ownerTableView, itemIndex, dataSetIndex, itemType)
		{
		}

		// Token: 0x0600B3AC RID: 45996
		public abstract void InitializeEditorInCell(IGridEditableColumn column);

		// Token: 0x17003A09 RID: 14857
		// (get) Token: 0x0600B3AD RID: 45997 RVA: 0x00273474 File Offset: 0x00271674
		public virtual GridEditManager EditManager
		{
			get
			{
				return new GridEditManager(this);
			}
		}

		// Token: 0x17003A0A RID: 14858
		public abstract TableCell this[string columnUniqueName]
		{
			get;
		}

		// Token: 0x17003A0B RID: 14859
		public abstract TableCell this[GridColumn column]
		{
			get;
		}

		// Token: 0x17003A0C RID: 14860
		// (get) Token: 0x0600B3B0 RID: 46000
		public abstract IDictionary SavedOldValues { get; }

		// Token: 0x17003A0D RID: 14861
		// (get) Token: 0x0600B3B1 RID: 46001 RVA: 0x0027347C File Offset: 0x0027167C
		public virtual bool CanExtractValues
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600B3B2 RID: 46002 RVA: 0x00273480 File Offset: 0x00271680
		public virtual void ExtractValues(IDictionary newValues)
		{
			foreach (GridColumn gridColumn in base.OwnerTableView.RenderColumns)
			{
				IGridEditableColumn gridEditableColumn = gridColumn as IGridEditableColumn;
				if (gridEditableColumn != null && gridEditableColumn.ShouldExtractValues(this))
				{
					gridEditableColumn.FillValues(newValues, this);
				}
			}
		}

		// Token: 0x0600B3B3 RID: 46003 RVA: 0x002734C8 File Offset: 0x002716C8
		[SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
		public virtual void UpdateValues(object objectToUpdate)
		{
			Hashtable hashtable = new Hashtable();
			this.ExtractValues(hashtable);
			if (objectToUpdate is DataRow)
			{
				DataRow dataRow = (DataRow)objectToUpdate;
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
			if (objectToUpdate is DataRowView)
			{
				DataRowView dataRowView = (DataRowView)objectToUpdate;
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
				if (propertyDescriptor != null && objectToUpdate != null)
				{
					propertyDescriptor.SetValue(objectToUpdate, propertyDescriptor.Converter.ConvertFromString((dictionaryEntry3.Value != null) ? dictionaryEntry3.Value.ToString() : ""));
				}
			}
		}

		// Token: 0x0600B3B4 RID: 46004 RVA: 0x0027365C File Offset: 0x0027185C
		public virtual object GetDataKeyValue(string keyName)
		{
			return base.OwnerTableView.DataKeyValues[this.ItemIndex][keyName];
		}

		// Token: 0x17003A0E RID: 14862
		// (get) Token: 0x0600B3B5 RID: 46005 RVA: 0x0027367C File Offset: 0x0027187C
		public string KeyValues
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				for (int i = 0; i < base.OwnerTableView.DataKeyNames.Length; i++)
				{
					string text = base.OwnerTableView.DataKeyNames[i];
					string str = base.OwnerTableView.DataKeyValues[this.ItemIndex][text].ToString();
					stringBuilder.Append(text + ":\"" + str + "\",");
				}
				string text2 = stringBuilder.ToString();
				if (text2.Length > 0 && text2[text2.Length - 1] == ',')
				{
					text2 = text2.Remove(text2.Length - 1, 1);
				}
				return "{" + text2 + "}";
			}
		}
	}
}
