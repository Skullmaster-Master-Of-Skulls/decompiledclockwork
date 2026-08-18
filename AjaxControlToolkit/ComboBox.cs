using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Security.Permissions;
using System.Web;
using System.Web.UI;
using System.Web.UI.Design.WebControls;
using System.Web.UI.WebControls;
using AjaxControlToolkit.Design;
using AjaxControlToolkit.ToolboxIcons;

namespace AjaxControlToolkit
{
	// Token: 0x0200006D RID: 109
	[RequiredScript(typeof(CommonToolkitScripts), 4)]
	[RequiredScript(typeof(ScriptControlBase), 2)]
	[RequiredScript(typeof(PopupExtender), 3)]
	[DefaultProperty("SelectedValue")]
	[Designer(typeof(ComboBoxDesigner))]
	[ClientCssResource("ComboBox")]
	[ClientScriptResource("Sys.Extended.UI.ComboBox", "ComboBox")]
	[ToolboxData("<{0}:ComboBox runat=\"server\"></{0}:ComboBox>")]
	[ToolboxBitmap(typeof(Accessor), "ComboBox.bmp")]
	[ParseChildren(true, "Items")]
	[DefaultEvent("SelectedIndexChanged")]
	[ControlValueProperty("SelectedValue")]
	[Bindable(true, BindingDirection.TwoWay)]
	[DataBindingHandler(typeof(ListControlDataBindingHandler))]
	[SupportsEventValidation]
	[ValidationProperty("SelectedItem")]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class ComboBox : ListControl, IScriptControl, IPostBackDataHandler, INamingContainer, IControlResolver
	{
		// Token: 0x1700015D RID: 349
		// (get) Token: 0x060003C3 RID: 963 RVA: 0x0000B282 File Offset: 0x00009482
		// (set) Token: 0x060003C2 RID: 962 RVA: 0x0000B279 File Offset: 0x00009479
		protected virtual ScriptManager ScriptManager
		{
			get
			{
				if (this._scriptManager == null)
				{
					this._scriptManager = ScriptManager.GetCurrent(this.Page);
					if (this._scriptManager == null)
					{
						throw new HttpException("A ScriptManager is required on the page to use ASP.NET AJAX Script Components.");
					}
				}
				return this._scriptManager;
			}
			set
			{
				this._scriptManager = value;
			}
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x060003C4 RID: 964 RVA: 0x0000B2B8 File Offset: 0x000094B8
		protected virtual string ClientControlType
		{
			get
			{
				ClientScriptResourceAttribute clientScriptResourceAttribute = (ClientScriptResourceAttribute)TypeDescriptor.GetAttributes(this)[typeof(ClientScriptResourceAttribute)];
				return clientScriptResourceAttribute.ComponentType;
			}
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x0000B2E6 File Offset: 0x000094E6
		public Control ResolveControl(string controlId)
		{
			return this.FindControl(controlId);
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x060003C7 RID: 967 RVA: 0x0000B307 File Offset: 0x00009507
		// (set) Token: 0x060003C6 RID: 966 RVA: 0x0000B2EF File Offset: 0x000094EF
		[Category("Layout")]
		[DefaultValue(typeof(ComboBoxRenderMode), "Inline")]
		[Description("Whether the ComboBox will render as a block or inline HTML element.")]
		public ComboBoxRenderMode RenderMode
		{
			get
			{
				if (this.ViewState["RenderMode"] != null)
				{
					return (ComboBoxRenderMode)this.ViewState["RenderMode"];
				}
				return ComboBoxRenderMode.Inline;
			}
			set
			{
				this.ViewState["RenderMode"] = value;
			}
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x060003C9 RID: 969 RVA: 0x0000B34C File Offset: 0x0000954C
		// (set) Token: 0x060003C8 RID: 968 RVA: 0x0000B332 File Offset: 0x00009532
		[Description("Whether the ComboBox requires typed text to match an item in the list or allows new items to be created.")]
		[Category("Behavior")]
		[DefaultValue(typeof(ComboBoxStyle), "DropDown")]
		public virtual ComboBoxStyle DropDownStyle
		{
			get
			{
				object obj = this.ViewState["DropDownStyle"];
				if (obj == null)
				{
					return ComboBoxStyle.DropDown;
				}
				return (ComboBoxStyle)obj;
			}
			set
			{
				this.ViewState["DropDownStyle"] = value;
			}
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x060003CB RID: 971 RVA: 0x0000B390 File Offset: 0x00009590
		// (set) Token: 0x060003CA RID: 970 RVA: 0x0000B375 File Offset: 0x00009575
		[Category("Behavior")]
		[Description("Whether the ComboBox auto-completes typing by suggesting an item in the list or appending matches as the user types.")]
		[DefaultValue(typeof(ComboBoxAutoCompleteMode), "None")]
		public virtual ComboBoxAutoCompleteMode AutoCompleteMode
		{
			get
			{
				object obj = this.ViewState["AutoCompleteMode"];
				if (obj == null)
				{
					return ComboBoxAutoCompleteMode.None;
				}
				return (ComboBoxAutoCompleteMode)obj;
			}
			set
			{
				this.ViewState["AutoCompleteMode"] = value;
			}
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x060003CD RID: 973 RVA: 0x0000B3D4 File Offset: 0x000095D4
		// (set) Token: 0x060003CC RID: 972 RVA: 0x0000B3B9 File Offset: 0x000095B9
		[DefaultValue(typeof(ComboBoxItemInsertLocation), "Append")]
		[Description("Whether a new item will be appended, prepended, or inserted ordinally into the items collection.")]
		[Category("Behavior")]
		public virtual ComboBoxItemInsertLocation ItemInsertLocation
		{
			get
			{
				object obj = this.ViewState["ItemInsertLocation"];
				if (obj == null)
				{
					return ComboBoxItemInsertLocation.Append;
				}
				return (ComboBoxItemInsertLocation)obj;
			}
			set
			{
				this.ViewState["ItemInsertLocation"] = value;
			}
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x060003CF RID: 975 RVA: 0x0000B418 File Offset: 0x00009618
		// (set) Token: 0x060003CE RID: 974 RVA: 0x0000B3FD File Offset: 0x000095FD
		[ExtenderControlProperty]
		[ClientPropertyName("caseSensitive")]
		[Category("Behavior")]
		[DefaultValue(false)]
		[Description("Whether the ComboBox auto-completes user typing on a case-sensitive basis.")]
		public virtual bool CaseSensitive
		{
			get
			{
				object obj = this.ViewState["CaseSensitive"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.ViewState["CaseSensitive"] = value;
			}
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x060003D1 RID: 977 RVA: 0x0000B454 File Offset: 0x00009654
		// (set) Token: 0x060003D0 RID: 976 RVA: 0x0000B441 File Offset: 0x00009641
		[ClientPropertyName("listItemHoverCssClass")]
		[Category("Style")]
		[Description("The CSS class to apply to a hovered item in the list.")]
		[ExtenderControlProperty]
		[DefaultValue("")]
		public virtual string ListItemHoverCssClass
		{
			get
			{
				object obj = this.ViewState["ListItemHoverCssClass"];
				if (obj == null)
				{
					return string.Empty;
				}
				return (string)obj;
			}
			set
			{
				this.ViewState["ListItemHoverCssClass"] = value;
			}
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x060003D2 RID: 978 RVA: 0x0000B484 File Offset: 0x00009684
		// (set) Token: 0x060003D3 RID: 979 RVA: 0x0000B499 File Offset: 0x00009699
		[ExtenderControlProperty]
		[ClientPropertyName("selectedIndex")]
		public override int SelectedIndex
		{
			get
			{
				return base.SelectedIndex;
			}
			set
			{
				base.SelectedIndex = value;
			}
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x060003D4 RID: 980 RVA: 0x0000B4A2 File Offset: 0x000096A2
		// (set) Token: 0x060003D5 RID: 981 RVA: 0x0000B4AA File Offset: 0x000096AA
		[ClientPropertyName("autoPostBack")]
		[ExtenderControlProperty]
		public override bool AutoPostBack
		{
			get
			{
				return base.AutoPostBack;
			}
			set
			{
				base.AutoPostBack = value;
			}
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x060003D6 RID: 982 RVA: 0x0000B4B3 File Offset: 0x000096B3
		// (set) Token: 0x060003D7 RID: 983 RVA: 0x0000B4C0 File Offset: 0x000096C0
		public virtual int MaxLength
		{
			get
			{
				return this.TextBoxControl.MaxLength;
			}
			set
			{
				this.TextBoxControl.MaxLength = value;
			}
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x060003D8 RID: 984 RVA: 0x0000B4CE File Offset: 0x000096CE
		// (set) Token: 0x060003D9 RID: 985 RVA: 0x0000B4DB File Offset: 0x000096DB
		public override short TabIndex
		{
			get
			{
				return this.TextBoxControl.TabIndex;
			}
			set
			{
				this.TextBoxControl.TabIndex = value;
			}
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x060003DA RID: 986 RVA: 0x0000B4E9 File Offset: 0x000096E9
		// (set) Token: 0x060003DB RID: 987 RVA: 0x0000B4F1 File Offset: 0x000096F1
		public override bool Enabled
		{
			get
			{
				return base.Enabled;
			}
			set
			{
				base.Enabled = value;
				this.TextBoxControl.Enabled = base.Enabled;
				this.ButtonControl.Enabled = base.Enabled;
			}
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x060003DC RID: 988 RVA: 0x0000B51C File Offset: 0x0000971C
		// (set) Token: 0x060003DD RID: 989 RVA: 0x0000B529 File Offset: 0x00009729
		public override Unit Height
		{
			get
			{
				return this.TextBoxControl.Height;
			}
			set
			{
				this.TextBoxControl.Height = value;
			}
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x060003DE RID: 990 RVA: 0x0000B537 File Offset: 0x00009737
		// (set) Token: 0x060003DF RID: 991 RVA: 0x0000B544 File Offset: 0x00009744
		public override Unit Width
		{
			get
			{
				return this.TextBoxControl.Width;
			}
			set
			{
				this.TextBoxControl.Width = value;
			}
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x060003E0 RID: 992 RVA: 0x0000B552 File Offset: 0x00009752
		// (set) Token: 0x060003E1 RID: 993 RVA: 0x0000B55F File Offset: 0x0000975F
		public override Color ForeColor
		{
			get
			{
				return this.TextBoxControl.ForeColor;
			}
			set
			{
				this.TextBoxControl.ForeColor = value;
			}
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x060003E2 RID: 994 RVA: 0x0000B56D File Offset: 0x0000976D
		// (set) Token: 0x060003E3 RID: 995 RVA: 0x0000B57A File Offset: 0x0000977A
		public override Color BackColor
		{
			get
			{
				return this.TextBoxControl.BackColor;
			}
			set
			{
				this.TextBoxControl.BackColor = value;
			}
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x060003E4 RID: 996 RVA: 0x0000B588 File Offset: 0x00009788
		public override FontInfo Font
		{
			get
			{
				return this.TextBoxControl.Font;
			}
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x060003E5 RID: 997 RVA: 0x0000B595 File Offset: 0x00009795
		// (set) Token: 0x060003E6 RID: 998 RVA: 0x0000B5A2 File Offset: 0x000097A2
		public override Color BorderColor
		{
			get
			{
				return this.TextBoxControl.BorderColor;
			}
			set
			{
				this.TextBoxControl.BorderColor = value;
				this.ButtonControl.BorderColor = value;
				this.TextBoxControl.Style.Add("border-right", "0px none");
			}
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x060003E7 RID: 999 RVA: 0x0000B5D6 File Offset: 0x000097D6
		// (set) Token: 0x060003E8 RID: 1000 RVA: 0x0000B5E3 File Offset: 0x000097E3
		public override BorderStyle BorderStyle
		{
			get
			{
				return this.TextBoxControl.BorderStyle;
			}
			set
			{
				this.TextBoxControl.BorderStyle = value;
				this.ButtonControl.BorderStyle = value;
			}
		}

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x060003E9 RID: 1001 RVA: 0x0000B5FD File Offset: 0x000097FD
		// (set) Token: 0x060003EA RID: 1002 RVA: 0x0000B60A File Offset: 0x0000980A
		public override Unit BorderWidth
		{
			get
			{
				return this.TextBoxControl.BorderWidth;
			}
			set
			{
				this.TextBoxControl.BorderWidth = value;
				this.ButtonControl.BorderWidth = value;
			}
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x060003EB RID: 1003 RVA: 0x0000B624 File Offset: 0x00009824
		protected virtual TextBox TextBoxControl
		{
			get
			{
				if (this._textBoxControl == null)
				{
					this._textBoxControl = new TextBox();
					this._textBoxControl.ID = this.ID + "_TextBox";
				}
				return this._textBoxControl;
			}
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x060003EC RID: 1004 RVA: 0x0000B65A File Offset: 0x0000985A
		protected virtual ComboBoxButton ButtonControl
		{
			get
			{
				if (this._buttonControl == null)
				{
					this._buttonControl = new ComboBoxButton();
					this._buttonControl.ID = this.ID + "_Button";
				}
				return this._buttonControl;
			}
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x060003ED RID: 1005 RVA: 0x0000B690 File Offset: 0x00009890
		protected virtual HiddenField HiddenFieldControl
		{
			get
			{
				if (this._hiddenFieldControl == null)
				{
					this._hiddenFieldControl = new HiddenField();
					this._hiddenFieldControl.ID = this.ID + "_HiddenField";
				}
				return this._hiddenFieldControl;
			}
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x060003EE RID: 1006 RVA: 0x0000B6C6 File Offset: 0x000098C6
		protected virtual BulletedList OptionListControl
		{
			get
			{
				if (this._optionListControl == null)
				{
					this._optionListControl = new BulletedList();
					this._optionListControl.ID = this.ID + "_OptionList";
				}
				return this._optionListControl;
			}
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x060003EF RID: 1007 RVA: 0x0000B6FC File Offset: 0x000098FC
		protected virtual Table ComboTable
		{
			get
			{
				if (this._comboTable == null)
				{
					this._comboTable = new Table();
					this._comboTable.ID = this.ID + "_Table";
					this._comboTable.Rows.Add(this.ComboTableRow);
				}
				return this._comboTable;
			}
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x060003F0 RID: 1008 RVA: 0x0000B754 File Offset: 0x00009954
		protected virtual TableRow ComboTableRow
		{
			get
			{
				if (this._comboTableRow == null)
				{
					this._comboTableRow = new TableRow();
					this._comboTableRow.Cells.Add(this.ComboTableTextBoxCell);
					this._comboTableRow.Cells.Add(this.ComboTableButtonCell);
				}
				return this._comboTableRow;
			}
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x060003F1 RID: 1009 RVA: 0x0000B7A8 File Offset: 0x000099A8
		protected virtual TableCell ComboTableTextBoxCell
		{
			get
			{
				if (this._comboTableTextBoxCell == null)
				{
					this._comboTableTextBoxCell = new TableCell();
				}
				return this._comboTableTextBoxCell;
			}
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x060003F2 RID: 1010 RVA: 0x0000B7C3 File Offset: 0x000099C3
		protected virtual TableCell ComboTableButtonCell
		{
			get
			{
				if (this._comboTableButtonCell == null)
				{
					this._comboTableButtonCell = new TableCell();
				}
				return this._comboTableButtonCell;
			}
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x0000B7DE File Offset: 0x000099DE
		IEnumerable<ScriptReference> IScriptControl.GetScriptReferences()
		{
			return this.GetScriptReferences();
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x0000B7E6 File Offset: 0x000099E6
		protected virtual IEnumerable<ScriptReference> GetScriptReferences()
		{
			if (!this.Visible)
			{
				return null;
			}
			return ToolkitResourceManager.GetControlScriptReferences(base.GetType());
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x0000B7FD File Offset: 0x000099FD
		IEnumerable<ScriptDescriptor> IScriptControl.GetScriptDescriptors()
		{
			return this.GetScriptDescriptors();
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x0000B808 File Offset: 0x00009A08
		protected virtual IEnumerable<ScriptDescriptor> GetScriptDescriptors()
		{
			if (!this.Visible)
			{
				return null;
			}
			ScriptControlDescriptor scriptControlDescriptor = new ScriptControlDescriptor(this.ClientControlType, this.ClientID);
			ComponentDescriber.DescribeComponent(this, new ScriptComponentDescriptorWrapper(scriptControlDescriptor), this, this);
			scriptControlDescriptor.AddElementProperty("textBoxControl", this.TextBoxControl.ClientID);
			scriptControlDescriptor.AddElementProperty("buttonControl", this.ButtonControl.ClientID);
			scriptControlDescriptor.AddElementProperty("hiddenFieldControl", this.HiddenFieldControl.ClientID);
			scriptControlDescriptor.AddElementProperty("optionListControl", this.OptionListControl.ClientID);
			scriptControlDescriptor.AddElementProperty("comboTableControl", this.ComboTable.ClientID);
			scriptControlDescriptor.AddProperty("autoCompleteMode", this.AutoCompleteMode);
			scriptControlDescriptor.AddProperty("dropDownStyle", this.DropDownStyle);
			return new List<ScriptDescriptor>
			{
				scriptControlDescriptor
			};
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x0000B8E7 File Offset: 0x00009AE7
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			ToolkitResourceManager.RegisterCssReferences(this);
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x0000B8F6 File Offset: 0x00009AF6
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			this.ScriptManager.RegisterScriptControl<ComboBox>(this);
			this.Page.RegisterRequiresPostBack(this);
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x0000B917 File Offset: 0x00009B17
		protected override void Render(HtmlTextWriter writer)
		{
			base.Render(writer);
			if (!base.DesignMode)
			{
				this.ScriptManager.RegisterScriptDescriptors(this);
			}
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x0000B934 File Offset: 0x00009B34
		protected override void CreateChildControls()
		{
			if (this.Controls.Count < 1 || this.Controls[0] != this.ComboTable)
			{
				this.Controls.Clear();
				this.ComboTableTextBoxCell.Controls.Add(this.TextBoxControl);
				this.ComboTableButtonCell.Controls.Add(this.ButtonControl);
				this.Controls.Add(this.ComboTable);
				this.Controls.Add(this.OptionListControl);
				this.Controls.Add(this.HiddenFieldControl);
			}
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x060003FB RID: 1019 RVA: 0x0000B9CD File Offset: 0x00009BCD
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Div;
			}
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x0000B9D1 File Offset: 0x00009BD1
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			this.AddContainerAttributesToRender(writer);
			this.AddTableAttributesToRender(writer);
			this.AddTextBoxAttributesToRender(writer);
			this.AddButtonAttributesToRender(writer);
			this.AddOptionListAttributesToRender(writer);
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x0000B9F6 File Offset: 0x00009BF6
		protected virtual void AddContainerAttributesToRender(HtmlTextWriter writer)
		{
			if (this.RenderMode == ComboBoxRenderMode.Inline)
			{
				base.Style.Add(HtmlTextWriterStyle.Display, this.GetInlineDisplayStyle());
			}
			base.AddAttributesToRender(writer);
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x0000BA1C File Offset: 0x00009C1C
		protected virtual void AddTableAttributesToRender(HtmlTextWriter writer)
		{
			this.ComboTable.CssClass = "ajax__combobox_inputcontainer";
			this.ComboTableTextBoxCell.CssClass = "ajax__combobox_textboxcontainer";
			this.ComboTableButtonCell.CssClass = "ajax__combobox_buttoncontainer";
			this.ComboTable.BorderStyle = BorderStyle.None;
			this.ComboTable.BorderWidth = Unit.Pixel(0);
			if (this.RenderMode == ComboBoxRenderMode.Inline)
			{
				this.ComboTable.Style.Add(HtmlTextWriterStyle.Display, this.GetInlineDisplayStyle());
				if (!base.DesignMode)
				{
					this.ComboTable.Style.Add(HtmlTextWriterStyle.Position, "relative");
					this.ComboTable.Style.Add(HtmlTextWriterStyle.Top, "5px");
				}
			}
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x0000BACC File Offset: 0x00009CCC
		protected virtual void AddTextBoxAttributesToRender(HtmlTextWriter writer)
		{
			this.TextBoxControl.AutoCompleteType = AutoCompleteType.None;
			this.TextBoxControl.Attributes.Add("autocomplete", "off");
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x0000BAF4 File Offset: 0x00009CF4
		protected virtual void AddButtonAttributesToRender(HtmlTextWriter writer)
		{
			if (!base.DesignMode)
			{
				this.ButtonControl.TabIndex = -1;
				return;
			}
			this.ButtonControl.Width = Unit.Pixel(14);
			this.ButtonControl.Height = Unit.Pixel(14);
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x0000BB2F File Offset: 0x00009D2F
		protected virtual void AddOptionListAttributesToRender(HtmlTextWriter writer)
		{
			this.OptionListControl.CssClass = "ajax__combobox_itemlist";
			this.OptionListControl.Style.Add(HtmlTextWriterStyle.Display, "none");
			this.OptionListControl.Style.Add(HtmlTextWriterStyle.Visibility, "hidden");
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x0000BB70 File Offset: 0x00009D70
		public override void RenderControl(HtmlTextWriter writer)
		{
			if (base.DesignMode)
			{
				this.CreateChildControls();
				this.AddAttributesToRender(writer);
				this.ComboTable.RenderControl(writer);
				return;
			}
			this.HiddenFieldControl.Value = this.SelectedIndex.ToString();
			base.RenderControl(writer);
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x0000BBC0 File Offset: 0x00009DC0
		protected override void RenderContents(HtmlTextWriter writer)
		{
			this.ComboTable.RenderControl(writer);
			this.OptionListControl.Items.Clear();
			ListItem[] array = new ListItem[this.Items.Count];
			this.Items.CopyTo(array, 0);
			this.OptionListControl.Items.AddRange(array);
			this.OptionListControl.RenderControl(writer);
			this.HiddenFieldControl.RenderControl(writer);
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x0000BC30 File Offset: 0x00009E30
		bool IPostBackDataHandler.LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			return this.LoadPostData(postDataKey, postCollection);
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x0000BC3A File Offset: 0x00009E3A
		void IPostBackDataHandler.RaisePostDataChangedEvent()
		{
			this.RaisePostDataChangedEvent();
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x0000BC44 File Offset: 0x00009E44
		protected virtual bool LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			if (!this.Enabled)
			{
				return false;
			}
			string[] values = postCollection.GetValues(this.HiddenFieldControl.UniqueID);
			if (values == null)
			{
				return false;
			}
			int num = Convert.ToInt32(values[0], CultureInfo.InvariantCulture);
			this.EnsureDataBound();
			if (num == -2 && (this.DropDownStyle == ComboBoxStyle.Simple || this.DropDownStyle == ComboBoxStyle.DropDown))
			{
				string text = postCollection.GetValues(this.TextBoxControl.UniqueID)[0];
				ComboBoxItemInsertEventArgs comboBoxItemInsertEventArgs = new ComboBoxItemInsertEventArgs(text, this.ItemInsertLocation);
				this.OnItemInserting(comboBoxItemInsertEventArgs);
				if (!comboBoxItemInsertEventArgs.Cancel)
				{
					this.InsertItem(comboBoxItemInsertEventArgs);
				}
				else
				{
					this.TextBoxControl.Text = ((this.SelectedIndex < 0) ? string.Empty : this.SelectedItem.Text);
				}
			}
			else if (num != this.SelectedIndex)
			{
				this.SelectedIndex = num;
				return true;
			}
			return false;
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x0000BD11 File Offset: 0x00009F11
		public virtual void RaisePostDataChangedEvent()
		{
			this.OnSelectedIndexChanged(EventArgs.Empty);
		}

		// Token: 0x1400000B RID: 11
		// (add) Token: 0x06000408 RID: 1032 RVA: 0x0000BD1E File Offset: 0x00009F1E
		// (remove) Token: 0x06000409 RID: 1033 RVA: 0x0000BD31 File Offset: 0x00009F31
		public event EventHandler<ComboBoxItemInsertEventArgs> ItemInserting
		{
			add
			{
				base.Events.AddHandler(ComboBox.EventItemInserting, value);
			}
			remove
			{
				base.Events.RemoveHandler(ComboBox.EventItemInserting, value);
			}
		}

		// Token: 0x1400000C RID: 12
		// (add) Token: 0x0600040A RID: 1034 RVA: 0x0000BD44 File Offset: 0x00009F44
		// (remove) Token: 0x0600040B RID: 1035 RVA: 0x0000BD57 File Offset: 0x00009F57
		public event EventHandler<ComboBoxItemInsertEventArgs> ItemInserted
		{
			add
			{
				base.Events.AddHandler(ComboBox.EventItemInserted, value);
			}
			remove
			{
				base.Events.RemoveHandler(ComboBox.EventItemInserted, value);
			}
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x0000BD6C File Offset: 0x00009F6C
		protected virtual void OnItemInserting(ComboBoxItemInsertEventArgs e)
		{
			EventHandler<ComboBoxItemInsertEventArgs> eventHandler = (EventHandler<ComboBoxItemInsertEventArgs>)base.Events[ComboBox.EventItemInserting];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x0000BD9C File Offset: 0x00009F9C
		protected virtual void OnItemInserted(ComboBoxItemInsertEventArgs e)
		{
			EventHandler<ComboBoxItemInsertEventArgs> eventHandler = (EventHandler<ComboBoxItemInsertEventArgs>)base.Events[ComboBox.EventItemInserted];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x0000BDCC File Offset: 0x00009FCC
		protected virtual void InsertItem(ComboBoxItemInsertEventArgs e)
		{
			if (!e.Cancel)
			{
				int num = -1;
				if (e.InsertLocation == ComboBoxItemInsertLocation.Prepend)
				{
					num = 0;
				}
				else if (e.InsertLocation == ComboBoxItemInsertLocation.Append)
				{
					num = this.Items.Count;
				}
				else
				{
					if (e.InsertLocation == ComboBoxItemInsertLocation.OrdinalText)
					{
						num = 0;
						using (IEnumerator enumerator = this.Items.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								object obj = enumerator.Current;
								ListItem listItem = (ListItem)obj;
								int num2 = string.Compare(e.Item.Text, listItem.Text, StringComparison.Ordinal);
								if (num2 <= 0)
								{
									break;
								}
								num++;
							}
							goto IL_106;
						}
					}
					if (e.InsertLocation == ComboBoxItemInsertLocation.OrdinalValue)
					{
						num = 0;
						foreach (object obj2 in this.Items)
						{
							ListItem listItem2 = (ListItem)obj2;
							int num3 = string.Compare(e.Item.Value, listItem2.Value, StringComparison.Ordinal);
							if (num3 <= 0)
							{
								break;
							}
							num++;
						}
					}
				}
				IL_106:
				if (num >= this.Items.Count)
				{
					this.Items.Add(e.Item);
					this.SelectedIndex = this.Items.Count - 1;
				}
				else
				{
					this.Items.Insert(num, e.Item);
					this.SelectedIndex = num;
				}
				this.OnItemInserted(e);
			}
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x0000BF50 File Offset: 0x0000A150
		private string GetInlineDisplayStyle()
		{
			string text = "inline";
			if (!base.DesignMode && (this.Page.Request.Browser.Browser.ToLower().Contains("safari") || this.Page.Request.Browser.Browser.ToLower().Contains("firefox")))
			{
				text += "-block";
			}
			return text;
		}

		// Token: 0x04000123 RID: 291
		private TextBox _textBoxControl;

		// Token: 0x04000124 RID: 292
		private ScriptManager _scriptManager;

		// Token: 0x04000125 RID: 293
		private ComboBoxButton _buttonControl;

		// Token: 0x04000126 RID: 294
		private HiddenField _hiddenFieldControl;

		// Token: 0x04000127 RID: 295
		private BulletedList _optionListControl;

		// Token: 0x04000128 RID: 296
		private Table _comboTable;

		// Token: 0x04000129 RID: 297
		private TableRow _comboTableRow;

		// Token: 0x0400012A RID: 298
		private TableCell _comboTableTextBoxCell;

		// Token: 0x0400012B RID: 299
		private TableCell _comboTableButtonCell;

		// Token: 0x0400012C RID: 300
		private static readonly object EventItemInserting = new object();

		// Token: 0x0400012D RID: 301
		private static readonly object EventItemInserted = new object();
	}
}
