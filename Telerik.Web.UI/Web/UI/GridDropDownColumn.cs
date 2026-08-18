using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x020010B6 RID: 4278
	[SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable")]
	public class GridDropDownColumn : GridEditableColumn
	{
		// Token: 0x17003850 RID: 14416
		// (get) Token: 0x0600AE5D RID: 44637 RVA: 0x0025A1E4 File Offset: 0x002583E4
		// (set) Token: 0x0600AE5E RID: 44638 RVA: 0x0025A211 File Offset: 0x00258411
		[Localizable(true)]
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		public string ListTextFormatString
		{
			get
			{
				object obj = base.ViewState["ListTextFormatString"];
				if (obj == null)
				{
					obj = "";
				}
				return (string)obj;
			}
			set
			{
				base.ViewState["ListTextFormatString"] = value;
			}
		}

		// Token: 0x17003851 RID: 14417
		// (get) Token: 0x0600AE5F RID: 44639 RVA: 0x0025A224 File Offset: 0x00258424
		// (set) Token: 0x0600AE60 RID: 44640 RVA: 0x0025A251 File Offset: 0x00258451
		[NotifyParentProperty(true)]
		[Category("Data")]
		[DefaultValue("")]
		public string ListDataMember
		{
			get
			{
				object obj = base.ViewState["ListDataMember"];
				if (obj == null)
				{
					obj = "";
				}
				return (string)obj;
			}
			set
			{
				base.ViewState["ListDataMember"] = value;
			}
		}

		// Token: 0x17003852 RID: 14418
		// (get) Token: 0x0600AE61 RID: 44641 RVA: 0x0025A264 File Offset: 0x00258464
		// (set) Token: 0x0600AE62 RID: 44642 RVA: 0x0025A291 File Offset: 0x00258491
		[NotifyParentProperty(true)]
		[Category("Data")]
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

		// Token: 0x17003853 RID: 14419
		// (get) Token: 0x0600AE63 RID: 44643 RVA: 0x0025A2A4 File Offset: 0x002584A4
		// (set) Token: 0x0600AE64 RID: 44644 RVA: 0x0025A2D1 File Offset: 0x002584D1
		[DefaultValue("")]
		[Category("Data")]
		[NotifyParentProperty(true)]
		public string ListTextField
		{
			get
			{
				object obj = base.ViewState["ListTextField"];
				if (obj == null)
				{
					obj = "";
				}
				return (string)obj;
			}
			set
			{
				base.ViewState["ListTextField"] = value;
			}
		}

		// Token: 0x17003854 RID: 14420
		// (get) Token: 0x0600AE65 RID: 44645 RVA: 0x0025A2E4 File Offset: 0x002584E4
		// (set) Token: 0x0600AE66 RID: 44646 RVA: 0x0025A311 File Offset: 0x00258511
		[NotifyParentProperty(true)]
		[Category("Data")]
		[DefaultValue("")]
		public string ListValueField
		{
			get
			{
				object obj = base.ViewState["ListValueField"];
				if (obj == null)
				{
					obj = "";
				}
				return (string)obj;
			}
			set
			{
				base.ViewState["ListValueField"] = value;
			}
		}

		// Token: 0x17003855 RID: 14421
		// (get) Token: 0x0600AE67 RID: 44647 RVA: 0x0025A324 File Offset: 0x00258524
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Category("Validation")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[NotifyParentProperty(true)]
		public GridColumnValidationSettings ColumnValidationSettings
		{
			get
			{
				if (this._columnValidationSettings == null)
				{
					this._columnValidationSettings = new GridColumnValidationSettings(base.ViewState, this);
				}
				return this._columnValidationSettings;
			}
		}

		// Token: 0x0600AE68 RID: 44648 RVA: 0x0025A346 File Offset: 0x00258546
		public override string GetDefaultGroupByExpression()
		{
			if (string.IsNullOrEmpty(this.DataField))
			{
				return base.GetDefaultGroupByExpression();
			}
			return this.DataField + " Group By " + this.DataField;
		}

		// Token: 0x17003856 RID: 14422
		// (get) Token: 0x0600AE69 RID: 44649 RVA: 0x0025A374 File Offset: 0x00258574
		// (set) Token: 0x0600AE6A RID: 44650 RVA: 0x0025A3A1 File Offset: 0x002585A1
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		[Description("DataField from RadGrid DataSource")]
		[Category("Data")]
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

		// Token: 0x17003857 RID: 14423
		// (get) Token: 0x0600AE6B RID: 44651 RVA: 0x0025A3C1 File Offset: 0x002585C1
		// (set) Token: 0x0600AE6C RID: 44652 RVA: 0x0025A3E2 File Offset: 0x002585E2
		[DefaultValue(false)]
		[Category("Data")]
		[NotifyParentProperty(true)]
		[Description("Gets or sets a value indicating whether automatic load-on-demand is enabled for the RadComboBox editor of this column.")]
		public bool AllowAutomaticLoadOnDemand
		{
			get
			{
				return (bool)(base.ViewState["AllowAutomaticLoadOnDemand"] ?? false);
			}
			set
			{
				base.ViewState["AllowAutomaticLoadOnDemand"] = value;
			}
		}

		// Token: 0x17003858 RID: 14424
		// (get) Token: 0x0600AE6D RID: 44653 RVA: 0x0025A3FA File Offset: 0x002585FA
		// (set) Token: 0x0600AE6E RID: 44654 RVA: 0x0025A41B File Offset: 0x0025861B
		[DefaultValue(false)]
		[NotifyParentProperty(true)]
		[Category("Behavior")]
		[Description("Gets or sets a value indicating whether the RadComboBox editor displays a More Results box. Setting this property to true requires EnableAutomaticLoadOnDemand to be set to true.")]
		public bool ShowMoreResultsBox
		{
			get
			{
				return (bool)(base.ViewState["ShowMoreResultsBox"] ?? false);
			}
			set
			{
				base.ViewState["ShowMoreResultsBox"] = value;
			}
		}

		// Token: 0x17003859 RID: 14425
		// (get) Token: 0x0600AE6F RID: 44655 RVA: 0x0025A433 File Offset: 0x00258633
		// (set) Token: 0x0600AE70 RID: 44656 RVA: 0x0025A454 File Offset: 0x00258654
		[DefaultValue(false)]
		[NotifyParentProperty(true)]
		[Category("Behavior")]
		[Description("Gets or sets a value indicating whether virtual scrolling is enabled for RadComboBox editor. Setting this property to true requires EnableAutomaticLoadOnDemand to be set to true")]
		public bool AllowVirtualScrolling
		{
			get
			{
				return (bool)(base.ViewState["AllowVirtualScrolling"] ?? false);
			}
			set
			{
				base.ViewState["AllowVirtualScrolling"] = value;
			}
		}

		// Token: 0x1700385A RID: 14426
		// (get) Token: 0x0600AE71 RID: 44657 RVA: 0x0025A46C File Offset: 0x0025866C
		// (set) Token: 0x0600AE72 RID: 44658 RVA: 0x0025A48D File Offset: 0x0025868D
		[Category("Behavior")]
		[DefaultValue(-1)]
		public int ItemsPerRequest
		{
			get
			{
				return (int)(base.ViewState["ItemsPerRequest"] ?? -1);
			}
			set
			{
				base.ViewState["ItemsPerRequest"] = value;
			}
		}

		// Token: 0x1700385B RID: 14427
		// (get) Token: 0x0600AE73 RID: 44659 RVA: 0x0025A4A8 File Offset: 0x002586A8
		// (set) Token: 0x0600AE74 RID: 44660 RVA: 0x0025A4D6 File Offset: 0x002586D6
		[NotifyParentProperty(true)]
		[DefaultValue(false)]
		public bool EnableEmptyListItem
		{
			get
			{
				object obj = base.ViewState["EnableEmptyListItem"];
				if (obj == null)
				{
					obj = false;
				}
				return (bool)obj;
			}
			set
			{
				base.ViewState["EnableEmptyListItem"] = value;
			}
		}

		// Token: 0x1700385C RID: 14428
		// (get) Token: 0x0600AE75 RID: 44661 RVA: 0x0025A4F0 File Offset: 0x002586F0
		// (set) Token: 0x0600AE76 RID: 44662 RVA: 0x0025A51D File Offset: 0x0025871D
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		public string EmptyListItemText
		{
			get
			{
				object obj = base.ViewState["EmptyListItemText"];
				if (obj == null)
				{
					obj = "";
				}
				return (string)obj;
			}
			set
			{
				base.ViewState["EmptyListItemText"] = value;
			}
		}

		// Token: 0x1700385D RID: 14429
		// (get) Token: 0x0600AE77 RID: 44663 RVA: 0x0025A530 File Offset: 0x00258730
		// (set) Token: 0x0600AE78 RID: 44664 RVA: 0x0025A55D File Offset: 0x0025875D
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		public string EmptyListItemValue
		{
			get
			{
				object obj = base.ViewState["EmptyListItemValue"];
				if (obj == null)
				{
					obj = "";
				}
				return (string)obj;
			}
			set
			{
				base.ViewState["EmptyListItemValue"] = value;
			}
		}

		// Token: 0x1700385E RID: 14430
		// (get) Token: 0x0600AE79 RID: 44665 RVA: 0x0025A570 File Offset: 0x00258770
		// (set) Token: 0x0600AE7A RID: 44666 RVA: 0x0025A59E File Offset: 0x0025879E
		[NotifyParentProperty(true)]
		[DefaultValue(typeof(GridDropDownColumnControlType), "RadComboBox")]
		[Description("Gets or sets the type of the dropdown control associated with the column.")]
		public GridDropDownColumnControlType DropDownControlType
		{
			get
			{
				object obj = base.ViewState["DropDownControlType"];
				return (GridDropDownColumnControlType)(obj ?? GridDropDownColumnControlType.RadComboBox);
			}
			set
			{
				base.ViewState["DropDownControlType"] = value;
			}
		}

		// Token: 0x1700385F RID: 14431
		// (get) Token: 0x0600AE7B RID: 44667 RVA: 0x0025A5B6 File Offset: 0x002587B6
		protected virtual DropDownList InnerList
		{
			get
			{
				if (this._innerList == null)
				{
					this._innerList = new DropDownList();
				}
				return this._innerList;
			}
		}

		// Token: 0x0600AE7C RID: 44668 RVA: 0x0025A5D4 File Offset: 0x002587D4
		protected virtual string GetTextFromInnerList(string value)
		{
			if (this.InnerList.Items.Count == 0)
			{
				this.InnerList.DataBind();
				if (this.EnableEmptyListItem && this.InnerList.Items.FindByValue(this.EmptyListItemValue) == null)
				{
					ListItem item = new ListItem(this.EmptyListItemText, this.EmptyListItemValue);
					this.InnerList.Items.Insert(0, item);
				}
			}
			ListItem listItem = this.InnerList.Items.FindByValue(value);
			if (listItem == null)
			{
				return string.Empty;
			}
			return listItem.Text;
		}

		// Token: 0x0600AE7D RID: 44669 RVA: 0x0025A668 File Offset: 0x00258868
		protected virtual void SetDropDownListDataSource(GridItem inItem)
		{
			this.DropDownListEditor.DataMember = (this.InnerList.DataMember = this.ListDataMember);
			if (!string.IsNullOrEmpty(this.ListDataMember) && this.DropDownListEditor.DataSource == null)
			{
				this.DropDownListEditor.DataSource = (this.InnerList.DataSource = inItem.OwnerTableView.DataSource);
			}
			else if (!string.IsNullOrEmpty(this.DataSourceID) && !base.DesignMode)
			{
				this.DropDownListEditor.DataSource = (this.InnerList.DataSource = DataSourceControlHelper.FindControl(inItem, this.DataSourceID));
			}
			else if (this.DropDownListEditor.DataSource != null)
			{
				this.InnerList.DataSource = this.DropDownListEditor.DataSource;
			}
			this.DropDownListEditor.DataTextField = (this.InnerList.DataTextField = this.ListTextField);
			this.DropDownListEditor.DataTextFormatString = (this.InnerList.DataTextFormatString = this.ListTextFormatString);
			this.DropDownListEditor.DataValueField = (this.InnerList.DataValueField = this.ListValueField);
		}

		// Token: 0x0600AE7E RID: 44670 RVA: 0x0025A794 File Offset: 0x00258994
		public override void InitializeCell(TableCell cell, int columnIndex, GridItem inItem)
		{
			base.InitializeCell(cell, columnIndex, inItem);
			if (!inItem.IsDataBound)
			{
				return;
			}
			this.SetDropDownListDataSource(inItem);
			if (inItem.IsInEditMode)
			{
				this.CurrentColumnEditor.InitializeInControl(cell);
				if (base.IsReadOnly(inItem))
				{
					this.FindOrCreateLiteral(cell, string.Format("ROLC_{0}", this.UniqueName), string.Empty);
				}
			}
			else
			{
				this.FindOrCreateLiteral(cell, string.Format("TXLC_{0}", this.UniqueName), string.Empty);
				this.FindOrCreateLiteral(cell, string.Format("VLLC_{0}", this.UniqueName), string.Empty);
			}
			inItem.CellDataBound += this.OnDataBindColumn;
		}

		// Token: 0x0600AE7F RID: 44671 RVA: 0x0025A844 File Offset: 0x00258A44
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
			if (!string.IsNullOrEmpty(this.DataField))
			{
				string text = "GridDropDownColumn";
				if (gridItem.IsInEditMode)
				{
					this.CurrentColumnEditor.InitializeFromControl(cell);
					if (!this.CurrentColumnEditor.IsInitialized)
					{
						return;
					}
				}
				if (base.DesignMode)
				{
					ArrayList arrayList = new ArrayList();
					arrayList.Add(new ListItem(text, "0"));
					this.DropDownListEditor.DataValueField = "Value";
					this.DropDownListEditor.DataTextField = "Text";
					this.DropDownListEditor.DataSource = arrayList;
					if (this.DropDownControlType == GridDropDownColumnControlType.RadComboBox && this.DropDownListEditor is GridDropDownListColumnEditor && ((GridDropDownListColumnEditor)this.DropDownListEditor).ComboBoxControl.EnableAutomaticLoadOnDemand)
					{
						((GridDropDownListColumnEditor)this.DropDownListEditor).ComboBoxControl.EnableAutomaticLoadOnDemand = false;
					}
					this.DropDownListEditor.DataBind();
					this.DropDownListEditor.SelectedValue = "0";
					if (!gridItem.IsInEditMode && this.DropDownListEditor.SelectedIndex >= 0)
					{
						cell.Text = HttpUtility.HtmlEncode(this.DropDownListEditor.SelectedText);
					}
					return;
				}
				text = this.ExtractValueFromDataItem(dataItem, text);
				if (base.Owner.EditMode != GridEditMode.Batch && this.EnableEmptyListItem)
				{
					if (this.DropDownControlType == GridDropDownColumnControlType.RadComboBox)
					{
						if (!this.AllowAutomaticLoadOnDemand && ((GridDropDownListColumnEditor)this.DropDownListEditor).ComboBoxControl.FindItemByValue(this.EmptyListItemValue) == null)
						{
							RadComboBoxItem item = new RadComboBoxItem(this.EmptyListItemText, this.EmptyListItemValue);
							((GridDropDownListColumnEditor)this.DropDownListEditor).ComboBoxControl.Items.Insert(0, item);
						}
					}
					else if (((GridDropDownListColumnEditor)this.DropDownListEditor).DropDownListControl.Items.FindByValue(this.EmptyListItemValue) == null)
					{
						ListItem item2 = new ListItem(this.EmptyListItemText, this.EmptyListItemValue);
						((GridDropDownListColumnEditor)this.DropDownListEditor).DropDownListControl.Items.Insert(0, item2);
					}
				}
				if (gridItem.IsInEditMode)
				{
					this.DropDownListEditor.SelectedValue = text;
					if (this.AllowAutomaticLoadOnDemand && this.DropDownControlType == GridDropDownColumnControlType.RadComboBox && this.DropDownListEditor is GridDropDownListColumnEditor)
					{
						((GridDropDownListColumnEditor)this.DropDownListEditor).SelectedText = this.GetTextFromInnerList(text);
					}
					if (base.IsReadOnly(gridItem) && this.DropDownListEditor is GridDropDownListColumnEditor)
					{
						((GridDropDownListColumnEditor)this.DropDownListEditor).DropDownListControl.Visible = false;
						((GridDropDownListColumnEditor)this.DropDownListEditor).ComboBoxControl.Visible = false;
						if (this.ColumnValidationSettings.EnableRequiredFieldValidation)
						{
							((GridDropDownListColumnEditor)this.CurrentColumnEditor).GetRequiredFieldValidator().Visible = false;
						}
						if (this.ColumnValidationSettings.EnableModelErrorMessageValidation)
						{
							((GridDropDownListColumnEditor)this.CurrentColumnEditor).GetModelErrorMessageValidator().Visible = false;
						}
						this.FindOrCreateLiteral(cell, string.Format("ROLC_{0}", this.UniqueName), this.DropDownListEditor.SelectedText);
						return;
					}
				}
				else
				{
					string textFromInnerList = this.GetTextFromInnerList(text);
					if (!string.IsNullOrEmpty(textFromInnerList))
					{
						cell.Text = HttpUtility.HtmlEncode(textFromInnerList);
						this.FindOrCreateLiteral(cell, string.Format("TXLC_{0}", this.UniqueName), textFromInnerList);
						this.FindOrCreateLiteral(cell, string.Format("VLLC_{0}", this.UniqueName), text).Visible = false;
						return;
					}
					this.FindOrCreateLiteral(cell, string.Format("ROLC_{0}", this.UniqueName), "&nbsp;");
					return;
				}
			}
			else if (!gridItem.IsInEditMode)
			{
				this.FindOrCreateLiteral(cell, string.Format("ROLC_{0}", this.UniqueName), "&nbsp;");
			}
		}

		// Token: 0x0600AE80 RID: 44672 RVA: 0x0025AC10 File Offset: 0x00258E10
		private Literal FindOrCreateLiteral(Control parent, string id, string text)
		{
			Literal literal = (Literal)parent.FindControl(id);
			if (literal == null)
			{
				literal = new Literal();
				literal.ID = id;
				parent.Controls.Add(literal);
			}
			literal.Text = text;
			return literal;
		}

		// Token: 0x0600AE81 RID: 44673 RVA: 0x0025AC50 File Offset: 0x00258E50
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

		// Token: 0x0600AE82 RID: 44674 RVA: 0x0025AD04 File Offset: 0x00258F04
		protected override string GenerateUniqueName()
		{
			return base.GenerateUniqueNameBase(this.DataField);
		}

		// Token: 0x0600AE83 RID: 44675 RVA: 0x0025AD12 File Offset: 0x00258F12
		protected override IGridColumnEditor CreateDefaultColumnEditor()
		{
			return new GridDropDownListColumnEditor(this);
		}

		// Token: 0x17003860 RID: 14432
		// (get) Token: 0x0600AE84 RID: 44676 RVA: 0x0025AD1A File Offset: 0x00258F1A
		private GridDropDownColumnEditor DropDownListEditor
		{
			get
			{
				return this.CurrentColumnEditor as GridDropDownColumnEditor;
			}
		}

		// Token: 0x0600AE85 RID: 44677 RVA: 0x0025AD27 File Offset: 0x00258F27
		protected override void ColumnEditorChange(IGridColumnEditor newValue)
		{
			if (!(newValue is GridDropDownColumnEditor))
			{
				throw new GridColumnEditorException(this.ToString() + " accepts only editor of type: " + typeof(GridDropDownColumnEditor).ToString());
			}
			base.ColumnEditorChange(newValue);
		}

		// Token: 0x0600AE86 RID: 44678 RVA: 0x0025AD5D File Offset: 0x00258F5D
		public override bool IsBoundToFieldName(string name)
		{
			return string.Compare(this.DataField, name, true) == 0;
		}

		// Token: 0x17003861 RID: 14433
		// (get) Token: 0x0600AE87 RID: 44679 RVA: 0x0025AD6F File Offset: 0x00258F6F
		public override bool IsEditable
		{
			get
			{
				return !this.ReadOnly;
			}
		}

		// Token: 0x0600AE88 RID: 44680 RVA: 0x0025AD7A File Offset: 0x00258F7A
		internal override string GetSortExpression()
		{
			if (string.IsNullOrEmpty(this.SortExpression) && !string.IsNullOrEmpty(this.DataField) && this.AllowSorting)
			{
				return this.DataField;
			}
			return base.GetSortExpression();
		}

		// Token: 0x0600AE89 RID: 44681 RVA: 0x0025ADAB File Offset: 0x00258FAB
		public override bool SupportsFiltering()
		{
			return this.AllowFiltering;
		}

		// Token: 0x17003862 RID: 14434
		// (get) Token: 0x0600AE8A RID: 44682 RVA: 0x0025ADB3 File Offset: 0x00258FB3
		protected override bool Sortable
		{
			get
			{
				return this.AllowSorting;
			}
		}

		// Token: 0x0600AE8B RID: 44683 RVA: 0x0025ADBB File Offset: 0x00258FBB
		protected override string GetFilterDataField()
		{
			return this.DataField;
		}

		// Token: 0x0600AE8C RID: 44684 RVA: 0x0025ADC4 File Offset: 0x00258FC4
		public override void FillValues(IDictionary newValues, GridEditableItem editableItem)
		{
			if (editableItem.IsInEditMode)
			{
				GridDropDownColumnEditor gridDropDownColumnEditor = (GridDropDownColumnEditor)editableItem.EditManager.GetColumnEditor(this);
				if (gridDropDownColumnEditor.SelectedIndex != -1 || !string.IsNullOrEmpty(gridDropDownColumnEditor.SelectedText))
				{
					newValues[this.DataField] = base.ConvertValueIfEmpty(gridDropDownColumnEditor.SelectedValue);
					return;
				}
				newValues[this.DataField] = null;
				return;
			}
			else
			{
				TableCell tableCell = editableItem[this];
				if (tableCell.Controls.Count > 1)
				{
					Literal literal = (Literal)tableCell.Controls[tableCell.Controls.Count - 1];
					newValues[this.DataField] = literal.Text;
					return;
				}
				newValues[this.DataField] = null;
				return;
			}
		}

		// Token: 0x0600AE8D RID: 44685 RVA: 0x0025AE7C File Offset: 0x0025907C
		public override IDictionary GetCustomPropertyDataFields(object dataItemInstance)
		{
			Hashtable hashtable = new Hashtable();
			GridColumn.AddSubPropertyFieldInfo(hashtable, this.DataField, dataItemInstance);
			return hashtable;
		}

		// Token: 0x0600AE8E RID: 44686 RVA: 0x0025AEA0 File Offset: 0x002590A0
		public override GridColumn Clone()
		{
			GridDropDownColumn gridDropDownColumn = new GridDropDownColumn();
			gridDropDownColumn.CopyBaseProperties(this);
			return gridDropDownColumn;
		}

		// Token: 0x0600AE8F RID: 44687 RVA: 0x0025AEBC File Offset: 0x002590BC
		protected override void CopyBaseProperties(GridColumn fromColumn)
		{
			base.CopyBaseProperties(fromColumn);
			GridDropDownColumn gridDropDownColumn = (GridDropDownColumn)fromColumn;
			this.DataField = gridDropDownColumn.DataField;
			this.ListValueField = gridDropDownColumn.ListValueField;
			this.ListDataMember = gridDropDownColumn.ListDataMember;
			this.ListTextField = gridDropDownColumn.ListTextField;
			this.ListTextFormatString = gridDropDownColumn.ListTextFormatString;
			this.DataSourceID = gridDropDownColumn.DataSourceID;
			this.EnableEmptyListItem = gridDropDownColumn.EnableEmptyListItem;
			this.EmptyListItemText = gridDropDownColumn.EmptyListItemText;
			this.EmptyListItemValue = gridDropDownColumn.EmptyListItemValue;
			this.AutoPostBackOnFilter = gridDropDownColumn.AutoPostBackOnFilter;
			this.DropDownControlType = gridDropDownColumn.DropDownControlType;
			this.AllowAutomaticLoadOnDemand = gridDropDownColumn.AllowAutomaticLoadOnDemand;
			this.ShowMoreResultsBox = gridDropDownColumn.ShowMoreResultsBox;
			this.AllowVirtualScrolling = gridDropDownColumn.AllowVirtualScrolling;
			this.ItemsPerRequest = gridDropDownColumn.ItemsPerRequest;
			this.ColumnValidationSettings.CopyBaseProperties(gridDropDownColumn.ColumnValidationSettings);
		}

		// Token: 0x04002E0F RID: 11791
		private GridColumnValidationSettings _columnValidationSettings;

		// Token: 0x04002E10 RID: 11792
		private DropDownList _innerList;
	}
}
