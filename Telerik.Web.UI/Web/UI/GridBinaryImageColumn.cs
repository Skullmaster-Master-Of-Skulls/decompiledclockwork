using System;
using System.Collections;
using System.ComponentModel;
using System.Reflection;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x020018FA RID: 6394
	public class GridBinaryImageColumn : GridEditableColumn
	{
		// Token: 0x17004A2E RID: 18990
		// (get) Token: 0x0600F676 RID: 63094 RVA: 0x0037ED20 File Offset: 0x0037CF20
		// (set) Token: 0x0600F677 RID: 63095 RVA: 0x0037ED4D File Offset: 0x0037CF4D
		[NotifyParentProperty(true)]
		[Description("DataField")]
		[Category("Data")]
		[DefaultValue("")]
		public virtual string DataField
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

		// Token: 0x17004A2F RID: 18991
		// (get) Token: 0x0600F678 RID: 63096 RVA: 0x0037ED70 File Offset: 0x0037CF70
		// (set) Token: 0x0600F679 RID: 63097 RVA: 0x0037ED9D File Offset: 0x0037CF9D
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		[Category("Behavior")]
		[Localizable(true)]
		[Description("Gets or sets a url, specifying the location of a default image which to be loaded if there is no data for the binary image ")]
		public string DefaultImageUrl
		{
			get
			{
				object obj = base.ViewState["DefaultImageUrl"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["DefaultImageUrl"] = value;
				this.OnColumnChanged();
			}
		}

		// Token: 0x17004A30 RID: 18992
		// (get) Token: 0x0600F67A RID: 63098 RVA: 0x0037EDB8 File Offset: 0x0037CFB8
		// (set) Token: 0x0600F67B RID: 63099 RVA: 0x0037EDE5 File Offset: 0x0037CFE5
		[Category("Behavior")]
		[NotifyParentProperty(true)]
		[Localizable(true)]
		[Description("ImageColumn_AlternateText")]
		[DefaultValue("")]
		public virtual string AlternateText
		{
			get
			{
				object obj = base.ViewState["AlternateText"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["AlternateText"] = value;
				this.OnColumnChanged();
			}
		}

		// Token: 0x17004A31 RID: 18993
		// (get) Token: 0x0600F67C RID: 63100 RVA: 0x0037EE00 File Offset: 0x0037D000
		// (set) Token: 0x0600F67D RID: 63101 RVA: 0x0037EE29 File Offset: 0x0037D029
		[DefaultValue(typeof(ImageAlign), "NotSet")]
		[Description("Gets or sets the GridBinaryImage.ImageAlign property for each cell")]
		[NotifyParentProperty(true)]
		[Category("Behavior")]
		public virtual ImageAlign ImageAlign
		{
			get
			{
				object obj = base.ViewState["ImageAlign"];
				if (obj != null)
				{
					return (ImageAlign)obj;
				}
				return ImageAlign.NotSet;
			}
			set
			{
				base.ViewState["ImageAlign"] = value;
				this.OnColumnChanged();
			}
		}

		// Token: 0x17004A32 RID: 18994
		// (get) Token: 0x0600F67E RID: 63102 RVA: 0x0037EE48 File Offset: 0x0037D048
		// (set) Token: 0x0600F67F RID: 63103 RVA: 0x0037EE7A File Offset: 0x0037D07A
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		[Category("Behavior")]
		[Localizable(true)]
		[Description("ImageColumn's image width")]
		public Unit ImageWidth
		{
			get
			{
				object obj = base.ViewState["ImageWidth"];
				if (obj == null)
				{
					obj = Unit.Empty;
				}
				return (Unit)obj;
			}
			set
			{
				base.ViewState["ImageWidth"] = value;
			}
		}

		// Token: 0x17004A33 RID: 18995
		// (get) Token: 0x0600F680 RID: 63104 RVA: 0x0037EE94 File Offset: 0x0037D094
		// (set) Token: 0x0600F681 RID: 63105 RVA: 0x0037EEC6 File Offset: 0x0037D0C6
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		[Category("Behavior")]
		[Localizable(true)]
		[Description("ImageColumn's image height")]
		public Unit ImageHeight
		{
			get
			{
				object obj = base.ViewState["ImageHeight"];
				if (obj == null)
				{
					obj = Unit.Empty;
				}
				return (Unit)obj;
			}
			set
			{
				base.ViewState["ImageHeight"] = value;
			}
		}

		// Token: 0x17004A34 RID: 18996
		// (get) Token: 0x0600F682 RID: 63106 RVA: 0x0037EEE0 File Offset: 0x0037D0E0
		// (set) Token: 0x0600F683 RID: 63107 RVA: 0x0037EF0D File Offset: 0x0037D10D
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		[Localizable(true)]
		[Description("Get or set the name of the file which will appear inside of the SaveAs browser dialog")]
		[Category("Behavior")]
		public virtual string SavedImageName
		{
			get
			{
				object obj = base.ViewState["SavedImageName"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["SavedImageName"] = value;
			}
		}

		// Token: 0x17004A35 RID: 18997
		// (get) Token: 0x0600F684 RID: 63108 RVA: 0x0037EF20 File Offset: 0x0037D120
		// (set) Token: 0x0600F685 RID: 63109 RVA: 0x0037EF49 File Offset: 0x0037D149
		[Description("Specifies if the HTML image element's dimensions are inferred from image's binary data")]
		[Category("Behavior")]
		[DefaultValue(true)]
		public bool AutoAdjustImageControlSize
		{
			get
			{
				object obj = base.ViewState["AutoAdjustImageControlSize"];
				return obj == null || (bool)obj;
			}
			set
			{
				base.ViewState["AutoAdjustImageControlSize"] = value;
			}
		}

		// Token: 0x17004A36 RID: 18998
		// (get) Token: 0x0600F686 RID: 63110 RVA: 0x0037EF64 File Offset: 0x0037D164
		// (set) Token: 0x0600F687 RID: 63111 RVA: 0x0037EF91 File Offset: 0x0037D191
		[DefaultValue("")]
		[Category("Data")]
		[Description("ImageColumn_DataAlternateTextField")]
		[NotifyParentProperty(true)]
		public virtual string DataAlternateTextField
		{
			get
			{
				object obj = base.ViewState["DataAlternateTextField"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["DataAlternateTextField"] = value;
				base.UpdateUniqueNameIfDefault(value);
				this.OnColumnChanged();
			}
		}

		// Token: 0x17004A37 RID: 18999
		// (get) Token: 0x0600F688 RID: 63112 RVA: 0x0037EFB4 File Offset: 0x0037D1B4
		// (set) Token: 0x0600F689 RID: 63113 RVA: 0x0037EFE1 File Offset: 0x0037D1E1
		[Category("Data")]
		[DefaultValue("")]
		[Description("The formatting applied to the value bound to the AlternateText property.")]
		[NotifyParentProperty(true)]
		[Localizable(true)]
		public virtual string DataAlternateTextFormatString
		{
			get
			{
				object obj = base.ViewState["DataAlternateTextFormatString"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["DataAlternateTextFormatString"] = value;
				this.OnColumnChanged();
			}
		}

		// Token: 0x17004A38 RID: 19000
		// (get) Token: 0x0600F68A RID: 63114 RVA: 0x0037EFFC File Offset: 0x0037D1FC
		// (set) Token: 0x0600F68B RID: 63115 RVA: 0x0037F025 File Offset: 0x0037D225
		[NotifyParentProperty(true)]
		[Description("UploadControlType")]
		[Category("Behavior")]
		[DefaultValue(GridUploadControlType.RadUpload)]
		public virtual GridUploadControlType UploadControlType
		{
			get
			{
				object obj = base.ViewState["UploadControlType"];
				if (obj != null)
				{
					return (GridUploadControlType)obj;
				}
				return GridUploadControlType.RadUpload;
			}
			set
			{
				base.ViewState["UploadControlType"] = value;
				this.OnColumnChanged();
			}
		}

		// Token: 0x17004A39 RID: 19001
		// (get) Token: 0x0600F68C RID: 63116 RVA: 0x0037F044 File Offset: 0x0037D244
		// (set) Token: 0x0600F68D RID: 63117 RVA: 0x0037F06D File Offset: 0x0037D26D
		[Category("Behavior")]
		[Description("Setting the value to true will pass the old binary image data to the data source so the image could be persisted and not deleted.")]
		[NotifyParentProperty(true)]
		[DefaultValue(false)]
		public virtual bool PersistBinaryDataOnEdit
		{
			get
			{
				object obj = base.ViewState["PersistBinaryDataOnEdit"];
				return obj != null && (bool)obj;
			}
			set
			{
				base.ViewState["PersistBinaryDataOnEdit"] = value;
				this.OnColumnChanged();
			}
		}

		// Token: 0x0600F68E RID: 63118 RVA: 0x0037F08C File Offset: 0x0037D28C
		public override void InitializeCell(TableCell cell, int columnIndex, GridItem inItem)
		{
			base.InitializeCell(cell, columnIndex, inItem);
			if (inItem.IsDataBound)
			{
				RadBinaryImage radBinaryImage = new RadBinaryImage();
				radBinaryImage.ID = string.Format("img{0}", this.UniqueName);
				radBinaryImage.AlternateText = this.AlternateText;
				radBinaryImage.ToolTip = this.AlternateText;
				radBinaryImage.ImageAlign = this.ImageAlign;
				radBinaryImage.Width = this.ImageWidth;
				radBinaryImage.Height = this.ImageHeight;
				radBinaryImage.AutoAdjustImageControlSize = this.AutoAdjustImageControlSize;
				radBinaryImage.PersistDataIfNotVisible = true;
				radBinaryImage.EnableAriaSupport = base.Owner.OwnerGrid.EnableAriaSupport;
				radBinaryImage.SavedImageName = this.SavedImageName;
				if (this.ImageWidth != Unit.Empty && this.ImageHeight != Unit.Empty && this.ResizeMode != BinaryImageResizeMode.None && this.ImageWidth.Type == UnitType.Pixel && this.ImageHeight.Type == UnitType.Pixel)
				{
					radBinaryImage.Filters.Add(new BinaryImageTransformationFilter
					{
						Height = (int)this.ImageHeight.Value,
						Width = (int)this.ImageWidth.Value,
						Mode = this.ResizeMode
					});
				}
				if (!string.IsNullOrEmpty(this.DataField) || !string.IsNullOrEmpty(this.DataAlternateTextField))
				{
					inItem.CellDataBound += this.OnCellDataBinding;
				}
				if (inItem.IsInEditMode && !base.IsReadOnly(inItem))
				{
					this.CurrentColumnEditor.InitializeInControl(cell);
					return;
				}
				cell.Controls.Add(radBinaryImage);
			}
		}

		// Token: 0x0600F68F RID: 63119 RVA: 0x0037F22C File Offset: 0x0037D42C
		protected virtual void OnCellDataBinding(object sender, GridCellDataBoundEventArgs args)
		{
			if (args.Column != this)
			{
				return;
			}
			GridItem gridItem = (GridItem)sender;
			object dataItem = gridItem.DataItem;
			if (dataItem == null)
			{
				return;
			}
			if (!string.IsNullOrEmpty(this.DataField) || !string.IsNullOrEmpty(this.DataAlternateTextField))
			{
				TableCell cell = args.Cell;
				if (cell == null)
				{
					return;
				}
				if (!gridItem.IsDataBound)
				{
					return;
				}
				if (!base.DesignMode)
				{
					if (gridItem.IsInEditMode && !base.IsReadOnly(gridItem))
					{
						if (this.PersistBinaryDataOnEdit && gridItem.ItemIndex >= 0 && gridItem.OwnerTableView.DataKeyValues.Count > gridItem.ItemIndex)
						{
							gridItem.OwnerTableView.PopulateDataKey(dataItem, gridItem.OwnerTableView.DataKeyValues[gridItem.ItemIndex], this.DataField);
						}
						this.CurrentColumnEditor.InitializeFromControl(cell);
						if (!this.CurrentColumnEditor.IsInitialized)
						{
							return;
						}
					}
					else
					{
						RadBinaryImage binaryImage = (RadBinaryImage)cell.Controls[0];
						if (!string.IsNullOrEmpty(this.DataField))
						{
							this.PopulateImageContol(dataItem, binaryImage);
						}
						if (!string.IsNullOrEmpty(this.DataAlternateTextField))
						{
							this.SetTextToImageControl(dataItem, binaryImage);
						}
					}
				}
			}
		}

		// Token: 0x0600F690 RID: 63120 RVA: 0x0037F34C File Offset: 0x0037D54C
		private void PopulateImageContol(object dataItem, RadBinaryImage binaryImage)
		{
			byte[] array = this.ExtractValueFromDataItem<byte[]>(dataItem, this.DataField);
			if ((array == null || array.Length == 0) && !string.IsNullOrEmpty(this.DefaultImageUrl))
			{
				binaryImage.ImageUrl = this.DefaultImageUrl;
				return;
			}
			binaryImage.DataValue = array;
		}

		// Token: 0x0600F691 RID: 63121 RVA: 0x0037F390 File Offset: 0x0037D590
		private void SetTextToImageControl(object dataItem, RadBinaryImage binaryImage)
		{
			if (!string.IsNullOrEmpty(this.DataAlternateTextField))
			{
				MethodInfo methodInfo = typeof(GridBinaryImageColumn).GetMethod("ExtractValueFromDataItem", BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.NonPublic);
				methodInfo = methodInfo.MakeGenericMethod(new Type[]
				{
					base.DataType
				});
				string text = methodInfo.Invoke(this, new object[]
				{
					dataItem,
					this.DataAlternateTextField
				}).ToString();
				text = (text ?? string.Empty);
				string text2 = text;
				if (!string.IsNullOrEmpty(this.DataAlternateTextFormatString))
				{
					text2 = string.Format(this.DataAlternateTextFormatString, text);
				}
				binaryImage.AlternateText = text2;
				binaryImage.ToolTip = text2;
			}
		}

		// Token: 0x0600F692 RID: 63122 RVA: 0x0037F438 File Offset: 0x0037D638
		private T ExtractValueFromDataItem<T>(object dataItem, string dataFieldName)
		{
			object obj = null;
			if (dataFieldName.IndexOf(".") > -1)
			{
				try
				{
					obj = DataBinder.GetPropertyValue(dataItem, dataFieldName);
					goto IL_44;
				}
				catch
				{
					try
					{
						obj = DataBinder.Eval(dataItem, dataFieldName);
					}
					catch
					{
						if (obj != null && !GridBaseDataList.IsBindableType(obj.GetType()))
						{
							obj = null;
						}
					}
					goto IL_44;
				}
			}
			obj = DataBinder.Eval(dataItem, dataFieldName);
			IL_44:
			if (obj == null)
			{
				PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(dataItem).Find(dataFieldName, true);
				if (propertyDescriptor != null)
				{
					obj = propertyDescriptor.GetValue(dataItem);
				}
			}
			if (obj == null || !(obj is T))
			{
				obj = default(T);
			}
			return (T)((object)obj);
		}

		// Token: 0x0600F693 RID: 63123 RVA: 0x0037F4E4 File Offset: 0x0037D6E4
		public override bool IsBoundToFieldName(string name)
		{
			return string.Compare(this.DataField, name, true) == 0 || string.Compare(this.DataAlternateTextField, name, true) == 0;
		}

		// Token: 0x0600F694 RID: 63124 RVA: 0x0037F507 File Offset: 0x0037D707
		protected override string GenerateUniqueName()
		{
			return base.GenerateUniqueNameBase(this.DataField);
		}

		// Token: 0x0600F695 RID: 63125 RVA: 0x0037F515 File Offset: 0x0037D715
		public override bool SupportsFiltering()
		{
			return this.AllowFiltering;
		}

		// Token: 0x0600F696 RID: 63126 RVA: 0x0037F51D File Offset: 0x0037D71D
		protected override string GetFilterDataField()
		{
			return this.DataAlternateTextField;
		}

		// Token: 0x0600F697 RID: 63127 RVA: 0x0037F528 File Offset: 0x0037D728
		public override void FillValues(IDictionary newValues, GridEditableItem editableItem)
		{
			if (editableItem.IsInEditMode)
			{
				GridBinaryImageColumnEditor gridBinaryImageColumnEditor = (GridBinaryImageColumnEditor)editableItem.EditManager.GetColumnEditor(this);
				if ((this.UploadControlType == GridUploadControlType.RadUpload && gridBinaryImageColumnEditor.RadUploadControl.UploadedFiles.Count > 0) || (this.UploadControlType == GridUploadControlType.RadAsyncUpload && gridBinaryImageColumnEditor.RadAsyncUploadControl.UploadedFiles.Count > 0))
				{
					newValues[this.DataField] = gridBinaryImageColumnEditor.UploadedFileContent;
					return;
				}
				if (this.PersistBinaryDataOnEdit && !(editableItem is IGridInsertItem))
				{
					newValues[this.DataField] = editableItem.GetDataKeyValue(this.DataField);
					return;
				}
				newValues[this.DataField] = null;
			}
		}

		// Token: 0x0600F698 RID: 63128 RVA: 0x0037F5D2 File Offset: 0x0037D7D2
		protected override IGridColumnEditor CreateDefaultColumnEditor()
		{
			return new GridBinaryImageColumnEditor(this);
		}

		// Token: 0x0600F699 RID: 63129 RVA: 0x0037F5DC File Offset: 0x0037D7DC
		public override GridColumn Clone()
		{
			GridBinaryImageColumn gridBinaryImageColumn = new GridBinaryImageColumn();
			gridBinaryImageColumn.CopyBaseProperties(this);
			return gridBinaryImageColumn;
		}

		// Token: 0x0600F69A RID: 63130 RVA: 0x0037F5F7 File Offset: 0x0037D7F7
		protected override void ColumnEditorChange(IGridColumnEditor newValue)
		{
			if (!(newValue is GridBinaryImageColumnEditor))
			{
				throw new GridColumnEditorException(this.ToString() + " accepts only editor of type: " + typeof(GridBinaryImageColumnEditor).ToString());
			}
			base.ColumnEditorChange(newValue);
		}

		// Token: 0x17004A3A RID: 19002
		// (get) Token: 0x0600F69B RID: 63131 RVA: 0x0037F62D File Offset: 0x0037D82D
		public override bool IsEditable
		{
			get
			{
				return !this.ReadOnly;
			}
		}

		// Token: 0x17004A3B RID: 19003
		// (get) Token: 0x0600F69C RID: 63132 RVA: 0x0037F638 File Offset: 0x0037D838
		// (set) Token: 0x0600F69D RID: 63133 RVA: 0x0037F661 File Offset: 0x0037D861
		[NotifyParentProperty(true)]
		[Category("Behavior")]
		[Description("Gets or sets the GridBinaryImage.ResizeMode property for each cell.")]
		[Localizable(true)]
		[DefaultValue(typeof(BinaryImageResizeMode), "None")]
		public virtual BinaryImageResizeMode ResizeMode
		{
			get
			{
				object obj = base.ViewState["ResizeMode"];
				if (obj != null)
				{
					return (BinaryImageResizeMode)obj;
				}
				return BinaryImageResizeMode.None;
			}
			set
			{
				base.ViewState["ResizeMode"] = value;
			}
		}

		// Token: 0x17004A3C RID: 19004
		// (get) Token: 0x0600F69E RID: 63134 RVA: 0x0037F679 File Offset: 0x0037D879
		protected override bool Sortable
		{
			get
			{
				return this.AllowSorting;
			}
		}

		// Token: 0x0600F69F RID: 63135 RVA: 0x0037F681 File Offset: 0x0037D881
		internal override string GetSortExpression()
		{
			if (string.IsNullOrEmpty(this.SortExpression) && !string.IsNullOrEmpty(this.DataAlternateTextField) && this.AllowSorting)
			{
				return this.DataAlternateTextField;
			}
			return base.GetSortExpression();
		}

		// Token: 0x0600F6A0 RID: 63136 RVA: 0x0037F6B4 File Offset: 0x0037D8B4
		protected override void CopyBaseProperties(GridColumn fromColumn)
		{
			base.CopyBaseProperties(fromColumn);
			GridBinaryImageColumn gridBinaryImageColumn = (GridBinaryImageColumn)fromColumn;
			this.AlternateText = gridBinaryImageColumn.AlternateText;
			this.ImageWidth = gridBinaryImageColumn.ImageWidth;
			this.ImageHeight = gridBinaryImageColumn.ImageHeight;
			this.ImageAlign = gridBinaryImageColumn.ImageAlign;
			this.DataAlternateTextField = gridBinaryImageColumn.DataAlternateTextField;
			this.DataAlternateTextFormatString = gridBinaryImageColumn.DataAlternateTextFormatString;
			this.DataField = gridBinaryImageColumn.DataField;
			this.ResizeMode = gridBinaryImageColumn.ResizeMode;
			this.DefaultImageUrl = gridBinaryImageColumn.DefaultImageUrl;
			this.SavedImageName = gridBinaryImageColumn.SavedImageName;
			this.AutoAdjustImageControlSize = gridBinaryImageColumn.AutoAdjustImageControlSize;
		}
	}
}
