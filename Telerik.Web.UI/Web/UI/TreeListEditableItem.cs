using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x0200124B RID: 4683
	public abstract class TreeListEditableItem : TreeListItem
	{
		// Token: 0x0600C0FB RID: 49403 RVA: 0x002AFED3 File Offset: 0x002AE0D3
		public TreeListEditableItem(RadTreeList ownerTreeList, TreeListItemType itemType, bool isDataBinding) : base(ownerTreeList, itemType, isDataBinding)
		{
			this._columnEditors = new Dictionary<TreeListEditableColumn, ITreeListColumnEditor>();
		}

		// Token: 0x17003E34 RID: 15924
		// (get) Token: 0x0600C0FC RID: 49404 RVA: 0x002AFEE9 File Offset: 0x002AE0E9
		// (set) Token: 0x0600C0FD RID: 49405 RVA: 0x002AFEF1 File Offset: 0x002AE0F1
		public virtual object DataItem { get; set; }

		// Token: 0x17003E35 RID: 15925
		// (get) Token: 0x0600C0FE RID: 49406
		public abstract bool IsInEditMode { get; }

		// Token: 0x17003E36 RID: 15926
		// (get) Token: 0x0600C0FF RID: 49407
		// (set) Token: 0x0600C100 RID: 49408
		public abstract bool Edit { get; set; }

		// Token: 0x17003E37 RID: 15927
		// (get) Token: 0x0600C101 RID: 49409 RVA: 0x002AFEFC File Offset: 0x002AE0FC
		public virtual IDictionary SavedOldValues
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

		// Token: 0x17003E38 RID: 15928
		// (get) Token: 0x0600C102 RID: 49410 RVA: 0x002AFF3A File Offset: 0x002AE13A
		public virtual bool CanExtractValues
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600C103 RID: 49411 RVA: 0x002AFF40 File Offset: 0x002AE140
		public virtual void ExtractValues(IDictionary newValues)
		{
			foreach (TreeListColumn treeListColumn in base.OwnerTreeList.RenderColumns)
			{
				TreeListEditableColumn treeListEditableColumn = treeListColumn as TreeListEditableColumn;
				if (treeListEditableColumn != null && treeListEditableColumn.ShouldExtractValues(this))
				{
					treeListEditableColumn.FillValues(newValues, this);
				}
			}
			ITreeListInsertItem treeListInsertItem = this as ITreeListInsertItem;
			if (treeListInsertItem != null)
			{
				RadTreeList.ExtractParentDataKeyValues(newValues, treeListInsertItem);
			}
		}

		// Token: 0x0600C104 RID: 49412 RVA: 0x002AFF9C File Offset: 0x002AE19C
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

		// Token: 0x0600C105 RID: 49413 RVA: 0x002B0130 File Offset: 0x002AE330
		public ITreeListColumnEditor GetColumnEditor(string columnUniqueName)
		{
			return this.GetColumnEditor(base.OwnerTreeList.GetColumn(columnUniqueName) as TreeListEditableColumn);
		}

		// Token: 0x0600C106 RID: 49414 RVA: 0x002B014C File Offset: 0x002AE34C
		public virtual ITreeListColumnEditor GetColumnEditor(TreeListEditableColumn editableColumn)
		{
			if (!this.IsInEditMode || editableColumn == null || !editableColumn.IsEditable)
			{
				return null;
			}
			if (this._columnEditors.ContainsKey(editableColumn))
			{
				return this._columnEditors[editableColumn];
			}
			ITreeListColumnEditor treeListColumnEditor = editableColumn.CreateDefaultColumnEditor();
			ITreeListColumnEditor treeListColumnEditor2 = base.OwnerTreeList.GetCustomEditorInitializer(editableColumn, treeListColumnEditor)();
			ITreeListColumnEditor treeListColumnEditor3 = treeListColumnEditor2 ?? treeListColumnEditor;
			this._columnEditors[editableColumn] = treeListColumnEditor3;
			return treeListColumnEditor3;
		}

		// Token: 0x0600C107 RID: 49415 RVA: 0x002B01B8 File Offset: 0x002AE3B8
		public virtual void InitializeColumnEditor(TableCell cell, int columnIndex, TreeListEditableColumn column)
		{
			ITreeListColumnEditor columnEditor = this.GetColumnEditor(column);
			if (columnEditor != null)
			{
				columnEditor.Initialize(this, cell);
			}
		}

		// Token: 0x0600C108 RID: 49416 RVA: 0x002B01D8 File Offset: 0x002AE3D8
		protected override void OnCellDataBound(TreeListColumn column, TableCell cell)
		{
			if (this.IsInEditMode)
			{
				TreeListEditableColumn treeListEditableColumn = column as TreeListEditableColumn;
				if (treeListEditableColumn != null && treeListEditableColumn.IsEditable)
				{
					ITreeListColumnEditor columnEditor = this.GetColumnEditor(treeListEditableColumn);
					if (columnEditor != null)
					{
						IEnumerable editorValues = treeListEditableColumn.GetEditorValues(this);
						columnEditor.SetValues(editorValues);
					}
				}
			}
			base.OnCellDataBound(column, cell);
		}

		// Token: 0x040032CE RID: 13006
		private Dictionary<TreeListEditableColumn, ITreeListColumnEditor> _columnEditors;
	}
}
