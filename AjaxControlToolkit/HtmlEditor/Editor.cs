using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit.Design;
using AjaxControlToolkit.HtmlEditor.Popups;
using AjaxControlToolkit.HtmlEditor.ToolbarButtons;
using AjaxControlToolkit.ToolboxIcons;

namespace AjaxControlToolkit.HtmlEditor
{
	// Token: 0x020000D1 RID: 209
	[RequiredScript(typeof(RegisteredField))]
	[RequiredScript(typeof(PopupBGIButton))]
	[ClientScriptResource("Sys.Extended.UI.HtmlEditor.Editor", "HtmlEditor.Editor")]
	[ToolboxBitmap(typeof(Accessor), "HtmlEditor.bmp")]
	[Obsolete("HtmlEditor is obsolete. Use HtmlEditorExtender instead.")]
	[Designer("AjaxControlToolkit.Design.EditorDesigner, AjaxControlToolkit")]
	[ToolboxItem(false)]
	[ValidationProperty("Content")]
	[ClientCssResource("HtmlEditor.Editor")]
	[RequiredScript(typeof(CommonToolkitScripts))]
	[RequiredScript(typeof(Enums))]
	[RequiredScript(typeof(BackColorClear))]
	[RequiredScript(typeof(BackColorSelector))]
	[RequiredScript(typeof(Bold))]
	[RequiredScript(typeof(BoxButton))]
	[RequiredScript(typeof(AjaxControlToolkit.HtmlEditor.ToolbarButtons.BulletedList))]
	[RequiredScript(typeof(ColorButton))]
	[RequiredScript(typeof(ColorSelector))]
	[RequiredScript(typeof(CommonButton))]
	[RequiredScript(typeof(Copy))]
	[RequiredScript(typeof(Cut))]
	[RequiredScript(typeof(DecreaseIndent))]
	[RequiredScript(typeof(DesignMode))]
	[RequiredScript(typeof(DesignModeBoxButton))]
	[RequiredScript(typeof(DesignModeImageButton))]
	[RequiredScript(typeof(DesignModePopupImageButton))]
	[RequiredScript(typeof(DesignModeSelectButton))]
	[RequiredScript(typeof(EditorToggleButton))]
	[RequiredScript(typeof(FixedBackColor))]
	[RequiredScript(typeof(FixedColorButton))]
	[RequiredScript(typeof(FixedForeColor))]
	[RequiredScript(typeof(FontName))]
	[RequiredScript(typeof(AjaxControlToolkit.HtmlEditor.ToolbarButtons.FontSize))]
	[RequiredScript(typeof(ForeColor))]
	[RequiredScript(typeof(ForeColorClear))]
	[RequiredScript(typeof(ForeColorSelector))]
	[RequiredScript(typeof(HorizontalSeparator))]
	[RequiredScript(typeof(HtmlMode))]
	[RequiredScript(typeof(AjaxControlToolkit.HtmlEditor.ToolbarButtons.ImageButton))]
	[RequiredScript(typeof(IncreaseIndent))]
	[RequiredScript(typeof(InsertHR))]
	[RequiredScript(typeof(PopupBoxButton))]
	[RequiredScript(typeof(InsertLink))]
	[RequiredScript(typeof(Italic))]
	[RequiredScript(typeof(JustifyCenter))]
	[RequiredScript(typeof(PopupCommonButton))]
	[RequiredScript(typeof(JustifyFull))]
	[RequiredScript(typeof(JustifyLeft))]
	[RequiredScript(typeof(JustifyRight))]
	[RequiredScript(typeof(Ltr))]
	[RequiredScript(typeof(MethodButton))]
	[RequiredScript(typeof(ModeButton))]
	[RequiredScript(typeof(OkCancelPopupButton))]
	[RequiredScript(typeof(OrderedList))]
	[RequiredScript(typeof(Paragraph))]
	[RequiredScript(typeof(Paste))]
	[RequiredScript(typeof(PasteText))]
	[RequiredScript(typeof(PasteWord))]
	[RequiredScript(typeof(PreviewMode))]
	[RequiredScript(typeof(Redo))]
	[RequiredScript(typeof(RemoveAlignment))]
	[RequiredScript(typeof(RemoveLink))]
	[RequiredScript(typeof(RemoveStyles))]
	[RequiredScript(typeof(Rtl))]
	[RequiredScript(typeof(SelectButton))]
	[RequiredScript(typeof(SelectOption))]
	[RequiredScript(typeof(Selector))]
	[RequiredScript(typeof(StrikeThrough))]
	[RequiredScript(typeof(SubScript))]
	[RequiredScript(typeof(SuperScript))]
	[RequiredScript(typeof(Underline))]
	[RequiredScript(typeof(Undo))]
	[RequiredScript(typeof(AttachedPopup))]
	[RequiredScript(typeof(AttachedTemplatePopup))]
	[RequiredScript(typeof(BaseColorsPopup))]
	[RequiredScript(typeof(LinkProperties))]
	[RequiredScript(typeof(OkCancelAttachedTemplatePopup))]
	[RequiredScript(typeof(Popup))]
	public class Editor : ScriptControlBase
	{
		// Token: 0x060005CE RID: 1486 RVA: 0x0000F10F File Offset: 0x0000D30F
		public Editor() : base(false, HtmlTextWriterTag.Div)
		{
		}

		// Token: 0x1400000E RID: 14
		// (add) Token: 0x060005CF RID: 1487 RVA: 0x0000F11A File Offset: 0x0000D31A
		// (remove) Token: 0x060005D0 RID: 1488 RVA: 0x0000F132 File Offset: 0x0000D332
		[Category("Behavior")]
		public event ContentChangedEventHandler ContentChanged
		{
			add
			{
				this.EditPanel.Events.AddHandler(EditPanel.EventContentChanged, value);
			}
			remove
			{
				this.EditPanel.Events.RemoveHandler(EditPanel.EventContentChanged, value);
			}
		}

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x060005D1 RID: 1489 RVA: 0x0000F14C File Offset: 0x0000D34C
		protected bool IsDesign
		{
			get
			{
				bool result;
				try
				{
					bool flag = this.Context == null || (base.Site != null && base.Site.DesignMode);
					result = flag;
				}
				catch
				{
					result = true;
				}
				return result;
			}
		}

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x060005D2 RID: 1490 RVA: 0x0000F19C File Offset: 0x0000D39C
		// (set) Token: 0x060005D3 RID: 1491 RVA: 0x0000F1A9 File Offset: 0x0000D3A9
		[DefaultValue(false)]
		[Category("Behavior")]
		public virtual bool SuppressTabInDesignMode
		{
			get
			{
				return this.EditPanel.SuppressTabInDesignMode;
			}
			set
			{
				this.EditPanel.SuppressTabInDesignMode = value;
			}
		}

		// Token: 0x1700025B RID: 603
		// (get) Token: 0x060005D4 RID: 1492 RVA: 0x0000F1B7 File Offset: 0x0000D3B7
		// (set) Token: 0x060005D5 RID: 1493 RVA: 0x0000F1D8 File Offset: 0x0000D3D8
		[DefaultValue(false)]
		public virtual bool TopToolbarPreservePlace
		{
			get
			{
				return (bool)(this.ViewState["TopToolbarPreservePlace"] ?? false);
			}
			set
			{
				this.ViewState["TopToolbarPreservePlace"] = value;
			}
		}

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x060005D6 RID: 1494 RVA: 0x0000F1F0 File Offset: 0x0000D3F0
		// (set) Token: 0x060005D7 RID: 1495 RVA: 0x0000F211 File Offset: 0x0000D411
		[DefaultValue(false)]
		[Category("Behavior")]
		public virtual bool IgnoreTab
		{
			get
			{
				return (bool)(this.ViewState["IgnoreTab"] ?? false);
			}
			set
			{
				this.ViewState["IgnoreTab"] = value;
			}
		}

		// Token: 0x1700025D RID: 605
		// (get) Token: 0x060005D8 RID: 1496 RVA: 0x0000F229 File Offset: 0x0000D429
		// (set) Token: 0x060005D9 RID: 1497 RVA: 0x0000F249 File Offset: 0x0000D449
		[Category("Appearance")]
		[Description("Folder used for toolbar's buttons' images")]
		[DefaultValue("")]
		public virtual string ButtonImagesFolder
		{
			get
			{
				return (string)(this.ViewState["ButtonImagesFolder"] ?? string.Empty);
			}
			set
			{
				this.ViewState["ButtonImagesFolder"] = value;
			}
		}

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x060005DA RID: 1498 RVA: 0x0000F25C File Offset: 0x0000D45C
		// (set) Token: 0x060005DB RID: 1499 RVA: 0x0000F269 File Offset: 0x0000D469
		[Category("Behavior")]
		[DefaultValue(false)]
		public virtual bool NoUnicode
		{
			get
			{
				return this.EditPanel.NoUnicode;
			}
			set
			{
				this.EditPanel.NoUnicode = value;
			}
		}

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x060005DC RID: 1500 RVA: 0x0000F277 File Offset: 0x0000D477
		// (set) Token: 0x060005DD RID: 1501 RVA: 0x0000F284 File Offset: 0x0000D484
		[DefaultValue(false)]
		[Category("Behavior")]
		public virtual bool NoScript
		{
			get
			{
				return this.EditPanel.NoScript;
			}
			set
			{
				this.EditPanel.NoScript = value;
			}
		}

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x060005DE RID: 1502 RVA: 0x0000F292 File Offset: 0x0000D492
		// (set) Token: 0x060005DF RID: 1503 RVA: 0x0000F29F File Offset: 0x0000D49F
		[DefaultValue(false)]
		[Category("Behavior")]
		public virtual bool InitialCleanUp
		{
			get
			{
				return this.EditPanel.InitialCleanUp;
			}
			set
			{
				this.EditPanel.InitialCleanUp = value;
			}
		}

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x060005E0 RID: 1504 RVA: 0x0000F2AD File Offset: 0x0000D4AD
		// (set) Token: 0x060005E1 RID: 1505 RVA: 0x0000F2BA File Offset: 0x0000D4BA
		[DefaultValue("ajax__htmleditor_htmlpanel_default")]
		[Category("Appearance")]
		public virtual string HtmlPanelCssClass
		{
			get
			{
				return this.EditPanel.HtmlPanelCssClass;
			}
			set
			{
				this.EditPanel.HtmlPanelCssClass = value;
			}
		}

		// Token: 0x17000262 RID: 610
		// (get) Token: 0x060005E2 RID: 1506 RVA: 0x0000F2C8 File Offset: 0x0000D4C8
		// (set) Token: 0x060005E3 RID: 1507 RVA: 0x0000F2D5 File Offset: 0x0000D4D5
		[Category("Appearance")]
		[DefaultValue("")]
		public virtual string DocumentCssPath
		{
			get
			{
				return this.EditPanel.DocumentCssPath;
			}
			set
			{
				this.EditPanel.DocumentCssPath = value;
			}
		}

		// Token: 0x17000263 RID: 611
		// (get) Token: 0x060005E4 RID: 1508 RVA: 0x0000F2E3 File Offset: 0x0000D4E3
		// (set) Token: 0x060005E5 RID: 1509 RVA: 0x0000F2F0 File Offset: 0x0000D4F0
		[Category("Appearance")]
		[DefaultValue("")]
		public virtual string DesignPanelCssPath
		{
			get
			{
				return this.EditPanel.DesignPanelCssPath;
			}
			set
			{
				this.EditPanel.DesignPanelCssPath = value;
			}
		}

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x060005E6 RID: 1510 RVA: 0x0000F2FE File Offset: 0x0000D4FE
		// (set) Token: 0x060005E7 RID: 1511 RVA: 0x0000F30B File Offset: 0x0000D50B
		[DefaultValue(true)]
		[Category("Behavior")]
		public virtual bool AutoFocus
		{
			get
			{
				return this.EditPanel.AutoFocus;
			}
			set
			{
				this.EditPanel.AutoFocus = value;
			}
		}

		// Token: 0x17000265 RID: 613
		// (get) Token: 0x060005E8 RID: 1512 RVA: 0x0000F319 File Offset: 0x0000D519
		// (set) Token: 0x060005E9 RID: 1513 RVA: 0x0000F326 File Offset: 0x0000D526
		[DefaultValue("")]
		[Category("Appearance")]
		public virtual string Content
		{
			get
			{
				return this.EditPanel.Content;
			}
			set
			{
				this.EditPanel.Content = value;
			}
		}

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x060005EA RID: 1514 RVA: 0x0000F334 File Offset: 0x0000D534
		// (set) Token: 0x060005EB RID: 1515 RVA: 0x0000F341 File Offset: 0x0000D541
		[Category("Behavior")]
		[DefaultValue(ActiveModeType.Design)]
		public virtual ActiveModeType ActiveMode
		{
			get
			{
				return this.EditPanel.ActiveMode;
			}
			set
			{
				this.EditPanel.ActiveMode = value;
			}
		}

		// Token: 0x17000267 RID: 615
		// (get) Token: 0x060005EC RID: 1516 RVA: 0x0000F34F File Offset: 0x0000D54F
		// (set) Token: 0x060005ED RID: 1517 RVA: 0x0000F35C File Offset: 0x0000D55C
		[DefaultValue("")]
		[Category("Behavior")]
		public virtual string OnClientActiveModeChanged
		{
			get
			{
				return this.EditPanel.OnClientActiveModeChanged;
			}
			set
			{
				this.EditPanel.OnClientActiveModeChanged = value;
			}
		}

		// Token: 0x17000268 RID: 616
		// (get) Token: 0x060005EE RID: 1518 RVA: 0x0000F36A File Offset: 0x0000D56A
		// (set) Token: 0x060005EF RID: 1519 RVA: 0x0000F377 File Offset: 0x0000D577
		[Category("Behavior")]
		[DefaultValue("")]
		public virtual string OnClientBeforeActiveModeChanged
		{
			get
			{
				return this.EditPanel.OnClientBeforeActiveModeChanged;
			}
			set
			{
				this.EditPanel.OnClientBeforeActiveModeChanged = value;
			}
		}

		// Token: 0x17000269 RID: 617
		// (get) Token: 0x060005F0 RID: 1520 RVA: 0x0000F385 File Offset: 0x0000D585
		// (set) Token: 0x060005F1 RID: 1521 RVA: 0x0000F38D File Offset: 0x0000D58D
		[DefaultValue(typeof(Unit), "")]
		[Category("Appearance")]
		public override Unit Height
		{
			get
			{
				return base.Height;
			}
			set
			{
				base.Height = value;
			}
		}

		// Token: 0x1700026A RID: 618
		// (get) Token: 0x060005F2 RID: 1522 RVA: 0x0000F396 File Offset: 0x0000D596
		// (set) Token: 0x060005F3 RID: 1523 RVA: 0x0000F39E File Offset: 0x0000D59E
		[Category("Appearance")]
		[DefaultValue(typeof(Unit), "")]
		public override Unit Width
		{
			get
			{
				return base.Width;
			}
			set
			{
				base.Width = value;
			}
		}

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x060005F4 RID: 1524 RVA: 0x0000F3A7 File Offset: 0x0000D5A7
		// (set) Token: 0x060005F5 RID: 1525 RVA: 0x0000F3AF File Offset: 0x0000D5AF
		[DefaultValue("ajax__htmleditor_editor_default")]
		[Category("Appearance")]
		public override string CssClass
		{
			get
			{
				return base.CssClass;
			}
			set
			{
				base.CssClass = value;
			}
		}

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x060005F6 RID: 1526 RVA: 0x0000F3B8 File Offset: 0x0000D5B8
		internal EditPanel EditPanel
		{
			get
			{
				if (this._editPanel == null)
				{
					this._editPanel = new EditPanelInstance();
				}
				return this._editPanel;
			}
		}

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x060005F7 RID: 1527 RVA: 0x0000F3D3 File Offset: 0x0000D5D3
		protected Toolbar BottomToolbar
		{
			get
			{
				if (this._bottomToolbar == null)
				{
					this._bottomToolbar = new ToolbarInstance();
				}
				return this._bottomToolbar;
			}
		}

		// Token: 0x1700026E RID: 622
		// (get) Token: 0x060005F8 RID: 1528 RVA: 0x0000F3EE File Offset: 0x0000D5EE
		protected Toolbar TopToolbar
		{
			get
			{
				if (this._topToolbar == null)
				{
					this._topToolbar = new ToolbarInstance();
				}
				return this._topToolbar;
			}
		}

		// Token: 0x060005F9 RID: 1529 RVA: 0x0000F40C File Offset: 0x0000D60C
		protected override Style CreateControlStyle()
		{
			return new Editor.EditorStyle(this.ViewState)
			{
				CssClass = "ajax__htmleditor_editor_default"
			};
		}

		// Token: 0x060005FA RID: 1530 RVA: 0x0000F431 File Offset: 0x0000D631
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			if (!base.ControlStyleCreated || this.IsDesign)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Class, (this.IsDesign ? "ajax__htmleditor_editor_base " : "") + "ajax__htmleditor_editor_default");
			}
			base.AddAttributesToRender(writer);
		}

		// Token: 0x060005FB RID: 1531 RVA: 0x0000F470 File Offset: 0x0000D670
		protected override void DescribeComponent(ScriptComponentDescriptor descriptor)
		{
			base.DescribeComponent(descriptor);
			descriptor.AddComponentProperty("editPanel", this.EditPanel.ClientID);
			if (this._changingToolbar != null)
			{
				descriptor.AddComponentProperty("changingToolbar", this._changingToolbar.ClientID);
			}
		}

		// Token: 0x060005FC RID: 1532 RVA: 0x0000F4B0 File Offset: 0x0000D6B0
		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			this.EditPanel.Toolbars.Add(this.BottomToolbar);
			this._changingToolbar = this.TopToolbar;
			this.EditPanel.Toolbars.Add(this.TopToolbar);
			Table table = new Table();
			table.CellPadding = 0;
			table.CellSpacing = 0;
			table.CssClass = "ajax__htmleditor_editor_container";
			table.Style[HtmlTextWriterStyle.BorderCollapse] = "separate";
			TableRow tableRow = this._topToolbarRow = new TableRow();
			TableCell tableCell = new TableCell();
			tableCell.Controls.Add(this.TopToolbar);
			tableCell.CssClass = "ajax__htmleditor_editor_toptoolbar";
			tableRow.Cells.Add(tableCell);
			table.Rows.Add(tableRow);
			tableRow = new TableRow();
			tableCell = (this._editPanelCell = new TableCell());
			tableCell.CssClass = "ajax__htmleditor_editor_editpanel";
			tableCell.Controls.Add(this.EditPanel);
			tableRow.Cells.Add(tableCell);
			table.Rows.Add(tableRow);
			tableRow = (this._bottomToolbarRow = new TableRow());
			tableCell = new TableCell();
			tableCell.Controls.Add(this.BottomToolbar);
			tableCell.CssClass = "ajax__htmleditor_editor_bottomtoolbar";
			tableRow.Cells.Add(tableCell);
			table.Rows.Add(tableRow);
			this.Controls.Add(table);
		}

		// Token: 0x060005FD RID: 1533 RVA: 0x0000F614 File Offset: 0x0000D814
		protected virtual void FillBottomToolbar()
		{
			this.BottomToolbar.Buttons.Add(new DesignMode());
			this.BottomToolbar.Buttons.Add(new HtmlMode());
			this.BottomToolbar.Buttons.Add(new PreviewMode());
		}

		// Token: 0x060005FE RID: 1534 RVA: 0x0000F660 File Offset: 0x0000D860
		protected virtual void FillTopToolbar()
		{
			this.TopToolbar.Buttons.Add(new Undo());
			this.TopToolbar.Buttons.Add(new Redo());
			this.TopToolbar.Buttons.Add(new HorizontalSeparator());
			this.TopToolbar.Buttons.Add(new Bold());
			this.TopToolbar.Buttons.Add(new Italic());
			this.TopToolbar.Buttons.Add(new Underline());
			this.TopToolbar.Buttons.Add(new StrikeThrough());
			this.TopToolbar.Buttons.Add(new SubScript());
			this.TopToolbar.Buttons.Add(new SuperScript());
			this.TopToolbar.Buttons.Add(new HorizontalSeparator());
			this.TopToolbar.Buttons.Add(new Ltr());
			this.TopToolbar.Buttons.Add(new Rtl());
			this.TopToolbar.Buttons.Add(new HorizontalSeparator());
			FixedForeColor fixedForeColor = new FixedForeColor();
			this.TopToolbar.Buttons.Add(fixedForeColor);
			ForeColorSelector foreColorSelector = new ForeColorSelector();
			foreColorSelector.FixedColorButtonId = (fixedForeColor.ID = "FixedForeColor");
			this.TopToolbar.Buttons.Add(foreColorSelector);
			this.TopToolbar.Buttons.Add(new ForeColorClear());
			this.TopToolbar.Buttons.Add(new HorizontalSeparator());
			FixedBackColor fixedBackColor = new FixedBackColor();
			this.TopToolbar.Buttons.Add(fixedBackColor);
			BackColorSelector backColorSelector = new BackColorSelector();
			backColorSelector.FixedColorButtonId = (fixedBackColor.ID = "FixedBackColor");
			this.TopToolbar.Buttons.Add(backColorSelector);
			this.TopToolbar.Buttons.Add(new BackColorClear());
			this.TopToolbar.Buttons.Add(new HorizontalSeparator());
			this.TopToolbar.Buttons.Add(new RemoveStyles());
			this.TopToolbar.Buttons.Add(new HorizontalSeparator());
			FontName fontName = new FontName();
			this.TopToolbar.Buttons.Add(fontName);
			Collection<SelectOption> options = fontName.Options;
			SelectOption item = new SelectOption
			{
				Text = "Arial",
				Value = "arial,helvetica,sans-serif"
			};
			options.Add(item);
			item = new SelectOption
			{
				Text = "Courier New",
				Value = "courier new,courier,monospace"
			};
			options.Add(item);
			item = new SelectOption
			{
				Text = "Georgia",
				Value = "georgia,times new roman,times,serif"
			};
			options.Add(item);
			item = new SelectOption
			{
				Text = "Tahoma",
				Value = "tahoma,arial,helvetica,sans-serif"
			};
			options.Add(item);
			item = new SelectOption
			{
				Text = "Times New Roman",
				Value = "times new roman,times,serif"
			};
			options.Add(item);
			item = new SelectOption
			{
				Text = "Verdana",
				Value = "verdana,arial,helvetica,sans-serif"
			};
			options.Add(item);
			item = new SelectOption
			{
				Text = "Impact",
				Value = "impact"
			};
			options.Add(item);
			item = new SelectOption
			{
				Text = "WingDings",
				Value = "wingdings"
			};
			options.Add(item);
			this.TopToolbar.Buttons.Add(new HorizontalSeparator());
			AjaxControlToolkit.HtmlEditor.ToolbarButtons.FontSize fontSize = new AjaxControlToolkit.HtmlEditor.ToolbarButtons.FontSize();
			this.TopToolbar.Buttons.Add(fontSize);
			options = fontSize.Options;
			item = new SelectOption
			{
				Text = "1 ( 8 pt)",
				Value = "8pt"
			};
			options.Add(item);
			item = new SelectOption
			{
				Text = "2 (10 pt)",
				Value = "10pt"
			};
			options.Add(item);
			item = new SelectOption
			{
				Text = "3 (12 pt)",
				Value = "12pt"
			};
			options.Add(item);
			item = new SelectOption
			{
				Text = "4 (14 pt)",
				Value = "14pt"
			};
			options.Add(item);
			item = new SelectOption
			{
				Text = "5 (18 pt)",
				Value = "18pt"
			};
			options.Add(item);
			item = new SelectOption
			{
				Text = "6 (24 pt)",
				Value = "24pt"
			};
			options.Add(item);
			item = new SelectOption
			{
				Text = "7 (36 pt)",
				Value = "36pt"
			};
			options.Add(item);
			this.TopToolbar.Buttons.Add(new HorizontalSeparator());
			this.TopToolbar.Buttons.Add(new Cut());
			this.TopToolbar.Buttons.Add(new Copy());
			this.TopToolbar.Buttons.Add(new Paste());
			this.TopToolbar.Buttons.Add(new PasteText());
			this.TopToolbar.Buttons.Add(new PasteWord());
			this.TopToolbar.Buttons.Add(new HorizontalSeparator());
			this.TopToolbar.Buttons.Add(new DecreaseIndent());
			this.TopToolbar.Buttons.Add(new IncreaseIndent());
			this.TopToolbar.Buttons.Add(new HorizontalSeparator());
			this.TopToolbar.Buttons.Add(new Paragraph());
			this.TopToolbar.Buttons.Add(new JustifyLeft());
			this.TopToolbar.Buttons.Add(new JustifyCenter());
			this.TopToolbar.Buttons.Add(new JustifyRight());
			this.TopToolbar.Buttons.Add(new JustifyFull());
			this.TopToolbar.Buttons.Add(new RemoveAlignment());
			this.TopToolbar.Buttons.Add(new HorizontalSeparator());
			this.TopToolbar.Buttons.Add(new OrderedList());
			this.TopToolbar.Buttons.Add(new AjaxControlToolkit.HtmlEditor.ToolbarButtons.BulletedList());
			this.TopToolbar.Buttons.Add(new HorizontalSeparator());
			this.TopToolbar.Buttons.Add(new InsertHR());
			this.TopToolbar.Buttons.Add(new InsertLink());
			this.TopToolbar.Buttons.Add(new RemoveLink());
		}

		// Token: 0x060005FF RID: 1535 RVA: 0x0000FD2C File Offset: 0x0000DF2C
		protected override void CreateChildControls()
		{
			this.BottomToolbar.Buttons.Clear();
			this.FillBottomToolbar();
			if (this.BottomToolbar.Buttons.Count == 0)
			{
				if (this.EditPanel.Toolbars.Contains(this.BottomToolbar))
				{
					this.EditPanel.Toolbars.Remove(this.BottomToolbar);
				}
				this._bottomToolbarRow.Visible = false;
				(this.EditPanel.Parent as TableCell).Style["border-bottom-width"] = "0";
			}
			else
			{
				this.BottomToolbar.AlwaysVisible = true;
				this.BottomToolbar.ButtonImagesFolder = this.ButtonImagesFolder;
				for (int i = 0; i < this.BottomToolbar.Buttons.Count; i++)
				{
					this.BottomToolbar.Buttons[i].IgnoreTab = this.IgnoreTab;
				}
			}
			this.TopToolbar.Buttons.Clear();
			this.FillTopToolbar();
			if (this.TopToolbar.Buttons.Count == 0)
			{
				if (this.EditPanel.Toolbars.Contains(this.TopToolbar))
				{
					this.EditPanel.Toolbars.Remove(this.TopToolbar);
				}
				this._topToolbarRow.Visible = false;
				(this.EditPanel.Parent as TableCell).Style["border-top-width"] = "0";
				this._changingToolbar = null;
			}
			else
			{
				this.TopToolbar.ButtonImagesFolder = this.ButtonImagesFolder;
				for (int j = 0; j < this.TopToolbar.Buttons.Count; j++)
				{
					this.TopToolbar.Buttons[j].IgnoreTab = this.IgnoreTab;
					this.TopToolbar.Buttons[j].PreservePlace = this.TopToolbarPreservePlace;
				}
			}
			if (!this.Height.IsEmpty)
			{
				(this.Controls[0] as Table).Style.Add(HtmlTextWriterStyle.Height, this.Height.ToString());
			}
			if (!this.Width.IsEmpty)
			{
				(this.Controls[0] as Table).Style.Add(HtmlTextWriterStyle.Width, this.Width.ToString());
			}
			if (EditPanel.IE(this.Page) && !this.IsDesign)
			{
				this._editPanelCell.Style[HtmlTextWriterStyle.Height] = "expression(Sys.Extended.UI.HtmlEditor.Editor.MidleCellHeightForIE(this.parentNode.parentNode.parentNode,this.parentNode))";
			}
			this.EditPanel.IgnoreTab = this.IgnoreTab;
		}

		// Token: 0x06000600 RID: 1536 RVA: 0x0000FFCC File Offset: 0x0000E1CC
		protected override void OnPreRender(EventArgs e)
		{
			try
			{
				base.OnPreRender(e);
			}
			catch
			{
			}
			this._wasPreRender = true;
		}

		// Token: 0x06000601 RID: 1537 RVA: 0x0000FFFC File Offset: 0x0000E1FC
		protected override void Render(HtmlTextWriter writer)
		{
			if (!this._wasPreRender)
			{
				this.OnPreRender(new EventArgs());
			}
			base.Render(writer);
		}

		// Token: 0x06000602 RID: 1538 RVA: 0x00010018 File Offset: 0x0000E218
		internal void CreateChilds(DesignerWithMapPath designer)
		{
			this.CreateChildControls();
			this.TopToolbar.CreateChilds(designer);
			this.BottomToolbar.CreateChilds(designer);
			this.EditPanel.SetDesigner(designer);
		}

		// Token: 0x040002D5 RID: 725
		internal Toolbar _bottomToolbar;

		// Token: 0x040002D6 RID: 726
		internal Toolbar _topToolbar;

		// Token: 0x040002D7 RID: 727
		private EditPanel _editPanel;

		// Token: 0x040002D8 RID: 728
		private Toolbar _changingToolbar;

		// Token: 0x040002D9 RID: 729
		private TableCell _editPanelCell;

		// Token: 0x040002DA RID: 730
		private TableRow _topToolbarRow;

		// Token: 0x040002DB RID: 731
		private TableRow _bottomToolbarRow;

		// Token: 0x040002DC RID: 732
		private bool _wasPreRender;

		// Token: 0x020000D2 RID: 210
		private sealed class EditorStyle : Style
		{
			// Token: 0x06000603 RID: 1539 RVA: 0x00010044 File Offset: 0x0000E244
			public EditorStyle(StateBag state) : base(state)
			{
			}

			// Token: 0x06000604 RID: 1540 RVA: 0x0001004D File Offset: 0x0000E24D
			protected override void FillStyleAttributes(CssStyleCollection attributes, IUrlResolutionService urlResolver)
			{
				base.FillStyleAttributes(attributes, urlResolver);
				attributes.Remove(HtmlTextWriterStyle.Height);
				attributes.Remove(HtmlTextWriterStyle.Width);
			}
		}
	}
}
