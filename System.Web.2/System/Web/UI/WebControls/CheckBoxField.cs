using System;
using System.Collections.Specialized;
using System.ComponentModel;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000392 RID: 914
	public class CheckBoxField : BoundField
	{
		// Token: 0x17000C3E RID: 3134
		// (get) Token: 0x06002B77 RID: 11127 RVA: 0x0008E1FE File Offset: 0x0008C3FE
		// (set) Token: 0x06002B78 RID: 11128 RVA: 0x0008E227 File Offset: 0x0008C427
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

		// Token: 0x17000C3F RID: 3135
		// (get) Token: 0x06002B79 RID: 11129 RVA: 0x0008E24F File Offset: 0x0008C44F
		// (set) Token: 0x06002B7A RID: 11130 RVA: 0x0008E257 File Offset: 0x0008C457
		[TypeConverter("System.Web.UI.Design.DataSourceBooleanViewSchemaConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
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

		// Token: 0x17000C40 RID: 3136
		// (get) Token: 0x06002B7B RID: 11131 RVA: 0x0008E260 File Offset: 0x0008C460
		// (set) Token: 0x06002B7C RID: 11132 RVA: 0x0008E28D File Offset: 0x0008C48D
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

		// Token: 0x17000C41 RID: 3137
		// (get) Token: 0x06002B7D RID: 11133 RVA: 0x0008E2B5 File Offset: 0x0008C4B5
		// (set) Token: 0x06002B7E RID: 11134 RVA: 0x0008E2DE File Offset: 0x0008C4DE
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

		// Token: 0x17000C42 RID: 3138
		// (get) Token: 0x06002B7F RID: 11135 RVA: 0x0008E306 File Offset: 0x0008C506
		// (set) Token: 0x06002B80 RID: 11136 RVA: 0x0008E32F File Offset: 0x0008C52F
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Never)]
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

		// Token: 0x17000C43 RID: 3139
		// (get) Token: 0x06002B81 RID: 11137 RVA: 0x0008E357 File Offset: 0x0008C557
		// (set) Token: 0x06002B82 RID: 11138 RVA: 0x0008E384 File Offset: 0x0008C584
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

		// Token: 0x17000C44 RID: 3140
		// (get) Token: 0x06002B83 RID: 11139 RVA: 0x00007722 File Offset: 0x00005922
		protected override bool SupportsHtmlEncode
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000C45 RID: 3141
		// (get) Token: 0x06002B84 RID: 11140 RVA: 0x0008E3AC File Offset: 0x0008C5AC
		// (set) Token: 0x06002B85 RID: 11141 RVA: 0x00088815 File Offset: 0x00086A15
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

		// Token: 0x17000C46 RID: 3142
		// (get) Token: 0x06002B86 RID: 11142 RVA: 0x0008E3D9 File Offset: 0x0008C5D9
		// (set) Token: 0x06002B87 RID: 11143 RVA: 0x0008E402 File Offset: 0x0008C602
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Never)]
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

		// Token: 0x06002B88 RID: 11144 RVA: 0x0008E42A File Offset: 0x0008C62A
		protected override void CopyProperties(DataControlField newField)
		{
			((CheckBoxField)newField).Text = this.Text;
			this._suppressPropertyThrows = true;
			((CheckBoxField)newField)._suppressPropertyThrows = true;
			base.CopyProperties(newField);
			this._suppressPropertyThrows = false;
			((CheckBoxField)newField)._suppressPropertyThrows = false;
		}

		// Token: 0x06002B89 RID: 11145 RVA: 0x0008E46A File Offset: 0x0008C66A
		protected override DataControlField CreateField()
		{
			return new CheckBoxField();
		}

		// Token: 0x06002B8A RID: 11146 RVA: 0x0008E474 File Offset: 0x0008C674
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

		// Token: 0x06002B8B RID: 11147 RVA: 0x0008E4E6 File Offset: 0x0008C6E6
		protected override object GetDesignTimeValue()
		{
			return true;
		}

		// Token: 0x06002B8C RID: 11148 RVA: 0x0008E4F0 File Offset: 0x0008C6F0
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

		// Token: 0x06002B8D RID: 11149 RVA: 0x0008E594 File Offset: 0x0008C794
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

		// Token: 0x06002B8E RID: 11150 RVA: 0x00006164 File Offset: 0x00004364
		public override void ValidateSupportsCallback()
		{
		}

		// Token: 0x04001F18 RID: 7960
		private bool _suppressPropertyThrows;
	}
}
