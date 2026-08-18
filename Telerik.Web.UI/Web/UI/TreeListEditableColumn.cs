using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x020011FB RID: 4603
	public abstract class TreeListEditableColumn : TreeListDataColumn
	{
		// Token: 0x17003D4C RID: 15692
		// (get) Token: 0x0600BE04 RID: 48644 RVA: 0x002A1870 File Offset: 0x0029FA70
		// (set) Token: 0x0600BE05 RID: 48645 RVA: 0x002A1899 File Offset: 0x0029FA99
		[DefaultValue(false)]
		[NotifyParentProperty(true)]
		public virtual bool ReadOnly
		{
			get
			{
				object obj = base.ViewState["ReadOnly"];
				return obj != null && (bool)obj;
			}
			set
			{
				base.ViewState["ReadOnly"] = value;
			}
		}

		// Token: 0x17003D4D RID: 15693
		// (get) Token: 0x0600BE06 RID: 48646 RVA: 0x002A18B1 File Offset: 0x0029FAB1
		public virtual bool IsEditable
		{
			get
			{
				return !this.ReadOnly;
			}
		}

		// Token: 0x0600BE07 RID: 48647
		public abstract ITreeListColumnEditor CreateDefaultColumnEditor();

		// Token: 0x0600BE08 RID: 48648 RVA: 0x002A18BC File Offset: 0x0029FABC
		public virtual ITreeListColumnEditor GetColumnEditor(TreeListEditableItem editedItem)
		{
			return editedItem.GetColumnEditor(this);
		}

		// Token: 0x0600BE09 RID: 48649 RVA: 0x002A18C5 File Offset: 0x0029FAC5
		public bool ShouldExtractValues(TreeListEditableItem item)
		{
			return (item.IsInEditMode && (this.ForceExtractValue == TreeListForceExtractValues.InEditMode || this.ForceExtractValue == TreeListForceExtractValues.Always)) || (!item.IsInEditMode && (this.ForceExtractValue == TreeListForceExtractValues.InBrowseMode || this.ForceExtractValue == TreeListForceExtractValues.Always)) || this.IsEditable;
		}

		// Token: 0x17003D4E RID: 15694
		// (get) Token: 0x0600BE0A RID: 48650 RVA: 0x002A1908 File Offset: 0x0029FB08
		// (set) Token: 0x0600BE0B RID: 48651 RVA: 0x002A1936 File Offset: 0x0029FB36
		[DefaultValue(TreeListForceExtractValues.None)]
		[NotifyParentProperty(true)]
		public TreeListForceExtractValues ForceExtractValue
		{
			get
			{
				object obj = base.ViewState["ForceExtractValue"];
				if (obj == null)
				{
					obj = TreeListForceExtractValues.None;
				}
				return (TreeListForceExtractValues)obj;
			}
			set
			{
				base.ViewState["ForceExtractValue"] = value;
			}
		}

		// Token: 0x0600BE0C RID: 48652 RVA: 0x002A1950 File Offset: 0x0029FB50
		public virtual void FillValues(IDictionary newValues, TreeListEditableItem editableItem)
		{
			if (!string.IsNullOrEmpty(base.DataField))
			{
				ITreeListColumnEditor columnEditor = editableItem.GetColumnEditor(this);
				if (columnEditor != null)
				{
					newValues[base.DataField] = TreeListColumnEditor.GetFirstValueFromEnumerable(columnEditor.GetValues());
					return;
				}
				TreeListDataItem parentDataItem = base.Owner.GetParentDataItem(editableItem);
				if (parentDataItem != null)
				{
					object value;
					if (this.TryGetColumnValueFromDataKeys(parentDataItem, out value))
					{
						if (!(editableItem is ITreeListInsertItem))
						{
							newValues[base.DataField] = value;
							return;
						}
					}
					else
					{
						newValues[base.DataField] = this.GetColumnValueFromDataCell(parentDataItem[this.UniqueName]);
					}
				}
			}
		}

		// Token: 0x0600BE0D RID: 48653 RVA: 0x002A19DC File Offset: 0x0029FBDC
		[SuppressMessage("Microsoft.Design", "CA1007:UseGenericsWhereAppropriate")]
		protected virtual bool TryGetColumnValueFromDataKeys(TreeListDataItem dataItem, out object result)
		{
			if (!string.IsNullOrEmpty(base.DataField))
			{
				foreach (string strA in base.Owner.DataKeyNames)
				{
					if (string.Compare(strA, base.DataField, true, CultureInfo.CurrentCulture) == 0)
					{
						result = dataItem.GetDataKeyValue(base.DataField);
						return true;
					}
				}
				foreach (string strA2 in base.Owner.ParentDataKeyNames)
				{
					if (string.Compare(strA2, base.DataField, true, CultureInfo.CurrentCulture) == 0)
					{
						result = dataItem.GetParentDataKeyValue(base.DataField);
						return true;
					}
				}
			}
			result = null;
			return false;
		}

		// Token: 0x0600BE0E RID: 48654 RVA: 0x002A1A94 File Offset: 0x0029FC94
		protected virtual object GetColumnValueFromDataCell(TableCell cell)
		{
			return cell.Text;
		}

		// Token: 0x0600BE0F RID: 48655 RVA: 0x002A1BC8 File Offset: 0x0029FDC8
		public virtual IEnumerable GetEditorValues(TreeListEditableItem editableItem)
		{
			object dataItem = editableItem.DataItem;
			object extractedValue = null;
			if (!string.IsNullOrEmpty(base.DataField) && dataItem != null && base.TryExtractDataValue(dataItem, base.DataField, out extractedValue))
			{
				yield return extractedValue;
			}
			yield break;
		}

		// Token: 0x17003D4F RID: 15695
		// (get) Token: 0x0600BE10 RID: 48656 RVA: 0x002A1BEC File Offset: 0x0029FDEC
		// (set) Token: 0x0600BE11 RID: 48657 RVA: 0x002A1C0C File Offset: 0x0029FE0C
		[Description("Gets or sets the default value for this column's editor when a new item is inserted in RadTreeList.")]
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		public string DefaultInsertValue
		{
			get
			{
				return (base.ViewState["DefaultInsertValue"] as string) ?? string.Empty;
			}
			set
			{
				base.ViewState["DefaultInsertValue"] = value;
			}
		}

		// Token: 0x17003D50 RID: 15696
		// (get) Token: 0x0600BE12 RID: 48658 RVA: 0x002A1C1F File Offset: 0x0029FE1F
		// (set) Token: 0x0600BE13 RID: 48659 RVA: 0x002A1C3F File Offset: 0x0029FE3F
		[Localizable(true)]
		[DefaultValue("{0}:")]
		[NotifyParentProperty(true)]
		public string EditFormHeaderTextFormat
		{
			get
			{
				return (base.ViewState["EditFormHeaderTextFormat"] as string) ?? "{0}:";
			}
			set
			{
				base.ViewState["EditFormHeaderTextFormat"] = value;
			}
		}

		// Token: 0x17003D51 RID: 15697
		// (get) Token: 0x0600BE14 RID: 48660 RVA: 0x002A1C54 File Offset: 0x0029FE54
		// (set) Token: 0x0600BE15 RID: 48661 RVA: 0x002A1C82 File Offset: 0x0029FE82
		[NotifyParentProperty(true)]
		[DefaultValue(0)]
		public int EditFormColumnIndex
		{
			get
			{
				object obj = base.ViewState["_efci"];
				if (obj == null)
				{
					obj = 0;
				}
				return (int)obj;
			}
			set
			{
				base.ViewState["_efci"] = value;
			}
		}

		// Token: 0x17003D52 RID: 15698
		// (get) Token: 0x0600BE16 RID: 48662 RVA: 0x002A1C9C File Offset: 0x0029FE9C
		// (set) Token: 0x0600BE17 RID: 48663 RVA: 0x002A1CF1 File Offset: 0x0029FEF1
		[DefaultValue(true)]
		[Description("Gets or sets whether the column editor will be native when treelist's RenderMode is set to Mobile")]
		[NotifyParentProperty(true)]
		public bool UseNativeEditorsInMobileMode
		{
			get
			{
				object obj = base.ViewState["_uneimm"];
				if (obj == null)
				{
					obj = ((ConfigurationManager.AppSettings["UseTreeListNativeEditorsInMobileMode"] == null) ? null : ConfigurationManager.AppSettings["UseTreeListNativeEditorsInMobileMode"]);
					if (obj == null)
					{
						obj = true;
					}
				}
				return Convert.ToBoolean(obj);
			}
			set
			{
				base.ViewState["_uneimm"] = value;
			}
		}
	}
}
