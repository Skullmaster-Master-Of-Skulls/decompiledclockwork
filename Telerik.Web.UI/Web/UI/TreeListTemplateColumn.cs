using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02001208 RID: 4616
	public class TreeListTemplateColumn : TreeListEditableColumn
	{
		// Token: 0x17003D9B RID: 15771
		// (get) Token: 0x0600BEE2 RID: 48866 RVA: 0x002A4AE8 File Offset: 0x002A2CE8
		// (set) Token: 0x0600BEE3 RID: 48867 RVA: 0x002A4AF0 File Offset: 0x002A2CF0
		[DefaultValue(null)]
		[Browsable(false)]
		[TemplateContainer(typeof(TreeListFooterItem), BindingDirection.TwoWay)]
		[Description("Gets or sets the FooterTemplate, which is rendered in the column cell.")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public virtual ITemplate FooterTemplate
		{
			get
			{
				return this._footerItemTemplate;
			}
			set
			{
				this._footerItemTemplate = value;
			}
		}

		// Token: 0x17003D9C RID: 15772
		// (get) Token: 0x0600BEE4 RID: 48868 RVA: 0x002A4AF9 File Offset: 0x002A2CF9
		// (set) Token: 0x0600BEE5 RID: 48869 RVA: 0x002A4B01 File Offset: 0x002A2D01
		[Description("Gets or sets the ItemTemplate, which is rendered in the column cell.")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TemplateContainer(typeof(TreeListDataItem), BindingDirection.TwoWay)]
		[Browsable(false)]
		[DefaultValue(null)]
		public virtual ITemplate ItemTemplate
		{
			get
			{
				return this._itemTemplate;
			}
			set
			{
				this._itemTemplate = value;
			}
		}

		// Token: 0x17003D9D RID: 15773
		// (get) Token: 0x0600BEE6 RID: 48870 RVA: 0x002A4B0A File Offset: 0x002A2D0A
		// (set) Token: 0x0600BEE7 RID: 48871 RVA: 0x002A4B12 File Offset: 0x002A2D12
		[TemplateContainer(typeof(TreeListHeaderItem), BindingDirection.TwoWay)]
		[DefaultValue(null)]
		[Description("Gets or sets the HeaderTemplate, which is rendered in the column cell.")]
		[Browsable(false)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public virtual ITemplate HeaderTemplate
		{
			get
			{
				return this._headerTemplate;
			}
			set
			{
				this._headerTemplate = value;
			}
		}

		// Token: 0x17003D9E RID: 15774
		// (get) Token: 0x0600BEE8 RID: 48872 RVA: 0x002A4B1B File Offset: 0x002A2D1B
		// (set) Token: 0x0600BEE9 RID: 48873 RVA: 0x002A4B23 File Offset: 0x002A2D23
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TemplateContainer(typeof(TreeListEditableItem), BindingDirection.TwoWay)]
		[Description("Gets or sets the EditItemTemplate, which is rendered in the edit item cells and the edit form.")]
		[Browsable(false)]
		[DefaultValue(null)]
		public virtual ITemplate EditItemTemplate
		{
			get
			{
				return this._editItemTemplate;
			}
			set
			{
				this._editItemTemplate = value;
			}
		}

		// Token: 0x17003D9F RID: 15775
		// (get) Token: 0x0600BEEA RID: 48874 RVA: 0x002A4B2C File Offset: 0x002A2D2C
		// (set) Token: 0x0600BEEB RID: 48875 RVA: 0x002A4B34 File Offset: 0x002A2D34
		[TemplateContainer(typeof(TreeListEditableItem), BindingDirection.TwoWay)]
		[DefaultValue(null)]
		[Browsable(false)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Description("Gets or sets the InsertItemTemplate, which is rendered in the insert form.")]
		public virtual ITemplate InsertItemTemplate
		{
			get
			{
				return this._insertItemTemplate;
			}
			set
			{
				this._insertItemTemplate = value;
			}
		}

		// Token: 0x17003DA0 RID: 15776
		// (get) Token: 0x0600BEEC RID: 48876 RVA: 0x002A4B40 File Offset: 0x002A2D40
		// (set) Token: 0x0600BEED RID: 48877 RVA: 0x002A4B6D File Offset: 0x002A2D6D
		[Description("Sets or gets format string for the footer aggregate.")]
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		[Category("Behavior")]
		[Localizable(true)]
		public virtual string FooterAggregateFormatString
		{
			get
			{
				object obj = base.ViewState["FooterAggregateFormatString"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["FooterAggregateFormatString"] = value;
			}
		}

		// Token: 0x17003DA1 RID: 15777
		// (get) Token: 0x0600BEEE RID: 48878 RVA: 0x002A4B80 File Offset: 0x002A2D80
		// (set) Token: 0x0600BEEF RID: 48879 RVA: 0x002A4BA9 File Offset: 0x002A2DA9
		[NotifyParentProperty(true)]
		[Description("TreeListBoundColumn aggregate function")]
		[Category("Data")]
		[DefaultValue(typeof(TreeListAggregateFunction), "None")]
		public virtual TreeListAggregateFunction Aggregate
		{
			get
			{
				object obj = base.ViewState["Aggregate"];
				if (obj != null)
				{
					return (TreeListAggregateFunction)obj;
				}
				return TreeListAggregateFunction.None;
			}
			set
			{
				base.ViewState["Aggregate"] = value;
			}
		}

		// Token: 0x17003DA2 RID: 15778
		// (get) Token: 0x0600BEF0 RID: 48880 RVA: 0x002A4BC4 File Offset: 0x002A2DC4
		// (set) Token: 0x0600BEF1 RID: 48881 RVA: 0x002A4BF2 File Offset: 0x002A2DF2
		[DefaultValue(true)]
		[Description("Convert the emty values to null when extracting values during data editing operations.")]
		[NotifyParentProperty(true)]
		public bool ConvertEmptyStringToNull
		{
			get
			{
				object obj = base.ViewState["ConvertEmptyStringToNull"];
				if (obj == null)
				{
					obj = true;
				}
				return (bool)obj;
			}
			set
			{
				base.ViewState["ConvertEmptyStringToNull"] = value;
			}
		}

		// Token: 0x0600BEF2 RID: 48882 RVA: 0x002A4C0A File Offset: 0x002A2E0A
		protected override void InitializeFooterCells(TableCell cell, int columnIndex, TreeListFooterItem inItem)
		{
			if (this.Aggregate != TreeListAggregateFunction.None)
			{
				base.InitializeFooterCells(cell, columnIndex, inItem);
				return;
			}
			if (this.FooterTemplate != null)
			{
				this.FooterTemplate.InstantiateIn(cell);
				return;
			}
			cell.Text = this.FooterText;
		}

		// Token: 0x0600BEF3 RID: 48883 RVA: 0x002A4C3F File Offset: 0x002A2E3F
		protected override string GenerateUniqueName()
		{
			return base.GenerateUniqueNameBase("TemplateColumn");
		}

		// Token: 0x0600BEF4 RID: 48884 RVA: 0x002A4C4C File Offset: 0x002A2E4C
		protected override void InitializeDataCells(TableCell cell, int columnIndex, TreeListDataItem inItem)
		{
			cell.Text = string.Empty;
			if (this.ItemTemplate == null)
			{
				return;
			}
			this.ItemTemplate.InstantiateIn(cell);
		}

		// Token: 0x0600BEF5 RID: 48885 RVA: 0x002A4C6E File Offset: 0x002A2E6E
		protected override void InitializeHeaderCells(TableCell cell, int columnIndex, TreeListHeaderItem inItem)
		{
			cell.Text = string.Empty;
			if (this.HeaderTemplate == null)
			{
				base.InitializeHeaderCells(cell, columnIndex, inItem);
				return;
			}
			this.HeaderTemplate.InstantiateIn(cell);
		}

		// Token: 0x0600BEF6 RID: 48886 RVA: 0x002A4C99 File Offset: 0x002A2E99
		protected override void OnColumnDataCellBinding(object sender, EventArgs e)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600BEF7 RID: 48887 RVA: 0x002A4CA0 File Offset: 0x002A2EA0
		public override ITreeListColumnEditor CreateDefaultColumnEditor()
		{
			return new TreeListTemplateColumnEditor(this);
		}

		// Token: 0x0600BEF8 RID: 48888 RVA: 0x002A4CA8 File Offset: 0x002A2EA8
		[SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
		public override void FillValues(IDictionary newValues, TreeListEditableItem editableItem)
		{
			TreeListTemplateColumnEditor treeListTemplateColumnEditor = editableItem.GetColumnEditor(this) as TreeListTemplateColumnEditor;
			if (treeListTemplateColumnEditor != null)
			{
				IOrderedDictionary orderedDictionary = treeListTemplateColumnEditor.GetValues() as IOrderedDictionary;
				if (orderedDictionary == null)
				{
					return;
				}
				using (IDictionaryEnumerator enumerator = orderedDictionary.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
						object value = dictionaryEntry.Value;
						if (value is string && string.IsNullOrEmpty((string)value) && this.ConvertEmptyStringToNull)
						{
							newValues[dictionaryEntry.Key] = null;
						}
						else
						{
							newValues[dictionaryEntry.Key] = value;
						}
					}
					return;
				}
			}
			base.FillValues(newValues, editableItem);
		}

		// Token: 0x0400321F RID: 12831
		private ITemplate _headerTemplate;

		// Token: 0x04003220 RID: 12832
		private ITemplate _itemTemplate;

		// Token: 0x04003221 RID: 12833
		private ITemplate _editItemTemplate;

		// Token: 0x04003222 RID: 12834
		private ITemplate _insertItemTemplate;

		// Token: 0x04003223 RID: 12835
		private ITemplate _footerItemTemplate;
	}
}
