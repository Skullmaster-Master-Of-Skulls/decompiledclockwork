using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Licensing;
using Telerik.Web.Design;
using Telerik.Web.UI.Dock;

namespace Telerik.Web.UI
{
	// Token: 0x02000458 RID: 1112
	[RequiredScript(typeof(TouchScrollExtender))]
	[EmbeddedSkin("DockZone")]
	[RequiredCss("Telerik.Web.UI.Skins.Common.fonticons.css", RenderMode.Lightweight, typeof(RadDockZone))]
	[RequiredScript(typeof(jQueryPlugins))]
	[RequiredScript(typeof(Core))]
	[LicenseProvider(typeof(TelerikLicenseProvider))]
	[RequiredScript(typeof(AnimationFramework))]
	[ClientScriptResource("Telerik.Web.UI.RadDockZone", "Telerik.Web.UI.Dock.RadDockZone.js")]
	[ParseChildren(true, "Controls")]
	[PersistChildren(true)]
	[TelerikToolboxCategory("Container")]
	[ToolboxBitmap(typeof(RadDockZone), "Telerik.Web.UI.Dock.png")]
	[ToolboxData("<{0}:RadDockZone Runat=server Width=300px Height=300px></{0}:RadDockZone>")]
	[Designer("Telerik.Web.Design.RadDockZoneDesigner, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4")]
	[LightweightRendering]
	public class RadDockZone : RadWebControl
	{
		// Token: 0x17000D0B RID: 3339
		// (get) Token: 0x0600282C RID: 10284 RVA: 0x00082798 File Offset: 0x00080998
		[Description("Collection of the RadDock objects inside the RadDockZone.")]
		public DockCollection Docks
		{
			get
			{
				if (this._docks == null)
				{
					this._docks = new DockCollection(this);
				}
				return this._docks;
			}
		}

		// Token: 0x17000D0C RID: 3340
		// (get) Token: 0x0600282D RID: 10285 RVA: 0x000827B4 File Offset: 0x000809B4
		[Description("The collection of controls in the dock zone.")]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public override ControlCollection Controls
		{
			get
			{
				return base.Controls;
			}
		}

		// Token: 0x0600282E RID: 10286 RVA: 0x000827BC File Offset: 0x000809BC
		public string GetUniqueName()
		{
			string text = this.UniqueName;
			if (string.IsNullOrEmpty(text))
			{
				text = this.ID;
			}
			return text;
		}

		// Token: 0x17000D0D RID: 3341
		// (get) Token: 0x0600282F RID: 10287 RVA: 0x000827E0 File Offset: 0x000809E0
		[DefaultValue(false)]
		public override bool EnableEmbeddedSkins
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000D0E RID: 3342
		// (get) Token: 0x06002830 RID: 10288 RVA: 0x000827E4 File Offset: 0x000809E4
		private IDockLayout Layout
		{
			get
			{
				if (this._layout == null)
				{
					for (Control parent = this.Parent; parent != null; parent = parent.Parent)
					{
						IDockLayout dockLayout = parent as IDockLayout;
						if (dockLayout != null)
						{
							this._layout = dockLayout;
							break;
						}
					}
				}
				return this._layout;
			}
		}

		// Token: 0x06002831 RID: 10289 RVA: 0x00082828 File Offset: 0x00080A28
		protected override void AddedControl(Control control, int index)
		{
			RadDock radDock = control as RadDock;
			if (radDock == null)
			{
				throw new InvalidOperationException(string.Format("{0} can contain only controls of type {1}", base.GetType().FullName, typeof(RadDock).FullName));
			}
			base.AddedControl(control, index);
			if (this.IsRenderModeSet)
			{
				radDock.RenderMode = this.RenderMode;
			}
			this.Docks.Insert(index, radDock);
		}

		// Token: 0x06002832 RID: 10290 RVA: 0x00082892 File Offset: 0x00080A92
		protected override void RemovedControl(Control control)
		{
			base.RemovedControl(control);
			this.Docks.Remove((RadDock)control);
		}

		// Token: 0x06002833 RID: 10291 RVA: 0x000828B0 File Offset: 0x00080AB0
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			if (this.Height != Unit.Empty)
			{
				base.Style.Add(HtmlTextWriterStyle.OverflowY, "auto");
			}
			base.AddAttributesToRender(writer);
			writer.AddStyleAttribute("min-width", this.MinWidth.ToString(CultureInfo.InvariantCulture));
			writer.AddStyleAttribute("min-height", this.MinHeight.ToString(CultureInfo.InvariantCulture));
		}

		// Token: 0x06002834 RID: 10292 RVA: 0x00082924 File Offset: 0x00080B24
		protected override void RenderChildren(HtmlTextWriter writer)
		{
			foreach (RadDock radDock in this.Docks)
			{
				radDock.RenderControlAlways(writer);
			}
			this.RenderDropPlaceholder(writer);
			if (this.Orientation == Orientation.Horizontal)
			{
				this.RenderClearElement(writer);
			}
		}

		// Token: 0x06002835 RID: 10293 RVA: 0x00082988 File Offset: 0x00080B88
		protected override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
		{
			List<ScriptDescriptor> list = new List<ScriptDescriptor>(base.GetScriptDescriptors());
			foreach (RadDock radDock in this.Docks)
			{
				if (radDock.Visible)
				{
					list.AddRange(radDock.GetDockScriptDescriptors());
				}
			}
			return list;
		}

		// Token: 0x06002836 RID: 10294 RVA: 0x000829F0 File Offset: 0x00080BF0
		private void RenderDropPlaceholder(HtmlTextWriter writer)
		{
			writer.AddStyleAttribute(HtmlTextWriterStyle.Display, "none");
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "RadDock RadDock_Default rdPlaceHolder");
			writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID + "_D");
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			writer.Write("<!-- -->");
			writer.RenderEndTag();
		}

		// Token: 0x06002837 RID: 10295 RVA: 0x00082A48 File Offset: 0x00080C48
		private void RenderClearElement(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rdzClear");
			writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID + "_C");
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			writer.Write("<!-- -->");
			writer.RenderEndTag();
		}

		// Token: 0x06002838 RID: 10296 RVA: 0x00082A88 File Offset: 0x00080C88
		protected override void DescribeComponent(IScriptDescriptor descriptor)
		{
			base.DescribeComponent(descriptor);
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			descriptor.AddScriptProperty("allowedDocks", javaScriptSerializer.Serialize(this.AllowedDocks));
			descriptor.AddProperty("uniqueName", this.GetUniqueName());
			descriptor.AddProperty("clientID", this.ClientID);
		}

		// Token: 0x06002839 RID: 10297 RVA: 0x00082ADB File Offset: 0x00080CDB
		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			if (this.Layout != null)
			{
				this.Layout.RegisterDockZone(this);
			}
		}

		// Token: 0x0600283A RID: 10298 RVA: 0x00082AF8 File Offset: 0x00080CF8
		protected override void OnUnload(EventArgs e)
		{
			if (this.Layout != null)
			{
				this.Layout.UnRegisterDockZone(this);
			}
			base.OnUnload(e);
		}

		// Token: 0x0600283B RID: 10299 RVA: 0x00082B18 File Offset: 0x00080D18
		protected override void ControlPreRender()
		{
			foreach (RadDock radDock in this.Docks)
			{
				if (radDock.IsInInvisibleParent)
				{
					radDock.RegisterScriptControlAndCssReferences();
					radDock.InitializeDefaultTitlebarContentAndCommands();
				}
			}
			base.ControlPreRender();
		}

		// Token: 0x17000D0F RID: 3343
		// (get) Token: 0x0600283C RID: 10300 RVA: 0x00082B78 File Offset: 0x00080D78
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Div;
			}
		}

		// Token: 0x17000D10 RID: 3344
		// (get) Token: 0x0600283D RID: 10301 RVA: 0x00082B7C File Offset: 0x00080D7C
		protected override string CssClassFormatString
		{
			get
			{
				return "RadDockZone RadDockZone_{0} " + ((this.Orientation == Orientation.Horizontal) ? "rdHorizontal" : "rdVertical");
			}
		}

		// Token: 0x17000D11 RID: 3345
		// (get) Token: 0x0600283E RID: 10302 RVA: 0x00082B9C File Offset: 0x00080D9C
		protected internal override bool SupportsRenderingMode
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600283F RID: 10303 RVA: 0x00082BBC File Offset: 0x00080DBC
		protected bool AllDocksAllowed(string[] allowedDocks)
		{
			return this.Docks.All((RadDock dock) => allowedDocks.Contains(dock.UniqueName));
		}

		// Token: 0x17000D12 RID: 3346
		// (get) Token: 0x06002840 RID: 10304 RVA: 0x00082BED File Offset: 0x00080DED
		// (set) Token: 0x06002841 RID: 10305 RVA: 0x00082C18 File Offset: 0x00080E18
		[ClientControlProperty]
		[DefaultValue(true)]
		[Description("Specifies whether the RadDocks will be resized to fit when docked in the RadDockZone.")]
		[Category("Behavior")]
		public bool FitDocks
		{
			get
			{
				return this.ViewState["FitDocks"] == null || (bool)this.ViewState["FitDocks"];
			}
			set
			{
				this.ViewState["FitDocks"] = value;
			}
		}

		// Token: 0x17000D13 RID: 3347
		// (get) Token: 0x06002842 RID: 10306 RVA: 0x00082C30 File Offset: 0x00080E30
		// (set) Token: 0x06002843 RID: 10307 RVA: 0x00082C5F File Offset: 0x00080E5F
		[DefaultValue("")]
		[ClientControlProperty]
		[Description("Specifies a CSS class name, which will be applied when the RadDockZone is highlighted.")]
		[Category("Behavior")]
		public string HighlightedCssClass
		{
			get
			{
				if (this.ViewState["HighlightedCssClass"] == null)
				{
					return string.Empty;
				}
				return (string)this.ViewState["HighlightedCssClass"];
			}
			set
			{
				this.ViewState["HighlightedCssClass"] = value;
			}
		}

		// Token: 0x17000D14 RID: 3348
		// (get) Token: 0x06002844 RID: 10308 RVA: 0x00082C72 File Offset: 0x00080E72
		// (set) Token: 0x06002845 RID: 10309 RVA: 0x00082CA1 File Offset: 0x00080EA1
		[Category("Behavior")]
		[DefaultValue("")]
		[Description("Specifies the unique name of the control.")]
		public string UniqueName
		{
			get
			{
				if (this.ViewState["UniqueName"] == null)
				{
					return string.Empty;
				}
				return (string)this.ViewState["UniqueName"];
			}
			set
			{
				this.ViewState["UniqueName"] = value;
			}
		}

		// Token: 0x17000D15 RID: 3349
		// (get) Token: 0x06002846 RID: 10310 RVA: 0x00082CB4 File Offset: 0x00080EB4
		// (set) Token: 0x06002847 RID: 10311 RVA: 0x00082CDF File Offset: 0x00080EDF
		[DefaultValue(Orientation.Vertical)]
		[Description("Specifies the dimension in which docked RadDock controls are arranged.")]
		public virtual Orientation Orientation
		{
			get
			{
				if (this.ViewState["Orientation"] == null)
				{
					return Orientation.Vertical;
				}
				return (Orientation)this.ViewState["Orientation"];
			}
			set
			{
				this.ViewState["Orientation"] = value;
			}
		}

		// Token: 0x17000D16 RID: 3350
		// (get) Token: 0x06002848 RID: 10312 RVA: 0x00082CF7 File Offset: 0x00080EF7
		// (set) Token: 0x06002849 RID: 10313 RVA: 0x00082D28 File Offset: 0x00080F28
		[NotifyParentProperty(true)]
		[Description("Specifies the minimum width of the RadDockZone control.")]
		[DefaultValue(typeof(Unit), "10px")]
		public virtual Unit MinWidth
		{
			get
			{
				if (this.ViewState["MinWidth"] == null)
				{
					return Unit.Pixel(10);
				}
				return (Unit)this.ViewState["MinWidth"];
			}
			set
			{
				this.ViewState["MinWidth"] = value;
			}
		}

		// Token: 0x17000D17 RID: 3351
		// (get) Token: 0x0600284A RID: 10314 RVA: 0x00082D40 File Offset: 0x00080F40
		// (set) Token: 0x0600284B RID: 10315 RVA: 0x00082D71 File Offset: 0x00080F71
		[NotifyParentProperty(true)]
		[DefaultValue(typeof(Unit), "10px")]
		[Description("Specifies the minimum height of the RadDockZone control.")]
		public virtual Unit MinHeight
		{
			get
			{
				if (this.ViewState["MinHeight"] == null)
				{
					return Unit.Pixel(10);
				}
				return (Unit)this.ViewState["MinHeight"];
			}
			set
			{
				this.ViewState["MinHeight"] = value;
			}
		}

		// Token: 0x17000D18 RID: 3352
		// (get) Token: 0x0600284C RID: 10316 RVA: 0x00082D89 File Offset: 0x00080F89
		[ClientControlProperty]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public string LayoutID
		{
			get
			{
				if (this.Layout != null)
				{
					return (this.Layout as Control).ID;
				}
				return string.Empty;
			}
		}

		// Token: 0x17000D19 RID: 3353
		// (get) Token: 0x0600284D RID: 10317 RVA: 0x00082DA9 File Offset: 0x00080FA9
		// (set) Token: 0x0600284E RID: 10318 RVA: 0x00082DD9 File Offset: 0x00080FD9
		[Description("Specifies the UniqueNames of the RadDock controls, that will be allowed to dock in the zone.")]
		[TypeConverter(typeof(ListConverter))]
		[Category("Behavior")]
		public string[] AllowedDocks
		{
			get
			{
				if (this.ViewState["AllowedDocks"] == null)
				{
					return new string[0];
				}
				return (string[])this.ViewState["AllowedDocks"];
			}
			set
			{
				if (!this.AllDocksAllowed(value))
				{
					throw new NotAllowedDockException("The Docks collection contains docks that are not listed in the AllowedDocks collection");
				}
				this.ViewState["AllowedDocks"] = value;
			}
		}

		// Token: 0x0600284F RID: 10319 RVA: 0x00082E00 File Offset: 0x00081000
		protected internal override void DescribeClientProperties(IScriptDescriptor descriptor)
		{
			base.DescribeProperty<bool>(descriptor, "fitDocks", this.FitDocks, true);
			base.DescribeProperty<string>(descriptor, "highlightedCssClass", this.HighlightedCssClass, "");
			base.DescribeProperty<string>(descriptor, "layoutID", this.LayoutID, null);
			base.DescribeClientProperties(descriptor);
		}

		// Token: 0x06002850 RID: 10320 RVA: 0x00082E51 File Offset: 0x00081051
		protected internal override void DescribeClientEvents(IScriptDescriptor descriptor)
		{
			base.DescribeClientEvents(descriptor);
		}

		// Token: 0x04000A2B RID: 2603
		private DockCollection _docks;

		// Token: 0x04000A2C RID: 2604
		private IDockLayout _layout;
	}
}
