using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Globalization;
using System.Reflection;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02000E0C RID: 3596
	[RequiredCss("Telerik.Web.UI.Skins.Common.MaterialRipple.css", RenderMode.Lightweight, typeof(RadButton))]
	[RequiredScript(typeof(RadToolTipScripts))]
	[ClientScriptResource("Telerik.Web.UI.RadToolTip", "Telerik.Web.UI.Common.Core.js")]
	[EmbeddedSkin("ToolTip")]
	[EmbeddedSkin("ToolTip", "Default")]
	[RequiredCss("Telerik.Web.UI.Skins.Common.fonticons.css", RenderMode.Lightweight, typeof(RadToolTipBase))]
	public abstract class RadToolTipBase : RadWebControl
	{
		// Token: 0x06008547 RID: 34119 RVA: 0x001E6664 File Offset: 0x001E4864
		public void Show()
		{
			this._showMethodInvoked = true;
		}

		// Token: 0x17002A2E RID: 10798
		// (get) Token: 0x06008548 RID: 34120 RVA: 0x001E666D File Offset: 0x001E486D
		// (set) Token: 0x06008549 RID: 34121 RVA: 0x001E6698 File Offset: 0x001E4898
		[Description("Specifies whether the tooltip will open automatically when the aspx page is loaded on the client.")]
		[Category("Behavior")]
		[DefaultValue(false)]
		[Browsable(true)]
		[Bindable(true)]
		[ClientControlProperty]
		public bool VisibleOnPageLoad
		{
			get
			{
				return this.ViewState["VisibleOnPageLoad"] != null && (bool)this.ViewState["VisibleOnPageLoad"];
			}
			set
			{
				this.ViewState["VisibleOnPageLoad"] = value;
			}
		}

		// Token: 0x17002A2F RID: 10799
		// (get) Token: 0x0600854A RID: 34122 RVA: 0x001E66B0 File Offset: 0x001E48B0
		// (set) Token: 0x0600854B RID: 34123 RVA: 0x001E66DB File Offset: 0x001E48DB
		[Description("Specifies the animation effect of the tooltip")]
		[ClientControlProperty]
		[DefaultValue(ToolTipAnimation.None)]
		[Category("Behavior")]
		public ToolTipAnimation Animation
		{
			get
			{
				if (this.ViewState["Animation"] == null)
				{
					return ToolTipAnimation.None;
				}
				return (ToolTipAnimation)this.ViewState["Animation"];
			}
			set
			{
				this.ViewState["Animation"] = value;
			}
		}

		// Token: 0x17002A30 RID: 10800
		// (get) Token: 0x0600854C RID: 34124 RVA: 0x001E66F3 File Offset: 0x001E48F3
		// (set) Token: 0x0600854D RID: 34125 RVA: 0x001E6722 File Offset: 0x001E4922
		[Description("Sets/gets the duration of the animation in milliseconds.")]
		[Category("Behavior")]
		[DefaultValue(500)]
		[ClientControlProperty]
		public int AnimationDuration
		{
			get
			{
				if (this.ViewState["AnimationDuration"] == null)
				{
					return 500;
				}
				return (int)this.ViewState["AnimationDuration"];
			}
			set
			{
				this.ViewState["AnimationDuration"] = value;
			}
		}

		// Token: 0x17002A31 RID: 10801
		// (get) Token: 0x0600854E RID: 34126 RVA: 0x001E673A File Offset: 0x001E493A
		// (set) Token: 0x0600854F RID: 34127 RVA: 0x001E6745 File Offset: 0x001E4945
		[Category("Appearance")]
		[Obsolete("This property is obsolete. Please use HideEvent=\"ManualClose\" instead.")]
		[DefaultValue(false)]
		public bool ManualClose
		{
			get
			{
				return this.HideEvent == ToolTipHideEvent.ManualClose;
			}
			set
			{
				this.HideEvent = (value ? ToolTipHideEvent.ManualClose : ToolTipHideEvent.Default);
			}
		}

		// Token: 0x17002A32 RID: 10802
		// (get) Token: 0x06008550 RID: 34128 RVA: 0x001E6754 File Offset: 0x001E4954
		// (set) Token: 0x06008551 RID: 34129 RVA: 0x001E675F File Offset: 0x001E495F
		[DefaultValue(false)]
		[Obsolete("This property is obsolete. Please use HideEvent=\"LeaveToolTip\" instead.")]
		[Category("Appearance")]
		public bool Sticky
		{
			get
			{
				return this.HideEvent == ToolTipHideEvent.LeaveToolTip;
			}
			set
			{
				this.HideEvent = (value ? ToolTipHideEvent.LeaveToolTip : ToolTipHideEvent.Default);
			}
		}

		// Token: 0x17002A33 RID: 10803
		// (get) Token: 0x06008552 RID: 34130 RVA: 0x001E676E File Offset: 0x001E496E
		// (set) Token: 0x06008553 RID: 34131 RVA: 0x001E6799 File Offset: 0x001E4999
		[Description("Specifies the client event at which the tooltip will be hidden.")]
		[ClientControlProperty]
		[DefaultValue(ToolTipHideEvent.Default)]
		[Category("Behavior")]
		public ToolTipHideEvent HideEvent
		{
			get
			{
				if (this.ViewState["HideEvent"] == null)
				{
					return ToolTipHideEvent.Default;
				}
				return (ToolTipHideEvent)this.ViewState["HideEvent"];
			}
			set
			{
				this.ViewState["HideEvent"] = value;
			}
		}

		// Token: 0x17002A34 RID: 10804
		// (get) Token: 0x06008554 RID: 34132 RVA: 0x001E67B1 File Offset: 0x001E49B1
		// (set) Token: 0x06008555 RID: 34133 RVA: 0x001E67DC File Offset: 0x001E49DC
		[ClientControlProperty]
		[Category("Behavior")]
		[DefaultValue(ToolTipShowEvent.OnMouseOver)]
		public ToolTipShowEvent ShowEvent
		{
			get
			{
				if (this.ViewState["ShowEvent"] == null)
				{
					return ToolTipShowEvent.OnMouseOver;
				}
				return (ToolTipShowEvent)this.ViewState["ShowEvent"];
			}
			set
			{
				this.ViewState["ShowEvent"] = value;
			}
		}

		// Token: 0x17002A35 RID: 10805
		// (get) Token: 0x06008556 RID: 34134 RVA: 0x001E67F4 File Offset: 0x001E49F4
		// (set) Token: 0x06008557 RID: 34135 RVA: 0x001E6823 File Offset: 0x001E4A23
		[Description("Specifies the Width of the tooltip in pixels")]
		[ClientControlProperty]
		[DefaultValue(typeof(Unit), "")]
		[TypeConverter(typeof(UnitConverter))]
		[Category("Behavior")]
		public override Unit Width
		{
			get
			{
				if (this.ViewState["Width"] == null)
				{
					return Unit.Empty;
				}
				return (Unit)this.ViewState["Width"];
			}
			set
			{
				this.ViewState["Width"] = value;
			}
		}

		// Token: 0x17002A36 RID: 10806
		// (get) Token: 0x06008558 RID: 34136 RVA: 0x001E683B File Offset: 0x001E4A3B
		// (set) Token: 0x06008559 RID: 34137 RVA: 0x001E686A File Offset: 0x001E4A6A
		[Description("Specifies the Height of the tooltip in pixels")]
		[Category("Behavior")]
		[ClientControlProperty]
		[DefaultValue(typeof(Unit), "")]
		[TypeConverter(typeof(UnitConverter))]
		public override Unit Height
		{
			get
			{
				if (this.ViewState["Height"] == null)
				{
					return Unit.Empty;
				}
				return (Unit)this.ViewState["Height"];
			}
			set
			{
				this.ViewState["Height"] = value;
			}
		}

		// Token: 0x17002A37 RID: 10807
		// (get) Token: 0x0600855A RID: 34138 RVA: 0x001E6882 File Offset: 0x001E4A82
		// (set) Token: 0x0600855B RID: 34139 RVA: 0x001E68B1 File Offset: 0x001E4AB1
		[Description("Specifies The Text that will appear in the tooltip if it must be different from the title attribute of the target element.")]
		[Category("Behavior")]
		[ClientControlProperty]
		[DefaultValue("")]
		public string Text
		{
			get
			{
				if (this.ViewState["Text"] == null)
				{
					return "";
				}
				return (string)this.ViewState["Text"];
			}
			set
			{
				this.ViewState["Text"] = value;
			}
		}

		// Token: 0x17002A38 RID: 10808
		// (get) Token: 0x0600855C RID: 34140 RVA: 0x001E68C4 File Offset: 0x001E4AC4
		// (set) Token: 0x0600855D RID: 34141 RVA: 0x001E68EF File Offset: 0x001E4AEF
		[Category("Behavior")]
		[Description("Indicates whether the Alt specified for the target should be ignored or not.")]
		[ClientControlProperty]
		[DefaultValue(false)]
		public bool IgnoreAltAttribute
		{
			get
			{
				return this.ViewState["IgnoreAltAttribute"] != null && (bool)this.ViewState["IgnoreAltAttribute"];
			}
			set
			{
				this.ViewState["IgnoreAltAttribute"] = value;
			}
		}

		// Token: 0x17002A39 RID: 10809
		// (get) Token: 0x0600855E RID: 34142 RVA: 0x001E6907 File Offset: 0x001E4B07
		// (set) Token: 0x0600855F RID: 34143 RVA: 0x001E6936 File Offset: 0x001E4B36
		[ClientControlProperty]
		[Category("Behavior")]
		[Description("Specifies a title for the tooltip.")]
		[DefaultValue("")]
		public string Title
		{
			get
			{
				if (this.ViewState["Title"] == null)
				{
					return "";
				}
				return (string)this.ViewState["Title"];
			}
			set
			{
				this.ViewState["Title"] = value;
			}
		}

		// Token: 0x17002A3A RID: 10810
		// (get) Token: 0x06008560 RID: 34144 RVA: 0x001E6949 File Offset: 0x001E4B49
		// (set) Token: 0x06008561 RID: 34145 RVA: 0x001E6978 File Offset: 0x001E4B78
		[Description("Specifies the manual close button's tooltip text.")]
		[DefaultValue("Close")]
		[Localizable(true)]
		public string ManualCloseButtonText
		{
			get
			{
				if (this.ViewState["ManualCloseButtonText"] == null)
				{
					return "Close";
				}
				return (string)this.ViewState["ManualCloseButtonText"];
			}
			set
			{
				this.ViewState["ManualCloseButtonText"] = value;
			}
		}

		// Token: 0x17002A3B RID: 10811
		// (get) Token: 0x06008562 RID: 34146 RVA: 0x001E698B File Offset: 0x001E4B8B
		// (set) Token: 0x06008563 RID: 34147 RVA: 0x001E69B7 File Offset: 0x001E4BB7
		[Category("Layout")]
		[Description("Specifies the position of the tooltip relative to the target element.")]
		[ClientControlProperty]
		[DefaultValue(ToolTipPosition.BottomCenter)]
		public ToolTipPosition Position
		{
			get
			{
				if (this.ViewState["Position"] == null)
				{
					return ToolTipPosition.BottomCenter;
				}
				return (ToolTipPosition)this.ViewState["Position"];
			}
			set
			{
				this.ViewState["Position"] = value;
			}
		}

		// Token: 0x17002A3C RID: 10812
		// (get) Token: 0x06008564 RID: 34148 RVA: 0x001E69CF File Offset: 0x001E4BCF
		// (set) Token: 0x06008565 RID: 34149 RVA: 0x001E69FA File Offset: 0x001E4BFA
		[ClientControlProperty]
		[Category("Layout")]
		[DefaultValue(ToolTipScrolling.Default)]
		[Description("Specifies the overflow of the tooltip's content area.")]
		public ToolTipScrolling ContentScrolling
		{
			get
			{
				if (this.ViewState["ContentScrolling"] == null)
				{
					return ToolTipScrolling.Default;
				}
				return (ToolTipScrolling)this.ViewState["ContentScrolling"];
			}
			set
			{
				this.ViewState["ContentScrolling"] = value;
			}
		}

		// Token: 0x17002A3D RID: 10813
		// (get) Token: 0x06008566 RID: 34150 RVA: 0x001E6A12 File Offset: 0x001E4C12
		// (set) Token: 0x06008567 RID: 34151 RVA: 0x001E6A3D File Offset: 0x001E4C3D
		[Category("Layout")]
		[Description("Specifies whether the tooltip is relative to the mouse, the target element or to the browser viewport.")]
		[ClientControlProperty]
		[DefaultValue(ToolTipRelativeDisplay.Mouse)]
		public ToolTipRelativeDisplay RelativeTo
		{
			get
			{
				if (this.ViewState["RelativeTo"] == null)
				{
					return ToolTipRelativeDisplay.Mouse;
				}
				return (ToolTipRelativeDisplay)this.ViewState["RelativeTo"];
			}
			set
			{
				this.ViewState["RelativeTo"] = value;
			}
		}

		// Token: 0x17002A3E RID: 10814
		// (get) Token: 0x06008568 RID: 34152 RVA: 0x001E6A55 File Offset: 0x001E4C55
		// (set) Token: 0x06008569 RID: 34153 RVA: 0x001E6A80 File Offset: 0x001E4C80
		[Category("Behavior")]
		[Description("Specifies the tooltip's horizontal offset from the target control in pixels.")]
		[DefaultValue(0)]
		[ClientControlProperty]
		public int OffsetX
		{
			get
			{
				if (this.ViewState["OffsetX"] == null)
				{
					return 0;
				}
				return (int)this.ViewState["OffsetX"];
			}
			set
			{
				this.ViewState["OffsetX"] = value;
			}
		}

		// Token: 0x17002A3F RID: 10815
		// (get) Token: 0x0600856A RID: 34154 RVA: 0x001E6A98 File Offset: 0x001E4C98
		// (set) Token: 0x0600856B RID: 34155 RVA: 0x001E6AC3 File Offset: 0x001E4CC3
		[DefaultValue(6)]
		[Description("Specifies the tooltip's vertical offset from the target control in pixels.")]
		[ClientControlProperty]
		[Category("Behavior")]
		public int OffsetY
		{
			get
			{
				if (this.ViewState["OffsetY"] == null)
				{
					return 6;
				}
				return (int)this.ViewState["OffsetY"];
			}
			set
			{
				this.ViewState["OffsetY"] = value;
			}
		}

		// Token: 0x17002A40 RID: 10816
		// (get) Token: 0x0600856C RID: 34156 RVA: 0x001E6ADB File Offset: 0x001E4CDB
		// (set) Token: 0x0600856D RID: 34157 RVA: 0x001E6B0A File Offset: 0x001E4D0A
		[DefaultValue(3000)]
		[Category("Behavior")]
		[Description("Specifies the delay (in milliseconds) after which the tooltip will hide if the mouse stands still over the target element.")]
		[ClientControlProperty]
		public int AutoCloseDelay
		{
			get
			{
				if (this.ViewState["AutoCloseDelay"] == null)
				{
					return 3000;
				}
				return (int)this.ViewState["AutoCloseDelay"];
			}
			set
			{
				this.ViewState["AutoCloseDelay"] = value;
			}
		}

		// Token: 0x17002A41 RID: 10817
		// (get) Token: 0x0600856E RID: 34158 RVA: 0x001E6B22 File Offset: 0x001E4D22
		// (set) Token: 0x0600856F RID: 34159 RVA: 0x001E6B51 File Offset: 0x001E4D51
		[Category("Behavior")]
		[DefaultValue(300)]
		[Description("Specifies the delay (in milliseconds) for the tooltip to hide after the mouse leaves the target element.")]
		[ClientControlProperty]
		public int HideDelay
		{
			get
			{
				if (this.ViewState["HideDelay"] == null)
				{
					return 300;
				}
				return (int)this.ViewState["HideDelay"];
			}
			set
			{
				this.ViewState["HideDelay"] = value;
			}
		}

		// Token: 0x17002A42 RID: 10818
		// (get) Token: 0x06008570 RID: 34160 RVA: 0x001E6B69 File Offset: 0x001E4D69
		// (set) Token: 0x06008571 RID: 34161 RVA: 0x001E6B98 File Offset: 0x001E4D98
		[DefaultValue(400)]
		[ClientControlProperty]
		[Category("Behavior")]
		[Description("Specifies the time (in milliseconds) for which the user should hold the mouse over a target element for the tooltip to appear.")]
		public int ShowDelay
		{
			get
			{
				if (this.ViewState["ShowDelay"] == null)
				{
					return 400;
				}
				return (int)this.ViewState["ShowDelay"];
			}
			set
			{
				this.ViewState["ShowDelay"] = value;
			}
		}

		// Token: 0x17002A43 RID: 10819
		// (get) Token: 0x06008572 RID: 34162 RVA: 0x001E6BB0 File Offset: 0x001E4DB0
		// (set) Token: 0x06008573 RID: 34163 RVA: 0x001E6BDB File Offset: 0x001E4DDB
		[ClientControlProperty]
		[Description("Specifies whether the tooltip will move to follow mouse movement over the target control or will stay fixed.")]
		[Category("Appearance")]
		[DefaultValue(false)]
		public bool MouseTrailing
		{
			get
			{
				return this.ViewState["MouseTrailing"] != null && (bool)this.ViewState["MouseTrailing"];
			}
			set
			{
				this.ViewState["MouseTrailing"] = value;
			}
		}

		// Token: 0x17002A44 RID: 10820
		// (get) Token: 0x06008574 RID: 34164 RVA: 0x001E6BF3 File Offset: 0x001E4DF3
		// (set) Token: 0x06008575 RID: 34165 RVA: 0x001E6C1E File Offset: 0x001E4E1E
		[ClientControlProperty]
		[DefaultValue(true)]
		[Category("Appearance")]
		[Description("Specifies whether the tooltip will show a small arrow pointing to its target element.")]
		public bool ShowCallout
		{
			get
			{
				return this.ViewState["ShowCallout"] == null || (bool)this.ViewState["ShowCallout"];
			}
			set
			{
				this.ViewState["ShowCallout"] = value;
			}
		}

		// Token: 0x17002A45 RID: 10821
		// (get) Token: 0x06008576 RID: 34166 RVA: 0x001E6C36 File Offset: 0x001E4E36
		// (set) Token: 0x06008577 RID: 34167 RVA: 0x001E6C61 File Offset: 0x001E4E61
		[Description("Specifies whether the tooltip should be added as a child of the form element or as a child of its direct parent.")]
		[Category("Appearance")]
		[ClientControlProperty]
		[DefaultValue(true)]
		public bool RenderInPageRoot
		{
			get
			{
				return this.ViewState["RenderInPageRoot"] == null || (bool)this.ViewState["RenderInPageRoot"];
			}
			set
			{
				this.ViewState["RenderInPageRoot"] = value;
			}
		}

		// Token: 0x17002A46 RID: 10822
		// (get) Token: 0x06008578 RID: 34168 RVA: 0x001E6C79 File Offset: 0x001E4E79
		// (set) Token: 0x06008579 RID: 34169 RVA: 0x001E6CA4 File Offset: 0x001E4EA4
		[Description("Specifies if the RadToolTip should have a shadow.")]
		[ClientControlProperty]
		[ClientPropertyName("enableShadow")]
		[DefaultValue(true)]
		[Category("Appearance")]
		public bool EnableShadow
		{
			get
			{
				return this.ViewState["EnableShadow"] == null || (bool)this.ViewState["EnableShadow"];
			}
			set
			{
				this.ViewState["EnableShadow"] = value;
			}
		}

		// Token: 0x17002A47 RID: 10823
		// (get) Token: 0x0600857A RID: 34170 RVA: 0x001E6CBC File Offset: 0x001E4EBC
		// (set) Token: 0x0600857B RID: 34171 RVA: 0x001E6CE7 File Offset: 0x001E4EE7
		[Description("Specifies if the RadToolTip should have rounded corners.")]
		[DefaultValue(true)]
		[Category("Appearance")]
		[ClientControlProperty]
		[ClientPropertyName("enableRoundedCorners")]
		public bool EnableRoundedCorners
		{
			get
			{
				return this.ViewState["EnableRoundedCorners"] == null || (bool)this.ViewState["EnableRoundedCorners"];
			}
			set
			{
				this.ViewState["EnableRoundedCorners"] = value;
			}
		}

		// Token: 0x17002A48 RID: 10824
		// (get) Token: 0x0600857C RID: 34172 RVA: 0x001E6CFF File Offset: 0x001E4EFF
		// (set) Token: 0x0600857D RID: 34173 RVA: 0x001E6D2A File Offset: 0x001E4F2A
		[DefaultValue(false)]
		[ClientControlProperty]
		[Browsable(true)]
		[Bindable(true)]
		[Category("Behavior")]
		[Description("Specifies whether a tooltip is modal or not.")]
		public bool Modal
		{
			get
			{
				return this.ViewState["Modal"] != null && (bool)this.ViewState["Modal"];
			}
			set
			{
				this.ViewState["Modal"] = value;
			}
		}

		// Token: 0x17002A49 RID: 10825
		// (get) Token: 0x0600857E RID: 34174 RVA: 0x001E6D42 File Offset: 0x001E4F42
		// (set) Token: 0x0600857F RID: 34175 RVA: 0x001E6D6D File Offset: 0x001E4F6D
		[DefaultValue(false)]
		[ClientControlProperty]
		[Browsable(true)]
		[Bindable(true)]
		[Category("Behavior")]
		[Description("Specifies whether the tooltip will create an overlay element to ensure it will be displayed over a flash element.")]
		public bool Overlay
		{
			get
			{
				return this.ViewState["Overlay"] != null && (bool)this.ViewState["Overlay"];
			}
			set
			{
				this.ViewState["Overlay"] = value;
			}
		}

		// Token: 0x17002A4A RID: 10826
		// (get) Token: 0x06008580 RID: 34176 RVA: 0x001E6D85 File Offset: 0x001E4F85
		// (set) Token: 0x06008581 RID: 34177 RVA: 0x001E6DA6 File Offset: 0x001E4FA6
		[ClientControlProperty]
		[DefaultValue(false)]
		[Description("When set to true enables support for WAI-ARIA")]
		[ClientPropertyName("enableAriaSupport")]
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

		// Token: 0x17002A4B RID: 10827
		// (get) Token: 0x06008582 RID: 34178 RVA: 0x001E6DBE File Offset: 0x001E4FBE
		// (set) Token: 0x06008583 RID: 34179 RVA: 0x001E6DED File Offset: 0x001E4FED
		[DefaultValue("")]
		[Description("Indicates the name of client-side JavaScript function that is called before the RadToolTip shows.")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[ClientPropertyName("beforeShow")]
		public virtual string OnClientBeforeShow
		{
			get
			{
				if (this.ViewState["OnClientBeforeShow"] == null)
				{
					return string.Empty;
				}
				return (string)this.ViewState["OnClientBeforeShow"];
			}
			set
			{
				this.ViewState["OnClientBeforeShow"] = value;
			}
		}

		// Token: 0x17002A4C RID: 10828
		// (get) Token: 0x06008584 RID: 34180 RVA: 0x001E6E00 File Offset: 0x001E5000
		// (set) Token: 0x06008585 RID: 34181 RVA: 0x001E6E2F File Offset: 0x001E502F
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Description("Specifies the name of client-side JavaScript function that is called just after the RadToolTip is shown.")]
		[DefaultValue("")]
		[ClientControlEvent]
		[ClientPropertyName("show")]
		public virtual string OnClientShow
		{
			get
			{
				if (this.ViewState["OnClientShow"] == null)
				{
					return string.Empty;
				}
				return (string)this.ViewState["OnClientShow"];
			}
			set
			{
				this.ViewState["OnClientShow"] = value;
			}
		}

		// Token: 0x17002A4D RID: 10829
		// (get) Token: 0x06008586 RID: 34182 RVA: 0x001E6E42 File Offset: 0x001E5042
		// (set) Token: 0x06008587 RID: 34183 RVA: 0x001E6E71 File Offset: 0x001E5071
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[DefaultValue("")]
		[Description("Specifies the name of client-side JavaScript function that is called just before the RadToolTip hides.")]
		[ClientControlEvent]
		[ClientPropertyName("beforeHide")]
		public virtual string OnClientBeforeHide
		{
			get
			{
				if (this.ViewState["OnClientBeforeHide"] == null)
				{
					return string.Empty;
				}
				return (string)this.ViewState["OnClientBeforeHide"];
			}
			set
			{
				this.ViewState["OnClientBeforeHide"] = value;
			}
		}

		// Token: 0x17002A4E RID: 10830
		// (get) Token: 0x06008588 RID: 34184 RVA: 0x001E6E84 File Offset: 0x001E5084
		// (set) Token: 0x06008589 RID: 34185 RVA: 0x001E6EB3 File Offset: 0x001E50B3
		[DefaultValue("")]
		[Description("Specifies the name of client-side JavaScript function that is called just after the RadToolTip is hidden.")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[ClientPropertyName("hide")]
		public virtual string OnClientHide
		{
			get
			{
				if (this.ViewState["OnClientHide"] == null)
				{
					return string.Empty;
				}
				return (string)this.ViewState["OnClientHide"];
			}
			set
			{
				this.ViewState["OnClientHide"] = value;
			}
		}

		// Token: 0x0600858A RID: 34186 RVA: 0x001E6EC8 File Offset: 0x001E50C8
		protected override Style CreateControlStyle()
		{
			Style result = base.CreateControlStyle();
			if (!base.DesignMode)
			{
				base.Style.Add("display", "none");
				base.Style.Add("position", "absolute");
			}
			return result;
		}

		// Token: 0x17002A4F RID: 10831
		// (get) Token: 0x0600858B RID: 34187 RVA: 0x001E6F0F File Offset: 0x001E510F
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Div;
			}
		}

		// Token: 0x17002A50 RID: 10832
		// (get) Token: 0x0600858C RID: 34188 RVA: 0x001E6F13 File Offset: 0x001E5113
		protected override string CssClassFormatString
		{
			get
			{
				return "";
			}
		}

		// Token: 0x0600858D RID: 34189 RVA: 0x001E6F1C File Offset: 0x001E511C
		protected override IEnumerable<ScriptReference> GetScriptReferences()
		{
			IEnumerable<ScriptReference> scriptReferences = base.GetScriptReferences();
			List<ScriptReference> list = new List<ScriptReference>(scriptReferences);
			if (this.Animation != ToolTipAnimation.None && this.EnableEmbeddedScripts)
			{
				list.Add(new ScriptReference("Telerik.Web.UI.Common.Animation.AnimationScripts.js", Assembly.GetExecutingAssembly().FullName));
			}
			return list;
		}

		// Token: 0x0600858E RID: 34190 RVA: 0x001E6F64 File Offset: 0x001E5164
		protected override void DescribeComponent(IScriptDescriptor descriptor)
		{
			base.DescribeComponent(descriptor);
			base.DescribeRenderMode(descriptor);
			descriptor.AddProperty("skin", base.RuntimeSkin);
			descriptor.AddProperty("_manualCloseButtonText", this.ManualCloseButtonText);
			descriptor.AddProperty("_cssClass", this.CssClass);
			if (this.Page.Form != null)
			{
				descriptor.AddProperty("formID", this.Page.Form.ClientID);
			}
			if (this._showMethodInvoked && !this.VisibleOnPageLoad)
			{
				descriptor.AddProperty("visibleOnPageLoad", "true");
			}
		}

		// Token: 0x0600858F RID: 34191 RVA: 0x001E6FFA File Offset: 0x001E51FA
		protected void ThrowControlNotFound(string ctrlID)
		{
			throw new ArgumentNullException(string.Format("Cannot find a server control with ID={0}. If you need to specify a client-side element ID, please set IsClientID to true.", ctrlID));
		}

		// Token: 0x17002A51 RID: 10833
		// (get) Token: 0x06008590 RID: 34192 RVA: 0x001E700C File Offset: 0x001E520C
		protected internal override bool SupportsRenderingMode
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06008591 RID: 34193 RVA: 0x001E7010 File Offset: 0x001E5210
		protected internal override void DescribeClientProperties(IScriptDescriptor descriptor)
		{
			base.DescribeProperty<ToolTipAnimation>(descriptor, "animation", this.Animation, ToolTipAnimation.None);
			base.DescribeProperty<int>(descriptor, "animationDuration", this.AnimationDuration, 500);
			base.DescribeProperty<int>(descriptor, "autoCloseDelay", this.AutoCloseDelay, 3000);
			base.DescribeProperty<ToolTipScrolling>(descriptor, "contentScrolling", this.ContentScrolling, ToolTipScrolling.Default);
			base.DescribeProperty<bool>(descriptor, "enableAriaSupport", this.EnableAriaSupport, false);
			base.DescribeProperty<bool>(descriptor, "enableRoundedCorners", this.EnableRoundedCorners, true);
			base.DescribeProperty<bool>(descriptor, "enableShadow", this.EnableShadow, true);
			base.DescribeProperty<string>(descriptor, "height", this.Height.ToString(CultureInfo.InvariantCulture), "");
			base.DescribeProperty<int>(descriptor, "hideDelay", this.HideDelay, 300);
			base.DescribeProperty<ToolTipHideEvent>(descriptor, "hideEvent", this.HideEvent, ToolTipHideEvent.Default);
			base.DescribeProperty<bool>(descriptor, "ignoreAltAttribute", this.IgnoreAltAttribute, false);
			base.DescribeProperty<bool>(descriptor, "modal", this.Modal, false);
			base.DescribeProperty<bool>(descriptor, "mouseTrailing", this.MouseTrailing, false);
			base.DescribeProperty<int>(descriptor, "offsetX", this.OffsetX, 0);
			base.DescribeProperty<int>(descriptor, "offsetY", this.OffsetY, 6);
			base.DescribeProperty<bool>(descriptor, "overlay", this.Overlay, false);
			base.DescribeProperty<ToolTipPosition>(descriptor, "position", this.Position, ToolTipPosition.BottomCenter);
			base.DescribeProperty<ToolTipRelativeDisplay>(descriptor, "relativeTo", this.RelativeTo, ToolTipRelativeDisplay.Mouse);
			base.DescribeProperty<bool>(descriptor, "renderInPageRoot", this.RenderInPageRoot, true);
			base.DescribeProperty<bool>(descriptor, "showCallout", this.ShowCallout, true);
			base.DescribeProperty<int>(descriptor, "showDelay", this.ShowDelay, 400);
			base.DescribeProperty<ToolTipShowEvent>(descriptor, "showEvent", this.ShowEvent, ToolTipShowEvent.OnMouseOver);
			base.DescribeProperty<string>(descriptor, "text", this.Text, "");
			base.DescribeProperty<string>(descriptor, "title", this.Title, "");
			base.DescribeProperty<bool>(descriptor, "visibleOnPageLoad", this.VisibleOnPageLoad, false);
			base.DescribeProperty<string>(descriptor, "width", this.Width.ToString(CultureInfo.InvariantCulture), "");
			base.DescribeClientProperties(descriptor);
		}

		// Token: 0x06008592 RID: 34194 RVA: 0x001E7250 File Offset: 0x001E5450
		protected internal override void DescribeClientEvents(IScriptDescriptor descriptor)
		{
			RadWebControl.DescribeEvent(descriptor, "beforeHide", this.OnClientBeforeHide);
			RadWebControl.DescribeEvent(descriptor, "beforeShow", this.OnClientBeforeShow);
			RadWebControl.DescribeEvent(descriptor, "hide", this.OnClientHide);
			RadWebControl.DescribeEvent(descriptor, "show", this.OnClientShow);
			base.DescribeClientEvents(descriptor);
		}

		// Token: 0x0400253F RID: 9535
		private bool _showMethodInvoked;
	}
}
