using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Licensing;
using Telerik.Web.UI.Common;

namespace Telerik.Web.UI
{
	// Token: 0x02000890 RID: 2192
	[LicenseProvider(typeof(TelerikLicenseProvider))]
	[LightweightRendering]
	[DefaultProperty("Items")]
	[ClientScriptResource("Telerik.Web.UI.RadSplitter", "Telerik.Web.UI.Splitter.RadSplitterScripts.js")]
	[RequiredCss("Telerik.Web.UI.Skins.Common.fonticons.css", RenderMode.Lightweight, typeof(RadEditor))]
	[RequiredCss("Telerik.Web.UI.Skins.Common.MaterialRipple.css", RenderMode.Lightweight, typeof(RadButton))]
	[TelerikToolboxCategory("Container")]
	[ToolboxBitmap(typeof(RadSplitter), "Telerik.Web.UI.Splitter.png")]
	[ToolboxData("<{0}:RadSplitter Runat=server></{0}:RadSplitter>")]
	[Description("Telerik RadSplitter")]
	[ParseChildren(typeof(SplitterItem))]
	[PersistChildren(true)]
	[EmbeddedSkin("Splitter")]
	[Designer("Telerik.Web.Design.RadSplitterDesigner, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4")]
	[EmbeddedSkin("Splitter", "Default")]
	[RequiredScript(typeof(jQueryPlugins))]
	[RequiredScript(typeof(MaterialRipple))]
	public class RadSplitter : SplitterItemsContainer
	{
		// Token: 0x17001AB5 RID: 6837
		// (get) Token: 0x0600516A RID: 20842 RVA: 0x000FD634 File Offset: 0x000FB834
		protected internal override bool SupportsRenderingMode
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001AB6 RID: 6838
		// (get) Token: 0x0600516B RID: 20843 RVA: 0x000FD637 File Offset: 0x000FB837
		// (set) Token: 0x0600516C RID: 20844 RVA: 0x000FD658 File Offset: 0x000FB858
		[Description("Sets/gets the pixels that should be subtracted from the splitter height when its height is defined in percent")]
		[ClientControlProperty]
		[DefaultValue(0)]
		[Category("Appearance")]
		public int HeightOffset
		{
			get
			{
				return (int)(this.ViewState["HeightOffset"] ?? 0);
			}
			set
			{
				this.ViewState["HeightOffset"] = value;
			}
		}

		// Token: 0x17001AB7 RID: 6839
		// (get) Token: 0x0600516D RID: 20845 RVA: 0x000FD670 File Offset: 0x000FB870
		// (set) Token: 0x0600516E RID: 20846 RVA: 0x000FD6CF File Offset: 0x000FB8CF
		[Description("Resize the splitter in 100% of the page")]
		[Obsolete("The FullScreenMode property is deprecated in RadSplitter for ASP.NET Ajax. Use Width and Height instead.", false)]
		[Browsable(false)]
		[Category("Appearance")]
		[DefaultValue(false)]
		public bool FullScreenMode
		{
			get
			{
				return this.Width.Equals(Unit.Percentage(100.0)) && this.Height.Equals(Unit.Percentage(100.0));
			}
			set
			{
				if (value)
				{
					this.Width = Unit.Percentage(100.0);
					this.Height = Unit.Percentage(100.0);
				}
			}
		}

		// Token: 0x17001AB8 RID: 6840
		// (get) Token: 0x0600516F RID: 20847 RVA: 0x000FD6FC File Offset: 0x000FB8FC
		// (set) Token: 0x06005170 RID: 20848 RVA: 0x000FD71D File Offset: 0x000FB91D
		[Category("Appearance")]
		[DefaultValue(true)]
		[ClientControlProperty]
		[Description("Whether the Splitter should be visible during its initialization or not")]
		[ClientPropertyName("_visibleDuringInit")]
		public bool VisibleDuringInit
		{
			get
			{
				return (bool)(this.ViewState["VisibleDuringInit"] ?? true);
			}
			set
			{
				this.ViewState["VisibleDuringInit"] = value;
			}
		}

		// Token: 0x17001AB9 RID: 6841
		// (get) Token: 0x06005171 RID: 20849 RVA: 0x000FD738 File Offset: 0x000FB938
		// (set) Token: 0x06005172 RID: 20850 RVA: 0x000FD793 File Offset: 0x000FB993
		[Description("Sets/gets the height of the splitter")]
		[ClientPropertyName("_height")]
		[ClientControlProperty]
		[DefaultValue(typeof(Unit), "400px")]
		[Category("Behavior")]
		public override Unit Height
		{
			get
			{
				if (this.ViewState["Height"] == null || string.IsNullOrEmpty(this.ViewState["Height"].ToString()))
				{
					return Unit.Pixel(400);
				}
				return (Unit)this.ViewState["Height"];
			}
			set
			{
				this.ViewState["Height"] = value;
			}
		}

		// Token: 0x17001ABA RID: 6842
		// (get) Token: 0x06005173 RID: 20851 RVA: 0x000FD7AC File Offset: 0x000FB9AC
		// (set) Token: 0x06005174 RID: 20852 RVA: 0x000FD807 File Offset: 0x000FBA07
		[ClientPropertyName("_width")]
		[Category("Behavior")]
		[Description("Sets/gets the width of the splitter")]
		[DefaultValue(typeof(Unit), "400px")]
		[ClientControlProperty]
		public override Unit Width
		{
			get
			{
				if (this.ViewState["Width"] == null || string.IsNullOrEmpty(this.ViewState["Width"].ToString()))
				{
					return Unit.Pixel(400);
				}
				return (Unit)this.ViewState["Width"];
			}
			set
			{
				this.ViewState["Width"] = value;
			}
		}

		// Token: 0x17001ABB RID: 6843
		// (get) Token: 0x06005175 RID: 20853 RVA: 0x000FD81F File Offset: 0x000FBA1F
		// (set) Token: 0x06005176 RID: 20854 RVA: 0x000FD840 File Offset: 0x000FBA40
		[DefaultValue(false)]
		[ClientControlProperty]
		[Category("Behavior")]
		[Description("Sets/gets whether the rendering of the splitter panes is previewed during the resize")]
		public bool LiveResize
		{
			get
			{
				return (bool)(this.ViewState["LiveResize"] ?? false);
			}
			set
			{
				this.ViewState["LiveResize"] = value;
			}
		}

		// Token: 0x17001ABC RID: 6844
		// (get) Token: 0x06005177 RID: 20855 RVA: 0x000FD858 File Offset: 0x000FBA58
		// (set) Token: 0x06005178 RID: 20856 RVA: 0x000FD879 File Offset: 0x000FBA79
		[DefaultValue(true)]
		[ClientPropertyName("_resizeWithBrowserWindow")]
		[Category("Behavior")]
		[Description("Sets/gets whether the splitter will be resized when the browser window is resized. The Width or Height properties should be defined in percent.")]
		[ClientControlProperty]
		public bool ResizeWithBrowserWindow
		{
			get
			{
				return (bool)(this.ViewState["ResizeWithBrowserWindow"] ?? true);
			}
			set
			{
				this.ViewState["ResizeWithBrowserWindow"] = value;
			}
		}

		// Token: 0x17001ABD RID: 6845
		// (get) Token: 0x06005179 RID: 20857 RVA: 0x000FD891 File Offset: 0x000FBA91
		// (set) Token: 0x0600517A RID: 20858 RVA: 0x000FD8B2 File Offset: 0x000FBAB2
		[Description("Sets/gets whether the splitter will resize when the parent pane is resized.")]
		[DefaultValue(true)]
		[Category("Behavior")]
		public bool ResizeWithParentPane
		{
			get
			{
				return (bool)(this.ViewState["ResizeWithParentPane"] ?? true);
			}
			set
			{
				this.ViewState["ResizeWithParentPane"] = value;
			}
		}

		// Token: 0x17001ABE RID: 6846
		// (get) Token: 0x0600517B RID: 20859 RVA: 0x000FD8CA File Offset: 0x000FBACA
		// (set) Token: 0x0600517C RID: 20860 RVA: 0x000FD8EB File Offset: 0x000FBAEB
		[ClientPropertyName("_orientation")]
		[ClientControlProperty]
		[DefaultValue(Orientation.Vertical)]
		[Category("Behavior")]
		[Description("Sets/gets the orientation of the panes inside the splitter")]
		public Orientation Orientation
		{
			get
			{
				return (Orientation)(this.ViewState["Orientation"] ?? Orientation.Vertical);
			}
			set
			{
				this.ViewState["Orientation"] = value;
			}
		}

		// Token: 0x17001ABF RID: 6847
		// (get) Token: 0x0600517D RID: 20861 RVA: 0x000FD903 File Offset: 0x000FBB03
		// (set) Token: 0x0600517E RID: 20862 RVA: 0x000FD924 File Offset: 0x000FBB24
		[Description("Set/Get the way the panes are resized")]
		[DefaultValue(SplitterResizeMode.AdjacentPane)]
		[Category("Behavior")]
		[ClientControlProperty]
		[ClientPropertyName("_resizeMode")]
		public SplitterResizeMode ResizeMode
		{
			get
			{
				return (SplitterResizeMode)(this.ViewState["ResizeMode"] ?? SplitterResizeMode.AdjacentPane);
			}
			set
			{
				this.ViewState["ResizeMode"] = value;
			}
		}

		// Token: 0x17001AC0 RID: 6848
		// (get) Token: 0x0600517F RID: 20863 RVA: 0x000FD93C File Offset: 0x000FBB3C
		// (set) Token: 0x06005180 RID: 20864 RVA: 0x000FD95D File Offset: 0x000FBB5D
		[ClientControlProperty]
		[DefaultValue(1)]
		[Category("Appearance")]
		[Description("Set/Get size of the splitter border")]
		[ClientPropertyName("_borderSize")]
		public int BorderSize
		{
			get
			{
				return (int)(this.ViewState["BorderSize"] ?? 1);
			}
			set
			{
				this.ViewState["BorderSize"] = value;
			}
		}

		// Token: 0x17001AC1 RID: 6849
		// (get) Token: 0x06005181 RID: 20865 RVA: 0x000FD975 File Offset: 0x000FBB75
		// (set) Token: 0x06005182 RID: 20866 RVA: 0x000FD996 File Offset: 0x000FBB96
		[Category("Appearance")]
		[ClientControlProperty]
		[DefaultValue(1)]
		[ClientPropertyName("_panesBorderSize")]
		[Description("Set/Get size of the splitter panes border")]
		public int PanesBorderSize
		{
			get
			{
				return (int)(this.ViewState["PanesBorderSize"] ?? 1);
			}
			set
			{
				this.ViewState["PanesBorderSize"] = value;
			}
		}

		// Token: 0x17001AC2 RID: 6850
		// (get) Token: 0x06005183 RID: 20867 RVA: 0x000FD9AE File Offset: 0x000FBBAE
		// (set) Token: 0x06005184 RID: 20868 RVA: 0x000FD9D3 File Offset: 0x000FBBD3
		[DefaultValue("")]
		[Category("Appearance")]
		[Description("Set/Get size of the split bars - in pixels")]
		public Unit SplitBarsSize
		{
			get
			{
				return (Unit)(this.ViewState["SplitBarsSize"] ?? Unit.Empty);
			}
			set
			{
				this.ViewState["SplitBarsSize"] = value;
			}
		}

		// Token: 0x17001AC3 RID: 6851
		// (get) Token: 0x06005185 RID: 20869 RVA: 0x000FD9EB File Offset: 0x000FBBEB
		// (set) Token: 0x06005186 RID: 20870 RVA: 0x000FD9F3 File Offset: 0x000FBBF3
		[Obsolete("This property is now obsolete. Please use the OnClientLoad property instead.", false)]
		[DefaultValue("")]
		public string OnClientLoaded
		{
			get
			{
				return this.OnClientLoad;
			}
			set
			{
				this.OnClientLoad = value;
			}
		}

		// Token: 0x17001AC4 RID: 6852
		// (get) Token: 0x06005187 RID: 20871 RVA: 0x000FD9FC File Offset: 0x000FBBFC
		// (set) Token: 0x06005188 RID: 20872 RVA: 0x000FDA1C File Offset: 0x000FBC1C
		[DefaultValue("")]
		[Description("The name of the javascript function called when the initialization of the splitter is done.")]
		[ClientPropertyName("_load")]
		[ClientControlProperty]
		[Category("Client-side events")]
		public string OnClientLoad
		{
			get
			{
				return ((string)this.ViewState["OnClientLoad"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["OnClientLoad"] = value;
			}
		}

		// Token: 0x17001AC5 RID: 6853
		// (get) Token: 0x06005189 RID: 20873 RVA: 0x000FDA2F File Offset: 0x000FBC2F
		// (set) Token: 0x0600518A RID: 20874 RVA: 0x000FDA5E File Offset: 0x000FBC5E
		[Description("The name of the javascript function called when the splitter is resized.")]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Category("Appearance")]
		[ClientControlEvent]
		[ClientPropertyName("resized")]
		public string OnClientResized
		{
			get
			{
				if (this.ViewState["OnClientResized"] == null)
				{
					return string.Empty;
				}
				return (string)this.ViewState["OnClientResized"];
			}
			set
			{
				this.ViewState["OnClientResized"] = value;
			}
		}

		// Token: 0x17001AC6 RID: 6854
		// (get) Token: 0x0600518B RID: 20875 RVA: 0x000FDA71 File Offset: 0x000FBC71
		// (set) Token: 0x0600518C RID: 20876 RVA: 0x000FDA79 File Offset: 0x000FBC79
		[Obsolete("This property is now obsolete. Please use the OnClientResizing property instead.", false)]
		[DefaultValue("")]
		public string OnClientBeforeResize
		{
			get
			{
				return this.OnClientResizing;
			}
			set
			{
				this.OnClientResizing = value;
			}
		}

		// Token: 0x17001AC7 RID: 6855
		// (get) Token: 0x0600518D RID: 20877 RVA: 0x000FDA82 File Offset: 0x000FBC82
		// (set) Token: 0x0600518E RID: 20878 RVA: 0x000FDAA2 File Offset: 0x000FBCA2
		[ClientControlEvent]
		[Category("Appearance")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientPropertyName("resizing")]
		[Description("The name of the javascript function called before the splitter is resized.")]
		[DefaultValue("")]
		public string OnClientResizing
		{
			get
			{
				return ((string)this.ViewState["OnClientResizing"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["OnClientResizing"] = value;
			}
		}

		// Token: 0x0600518F RID: 20879 RVA: 0x000FDAB8 File Offset: 0x000FBCB8
		private ArrayList GetPanesFixedSize()
		{
			ArrayList arrayList = new ArrayList();
			ArrayList panes = this.GetPanes();
			foreach (object obj in panes)
			{
				RadPane radPane = (RadPane)obj;
				if (radPane.IsFixedSize())
				{
					arrayList.Add(radPane);
				}
			}
			return arrayList;
		}

		// Token: 0x06005190 RID: 20880 RVA: 0x000FDB28 File Offset: 0x000FBD28
		private ArrayList GetPanesPercenatgesSize()
		{
			ArrayList arrayList = new ArrayList();
			ArrayList panes = this.GetPanes();
			foreach (object obj in panes)
			{
				RadPane radPane = (RadPane)obj;
				if (radPane.IsPercentagesSize())
				{
					arrayList.Add(radPane);
				}
			}
			return arrayList;
		}

		// Token: 0x06005191 RID: 20881 RVA: 0x000FDB98 File Offset: 0x000FBD98
		private int GetPanesFixedVarSize()
		{
			int num = 0;
			ArrayList panesFixedSize = this.GetPanesFixedSize();
			foreach (object obj in panesFixedSize)
			{
				RadPane radPane = (RadPane)obj;
				num += (int)radPane.GetVarSize().Value;
			}
			return num;
		}

		// Token: 0x06005192 RID: 20882 RVA: 0x000FDC08 File Offset: 0x000FBE08
		private int GetPanesPercenatgesVarSize()
		{
			int num = 0;
			ArrayList panesPercenatgesSize = this.GetPanesPercenatgesSize();
			foreach (object obj in panesPercenatgesSize)
			{
				RadPane radPane = (RadPane)obj;
				num += (int)radPane.GetVarSize().Value;
			}
			return num;
		}

		// Token: 0x06005193 RID: 20883 RVA: 0x000FDC78 File Offset: 0x000FBE78
		private int GetFreePanesCount()
		{
			int num = 0;
			ArrayList panes = this.GetPanes();
			foreach (object obj in panes)
			{
				RadPane radPane = (RadPane)obj;
				if (radPane.IsFreeSize())
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x06005194 RID: 20884 RVA: 0x000FDCE0 File Offset: 0x000FBEE0
		private int GetSplitBarsSize()
		{
			ArrayList splitBars = this.GetSplitBars();
			if (splitBars.Count < 1)
			{
				return 0;
			}
			int defaultSize = ((RadSplitBar)splitBars[0]).GetDefaultSize();
			int num = this.SplitBarsSize.IsEmpty ? defaultSize : ((int)this.SplitBarsSize.Value);
			if (this.BorderSize == 0)
			{
				num += 2;
			}
			return splitBars.Count * num;
		}

		// Token: 0x06005195 RID: 20885 RVA: 0x000FDD4C File Offset: 0x000FBF4C
		private int GetBordersSize()
		{
			int num = 0;
			ArrayList panes = this.GetPanes();
			foreach (object obj in panes)
			{
				RadPane radPane = (RadPane)obj;
				if (radPane.GetExpandedSize().Value == 0.0)
				{
					num++;
				}
			}
			return (num + this.GetSplitBars().Count + 1) * this.BorderSize;
		}

		// Token: 0x06005196 RID: 20886 RVA: 0x000FDDDC File Offset: 0x000FBFDC
		private Unit GetWidth()
		{
			Unit result = this.Width;
			if (this.IsNested())
			{
				RadPane parentPane = base.GetParentPane();
				Unit width = parentPane.GetWidth();
				if (!width.IsEmpty && width.Type == UnitType.Pixel)
				{
					result = width;
				}
				else if (result.Type != UnitType.Pixel)
				{
					result = Unit.Pixel(400);
				}
			}
			return result;
		}

		// Token: 0x06005197 RID: 20887 RVA: 0x000FDE34 File Offset: 0x000FC034
		private Unit GetHeight()
		{
			Unit result = this.Height;
			if (this.IsNested())
			{
				RadPane parentPane = base.GetParentPane();
				Unit height = parentPane.GetHeight();
				if (!height.IsEmpty && height.Type == UnitType.Pixel)
				{
					result = height;
				}
				else if (result.Type != UnitType.Pixel)
				{
					result = Unit.Pixel(400);
				}
			}
			return result;
		}

		// Token: 0x06005198 RID: 20888 RVA: 0x000FDE8C File Offset: 0x000FC08C
		private string GetWidthAttribute()
		{
			Unit width = this.GetWidth();
			string result = "";
			if (!width.IsEmpty && width.Type == UnitType.Pixel)
			{
				result = string.Format("width:{0};", width);
			}
			return result;
		}

		// Token: 0x06005199 RID: 20889 RVA: 0x000FDECC File Offset: 0x000FC0CC
		private string GetHeightAttribute()
		{
			Unit height = this.GetHeight();
			string result = "";
			if (!height.IsEmpty && height.Type == UnitType.Pixel)
			{
				result = string.Format("height:{0};", height);
			}
			return result;
		}

		// Token: 0x0600519A RID: 20890 RVA: 0x000FDF0B File Offset: 0x000FC10B
		protected Unit GetVarSize()
		{
			if (!this.IsHorizontal())
			{
				return this.Width;
			}
			return this.Height;
		}

		// Token: 0x0600519B RID: 20891 RVA: 0x000FDF24 File Offset: 0x000FC124
		internal Unit GetNotFixedPaneSize(Unit size, bool useDefaultSplitterSize)
		{
			int num = useDefaultSplitterSize ? 400 : ((int)this.GetVarSize().Value);
			int num2 = num - this.GetBordersSize() - this.GetSplitBarsSize();
			Unit result = Unit.Pixel(0);
			if (num2 > 0)
			{
				if (size == Unit.Empty)
				{
					int freePanesCount = this.GetFreePanesCount();
					if (freePanesCount > 0)
					{
						int num3 = num2 - this.GetPanesFixedVarSize() - num2 * this.GetPanesPercenatgesVarSize() / 100;
						result = Unit.Pixel(num3 / freePanesCount);
					}
				}
				else if (size.Type == UnitType.Percentage)
				{
					result = Unit.Pixel((int)((double)num2 * size.Value) / 100);
				}
			}
			if (result.Value < 0.0)
			{
				result = Unit.Pixel(0);
			}
			return result;
		}

		// Token: 0x0600519C RID: 20892 RVA: 0x000FDFDC File Offset: 0x000FC1DC
		internal bool IsFixedSize()
		{
			Unit varSize = this.GetVarSize();
			return !varSize.IsEmpty && varSize.Type == UnitType.Pixel;
		}

		// Token: 0x0600519D RID: 20893 RVA: 0x000FE005 File Offset: 0x000FC205
		internal bool IsTouchSkin()
		{
			return this.EnableEmbeddedSkins && base.RuntimeSkin.EndsWith("Touch");
		}

		// Token: 0x0600519E RID: 20894 RVA: 0x000FE024 File Offset: 0x000FC224
		private bool GetAttachResizeHandler()
		{
			if ((this.Width.Type == UnitType.Percentage || this.Height.Type == UnitType.Percentage) && this.ResizeWithBrowserWindow && !this.IsNested())
			{
				int num = 0;
				ArrayList panes = this.GetPanes();
				foreach (object obj in panes)
				{
					RadPane radPane = (RadPane)obj;
					if (radPane.IsFreeSize() || radPane.IsPercentagesSize())
					{
						num++;
					}
				}
				if (num > 0)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600519F RID: 20895 RVA: 0x000FE0D0 File Offset: 0x000FC2D0
		internal bool IsNested()
		{
			return base.GetParentPane() != null && this.ResizeWithParentPane;
		}

		// Token: 0x17001AC8 RID: 6856
		// (get) Token: 0x060051A0 RID: 20896 RVA: 0x000FE0E2 File Offset: 0x000FC2E2
		internal HttpBrowserCapabilities Browser
		{
			get
			{
				if (!base.DesignMode)
				{
					return this.Context.Request.Browser;
				}
				return new HttpBrowserCapabilities();
			}
		}

		// Token: 0x060051A1 RID: 20897 RVA: 0x000FE104 File Offset: 0x000FC304
		internal Unit GetInnerSize(bool useDefaultSplitterSize)
		{
			Unit result = useDefaultSplitterSize ? 400 : (this.IsHorizontal() ? this.GetWidth() : this.GetHeight());
			if (!result.IsEmpty && result.Type == UnitType.Pixel)
			{
				double num = result.Value;
				if (!this.IsNested())
				{
					num -= (double)(2 * this.BorderSize);
				}
				return new Unit(num);
			}
			return result;
		}

		// Token: 0x060051A2 RID: 20898 RVA: 0x000FE16E File Offset: 0x000FC36E
		private string GetInitializeScript()
		{
			return string.Format("Telerik.Web.UI.RadSplitter._preInitialize(\"{0}\");", this.ClientID);
		}

		// Token: 0x060051A3 RID: 20899 RVA: 0x000FE180 File Offset: 0x000FC380
		protected override void RegisterInitializeScriptWithScriptManager()
		{
			string initializeScript = this.GetInitializeScript();
			ScriptManager.RegisterStartupScript(this.Page, typeof(RadSplitter), this.ClientID + initializeScript, initializeScript, true);
		}

		// Token: 0x060051A4 RID: 20900 RVA: 0x000FE1B7 File Offset: 0x000FC3B7
		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			if (this.Page != null)
			{
				this.Page.PreRenderComplete += this.Page_PreRenderComplete;
			}
		}

		// Token: 0x060051A5 RID: 20901 RVA: 0x000FE1E0 File Offset: 0x000FC3E0
		protected override void ControlPreRender()
		{
			if (this.Page != null && this.Page.Form != null && this.RegisterWithScriptManager && base.ScriptManager != null && base.ScriptManager.LoadScriptsBeforeUI)
			{
				this.RegisterInitializeScriptWithScriptManager();
			}
			if (this.IsNested())
			{
				this.RegisterToResizeWithParentPane();
				this.Height = this.GetHeight();
				this.Width = this.GetWidth();
			}
			else
			{
				ArrayList panesFixedSize = this.GetPanesFixedSize();
				if (panesFixedSize.Count == this.GetPanes().Count)
				{
					Unit unit = Unit.Pixel(this.GetPanesFixedVarSize() + this.GetSplitBarsSize() + this.GetBordersSize());
					if (this.IsHorizontal())
					{
						this.Height = unit;
					}
					else
					{
						this.Width = unit;
					}
				}
			}
			base.ControlPreRender();
		}

		// Token: 0x060051A6 RID: 20902 RVA: 0x000FE2A0 File Offset: 0x000FC4A0
		public override void RenderBeginTag(HtmlTextWriter writer)
		{
			string arg = (!string.IsNullOrEmpty(this.CssClass)) ? string.Format(" class=\"{0}\" ", this.CssClass) : "";
			string widthAttribute = this.GetWidthAttribute();
			string heightAttribute = this.GetHeightAttribute();
			string arg2 = string.Format(" style=\"{0}{1}\"", widthAttribute, heightAttribute);
			writer.Write(string.Format("<div id=\"{0}\"{1}{2}>", this.ClientID, arg, arg2));
			BaseClass.RenderVersionStamp(writer);
			if ((this.Page != null && this.Page.Form == null) || !this.RegisterWithScriptManager)
			{
				string text = string.Format("<script type=\"text/javascript\">{0}</script>", this.GetInitializeScript());
				LiteralControl literalControl = new LiteralControl(text);
				literalControl.RenderControl(writer);
			}
			if (!base.DesignMode && !this.Browser.IsBrowser("IE"))
			{
				writer.Write("<div>");
			}
			this.AddAttributesToRender(writer);
			if (!base.DesignMode)
			{
				writer.RenderBeginTag(HtmlTextWriterTag.Table);
				if (!this.IsHorizontal())
				{
					writer.RenderBeginTag(HtmlTextWriterTag.Tr);
					return;
				}
			}
			else
			{
				writer.RenderBeginTag(HtmlTextWriterTag.Div);
			}
		}

		// Token: 0x060051A7 RID: 20903 RVA: 0x000FE3A8 File Offset: 0x000FC5A8
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Id, string.Format("RAD_SPLITTER_{0}", this.ClientID));
			WebColorConverter webColorConverter = new WebColorConverter();
			string arg = "";
			if (!this.BackColor.IsEmpty)
			{
				arg = string.Format("background-color:{0};", webColorConverter.ConvertToString(this.BackColor));
			}
			string arg2 = "";
			if (!this.IsNested())
			{
				string arg3 = (this.BorderStyle == BorderStyle.NotSet) ? string.Empty : string.Format("border-style:{0};", this.BorderStyle.ToString());
				string arg4 = this.BorderColor.IsEmpty ? string.Empty : string.Format("border-color:{0};", webColorConverter.ConvertToString(this.BorderColor));
				arg2 = string.Format("border-left-width:{0}px;border-top-width:{0}px;{1}{2}", this.BorderSize, arg3, arg4);
			}
			writer.AddAttribute(HtmlTextWriterAttribute.Style, string.Format("width:1px;height:1px;{0}{1}", arg, arg2));
			writer.AddAttribute(HtmlTextWriterAttribute.Class, string.Format(this.CssClassFormatString, base.RuntimeSkin));
			if (base.DesignMode)
			{
				writer.AddStyleAttribute("border", "1px solid red");
			}
		}

		// Token: 0x060051A8 RID: 20904 RVA: 0x000FE4D4 File Offset: 0x000FC6D4
		public override void RenderEndTag(HtmlTextWriter writer)
		{
			if (!base.DesignMode)
			{
				if (!this.IsHorizontal())
				{
					writer.RenderEndTag();
				}
				writer.RenderEndTag();
			}
			else
			{
				writer.RenderEndTag();
			}
			this.RenderClientStateField(writer);
			BaseClass.RenderAjaxCssReferences(this, writer);
			if (!base.DesignMode && !this.Browser.IsBrowser("IE"))
			{
				writer.Write("</div>");
			}
			writer.Write("</div>");
		}

		// Token: 0x060051A9 RID: 20905 RVA: 0x000FE544 File Offset: 0x000FC744
		protected override void RenderContents(HtmlTextWriter writer)
		{
			if (base.DesignMode)
			{
				writer.Write(SkinRegistrar.GetDesignTimeStyleSheet(this));
			}
			this.EnsureChildControls();
			foreach (object obj in base.Items)
			{
				SplitterItem splitterItem = (SplitterItem)obj;
				if (splitterItem.Visible)
				{
					splitterItem.RenderControl(writer);
				}
			}
		}

		// Token: 0x17001AC9 RID: 6857
		// (get) Token: 0x060051AA RID: 20906 RVA: 0x000FE5C0 File Offset: 0x000FC7C0
		protected override string CssClassFormatString
		{
			get
			{
				string text = "RadSplitter RadSplitter_{0}";
				if (this.IsNested())
				{
					text += (this.IsHorizontal() ? " rspNestedHorizontal" : " rspNested");
				}
				if (!this.VisibleDuringInit)
				{
					text += " rspHideRadSplitter";
				}
				return text;
			}
		}

		// Token: 0x060051AB RID: 20907 RVA: 0x000FE60C File Offset: 0x000FC80C
		private void RegisterToResizeWithParentPane()
		{
			RadPane parentPane = base.GetParentPane();
			if (parentPane != null)
			{
				parentPane.ChildSplitterID = this.ClientID;
			}
		}

		// Token: 0x060051AC RID: 20908 RVA: 0x000FE62F File Offset: 0x000FC82F
		public bool IsHorizontal()
		{
			return this.Orientation == Orientation.Horizontal;
		}

		// Token: 0x060051AD RID: 20909 RVA: 0x000FE63C File Offset: 0x000FC83C
		public ArrayList GetSplitBars()
		{
			ArrayList arrayList = new ArrayList();
			foreach (object obj in base.Items)
			{
				SplitterItem splitterItem = (SplitterItem)obj;
				if (splitterItem is RadSplitBar)
				{
					arrayList.Add(splitterItem);
				}
			}
			return arrayList;
		}

		// Token: 0x060051AE RID: 20910 RVA: 0x000FE6A8 File Offset: 0x000FC8A8
		public RadSplitBar GetSplitBarById(string splitBarId)
		{
			return (RadSplitBar)base.GetItemById(splitBarId);
		}

		// Token: 0x060051AF RID: 20911 RVA: 0x000FE6B8 File Offset: 0x000FC8B8
		public ArrayList GetPanes()
		{
			ArrayList arrayList = new ArrayList();
			foreach (object obj in base.Items)
			{
				SplitterItem splitterItem = (SplitterItem)obj;
				if (splitterItem is RadPane)
				{
					arrayList.Add(splitterItem);
				}
			}
			return arrayList;
		}

		// Token: 0x060051B0 RID: 20912 RVA: 0x000FE724 File Offset: 0x000FC924
		public virtual RadPane GetPaneById(string paneId)
		{
			return (RadPane)base.GetItemById(paneId);
		}

		// Token: 0x060051B1 RID: 20913 RVA: 0x000FE734 File Offset: 0x000FC934
		protected override void DescribeComponent(IScriptDescriptor descriptor)
		{
			base.DescribeComponent(descriptor);
			base.DescribeRenderMode(descriptor);
			if (!this.SplitBarsSize.IsEmpty)
			{
				descriptor.AddProperty("_splitBarSize", (int)this.SplitBarsSize.Value);
			}
			RadPane parentPane = base.GetParentPane();
			if (parentPane != null && this.ResizeWithParentPane)
			{
				descriptor.AddProperty("_parentPaneId", parentPane.ClientID);
			}
			descriptor.AddProperty("_isNested", this.IsNested());
			descriptor.AddProperty("_attachResizeHandler", this.GetAttachResizeHandler());
			descriptor.AddProperty("_registerWithScriptManager", this.RegisterWithScriptManager);
		}

		// Token: 0x060051B2 RID: 20914 RVA: 0x000FE7E4 File Offset: 0x000FC9E4
		protected internal override void DescribeClientProperties(IScriptDescriptor descriptor)
		{
			base.DescribeProperty<int>(descriptor, "_borderSize", this.BorderSize, 1);
			base.DescribeProperty<string>(descriptor, "_height", this.Height.ToString(CultureInfo.InvariantCulture), "400px");
			base.DescribeProperty<int>(descriptor, "heightOffset", this.HeightOffset, 0);
			base.DescribeProperty<bool>(descriptor, "liveResize", this.LiveResize, false);
			base.DescribeProperty<string>(descriptor, "_load", this.OnClientLoad, "");
			base.DescribeProperty<Orientation>(descriptor, "_orientation", this.Orientation, Orientation.Vertical);
			base.DescribeProperty<int>(descriptor, "_panesBorderSize", this.PanesBorderSize, 1);
			base.DescribeProperty<SplitterResizeMode>(descriptor, "_resizeMode", this.ResizeMode, SplitterResizeMode.AdjacentPane);
			base.DescribeProperty<bool>(descriptor, "_resizeWithBrowserWindow", this.ResizeWithBrowserWindow, true);
			base.DescribeProperty<bool>(descriptor, "_visibleDuringInit", this.VisibleDuringInit, true);
			base.DescribeProperty<string>(descriptor, "_width", this.Width.ToString(CultureInfo.InvariantCulture), "400px");
			base.DescribeClientProperties(descriptor);
		}

		// Token: 0x060051B3 RID: 20915 RVA: 0x000FE8EF File Offset: 0x000FCAEF
		protected internal override void DescribeClientEvents(IScriptDescriptor descriptor)
		{
			RadWebControl.DescribeEvent(descriptor, "resized", this.OnClientResized);
			RadWebControl.DescribeEvent(descriptor, "resizing", this.OnClientResizing);
			base.DescribeClientEvents(descriptor);
		}
	}
}
