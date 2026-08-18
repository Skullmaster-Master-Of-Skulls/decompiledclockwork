using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Serialization;
using Telerik.Web.UI.Menu;
using Telerik.Web.UI.Menu.Renderers;

namespace Telerik.Web.UI
{
	// Token: 0x020011BB RID: 4539
	[DefaultProperty("Text")]
	[XmlRoot("Item")]
	[ToolboxItem(false)]
	public class RadMenuItem : NavigationItem, ICloneable, IRadMenuItemContainer
	{
		// Token: 0x17003C12 RID: 15378
		// (get) Token: 0x0600BA52 RID: 47698 RVA: 0x00297CBA File Offset: 0x00295EBA
		[DefaultValue(null)]
		[Browsable(false)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[MergableProperty(false)]
		public RadMenuItemCollection Items
		{
			get
			{
				return (RadMenuItemCollection)base.Children;
			}
		}

		// Token: 0x17003C13 RID: 15379
		// (get) Token: 0x0600BA53 RID: 47699 RVA: 0x00297CC7 File Offset: 0x00295EC7
		// (set) Token: 0x0600BA54 RID: 47700 RVA: 0x00297CCF File Offset: 0x00295ECF
		[Browsable(false)]
		public IRadMenuItemContainer Owner { get; internal set; }

		// Token: 0x17003C14 RID: 15380
		// (get) Token: 0x0600BA55 RID: 47701 RVA: 0x00297CD8 File Offset: 0x00295ED8
		// (set) Token: 0x0600BA56 RID: 47702 RVA: 0x00297CE0 File Offset: 0x00295EE0
		[Browsable(false)]
		public override object DataItem
		{
			get
			{
				return base.DataItem;
			}
			set
			{
				base.DataItem = value;
			}
		}

		// Token: 0x17003C15 RID: 15381
		// (get) Token: 0x0600BA57 RID: 47703 RVA: 0x00297CE9 File Offset: 0x00295EE9
		// (set) Token: 0x0600BA58 RID: 47704 RVA: 0x00297CF1 File Offset: 0x00295EF1
		[Description("The display text of the item.")]
		public override string Text
		{
			get
			{
				return base.Text;
			}
			set
			{
				base.Text = value;
			}
		}

		// Token: 0x17003C16 RID: 15382
		// (get) Token: 0x0600BA59 RID: 47705 RVA: 0x00297CFA File Offset: 0x00295EFA
		// (set) Token: 0x0600BA5A RID: 47706 RVA: 0x00297D02 File Offset: 0x00295F02
		[Description("The value of the menu item")]
		public override string Value
		{
			get
			{
				return base.Value;
			}
			set
			{
				base.Value = value;
			}
		}

		// Token: 0x17003C17 RID: 15383
		// (get) Token: 0x0600BA5B RID: 47707 RVA: 0x00297D0B File Offset: 0x00295F0B
		// (set) Token: 0x0600BA5C RID: 47708 RVA: 0x00297D13 File Offset: 0x00295F13
		[TemplateContainer(typeof(RadMenuItem))]
		[Bindable(false)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Browsable(false)]
		public ITemplate ItemTemplate
		{
			get
			{
				return this.Template;
			}
			set
			{
				this.Template = value;
			}
		}

		// Token: 0x17003C18 RID: 15384
		// (get) Token: 0x0600BA5D RID: 47709 RVA: 0x00297D1C File Offset: 0x00295F1C
		// (set) Token: 0x0600BA5E RID: 47710 RVA: 0x00297D24 File Offset: 0x00295F24
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Bindable(false)]
		[TemplateInstance(TemplateInstance.Single)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TemplateContainer(typeof(MenuItemContentTemplateContainer))]
		[Browsable(false)]
		public virtual ITemplate ContentTemplate
		{
			get
			{
				return this._contentTemplate;
			}
			set
			{
				this._contentTemplate = value;
			}
		}

		// Token: 0x17003C19 RID: 15385
		// (get) Token: 0x0600BA5F RID: 47711 RVA: 0x00297D2D File Offset: 0x00295F2D
		[Browsable(false)]
		public MenuItemContentTemplateContainer ContentTemplateContainer
		{
			get
			{
				if (this._content == null)
				{
					this._content = new MenuItemContentTemplateContainer(this);
					this.EnsureChildControls();
				}
				return this._content;
			}
		}

		// Token: 0x17003C1A RID: 15386
		// (get) Token: 0x0600BA60 RID: 47712 RVA: 0x00297D4F File Offset: 0x00295F4F
		[Category("Layout")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Description("Child item settings")]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public RadMenuItemGroupSettings GroupSettings
		{
			get
			{
				return this._groupSettings;
			}
		}

		// Token: 0x17003C1B RID: 15387
		// (get) Token: 0x0600BA61 RID: 47713 RVA: 0x00297D57 File Offset: 0x00295F57
		// (set) Token: 0x0600BA62 RID: 47714 RVA: 0x00297D78 File Offset: 0x00295F78
		[Description("The expand behavior of the item")]
		[DefaultValue(MenuItemExpandMode.ClientSide)]
		[Category("Behavior")]
		public MenuItemExpandMode ExpandMode
		{
			get
			{
				return (MenuItemExpandMode)(this.ViewState["ExpandMode"] ?? MenuItemExpandMode.ClientSide);
			}
			set
			{
				this.ViewState["ExpandMode"] = value;
			}
		}

		// Token: 0x17003C1C RID: 15388
		// (get) Token: 0x0600BA63 RID: 47715 RVA: 0x00297D90 File Offset: 0x00295F90
		// (set) Token: 0x0600BA64 RID: 47716 RVA: 0x00297DB1 File Offset: 0x00295FB1
		[DefaultValue(false)]
		[Description("A value indicating if an image sprite container should be used instead of the default image")]
		[ClientPropertyName("enableImageSprite")]
		[Category("Behavior")]
		[ClientControlProperty]
		public bool EnableImageSprite
		{
			get
			{
				return (bool)(this.ViewState["EnableImageSprite"] ?? false);
			}
			set
			{
				this.ViewState["EnableImageSprite"] = value;
			}
		}

		// Token: 0x17003C1D RID: 15389
		// (get) Token: 0x0600BA65 RID: 47717 RVA: 0x00297DC9 File Offset: 0x00295FC9
		internal bool EnableImageSpriteResolved
		{
			get
			{
				if (this.ViewState["EnableImageSprite"] == null)
				{
					return this.Menu != null && this.Menu.EnableImageSprites;
				}
				return this.EnableImageSprite;
			}
		}

		// Token: 0x17003C1E RID: 15390
		// (get) Token: 0x0600BA66 RID: 47718 RVA: 0x00297DF9 File Offset: 0x00295FF9
		// (set) Token: 0x0600BA67 RID: 47719 RVA: 0x00297E01 File Offset: 0x00296001
		[Description("The URL to which the menu item navigates when selected.")]
		[UrlProperty]
		[Category("Navigation")]
		[Editor("Telerik.Web.Design.ControlItemUrlEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[DefaultValue("")]
		[Bindable(true)]
		public override string NavigateUrl
		{
			get
			{
				return base.NavigateUrl;
			}
			set
			{
				base.NavigateUrl = value;
			}
		}

		// Token: 0x17003C1F RID: 15391
		// (get) Token: 0x0600BA68 RID: 47720 RVA: 0x00297E0A File Offset: 0x0029600A
		// (set) Token: 0x0600BA69 RID: 47721 RVA: 0x00297E2B File Offset: 0x0029602B
		[Description("Whether the item should postback")]
		[DefaultValue(true)]
		public bool PostBack
		{
			get
			{
				return (bool)(this.ViewState["PostBack"] ?? true);
			}
			set
			{
				this.ViewState["PostBack"] = value;
			}
		}

		// Token: 0x17003C20 RID: 15392
		// (get) Token: 0x0600BA6A RID: 47722 RVA: 0x00297E43 File Offset: 0x00296043
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public RadMenu Menu
		{
			get
			{
				return (RadMenu)base.Container;
			}
		}

		// Token: 0x17003C21 RID: 15393
		// (get) Token: 0x0600BA6B RID: 47723 RVA: 0x00297E50 File Offset: 0x00296050
		// (set) Token: 0x0600BA6C RID: 47724 RVA: 0x00297E71 File Offset: 0x00296071
		[DefaultValue(false)]
		[Description("Sets/gets that the item is separator. It also represents a logical state of the item. Might be used in some applications like keyboard navigation to omit processing of items that are marked like separators.")]
		[Category("Behavior")]
		public bool IsSeparator
		{
			get
			{
				return (bool)(this.ViewState["IsSeparator"] ?? false);
			}
			set
			{
				this.ViewState["IsSeparator"] = value;
			}
		}

		// Token: 0x17003C22 RID: 15394
		// (get) Token: 0x0600BA6D RID: 47725 RVA: 0x00297E89 File Offset: 0x00296089
		// (set) Token: 0x0600BA6E RID: 47726 RVA: 0x00297EB9 File Offset: 0x002960B9
		[Category("Behavior")]
		[Description("Whether the item is selected or not")]
		[DefaultValue(false)]
		public bool Selected
		{
			get
			{
				if (base.Container == null)
				{
					return this._selected;
				}
				return (bool)(this.ViewState["Selected"] ?? false);
			}
			set
			{
				if (base.Container == null)
				{
					this._selected = value;
					return;
				}
				if (value && this.Menu != null)
				{
					this.Menu.ClearSelectedItem();
				}
				this.ViewState["Selected"] = value;
			}
		}

		// Token: 0x17003C23 RID: 15395
		// (get) Token: 0x0600BA6F RID: 47727 RVA: 0x00297EF7 File Offset: 0x002960F7
		// (set) Token: 0x0600BA70 RID: 47728 RVA: 0x00297F17 File Offset: 0x00296117
		[DefaultValue("rmDisabled")]
		[Description("CSS Class name applied to the menu item when it is disabled.")]
		[Category("Appearance")]
		public new string DisabledCssClass
		{
			get
			{
				return (string)(this.ViewState["DisabledCssClass"] ?? "rmDisabled");
			}
			set
			{
				this.ViewState["DisabledCssClass"] = value;
			}
		}

		// Token: 0x17003C24 RID: 15396
		// (get) Token: 0x0600BA71 RID: 47729 RVA: 0x00297F2A File Offset: 0x0029612A
		// (set) Token: 0x0600BA72 RID: 47730 RVA: 0x00297F4A File Offset: 0x0029614A
		[Description("CSS class applied to the menu item when it is expanded.")]
		[Category("Appearance")]
		[DefaultValue("rmExpanded")]
		public string ExpandedCssClass
		{
			get
			{
				return (string)(this.ViewState["ExpandedCssClass"] ?? "rmExpanded");
			}
			set
			{
				this.ViewState["ExpandedCssClass"] = value;
			}
		}

		// Token: 0x17003C25 RID: 15397
		// (get) Token: 0x0600BA73 RID: 47731 RVA: 0x00297F5D File Offset: 0x0029615D
		// (set) Token: 0x0600BA74 RID: 47732 RVA: 0x00297F7D File Offset: 0x0029617D
		[Description("CSS class applied to the menu item when it is focused.")]
		[Category("Appearance")]
		[DefaultValue("rmFocused")]
		public string FocusedCssClass
		{
			get
			{
				return (string)(this.ViewState["FocusedCssClass"] ?? "rmFocused");
			}
			set
			{
				this.ViewState["FocusedCssClass"] = value;
			}
		}

		// Token: 0x17003C26 RID: 15398
		// (get) Token: 0x0600BA75 RID: 47733 RVA: 0x00297F90 File Offset: 0x00296190
		// (set) Token: 0x0600BA76 RID: 47734 RVA: 0x00297FB0 File Offset: 0x002961B0
		[Description("Applied when the item is selected")]
		[DefaultValue("rmSelected")]
		[Category("Appearance")]
		public string SelectedCssClass
		{
			get
			{
				return (string)(this.ViewState["SelectedCssClass"] ?? "rmSelected");
			}
			set
			{
				this.ViewState["SelectedCssClass"] = value;
			}
		}

		// Token: 0x17003C27 RID: 15399
		// (get) Token: 0x0600BA77 RID: 47735 RVA: 0x00297FC3 File Offset: 0x002961C3
		// (set) Token: 0x0600BA78 RID: 47736 RVA: 0x00297FE3 File Offset: 0x002961E3
		[Category("Appearance")]
		[DefaultValue("rmClicked")]
		[Description("CSS class applied to the menu item when it is clicked.")]
		public string ClickedCssClass
		{
			get
			{
				return (string)(this.ViewState["ClickedCssClass"] ?? "rmClicked");
			}
			set
			{
				this.ViewState["ClickedCssClass"] = value;
			}
		}

		// Token: 0x17003C28 RID: 15400
		// (get) Token: 0x0600BA79 RID: 47737 RVA: 0x00297FF6 File Offset: 0x002961F6
		// (set) Token: 0x0600BA7A RID: 47738 RVA: 0x00298016 File Offset: 0x00296216
		[Description("CSS Class name applied on the outmost item wrapper (<LI>).")]
		[Category("Appearance")]
		[DefaultValue("")]
		public string OuterCssClass
		{
			get
			{
				return (string)(this.ViewState["OuterCssClass"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OuterCssClass"] = value;
			}
		}

		// Token: 0x17003C29 RID: 15401
		// (get) Token: 0x0600BA7B RID: 47739 RVA: 0x00298029 File Offset: 0x00296229
		// (set) Token: 0x0600BA7C RID: 47740 RVA: 0x00298049 File Offset: 0x00296249
		[Category("Appearance")]
		[DefaultValue("")]
		[Description("The CSS that is used in sprite image scenarios.")]
		public string SpriteCssClass
		{
			get
			{
				return (string)(this.ViewState["SpriteCssClass"] ?? string.Empty);
			}
			set
			{
				this.ViewState["SpriteCssClass"] = value;
			}
		}

		// Token: 0x17003C2A RID: 15402
		// (get) Token: 0x0600BA7D RID: 47741 RVA: 0x0029805C File Offset: 0x0029625C
		// (set) Token: 0x0600BA7E RID: 47742 RVA: 0x00298064 File Offset: 0x00296264
		[Description("The navigation target used when the menu item is selected.")]
		[DefaultValue("")]
		[Category("Navigation")]
		[TypeConverter(typeof(TargetConverter))]
		public override string Target
		{
			get
			{
				return base.Target;
			}
			set
			{
				base.Target = value;
			}
		}

		// Token: 0x17003C2B RID: 15403
		// (get) Token: 0x0600BA7F RID: 47743 RVA: 0x0029806D File Offset: 0x0029626D
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public int Level
		{
			get
			{
				if (this.Owner is RadContextMenu)
				{
					return 1;
				}
				if (this.Owner is RadMenu || this.Owner == null)
				{
					return 0;
				}
				return ((RadMenuItem)this.Owner).Level + 1;
			}
		}

		// Token: 0x17003C2C RID: 15404
		// (get) Token: 0x0600BA80 RID: 47744 RVA: 0x002980A7 File Offset: 0x002962A7
		// (set) Token: 0x0600BA81 RID: 47745 RVA: 0x002980AF File Offset: 0x002962AF
		[Editor("Telerik.Web.Design.ControlItemImageUrlEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Category("Appearance")]
		[UrlProperty]
		[Description("The URL for the image for the Item.")]
		[DefaultValue("")]
		public override string ImageUrl
		{
			get
			{
				return base.ImageUrl;
			}
			set
			{
				base.ImageUrl = value;
			}
		}

		// Token: 0x17003C2D RID: 15405
		// (get) Token: 0x0600BA82 RID: 47746 RVA: 0x002980B8 File Offset: 0x002962B8
		// (set) Token: 0x0600BA83 RID: 47747 RVA: 0x002980C0 File Offset: 0x002962C0
		[Category("Appearance")]
		[Description("The URL for the image when the mouse moves over the item.")]
		[DefaultValue("")]
		[UrlProperty]
		[Editor("Telerik.Web.Design.ControlItemImageUrlEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		public override string HoveredImageUrl
		{
			get
			{
				return base.HoveredImageUrl;
			}
			set
			{
				base.HoveredImageUrl = value;
			}
		}

		// Token: 0x17003C2E RID: 15406
		// (get) Token: 0x0600BA84 RID: 47748 RVA: 0x002980C9 File Offset: 0x002962C9
		// (set) Token: 0x0600BA85 RID: 47749 RVA: 0x002980E9 File Offset: 0x002962E9
		[UrlProperty]
		[Category("Appearance")]
		[Description("The URL for the image when the item is clicked.")]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.ControlItemImageUrlEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		public string ClickedImageUrl
		{
			get
			{
				return (string)(this.ViewState["ClickedImageUrl"] ?? string.Empty);
			}
			set
			{
				this.ViewState["ClickedImageUrl"] = value;
			}
		}

		// Token: 0x17003C2F RID: 15407
		// (get) Token: 0x0600BA86 RID: 47750 RVA: 0x002980FC File Offset: 0x002962FC
		// (set) Token: 0x0600BA87 RID: 47751 RVA: 0x0029811C File Offset: 0x0029631C
		[Category("Appearance")]
		[UrlProperty]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.ControlItemImageUrlEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		public string DisabledImageUrl
		{
			get
			{
				return (string)(this.ViewState["DisabledImageUrl"] ?? string.Empty);
			}
			set
			{
				this.ViewState["DisabledImageUrl"] = value;
			}
		}

		// Token: 0x17003C30 RID: 15408
		// (get) Token: 0x0600BA88 RID: 47752 RVA: 0x0029812F File Offset: 0x0029632F
		// (set) Token: 0x0600BA89 RID: 47753 RVA: 0x0029814F File Offset: 0x0029634F
		[Editor("Telerik.Web.Design.ControlItemImageUrlEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[UrlProperty]
		[Category("Appearance")]
		[DefaultValue("")]
		public string ExpandedImageUrl
		{
			get
			{
				return (string)(this.ViewState["ExpandedImageUrl"] ?? string.Empty);
			}
			set
			{
				this.ViewState["ExpandedImageUrl"] = value;
			}
		}

		// Token: 0x17003C31 RID: 15409
		// (get) Token: 0x0600BA8A RID: 47754 RVA: 0x00298162 File Offset: 0x00296362
		// (set) Token: 0x0600BA8B RID: 47755 RVA: 0x00298182 File Offset: 0x00296382
		[Category("Appearance")]
		[DefaultValue("")]
		[Description("The image used when the item is selected.")]
		[Editor("Telerik.Web.Design.ControlItemImageUrlEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[UrlProperty]
		public string SelectedImageUrl
		{
			get
			{
				return (string)(this.ViewState["SelectedImageUrl"] ?? string.Empty);
			}
			set
			{
				this.ViewState["SelectedImageUrl"] = value;
			}
		}

		// Token: 0x0600BA8C RID: 47756 RVA: 0x00298198 File Offset: 0x00296398
		public void ApplyContentTemplate()
		{
			if (this._contentTemplate != null)
			{
				if (this._content == null)
				{
					this._content = new MenuItemContentTemplateContainer(this);
					this.Controls.Add(this._content);
				}
				this._content.Controls.Clear();
				this._contentTemplate.InstantiateIn(this._content);
			}
		}

		// Token: 0x0600BA8D RID: 47757 RVA: 0x002981F4 File Offset: 0x002963F4
		public void HighlightPath()
		{
			for (RadMenuItem radMenuItem = this; radMenuItem != null; radMenuItem = (radMenuItem.Owner as RadMenuItem))
			{
				if (!radMenuItem.CssClass.Contains("rmSelected"))
				{
					radMenuItem.CssClass = (radMenuItem.CssClass + " rmSelected").Trim();
				}
			}
		}

		// Token: 0x0600BA8E RID: 47758 RVA: 0x00298241 File Offset: 0x00296441
		public void Remove()
		{
			if (this.Owner == null)
			{
				return;
			}
			this.Owner.Items.Remove(this);
		}

		// Token: 0x0600BA8F RID: 47759 RVA: 0x00298260 File Offset: 0x00296460
		public RadMenuItem Clone()
		{
			RadMenuItem radMenuItem = new RadMenuItem
			{
				Enabled = this.Enabled,
				ImageUrl = this.ImageUrl,
				HoveredImageUrl = this.HoveredImageUrl,
				NavigateUrl = this.NavigateUrl,
				Target = this.Target,
				Text = this.Text,
				ToolTip = this.ToolTip,
				Value = this.Value,
				Visible = this.Visible
			};
			foreach (object obj in base.Attributes.Keys)
			{
				string key = (string)obj;
				radMenuItem.Attributes[key] = base.Attributes[key];
			}
			return radMenuItem;
		}

		// Token: 0x0600BA90 RID: 47760 RVA: 0x00298348 File Offset: 0x00296548
		object ICloneable.Clone()
		{
			return this.Clone();
		}

		// Token: 0x17003C32 RID: 15410
		// (get) Token: 0x0600BA91 RID: 47761 RVA: 0x00298350 File Offset: 0x00296550
		protected internal override IDictionary<string, string> PropertyMappings
		{
			get
			{
				return new Dictionary<string, string>
				{
					{
						"CssClassClicked",
						"ClickedCssClass"
					},
					{
						"CssClassDisabled",
						"DisabledCssClass"
					},
					{
						"Href",
						"NavigateUrl"
					},
					{
						"Image",
						"ImageUrl"
					},
					{
						"ImageOver",
						"HoveredImageUrl"
					},
					{
						"Key",
						"AccessKey"
					},
					{
						"LeftLogo",
						"ImageUrl"
					},
					{
						"LeftLogoOver",
						"HoveredImageUrl"
					}
				};
			}
		}

		// Token: 0x17003C33 RID: 15411
		// (get) Token: 0x0600BA92 RID: 47762 RVA: 0x002983E8 File Offset: 0x002965E8
		internal override bool Templated
		{
			get
			{
				if (base.TemplateInstantiated)
				{
					return true;
				}
				if (!this._controlsTraversed)
				{
					this._controlsTraversed = true;
					foreach (object obj in this.Controls)
					{
						Control control = (Control)obj;
						if (!this.IsChildControl(control) && !control.Equals(this.ContentTemplateContainer))
						{
							this._templated = true;
							break;
						}
					}
				}
				return this._templated;
			}
		}

		// Token: 0x17003C34 RID: 15412
		// (get) Token: 0x0600BA93 RID: 47763 RVA: 0x0029847C File Offset: 0x0029667C
		protected virtual IRenderer Renderer
		{
			get
			{
				if (this._renderer == null)
				{
					this._renderer = this.CreateItemRenderer();
				}
				return this._renderer;
			}
		}

		// Token: 0x17003C35 RID: 15413
		// (get) Token: 0x0600BA94 RID: 47764 RVA: 0x00298498 File Offset: 0x00296698
		protected internal virtual bool HasContentTemplate
		{
			get
			{
				return this._contentTemplate != null || (this._content != null && this._content.Controls.Count > 0);
			}
		}

		// Token: 0x0600BA95 RID: 47765 RVA: 0x002984C1 File Offset: 0x002966C1
		public RadMenuItem()
		{
			this._groupSettings = new RadMenuItemGroupSettings(this.ViewState, this);
		}

		// Token: 0x0600BA96 RID: 47766 RVA: 0x002984DB File Offset: 0x002966DB
		public RadMenuItem(string text) : this()
		{
			this.Text = text;
		}

		// Token: 0x0600BA97 RID: 47767 RVA: 0x002984EA File Offset: 0x002966EA
		public RadMenuItem(string text, string navigateUrl) : this(text)
		{
			this.NavigateUrl = navigateUrl;
		}

		// Token: 0x0600BA98 RID: 47768 RVA: 0x002984FA File Offset: 0x002966FA
		protected internal virtual IRenderer CreateItemRenderer()
		{
			return RendererFactory.CreateItemRenderer(this);
		}

		// Token: 0x0600BA99 RID: 47769 RVA: 0x00298502 File Offset: 0x00296702
		protected override ControlItemCollection CreateChildItemCollection()
		{
			return new RadMenuItemCollection(this);
		}

		// Token: 0x0600BA9A RID: 47770 RVA: 0x0029850A File Offset: 0x0029670A
		protected internal override void SetItemContainer(ControlItemContainer itemContainer)
		{
			base.SetItemContainer(itemContainer);
			if (this._selected)
			{
				this.Selected = true;
				this._selected = false;
			}
		}

		// Token: 0x0600BA9B RID: 47771 RVA: 0x00298529 File Offset: 0x00296729
		protected override void ReadXmlForChildren(XmlReader reader)
		{
			do
			{
				reader.Read();
			}
			while (reader.NodeType == XmlNodeType.Comment);
			XmlPersister.Deserialize(this.GroupSettings, null, null, reader, false);
			base.ReadXmlForChildren(reader);
		}

		// Token: 0x0600BA9C RID: 47772 RVA: 0x00298554 File Offset: 0x00296754
		protected override void WriteXmlForChildren(XmlWriter writer)
		{
			if (this.GroupSettings.ShouldSerialize() || this.Items.Count > 0)
			{
				writer.WriteStartElement("Group");
				this.GroupSettings.SerializeTo(writer);
			}
			base.WriteXmlForChildren(writer);
			if (this.GroupSettings.ShouldSerialize() || this.Items.Count > 0)
			{
				writer.WriteEndElement();
			}
		}

		// Token: 0x0600BA9D RID: 47773 RVA: 0x002985BC File Offset: 0x002967BC
		protected internal override void LoadFromDictionary(IDictionary<string, object> dictionary)
		{
			base.LoadFromDictionary(dictionary);
			if (dictionary.ContainsKey("expandMode"))
			{
				this.ExpandMode = (MenuItemExpandMode)dictionary["expandMode"];
			}
			if (dictionary.ContainsKey("selected"))
			{
				this.Selected = (bool)dictionary["selected"];
			}
			if (dictionary.ContainsKey("navigateUrl"))
			{
				this.NavigateUrl = (string)dictionary["navigateUrl"];
			}
			if (dictionary.ContainsKey("postBack"))
			{
				this.PostBack = Convert.ToBoolean(dictionary["postBack"]);
			}
			if (dictionary.ContainsKey("target"))
			{
				this.Target = (string)dictionary["target"];
			}
			if (dictionary.ContainsKey("isSeparator"))
			{
				this.IsSeparator = Convert.ToBoolean(dictionary["isSeparator"]);
			}
			if (dictionary.ContainsKey("cssClass"))
			{
				this.CssClass = dictionary["cssClass"].ToString();
			}
			if (dictionary.ContainsKey("disabledCssClass"))
			{
				this.DisabledCssClass = (string)dictionary["disabledCssClass"];
			}
			if (dictionary.ContainsKey("expandedCssClass"))
			{
				this.ExpandedCssClass = (string)dictionary["expandedCssClass"];
			}
			if (dictionary.ContainsKey("focusedCssClass"))
			{
				this.FocusedCssClass = (string)dictionary["focusedCssClass"];
			}
			if (dictionary.ContainsKey("clickedCssClass"))
			{
				this.ClickedCssClass = (string)dictionary["clickedCssClass"];
			}
			if (dictionary.ContainsKey("imageUrl"))
			{
				this.ImageUrl = (string)dictionary["imageUrl"];
			}
			if (dictionary.ContainsKey("hoveredImageUrl"))
			{
				this.HoveredImageUrl = (string)dictionary["hoveredImageUrl"];
			}
			if (dictionary.ContainsKey("clickedImageUrl"))
			{
				this.ClickedImageUrl = (string)dictionary["clickedImageUrl"];
			}
			if (dictionary.ContainsKey("disabledImageUrl"))
			{
				this.DisabledImageUrl = (string)dictionary["disabledImageUrl"];
			}
			if (dictionary.ContainsKey("expandedImageUrl"))
			{
				this.ExpandedImageUrl = (string)dictionary["expandedImageUrl"];
			}
		}

		// Token: 0x0600BA9E RID: 47774 RVA: 0x00298800 File Offset: 0x00296A00
		internal override void PopulateFromDataItem(PropertyDescriptorCache properties, object dataItem, string dataMember, int depth)
		{
			base.PopulateFromDataItem(properties, dataItem, dataMember, depth);
			if (dataItem is INavigateUIData)
			{
				IHierarchyData hierarchyData = dataItem as IHierarchyData;
				if (string.Equals(hierarchyData.Path, this.Menu.CurrentSiteMapUrl, StringComparison.OrdinalIgnoreCase))
				{
					this.HighlightPath();
				}
			}
		}

		// Token: 0x0600BA9F RID: 47775 RVA: 0x00298846 File Offset: 0x00296A46
		protected override void CreateChildControls()
		{
			if (this._content != null)
			{
				this.Controls.Add(this._content);
			}
			base.CreateChildControls();
		}

		// Token: 0x0600BAA0 RID: 47776 RVA: 0x00298867 File Offset: 0x00296A67
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			if (this.HasContentTemplate && this.Items.Count > 0)
			{
				throw new MenuItemTemplateException("Cannot set ContentTemplate on a RadMenuItem, which has child Items.");
			}
		}

		// Token: 0x17003C36 RID: 15414
		// (get) Token: 0x0600BAA1 RID: 47777 RVA: 0x00298891 File Offset: 0x00296A91
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return this.Renderer.TagKey;
			}
		}

		// Token: 0x17003C37 RID: 15415
		// (get) Token: 0x0600BAA2 RID: 47778 RVA: 0x0029889E File Offset: 0x00296A9E
		internal string CurrentImageUrl
		{
			get
			{
				if (!this.Enabled && !string.IsNullOrEmpty(this.DisabledImageUrl))
				{
					return this.DisabledImageUrl;
				}
				if (this.Selected && !string.IsNullOrEmpty(this.SelectedImageUrl))
				{
					return this.SelectedImageUrl;
				}
				return this.ImageUrl;
			}
		}

		// Token: 0x17003C38 RID: 15416
		// (get) Token: 0x0600BAA3 RID: 47779 RVA: 0x002988DE File Offset: 0x00296ADE
		internal bool ShouldRenderLink
		{
			get
			{
				return !this.IsSeparator && !this.Templated;
			}
		}

		// Token: 0x17003C39 RID: 15417
		// (get) Token: 0x0600BAA4 RID: 47780 RVA: 0x002988F4 File Offset: 0x00296AF4
		internal bool IsFirstVisibleItem
		{
			get
			{
				if (base.Index == 0 && this.Visible)
				{
					return true;
				}
				for (int i = base.Index - 1; i > -1; i--)
				{
					if (this.Owner.Items[i].Visible)
					{
						return false;
					}
				}
				return true;
			}
		}

		// Token: 0x17003C3A RID: 15418
		// (get) Token: 0x0600BAA5 RID: 47781 RVA: 0x00298941 File Offset: 0x00296B41
		internal bool HasMultipleColumns
		{
			get
			{
				return this.GroupSettings.RepeatColumnsResolved > 1;
			}
		}

		// Token: 0x17003C3B RID: 15419
		// (get) Token: 0x0600BAA6 RID: 47782 RVA: 0x00298954 File Offset: 0x00296B54
		internal bool ShouldRenderScrollWrap
		{
			get
			{
				return !this.HasMultipleColumns && (!this.GroupSettings.WidthResolved.IsEmpty || !this.GroupSettings.HeightResolved.IsEmpty);
			}
		}

		// Token: 0x17003C3C RID: 15420
		// (get) Token: 0x0600BAA7 RID: 47783 RVA: 0x00298998 File Offset: 0x00296B98
		internal bool ShouldRenderToggleButton
		{
			get
			{
				return !this.Templated && this.Menu.ShowToggleHandle && (this.Items.Count > 0 || this.ExpandMode == MenuItemExpandMode.WebService);
			}
		}

		// Token: 0x17003C3D RID: 15421
		// (get) Token: 0x0600BAA8 RID: 47784 RVA: 0x002989CA File Offset: 0x00296BCA
		internal string GroupLevelCssClass
		{
			get
			{
				return "rmLevel" + (this.Level + 1);
			}
		}

		// Token: 0x17003C3E RID: 15422
		// (get) Token: 0x0600BAA9 RID: 47785 RVA: 0x002989E3 File Offset: 0x00296BE3
		internal bool ShouldRenderImagePlaceholder
		{
			get
			{
				return this.EnableImageSpriteResolved && this.ShouldRenderLink;
			}
		}

		// Token: 0x17003C3F RID: 15423
		// (get) Token: 0x0600BAAA RID: 47786 RVA: 0x002989F5 File Offset: 0x00296BF5
		// (set) Token: 0x0600BAAB RID: 47787 RVA: 0x002989FD File Offset: 0x00296BFD
		internal string PositionCssClass { get; set; }

		// Token: 0x0600BAAC RID: 47788 RVA: 0x00298A06 File Offset: 0x00296C06
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			this.Renderer.AddAttributesToRender(writer);
		}

		// Token: 0x0600BAAD RID: 47789 RVA: 0x00298A14 File Offset: 0x00296C14
		protected override void RenderContents(HtmlTextWriter writer)
		{
			this.Renderer.RenderContents(writer);
		}

		// Token: 0x0600BAAE RID: 47790 RVA: 0x00298A24 File Offset: 0x00296C24
		internal int GetLastVisibleItemIndex()
		{
			for (int i = this.Owner.Items.Count - 1; i > -1; i--)
			{
				if (this.Owner.Items[i].Visible)
				{
					return i;
				}
			}
			return 0;
		}

		// Token: 0x0600BAAF RID: 47791 RVA: 0x00298A6C File Offset: 0x00296C6C
		internal string GetGroupCssClass()
		{
			List<string> list = new List<string>
			{
				RadMenu.GetFlowCssClass(this.GroupSettings.FlowResolved)
			};
			if (!this.ShouldRenderScrollWrap)
			{
				list.AddRange(new string[]
				{
					"rmGroup",
					this.GroupLevelCssClass
				});
			}
			return string.Join(" ", list.ToArray());
		}

		// Token: 0x0600BAB0 RID: 47792 RVA: 0x00298ACE File Offset: 0x00296CCE
		internal void CallBaseRenderChildren(HtmlTextWriter writer)
		{
			base.RenderChildren(writer);
		}

		// Token: 0x0600BAB1 RID: 47793 RVA: 0x00298AD8 File Offset: 0x00296CD8
		internal static void UpdatePositionCssClass(IList<RadMenuItem> items)
		{
			List<RadMenuItem> list = new List<RadMenuItem>();
			foreach (RadMenuItem radMenuItem in items)
			{
				if (radMenuItem.Visible)
				{
					list.Add(radMenuItem);
				}
			}
			if (list.Count == 0)
			{
				return;
			}
			RadMenuItem radMenuItem2 = list[0];
			RadMenuItem radMenuItem3 = list[list.Count - 1];
			radMenuItem2.PositionCssClass = "rmFirst";
			radMenuItem3.PositionCssClass = (radMenuItem3.PositionCssClass + " rmLast").Trim();
		}

		// Token: 0x04003144 RID: 12612
		private const string TemplateExceptionMessage = "Cannot set ContentTemplate on a RadMenuItem, which has child Items.";

		// Token: 0x04003145 RID: 12613
		private readonly RadMenuItemGroupSettings _groupSettings;

		// Token: 0x04003146 RID: 12614
		private bool _selected;

		// Token: 0x04003147 RID: 12615
		private bool _controlsTraversed;

		// Token: 0x04003148 RID: 12616
		private bool _templated;

		// Token: 0x04003149 RID: 12617
		private IRenderer _renderer;

		// Token: 0x0400314A RID: 12618
		private MenuItemContentTemplateContainer _content;

		// Token: 0x0400314B RID: 12619
		private ITemplate _contentTemplate;
	}
}
