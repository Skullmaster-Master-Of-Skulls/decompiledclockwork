using System;
using System.Collections;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x020004C0 RID: 1216
	public class GridAutoCompleteColumn : GridEditableColumn
	{
		// Token: 0x17000E39 RID: 3641
		// (get) Token: 0x06002C0D RID: 11277 RVA: 0x000902FC File Offset: 0x0008E4FC
		// (set) Token: 0x06002C0E RID: 11278 RVA: 0x00090329 File Offset: 0x0008E529
		[Category("Data")]
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		public string DataSourceID
		{
			get
			{
				object obj = base.ViewState["_dsID"];
				if (obj == null)
				{
					obj = "";
				}
				return (string)obj;
			}
			set
			{
				base.ViewState["_dsID"] = value;
			}
		}

		// Token: 0x17000E3A RID: 3642
		// (get) Token: 0x06002C0F RID: 11279 RVA: 0x0009033C File Offset: 0x0008E53C
		// (set) Token: 0x06002C10 RID: 11280 RVA: 0x00090369 File Offset: 0x0008E569
		[Description("DataField from RadGrid DataSource")]
		[Category("Data")]
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		public string DataField
		{
			get
			{
				object obj = base.ViewState["DataField"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["DataField"] = value;
				base.UpdateUniqueNameIfDefault(value);
				this.OnColumnChanged();
			}
		}

		// Token: 0x06002C11 RID: 11281 RVA: 0x0009038C File Offset: 0x0008E58C
		public override GridColumn Clone()
		{
			GridAutoCompleteColumn gridAutoCompleteColumn = new GridAutoCompleteColumn();
			gridAutoCompleteColumn.CopyBaseProperties(this);
			return gridAutoCompleteColumn;
		}

		// Token: 0x06002C12 RID: 11282 RVA: 0x000903A8 File Offset: 0x0008E5A8
		protected override void CopyBaseProperties(GridColumn fromColumn)
		{
			base.CopyBaseProperties(fromColumn);
			GridAutoCompleteColumn gridAutoCompleteColumn = (GridAutoCompleteColumn)fromColumn;
			this.DataField = gridAutoCompleteColumn.DataField;
			this.DataSourceID = gridAutoCompleteColumn.DataSourceID;
			this.InputType = gridAutoCompleteColumn.InputType;
			this.Filter = gridAutoCompleteColumn.Filter;
			this.AllowCustomEntry = gridAutoCompleteColumn.AllowCustomEntry;
			this.SelectionMode = gridAutoCompleteColumn.SelectionMode;
			this.AllowTokenEditing = gridAutoCompleteColumn.AllowTokenEditing;
			this.Delimiter = gridAutoCompleteColumn.Delimiter;
			this.DataTextField = gridAutoCompleteColumn.DataTextField;
			this.DataValueField = gridAutoCompleteColumn.DataValueField;
			this.EmptyDataText = gridAutoCompleteColumn.EmptyDataText;
		}

		// Token: 0x06002C13 RID: 11283 RVA: 0x00090448 File Offset: 0x0008E648
		public override void FillValues(IDictionary newValues, GridEditableItem editableItem)
		{
			if (editableItem.IsInEditMode)
			{
				GridAutoCompleteColumnEditor gridAutoCompleteColumnEditor = (GridAutoCompleteColumnEditor)editableItem.EditManager.GetColumnEditor(this);
				newValues[this.DataField] = base.ConvertValueIfEmpty(gridAutoCompleteColumnEditor.AutoCompleteBox.Text.Trim());
				return;
			}
			TableCell tableCell = editableItem[this];
			Literal literal = tableCell.FindControl(string.Format("ROLC_{0}", this.UniqueName)) as Literal;
			if (literal != null)
			{
				newValues[this.DataField] = literal.Text;
				if (string.Equals(editableItem[this].Text.Trim(), this.EmptyDataText, StringComparison.InvariantCultureIgnoreCase))
				{
					editableItem[this].Text = string.Empty;
				}
			}
			else
			{
				newValues[this.DataField] = "";
			}
			newValues[this.DataField] = base.ConvertValueIfEmpty(editableItem[this].Text);
		}

		// Token: 0x17000E3B RID: 3643
		// (get) Token: 0x06002C14 RID: 11284 RVA: 0x0009052C File Offset: 0x0008E72C
		// (set) Token: 0x06002C15 RID: 11285 RVA: 0x00090559 File Offset: 0x0008E759
		[Description("Sets or gets default text when column is empty")]
		[Localizable(true)]
		[DefaultValue("&nbsp;")]
		[NotifyParentProperty(true)]
		public virtual string EmptyDataText
		{
			get
			{
				object obj = base.ViewState["EmptyDataText"];
				if (obj != null)
				{
					return (string)obj;
				}
				return "&nbsp;";
			}
			set
			{
				base.ViewState["EmptyDataText"] = value;
				this.OnColumnChanged();
			}
		}

		// Token: 0x17000E3C RID: 3644
		// (get) Token: 0x06002C16 RID: 11286 RVA: 0x00090572 File Offset: 0x0008E772
		public override bool IsEditable
		{
			get
			{
				return !this.ReadOnly;
			}
		}

		// Token: 0x06002C17 RID: 11287 RVA: 0x0009057D File Offset: 0x0008E77D
		internal override string GetSortExpression()
		{
			if (string.IsNullOrEmpty(this.SortExpression) && !string.IsNullOrEmpty(this.DataField) && this.AllowSorting)
			{
				return this.DataField;
			}
			return base.GetSortExpression();
		}

		// Token: 0x17000E3D RID: 3645
		// (get) Token: 0x06002C18 RID: 11288 RVA: 0x000905AE File Offset: 0x0008E7AE
		protected override bool Sortable
		{
			get
			{
				return this.AllowSorting;
			}
		}

		// Token: 0x06002C19 RID: 11289 RVA: 0x000905B6 File Offset: 0x0008E7B6
		public override bool SupportsFiltering()
		{
			return this.AllowFiltering;
		}

		// Token: 0x06002C1A RID: 11290 RVA: 0x000905C0 File Offset: 0x0008E7C0
		private bool cellIsEmpty(TableCell cell)
		{
			string text = cell.Text ?? "";
			return (string.IsNullOrEmpty(text) || text == "&nbsp;") && cell.Controls.Count == 0;
		}

		// Token: 0x06002C1B RID: 11291 RVA: 0x00090604 File Offset: 0x0008E804
		public override void InitializeCell(TableCell cell, int columnIndex, GridItem inItem)
		{
			if (inItem is GridFilteringItem)
			{
				if (base.Owner.OwnerGrid.IsExporting)
				{
					base.Owner.ClearTableViewScriptControls(cell);
				}
				if (inItem.OwnerTableView.AllowFilteringByColumn)
				{
					if (this.AllowFiltering)
					{
						cell.Style["white-space"] = "nowrap";
						this.SetupFilterControls(cell, inItem);
						inItem.CellDataBound += this.inItem_CellDataBound;
						return;
					}
					if (this.cellIsEmpty(cell))
					{
						cell.Text = this.EmptyDataText;
					}
				}
				return;
			}
			base.InitializeCell(cell, columnIndex, inItem);
			if (!inItem.IsDataBound)
			{
				return;
			}
			if (this.AutoCompleteBoxEditor.DataSource == null && !base.DesignMode)
			{
				if (!string.IsNullOrEmpty(this.DataSourceID))
				{
					this.AutoCompleteBoxEditor.DataSource = DataSourceControlHelper.FindControl(inItem, this.DataSourceID);
				}
				else
				{
					this.AutoCompleteBoxEditor.DataSource = inItem.OwnerTableView.DataSource;
				}
			}
			if (inItem.IsInEditMode && !base.IsReadOnly(inItem))
			{
				this.CurrentColumnEditor.InitializeInControl(cell);
			}
			else if ((Literal)inItem.FindControl(string.Format("ROLC_{0}", this.UniqueName)) == null)
			{
				Literal literal = new Literal();
				literal.ID = string.Format("ROLC_{0}", this.UniqueName);
				cell.Controls.Add(literal);
			}
			inItem.CellDataBound += this.OnDataBindColumn;
		}

		// Token: 0x06002C1C RID: 11292 RVA: 0x0009076D File Offset: 0x0008E96D
		private void inItem_CellDataBound(object sender, GridCellDataBoundEventArgs args)
		{
			if (args.Column == this && this.SupportsFiltering())
			{
				this.SetCurrentFilterValueToControl(args.Cell);
			}
		}

		// Token: 0x06002C1D RID: 11293 RVA: 0x0009078C File Offset: 0x0008E98C
		private void OnDataBindColumn(object sender, GridCellDataBoundEventArgs args)
		{
			if (args.Column != this)
			{
				return;
			}
			GridItem gridItem = (GridItem)sender;
			object dataItem = gridItem.DataItem;
			TableCell cell = args.Cell;
			if (dataItem == null || cell == null || !gridItem.IsDataBound)
			{
				return;
			}
			string text = this.EmptyDataText;
			if (!string.IsNullOrEmpty(this.DataField))
			{
				text = this.ExtractValueFromDataItem(dataItem, text);
			}
			if (gridItem.IsInEditMode)
			{
				this.AutoCompleteBoxEditor.Text = text;
				return;
			}
			Literal literal = cell.FindControl(string.Format("ROLC_{0}", this.UniqueName)) as Literal;
			if (literal == null)
			{
				literal = new Literal();
				literal.ID = string.Format("ROLC_{0}", this.UniqueName);
				cell.Controls.Add(literal);
			}
			literal.Text = text;
		}

		// Token: 0x06002C1E RID: 11294 RVA: 0x00090850 File Offset: 0x0008EA50
		private string ExtractValueFromDataItem(object dataItem, string currValue)
		{
			object obj = null;
			if (this.DataField.IndexOf(".") > -1)
			{
				try
				{
					obj = DataBinder.GetPropertyValue(dataItem, this.DataField);
					goto IL_55;
				}
				catch
				{
					try
					{
						obj = DataBinder.Eval(dataItem, this.DataField);
					}
					catch
					{
						if (!GridBaseDataList.IsBindableType(obj.GetType()))
						{
							obj = null;
						}
					}
					goto IL_55;
				}
			}
			obj = DataBinder.Eval(dataItem, this.DataField);
			IL_55:
			if (obj == null)
			{
				PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(dataItem).Find(this.DataField, true);
				if (propertyDescriptor != null)
				{
					obj = propertyDescriptor.GetValue(dataItem);
				}
			}
			return (obj != null) ? obj.ToString() : string.Empty;
		}

		// Token: 0x06002C1F RID: 11295 RVA: 0x00090904 File Offset: 0x0008EB04
		protected override string GenerateUniqueName()
		{
			return base.GenerateUniqueNameBase(this.DataField);
		}

		// Token: 0x06002C20 RID: 11296 RVA: 0x00090912 File Offset: 0x0008EB12
		protected override IGridColumnEditor CreateDefaultColumnEditor()
		{
			return new GridAutoCompleteColumnEditor(this);
		}

		// Token: 0x17000E3E RID: 3646
		// (get) Token: 0x06002C21 RID: 11297 RVA: 0x0009091A File Offset: 0x0008EB1A
		private GridAutoCompleteColumnEditor AutoCompleteBoxEditor
		{
			get
			{
				return this.CurrentColumnEditor as GridAutoCompleteColumnEditor;
			}
		}

		// Token: 0x06002C22 RID: 11298 RVA: 0x00090927 File Offset: 0x0008EB27
		protected override void ColumnEditorChange(IGridColumnEditor newValue)
		{
			if (!(newValue is GridAutoCompleteColumnEditor))
			{
				throw new GridColumnEditorException(this.ToString() + " accepts only editor of type: " + typeof(GridAutoCompleteColumnEditor).ToString());
			}
			base.ColumnEditorChange(newValue);
		}

		// Token: 0x06002C23 RID: 11299 RVA: 0x0009095D File Offset: 0x0008EB5D
		public override bool IsBoundToFieldName(string name)
		{
			return string.Compare(this.DataField, name, true) == 0;
		}

		// Token: 0x17000E3F RID: 3647
		// (get) Token: 0x06002C24 RID: 11300 RVA: 0x0009096F File Offset: 0x0008EB6F
		// (set) Token: 0x06002C25 RID: 11301 RVA: 0x00090990 File Offset: 0x0008EB90
		[Category("Behavior")]
		[ClientControlProperty]
		[ClientPropertyName("inputType")]
		[DefaultValue(RadAutoCompleteInputType.Token)]
		[Bindable(false)]
		public RadAutoCompleteInputType InputType
		{
			get
			{
				return (RadAutoCompleteInputType)(base.ViewState["InputType"] ?? RadAutoCompleteInputType.Token);
			}
			set
			{
				base.ViewState["InputType"] = value;
			}
		}

		// Token: 0x17000E40 RID: 3648
		// (get) Token: 0x06002C26 RID: 11302 RVA: 0x000909A8 File Offset: 0x0008EBA8
		// (set) Token: 0x06002C27 RID: 11303 RVA: 0x000909C9 File Offset: 0x0008EBC9
		[ClientControlProperty]
		[ClientPropertyName("filter")]
		[Category("Behavior")]
		[DefaultValue(RadAutoCompleteFilter.Contains)]
		[Bindable(false)]
		public RadAutoCompleteFilter Filter
		{
			get
			{
				return (RadAutoCompleteFilter)(base.ViewState["Filter"] ?? RadAutoCompleteFilter.Contains);
			}
			set
			{
				base.ViewState["Filter"] = value;
			}
		}

		// Token: 0x17000E41 RID: 3649
		// (get) Token: 0x06002C28 RID: 11304 RVA: 0x000909E1 File Offset: 0x0008EBE1
		// (set) Token: 0x06002C29 RID: 11305 RVA: 0x00090A02 File Offset: 0x0008EC02
		[ClientPropertyName("allowCustomEntry")]
		[DefaultValue(true)]
		[Category("Behavior")]
		[Bindable(false)]
		[ClientControlProperty]
		public bool AllowCustomEntry
		{
			get
			{
				return (bool)(base.ViewState["AllowCustomEntry"] ?? true);
			}
			set
			{
				base.ViewState["AllowCustomEntry"] = value;
			}
		}

		// Token: 0x17000E42 RID: 3650
		// (get) Token: 0x06002C2A RID: 11306 RVA: 0x00090A1A File Offset: 0x0008EC1A
		// (set) Token: 0x06002C2B RID: 11307 RVA: 0x00090A3B File Offset: 0x0008EC3B
		[Description("The selection mode of the RadAutoCompleteBox.")]
		[Category("Behavior")]
		[NotifyParentProperty(true)]
		[DefaultValue(RadAutoCompleteSelectionMode.Multiple)]
		public RadAutoCompleteSelectionMode SelectionMode
		{
			get
			{
				return (RadAutoCompleteSelectionMode)(base.ViewState["SelectionMode"] ?? RadAutoCompleteSelectionMode.Multiple);
			}
			set
			{
				base.ViewState["SelectionMode"] = value;
			}
		}

		// Token: 0x17000E43 RID: 3651
		// (get) Token: 0x06002C2C RID: 11308 RVA: 0x00090A53 File Offset: 0x0008EC53
		// (set) Token: 0x06002C2D RID: 11309 RVA: 0x00090A74 File Offset: 0x0008EC74
		[Description("Allow token editing at client-side upon double click.")]
		[Category("Behavior")]
		[NotifyParentProperty(true)]
		[DefaultValue(false)]
		public bool AllowTokenEditing
		{
			get
			{
				return (bool)(base.ViewState["AllowTokenEditing"] ?? false);
			}
			set
			{
				base.ViewState["AllowTokenEditing"] = value;
			}
		}

		// Token: 0x17000E44 RID: 3652
		// (get) Token: 0x06002C2E RID: 11310 RVA: 0x00090A8C File Offset: 0x0008EC8C
		// (set) Token: 0x06002C2F RID: 11311 RVA: 0x00090AAC File Offset: 0x0008ECAC
		[DefaultValue(" ")]
		[Category("Behavior")]
		[ClientPropertyName("delimiter")]
		[Bindable(false)]
		[ClientControlProperty]
		public string Delimiter
		{
			get
			{
				return (string)(base.ViewState["Delimiter"] ?? " ");
			}
			set
			{
				base.ViewState["Delimiter"] = value;
			}
		}

		// Token: 0x17000E45 RID: 3653
		// (get) Token: 0x06002C30 RID: 11312 RVA: 0x00090ABF File Offset: 0x0008ECBF
		// (set) Token: 0x06002C31 RID: 11313 RVA: 0x00090ADF File Offset: 0x0008ECDF
		[DefaultValue("")]
		[Category("Data")]
		public virtual string DataTextField
		{
			get
			{
				return (string)(base.ViewState["DataTextField"] ?? string.Empty);
			}
			set
			{
				base.ViewState["DataTextField"] = value;
			}
		}

		// Token: 0x17000E46 RID: 3654
		// (get) Token: 0x06002C32 RID: 11314 RVA: 0x00090AF2 File Offset: 0x0008ECF2
		// (set) Token: 0x06002C33 RID: 11315 RVA: 0x00090B12 File Offset: 0x0008ED12
		[Category("Data")]
		[DefaultValue("")]
		public virtual string DataValueField
		{
			get
			{
				return (string)(base.ViewState["DataValueField"] ?? string.Empty);
			}
			set
			{
				base.ViewState["DataValueField"] = value;
			}
		}

		// Token: 0x06002C34 RID: 11316 RVA: 0x00090B44 File Offset: 0x0008ED44
		protected virtual void SetupFilterControls(TableCell cell, GridItem inItem)
		{
			if (this.FilterTemplate != null)
			{
				this.FilterTemplate.InstantiateIn(cell);
				return;
			}
			RadAutoCompleteBox radAutoCompleteBox = new RadAutoCompleteBox();
			radAutoCompleteBox.ID = string.Format("RACBF_{0}", this.UniqueName);
			radAutoCompleteBox.RenderMode = base.Owner.OwnerGrid.RenderMode;
			cell.Controls.Add(radAutoCompleteBox);
			radAutoCompleteBox.Attributes["alt"] = this.FilterControlAltText;
			radAutoCompleteBox.ToolTip = this.FilterControlToolTip;
			radAutoCompleteBox.InputType = this.InputType;
			radAutoCompleteBox.Filter = this.Filter;
			radAutoCompleteBox.AllowCustomEntry = this.AllowCustomEntry;
			radAutoCompleteBox.TokensSettings.AllowTokenEditing = this.AllowTokenEditing;
			radAutoCompleteBox.Delimiter = this.Delimiter;
			radAutoCompleteBox.DataTextField = this.DataTextField;
			radAutoCompleteBox.DataValueField = this.DataValueField;
			radAutoCompleteBox.TextSettings.SelectionMode = RadAutoCompleteSelectionMode.Single;
			if (radAutoCompleteBox.DataSource == null && !base.DesignMode)
			{
				if (!string.IsNullOrEmpty(this.DataSourceID))
				{
					radAutoCompleteBox.DataSource = DataSourceControlHelper.FindControl(inItem, this.DataSourceID);
				}
				else
				{
					radAutoCompleteBox.DataSource = inItem.OwnerTableView.DataSource;
				}
			}
			radAutoCompleteBox.PreRender += delegate(object sender, EventArgs e)
			{
				((RadAutoCompleteBox)sender).Skin = base.Owner.OwnerGrid.RuntimeSkin;
			};
			if (!this.FilterControlWidth.IsEmpty)
			{
				radAutoCompleteBox.Width = this.FilterControlWidth;
			}
			if (this.ShowFilterIcon)
			{
				if (base.Owner.OwnerGrid.ShouldRenderImg(this.FilterImageUrl))
				{
					Image image = new Image();
					image.ImageUrl = this.FilterImageUrl;
					image.AlternateText = this.FilterImageToolTip;
					image.ToolTip = this.FilterImageToolTip;
					image.BorderWidth = Unit.Pixel(0);
					image.ID = string.Format("Filter_{0}", this.UniqueName);
					cell.Controls.Add(image);
					return;
				}
				if (base.Owner.OwnerGrid.ResolvedRenderMode == RenderMode.Lightweight)
				{
					ElasticButton elasticButton = new ElasticButton
					{
						CssClass = "t-button rgActionButton ",
						FirstSpanClass = "t-font-icon rgIcon rgFilterIcon"
					};
					ElasticButton elasticButton2 = elasticButton;
					elasticButton2.CssClass += "rgFilter";
					RadGrid.ToggleColumnFilteredClass(elasticButton, this);
					elasticButton.ToolTip = this.FilterImageToolTip;
					elasticButton.Text = this.FilterImageToolTip;
					elasticButton.ID = string.Format("Filter_{0}", this.UniqueName);
					cell.Controls.Add(elasticButton);
					return;
				}
				Button button = new Button();
				button.CssClass = "rgFilter";
				RadGrid.ToggleColumnFilteredClass(button, this);
				button.ToolTip = this.FilterImageToolTip;
				button.ID = string.Format("Filter_{0}", this.UniqueName);
				cell.Controls.Add(button);
			}
		}

		// Token: 0x06002C35 RID: 11317 RVA: 0x00090DF0 File Offset: 0x0008EFF0
		protected override string GetCurrentFilterValueFromControl(TableCell cell)
		{
			foreach (object obj in cell.Controls)
			{
				Control control = (Control)obj;
				RadAutoCompleteBox radAutoCompleteBox = control as RadAutoCompleteBox;
				if (radAutoCompleteBox != null)
				{
					return radAutoCompleteBox.Text.Trim();
				}
			}
			return string.Empty;
		}

		// Token: 0x06002C36 RID: 11318 RVA: 0x00090E64 File Offset: 0x0008F064
		protected override void SetCurrentFilterValueToControl(TableCell cell)
		{
			if (!string.IsNullOrEmpty(this.CurrentFilterValue))
			{
				foreach (object obj in cell.Controls)
				{
					Control control = (Control)obj;
					RadAutoCompleteBox radAutoCompleteBox = control as RadAutoCompleteBox;
					if (radAutoCompleteBox != null)
					{
						radAutoCompleteBox.PopulateFromString(this.CurrentFilterValue);
					}
				}
			}
		}

		// Token: 0x06002C37 RID: 11319 RVA: 0x00090EDC File Offset: 0x0008F0DC
		protected override string GetFilterDataField()
		{
			return this.DataField;
		}
	}
}
