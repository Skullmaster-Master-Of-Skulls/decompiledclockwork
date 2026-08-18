using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	// Token: 0x020004F0 RID: 1264
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class CheckBoxField : BoundField
	{
		// Token: 0x17000E6B RID: 3691
		// (get) Token: 0x06003D8E RID: 15758 RVA: 0x0010204C File Offset: 0x0010104C
		// (set) Token: 0x06003D8F RID: 15759 RVA: 0x00102084 File Offset: 0x00101084
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool ApplyFormatInEditMode
		{
			get
			{
				if (!this._suppressPropertyThrows)
				{
					throw new NotSupportedException(SR.GetString("CheckBoxField_NotSupported", new object[]
					{
						"ApplyFormatInEditMode"
					}));
				}
				return false;
			}
			set
			{
				if (!this._suppressPropertyThrows)
				{
					throw new NotSupportedException(SR.GetString("CheckBoxField_NotSupported", new object[]
					{
						"ApplyFormatInEditMode"
					}));
				}
			}
		}

		// Token: 0x17000E6C RID: 3692
		// (get) Token: 0x06003D90 RID: 15760 RVA: 0x001020B9 File Offset: 0x001010B9
		// (set) Token: 0x06003D91 RID: 15761 RVA: 0x001020C1 File Offset: 0x001010C1
		[TypeConverter("System.Web.UI.Design.DataSourceBooleanViewSchemaConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public override string DataField
		{
			get
			{
				return base.DataField;
			}
			set
			{
				base.DataField = value;
			}
		}

		// Token: 0x17000E6D RID: 3693
		// (get) Token: 0x06003D92 RID: 15762 RVA: 0x001020CC File Offset: 0x001010CC
		// (set) Token: 0x06003D93 RID: 15763 RVA: 0x00102108 File Offset: 0x00101108
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string DataFormatString
		{
			get
			{
				if (!this._suppressPropertyThrows)
				{
					throw new NotSupportedException(SR.GetString("CheckBoxField_NotSupported", new object[]
					{
						"DataFormatString"
					}));
				}
				return string.Empty;
			}
			set
			{
				if (!this._suppressPropertyThrows)
				{
					throw new NotSupportedException(SR.GetString("CheckBoxField_NotSupported", new object[]
					{
						"DataFormatString"
					}));
				}
			}
		}

		// Token: 0x17000E6E RID: 3694
		// (get) Token: 0x06003D94 RID: 15764 RVA: 0x00102140 File Offset: 0x00101140
		// (set) Token: 0x06003D95 RID: 15765 RVA: 0x00102178 File Offset: 0x00101178
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool HtmlEncode
		{
			get
			{
				if (!this._suppressPropertyThrows)
				{
					throw new NotSupportedException(SR.GetString("CheckBoxField_NotSupported", new object[]
					{
						"HtmlEncode"
					}));
				}
				return false;
			}
			set
			{
				if (!this._suppressPropertyThrows)
				{
					throw new NotSupportedException(SR.GetString("CheckBoxField_NotSupported", new object[]
					{
						"HtmlEncode"
					}));
				}
			}
		}

		// Token: 0x17000E6F RID: 3695
		// (get) Token: 0x06003D96 RID: 15766 RVA: 0x001021B0 File Offset: 0x001011B0
		// (set) Token: 0x06003D97 RID: 15767 RVA: 0x001021E8 File Offset: 0x001011E8
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override bool HtmlEncodeFormatString
		{
			get
			{
				if (!this._suppressPropertyThrows)
				{
					throw new NotSupportedException(SR.GetString("CheckBoxField_NotSupported", new object[]
					{
						"HtmlEncodeFormatString"
					}));
				}
				return false;
			}
			set
			{
				if (!this._suppressPropertyThrows)
				{
					throw new NotSupportedException(SR.GetString("CheckBoxField_NotSupported", new object[]
					{
						"HtmlEncodeFormatString"
					}));
				}
			}
		}

		// Token: 0x17000E70 RID: 3696
		// (get) Token: 0x06003D98 RID: 15768 RVA: 0x00102220 File Offset: 0x00101220
		// (set) Token: 0x06003D99 RID: 15769 RVA: 0x0010225C File Offset: 0x0010125C
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string NullDisplayText
		{
			get
			{
				if (!this._suppressPropertyThrows)
				{
					throw new NotSupportedException(SR.GetString("CheckBoxField_NotSupported", new object[]
					{
						"NullDisplayText"
					}));
				}
				return string.Empty;
			}
			set
			{
				if (!this._suppressPropertyThrows)
				{
					throw new NotSupportedException(SR.GetString("CheckBoxField_NotSupported", new object[]
					{
						"NullDisplayText"
					}));
				}
			}
		}

		// Token: 0x17000E71 RID: 3697
		// (get) Token: 0x06003D9A RID: 15770 RVA: 0x00102291 File Offset: 0x00101291
		protected override bool SupportsHtmlEncode
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000E72 RID: 3698
		// (get) Token: 0x06003D9B RID: 15771 RVA: 0x00102294 File Offset: 0x00101294
		// (set) Token: 0x06003D9C RID: 15772 RVA: 0x001022C1 File Offset: 0x001012C1
		[Localizable(true)]
		[WebCategory("Appearance")]
		[DefaultValue("")]
		[WebSysDescription("CheckBoxField_Text")]
		public virtual string Text
		{
			get
			{
				object obj = base.ViewState["Text"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				if (!object.Equals(value, base.ViewState["Text"]))
				{
					base.ViewState["Text"] = value;
					this.OnFieldChanged();
				}
			}
		}

		// Token: 0x17000E73 RID: 3699
		// (get) Token: 0x06003D9D RID: 15773 RVA: 0x001022F4 File Offset: 0x001012F4
		// (set) Token: 0x06003D9E RID: 15774 RVA: 0x0010232C File Offset: 0x0010132C
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public override bool ConvertEmptyStringToNull
		{
			get
			{
				if (!this._suppressPropertyThrows)
				{
					throw new NotSupportedException(SR.GetString("CheckBoxField_NotSupported", new object[]
					{
						"ConvertEmptyStringToNull"
					}));
				}
				return false;
			}
			set
			{
				if (!this._suppressPropertyThrows)
				{
					throw new NotSupportedException(SR.GetString("CheckBoxField_NotSupported", new object[]
					{
						"ConvertEmptyStringToNull"
					}));
				}
			}
		}

		// Token: 0x06003D9F RID: 15775 RVA: 0x00102361 File Offset: 0x00101361
		protected override void CopyProperties(DataControlField newField)
		{
			((CheckBoxField)newField).Text = this.Text;
			this._suppressPropertyThrows = true;
			((CheckBoxField)newField)._suppressPropertyThrows = true;
			base.CopyProperties(newField);
			this._suppressPropertyThrows = false;
			((CheckBoxField)newField)._suppressPropertyThrows = false;
		}

		// Token: 0x06003DA0 RID: 15776 RVA: 0x001023A1 File Offset: 0x001013A1
		protected override DataControlField CreateField()
		{
			return new CheckBoxField();
		}

		// Token: 0x06003DA1 RID: 15777 RVA: 0x001023A8 File Offset: 0x001013A8
		public override void ExtractValuesFromCell(IOrderedDictionary dictionary, DataControlFieldCell cell, DataControlRowState rowState, bool includeReadOnly)
		{
			string dataField = this.DataField;
			object obj = null;
			if (cell.Controls.Count > 0)
			{
				Control control = cell.Controls[0];
				CheckBox checkBox = control as CheckBox;
				if (checkBox != null && (includeReadOnly || checkBox.Enabled))
				{
					obj = checkBox.Checked;
				}
			}
			if (obj != null)
			{
				if (dictionary.Contains(dataField))
				{
					dictionary[dataField] = obj;
					return;
				}
				dictionary.Add(dataField, obj);
			}
		}

		// Token: 0x06003DA2 RID: 15778 RVA: 0x0010241A File Offset: 0x0010141A
		protected override object GetDesignTimeValue()
		{
			return true;
		}

		// Token: 0x06003DA3 RID: 15779 RVA: 0x00102424 File Offset: 0x00101424
		protected override void InitializeDataCell(DataControlFieldCell cell, DataControlRowState rowState)
		{
			CheckBox checkBox = null;
			CheckBox checkBox2 = null;
			if (((rowState & DataControlRowState.Edit) != DataControlRowState.Normal && !this.ReadOnly) || (rowState & DataControlRowState.Insert) != DataControlRowState.Normal)
			{
				CheckBox checkBox3 = new CheckBox();
				checkBox3.ToolTip = this.HeaderText;
				checkBox = checkBox3;
				if (this.DataField.Length != 0 && (rowState & DataControlRowState.Edit) != DataControlRowState.Normal)
				{
					checkBox2 = checkBox3;
				}
			}
			else if (this.DataField.Length != 0)
			{
				CheckBox checkBox4 = new CheckBox();
				checkBox4.Text = this.Text;
				checkBox4.Enabled = false;
				checkBox = checkBox4;
				checkBox2 = checkBox4;
			}
			if (checkBox != null)
			{
				cell.Controls.Add(checkBox);
			}
			if (checkBox2 != null && base.Visible)
			{
				checkBox2.DataBinding += this.OnDataBindField;
			}
		}

		// Token: 0x06003DA4 RID: 15780 RVA: 0x001024C8 File Offset: 0x001014C8
		protected override void OnDataBindField(object sender, EventArgs e)
		{
			Control control = (Control)sender;
			Control namingContainer = control.NamingContainer;
			object value = this.GetValue(namingContainer);
			if (!(control is CheckBox))
			{
				throw new HttpException(SR.GetString("CheckBoxField_WrongControlType", new object[]
				{
					this.DataField
				}));
			}
			if (DataBinder.IsNull(value))
			{
				((CheckBox)control).Checked = false;
			}
			else if (value is bool)
			{
				((CheckBox)control).Checked = (bool)value;
			}
			else
			{
				try
				{
					((CheckBox)control).Checked = bool.Parse(value.ToString());
				}
				catch (FormatException innerException)
				{
					throw new HttpException(SR.GetString("CheckBoxField_CouldntParseAsBoolean", new object[]
					{
						this.DataField
					}), innerException);
				}
			}
			((CheckBox)control).Text = this.Text;
		}

		// Token: 0x06003DA5 RID: 15781 RVA: 0x001025A8 File Offset: 0x001015A8
		public override void ValidateSupportsCallback()
		{
		}

		// Token: 0x04002784 RID: 10116
		private bool _suppressPropertyThrows;
	}
}
