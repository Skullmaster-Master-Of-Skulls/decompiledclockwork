using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing.Design;
using System.Globalization;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000444 RID: 1092
	public class ImageField : DataControlField
	{
		// Token: 0x17000F5C RID: 3932
		// (get) Token: 0x060034D9 RID: 13529 RVA: 0x000AB874 File Offset: 0x000A9A74
		// (set) Token: 0x060034DA RID: 13530 RVA: 0x000AB8A1 File Offset: 0x000A9AA1
		[Localizable(true)]
		[WebCategory("Appearance")]
		[DefaultValue("")]
		[WebSysDescription("ImageField_AlternateText")]
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
				if (!object.Equals(value, base.ViewState["AlternateText"]))
				{
					base.ViewState["AlternateText"] = value;
					this.OnFieldChanged();
				}
			}
		}

		// Token: 0x17000F5D RID: 3933
		// (get) Token: 0x060034DB RID: 13531 RVA: 0x000AB8D4 File Offset: 0x000A9AD4
		// (set) Token: 0x060034DC RID: 13532 RVA: 0x00086C49 File Offset: 0x00084E49
		[WebCategory("Behavior")]
		[DefaultValue(true)]
		[WebSysDescription("ImageField_ConvertEmptyStringToNull")]
		public virtual bool ConvertEmptyStringToNull
		{
			get
			{
				object obj = base.ViewState["ConvertEmptyStringToNull"];
				return obj == null || (bool)obj;
			}
			set
			{
				base.ViewState["ConvertEmptyStringToNull"] = value;
			}
		}

		// Token: 0x17000F5E RID: 3934
		// (get) Token: 0x060034DD RID: 13533 RVA: 0x000AB900 File Offset: 0x000A9B00
		// (set) Token: 0x060034DE RID: 13534 RVA: 0x000AB92D File Offset: 0x000A9B2D
		[WebCategory("Data")]
		[DefaultValue("")]
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[WebSysDescription("ImageField_DataAlternateTextField")]
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
				if (!object.Equals(value, base.ViewState["DataAlternateTextField"]))
				{
					base.ViewState["DataAlternateTextField"] = value;
					this.OnFieldChanged();
				}
			}
		}

		// Token: 0x17000F5F RID: 3935
		// (get) Token: 0x060034DF RID: 13535 RVA: 0x000AB960 File Offset: 0x000A9B60
		// (set) Token: 0x060034E0 RID: 13536 RVA: 0x000AB98D File Offset: 0x000A9B8D
		[WebCategory("Data")]
		[DefaultValue("")]
		[WebSysDescription("ImageField_DataAlternateTextFormatString")]
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
				if (!object.Equals(value, base.ViewState["DataAlternateTextFormatString"]))
				{
					base.ViewState["DataAlternateTextFormatString"] = value;
					this.OnFieldChanged();
				}
			}
		}

		// Token: 0x17000F60 RID: 3936
		// (get) Token: 0x060034E1 RID: 13537 RVA: 0x000AB9C0 File Offset: 0x000A9BC0
		// (set) Token: 0x060034E2 RID: 13538 RVA: 0x000ABA08 File Offset: 0x000A9C08
		[WebCategory("Data")]
		[DefaultValue("")]
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[WebSysDescription("ImageField_ImageUrlField")]
		public virtual string DataImageUrlField
		{
			get
			{
				if (this._dataField == null)
				{
					object obj = base.ViewState["DataImageUrlField"];
					if (obj != null)
					{
						this._dataField = (string)obj;
					}
					else
					{
						this._dataField = string.Empty;
					}
				}
				return this._dataField;
			}
			set
			{
				if (!object.Equals(value, base.ViewState["DataImageUrlField"]))
				{
					base.ViewState["DataImageUrlField"] = value;
					this._dataField = value;
					this.OnFieldChanged();
				}
			}
		}

		// Token: 0x17000F61 RID: 3937
		// (get) Token: 0x060034E3 RID: 13539 RVA: 0x000ABA40 File Offset: 0x000A9C40
		// (set) Token: 0x060034E4 RID: 13540 RVA: 0x000ABA6D File Offset: 0x000A9C6D
		[WebCategory("Data")]
		[DefaultValue("")]
		[WebSysDescription("ImageField_ImageUrlFormatString")]
		public virtual string DataImageUrlFormatString
		{
			get
			{
				object obj = base.ViewState["DataImageUrlFormatString"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				if (!object.Equals(value, base.ViewState["DataImageUrlFormatString"]))
				{
					base.ViewState["DataImageUrlFormatString"] = value;
					this.OnFieldChanged();
				}
			}
		}

		// Token: 0x17000F62 RID: 3938
		// (get) Token: 0x060034E5 RID: 13541 RVA: 0x000ABAA0 File Offset: 0x000A9CA0
		// (set) Token: 0x060034E6 RID: 13542 RVA: 0x00086F15 File Offset: 0x00085115
		[Localizable(true)]
		[WebCategory("Behavior")]
		[DefaultValue("")]
		[WebSysDescription("BoundField_NullDisplayText")]
		public virtual string NullDisplayText
		{
			get
			{
				object obj = base.ViewState["NullDisplayText"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				if (!object.Equals(value, base.ViewState["NullDisplayText"]))
				{
					base.ViewState["NullDisplayText"] = value;
					this.OnFieldChanged();
				}
			}
		}

		// Token: 0x17000F63 RID: 3939
		// (get) Token: 0x060034E7 RID: 13543 RVA: 0x000ABAD0 File Offset: 0x000A9CD0
		// (set) Token: 0x060034E8 RID: 13544 RVA: 0x000ABAFD File Offset: 0x000A9CFD
		[WebCategory("Behavior")]
		[DefaultValue("")]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[UrlProperty]
		[WebSysDescription("ImageField_NullImageUrl")]
		public virtual string NullImageUrl
		{
			get
			{
				object obj = base.ViewState["NullImageUrl"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				if (!object.Equals(value, base.ViewState["NullImageUrl"]))
				{
					base.ViewState["NullImageUrl"] = value;
					this.OnFieldChanged();
				}
			}
		}

		// Token: 0x17000F64 RID: 3940
		// (get) Token: 0x060034E9 RID: 13545 RVA: 0x000ABB30 File Offset: 0x000A9D30
		// (set) Token: 0x060034EA RID: 13546 RVA: 0x000ABB5C File Offset: 0x000A9D5C
		[WebCategory("Behavior")]
		[DefaultValue(false)]
		[WebSysDescription("ImageField_ReadOnly")]
		public virtual bool ReadOnly
		{
			get
			{
				object obj = base.ViewState["ReadOnly"];
				return obj != null && (bool)obj;
			}
			set
			{
				object obj = base.ViewState["ReadOnly"];
				if (obj == null || (bool)obj != value)
				{
					base.ViewState["ReadOnly"] = value;
					this.OnFieldChanged();
				}
			}
		}

		// Token: 0x060034EB RID: 13547 RVA: 0x000ABBA4 File Offset: 0x000A9DA4
		protected override void CopyProperties(DataControlField newField)
		{
			((ImageField)newField).AlternateText = this.AlternateText;
			((ImageField)newField).ConvertEmptyStringToNull = this.ConvertEmptyStringToNull;
			((ImageField)newField).DataAlternateTextField = this.DataAlternateTextField;
			((ImageField)newField).DataAlternateTextFormatString = this.DataAlternateTextFormatString;
			((ImageField)newField).DataImageUrlField = this.DataImageUrlField;
			((ImageField)newField).DataImageUrlFormatString = this.DataImageUrlFormatString;
			((ImageField)newField).NullDisplayText = this.NullDisplayText;
			((ImageField)newField).NullImageUrl = this.NullImageUrl;
			((ImageField)newField).ReadOnly = this.ReadOnly;
			base.CopyProperties(newField);
		}

		// Token: 0x060034EC RID: 13548 RVA: 0x000ABC51 File Offset: 0x000A9E51
		protected override DataControlField CreateField()
		{
			return new ImageField();
		}

		// Token: 0x060034ED RID: 13549 RVA: 0x000ABC58 File Offset: 0x000A9E58
		public override void ExtractValuesFromCell(IOrderedDictionary dictionary, DataControlFieldCell cell, DataControlRowState rowState, bool includeReadOnly)
		{
			string dataImageUrlField = this.DataImageUrlField;
			object obj = null;
			bool flag = false;
			if ((rowState & DataControlRowState.Insert) != DataControlRowState.Normal && !this.InsertVisible)
			{
				return;
			}
			if (cell.Controls.Count == 0)
			{
				return;
			}
			Control control = cell.Controls[0];
			Image image = control as Image;
			if (image != null)
			{
				if (includeReadOnly)
				{
					flag = true;
					if (image.Visible)
					{
						obj = image.ImageUrl;
					}
				}
			}
			else
			{
				TextBox textBox = control as TextBox;
				if (textBox != null)
				{
					obj = textBox.Text;
					flag = true;
				}
			}
			if (obj != null || flag)
			{
				if (this.ConvertEmptyStringToNull && obj is string && ((string)obj).Length == 0)
				{
					obj = null;
				}
				if (dictionary.Contains(dataImageUrlField))
				{
					dictionary[dataImageUrlField] = obj;
					return;
				}
				dictionary.Add(dataImageUrlField, obj);
			}
		}

		// Token: 0x060034EE RID: 13550 RVA: 0x000ABD18 File Offset: 0x000A9F18
		protected virtual string FormatImageUrlValue(object dataValue)
		{
			string result = string.Empty;
			string dataImageUrlFormatString = this.DataImageUrlFormatString;
			if (!DataBinder.IsNull(dataValue))
			{
				string text = dataValue.ToString();
				if (text.Length > 0)
				{
					if (dataImageUrlFormatString.Length == 0)
					{
						result = text;
					}
					else
					{
						result = string.Format(CultureInfo.CurrentCulture, dataImageUrlFormatString, new object[]
						{
							dataValue
						});
					}
				}
				return result;
			}
			return null;
		}

		// Token: 0x060034EF RID: 13551 RVA: 0x000ABD74 File Offset: 0x000A9F74
		protected virtual string GetFormattedAlternateText(Control controlContainer)
		{
			string dataAlternateTextField = this.DataAlternateTextField;
			string dataAlternateTextFormatString = this.DataAlternateTextFormatString;
			string result;
			if (dataAlternateTextField.Length > 0)
			{
				object value = this.GetValue(controlContainer, dataAlternateTextField, ref this._altTextFieldDesc);
				string text = string.Empty;
				if (!DataBinder.IsNull(value))
				{
					text = value.ToString();
				}
				if (dataAlternateTextFormatString.Length > 0)
				{
					result = string.Format(CultureInfo.CurrentCulture, dataAlternateTextFormatString, new object[]
					{
						value
					});
				}
				else
				{
					result = text;
				}
			}
			else
			{
				result = this.AlternateText;
			}
			return result;
		}

		// Token: 0x060034F0 RID: 13552 RVA: 0x00087251 File Offset: 0x00085451
		protected virtual string GetDesignTimeValue()
		{
			return SR.GetString("Sample_Databound_Text");
		}

		// Token: 0x060034F1 RID: 13553 RVA: 0x000ABDEC File Offset: 0x000A9FEC
		protected virtual object GetValue(Control controlContainer, string fieldName, ref PropertyDescriptor cachedDescriptor)
		{
			object result = null;
			if (controlContainer == null)
			{
				throw new HttpException(SR.GetString("DataControlField_NoContainer"));
			}
			object dataItem = DataBinder.GetDataItem(controlContainer);
			if (dataItem == null && !base.DesignMode)
			{
				throw new HttpException(SR.GetString("DataItem_Not_Found"));
			}
			if (cachedDescriptor == null && !fieldName.Equals(ImageField.ThisExpression))
			{
				cachedDescriptor = TypeDescriptor.GetProperties(dataItem).Find(fieldName, true);
				if (cachedDescriptor == null && !base.DesignMode)
				{
					throw new HttpException(SR.GetString("Field_Not_Found", new object[]
					{
						fieldName
					}));
				}
			}
			if (cachedDescriptor != null && dataItem != null)
			{
				result = cachedDescriptor.GetValue(dataItem);
			}
			else if (!base.DesignMode)
			{
				result = dataItem;
			}
			return result;
		}

		// Token: 0x060034F2 RID: 13554 RVA: 0x000ABE95 File Offset: 0x000AA095
		public override bool Initialize(bool enableSorting, Control control)
		{
			base.Initialize(enableSorting, control);
			this._imageFieldDesc = null;
			this._altTextFieldDesc = null;
			return false;
		}

		// Token: 0x060034F3 RID: 13555 RVA: 0x000ABEAF File Offset: 0x000AA0AF
		public override void InitializeCell(DataControlFieldCell cell, DataControlCellType cellType, DataControlRowState rowState, int rowIndex)
		{
			base.InitializeCell(cell, cellType, rowState, rowIndex);
			if (cellType == DataControlCellType.DataCell)
			{
				this.InitializeDataCell(cell, rowState);
			}
		}

		// Token: 0x060034F4 RID: 13556 RVA: 0x000ABEC8 File Offset: 0x000AA0C8
		protected virtual void InitializeDataCell(DataControlFieldCell cell, DataControlRowState rowState)
		{
			Control control = null;
			if (((rowState & DataControlRowState.Edit) != DataControlRowState.Normal && !this.ReadOnly) || (rowState & DataControlRowState.Insert) != DataControlRowState.Normal)
			{
				TextBox textBox = new TextBox();
				cell.Controls.Add(textBox);
				if (this.DataImageUrlField.Length != 0 && (rowState & DataControlRowState.Edit) != DataControlRowState.Normal)
				{
					control = textBox;
				}
			}
			else if (this.DataImageUrlField.Length != 0)
			{
				control = cell;
				Image child = new Image();
				Label child2 = new Label();
				cell.Controls.Add(child);
				cell.Controls.Add(child2);
			}
			if (control != null && base.Visible)
			{
				control.DataBinding += this.OnDataBindField;
			}
		}

		// Token: 0x060034F5 RID: 13557 RVA: 0x000ABF64 File Offset: 0x000AA164
		protected virtual void OnDataBindField(object sender, EventArgs e)
		{
			Control control = (Control)sender;
			Control namingContainer = control.NamingContainer;
			string nullImageUrl = this.NullImageUrl;
			string formattedAlternateText = this.GetFormattedAlternateText(namingContainer);
			if (base.DesignMode && control is TableCell)
			{
				if (control.Controls.Count == 0 || !(control.Controls[0] is Image))
				{
					throw new HttpException(SR.GetString("ImageField_WrongControlType", new object[]
					{
						this.DataImageUrlField
					}));
				}
				((Image)control.Controls[0]).Visible = false;
				((TableCell)control).Text = this.GetDesignTimeValue();
				return;
			}
			else
			{
				object value = this.GetValue(namingContainer, this.DataImageUrlField, ref this._imageFieldDesc);
				string text = this.FormatImageUrlValue(value);
				if (control is TableCell)
				{
					TableCell tableCell = (TableCell)control;
					if (tableCell.Controls.Count < 2 || !(tableCell.Controls[0] is Image) || !(tableCell.Controls[1] is Label))
					{
						throw new HttpException(SR.GetString("ImageField_WrongControlType", new object[]
						{
							this.DataImageUrlField
						}));
					}
					Image image = (Image)tableCell.Controls[0];
					Label label = (Label)tableCell.Controls[1];
					label.Visible = false;
					if (text == null || (this.ConvertEmptyStringToNull && text.Length == 0))
					{
						if (nullImageUrl.Length > 0)
						{
							text = nullImageUrl;
						}
						else
						{
							image.Visible = false;
							label.Text = this.NullDisplayText;
							label.Visible = true;
						}
					}
					if (!CrossSiteScriptingValidation.IsDangerousUrl(text))
					{
						image.ImageUrl = text;
					}
					image.AlternateText = formattedAlternateText;
					return;
				}
				else
				{
					if (!(control is TextBox))
					{
						throw new HttpException(SR.GetString("ImageField_WrongControlType", new object[]
						{
							this.DataImageUrlField
						}));
					}
					((TextBox)control).Text = value.ToString();
					((TextBox)control).ToolTip = formattedAlternateText;
					if (value != null && value.GetType().IsPrimitive)
					{
						((TextBox)control).Columns = 5;
					}
					return;
				}
			}
		}

		// Token: 0x060034F6 RID: 13558 RVA: 0x00006164 File Offset: 0x00004364
		public override void ValidateSupportsCallback()
		{
		}

		// Token: 0x040021B2 RID: 8626
		public static readonly string ThisExpression = "!";

		// Token: 0x040021B3 RID: 8627
		private PropertyDescriptor _imageFieldDesc;

		// Token: 0x040021B4 RID: 8628
		private PropertyDescriptor _altTextFieldDesc;

		// Token: 0x040021B5 RID: 8629
		private string _dataField;
	}
}
