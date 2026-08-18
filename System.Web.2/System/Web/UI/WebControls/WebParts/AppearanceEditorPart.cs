using System;
using System.ComponentModel;
using System.Globalization;

namespace System.Web.UI.WebControls.WebParts
{
	// Token: 0x02000528 RID: 1320
	public sealed class AppearanceEditorPart : EditorPart
	{
		// Token: 0x1700139E RID: 5022
		// (get) Token: 0x060042D8 RID: 17112 RVA: 0x000D9E7A File Offset: 0x000D807A
		// (set) Token: 0x060042D9 RID: 17113 RVA: 0x000D9E82 File Offset: 0x000D8082
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Themeable(false)]
		public override string DefaultButton
		{
			get
			{
				return base.DefaultButton;
			}
			set
			{
				base.DefaultButton = value;
			}
		}

		// Token: 0x1700139F RID: 5023
		// (get) Token: 0x060042DA RID: 17114 RVA: 0x000D9E8B File Offset: 0x000D808B
		private bool HasError
		{
			get
			{
				return this._titleErrorMessage != null || this._heightErrorMessage != null || this._widthErrorMessage != null || this._chromeTypeErrorMessage != null || this._hiddenErrorMessage != null || this._directionErrorMessage != null;
			}
		}

		// Token: 0x170013A0 RID: 5024
		// (get) Token: 0x060042DB RID: 17115 RVA: 0x000D9EC0 File Offset: 0x000D80C0
		// (set) Token: 0x060042DC RID: 17116 RVA: 0x000D9EF2 File Offset: 0x000D80F2
		[WebSysDefaultValue("AppearanceEditorPart_PartTitle")]
		public override string Title
		{
			get
			{
				string text = (string)this.ViewState["Title"];
				if (text == null)
				{
					return SR.GetString("AppearanceEditorPart_PartTitle");
				}
				return text;
			}
			set
			{
				this.ViewState["Title"] = value;
			}
		}

		// Token: 0x060042DD RID: 17117 RVA: 0x000D9F08 File Offset: 0x000D8108
		public override bool ApplyChanges()
		{
			WebPart webPartToEdit = base.WebPartToEdit;
			if (webPartToEdit != null)
			{
				this.EnsureChildControls();
				bool allowLayoutChange = webPartToEdit.Zone.AllowLayoutChange;
				try
				{
					webPartToEdit.Title = this._title.Text;
				}
				catch (Exception ex)
				{
					this._titleErrorMessage = base.CreateErrorMessage(ex.Message);
				}
				if (allowLayoutChange)
				{
					try
					{
						TypeConverter converter = TypeDescriptor.GetConverter(typeof(PartChromeType));
						webPartToEdit.ChromeType = (PartChromeType)converter.ConvertFromString(this._chromeType.SelectedValue);
					}
					catch (Exception ex2)
					{
						this._chromeTypeErrorMessage = base.CreateErrorMessage(ex2.Message);
					}
				}
				try
				{
					TypeConverter converter2 = TypeDescriptor.GetConverter(typeof(ContentDirection));
					webPartToEdit.Direction = (ContentDirection)converter2.ConvertFromString(this._direction.SelectedValue);
				}
				catch (Exception ex3)
				{
					this._directionErrorMessage = base.CreateErrorMessage(ex3.Message);
				}
				if (allowLayoutChange)
				{
					Unit empty = Unit.Empty;
					string value = this._height.Value;
					if (!string.IsNullOrEmpty(value))
					{
						double num;
						if (double.TryParse(this._height.Value, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowExponent, CultureInfo.CurrentCulture, out num))
						{
							if (num < 0.0)
							{
								this._heightErrorMessage = SR.GetString("EditorPart_PropertyMinValue", new object[]
								{
									0.ToString(CultureInfo.CurrentCulture)
								});
							}
							else if (num > 32767.0)
							{
								this._heightErrorMessage = SR.GetString("EditorPart_PropertyMaxValue", new object[]
								{
									32767.ToString(CultureInfo.CurrentCulture)
								});
							}
							else
							{
								empty = new Unit(num, this._height.Type);
							}
						}
						else
						{
							this._heightErrorMessage = SR.GetString("EditorPart_PropertyMustBeDecimal");
						}
					}
					if (this._heightErrorMessage == null)
					{
						try
						{
							webPartToEdit.Height = empty;
						}
						catch (Exception ex4)
						{
							this._heightErrorMessage = base.CreateErrorMessage(ex4.Message);
						}
					}
				}
				if (allowLayoutChange)
				{
					Unit empty2 = Unit.Empty;
					string value2 = this._width.Value;
					if (!string.IsNullOrEmpty(value2))
					{
						double num2;
						if (double.TryParse(this._width.Value, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowExponent, CultureInfo.CurrentCulture, out num2))
						{
							if (num2 < 0.0)
							{
								this._widthErrorMessage = SR.GetString("EditorPart_PropertyMinValue", new object[]
								{
									0.ToString(CultureInfo.CurrentCulture)
								});
							}
							else if (num2 > 32767.0)
							{
								this._widthErrorMessage = SR.GetString("EditorPart_PropertyMaxValue", new object[]
								{
									32767.ToString(CultureInfo.CurrentCulture)
								});
							}
							else
							{
								empty2 = new Unit(num2, this._width.Type);
							}
						}
						else
						{
							this._widthErrorMessage = SR.GetString("EditorPart_PropertyMustBeDecimal");
						}
					}
					if (this._widthErrorMessage == null)
					{
						try
						{
							webPartToEdit.Width = empty2;
						}
						catch (Exception ex5)
						{
							this._widthErrorMessage = base.CreateErrorMessage(ex5.Message);
						}
					}
				}
				if (allowLayoutChange && webPartToEdit.AllowHide)
				{
					try
					{
						webPartToEdit.Hidden = this._hidden.Checked;
					}
					catch (Exception ex6)
					{
						this._hiddenErrorMessage = base.CreateErrorMessage(ex6.Message);
					}
				}
			}
			return !this.HasError;
		}

		// Token: 0x060042DE RID: 17118 RVA: 0x000DA288 File Offset: 0x000D8488
		protected internal override void CreateChildControls()
		{
			ControlCollection controls = this.Controls;
			controls.Clear();
			this._title = new TextBox();
			this._title.Columns = 30;
			controls.Add(this._title);
			TypeConverter converter = TypeDescriptor.GetConverter(typeof(PartChromeType));
			this._chromeType = new DropDownList();
			this._chromeType.Items.Add(new ListItem(SR.GetString("PartChromeType_Default"), converter.ConvertToString(PartChromeType.Default)));
			this._chromeType.Items.Add(new ListItem(SR.GetString("PartChromeType_TitleAndBorder"), converter.ConvertToString(PartChromeType.TitleAndBorder)));
			this._chromeType.Items.Add(new ListItem(SR.GetString("PartChromeType_TitleOnly"), converter.ConvertToString(PartChromeType.TitleOnly)));
			this._chromeType.Items.Add(new ListItem(SR.GetString("PartChromeType_BorderOnly"), converter.ConvertToString(PartChromeType.BorderOnly)));
			this._chromeType.Items.Add(new ListItem(SR.GetString("PartChromeType_None"), converter.ConvertToString(PartChromeType.None)));
			controls.Add(this._chromeType);
			TypeConverter converter2 = TypeDescriptor.GetConverter(typeof(ContentDirection));
			this._direction = new DropDownList();
			this._direction.Items.Add(new ListItem(SR.GetString("ContentDirection_NotSet"), converter2.ConvertToString(ContentDirection.NotSet)));
			this._direction.Items.Add(new ListItem(SR.GetString("ContentDirection_LeftToRight"), converter2.ConvertToString(ContentDirection.LeftToRight)));
			this._direction.Items.Add(new ListItem(SR.GetString("ContentDirection_RightToLeft"), converter2.ConvertToString(ContentDirection.RightToLeft)));
			controls.Add(this._direction);
			this._height = new AppearanceEditorPart.UnitInput();
			controls.Add(this._height);
			this._width = new AppearanceEditorPart.UnitInput();
			controls.Add(this._width);
			this._hidden = new CheckBox();
			controls.Add(this._hidden);
			foreach (object obj in controls)
			{
				Control control = (Control)obj;
				control.EnableViewState = false;
			}
		}

		// Token: 0x060042DF RID: 17119 RVA: 0x000DA500 File Offset: 0x000D8700
		protected internal override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			if (this.Display && this.Visible && !this.HasError)
			{
				this.SyncChanges();
			}
		}

		// Token: 0x060042E0 RID: 17120 RVA: 0x000DA528 File Offset: 0x000D8728
		protected internal override void RenderContents(HtmlTextWriter writer)
		{
			if (this.Page != null)
			{
				this.Page.VerifyRenderingInServerForm(this);
			}
			this.EnsureChildControls();
			string[] propertyDisplayNames = new string[]
			{
				SR.GetString("AppearanceEditorPart_Title"),
				SR.GetString("AppearanceEditorPart_ChromeType"),
				SR.GetString("AppearanceEditorPart_Direction"),
				SR.GetString("AppearanceEditorPart_Height"),
				SR.GetString("AppearanceEditorPart_Width"),
				SR.GetString("AppearanceEditorPart_Hidden")
			};
			WebControl[] propertyEditors = new WebControl[]
			{
				this._title,
				this._chromeType,
				this._direction,
				this._height,
				this._width,
				this._hidden
			};
			string[] errorMessages = new string[]
			{
				this._titleErrorMessage,
				this._chromeTypeErrorMessage,
				this._directionErrorMessage,
				this._heightErrorMessage,
				this._widthErrorMessage,
				this._hiddenErrorMessage
			};
			base.RenderPropertyEditors(writer, propertyDisplayNames, null, propertyEditors, errorMessages);
		}

		// Token: 0x060042E1 RID: 17121 RVA: 0x000DA62C File Offset: 0x000D882C
		public override void SyncChanges()
		{
			WebPart webPartToEdit = base.WebPartToEdit;
			if (webPartToEdit != null)
			{
				bool allowLayoutChange = webPartToEdit.Zone.AllowLayoutChange;
				this.EnsureChildControls();
				this._title.Text = webPartToEdit.Title;
				TypeConverter converter = TypeDescriptor.GetConverter(typeof(PartChromeType));
				this._chromeType.SelectedValue = converter.ConvertToString(webPartToEdit.ChromeType);
				this._chromeType.Enabled = allowLayoutChange;
				TypeConverter converter2 = TypeDescriptor.GetConverter(typeof(ContentDirection));
				this._direction.SelectedValue = converter2.ConvertToString(webPartToEdit.Direction);
				this._height.Unit = webPartToEdit.Height;
				this._height.Enabled = allowLayoutChange;
				this._width.Unit = webPartToEdit.Width;
				this._width.Enabled = allowLayoutChange;
				this._hidden.Checked = webPartToEdit.Hidden;
				this._hidden.Enabled = (allowLayoutChange && webPartToEdit.AllowHide);
			}
		}

		// Token: 0x04002589 RID: 9609
		private TextBox _title;

		// Token: 0x0400258A RID: 9610
		private AppearanceEditorPart.UnitInput _height;

		// Token: 0x0400258B RID: 9611
		private AppearanceEditorPart.UnitInput _width;

		// Token: 0x0400258C RID: 9612
		private DropDownList _chromeType;

		// Token: 0x0400258D RID: 9613
		private CheckBox _hidden;

		// Token: 0x0400258E RID: 9614
		private DropDownList _direction;

		// Token: 0x0400258F RID: 9615
		private string _titleErrorMessage;

		// Token: 0x04002590 RID: 9616
		private string _heightErrorMessage;

		// Token: 0x04002591 RID: 9617
		private string _widthErrorMessage;

		// Token: 0x04002592 RID: 9618
		private string _chromeTypeErrorMessage;

		// Token: 0x04002593 RID: 9619
		private string _hiddenErrorMessage;

		// Token: 0x04002594 RID: 9620
		private string _directionErrorMessage;

		// Token: 0x04002595 RID: 9621
		private const int TextBoxColumns = 30;

		// Token: 0x04002596 RID: 9622
		private const int MinUnitValue = 0;

		// Token: 0x04002597 RID: 9623
		private const int MaxUnitValue = 32767;

		// Token: 0x020009E3 RID: 2531
		private sealed class UnitInput : CompositeControl
		{
			// Token: 0x17001E04 RID: 7684
			// (get) Token: 0x06006D04 RID: 27908 RVA: 0x00186456 File Offset: 0x00184656
			public string Value
			{
				get
				{
					if (this._value == null)
					{
						return string.Empty;
					}
					return this._value.Text;
				}
			}

			// Token: 0x17001E05 RID: 7685
			// (get) Token: 0x06006D05 RID: 27909 RVA: 0x00186471 File Offset: 0x00184671
			public UnitType Type
			{
				get
				{
					if (this._type == null)
					{
						return (UnitType)0;
					}
					return (UnitType)int.Parse(this._type.SelectedValue, CultureInfo.InvariantCulture);
				}
			}

			// Token: 0x17001E06 RID: 7686
			// (set) Token: 0x06006D06 RID: 27910 RVA: 0x00186494 File Offset: 0x00184694
			public Unit Unit
			{
				set
				{
					this.EnsureChildControls();
					if (value == Unit.Empty)
					{
						this._value.Text = string.Empty;
						this._type.SelectedIndex = 0;
						return;
					}
					this._value.Text = value.Value.ToString(CultureInfo.CurrentCulture);
					this._type.SelectedValue = ((int)value.Type).ToString(CultureInfo.InvariantCulture);
				}
			}

			// Token: 0x06006D07 RID: 27911 RVA: 0x00186510 File Offset: 0x00184710
			protected internal override void CreateChildControls()
			{
				this.Controls.Clear();
				this._value = new TextBox();
				this._value.Columns = 2;
				this.Controls.Add(this._value);
				this._type = new DropDownList();
				this._type.Items.Add(new ListItem(SR.GetString("AppearanceEditorPart_Pixels"), 1.ToString(CultureInfo.InvariantCulture)));
				this._type.Items.Add(new ListItem(SR.GetString("AppearanceEditorPart_Points"), 2.ToString(CultureInfo.InvariantCulture)));
				this._type.Items.Add(new ListItem(SR.GetString("AppearanceEditorPart_Picas"), 3.ToString(CultureInfo.InvariantCulture)));
				this._type.Items.Add(new ListItem(SR.GetString("AppearanceEditorPart_Inches"), 4.ToString(CultureInfo.InvariantCulture)));
				this._type.Items.Add(new ListItem(SR.GetString("AppearanceEditorPart_Millimeters"), 5.ToString(CultureInfo.InvariantCulture)));
				this._type.Items.Add(new ListItem(SR.GetString("AppearanceEditorPart_Centimeters"), 6.ToString(CultureInfo.InvariantCulture)));
				this._type.Items.Add(new ListItem(SR.GetString("AppearanceEditorPart_Percent"), 7.ToString(CultureInfo.InvariantCulture)));
				this._type.Items.Add(new ListItem(SR.GetString("AppearanceEditorPart_Em"), 8.ToString(CultureInfo.InvariantCulture)));
				this._type.Items.Add(new ListItem(SR.GetString("AppearanceEditorPart_Ex"), 9.ToString(CultureInfo.InvariantCulture)));
				this.Controls.Add(this._type);
			}

			// Token: 0x06006D08 RID: 27912 RVA: 0x00186704 File Offset: 0x00184904
			protected internal override void Render(HtmlTextWriter writer)
			{
				this.EnsureChildControls();
				this._value.ApplyStyle(base.ControlStyle);
				this._value.RenderControl(writer);
				writer.Write("&nbsp;");
				this._type.ApplyStyle(base.ControlStyle);
				this._type.RenderControl(writer);
			}

			// Token: 0x04003A01 RID: 14849
			private TextBox _value;

			// Token: 0x04003A02 RID: 14850
			private DropDownList _type;

			// Token: 0x04003A03 RID: 14851
			private const int TextBoxColumns = 2;
		}
	}
}
