using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Licensing;
using Telerik.Web.UI.Calendar.Utils;

namespace Telerik.Web.UI
{
	// Token: 0x0200100F RID: 4111
	[EmbeddedSkin("Calendar", "WebBlue", typeof(RadTimeView))]
	[EmbeddedSkin("Calendar", "Telerik", typeof(RadTimeView))]
	[EmbeddedSkin("Calendar", "Vista", typeof(RadTimeView))]
	[EmbeddedSkin("Calendar", "Web20", typeof(RadTimeView))]
	[EmbeddedSkin("Calendar", typeof(RadTimeView))]
	[EmbeddedSkin("Calendar", "Windows7", typeof(RadTimeView))]
	[ToolboxBitmap(typeof(RadTimeView), "Telerik.Web.UI.Calendar.png")]
	[ToolboxData("<{0}:RadTimeView Runat=\"server\"></{0}:RadTimeView>")]
	[TelerikToolboxCategory("Date/Color Picker")]
	[Designer("Telerik.Web.Design.RadTimeViewDesigner, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4")]
	[Description("Note that RadTimeView is not a stand-alone control. Use this class only when you need the SharedTimeView functionality for RadDateTimePicker / RadTimePicker.")]
	[ClientScriptResource("Telerik.Web.UI.RadTimeView", "Telerik.Web.UI.Calendar.RadTimeViewScripts.js")]
	[LightweightRendering]
	[LicenseProvider(typeof(TelerikLicenseProvider))]
	[EmbeddedSkin("Calendar", "Black", typeof(RadTimeView))]
	[EmbeddedSkin("Calendar", "Default", typeof(RadTimeView))]
	[EmbeddedSkin("Calendar", "Office2007", typeof(RadTimeView))]
	[EmbeddedSkin("Calendar", "Outlook", typeof(RadTimeView))]
	[EmbeddedSkin("Calendar", "Simple", typeof(RadTimeView))]
	[EmbeddedSkin("Calendar", "Sunset", typeof(RadTimeView))]
	public class RadTimeView : RadWebControl, INamingContainer, ICustomTypeDescriptor
	{
		// Token: 0x170032FB RID: 13051
		// (get) Token: 0x0600A132 RID: 41266 RVA: 0x0023DB44 File Offset: 0x0023BD44
		[NotifyParentProperty(true)]
		[Browsable(false)]
		[DefaultValue("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public DataList DataList
		{
			get
			{
				if (this.dataList == null)
				{
					this.dataList = new TimeDataList();
				}
				this.EnsureChildControls();
				return this.dataList;
			}
		}

		// Token: 0x170032FC RID: 13052
		// (get) Token: 0x0600A133 RID: 41267 RVA: 0x0023DB65 File Offset: 0x0023BD65
		// (set) Token: 0x0600A134 RID: 41268 RVA: 0x0023DB72 File Offset: 0x0023BD72
		[DefaultValue(RepeatDirection.Horizontal)]
		[NotifyParentProperty(true)]
		[Browsable(true)]
		public RepeatDirection RenderDirection
		{
			get
			{
				return this.DataList.RepeatDirection;
			}
			set
			{
				this.DataList.RepeatDirection = value;
			}
		}

		// Token: 0x170032FD RID: 13053
		// (get) Token: 0x0600A135 RID: 41269 RVA: 0x0023DB80 File Offset: 0x0023BD80
		// (set) Token: 0x0600A136 RID: 41270 RVA: 0x0023DBA9 File Offset: 0x0023BDA9
		[Description("Enable client side navigation with keyboard")]
		[DefaultValue(false)]
		public bool EnableKeyboardNavigation
		{
			get
			{
				object obj = this.ViewState["EnableKeyboardNavigation"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.ViewState["EnableKeyboardNavigation"] = value;
			}
		}

		// Token: 0x170032FE RID: 13054
		// (get) Token: 0x0600A137 RID: 41271 RVA: 0x0023DBC1 File Offset: 0x0023BDC1
		// (set) Token: 0x0600A138 RID: 41272 RVA: 0x0023DBD3 File Offset: 0x0023BDD3
		[Browsable(false)]
		public object CustomTimeValues
		{
			get
			{
				return this.ViewState["ctv"];
			}
			set
			{
				if (value is string[] || value is DateTime[] || value is TimeSpan[])
				{
					this.ViewState["ctv"] = value;
					return;
				}
				throw new NotSupportedException("Only string arrays, DateTime arrays and TimeSpan arrays are supported");
			}
		}

		// Token: 0x170032FF RID: 13055
		// (get) Token: 0x0600A139 RID: 41273 RVA: 0x0023DC09 File Offset: 0x0023BE09
		// (set) Token: 0x0600A13A RID: 41274 RVA: 0x0023DC34 File Offset: 0x0023BE34
		[DefaultValue(false)]
		[NotifyParentProperty(true)]
		[Description("Gets or sets a value indicating whether the TimeView should use client time zone offset for the values bound to a custom collection")]
		[Category("Behavior")]
		protected virtual bool UseClientTimeOffset
		{
			get
			{
				return this.ViewState["UseClientTimeOffset"] != null && (bool)this.ViewState["UseClientTimeOffset"];
			}
			set
			{
				this.ViewState["UseClientTimeOffset"] = value;
			}
		}

		// Token: 0x17003300 RID: 13056
		// (get) Token: 0x0600A13B RID: 41275 RVA: 0x0023DC4C File Offset: 0x0023BE4C
		// (set) Token: 0x0600A13C RID: 41276 RVA: 0x0023DC6D File Offset: 0x0023BE6D
		[DefaultValue(false)]
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

		// Token: 0x17003301 RID: 13057
		// (get) Token: 0x0600A13D RID: 41277 RVA: 0x0023DC85 File Offset: 0x0023BE85
		// (set) Token: 0x0600A13E RID: 41278 RVA: 0x0023DC92 File Offset: 0x0023BE92
		[Browsable(false)]
		[DefaultValue(typeof(ITemplate), "")]
		[Description("Control template")]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public ITemplate AlternatingTimeTemplate
		{
			get
			{
				return this.DataList.AlternatingItemTemplate;
			}
			set
			{
				this.DataList.AlternatingItemTemplate = value;
			}
		}

		// Token: 0x17003302 RID: 13058
		// (get) Token: 0x0600A13F RID: 41279 RVA: 0x0023DCA0 File Offset: 0x0023BEA0
		// (set) Token: 0x0600A140 RID: 41280 RVA: 0x0023DCAD File Offset: 0x0023BEAD
		[Browsable(false)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Description("Control template")]
		[NotifyParentProperty(true)]
		[DefaultValue(typeof(ITemplate), "")]
		public ITemplate FooterTemplate
		{
			get
			{
				return this.DataList.FooterTemplate;
			}
			set
			{
				this.DataList.FooterTemplate = value;
			}
		}

		// Token: 0x17003303 RID: 13059
		// (get) Token: 0x0600A141 RID: 41281 RVA: 0x0023DCBB File Offset: 0x0023BEBB
		// (set) Token: 0x0600A142 RID: 41282 RVA: 0x0023DCC8 File Offset: 0x0023BEC8
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DefaultValue(typeof(ITemplate), "")]
		[Description("Control template")]
		[Browsable(false)]
		[NotifyParentProperty(true)]
		public ITemplate HeaderTemplate
		{
			get
			{
				return this.DataList.HeaderTemplate;
			}
			set
			{
				this.DataList.HeaderTemplate = value;
			}
		}

		// Token: 0x17003304 RID: 13060
		// (get) Token: 0x0600A143 RID: 41283 RVA: 0x0023DCD6 File Offset: 0x0023BED6
		// (set) Token: 0x0600A144 RID: 41284 RVA: 0x0023DCE3 File Offset: 0x0023BEE3
		[Description("Control template")]
		[Browsable(false)]
		[DefaultValue(typeof(ITemplate), "")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[NotifyParentProperty(true)]
		public ITemplate TimeTemplate
		{
			get
			{
				return this.DataList.ItemTemplate;
			}
			set
			{
				this.DataList.ItemTemplate = value;
			}
		}

		// Token: 0x17003305 RID: 13061
		// (get) Token: 0x0600A145 RID: 41285 RVA: 0x0023DCF1 File Offset: 0x0023BEF1
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return this.tagKey;
			}
		}

		// Token: 0x17003306 RID: 13062
		// (get) Token: 0x0600A146 RID: 41286 RVA: 0x0023DCF9 File Offset: 0x0023BEF9
		// (set) Token: 0x0600A147 RID: 41287 RVA: 0x0023DD06 File Offset: 0x0023BF06
		[NotifyParentProperty(true)]
		public override Color ForeColor
		{
			get
			{
				return this.DataList.ForeColor;
			}
			set
			{
				this.DataList.ForeColor = value;
			}
		}

		// Token: 0x17003307 RID: 13063
		// (get) Token: 0x0600A148 RID: 41288 RVA: 0x0023DD14 File Offset: 0x0023BF14
		// (set) Token: 0x0600A149 RID: 41289 RVA: 0x0023DD21 File Offset: 0x0023BF21
		[NotifyParentProperty(true)]
		public override Color BackColor
		{
			get
			{
				return this.DataList.BackColor;
			}
			set
			{
				this.DataList.BackColor = value;
			}
		}

		// Token: 0x17003308 RID: 13064
		// (get) Token: 0x0600A14A RID: 41290 RVA: 0x0023DD2F File Offset: 0x0023BF2F
		// (set) Token: 0x0600A14B RID: 41291 RVA: 0x0023DD3C File Offset: 0x0023BF3C
		[NotifyParentProperty(true)]
		public override Color BorderColor
		{
			get
			{
				return this.DataList.BorderColor;
			}
			set
			{
				this.DataList.BorderColor = value;
			}
		}

		// Token: 0x17003309 RID: 13065
		// (get) Token: 0x0600A14C RID: 41292 RVA: 0x0023DD4A File Offset: 0x0023BF4A
		// (set) Token: 0x0600A14D RID: 41293 RVA: 0x0023DD57 File Offset: 0x0023BF57
		[NotifyParentProperty(true)]
		public override BorderStyle BorderStyle
		{
			get
			{
				return this.DataList.BorderStyle;
			}
			set
			{
				this.DataList.BorderStyle = value;
			}
		}

		// Token: 0x1700330A RID: 13066
		// (get) Token: 0x0600A14E RID: 41294 RVA: 0x0023DD65 File Offset: 0x0023BF65
		// (set) Token: 0x0600A14F RID: 41295 RVA: 0x0023DD72 File Offset: 0x0023BF72
		[NotifyParentProperty(true)]
		public override Unit BorderWidth
		{
			get
			{
				return this.DataList.BorderWidth;
			}
			set
			{
				this.DataList.BorderWidth = value;
			}
		}

		// Token: 0x1700330B RID: 13067
		// (get) Token: 0x0600A150 RID: 41296 RVA: 0x0023DD80 File Offset: 0x0023BF80
		// (set) Token: 0x0600A151 RID: 41297 RVA: 0x0023DD8D File Offset: 0x0023BF8D
		[NotifyParentProperty(true)]
		public override string CssClass
		{
			get
			{
				return this.DataList.CssClass;
			}
			set
			{
				this.DataList.CssClass = value;
			}
		}

		// Token: 0x1700330C RID: 13068
		// (get) Token: 0x0600A152 RID: 41298 RVA: 0x0023DD9B File Offset: 0x0023BF9B
		// (set) Token: 0x0600A153 RID: 41299 RVA: 0x0023DDA8 File Offset: 0x0023BFA8
		[NotifyParentProperty(true)]
		public override Unit Height
		{
			get
			{
				return this.DataList.Height;
			}
			set
			{
				this.DataList.Height = value;
			}
		}

		// Token: 0x1700330D RID: 13069
		// (get) Token: 0x0600A154 RID: 41300 RVA: 0x0023DDB6 File Offset: 0x0023BFB6
		// (set) Token: 0x0600A155 RID: 41301 RVA: 0x0023DDC3 File Offset: 0x0023BFC3
		[NotifyParentProperty(true)]
		public override Unit Width
		{
			get
			{
				return this.DataList.Width;
			}
			set
			{
				this.DataList.Width = value;
			}
		}

		// Token: 0x1700330E RID: 13070
		// (get) Token: 0x0600A156 RID: 41302 RVA: 0x0023DDD1 File Offset: 0x0023BFD1
		[NotifyParentProperty(true)]
		public override FontInfo Font
		{
			get
			{
				return this.DataList.Font;
			}
		}

		// Token: 0x1700330F RID: 13071
		// (get) Token: 0x0600A157 RID: 41303 RVA: 0x0023DDDE File Offset: 0x0023BFDE
		// (set) Token: 0x0600A158 RID: 41304 RVA: 0x0023DDE6 File Offset: 0x0023BFE6
		[NotifyParentProperty(true)]
		public override string AccessKey
		{
			get
			{
				return base.AccessKey;
			}
			set
			{
				base.AccessKey = value;
			}
		}

		// Token: 0x17003310 RID: 13072
		// (get) Token: 0x0600A159 RID: 41305 RVA: 0x0023DDEF File Offset: 0x0023BFEF
		// (set) Token: 0x0600A15A RID: 41306 RVA: 0x0023DDF7 File Offset: 0x0023BFF7
		[NotifyParentProperty(true)]
		public override bool Enabled
		{
			get
			{
				return base.Enabled;
			}
			set
			{
				base.Enabled = value;
			}
		}

		// Token: 0x17003311 RID: 13073
		// (get) Token: 0x0600A15B RID: 41307 RVA: 0x0023DE00 File Offset: 0x0023C000
		// (set) Token: 0x0600A15C RID: 41308 RVA: 0x0023DE08 File Offset: 0x0023C008
		[NotifyParentProperty(true)]
		public override bool EnableTheming
		{
			get
			{
				return base.EnableTheming;
			}
			set
			{
				base.EnableTheming = value;
			}
		}

		// Token: 0x17003312 RID: 13074
		// (get) Token: 0x0600A15D RID: 41309 RVA: 0x0023DE11 File Offset: 0x0023C011
		// (set) Token: 0x0600A15E RID: 41310 RVA: 0x0023DE19 File Offset: 0x0023C019
		[NotifyParentProperty(true)]
		public override string SkinID
		{
			get
			{
				return base.SkinID;
			}
			set
			{
				base.SkinID = value;
			}
		}

		// Token: 0x17003313 RID: 13075
		// (get) Token: 0x0600A15F RID: 41311 RVA: 0x0023DE22 File Offset: 0x0023C022
		// (set) Token: 0x0600A160 RID: 41312 RVA: 0x0023DE2A File Offset: 0x0023C02A
		[NotifyParentProperty(true)]
		public override bool EnableViewState
		{
			get
			{
				return base.EnableViewState;
			}
			set
			{
				base.EnableViewState = value;
			}
		}

		// Token: 0x17003314 RID: 13076
		// (get) Token: 0x0600A161 RID: 41313 RVA: 0x0023DE33 File Offset: 0x0023C033
		// (set) Token: 0x0600A162 RID: 41314 RVA: 0x0023DE3B File Offset: 0x0023C03B
		[NotifyParentProperty(true)]
		public override short TabIndex
		{
			get
			{
				return base.TabIndex;
			}
			set
			{
				base.TabIndex = value;
			}
		}

		// Token: 0x17003315 RID: 13077
		// (get) Token: 0x0600A163 RID: 41315 RVA: 0x0023DE44 File Offset: 0x0023C044
		// (set) Token: 0x0600A164 RID: 41316 RVA: 0x0023DE4C File Offset: 0x0023C04C
		[NotifyParentProperty(true)]
		public override string ToolTip
		{
			get
			{
				return base.ToolTip;
			}
			set
			{
				base.ToolTip = value;
			}
		}

		// Token: 0x17003316 RID: 13078
		// (get) Token: 0x0600A165 RID: 41317 RVA: 0x0023DE55 File Offset: 0x0023C055
		// (set) Token: 0x0600A166 RID: 41318 RVA: 0x0023DE5D File Offset: 0x0023C05D
		[NotifyParentProperty(true)]
		public override bool Visible
		{
			get
			{
				return base.Visible;
			}
			set
			{
				base.Visible = value;
			}
		}

		// Token: 0x17003317 RID: 13079
		// (get) Token: 0x0600A167 RID: 41319 RVA: 0x0023DE66 File Offset: 0x0023C066
		// (set) Token: 0x0600A168 RID: 41320 RVA: 0x0023DE95 File Offset: 0x0023C095
		internal string ImagesPath
		{
			get
			{
				if (this.ViewState["ImagesPath"] == null)
				{
					return "";
				}
				return (string)this.ViewState["ImagesPath"];
			}
			set
			{
				this.ViewState["ImagesPath"] = value;
			}
		}

		// Token: 0x17003318 RID: 13080
		// (get) Token: 0x0600A169 RID: 41321 RVA: 0x0023DEA8 File Offset: 0x0023C0A8
		// (set) Token: 0x0600A16A RID: 41322 RVA: 0x0023DEB5 File Offset: 0x0023C0B5
		[NotifyParentProperty(true)]
		[Bindable(true)]
		[Category("Appearance")]
		[DefaultValue(typeof(GridLines), "None")]
		[Description("Settings for grid lines between cells.")]
		public virtual GridLines GridLines
		{
			get
			{
				return this.DataList.GridLines;
			}
			set
			{
				this.DataList.GridLines = value;
			}
		}

		// Token: 0x17003319 RID: 13081
		// (get) Token: 0x0600A16B RID: 41323 RVA: 0x0023DEC3 File Offset: 0x0023C0C3
		// (set) Token: 0x0600A16C RID: 41324 RVA: 0x0023DEF2 File Offset: 0x0023C0F2
		[Category("Accessibility")]
		[Bindable(true)]
		[Localizable(true)]
		[DefaultValue("Time Picker")]
		[Description("The hetader associated with the control.")]
		[NotifyParentProperty(true)]
		public virtual string HeaderText
		{
			get
			{
				if (this.ViewState["HeaderText"] == null)
				{
					return "Time Picker";
				}
				return (string)this.ViewState["HeaderText"];
			}
			set
			{
				this.ViewState["HeaderText"] = value;
				this.defaultTimeHeaderTemplate.HeaderText = value;
			}
		}

		// Token: 0x1700331A RID: 13082
		// (get) Token: 0x0600A16D RID: 41325 RVA: 0x0023DF11 File Offset: 0x0023C111
		// (set) Token: 0x0600A16E RID: 41326 RVA: 0x0023DF1E File Offset: 0x0023C11E
		[NotifyParentProperty(true)]
		[Bindable(true)]
		[Category("Accessibility")]
		[DefaultValue(typeof(TableCaptionAlign), "NotSet")]
		[Description("The alignemt of the associated caption.")]
		public virtual TableCaptionAlign CaptionAlign
		{
			get
			{
				return this.DataList.CaptionAlign;
			}
			set
			{
				this.DataList.CaptionAlign = value;
			}
		}

		// Token: 0x1700331B RID: 13083
		// (get) Token: 0x0600A16F RID: 41327 RVA: 0x0023DF2C File Offset: 0x0023C12C
		// (set) Token: 0x0600A170 RID: 41328 RVA: 0x0023DF39 File Offset: 0x0023C139
		[Category("Accessibility")]
		[Bindable(true)]
		[DefaultValue(typeof(bool), "True")]
		[Description("Indicates that the control should use accessible header cells in its containing table control.")]
		[NotifyParentProperty(true)]
		public virtual bool UseAccessibleHeader
		{
			get
			{
				return this.DataList.UseAccessibleHeader;
			}
			set
			{
				this.DataList.UseAccessibleHeader = value;
			}
		}

		// Token: 0x1700331C RID: 13084
		// (get) Token: 0x0600A171 RID: 41329 RVA: 0x0023DF47 File Offset: 0x0023C147
		// (set) Token: 0x0600A172 RID: 41330 RVA: 0x0023DF76 File Offset: 0x0023C176
		[DefaultValue("Table holding time picker for selecting time of day.")]
		[Category("Accessibility")]
		[NotifyParentProperty(true)]
		[Description("Gets or sets the summary attribute for the RadTimeView.")]
		[Localizable(true)]
		public virtual string Summary
		{
			get
			{
				if (this.ViewState["Summary"] == null)
				{
					return "Table holding time picker for selecting time of day.";
				}
				return (string)this.ViewState["Summary"];
			}
			set
			{
				this.ViewState["Summary"] = value;
			}
		}

		// Token: 0x1700331D RID: 13085
		// (get) Token: 0x0600A173 RID: 41331 RVA: 0x0023DF89 File Offset: 0x0023C189
		// (set) Token: 0x0600A174 RID: 41332 RVA: 0x0023DFB8 File Offset: 0x0023C1B8
		[DefaultValue("Time picker")]
		[Localizable(true)]
		[NotifyParentProperty(true)]
		[Description("Gets or sets the caption for the RadTimeView")]
		public virtual string Caption
		{
			get
			{
				if (this.ViewState["Caption"] == null)
				{
					return "Time picker";
				}
				return (string)this.ViewState["Caption"];
			}
			set
			{
				this.ViewState["Caption"] = value;
			}
		}

		// Token: 0x1700331E RID: 13086
		// (get) Token: 0x0600A175 RID: 41333 RVA: 0x0023DFCB File Offset: 0x0023C1CB
		// (set) Token: 0x0600A176 RID: 41334 RVA: 0x0023DFFA File Offset: 0x0023C1FA
		[NotifyParentProperty(true)]
		[Bindable(true)]
		[Category("Appearance")]
		[DefaultValue(typeof(string), "")]
		[Description("Occurs on the client when an time sell in the RadTimeView control is selected.")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[ClientPropertyName("clientTimeSelected")]
		public virtual string OnClientTimeSelected
		{
			get
			{
				if (this.ViewState["OnClientTimeSelected"] == null)
				{
					return string.Empty;
				}
				return (string)this.ViewState["OnClientTimeSelected"];
			}
			set
			{
				this.ViewState["OnClientTimeSelected"] = value;
			}
		}

		// Token: 0x1700331F RID: 13087
		// (get) Token: 0x0600A177 RID: 41335 RVA: 0x0023E00D File Offset: 0x0023C20D
		// (set) Token: 0x0600A178 RID: 41336 RVA: 0x0023E03C File Offset: 0x0023C23C
		[ClientPropertyName("clientTimeSelecting")]
		[NotifyParentProperty(true)]
		[Bindable(true)]
		[Category("Appearance")]
		[DefaultValue("")]
		[Description("Occurs on the client when a time cell in RadTimeView is about to be selected")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		public virtual string OnClientTimeSelecting
		{
			get
			{
				if (this.ViewState["OnClientTimeSelecting"] == null)
				{
					return string.Empty;
				}
				return (string)this.ViewState["OnClientTimeSelecting"];
			}
			set
			{
				this.ViewState["OnClientTimeSelecting"] = value;
			}
		}

		// Token: 0x17003320 RID: 13088
		// (get) Token: 0x0600A179 RID: 41337 RVA: 0x0023E04F File Offset: 0x0023C24F
		// (set) Token: 0x0600A17A RID: 41338 RVA: 0x0023E05C File Offset: 0x0023C25C
		[Bindable(true)]
		[NotifyParentProperty(true)]
		[Category("Layout")]
		[DefaultValue(typeof(HorizontalAlign), "NotSet")]
		[Description("The horizontal aligment of the control.")]
		public virtual HorizontalAlign HorizontalAlign
		{
			get
			{
				return this.DataList.HorizontalAlign;
			}
			set
			{
				this.DataList.HorizontalAlign = value;
			}
		}

		// Token: 0x17003321 RID: 13089
		// (get) Token: 0x0600A17B RID: 41339 RVA: 0x0023E06A File Offset: 0x0023C26A
		// (set) Token: 0x0600A17C RID: 41340 RVA: 0x0023E077 File Offset: 0x0023C277
		[NotifyParentProperty(true)]
		[Bindable(true)]
		[Category("Layout")]
		[DefaultValue(typeof(int), "-1")]
		[Description("The padding within cells.")]
		public virtual int CellPadding
		{
			get
			{
				return this.DataList.CellPadding;
			}
			set
			{
				this.DataList.CellPadding = value;
			}
		}

		// Token: 0x17003322 RID: 13090
		// (get) Token: 0x0600A17D RID: 41341 RVA: 0x0023E085 File Offset: 0x0023C285
		// (set) Token: 0x0600A17E RID: 41342 RVA: 0x0023E092 File Offset: 0x0023C292
		[Category("Layout")]
		[Bindable(true)]
		[DefaultValue(typeof(int), "0")]
		[Description("The spacing between cells.")]
		[NotifyParentProperty(true)]
		public virtual int CellSpacing
		{
			get
			{
				return this.DataList.CellSpacing;
			}
			set
			{
				this.DataList.CellSpacing = value;
			}
		}

		// Token: 0x17003323 RID: 13091
		// (get) Token: 0x0600A17F RID: 41343 RVA: 0x0023E0A0 File Offset: 0x0023C2A0
		// (set) Token: 0x0600A180 RID: 41344 RVA: 0x0023E0AD File Offset: 0x0023C2AD
		[NotifyParentProperty(true)]
		[Bindable(true)]
		[Category("Layout")]
		[DefaultValue(3)]
		[Description("The number of columns to be used for the layout.")]
		[ClientControlProperty]
		public virtual int Columns
		{
			get
			{
				return this.DataList.RepeatColumns;
			}
			set
			{
				this.DataList.RepeatColumns = value;
			}
		}

		// Token: 0x17003324 RID: 13092
		// (get) Token: 0x0600A181 RID: 41345 RVA: 0x0023E0BB File Offset: 0x0023C2BB
		// (set) Token: 0x0600A182 RID: 41346 RVA: 0x0023E0C8 File Offset: 0x0023C2C8
		[DefaultValue(true)]
		[Bindable(true)]
		[Category("Appearance")]
		[NotifyParentProperty(true)]
		[Description("Whether to the show the control's footer.")]
		[ClientControlProperty]
		public virtual bool ShowFooter
		{
			get
			{
				return this.DataList.ShowFooter;
			}
			set
			{
				this.DataList.ShowFooter = value;
			}
		}

		// Token: 0x17003325 RID: 13093
		// (get) Token: 0x0600A183 RID: 41347 RVA: 0x0023E0D6 File Offset: 0x0023C2D6
		// (set) Token: 0x0600A184 RID: 41348 RVA: 0x0023E0E3 File Offset: 0x0023C2E3
		[NotifyParentProperty(true)]
		[Bindable(true)]
		[Category("Appearance")]
		[DefaultValue(true)]
		[Description("Whether to the show the control's header.")]
		[ClientControlProperty]
		public virtual bool ShowHeader
		{
			get
			{
				return this.DataList.ShowHeader;
			}
			set
			{
				this.DataList.ShowHeader = value;
			}
		}

		// Token: 0x17003326 RID: 13094
		// (get) Token: 0x0600A185 RID: 41349 RVA: 0x0023E0F1 File Offset: 0x0023C2F1
		// (set) Token: 0x0600A186 RID: 41350 RVA: 0x0023E123 File Offset: 0x0023C323
		[Description("The start time of the TimePicker.")]
		[NotifyParentProperty(true)]
		[Bindable(true)]
		[Category("Behavior")]
		[DefaultValue(typeof(TimeSpan), "0:0:0")]
		[ClientControlProperty]
		public virtual TimeSpan StartTime
		{
			get
			{
				if (this.ViewState["StartTime"] == null)
				{
					return new TimeSpan(0, 0, 0);
				}
				return (TimeSpan)this.ViewState["StartTime"];
			}
			set
			{
				this.ViewState["StartTime"] = value;
			}
		}

		// Token: 0x17003327 RID: 13095
		// (get) Token: 0x0600A187 RID: 41351 RVA: 0x0023E13B File Offset: 0x0023C33B
		// (set) Token: 0x0600A188 RID: 41352 RVA: 0x0023E16A File Offset: 0x0023C36A
		[Category("Appearance")]
		[Description("Gets or sets the information about a specific culture that will be applied to the calendar representation.")]
		[NotifyParentProperty(true)]
		[TypeConverter(typeof(CultureInfoConverter))]
		[DefaultValue(typeof(CultureInfo), "en-US")]
		public virtual CultureInfo Culture
		{
			get
			{
				if (this.ViewState["Culture"] == null)
				{
					return CultureInfo.CurrentCulture;
				}
				return (CultureInfo)this.ViewState["Culture"];
			}
			set
			{
				this.ViewState["Culture"] = value;
				this.defaultTimeTemplate.Culture = value;
			}
		}

		// Token: 0x17003328 RID: 13096
		// (get) Token: 0x0600A189 RID: 41353 RVA: 0x0023E189 File Offset: 0x0023C389
		// (set) Token: 0x0600A18A RID: 41354 RVA: 0x0023E1BE File Offset: 0x0023C3BE
		[DefaultValue(typeof(TimeSpan), "23:59:59")]
		[Bindable(true)]
		[Category("Behavior")]
		[NotifyParentProperty(true)]
		[Description("The end time of the TimePicker.")]
		[ClientControlProperty]
		public virtual TimeSpan EndTime
		{
			get
			{
				if (this.ViewState["EndTime"] == null)
				{
					return new TimeSpan(23, 59, 59);
				}
				return (TimeSpan)this.ViewState["EndTime"];
			}
			set
			{
				this.ViewState["EndTime"] = value;
			}
		}

		// Token: 0x17003329 RID: 13097
		// (get) Token: 0x0600A18B RID: 41355 RVA: 0x0023E1D6 File Offset: 0x0023C3D6
		// (set) Token: 0x0600A18C RID: 41356 RVA: 0x0023E208 File Offset: 0x0023C408
		[NotifyParentProperty(true)]
		[Bindable(true)]
		[Category("Behavior")]
		[DefaultValue(typeof(TimeSpan), "1:00:00")]
		[Description("")]
		[ClientControlProperty]
		public virtual TimeSpan Interval
		{
			get
			{
				if (this.ViewState["Interval"] == null)
				{
					return new TimeSpan(1, 0, 0);
				}
				return (TimeSpan)this.ViewState["Interval"];
			}
			set
			{
				TimeSpan timeSpan = value;
				if (timeSpan.Days > 0 && timeSpan.Minutes == 0 && timeSpan.Hours == 0 && timeSpan.Seconds == 0)
				{
					timeSpan = new TimeSpan(0, timeSpan.Days, 0);
				}
				this.ViewState["Interval"] = timeSpan;
			}
		}

		// Token: 0x1700332A RID: 13098
		// (get) Token: 0x0600A18D RID: 41357 RVA: 0x0023E262 File Offset: 0x0023C462
		// (set) Token: 0x0600A18E RID: 41358 RVA: 0x0023E291 File Offset: 0x0023C491
		[Bindable(true)]
		[NotifyParentProperty(true)]
		[Category("Appearance")]
		[DefaultValue("t")]
		[Description("The format of the time.")]
		[Localizable(true)]
		public virtual string TimeFormat
		{
			get
			{
				if (this.ViewState["TimeFormat"] == null)
				{
					return "t";
				}
				return (string)this.ViewState["TimeFormat"];
			}
			set
			{
				this.ViewState["TimeFormat"] = value;
				this.defaultTimeTemplate.Format = value;
			}
		}

		// Token: 0x0600A18F RID: 41359 RVA: 0x0023E2B0 File Offset: 0x0023C4B0
		protected override void DescribeComponent(IScriptDescriptor descriptor)
		{
			base.DescribeComponent(descriptor);
			this.DescribeProperties(descriptor);
		}

		// Token: 0x0600A190 RID: 41360 RVA: 0x0023E2C0 File Offset: 0x0023C4C0
		protected virtual void DescribeProperties(IScriptDescriptor descriptor)
		{
			descriptor.AddProperty("_OwnerDatePickerID", this.Parent.ClientID);
			descriptor.AddProperty("_ItemsCount", this.DataList.Items.Count);
			descriptor.AddProperty("_TimeOverStyleCss", this.TimeOverStyle.CssClass);
			descriptor.AddProperty("_culture", this.Culture.Name);
			descriptor.AddProperty("_timeFormat", this.TimeFormat);
			descriptor.AddProperty("_renderDirection", this.RenderDirection.ToString());
			if (this.UseClientTimeOffset)
			{
				descriptor.AddProperty("_useClientTimeOffset", this.UseClientTimeOffset.ToString().ToLower());
			}
			descriptor.AddScriptProperty("itemStyles", this.GetStyles());
			if (this.shouldSerializeDataSource)
			{
				descriptor.AddScriptProperty("dataSource", new JavaScriptSerializer().Serialize(this.DataList.DataSource));
			}
			if (this.EnableAriaSupport)
			{
				descriptor.AddProperty("_enableAriaSupport", this.EnableAriaSupport);
			}
			descriptor.AddProperty("_enableKeyboardNavigation", this.EnableKeyboardNavigation);
		}

		// Token: 0x1700332B RID: 13099
		// (get) Token: 0x0600A191 RID: 41361 RVA: 0x0023E3EE File Offset: 0x0023C5EE
		[DefaultValue(null)]
		[Description("The style applied to items.")]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public virtual TableItemStyle TimeStyle
		{
			get
			{
				return this.DataList.ItemStyle;
			}
		}

		// Token: 0x1700332C RID: 13100
		// (get) Token: 0x0600A192 RID: 41362 RVA: 0x0023E3FB File Offset: 0x0023C5FB
		[DefaultValue(null)]
		[Description("The style applied to items.")]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public TableItemStyle TimeOverStyle
		{
			get
			{
				if (this.timeOverStyle == null)
				{
					this.timeOverStyle = new TableItemStyle();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this.timeOverStyle).TrackViewState();
					}
				}
				return this.timeOverStyle;
			}
		}

		// Token: 0x1700332D RID: 13101
		// (get) Token: 0x0600A193 RID: 41363 RVA: 0x0023E429 File Offset: 0x0023C629
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Description("The style applied to alternating items.")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DefaultValue(null)]
		public virtual TableItemStyle AlternatingTimeStyle
		{
			get
			{
				return this.DataList.AlternatingItemStyle;
			}
		}

		// Token: 0x1700332E RID: 13102
		// (get) Token: 0x0600A194 RID: 41364 RVA: 0x0023E436 File Offset: 0x0023C636
		[Description("The style applied to the header.")]
		[DefaultValue(null)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public virtual TableItemStyle HeaderStyle
		{
			get
			{
				return this.DataList.HeaderStyle;
			}
		}

		// Token: 0x1700332F RID: 13103
		// (get) Token: 0x0600A195 RID: 41365 RVA: 0x0023E443 File Offset: 0x0023C643
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[DefaultValue(null)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Description("The style applied to the footer.")]
		public virtual TableItemStyle FooterStyle
		{
			get
			{
				return this.DataList.FooterStyle;
			}
		}

		// Token: 0x0600A196 RID: 41366 RVA: 0x0023E450 File Offset: 0x0023C650
		protected override void CreateChildControls()
		{
			this.Controls.Clear();
			this.DataList.ID = "tdl";
			if (!string.IsNullOrEmpty(this.Summary))
			{
				this.DataList.Attributes["summary"] = this.Summary;
			}
			if (!string.IsNullOrEmpty(this.Caption))
			{
				this.DataList.Caption = string.Format("<span style='display: none'>{0}</span>", this.Caption);
			}
			int cellSpacing = this.CellSpacing;
			this.DataList.CellSpacing = -1;
			this.DataList.Attributes["cellspacing"] = cellSpacing.ToString();
			if (!base.DesignMode)
			{
				this.Controls.Add(this.DataList);
				if (this.DataList.ItemTemplate == null)
				{
					this.defaultTimeTemplate.Culture = this.Culture;
					this.defaultTimeTemplate.Format = this.TimeFormat;
					this.DataList.ItemTemplate = this.defaultTimeTemplate;
				}
				if (this.DataList.HeaderTemplate == null)
				{
					this.defaultTimeHeaderTemplate.HeaderText = this.HeaderText;
					this.DataList.HeaderTemplate = this.defaultTimeHeaderTemplate;
				}
			}
		}

		// Token: 0x0600A197 RID: 41367 RVA: 0x0023E584 File Offset: 0x0023C784
		public override void DataBind()
		{
			this.EnsureChildControls();
			if (this.DataList.DataSource == null)
			{
				this.CalculateTimeCollection();
			}
			else if (this.DataList.DataSource is IList && ((IList)this.DataList.DataSource).Count > 0 && ((IList)this.DataList.DataSource)[0] is DateTime)
			{
				this.shouldSerializeDataSource = true;
				if (((DateTime)((IList)this.DataList.DataSource)[0]).Kind == DateTimeKind.Utc)
				{
					this.UseClientTimeOffset = true;
				}
			}
			else if (this.DataList.DataSource is object[] && ((object[])this.DataList.DataSource).Length > 0 && (((object[])this.DataList.DataSource)[0] is DateTime || this.DataList.DataSource is DateTime[]))
			{
				this.shouldSerializeDataSource = true;
				if (((DateTime)((object[])this.DataList.DataSource)[0]).Kind == DateTimeKind.Utc)
				{
					this.UseClientTimeOffset = true;
				}
			}
			else if (!this.isBound || !(this.DataList.DataSource is DataView))
			{
				throw new NotSupportedException(string.Format("Provided DataSource for {0} is not supported", this.ID));
			}
			this.isBound = true;
			base.DataBind();
		}

		// Token: 0x0600A198 RID: 41368 RVA: 0x0023E6F4 File Offset: 0x0023C8F4
		protected virtual void CalculateTimeCollection()
		{
			if (this.CustomTimeValues == null)
			{
				this.PopulateStandard();
				return;
			}
			if (this.CustomTimeValues is string[])
			{
				this.PopulateStringValues();
				return;
			}
			if (this.CustomTimeValues is TimeSpan[])
			{
				this.PopulateTimeSpanValues();
				return;
			}
			if (this.CustomTimeValues is DateTime[])
			{
				this.PopulateDateTimeValues();
			}
		}

		// Token: 0x0600A199 RID: 41369 RVA: 0x0023E74C File Offset: 0x0023C94C
		protected void PopulateStandard()
		{
			DataTable dataTable = new DataTable();
			DataColumn dataColumn = new DataColumn(RadTimeView.TimeColName);
			dataColumn.DataType = typeof(DateTime);
			dataTable.Columns.Add(dataColumn);
			TimeSpan t = this.StartTime;
			TimeSpan endTime = this.EndTime;
			TimeSpan interval = this.Interval;
			while (t < endTime)
			{
				DataRow dataRow = dataTable.NewRow();
				dataRow[RadTimeView.TimeColName] = new DateTime(1990, 1, 1, t.Hours, t.Minutes, t.Seconds, t.Milliseconds);
				dataTable.Rows.Add(dataRow);
				t += interval;
			}
			this.DataList.DataSource = dataTable.DefaultView;
		}

		// Token: 0x0600A19A RID: 41370 RVA: 0x0023E810 File Offset: 0x0023CA10
		protected void PopulateStringValues()
		{
			List<DateTime> list = new List<DateTime>();
			string[] array = this.CustomTimeValues as string[];
			foreach (string timeString in array)
			{
				TimeSpan timeSpanFromString = this.GetTimeSpanFromString(timeString);
				list.Add(new DateTime(1990, 1, 1, timeSpanFromString.Hours, timeSpanFromString.Minutes, timeSpanFromString.Seconds));
			}
			this.SetDateTimeValuesToTimeMatrix(list);
		}

		// Token: 0x0600A19B RID: 41371 RVA: 0x0023E884 File Offset: 0x0023CA84
		protected void PopulateTimeSpanValues()
		{
			List<DateTime> list = new List<DateTime>();
			TimeSpan[] array = this.CustomTimeValues as TimeSpan[];
			foreach (TimeSpan timeSpan in array)
			{
				list.Add(new DateTime(1990, 1, 1, timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds));
			}
			this.SetDateTimeValuesToTimeMatrix(list);
		}

		// Token: 0x0600A19C RID: 41372 RVA: 0x0023E8F3 File Offset: 0x0023CAF3
		protected void PopulateDateTimeValues()
		{
			this.SetDateTimeValuesToTimeMatrix(((DateTime[])this.CustomTimeValues).ToList<DateTime>());
		}

		// Token: 0x0600A19D RID: 41373 RVA: 0x0023E90C File Offset: 0x0023CB0C
		private void SetDateTimeValuesToTimeMatrix(List<DateTime> timeValues)
		{
			this.ValidateTimes(timeValues);
			DataTable dataTable = new DataTable();
			DataColumn dataColumn = new DataColumn(RadTimeView.TimeColName);
			dataColumn.DataType = typeof(DateTime);
			dataTable.Columns.Add(dataColumn);
			foreach (DateTime dateTime in timeValues)
			{
				DataRow dataRow = dataTable.NewRow();
				dataRow[RadTimeView.TimeColName] = dateTime;
				dataTable.Rows.Add(dataRow);
			}
			this.DataList.DataSource = dataTable.DefaultView;
		}

		// Token: 0x0600A19E RID: 41374 RVA: 0x0023E9C0 File Offset: 0x0023CBC0
		protected List<TimeSpan> GetCustomTimes()
		{
			List<TimeSpan> list = new List<TimeSpan>();
			if (this.CustomTimeValues != null)
			{
				if (this.CustomTimeValues is string[])
				{
					string[] array = this.CustomTimeValues as string[];
					foreach (string timeString in array)
					{
						list.Add(this.GetTimeSpanFromString(timeString));
					}
				}
				else if (this.CustomTimeValues is TimeSpan[])
				{
					list = ((TimeSpan[])this.CustomTimeValues).ToList<TimeSpan>();
				}
				else if (this.CustomTimeValues is DateTime[])
				{
					DateTime[] array3 = this.CustomTimeValues as DateTime[];
					foreach (DateTime dateTime in array3)
					{
						list.Add(dateTime.TimeOfDay);
					}
				}
			}
			return list;
		}

		// Token: 0x0600A19F RID: 41375 RVA: 0x0023EA90 File Offset: 0x0023CC90
		protected override void Render(HtmlTextWriter writer)
		{
			string arg = this.ClientID + "_wrapper";
			string arg2 = string.Empty;
			if (!base.DesignMode)
			{
				string arg3 = string.Empty;
				if (!this.Width.IsEmpty)
				{
					arg3 = string.Format("width:{0};overflow-x:auto;", this.Width.ToString());
				}
				string arg4 = string.Empty;
				if (!this.Height.IsEmpty)
				{
					arg4 = string.Format("height:{0};overflow-y:auto;", this.Height.ToString());
				}
				arg2 = string.Format(" style=\"display:none;{0}{1}\" ", arg3, arg4);
			}
			writer.Write(string.Format("<div id=\"{0}\"{1}>", arg, arg2));
			base.Render(writer);
			writer.WriteEndTag("div");
		}

		// Token: 0x0600A1A0 RID: 41376 RVA: 0x0023EB5B File Offset: 0x0023CD5B
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			this.SetStyleClasses();
			if (!base.DesignMode && !this.isBound)
			{
				this.DataBind();
			}
		}

		// Token: 0x0600A1A1 RID: 41377 RVA: 0x0023EB80 File Offset: 0x0023CD80
		protected virtual void SetStyleClasses()
		{
			this.TimeStyle.CssClass = this.FormatCssClass("", this.TimeStyle.CssClass);
			this.AlternatingTimeStyle.CssClass = this.FormatCssClass("", this.AlternatingTimeStyle.CssClass);
			this.HeaderStyle.CssClass = this.FormatCssClass("rcHeader", this.HeaderStyle.CssClass);
			this.FooterStyle.CssClass = this.FormatCssClass("rcFooter", this.FooterStyle.CssClass);
			this.TimeOverStyle.CssClass = this.FormatCssClass("rcHover", this.TimeOverStyle.CssClass);
			this.CssClass = this.FormatCssClass("RadCalendarTimeView", this.CssClass);
		}

		// Token: 0x0600A1A2 RID: 41378 RVA: 0x0023EC4C File Offset: 0x0023CE4C
		private string FormatCssClass(string prefix, string userDefined)
		{
			string text;
			if (prefix == "RadCalendarTimeView")
			{
				text = (this.EmptySkin ? prefix : string.Format("{0} {0}_{1}", prefix, base.RuntimeSkin));
			}
			else
			{
				text = prefix;
			}
			userDefined = Regex.Replace(userDefined, prefix + "_\\S+\\s?", "");
			if (userDefined.IndexOf(text) >= 0)
			{
				return userDefined;
			}
			if (string.IsNullOrEmpty(userDefined))
			{
				return text;
			}
			return string.Format("{0} {1}", text, userDefined);
		}

		// Token: 0x0600A1A3 RID: 41379 RVA: 0x0023ECC8 File Offset: 0x0023CEC8
		private TimeSpan GetTimeSpanFromString(string timeString)
		{
			TimeSpan result = default(TimeSpan);
			string[] timeParts = this.GetTimeParts(timeString);
			int num;
			int num2;
			if (timeParts.Length == 5)
			{
				int days;
				int num3;
				int num4;
				if (int.TryParse(timeParts[0], out days) && int.TryParse(timeParts[1], out num) && int.TryParse(timeParts[2], out num2) && int.TryParse(timeParts[3], out num3) && int.TryParse(timeParts[4], out num4))
				{
					result = new TimeSpan(days, num, num2, num3, num4);
				}
			}
			else if (timeParts.Length == 4)
			{
				int num3;
				int num4;
				if (int.TryParse(timeParts[0], out num) && int.TryParse(timeParts[1], out num2) && int.TryParse(timeParts[2], out num3) && int.TryParse(timeParts[3], out num4))
				{
					result = new TimeSpan(num, num2, num3, num4);
				}
			}
			else if (timeParts.Length == 3)
			{
				int num3;
				if (int.TryParse(timeParts[0], out num) && int.TryParse(timeParts[1], out num2) && int.TryParse(timeParts[2], out num3))
				{
					result = new TimeSpan(num, num2, num3);
				}
			}
			else if (int.TryParse(timeParts[0], out num) && int.TryParse(timeParts[1], out num2))
			{
				result = new TimeSpan(num, num2, 0);
			}
			return result;
		}

		// Token: 0x0600A1A4 RID: 41380 RVA: 0x0023EE00 File Offset: 0x0023D000
		private string[] GetTimeParts(string timeString)
		{
			string[] array = timeString.Split(new char[]
			{
				':'
			});
			if (array.Length == 1)
			{
				array = timeString.Split(new char[]
				{
					'-'
				});
				if (array.Length == 1)
				{
					array = timeString.Split(new char[]
					{
						','
					});
				}
			}
			return array;
		}

		// Token: 0x0600A1A5 RID: 41381 RVA: 0x0023EE58 File Offset: 0x0023D058
		private int GetCustomTimeValuesArrayLength(object customTimes)
		{
			int result = 0;
			if (customTimes is string[])
			{
				result = ((string[])this.CustomTimeValues).Length;
			}
			else if (customTimes is TimeSpan[])
			{
				result = ((TimeSpan[])this.CustomTimeValues).Length;
			}
			else if (customTimes is DateTime[])
			{
				result = ((DateTime[])this.CustomTimeValues).Length;
			}
			return result;
		}

		// Token: 0x0600A1A6 RID: 41382 RVA: 0x0023EEB0 File Offset: 0x0023D0B0
		private string GetStyles()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("{");
			stringBuilder.Append(Utility.GetStyle("TimeStyle", this.TimeStyle) + ",");
			stringBuilder.Append(Utility.GetStyle("AlternatingTimeStyle", this.AlternatingTimeStyle) + ",");
			stringBuilder.Append(Utility.GetStyle("HeaderStyle", this.HeaderStyle) + ",");
			stringBuilder.Append(Utility.GetStyle("FooterStyle", this.FooterStyle) + ",");
			stringBuilder.Append(Utility.GetStyle("TimeOverStyle", this.TimeOverStyle));
			stringBuilder.Append("}");
			return stringBuilder.ToString();
		}

		// Token: 0x0600A1A7 RID: 41383 RVA: 0x0023EF7C File Offset: 0x0023D17C
		private void ValidateTime(TimeSpan time)
		{
			if (time < new TimeSpan(0, 0, 0) || time > this.EndTime)
			{
				throw new ArgumentOutOfRangeException("Time values was out of range. Must be non-negative and less than the EndTime");
			}
		}

		// Token: 0x0600A1A8 RID: 41384 RVA: 0x0023EFA8 File Offset: 0x0023D1A8
		private void ValidateTimes()
		{
			this.ValidateTime(this.StartTime);
			if (this.Interval < new TimeSpan(0, 0, 0) || this.Interval > this.EndTime - this.StartTime)
			{
				throw new ArgumentOutOfRangeException("Interval was out of range. Must be non-negative and less than (EndTime - StartTime)");
			}
		}

		// Token: 0x0600A1A9 RID: 41385 RVA: 0x0023F000 File Offset: 0x0023D200
		private void ValidateTimes(List<DateTime> dates)
		{
			foreach (DateTime dateTime in dates)
			{
				this.ValidateTime(dateTime.TimeOfDay);
			}
		}

		// Token: 0x0600A1AA RID: 41386 RVA: 0x0023F054 File Offset: 0x0023D254
		private void ValidateTimes(List<TimeSpan> times)
		{
			foreach (TimeSpan time in times)
			{
				this.ValidateTime(time);
			}
		}

		// Token: 0x17003330 RID: 13104
		// (get) Token: 0x0600A1AB RID: 41387 RVA: 0x0023F0A4 File Offset: 0x0023D2A4
		protected internal bool EmptySkin
		{
			get
			{
				return string.IsNullOrEmpty(base.RuntimeSkin);
			}
		}

		// Token: 0x0600A1AC RID: 41388 RVA: 0x0023F0B4 File Offset: 0x0023D2B4
		protected override void LoadViewState(object savedState)
		{
			if (!this.EnableViewState)
			{
				return;
			}
			if (savedState == null)
			{
				base.LoadViewState(null);
				return;
			}
			object[] array = (object[])savedState;
			base.LoadViewState(array[0]);
			if (array[1] != null)
			{
				((IStateManager)this.TimeOverStyle).LoadViewState(array[1]);
			}
		}

		// Token: 0x0600A1AD RID: 41389 RVA: 0x0023F0F8 File Offset: 0x0023D2F8
		protected override object SaveViewState()
		{
			return new object[]
			{
				base.SaveViewState(),
				(this.timeOverStyle != null) ? ((IStateManager)this.timeOverStyle).SaveViewState() : null
			};
		}

		// Token: 0x0600A1AE RID: 41390 RVA: 0x0023F12F File Offset: 0x0023D32F
		protected override void TrackViewState()
		{
			base.TrackViewState();
			if (this.timeOverStyle != null)
			{
				((IStateManager)this.timeOverStyle).TrackViewState();
			}
		}

		// Token: 0x0600A1AF RID: 41391 RVA: 0x0023F14A File Offset: 0x0023D34A
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			this.defaultTimeTemplate.Format = this.TimeFormat;
			this.defaultTimeTemplate.Culture = this.Culture;
		}

		// Token: 0x0600A1B0 RID: 41392 RVA: 0x0023F175 File Offset: 0x0023D375
		public System.ComponentModel.AttributeCollection GetAttributes()
		{
			return TypeDescriptor.GetAttributes(this, true);
		}

		// Token: 0x0600A1B1 RID: 41393 RVA: 0x0023F17E File Offset: 0x0023D37E
		public string GetClassName()
		{
			return TypeDescriptor.GetClassName(this, true);
		}

		// Token: 0x0600A1B2 RID: 41394 RVA: 0x0023F187 File Offset: 0x0023D387
		public string GetComponentName()
		{
			return TypeDescriptor.GetComponentName(this, true);
		}

		// Token: 0x0600A1B3 RID: 41395 RVA: 0x0023F190 File Offset: 0x0023D390
		public TypeConverter GetConverter()
		{
			return TypeDescriptor.GetConverter(this, true);
		}

		// Token: 0x0600A1B4 RID: 41396 RVA: 0x0023F199 File Offset: 0x0023D399
		public EventDescriptor GetDefaultEvent()
		{
			return TypeDescriptor.GetDefaultEvent(this, true);
		}

		// Token: 0x0600A1B5 RID: 41397 RVA: 0x0023F1A2 File Offset: 0x0023D3A2
		public PropertyDescriptor GetDefaultProperty()
		{
			return TypeDescriptor.GetDefaultProperty(this, true);
		}

		// Token: 0x0600A1B6 RID: 41398 RVA: 0x0023F1AB File Offset: 0x0023D3AB
		public object GetEditor(Type editorBaseType)
		{
			return TypeDescriptor.GetEditor(this, editorBaseType, true);
		}

		// Token: 0x0600A1B7 RID: 41399 RVA: 0x0023F1B5 File Offset: 0x0023D3B5
		public EventDescriptorCollection GetEvents()
		{
			return new EventDescriptorCollection(new EventDescriptor[0]);
		}

		// Token: 0x0600A1B8 RID: 41400 RVA: 0x0023F1C2 File Offset: 0x0023D3C2
		public EventDescriptorCollection GetEvents(Attribute[] attributes)
		{
			return new EventDescriptorCollection(new EventDescriptor[0]);
		}

		// Token: 0x0600A1B9 RID: 41401 RVA: 0x0023F1D0 File Offset: 0x0023D3D0
		public PropertyDescriptorCollection GetProperties()
		{
			PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(this, true);
			return PropertyFilter.Filter(properties);
		}

		// Token: 0x0600A1BA RID: 41402 RVA: 0x0023F1EC File Offset: 0x0023D3EC
		public PropertyDescriptorCollection GetProperties(Attribute[] attributes)
		{
			PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(this, attributes, true);
			return PropertyFilter.Filter(properties);
		}

		// Token: 0x0600A1BB RID: 41403 RVA: 0x0023F208 File Offset: 0x0023D408
		public object GetPropertyOwner(PropertyDescriptor pd)
		{
			return this;
		}

		// Token: 0x0600A1BC RID: 41404 RVA: 0x0023F20C File Offset: 0x0023D40C
		internal string GetProperWebResourceUrl(string webResourceName)
		{
			string webResourceUrl = this.Page.ClientScript.GetWebResourceUrl(this.GetWebResourceType(), webResourceName);
			return webResourceUrl.Replace("&t", "&amp;t");
		}

		// Token: 0x0600A1BD RID: 41405 RVA: 0x0023F244 File Offset: 0x0023D444
		internal Type GetWebResourceType()
		{
			Type type = base.GetType();
			while (type != typeof(RadTimeView))
			{
				type = type.BaseType;
			}
			return type;
		}

		// Token: 0x17003331 RID: 13105
		// (get) Token: 0x0600A1BE RID: 41406 RVA: 0x0023F274 File Offset: 0x0023D474
		protected internal override bool SupportsRenderingMode
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600A1BF RID: 41407 RVA: 0x0023F278 File Offset: 0x0023D478
		protected internal string GetImage(string fileName)
		{
			if (!VirtualPathUtility.IsAbsolute(fileName) && VirtualPathUtility.IsAppRelative(fileName))
			{
				return VirtualPathUtility.ToAbsolute(fileName);
			}
			if (!string.IsNullOrEmpty(this.ImagesPath.TrimStart(new char[0]).TrimEnd(new char[0])))
			{
				return base.ResolveUrl(Path.Combine(this.ImagesPath.TrimStart(new char[0]).TrimEnd(new char[0]), fileName));
			}
			return string.Empty;
		}

		// Token: 0x0600A1C0 RID: 41408 RVA: 0x0023F2F0 File Offset: 0x0023D4F0
		protected internal override void DescribeClientProperties(IScriptDescriptor descriptor)
		{
			base.DescribeProperty<int>(descriptor, "columns", this.Columns, 3);
			base.DescribeProperty<string>(descriptor, "endTime", RadTimeView.SerializeTimeSpan(this.EndTime), RadTimeView.SerializeTimeSpan(TimeSpan.Parse("23:59:59")));
			base.DescribeProperty<string>(descriptor, "interval", RadTimeView.SerializeTimeSpan(this.Interval), RadTimeView.SerializeTimeSpan(TimeSpan.Parse("1:00:00")));
			base.DescribeProperty<bool>(descriptor, "showFooter", this.ShowFooter, true);
			base.DescribeProperty<bool>(descriptor, "showHeader", this.ShowHeader, true);
			base.DescribeProperty<string>(descriptor, "startTime", RadTimeView.SerializeTimeSpan(this.StartTime), RadTimeView.SerializeTimeSpan(TimeSpan.Parse("0:0:0")));
			if (this.CustomTimeValues != null)
			{
				base.DescribeProperty<List<string>>(descriptor, "customTimes", this.SerializeCustomTimes(), null);
			}
			base.DescribeClientProperties(descriptor);
		}

		// Token: 0x0600A1C1 RID: 41409 RVA: 0x0023F3CA File Offset: 0x0023D5CA
		protected internal override void DescribeClientEvents(IScriptDescriptor descriptor)
		{
			RadWebControl.DescribeEvent(descriptor, "clientTimeSelected", this.OnClientTimeSelected);
			RadWebControl.DescribeEvent(descriptor, "clientTimeSelecting", this.OnClientTimeSelecting);
			base.DescribeClientEvents(descriptor);
		}

		// Token: 0x0600A1C2 RID: 41410 RVA: 0x0023F3F8 File Offset: 0x0023D5F8
		private static string SerializeTimeSpan(TimeSpan value)
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}-{1}-{2}-{3}-{4}", new object[]
			{
				value.Days,
				value.Hours,
				value.Minutes,
				value.Seconds,
				value.Milliseconds
			});
		}

		// Token: 0x0600A1C3 RID: 41411 RVA: 0x0023F484 File Offset: 0x0023D684
		private List<string> SerializeCustomTimes()
		{
			List<string> customTimes = new List<string>();
			this.GetCustomTimes().ForEach(delegate(TimeSpan t)
			{
				customTimes.Add(RadTimeView.SerializeTimeSpan(t));
			});
			return customTimes;
		}

		// Token: 0x04002CFF RID: 11519
		private bool isBound;

		// Token: 0x04002D00 RID: 11520
		private TableItemStyle timeOverStyle;

		// Token: 0x04002D01 RID: 11521
		internal static readonly string TimeColName = "Time";

		// Token: 0x04002D02 RID: 11522
		private TimeDataList dataList;

		// Token: 0x04002D03 RID: 11523
		private HtmlTextWriterTag tagKey = HtmlTextWriterTag.Div;

		// Token: 0x04002D04 RID: 11524
		private DefaultTimeTemplate defaultTimeTemplate = new DefaultTimeTemplate();

		// Token: 0x04002D05 RID: 11525
		private DefaultTimeHeaderTemplate defaultTimeHeaderTemplate = new DefaultTimeHeaderTemplate();

		// Token: 0x04002D06 RID: 11526
		internal bool shouldSerializeDataSource;
	}
}
