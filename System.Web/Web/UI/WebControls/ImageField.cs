using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing.Design;
using System.Globalization;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	// Token: 0x020005BE RID: 1470
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class ImageField : DataControlField
	{
		// Token: 0x170011B5 RID: 4533
		// (get) Token: 0x060047D3 RID: 18387 RVA: 0x001259D0 File Offset: 0x001249D0
		// (set) Token: 0x060047D4 RID: 18388 RVA: 0x001259FD File Offset: 0x001249FD
		[DefaultValue("")]
		[Localizable(true)]
		[WebCategory("Appearance")]
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

		// Token: 0x170011B6 RID: 4534
		// (get) Token: 0x060047D5 RID: 18389 RVA: 0x00125A30 File Offset: 0x00124A30
		// (set) Token: 0x060047D6 RID: 18390 RVA: 0x00125A59 File Offset: 0x00124A59
		[WebSysDescription("ImageField_ConvertEmptyStringToNull")]
		[WebCategory("Behavior")]
		[DefaultValue(true)]
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

		// Token: 0x170011B7 RID: 4535
		// (get) Token: 0x060047D7 RID: 18391 RVA: 0x00125A74 File Offset: 0x00124A74
		// (set) Token: 0x060047D8 RID: 18392 RVA: 0x00125AA1 File Offset: 0x00124AA1
		[WebSysDescription("ImageField_DataAlternateTextField")]
		[DefaultValue("")]
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[WebCategory("Data")]
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

		// Token: 0x170011B8 RID: 4536
		// (get) Token: 0x060047D9 RID: 18393 RVA: 0x00125AD4 File Offset: 0x00124AD4
		// (set) Token: 0x060047DA RID: 18394 RVA: 0x00125B01 File Offset: 0x00124B01
		[WebCategory("Data")]
		[WebSysDescription("ImageField_DataAlternateTextFormatString")]
		[DefaultValue("")]
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

		// Token: 0x170011B9 RID: 4537
		// (get) Token: 0x060047DB RID: 18395 RVA: 0x00125B34 File Offset: 0x00124B34
		// (set) Token: 0x060047DC RID: 18396 RVA: 0x00125B7C File Offset: 0x00124B7C
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[WebCategory("Data")]
		[WebSysDescription("ImageField_ImageUrlField")]
		[DefaultValue("")]
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

		// Token: 0x170011BA RID: 4538
		// (get) Token: 0x060047DD RID: 18397 RVA: 0x00125BB4 File Offset: 0x00124BB4
		// (set) Token: 0x060047DE RID: 18398 RVA: 0x00125BE1 File Offset: 0x00124BE1
		[WebSysDescription("ImageField_ImageUrlFormatString")]
		[DefaultValue("")]
		[WebCategory("Data")]
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

		// Token: 0x170011BB RID: 4539
		// (get) Token: 0x060047DF RID: 18399 RVA: 0x00125C14 File Offset: 0x00124C14
		// (set) Token: 0x060047E0 RID: 18400 RVA: 0x00125C41 File Offset: 0x00124C41
		[Localizable(true)]
		[WebSysDescription("BoundField_NullDisplayText")]
		[WebCategory("Behavior")]
		[DefaultValue("")]
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

		// Token: 0x170011BC RID: 4540
		// (get) Token: 0x060047E1 RID: 18401 RVA: 0x00125C74 File Offset: 0x00124C74
		// (set) Token: 0x060047E2 RID: 18402 RVA: 0x00125CA1 File Offset: 0x00124CA1
		[UrlProperty]
		[WebSysDescription("ImageField_NullImageUrl")]
		[WebCategory("Behavior")]
		[DefaultValue("")]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
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

		// Token: 0x170011BD RID: 4541
		// (get) Token: 0x060047E3 RID: 18403 RVA: 0x00125CD4 File Offset: 0x00124CD4
		// (set) Token: 0x060047E4 RID: 18404 RVA: 0x00125D00 File Offset: 0x00124D00
		[DefaultValue(false)]
		[WebSysDescription("ImageField_ReadOnly")]
		[WebCategory("Behavior")]
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

		// Token: 0x060047E5 RID: 18405 RVA: 0x00125D48 File Offset: 0x00124D48
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

		// Token: 0x060047E6 RID: 18406 RVA: 0x00125DF5 File Offset: 0x00124DF5
		protected override DataControlField CreateField()
		{
			return new ImageField();
		}

		// Token: 0x060047E7 RID: 18407 RVA: 0x00125DFC File Offset: 0x00124DFC
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

		// Token: 0x060047E8 RID: 18408 RVA: 0x00125EBC File Offset: 0x00124EBC
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

		// Token: 0x060047E9 RID: 18409 RVA: 0x00125F18 File Offset: 0x00124F18
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

		// Token: 0x060047EA RID: 18410 RVA: 0x00125F95 File Offset: 0x00124F95
		protected virtual string GetDesignTimeValue()
		{
			return SR.GetString("Sample_Databound_Text");
		}

		// Token: 0x060047EB RID: 18411 RVA: 0x00125FA4 File Offset: 0x00124FA4
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

		// Token: 0x060047EC RID: 18412 RVA: 0x0012604F File Offset: 0x0012504F
		public override bool Initialize(bool enableSorting, Control control)
		{
			base.Initialize(enableSorting, control);
			this._imageFieldDesc = null;
			this._altTextFieldDesc = null;
			return false;
		}

		// Token: 0x060047ED RID: 18413 RVA: 0x0012606C File Offset: 0x0012506C
		public override void InitializeCell(DataControlFieldCell cell, DataControlCellType cellType, DataControlRowState rowState, int rowIndex)
		{
			base.InitializeCell(cell, cellType, rowState, rowIndex);
			if (cellType != DataControlCellType.DataCell)
			{
				return;
			}
			this.InitializeDataCell(cell, rowState);
		}

		// Token: 0x060047EE RID: 18414 RVA: 0x00126094 File Offset: 0x00125094
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

		// Token: 0x060047EF RID: 18415 RVA: 0x00126130 File Offset: 0x00125130
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

		// Token: 0x060047F0 RID: 18416 RVA: 0x00126356 File Offset: 0x00125356
		public override void ValidateSupportsCallback()
		{
		}

		// Token: 0x04002ABB RID: 10939
		public static readonly string ThisExpression = "!";

		// Token: 0x04002ABC RID: 10940
		private PropertyDescriptor _imageFieldDesc;

		// Token: 0x04002ABD RID: 10941
		private PropertyDescriptor _altTextFieldDesc;

		// Token: 0x04002ABE RID: 10942
		private string _dataField;
	}
}
