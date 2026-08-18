using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Globalization;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02000985 RID: 2437
	[RequiredCss("Telerik.Web.UI.Skins.Common.fonticons.css", RenderMode.Lightweight, typeof(RadWindowBase))]
	[RequiredCss("Telerik.Web.UI.Skins.Common.MaterialRipple.css", RenderMode.Lightweight, typeof(RadButton))]
	[EmbeddedSkin("Window")]
	[RequiredScript(typeof(PopupBehavior))]
	[RequiredScript(typeof(ResizeExtender))]
	[RequiredScript(typeof(ModalExtender))]
	[RequiredScript(typeof(TouchScrollExtender))]
	[RequiredScript(typeof(MaterialRipple))]
	[RequiredScript(typeof(Resizable))]
	[RequiredScript(typeof(ShortCutManager))]
	[EmbeddedSkin("Window", "Default")]
	[RequiredScript(typeof(Draggable))]
	[ClientScriptResource("Telerik.Web.UI.RadWindow", "Telerik.Web.UI.Window.RadWindowScripts.js")]
	[RequiredScript(typeof(AnimationScripts))]
	public abstract class RadWindowBase : RadWebControl
	{
		// Token: 0x06005CA4 RID: 23716 RVA: 0x0011B26C File Offset: 0x0011946C
		protected override IEnumerable<ScriptReference> GetScriptReferences()
		{
			IEnumerable<ScriptReference> scriptReferences = base.GetScriptReferences();
			List<ScriptReference> list = new List<ScriptReference>(scriptReferences);
			if (this.Animation != WindowAnimation.None && this.EnableEmbeddedScripts)
			{
				list.Add(new ScriptReference("Telerik.Web.UI.Common.Animation.AnimationScripts.js", typeof(RadWindowBase).Assembly.FullName));
			}
			return list;
		}

		// Token: 0x06005CA5 RID: 23717 RVA: 0x0011B2BC File Offset: 0x001194BC
		protected internal object GetRawViewStateValue(string key)
		{
			return this.ViewState[key];
		}

		// Token: 0x06005CA6 RID: 23718 RVA: 0x0011B2CA File Offset: 0x001194CA
		protected internal void SetRawViewStateValue(string key, object val)
		{
			this.ViewState[key] = val;
		}

		// Token: 0x17001E7D RID: 7805
		// (get) Token: 0x06005CA7 RID: 23719 RVA: 0x0011B2D9 File Offset: 0x001194D9
		// (set) Token: 0x06005CA8 RID: 23720 RVA: 0x0011B2E1 File Offset: 0x001194E1
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Category("Behavior")]
		[Description("Specifies the client callback function that will be called when a window dialog is being closed. This property is obsolete. Please use OnclientClose instead.")]
		[Bindable(true)]
		[Obsolete("The ClientCallBackFunction property is obsolete. Please use OnClientClose instead.")]
		[Browsable(false)]
		[DefaultValue("")]
		public string ClientCallBackFunction
		{
			get
			{
				return this.OnClientClose;
			}
			set
			{
				throw new ArgumentException("The ClientCallBackFunction property is obsolete and should not be used. Please use OnClientClose instead. For more information visit http://www.telerik.com/help/aspnet-ajax/window-programming-using-radwindow-as-dialog.html");
			}
		}

		// Token: 0x17001E7E RID: 7806
		// (get) Token: 0x06005CA9 RID: 23721 RVA: 0x0011B2ED File Offset: 0x001194ED
		// (set) Token: 0x06005CAA RID: 23722 RVA: 0x0011B30D File Offset: 0x0011950D
		[ClientControlProperty]
		[Description("Specifies the id (ClientID if a runat=server is used) of a html element, whose left and top position will be used as 0,0 of the RadWindow object when it is first shown.")]
		[Browsable(true)]
		[Bindable(true)]
		[Category("Behavior")]
		[DefaultValue("")]
		public string OffsetElementID
		{
			get
			{
				return ((string)this.ViewState["OffsetElementID"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["OffsetElementID"] = value;
			}
		}

		// Token: 0x17001E7F RID: 7807
		// (get) Token: 0x06005CAB RID: 23723 RVA: 0x0011B320 File Offset: 0x00119520
		// (set) Token: 0x06005CAC RID: 23724 RVA: 0x0011B340 File Offset: 0x00119540
		[Bindable(true)]
		[Description("Specifies the id (ClientID if a runat=server is used) of a html element where the windows will be docked when minimized")]
		[Category("Behavior")]
		[DefaultValue("")]
		[ClientControlProperty]
		[Browsable(true)]
		public string MinimizeZoneID
		{
			get
			{
				return ((string)this.ViewState["MinimizeZoneID"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["MinimizeZoneID"] = value;
			}
		}

		// Token: 0x17001E80 RID: 7808
		// (get) Token: 0x06005CAD RID: 23725 RVA: 0x0011B353 File Offset: 0x00119553
		// (set) Token: 0x06005CAE RID: 23726 RVA: 0x0011B373 File Offset: 0x00119573
		[Category("Appearance")]
		[Description("Gets or sets the url of the icon in the upper left corner of the RadWindow title bar")]
		[UrlProperty]
		[DefaultValue("")]
		public string IconUrl
		{
			get
			{
				return ((string)this.ViewState["IconUrl"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["IconUrl"] = value;
			}
		}

		// Token: 0x17001E81 RID: 7809
		// (get) Token: 0x06005CAF RID: 23727 RVA: 0x0011B386 File Offset: 0x00119586
		// (set) Token: 0x06005CB0 RID: 23728 RVA: 0x0011B3A6 File Offset: 0x001195A6
		[UrlProperty]
		[Description("Gets or sets the url of the minimized icon of the RadWindow")]
		[Category("Appearance")]
		[DefaultValue("")]
		public string MinimizeIconUrl
		{
			get
			{
				return ((string)this.ViewState["MinimizeIconUrl"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["MinimizeIconUrl"] = value;
			}
		}

		// Token: 0x17001E82 RID: 7810
		// (get) Token: 0x06005CB1 RID: 23729 RVA: 0x0011B3B9 File Offset: 0x001195B9
		// (set) Token: 0x06005CB2 RID: 23730 RVA: 0x0011B3C7 File Offset: 0x001195C7
		[ClientControlProperty]
		[Category("Appearance")]
		[DefaultValue(false)]
		[ClientPropertyName("enableShadow")]
		[Description("Indicates whether the RadWindow should have a shadow.")]
		public bool EnableShadow
		{
			get
			{
				return base.GetViewStateValue<bool>("EnableShadow", false);
			}
			set
			{
				this.ViewState["EnableShadow"] = value;
			}
		}

		// Token: 0x17001E83 RID: 7811
		// (get) Token: 0x06005CB3 RID: 23731 RVA: 0x0011B3DF File Offset: 0x001195DF
		// (set) Token: 0x06005CB4 RID: 23732 RVA: 0x0011B40D File Offset: 0x0011960D
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Category("Appearance")]
		public RadWindowLocalization Localization
		{
			get
			{
				if (this._localization == null)
				{
					this._localization = new RadWindowLocalization();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._localization).TrackViewState();
					}
				}
				return this._localization;
			}
			set
			{
				this._localization = value;
			}
		}

		// Token: 0x17001E84 RID: 7812
		// (get) Token: 0x06005CB5 RID: 23733 RVA: 0x0011B416 File Offset: 0x00119616
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public WindowShortcutCollection Shortcuts
		{
			get
			{
				if (this._shortcuts == null)
				{
					this._shortcuts = new WindowShortcutCollection();
				}
				return this._shortcuts;
			}
		}

		// Token: 0x17001E85 RID: 7813
		// (get) Token: 0x06005CB6 RID: 23734 RVA: 0x0011B431 File Offset: 0x00119631
		// (set) Token: 0x06005CB7 RID: 23735 RVA: 0x0011B440 File Offset: 0x00119640
		[Description("Gets or sets a value indicating the behavior of this object - if can be resized, has expand/collapse commands, closed command, etc.")]
		[Category("Behavior")]
		[DefaultValue(WindowBehaviors.Default)]
		[Editor("Telerik.Web.Design.Common.FlagEnumUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlProperty]
		public WindowBehaviors Behaviors
		{
			get
			{
				return base.GetViewStateValue<WindowBehaviors>("Behaviors", WindowBehaviors.Default);
			}
			set
			{
				this.ViewState["Behaviors"] = value;
			}
		}

		// Token: 0x17001E86 RID: 7814
		// (get) Token: 0x06005CB8 RID: 23736 RVA: 0x0011B458 File Offset: 0x00119658
		// (set) Token: 0x06005CB9 RID: 23737 RVA: 0x0011B460 File Offset: 0x00119660
		[DefaultValue(WindowBehaviors.Default)]
		[Obsolete("This property is obsolete. Please use Behaviors instead.")]
		[Browsable(false)]
		public WindowBehaviors Behavior
		{
			get
			{
				return this.Behaviors;
			}
			set
			{
				this.Behaviors = value;
			}
		}

		// Token: 0x17001E87 RID: 7815
		// (get) Token: 0x06005CBA RID: 23738 RVA: 0x0011B469 File Offset: 0x00119669
		// (set) Token: 0x06005CBB RID: 23739 RVA: 0x0011B495 File Offset: 0x00119695
		[ClientControlProperty]
		[Category("Behavior")]
		[Editor("Telerik.Web.Design.Common.FlagEnumUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[DefaultValue(WindowAutoSizeBehaviors.Default)]
		public WindowAutoSizeBehaviors AutoSizeBehaviors
		{
			get
			{
				if (this.ViewState["AutoSizeBehaviors"] == null)
				{
					return WindowAutoSizeBehaviors.Default;
				}
				return (WindowAutoSizeBehaviors)this.ViewState["AutoSizeBehaviors"];
			}
			set
			{
				this.ViewState["AutoSizeBehaviors"] = value;
			}
		}

		// Token: 0x17001E88 RID: 7816
		// (get) Token: 0x06005CBC RID: 23740 RVA: 0x0011B4AD File Offset: 0x001196AD
		// (set) Token: 0x06005CBD RID: 23741 RVA: 0x0011B4B5 File Offset: 0x001196B5
		[DefaultValue(WindowBehaviors.None)]
		[Browsable(false)]
		[Obsolete("This property is obsolete. Please use InitialBehaviors instead.")]
		public WindowBehaviors InitialBehavior
		{
			get
			{
				return this.InitialBehaviors;
			}
			set
			{
				this.InitialBehaviors = value;
			}
		}

		// Token: 0x17001E89 RID: 7817
		// (get) Token: 0x06005CBE RID: 23742 RVA: 0x0011B4BE File Offset: 0x001196BE
		// (set) Token: 0x06005CBF RID: 23743 RVA: 0x0011B4CC File Offset: 0x001196CC
		[DefaultValue(WindowBehaviors.None)]
		[Editor("Telerik.Web.Design.Common.FlagEnumUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Description("Indicates the initial behavior of the RadWindow - most useful to specify an initially minimized, maximized or pinned window.")]
		[Category("Behavior")]
		[ClientControlProperty]
		public WindowBehaviors InitialBehaviors
		{
			get
			{
				return base.GetViewStateValue<WindowBehaviors>("InitialBehaviors", WindowBehaviors.None);
			}
			set
			{
				this.ViewState["InitialBehaviors"] = value;
			}
		}

		// Token: 0x17001E8A RID: 7818
		// (get) Token: 0x06005CC0 RID: 23744 RVA: 0x0011B4E4 File Offset: 0x001196E4
		// (set) Token: 0x06005CC1 RID: 23745 RVA: 0x0011B4F2 File Offset: 0x001196F2
		[ClientControlProperty]
		[DefaultValue(true)]
		[Description("Specifies whether the maximized window should have the biggest z-index")]
		[Category("Behavior")]
		public bool ShowOnTopWhenMaximized
		{
			get
			{
				return base.GetViewStateValue<bool>("ShowOnTopWhenMaximized", true);
			}
			set
			{
				this.ViewState["ShowOnTopWhenMaximized"] = value;
			}
		}

		// Token: 0x17001E8B RID: 7819
		// (get) Token: 0x06005CC2 RID: 23746 RVA: 0x0011B50A File Offset: 0x0011970A
		// (set) Token: 0x06005CC3 RID: 23747 RVA: 0x0011B518 File Offset: 0x00119718
		[DefaultValue(WindowAnimation.None)]
		[Category("Behavior")]
		[ClientControlProperty]
		public WindowAnimation Animation
		{
			get
			{
				return base.GetViewStateValue<WindowAnimation>("Animation", WindowAnimation.None);
			}
			set
			{
				this.ViewState["Animation"] = value;
			}
		}

		// Token: 0x17001E8C RID: 7820
		// (get) Token: 0x06005CC4 RID: 23748 RVA: 0x0011B530 File Offset: 0x00119730
		// (set) Token: 0x06005CC5 RID: 23749 RVA: 0x0011B542 File Offset: 0x00119742
		[Category("Behavior")]
		[DefaultValue(500)]
		[Description("Sets/gets the duration of the animation in milliseconds.")]
		[ClientControlProperty]
		public int AnimationDuration
		{
			get
			{
				return base.GetViewStateValue<int>("AnimationDuration", 500);
			}
			set
			{
				this.ViewState["AnimationDuration"] = value;
			}
		}

		// Token: 0x17001E8D RID: 7821
		// (get) Token: 0x06005CC6 RID: 23750 RVA: 0x0011B55A File Offset: 0x0011975A
		// (set) Token: 0x06005CC7 RID: 23751 RVA: 0x0011B56C File Offset: 0x0011976C
		[ClientControlProperty]
		[Category("Behavior")]
		[Description("The Width of the RadWindow in pixels.")]
		[DefaultValue(typeof(Unit), "")]
		[TypeConverter(typeof(UnitConverter))]
		public override Unit Width
		{
			get
			{
				return base.GetViewStateValue<Unit>("Width", Unit.Empty);
			}
			set
			{
				if (!this.MaxWidth.IsEmpty && this.MaxWidth.Value < value.Value)
				{
					value = this.MaxWidth;
				}
				if (!this.MinWidth.IsEmpty && this.MinWidth.Value > value.Value)
				{
					value = this.MinWidth;
				}
				this.ViewState["Width"] = value;
			}
		}

		// Token: 0x17001E8E RID: 7822
		// (get) Token: 0x06005CC8 RID: 23752 RVA: 0x0011B5ED File Offset: 0x001197ED
		// (set) Token: 0x06005CC9 RID: 23753 RVA: 0x0011B600 File Offset: 0x00119800
		[Category("Behavior")]
		[TypeConverter(typeof(UnitConverter))]
		[DefaultValue(typeof(Unit), "")]
		[Description("The minimum Width of the RadWindow in pixels.")]
		[ClientControlProperty]
		public Unit MinWidth
		{
			get
			{
				return base.GetViewStateValue<Unit>("MinWidth", Unit.Empty);
			}
			set
			{
				this.ViewState["MinWidth"] = value;
				if (!this.Width.IsEmpty && !value.IsEmpty && this.Width.Value < value.Value)
				{
					this.Width = this.MinWidth;
				}
			}
		}

		// Token: 0x17001E8F RID: 7823
		// (get) Token: 0x06005CCA RID: 23754 RVA: 0x0011B65F File Offset: 0x0011985F
		// (set) Token: 0x06005CCB RID: 23755 RVA: 0x0011B674 File Offset: 0x00119874
		[Category("Behavior")]
		[TypeConverter(typeof(UnitConverter))]
		[ClientControlProperty]
		[DefaultValue(typeof(Unit), "")]
		[Description("The maximum Width of the RadWindow in pixels.")]
		public Unit MaxWidth
		{
			get
			{
				return base.GetViewStateValue<Unit>("MaxWidth", Unit.Empty);
			}
			set
			{
				this.ViewState["MaxWidth"] = value;
				if (!this.Width.IsEmpty && !value.IsEmpty && this.Width.Value > value.Value)
				{
					this.Width = this.MaxWidth;
				}
			}
		}

		// Token: 0x17001E90 RID: 7824
		// (get) Token: 0x06005CCC RID: 23756 RVA: 0x0011B6D3 File Offset: 0x001198D3
		// (set) Token: 0x06005CCD RID: 23757 RVA: 0x0011B6E8 File Offset: 0x001198E8
		[Description("The Height of the RadWindow in pixels.")]
		[TypeConverter(typeof(UnitConverter))]
		[Category("Behavior")]
		[ClientControlProperty]
		[DefaultValue(typeof(Unit), "")]
		public override Unit Height
		{
			get
			{
				return base.GetViewStateValue<Unit>("Height", Unit.Empty);
			}
			set
			{
				if (!this.MaxHeight.IsEmpty && this.MaxHeight.Value < value.Value)
				{
					value = this.MaxHeight;
				}
				if (!this.MinHeight.IsEmpty && this.MinHeight.Value > value.Value)
				{
					value = this.MinHeight;
				}
				this.ViewState["Height"] = value;
			}
		}

		// Token: 0x17001E91 RID: 7825
		// (get) Token: 0x06005CCE RID: 23758 RVA: 0x0011B769 File Offset: 0x00119969
		// (set) Token: 0x06005CCF RID: 23759 RVA: 0x0011B77C File Offset: 0x0011997C
		[Category("Behavior")]
		[ClientControlProperty]
		[TypeConverter(typeof(UnitConverter))]
		[Description("The minimum Height of the RadWindow in pixels.")]
		[DefaultValue(typeof(Unit), "")]
		public Unit MinHeight
		{
			get
			{
				return base.GetViewStateValue<Unit>("MinHeight", Unit.Empty);
			}
			set
			{
				this.ViewState["MinHeight"] = value;
				if (!this.Height.IsEmpty && !value.IsEmpty && this.Height.Value < value.Value)
				{
					this.Height = this.MinHeight;
				}
			}
		}

		// Token: 0x17001E92 RID: 7826
		// (get) Token: 0x06005CD0 RID: 23760 RVA: 0x0011B7DB File Offset: 0x001199DB
		// (set) Token: 0x06005CD1 RID: 23761 RVA: 0x0011B7F0 File Offset: 0x001199F0
		[Category("Behavior")]
		[DefaultValue(typeof(Unit), "")]
		[Description("The maximum Height of the RadWindow in pixels.")]
		[TypeConverter(typeof(UnitConverter))]
		[ClientControlProperty]
		public Unit MaxHeight
		{
			get
			{
				return base.GetViewStateValue<Unit>("MaxHeight", Unit.Empty);
			}
			set
			{
				this.ViewState["MaxHeight"] = value;
				if (!this.Height.IsEmpty && !value.IsEmpty && this.Height.Value > value.Value)
				{
					this.Height = this.MaxHeight;
				}
			}
		}

		// Token: 0x17001E93 RID: 7827
		// (get) Token: 0x06005CD2 RID: 23762 RVA: 0x0011B84F File Offset: 0x00119A4F
		// (set) Token: 0x06005CD3 RID: 23763 RVA: 0x0011B86F File Offset: 0x00119A6F
		[Description("The title for the RadWindow")]
		[DefaultValue("")]
		[ClientControlProperty]
		[Category("Behavior")]
		public string Title
		{
			get
			{
				return ((string)this.ViewState["Title"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["Title"] = value;
			}
		}

		// Token: 0x17001E94 RID: 7828
		// (get) Token: 0x06005CD4 RID: 23764 RVA: 0x0011B882 File Offset: 0x00119A82
		// (set) Token: 0x06005CD5 RID: 23765 RVA: 0x0011B8B1 File Offset: 0x00119AB1
		[ClientControlProperty]
		[DefaultValue(typeof(Unit), "")]
		[Category("Behavior")]
		[Description("The horizontal distance (in pixels) from the left edge of browser viewport, or from the top left corner of the OffsetElement (if set). Not applicable for a Modal RadWindow")]
		public Unit Left
		{
			get
			{
				if (this.ViewState["Left"] == null)
				{
					return Unit.Empty;
				}
				return (Unit)this.ViewState["Left"];
			}
			set
			{
				this.ViewState["Left"] = value;
			}
		}

		// Token: 0x17001E95 RID: 7829
		// (get) Token: 0x06005CD6 RID: 23766 RVA: 0x0011B8C9 File Offset: 0x00119AC9
		// (set) Token: 0x06005CD7 RID: 23767 RVA: 0x0011B8DB File Offset: 0x00119ADB
		[DefaultValue(typeof(Unit), "")]
		[Category("Behavior")]
		[ClientControlProperty]
		[Description("The vertical distance (in pixels) from the top edge of browser viewport, or from the top left corner of the OffsetElement (if set). Not applicable for a Modal RadWindow")]
		public Unit Top
		{
			get
			{
				return base.GetViewStateValue<Unit>("Top", Unit.Empty);
			}
			set
			{
				this.ViewState["Top"] = value;
			}
		}

		// Token: 0x17001E96 RID: 7830
		// (get) Token: 0x06005CD8 RID: 23768 RVA: 0x0011B8F3 File Offset: 0x00119AF3
		// (set) Token: 0x06005CD9 RID: 23769 RVA: 0x0011B913 File Offset: 0x00119B13
		[Category("Behavior")]
		[Description("Specifies the id (ClientID if a runat=server is used) of a html element in which the windows will be able to move.")]
		[Bindable(true)]
		[ClientControlProperty]
		[Browsable(true)]
		[DefaultValue("")]
		public string RestrictionZoneID
		{
			get
			{
				return ((string)this.ViewState["RestrictionZoneID"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["RestrictionZoneID"] = value;
			}
		}

		// Token: 0x17001E97 RID: 7831
		// (get) Token: 0x06005CDA RID: 23770 RVA: 0x0011B926 File Offset: 0x00119B26
		// (set) Token: 0x06005CDB RID: 23771 RVA: 0x0011B934 File Offset: 0x00119B34
		[Bindable(true)]
		[ClientControlProperty]
		[Category("Behavior")]
		[Description("Specifies whether the RadWindow will be made inaccessible on the client once it is closed.")]
		[Browsable(true)]
		[DefaultValue(false)]
		public bool DestroyOnClose
		{
			get
			{
				return base.GetViewStateValue<bool>("DestroyOnClose", false);
			}
			set
			{
				this.ViewState["DestroyOnClose"] = value;
			}
		}

		// Token: 0x17001E98 RID: 7832
		// (get) Token: 0x06005CDC RID: 23772 RVA: 0x0011B94C File Offset: 0x00119B4C
		// (set) Token: 0x06005CDD RID: 23773 RVA: 0x0011B95A File Offset: 0x00119B5A
		[Browsable(true)]
		[Bindable(true)]
		[Category("Behavior")]
		[DefaultValue(false)]
		[Description("Specifies whether the page that is loaded in the RadWindow will be cached from the browser or no.")]
		[ClientControlProperty]
		public bool ReloadOnShow
		{
			get
			{
				return base.GetViewStateValue<bool>("ReloadOnShow", false);
			}
			set
			{
				this.ViewState["ReloadOnShow"] = value;
			}
		}

		// Token: 0x17001E99 RID: 7833
		// (get) Token: 0x06005CDE RID: 23774 RVA: 0x0011B972 File Offset: 0x00119B72
		// (set) Token: 0x06005CDF RID: 23775 RVA: 0x0011B980 File Offset: 0x00119B80
		[Bindable(true)]
		[Description("Indicates whether the page that is loaded in the RadWindow should be shown during the loading process, or when it has finished loading.")]
		[ClientControlProperty]
		[Browsable(true)]
		[Category("Behavior")]
		[DefaultValue(true)]
		public bool ShowContentDuringLoad
		{
			get
			{
				return base.GetViewStateValue<bool>("ShowContentDuringLoad", true);
			}
			set
			{
				this.ViewState["ShowContentDuringLoad"] = value;
			}
		}

		// Token: 0x17001E9A RID: 7834
		// (get) Token: 0x06005CE0 RID: 23776 RVA: 0x0011B998 File Offset: 0x00119B98
		// (set) Token: 0x06005CE1 RID: 23777 RVA: 0x0011B9A6 File Offset: 0x00119BA6
		[Description("Specifies whether the RadWindow will open automatically when the aspx page is loaded on the client.")]
		[ClientControlProperty]
		[Browsable(true)]
		[Bindable(true)]
		[Category("Behavior")]
		[DefaultValue(false)]
		public bool VisibleOnPageLoad
		{
			get
			{
				return base.GetViewStateValue<bool>("VisibleOnPageLoad", false);
			}
			set
			{
				this.ViewState["VisibleOnPageLoad"] = value;
			}
		}

		// Token: 0x17001E9B RID: 7835
		// (get) Token: 0x06005CE2 RID: 23778 RVA: 0x0011B9BE File Offset: 0x00119BBE
		// (set) Token: 0x06005CE3 RID: 23779 RVA: 0x0011B9CC File Offset: 0x00119BCC
		[Bindable(true)]
		[ClientControlProperty]
		[Browsable(true)]
		[Category("Behavior")]
		[DefaultValue(true)]
		[Description("Specifies whether the RadWindow has a title bar visible.")]
		public bool VisibleTitlebar
		{
			get
			{
				return base.GetViewStateValue<bool>("VisibleTitlebar", true);
			}
			set
			{
				this.ViewState["VisibleTitlebar"] = value;
			}
		}

		// Token: 0x17001E9C RID: 7836
		// (get) Token: 0x06005CE4 RID: 23780 RVA: 0x0011B9E4 File Offset: 0x00119BE4
		// (set) Token: 0x06005CE5 RID: 23781 RVA: 0x0011B9F2 File Offset: 0x00119BF2
		[Bindable(true)]
		[ClientControlProperty]
		[Browsable(true)]
		[Description("Specifies whether the RadWindow has a visible status bar or not.")]
		[Category("Behavior")]
		[DefaultValue(true)]
		public bool VisibleStatusbar
		{
			get
			{
				return base.GetViewStateValue<bool>("VisibleStatusbar", true);
			}
			set
			{
				this.ViewState["VisibleStatusbar"] = value;
			}
		}

		// Token: 0x17001E9D RID: 7837
		// (get) Token: 0x06005CE6 RID: 23782 RVA: 0x0011BA0A File Offset: 0x00119C0A
		// (set) Token: 0x06005CE7 RID: 23783 RVA: 0x0011BA18 File Offset: 0x00119C18
		[Description("Specifies whether the RadWindow is modal or not.")]
		[ClientControlProperty]
		[Browsable(true)]
		[Bindable(true)]
		[Category("Behavior")]
		[DefaultValue(false)]
		public bool Modal
		{
			get
			{
				return base.GetViewStateValue<bool>("Modal", false);
			}
			set
			{
				this.ViewState["Modal"] = value;
			}
		}

		// Token: 0x17001E9E RID: 7838
		// (get) Token: 0x06005CE8 RID: 23784 RVA: 0x0011BA30 File Offset: 0x00119C30
		// (set) Token: 0x06005CE9 RID: 23785 RVA: 0x0011BA3E File Offset: 0x00119C3E
		[Description("Specifies whether a modal RadWindow is centered automatically or not.")]
		[ClientControlProperty]
		[Category("Behavior")]
		[Browsable(true)]
		[Bindable(true)]
		[DefaultValue(true)]
		public bool CenterIfModal
		{
			get
			{
				return base.GetViewStateValue<bool>("CenterIfModal", true);
			}
			set
			{
				this.ViewState["CenterIfModal"] = value;
			}
		}

		// Token: 0x17001E9F RID: 7839
		// (get) Token: 0x06005CEA RID: 23786 RVA: 0x0011BA56 File Offset: 0x00119C56
		// (set) Token: 0x06005CEB RID: 23787 RVA: 0x0011BA64 File Offset: 0x00119C64
		[Category("Behavior")]
		[Description("Specifies whether the RadWindow will create an overlay element to ensure it will be displayed over a flash element.")]
		[DefaultValue(false)]
		[ClientControlProperty]
		[Browsable(true)]
		[Bindable(true)]
		public bool Overlay
		{
			get
			{
				return base.GetViewStateValue<bool>("Overlay", false);
			}
			set
			{
				this.ViewState["Overlay"] = value;
			}
		}

		// Token: 0x17001EA0 RID: 7840
		// (get) Token: 0x06005CEC RID: 23788 RVA: 0x0011BA7C File Offset: 0x00119C7C
		// (set) Token: 0x06005CED RID: 23789 RVA: 0x0011BA9D File Offset: 0x00119C9D
		[DefaultValue(false)]
		[ClientControlProperty]
		[ClientPropertyName("enableAriaSupport")]
		[Description("When set to true enables support for WAI-ARIA")]
		[Category("Behavior")]
		public bool EnableAriaSupport
		{
			get
			{
				return (bool)(this.ViewState["EnableAriaSupport"] ?? false);
			}
			set
			{
				this.ViewState["EnableAriaSupport"] = value;
			}
		}

		// Token: 0x17001EA1 RID: 7841
		// (get) Token: 0x06005CEE RID: 23790 RVA: 0x0011BAB8 File Offset: 0x00119CB8
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Description("Gets the object that controls the WAI-ARIA settings applied on the control's element.")]
		[DefaultValue(null)]
		[MergableProperty(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public WindowWaiAriaSettings AriaSettings
		{
			get
			{
				WindowWaiAriaSettings result;
				if ((result = this._ariaSettings) == null)
				{
					result = (this._ariaSettings = new WindowWaiAriaSettings());
				}
				return result;
			}
		}

		// Token: 0x17001EA2 RID: 7842
		// (get) Token: 0x06005CEF RID: 23791 RVA: 0x0011BADD File Offset: 0x00119CDD
		// (set) Token: 0x06005CF0 RID: 23792 RVA: 0x0011BAE5 File Offset: 0x00119CE5
		[Description("Gets or sets the TabIndex of the RadWidnow control.")]
		[ClientPropertyName("tabIndex")]
		[NotifyParentProperty(true)]
		public override short TabIndex
		{
			get
			{
				return base.TabIndex;
			}
			set
			{
				this._tabIndex = new short?(value);
				base.TabIndex = value;
			}
		}

		// Token: 0x17001EA3 RID: 7843
		// (get) Token: 0x06005CF1 RID: 23793 RVA: 0x0011BAFC File Offset: 0x00119CFC
		// (set) Token: 0x06005CF2 RID: 23794 RVA: 0x0011BB24 File Offset: 0x00119D24
		[Category("Appearance")]
		[DefaultValue(100)]
		[Description("Specifies what should the opacity of the RadWindow be.")]
		[ClientControlProperty]
		public int Opacity
		{
			get
			{
				return int.Parse(base.GetViewStateValue<int>("Opacity", 100).ToString());
			}
			set
			{
				if (value < 0 || value > 100)
				{
					throw new ArgumentOutOfRangeException("Opacity", "The Opacity value should be between 0 and 100");
				}
				this.ViewState["Opacity"] = value;
			}
		}

		// Token: 0x17001EA4 RID: 7844
		// (get) Token: 0x06005CF3 RID: 23795 RVA: 0x0011BB62 File Offset: 0x00119D62
		// (set) Token: 0x06005CF4 RID: 23796 RVA: 0x0011BB70 File Offset: 0x00119D70
		[Browsable(true)]
		[DefaultValue(false)]
		[Description("Specifies whether the RadWindow will stay in the visible viewport of the browser window.")]
		[Bindable(true)]
		[Category("Behavior")]
		[ClientControlProperty]
		public bool KeepInScreenBounds
		{
			get
			{
				return base.GetViewStateValue<bool>("KeepInScreenBounds", false);
			}
			set
			{
				this.ViewState["KeepInScreenBounds"] = value;
			}
		}

		// Token: 0x17001EA5 RID: 7845
		// (get) Token: 0x06005CF5 RID: 23797 RVA: 0x0011BB88 File Offset: 0x00119D88
		// (set) Token: 0x06005CF6 RID: 23798 RVA: 0x0011BB96 File Offset: 0x00119D96
		[DefaultValue(false)]
		[Category("Behavior")]
		[ClientControlProperty]
		[Description("Specifies whether the window will automatically resize itself according to its content or not.")]
		[Browsable(true)]
		[Bindable(true)]
		public bool AutoSize
		{
			get
			{
				return base.GetViewStateValue<bool>("AutoSize", false);
			}
			set
			{
				this.ViewState["AutoSize"] = value;
			}
		}

		// Token: 0x17001EA6 RID: 7846
		// (get) Token: 0x06005CF7 RID: 23799 RVA: 0x0011BBAE File Offset: 0x00119DAE
		// (set) Token: 0x06005CF8 RID: 23800 RVA: 0x0011BBCE File Offset: 0x00119DCE
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientPropertyName("command")]
		[Category("Client-side events")]
		[Description("The name of the client-side JavaScript function that executes when a RadWindow command (Restore, Minimize, Maximize, Pin On, Pin Off, Reload) is raised.")]
		[DefaultValue("")]
		[ClientControlEvent]
		public string OnClientCommand
		{
			get
			{
				return ((string)this.ViewState["OnClientCommand"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["OnClientCommand"] = value;
			}
		}

		// Token: 0x17001EA7 RID: 7847
		// (get) Token: 0x06005CF9 RID: 23801 RVA: 0x0011BBE1 File Offset: 0x00119DE1
		// (set) Token: 0x06005CFA RID: 23802 RVA: 0x0011BBF3 File Offset: 0x00119DF3
		[Description("The name of the client-side JavaScript function that executes when a RadWindow ResizeStart event is raised.")]
		[ClientControlEvent]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientPropertyName("resizeStart")]
		[Category("Client-side events")]
		public string OnClientResizeStart
		{
			get
			{
				return base.GetViewStateValue<string>("OnClientResizeStart", string.Empty);
			}
			set
			{
				this.ViewState["OnClientResizeStart"] = value;
			}
		}

		// Token: 0x17001EA8 RID: 7848
		// (get) Token: 0x06005CFB RID: 23803 RVA: 0x0011BC06 File Offset: 0x00119E06
		// (set) Token: 0x06005CFC RID: 23804 RVA: 0x0011BC0E File Offset: 0x00119E0E
		[Obsolete("This property is now obsolete. Please use the OnClientResizeEnd property instead.", false)]
		[DefaultValue("")]
		public string OnClientResize
		{
			get
			{
				return this.OnClientResizeEnd;
			}
			set
			{
				this.OnClientResizeEnd = value;
			}
		}

		// Token: 0x17001EA9 RID: 7849
		// (get) Token: 0x06005CFD RID: 23805 RVA: 0x0011BC17 File Offset: 0x00119E17
		// (set) Token: 0x06005CFE RID: 23806 RVA: 0x0011BC29 File Offset: 0x00119E29
		[DefaultValue("")]
		[ClientPropertyName("resizeEnd")]
		[Category("Client-side events")]
		[Description("The name of the client-side JavaScript function that executes when a RadWindow Resize event is raised.")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		public string OnClientResizeEnd
		{
			get
			{
				return base.GetViewStateValue<string>("OnClientResizeEnd", string.Empty);
			}
			set
			{
				this.ViewState["OnClientResizeEnd"] = value;
			}
		}

		// Token: 0x17001EAA RID: 7850
		// (get) Token: 0x06005CFF RID: 23807 RVA: 0x0011BC3C File Offset: 0x00119E3C
		// (set) Token: 0x06005D00 RID: 23808 RVA: 0x0011BC5C File Offset: 0x00119E5C
		[ClientPropertyName("dragStart")]
		[DefaultValue("")]
		[Category("Client-side events")]
		[Description("The name of the client-side JavaScript function that executes when a RadWindow DragStart event is raised.")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		public string OnClientDragStart
		{
			get
			{
				return ((string)this.ViewState["OnClientDragStart"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["OnClientDragStart"] = value;
			}
		}

		// Token: 0x17001EAB RID: 7851
		// (get) Token: 0x06005D01 RID: 23809 RVA: 0x0011BC6F File Offset: 0x00119E6F
		// (set) Token: 0x06005D02 RID: 23810 RVA: 0x0011BC8F File Offset: 0x00119E8F
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Description("The name of the client-side JavaScript function that executes when a RadWindow DragEnd event is raised.")]
		[DefaultValue("")]
		[ClientControlEvent]
		[ClientPropertyName("dragEnd")]
		[Category("Client-side events")]
		public string OnClientDragEnd
		{
			get
			{
				return ((string)this.ViewState["OnClientDragEnd"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["OnClientDragEnd"] = value;
			}
		}

		// Token: 0x17001EAC RID: 7852
		// (get) Token: 0x06005D03 RID: 23811 RVA: 0x0011BCA2 File Offset: 0x00119EA2
		// (set) Token: 0x06005D04 RID: 23812 RVA: 0x0011BCC2 File Offset: 0x00119EC2
		[ClientControlEvent]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Description("The name of the client-side JavaScript function that executes when RadWindow AutoSize has finished.")]
		[ClientPropertyName("autoSizeEnd")]
		[Category("Client-side events")]
		public string OnClientAutoSizeEnd
		{
			get
			{
				return ((string)this.ViewState["OnClientAutoSizeEnd"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["OnClientAutoSizeEnd"] = value;
			}
		}

		// Token: 0x17001EAD RID: 7853
		// (get) Token: 0x06005D05 RID: 23813 RVA: 0x0011BCD5 File Offset: 0x00119ED5
		// (set) Token: 0x06005D06 RID: 23814 RVA: 0x0011BCF5 File Offset: 0x00119EF5
		[Description("The name of the client-side JavaScript function that is called when the RadWindow becomes the active visible window.")]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[ClientPropertyName("activate")]
		[Category("Client-side events")]
		public virtual string OnClientActivate
		{
			get
			{
				return ((string)this.ViewState["OnClientActivate"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["OnClientActivate"] = value;
			}
		}

		// Token: 0x17001EAE RID: 7854
		// (get) Token: 0x06005D07 RID: 23815 RVA: 0x0011BD08 File Offset: 0x00119F08
		// (set) Token: 0x06005D08 RID: 23816 RVA: 0x0011BD28 File Offset: 0x00119F28
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Description("The name of the client-side JavaScript function that is called just before the RadWindow is shown. The event can be canceled.")]
		[DefaultValue("")]
		[ClientControlEvent]
		[ClientPropertyName("beforeShow")]
		[Category("Client-side events")]
		public virtual string OnClientBeforeShow
		{
			get
			{
				return ((string)this.ViewState["OnClientBeforeShow"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["OnClientBeforeShow"] = value;
			}
		}

		// Token: 0x17001EAF RID: 7855
		// (get) Token: 0x06005D09 RID: 23817 RVA: 0x0011BD3B File Offset: 0x00119F3B
		// (set) Token: 0x06005D0A RID: 23818 RVA: 0x0011BD5B File Offset: 0x00119F5B
		[Category("Client-side events")]
		[Description("The name of the client-side JavaScript function that is called when the RadWindow is shown.")]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[ClientPropertyName("show")]
		public virtual string OnClientShow
		{
			get
			{
				return ((string)this.ViewState["OnClientShow"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["OnClientShow"] = value;
			}
		}

		// Token: 0x17001EB0 RID: 7856
		// (get) Token: 0x06005D0B RID: 23819 RVA: 0x0011BD6E File Offset: 0x00119F6E
		// (set) Token: 0x06005D0C RID: 23820 RVA: 0x0011BD8E File Offset: 0x00119F8E
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Category("Client-side events")]
		[Description("The name of the client-side JavaScript function that is called when the page inside the RadWindow object completes loading.")]
		[DefaultValue("")]
		[ClientControlEvent]
		[ClientPropertyName("pageLoad")]
		public virtual string OnClientPageLoad
		{
			get
			{
				return ((string)this.ViewState["OnClientPageLoad"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["OnClientPageLoad"] = value;
			}
		}

		// Token: 0x17001EB1 RID: 7857
		// (get) Token: 0x06005D0D RID: 23821 RVA: 0x0011BDA1 File Offset: 0x00119FA1
		// (set) Token: 0x06005D0E RID: 23822 RVA: 0x0011BDC1 File Offset: 0x00119FC1
		[DefaultValue("")]
		[Description("The name of the client-side JavaScript function that is called when the RadWindow is closed.")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[ClientPropertyName("close")]
		[Category("Client-side events")]
		public virtual string OnClientClose
		{
			get
			{
				return ((string)this.ViewState["OnClientClose"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["OnClientClose"] = value;
			}
		}

		// Token: 0x17001EB2 RID: 7858
		// (get) Token: 0x06005D0F RID: 23823 RVA: 0x0011BDD4 File Offset: 0x00119FD4
		// (set) Token: 0x06005D10 RID: 23824 RVA: 0x0011BDF4 File Offset: 0x00119FF4
		[DefaultValue("")]
		[Description("The name of the client-side JavaScript function that is called just before the RadWindow is closed. The event can be canceled.")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[ClientPropertyName("beforeClose")]
		[Category("Client-side events")]
		public virtual string OnClientBeforeClose
		{
			get
			{
				return ((string)this.ViewState["OnClientBeforeClose"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["OnClientBeforeClose"] = value;
			}
		}

		// Token: 0x06005D11 RID: 23825 RVA: 0x0011BE08 File Offset: 0x0011A008
		protected override Style CreateControlStyle()
		{
			Style result = base.CreateControlStyle();
			if (!base.DesignMode)
			{
				base.Style.Add("display", "none");
			}
			return result;
		}

		// Token: 0x17001EB3 RID: 7859
		// (get) Token: 0x06005D12 RID: 23826 RVA: 0x0011BE3A File Offset: 0x0011A03A
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Div;
			}
		}

		// Token: 0x17001EB4 RID: 7860
		// (get) Token: 0x06005D13 RID: 23827 RVA: 0x0011BE3E File Offset: 0x0011A03E
		protected override string CssClassFormatString
		{
			get
			{
				return "";
			}
		}

		// Token: 0x17001EB5 RID: 7861
		// (get) Token: 0x06005D14 RID: 23828 RVA: 0x0011BE45 File Offset: 0x0011A045
		protected internal override bool SupportsRenderingMode
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06005D15 RID: 23829 RVA: 0x0011BE48 File Offset: 0x0011A048
		private string GetMinimizeIconUrl()
		{
			string iconUrl = (!string.IsNullOrEmpty(this.MinimizeIconUrl)) ? this.MinimizeIconUrl : this.IconUrl;
			return RadWindowBase.GetIconUrl(iconUrl);
		}

		// Token: 0x06005D16 RID: 23830 RVA: 0x0011BE77 File Offset: 0x0011A077
		private string GetIconUrl()
		{
			return RadWindowBase.GetIconUrl(this.IconUrl);
		}

		// Token: 0x06005D17 RID: 23831 RVA: 0x0011BE84 File Offset: 0x0011A084
		private static string GetIconUrl(string iconUrl)
		{
			string result = "";
			if (iconUrl.StartsWith("~"))
			{
				result = VirtualPathUtility.ToAbsolute(iconUrl);
			}
			else if (!string.IsNullOrEmpty(iconUrl))
			{
				result = iconUrl;
			}
			return result;
		}

		// Token: 0x06005D18 RID: 23832 RVA: 0x0011BEB8 File Offset: 0x0011A0B8
		protected override void DescribeComponent(IScriptDescriptor descriptor)
		{
			base.DescribeComponent(descriptor);
			base.DescribeRenderMode(descriptor);
			descriptor.AddProperty("skin", base.RuntimeSkin);
			if (this.Page.Form != null)
			{
				descriptor.AddProperty("formID", this.Page.Form.ClientID);
			}
			if (!this.Localization.isDefault())
			{
				descriptor.AddProperty("localization", new JavaScriptSerializer().Serialize(this.Localization.getLocalizationStrings()));
			}
			if (this.EnableAriaSupport)
			{
				WindowShortcut windowShortcut = new WindowShortcut("close", "Esc");
				if (this.Shortcuts.ToString().IndexOf(windowShortcut.ToString()) < 0)
				{
					this.Shortcuts.Add(windowShortcut);
				}
			}
			if (this.Shortcuts.Count > 0)
			{
				descriptor.AddProperty("shortcuts", this.Shortcuts.ToString());
			}
			short? tabIndex = this._tabIndex;
			int? num = (tabIndex != null) ? new int?((int)tabIndex.GetValueOrDefault()) : null;
			if (num != null)
			{
				descriptor.AddProperty("tabIndex", this.TabIndex);
			}
			descriptor.AddProperty("name", this.ID);
			descriptor.AddProperty("iconUrl", this.GetIconUrl());
			descriptor.AddProperty("minimizeIconUrl", this.GetMinimizeIconUrl());
			this.AriaSettings.Describe(descriptor);
		}

		// Token: 0x06005D19 RID: 23833 RVA: 0x0011C020 File Offset: 0x0011A220
		internal Control FindControlRecursive(string id)
		{
			Control control;
			if (this.Page.Master != null)
			{
				control = this.Page.Master;
			}
			else
			{
				control = this.Page;
			}
			Control control2 = control.FindControl(id);
			if (control2 != null)
			{
				return control2;
			}
			return this.FindControlRecursive(id, control);
		}

		// Token: 0x06005D1A RID: 23834 RVA: 0x0011C064 File Offset: 0x0011A264
		private Control FindControlRecursive(string id, Control root)
		{
			Control control = null;
			foreach (object obj in root.Controls)
			{
				Control control2 = (Control)obj;
				if (control2 is INamingContainer && control2.FindControl(id) != null)
				{
					control = control2.FindControl(id);
					break;
				}
				if (control2.HasControls())
				{
					control = this.FindControlRecursive(id, control2);
					if (control != null && control.ID == id)
					{
						break;
					}
				}
			}
			return control;
		}

		// Token: 0x06005D1B RID: 23835 RVA: 0x0011C0F8 File Offset: 0x0011A2F8
		protected override void LoadViewState(object state)
		{
			object[] array = (object[])state;
			base.LoadViewState(array[0]);
			((IStateManager)this.Localization).LoadViewState(array[1]);
			((IStateManager)this.Shortcuts).LoadViewState(array[2]);
			((IStateManager)this.AriaSettings).LoadViewState(array[3]);
		}

		// Token: 0x06005D1C RID: 23836 RVA: 0x0011C140 File Offset: 0x0011A340
		protected override object SaveViewState()
		{
			return new object[]
			{
				base.SaveViewState(),
				((IStateManager)this.Localization).SaveViewState(),
				((IStateManager)this.Shortcuts).SaveViewState(),
				((IStateManager)this.AriaSettings).SaveViewState()
			};
		}

		// Token: 0x06005D1D RID: 23837 RVA: 0x0011C18A File Offset: 0x0011A38A
		protected override void TrackViewState()
		{
			base.TrackViewState();
			((IStateManager)this.Localization).TrackViewState();
			((IStateManager)this.Shortcuts).TrackViewState();
			((IStateManager)this.AriaSettings).TrackViewState();
		}

		// Token: 0x06005D1E RID: 23838 RVA: 0x0011C1B4 File Offset: 0x0011A3B4
		protected internal override void DescribeClientProperties(IScriptDescriptor descriptor)
		{
			base.DescribeProperty<WindowAnimation>(descriptor, "animation", this.Animation, WindowAnimation.None);
			base.DescribeProperty<int>(descriptor, "animationDuration", this.AnimationDuration, 500);
			base.DescribeProperty<bool>(descriptor, "autoSize", this.AutoSize, false);
			base.DescribeProperty<WindowAutoSizeBehaviors>(descriptor, "autoSizeBehaviors", this.AutoSizeBehaviors, WindowAutoSizeBehaviors.Default);
			base.DescribeProperty<WindowBehaviors>(descriptor, "behaviors", this.Behaviors, WindowBehaviors.Default);
			base.DescribeProperty<bool>(descriptor, "centerIfModal", this.CenterIfModal, true);
			base.DescribeProperty<bool>(descriptor, "destroyOnClose", this.DestroyOnClose, false);
			base.DescribeProperty<bool>(descriptor, "enableAriaSupport", this.EnableAriaSupport, false);
			base.DescribeProperty<bool>(descriptor, "enableShadow", this.EnableShadow, false);
			base.DescribeProperty<string>(descriptor, "height", this.Height.ToString(CultureInfo.InvariantCulture), "");
			base.DescribeProperty<WindowBehaviors>(descriptor, "initialBehaviors", this.InitialBehaviors, WindowBehaviors.None);
			base.DescribeProperty<bool>(descriptor, "keepInScreenBounds", this.KeepInScreenBounds, false);
			base.DescribeProperty<string>(descriptor, "left", this.Left.ToString(CultureInfo.InvariantCulture), "");
			base.DescribeProperty<string>(descriptor, "maxHeight", this.MaxHeight.ToString(CultureInfo.InvariantCulture), "");
			base.DescribeProperty<string>(descriptor, "maxWidth", this.MaxWidth.ToString(CultureInfo.InvariantCulture), "");
			base.DescribeProperty<string>(descriptor, "minHeight", this.MinHeight.ToString(CultureInfo.InvariantCulture), "");
			base.DescribeProperty<string>(descriptor, "minimizeZoneID", this.MinimizeZoneID, "");
			base.DescribeProperty<string>(descriptor, "minWidth", this.MinWidth.ToString(CultureInfo.InvariantCulture), "");
			base.DescribeProperty<bool>(descriptor, "modal", this.Modal, false);
			base.DescribeProperty<string>(descriptor, "offsetElementID", this.OffsetElementID, "");
			base.DescribeProperty<int>(descriptor, "opacity", this.Opacity, 100);
			base.DescribeProperty<bool>(descriptor, "overlay", this.Overlay, false);
			base.DescribeProperty<bool>(descriptor, "reloadOnShow", this.ReloadOnShow, false);
			base.DescribeProperty<string>(descriptor, "restrictionZoneID", this.RestrictionZoneID, "");
			base.DescribeProperty<bool>(descriptor, "showContentDuringLoad", this.ShowContentDuringLoad, true);
			base.DescribeProperty<bool>(descriptor, "showOnTopWhenMaximized", this.ShowOnTopWhenMaximized, true);
			base.DescribeProperty<string>(descriptor, "title", this.Title, "");
			base.DescribeProperty<string>(descriptor, "top", this.Top.ToString(CultureInfo.InvariantCulture), "");
			base.DescribeProperty<bool>(descriptor, "visibleOnPageLoad", this.VisibleOnPageLoad, false);
			base.DescribeProperty<bool>(descriptor, "visibleStatusbar", this.VisibleStatusbar, true);
			base.DescribeProperty<bool>(descriptor, "visibleTitlebar", this.VisibleTitlebar, true);
			base.DescribeProperty<string>(descriptor, "width", this.Width.ToString(CultureInfo.InvariantCulture), "");
			base.DescribeClientProperties(descriptor);
		}

		// Token: 0x06005D1F RID: 23839 RVA: 0x0011C4CC File Offset: 0x0011A6CC
		protected internal override void DescribeClientEvents(IScriptDescriptor descriptor)
		{
			RadWebControl.DescribeEvent(descriptor, "activate", this.OnClientActivate);
			RadWebControl.DescribeEvent(descriptor, "autoSizeEnd", this.OnClientAutoSizeEnd);
			RadWebControl.DescribeEvent(descriptor, "beforeClose", this.OnClientBeforeClose);
			RadWebControl.DescribeEvent(descriptor, "beforeShow", this.OnClientBeforeShow);
			RadWebControl.DescribeEvent(descriptor, "close", this.OnClientClose);
			RadWebControl.DescribeEvent(descriptor, "command", this.OnClientCommand);
			RadWebControl.DescribeEvent(descriptor, "dragEnd", this.OnClientDragEnd);
			RadWebControl.DescribeEvent(descriptor, "dragStart", this.OnClientDragStart);
			RadWebControl.DescribeEvent(descriptor, "pageLoad", this.OnClientPageLoad);
			RadWebControl.DescribeEvent(descriptor, "resizeEnd", this.OnClientResizeEnd);
			RadWebControl.DescribeEvent(descriptor, "resizeStart", this.OnClientResizeStart);
			RadWebControl.DescribeEvent(descriptor, "show", this.OnClientShow);
			base.DescribeClientEvents(descriptor);
		}

		// Token: 0x04001647 RID: 5703
		private RadWindowLocalization _localization;

		// Token: 0x04001648 RID: 5704
		private WindowShortcutCollection _shortcuts;

		// Token: 0x04001649 RID: 5705
		private WindowWaiAriaSettings _ariaSettings;

		// Token: 0x0400164A RID: 5706
		private short? _tabIndex = null;
	}
}
