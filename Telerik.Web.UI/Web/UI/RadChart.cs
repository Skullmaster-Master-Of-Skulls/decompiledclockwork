using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Web;
using System.Web.Configuration;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using Telerik.Charting;
using Telerik.Charting.Styles;
using Telerik.Licensing;
using Telerik.Web.UI.Common;

namespace Telerik.Web.UI
{
	// Token: 0x02001804 RID: 6148
	[ToolboxData("<{0}:RadChart runat=\"server\"></{0}:RadChart>")]
	[ClientScriptResource("Telerik.Web.UI.RadChart", "Telerik.Web.UI.Chart.RadChart.js")]
	[Description("Telerik RadChart")]
	[Designer("Telerik.Web.Design.ChartDesigner, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4")]
	[ToolboxBitmap(typeof(RadChart), "Telerik.Web.UI.Chart.png")]
	[TelerikToolboxCategory("Visualization")]
	[ClientCssResource("Telerik.Web.UI.Skins.Chart.css")]
	[PersistChildren(false)]
	[LicenseProvider(typeof(TelerikLicenseProvider))]
	[ParseChildren(true)]
	[DefaultEvent("Click")]
	public class RadChart : RadDataBoundControl, INamingContainer, IPostBackEventHandler, IChartComponent, IChartSupportsScaling, IStateManager, ICallbackEventHandler, IPostBackDataHandler
	{
		// Token: 0x1700484C RID: 18508
		// (get) Token: 0x0600EF0D RID: 61197 RVA: 0x0036676C File Offset: 0x0036496C
		private string applicationPath
		{
			get
			{
				if (string.IsNullOrEmpty(this._applicationPath))
				{
					if (base.DesignMode)
					{
						this._applicationPath = this.ResolvePhysicalLocation("~/");
					}
					else
					{
						this._applicationPath = HttpContext.Current.Request.ApplicationPath;
					}
					if (this._applicationPath != "/")
					{
						this._applicationPath += "/";
					}
				}
				return this._applicationPath;
			}
		}

		// Token: 0x1700484D RID: 18509
		// (get) Token: 0x0600EF0E RID: 61198 RVA: 0x003667E4 File Offset: 0x003649E4
		// (set) Token: 0x0600EF0F RID: 61199 RVA: 0x0036680F File Offset: 0x00364A0F
		[Description("Gets or sets a value indicating whether RadChart should automatically check for the ChartHttpHandler existence in the system.web section of the application configuration file.")]
		[DefaultValue(true)]
		[Category("Behavior")]
		public bool EnableHandlerDetection
		{
			get
			{
				return this.ViewState["EnableHandlerDetection"] == null || (bool)this.ViewState["EnableHandlerDetection"];
			}
			set
			{
				this.ViewState["EnableHandlerDetection"] = value;
			}
		}

		// Token: 0x1700484E RID: 18510
		// (get) Token: 0x0600EF10 RID: 61200 RVA: 0x00366827 File Offset: 0x00364A27
		// (set) Token: 0x0600EF11 RID: 61201 RVA: 0x00366847 File Offset: 0x00364A47
		[Description("Gets or sets a value indicating the URL to the ChartHttpHandler.")]
		[DefaultValue("ChartImage.axd")]
		[Category("Behavior")]
		public string HttpHandlerUrl
		{
			get
			{
				return ((string)this.ViewState["HttpHandlerUrl"]) ?? ChartHttpHandler.Path;
			}
			set
			{
				if (!string.IsNullOrEmpty(value))
				{
					this.ViewState["HttpHandlerUrl"] = value;
				}
			}
		}

		// Token: 0x1700484F RID: 18511
		// (get) Token: 0x0600EF12 RID: 61202 RVA: 0x00366862 File Offset: 0x00364A62
		[DefaultValue(false)]
		public override bool EnableEmbeddedSkins
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17004850 RID: 18512
		// (get) Token: 0x0600EF13 RID: 61203 RVA: 0x00366865 File Offset: 0x00364A65
		// (set) Token: 0x0600EF14 RID: 61204 RVA: 0x0036689F File Offset: 0x00364A9F
		protected string SiteDomain
		{
			get
			{
				if (this.Context.Items["TelerikSiteDomain"] != null)
				{
					return (string)this.Context.Items["TelerikSiteDomain"];
				}
				return this._siteDomain;
			}
			set
			{
				this._siteDomain = value;
			}
		}

		// Token: 0x17004851 RID: 18513
		// (get) Token: 0x0600EF15 RID: 61205 RVA: 0x003668A8 File Offset: 0x00364AA8
		[NotifyParentProperty(true)]
		[Category("Appearance")]
		[Description("Specifies the custom palettes for chart.")]
		[Browsable(true)]
		[Editor("Telerik.Charting.CustomFiguresCollectionEditor", "System.Drawing.Design.UITypeEditor")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public CustomFiguresCollection CustomFigures
		{
			get
			{
				return this._chart.CustomFigures;
			}
		}

		// Token: 0x17004852 RID: 18514
		// (get) Token: 0x0600EF16 RID: 61206 RVA: 0x003668B5 File Offset: 0x00364AB5
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Chart Chart
		{
			get
			{
				return this._chart;
			}
		}

		// Token: 0x17004853 RID: 18515
		// (get) Token: 0x0600EF17 RID: 61207 RVA: 0x003668BD File Offset: 0x00364ABD
		// (set) Token: 0x0600EF18 RID: 61208 RVA: 0x003668CA File Offset: 0x00364ACA
		[DefaultValue(typeof(ChartSeriesType), "Bar")]
		[Description("Specifies the default type of the data series.")]
		[Category("Appearance")]
		public ChartSeriesType DefaultType
		{
			get
			{
				return this._chart.DefaultType;
			}
			set
			{
				this._chart.DefaultType = value;
			}
		}

		// Token: 0x17004854 RID: 18516
		// (get) Token: 0x0600EF19 RID: 61209 RVA: 0x003668D8 File Offset: 0x00364AD8
		// (set) Token: 0x0600EF1A RID: 61210 RVA: 0x003668E5 File Offset: 0x00364AE5
		[Category("Appearance")]
		[Description("Specifies AutoLayout mode to all items on the chart control.")]
		[DefaultValue(false)]
		public bool AutoLayout
		{
			get
			{
				return this._chart.AutoLayoutWrapper;
			}
			set
			{
				this._chart.AutoLayoutWrapper = value;
			}
		}

		// Token: 0x17004855 RID: 18517
		// (get) Token: 0x0600EF1B RID: 61211 RVA: 0x003668F3 File Offset: 0x00364AF3
		// (set) Token: 0x0600EF1C RID: 61212 RVA: 0x00366900 File Offset: 0x00364B00
		[Description("Specifies AutoTextWrap mode for all text blocks of the chart control.")]
		[Category("Appearance")]
		[DefaultValue(false)]
		public bool AutoTextWrap
		{
			get
			{
				return this._chart.AutoTextWrapWrapper;
			}
			set
			{
				this._chart.AutoTextWrapWrapper = value;
			}
		}

		// Token: 0x17004856 RID: 18518
		// (get) Token: 0x0600EF1D RID: 61213 RVA: 0x0036690E File Offset: 0x00364B0E
		// (set) Token: 0x0600EF1E RID: 61214 RVA: 0x0036691B File Offset: 0x00364B1B
		[Category("Appearance")]
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		[Editor(typeof(ChartPaletteEditor), typeof(UITypeEditor))]
		public string SeriesPalette
		{
			get
			{
				return this._chart.SeriesPaletteWrapper;
			}
			set
			{
				this._chart.SeriesPaletteWrapper = value;
			}
		}

		// Token: 0x17004857 RID: 18519
		// (get) Token: 0x0600EF1F RID: 61215 RVA: 0x00366929 File Offset: 0x00364B29
		[Description("Chart visual settings")]
		[Category("Appearance")]
		[DefaultValue("Appearance")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public StyleChart Appearance
		{
			get
			{
				return this._chart.Appearance;
			}
		}

		// Token: 0x17004858 RID: 18520
		// (get) Token: 0x0600EF20 RID: 61216 RVA: 0x00366936 File Offset: 0x00364B36
		// (set) Token: 0x0600EF21 RID: 61217 RVA: 0x00366943 File Offset: 0x00364B43
		[Description("Specifies the chart's skin")]
		[Category("Appearance")]
		[Editor(typeof(ChartSkinEditor), typeof(UITypeEditor))]
		[DefaultValue("")]
		public new string Skin
		{
			get
			{
				return this._chart.Skin;
			}
			set
			{
				this._chart.Skin = value;
			}
		}

		// Token: 0x17004859 RID: 18521
		// (get) Token: 0x0600EF22 RID: 61218 RVA: 0x00366951 File Offset: 0x00364B51
		// (set) Token: 0x0600EF23 RID: 61219 RVA: 0x0036695E File Offset: 0x00364B5E
		[Category("Appearance")]
		[DefaultValue(false)]
		[NotifyParentProperty(true)]
		[Description("Should skin override user setting or not")]
		public bool SkinsOverrideStyles
		{
			get
			{
				return this._chart.SkinsOverrideStyles;
			}
			set
			{
				this._chart.SkinsOverrideStyles = value;
			}
		}

		// Token: 0x1700485A RID: 18522
		// (get) Token: 0x0600EF24 RID: 61220 RVA: 0x0036696C File Offset: 0x00364B6C
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Category("Data")]
		[Browsable(false)]
		public DataManager DataManager
		{
			get
			{
				return this._chart.DataManager;
			}
		}

		// Token: 0x1700485B RID: 18523
		// (get) Token: 0x0600EF25 RID: 61221 RVA: 0x00366979 File Offset: 0x00364B79
		[NotifyParentProperty(true)]
		[Description("Series collection.")]
		[Browsable(true)]
		[Category("Data")]
		[Editor("Telerik.Charting.SeriesCollectionEditor", "System.Drawing.Design.UITypeEditor")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public ChartSeriesCollection Series
		{
			get
			{
				return this._chart.Series;
			}
		}

		// Token: 0x1700485C RID: 18524
		// (get) Token: 0x0600EF26 RID: 61222 RVA: 0x00366986 File Offset: 0x00364B86
		// (set) Token: 0x0600EF27 RID: 61223 RVA: 0x003669A7 File Offset: 0x00364BA7
		[DefaultValue(typeof(System.Web.UI.WebControls.Unit), "300px")]
		[NotifyParentProperty(true)]
		public override System.Web.UI.WebControls.Unit Height
		{
			get
			{
				return new System.Web.UI.WebControls.Unit(this.Chart.Appearance.Dimensions.Height.ToString());
			}
			set
			{
				base.Height = value;
				if (this._chart != null)
				{
					this._chart.Appearance.Dimensions.Height = new Telerik.Charting.Styles.Unit(value.ToString());
				}
			}
		}

		// Token: 0x1700485D RID: 18525
		// (get) Token: 0x0600EF28 RID: 61224 RVA: 0x003669DF File Offset: 0x00364BDF
		// (set) Token: 0x0600EF29 RID: 61225 RVA: 0x00366A00 File Offset: 0x00364C00
		[NotifyParentProperty(true)]
		[DefaultValue(typeof(System.Web.UI.WebControls.Unit), "400px")]
		public override System.Web.UI.WebControls.Unit Width
		{
			get
			{
				return new System.Web.UI.WebControls.Unit(this.Chart.Appearance.Dimensions.Width.ToString());
			}
			set
			{
				base.Width = value;
				if (this._chart != null)
				{
					this._chart.Appearance.Dimensions.Width = new Telerik.Charting.Styles.Unit(value.ToString());
				}
			}
		}

		// Token: 0x1700485E RID: 18526
		// (get) Token: 0x0600EF2A RID: 61226 RVA: 0x00366A38 File Offset: 0x00364C38
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Description("Legend settings.")]
		[Browsable(true)]
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Category("Elements")]
		public ChartLegend Legend
		{
			get
			{
				return this._chart.Legend;
			}
		}

		// Token: 0x1700485F RID: 18527
		// (get) Token: 0x0600EF2B RID: 61227 RVA: 0x00366A45 File Offset: 0x00364C45
		[Category("Elements")]
		[Description("Plot area settings.")]
		[Browsable(true)]
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public ChartPlotArea PlotArea
		{
			get
			{
				return this._chart.PlotArea;
			}
		}

		// Token: 0x17004860 RID: 18528
		// (get) Token: 0x0600EF2C RID: 61228 RVA: 0x00366A52 File Offset: 0x00364C52
		[NotifyParentProperty(true)]
		[Bindable(false)]
		[Description("The chart title.")]
		[Category("Elements")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Browsable(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public ChartTitle ChartTitle
		{
			get
			{
				return this._chart.ChartTitle;
			}
		}

		// Token: 0x17004861 RID: 18529
		// (get) Token: 0x0600EF2D RID: 61229 RVA: 0x00366A5F File Offset: 0x00364C5F
		// (set) Token: 0x0600EF2E RID: 61230 RVA: 0x00366A67 File Offset: 0x00364C67
		[Category("Behavior")]
		[Description("Enables or disables use of the session.")]
		[Browsable(true)]
		[Bindable(true)]
		[DefaultValue(true)]
		public bool UseSession
		{
			get
			{
				return this._useSession;
			}
			set
			{
				this._useSession = value;
			}
		}

		// Token: 0x17004862 RID: 18530
		// (get) Token: 0x0600EF2F RID: 61231 RVA: 0x00366A70 File Offset: 0x00364C70
		// (set) Token: 0x0600EF30 RID: 61232 RVA: 0x00366AA5 File Offset: 0x00364CA5
		[DefaultValue(true)]
		[Description("Specifies whether image maps for chart elements will be created.")]
		[Browsable(true)]
		[Bindable(true)]
		[Category("Behavior")]
		public bool CreateImageMap
		{
			get
			{
				if (this.ViewState["CreateImageMap"] != null)
				{
					this._createImageMap = (bool)this.ViewState["CreateImageMap"];
				}
				return this._createImageMap;
			}
			set
			{
				this.ViewState["createImageMap"] = value;
				this._createImageMap = value;
			}
		}

		// Token: 0x17004863 RID: 18531
		// (get) Token: 0x0600EF31 RID: 61233 RVA: 0x00366AC4 File Offset: 0x00364CC4
		// (set) Token: 0x0600EF32 RID: 61234 RVA: 0x00366AF4 File Offset: 0x00364CF4
		[Category("Settings")]
		[Description("Specifies the folder for the chart's temp images.")]
		[Browsable(true)]
		[Bindable(true)]
		[DefaultValue("~/Temp")]
		public string TempImagesFolder
		{
			get
			{
				if (this.ViewState["TempImagesFolder"] != null)
				{
					return this.ViewState["TempImagesFolder"] as string;
				}
				return this._tempImagesFolder;
			}
			set
			{
				this.ViewState["TempImagesFolder"] = value;
			}
		}

		// Token: 0x17004864 RID: 18532
		// (get) Token: 0x0600EF33 RID: 61235 RVA: 0x00366B08 File Offset: 0x00364D08
		// (set) Token: 0x0600EF34 RID: 61236 RVA: 0x00366B35 File Offset: 0x00364D35
		[Category("Data")]
		[Description("Specifies an xml content file for the chart.")]
		[Browsable(true)]
		[Bindable(false)]
		[DefaultValue("")]
		public string ContentFile
		{
			get
			{
				string text = (string)this.ViewState["cntfile"];
				return text ?? string.Empty;
			}
			set
			{
				this.ViewState["cntfile"] = value;
			}
		}

		// Token: 0x17004865 RID: 18533
		// (get) Token: 0x0600EF35 RID: 61237 RVA: 0x00366B48 File Offset: 0x00364D48
		// (set) Token: 0x0600EF36 RID: 61238 RVA: 0x00366B7D File Offset: 0x00364D7D
		[Category("Settings")]
		[Description("Specifies the image format in which the image is streamed.")]
		[Browsable(true)]
		[Bindable(true)]
		[DefaultValue(typeof(ImageFormat), "png")]
		public ImageFormat ChartImageFormat
		{
			get
			{
				if (this.ViewState["ImageFormat"] != null)
				{
					return (ImageFormat)this.ViewState["ImageFormat"];
				}
				return this._chart.ImageFormat;
			}
			set
			{
				this.ViewState["ImageFormat"] = value;
				this._chart.ImageFormat = value;
			}
		}

		// Token: 0x17004866 RID: 18534
		// (get) Token: 0x0600EF37 RID: 61239 RVA: 0x00366B9C File Offset: 0x00364D9C
		// (set) Token: 0x0600EF38 RID: 61240 RVA: 0x00366BCC File Offset: 0x00364DCC
		[Category("Appearance")]
		[DefaultValue("")]
		[Description("The alternate text displayed when the image cannot be shown.")]
		[Browsable(true)]
		[Bindable(true)]
		public string AlternateText
		{
			get
			{
				if (this.ViewState["AternateText"] != null)
				{
					return (string)this.ViewState["AternateText"];
				}
				return this._alternateText;
			}
			set
			{
				this.ViewState["AternateText"] = value;
				this._alternateText = value;
			}
		}

		// Token: 0x17004867 RID: 18535
		// (get) Token: 0x0600EF39 RID: 61241 RVA: 0x00366BE6 File Offset: 0x00364DE6
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Category("Appearance")]
		[Description("Specifies the custom palettes for chart.")]
		[Browsable(true)]
		[NotifyParentProperty(true)]
		[Editor("Telerik.Charting.CustomPaletteCollectionEditor", "System.Drawing.Design.UITypeEditor")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public CustomPalettesCollection CustomPalettes
		{
			get
			{
				return this._chart.CustomPalettes;
			}
		}

		// Token: 0x17004868 RID: 18536
		// (get) Token: 0x0600EF3A RID: 61242 RVA: 0x00366BF3 File Offset: 0x00364DF3
		// (set) Token: 0x0600EF3B RID: 61243 RVA: 0x00366C00 File Offset: 0x00364E00
		[Category("Appearance")]
		[DefaultValue(typeof(ChartSeriesOrientation), "Vertical")]
		[Description("Specifies the orientation of the chart series on the plot area.")]
		[Browsable(true)]
		[Bindable(true)]
		public ChartSeriesOrientation SeriesOrientation
		{
			get
			{
				return this._chart.SeriesOrientation;
			}
			set
			{
				this._chart.SeriesOrientation = value;
			}
		}

		// Token: 0x17004869 RID: 18537
		// (get) Token: 0x0600EF3C RID: 61244 RVA: 0x00366C0E File Offset: 0x00364E0E
		// (set) Token: 0x0600EF3D RID: 61245 RVA: 0x00366C1B File Offset: 0x00364E1B
		[Description("Enables / disables Intelligent labels logic for series items labels in all plot areas")]
		[Category("Appearance")]
		[DefaultValue(typeof(bool), "false")]
		[Browsable(true)]
		[Bindable(true)]
		public bool IntelligentLabelsEnabled
		{
			get
			{
				return this._chart.IntelligentLabelsEnabled;
			}
			set
			{
				this._chart.IntelligentLabelsEnabled = value;
			}
		}

		// Token: 0x1700486A RID: 18538
		// (get) Token: 0x0600EF3E RID: 61246 RVA: 0x00366C29 File Offset: 0x00364E29
		internal MapAreaBuilder MapAreaBuilder
		{
			get
			{
				if (!this.CreateImageMap)
				{
					return null;
				}
				if (this._mapAreaBuilder == null)
				{
					this._mapAreaBuilder = new MapAreaBuilder(this);
				}
				return this._mapAreaBuilder;
			}
		}

		// Token: 0x1700486B RID: 18539
		// (get) Token: 0x0600EF3F RID: 61247 RVA: 0x00366C4F File Offset: 0x00364E4F
		[Category("Behavior")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Browsable(true)]
		[Description("RadChart client properties.")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public ChartClientSettings ClientSettings
		{
			get
			{
				return this._chartClientSettings;
			}
		}

		// Token: 0x0600EF40 RID: 61248 RVA: 0x00366C58 File Offset: 0x00364E58
		internal ISite GetSite()
		{
			if (base.Site != null)
			{
				return base.Site;
			}
			for (Control parent = this.Parent; parent != null; parent = parent.Parent)
			{
				if (parent.Site != null)
				{
					return parent.Site;
				}
			}
			return null;
		}

		// Token: 0x0600EF41 RID: 61249 RVA: 0x00366C97 File Offset: 0x00364E97
		[SuppressMessage("Microsoft.Security", "CA2116:AptcaMethodsShouldOnlyCallAptcaMethods")]
		public string ResolvePhysicalLocation(string path)
		{
			return this._chart.ResolvePhysicalLocation(path);
		}

		// Token: 0x0600EF42 RID: 61250 RVA: 0x00366CA5 File Offset: 0x00364EA5
		[Description("Adds a new series to the chart series collection.")]
		public void AddChartSeries(ChartSeries chartSeries)
		{
			this.Series.Add(chartSeries);
		}

		// Token: 0x0600EF43 RID: 61251 RVA: 0x00366CB3 File Offset: 0x00364EB3
		[SuppressMessage("Microsoft.Design", "CA1063:ImplementIDisposableCorrectly")]
		public override void Dispose()
		{
			base.Dispose();
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600EF44 RID: 61252 RVA: 0x00366CC8 File Offset: 0x00364EC8
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this._chart != null)
				{
					this._chart.Dispose();
				}
				if (this._mapAreaBuilder != null)
				{
					this._mapAreaBuilder = null;
				}
				if (this._chartClientSettings != null)
				{
					this._chartClientSettings.Dispose();
				}
			}
		}

		// Token: 0x140001C5 RID: 453
		// (add) Token: 0x0600EF45 RID: 61253 RVA: 0x00366D04 File Offset: 0x00364F04
		// (remove) Token: 0x0600EF46 RID: 61254 RVA: 0x00366D3C File Offset: 0x00364F3C
		public event RadChart.ChartClickEventHandler Click;

		// Token: 0x0600EF47 RID: 61255 RVA: 0x00366D71 File Offset: 0x00364F71
		internal bool IsClickable()
		{
			return this.Click != null;
		}

		// Token: 0x140001C6 RID: 454
		// (add) Token: 0x0600EF48 RID: 61256 RVA: 0x00366D80 File Offset: 0x00364F80
		// (remove) Token: 0x0600EF49 RID: 61257 RVA: 0x00366DB8 File Offset: 0x00364FB8
		public event EventHandler<EventArgs> BeforeLayout;

		// Token: 0x140001C7 RID: 455
		// (add) Token: 0x0600EF4A RID: 61258 RVA: 0x00366DF0 File Offset: 0x00364FF0
		// (remove) Token: 0x0600EF4B RID: 61259 RVA: 0x00366E28 File Offset: 0x00365028
		public event EventHandler<EventArgs> PrePaint;

		// Token: 0x140001C8 RID: 456
		// (add) Token: 0x0600EF4C RID: 61260 RVA: 0x00366E60 File Offset: 0x00365060
		// (remove) Token: 0x0600EF4D RID: 61261 RVA: 0x00366E98 File Offset: 0x00365098
		public event EventHandler<ChartItemDataBoundEventArgs> ItemDataBound;

		// Token: 0x140001C9 RID: 457
		// (add) Token: 0x0600EF4E RID: 61262 RVA: 0x00366ED0 File Offset: 0x003650D0
		// (remove) Token: 0x0600EF4F RID: 61263 RVA: 0x00366F08 File Offset: 0x00365108
		public event EventHandler<ChartZoomEventArgs> Zoom;

		// Token: 0x0600EF50 RID: 61264 RVA: 0x00366F40 File Offset: 0x00365140
		public void RaisePostBackEvent(string eventArg)
		{
			string[] array = eventArg.Split(new char[]
			{
				','
			});
			if (array[0] == "zoom")
			{
				return;
			}
			if (array[0] == "zoomout")
			{
				this.OnZoomOut();
				return;
			}
			Telerik.Charting.IContainer container = this._chart;
			IOrdering ordering = null;
			bool flag = bool.Parse(array[0]);
			if (flag)
			{
				int num = int.Parse(array[1]);
				int num2 = int.Parse(array[2]);
				ChartSeries chartSeries = (num < 0) ? null : this._chart.Series[num];
				ChartSeriesItem chartSeriesItem = (chartSeries == null) ? null : ((num2 < 0) ? null : chartSeries[num2]);
				if (chartSeriesItem != null)
				{
					if (this.Click != null)
					{
						this.Click(this, new ChartClickEventArgs(chartSeriesItem, chartSeries, chartSeriesItem));
					}
					if (chartSeriesItem != null && chartSeriesItem.ActiveRegion.HasClickEvent())
					{
						chartSeriesItem.ActiveRegion.OnClick();
						return;
					}
				}
			}
			else
			{
				for (int i = 1; i < array.Length; i++)
				{
					int index;
					if (int.TryParse(array[i], out index))
					{
						ordering = container.OrderList[index];
						Telerik.Charting.IContainer container2 = ordering as Telerik.Charting.IContainer;
						if (container2 != null)
						{
							container = container2;
						}
					}
				}
				IActiveRegion activeRegion = ordering as IActiveRegion;
				if (activeRegion != null)
				{
					BindableLegendItem bindableLegendItem = activeRegion as BindableLegendItem;
					if (bindableLegendItem != null)
					{
						BindableLegendItem bindableLegendItem2 = bindableLegendItem;
						if (bindableLegendItem2.BindableLegendItemSource is ChartSeriesItem)
						{
							ChartSeriesItem chartSeriesItem2 = (ChartSeriesItem)bindableLegendItem2.BindableLegendItemSource;
							if (this.Click != null)
							{
								this.Click(this, new ChartClickEventArgs(activeRegion, chartSeriesItem2.Parent, chartSeriesItem2));
							}
							if (bindableLegendItem2.ActiveRegion.HasClickEvent())
							{
								bindableLegendItem2.ActiveRegion.OnClick(chartSeriesItem2);
								return;
							}
						}
						else
						{
							ChartSeries chartSeries2 = bindableLegendItem2.BindableLegendItemSource as ChartSeries;
							if (this.Click != null)
							{
								this.Click(this, new ChartClickEventArgs(activeRegion, chartSeries2));
							}
							if (activeRegion.ActiveRegion.HasClickEvent())
							{
								bindableLegendItem2.ActiveRegion.OnClick(chartSeries2);
								return;
							}
						}
					}
					else
					{
						if (this.Click != null)
						{
							this.Click(this, new ChartClickEventArgs(activeRegion));
						}
						if (activeRegion.ActiveRegion.HasClickEvent())
						{
							activeRegion.ActiveRegion.OnClick();
						}
					}
				}
			}
		}

		// Token: 0x1700486C RID: 18540
		// (get) Token: 0x0600EF51 RID: 61265 RVA: 0x00367173 File Offset: 0x00365373
		// (set) Token: 0x0600EF52 RID: 61266 RVA: 0x003671B1 File Offset: 0x003653B1
		private Stack<ZoomInfo> ZoomHistory
		{
			get
			{
				if (this.ViewState["ZoomHistory"] == null)
				{
					this.ViewState["ZoomHistory"] = new Stack<ZoomInfo>();
				}
				return (Stack<ZoomInfo>)this.ViewState["ZoomHistory"];
			}
			set
			{
				this.ViewState["ZoomHistory"] = value;
			}
		}

		// Token: 0x0600EF53 RID: 61267 RVA: 0x003671C4 File Offset: 0x003653C4
		private void OnZoomOut()
		{
			ZoomInfo zoomInfo = new ZoomInfo();
			double xScaleOld = (double)this.ClientSettings.XScale;
			double yScaleOld = (double)this.ClientSettings.YScale;
			if (this.ZoomHistory.Count > 0)
			{
				zoomInfo = this.ZoomHistory.Pop();
			}
			this.ClientSettings.XScale = zoomInfo.XScale;
			this.ClientSettings.YScale = zoomInfo.YScale;
			this.ClientSettings.YScrollOffset = zoomInfo.YScrollOffset;
			this.ClientSettings.XScrollOffset = zoomInfo.XScrollOffset;
			this.OnZoom(new ChartZoomEventArgs(xScaleOld, (double)zoomInfo.XScale, yScaleOld, (double)zoomInfo.YScale));
		}

		// Token: 0x0600EF54 RID: 61268 RVA: 0x0036726C File Offset: 0x0036546C
		private void OnZoomIn(string[] arguments)
		{
			ZoomInfo zoomInfo = new ZoomInfo();
			zoomInfo.XScale = float.Parse(arguments[5], CultureInfo.InvariantCulture);
			zoomInfo.YScale = float.Parse(arguments[6], CultureInfo.InvariantCulture);
			zoomInfo.XScrollOffset = this.ClientSettings.XScrollOffset;
			zoomInfo.YScrollOffset = this.ClientSettings.YScrollOffset;
			this.ZoomHistory.Push(zoomInfo);
			if (this.SeriesOrientation == ChartSeriesOrientation.Vertical)
			{
				this.ClientSettings.XScale = float.Parse(arguments[3], CultureInfo.InvariantCulture);
				this.ClientSettings.YScale = float.Parse(arguments[4], CultureInfo.InvariantCulture);
				this.ClientSettings.YScrollOffset = float.Parse(arguments[2], CultureInfo.InvariantCulture);
				this.ClientSettings.XScrollOffset = float.Parse(arguments[1], CultureInfo.InvariantCulture);
			}
			else
			{
				this.ClientSettings.XScale = float.Parse(arguments[4], CultureInfo.InvariantCulture);
				this.ClientSettings.YScale = float.Parse(arguments[3], CultureInfo.InvariantCulture);
				this.ClientSettings.YScrollOffset = float.Parse(arguments[1], CultureInfo.InvariantCulture);
				this.ClientSettings.XScrollOffset = float.Parse(arguments[2], CultureInfo.InvariantCulture);
			}
			this.OnZoom(new ChartZoomEventArgs((double)zoomInfo.XScale, (double)this.ClientSettings.XScale, (double)zoomInfo.YScale, (double)this.ClientSettings.YScale));
		}

		// Token: 0x0600EF55 RID: 61269 RVA: 0x003673D0 File Offset: 0x003655D0
		public RadChart()
		{
			this._chartClientSettings = new ChartClientSettings();
			this._alternateText = string.Empty;
			this._createImageMap = true;
			this._applicationPath = string.Empty;
			this._useSession = true;
			this._siteDomain = "www.telerik.com";
			this._tempImagesFolder = "~/Temp";
			this._chart = new Chart(this);
			this.DataManager.ItemDataBound += this.DataManager_ItemDataBound;
			this.Chart.BeforeLayoutEventHandler += this.Chart_BeforeLayout;
			this.Chart.PrePaintEventHandler += this.Chart_PrePaint;
			this._mapAreaBuilder = new MapAreaBuilder(this);
			this.Height = 300;
			this.Width = 400;
		}

		// Token: 0x0600EF56 RID: 61270 RVA: 0x003674A5 File Offset: 0x003656A5
		private void Chart_BeforeLayout(object sender, EventArgs args)
		{
			this.OnBeforeLayout(args);
		}

		// Token: 0x0600EF57 RID: 61271 RVA: 0x003674AE File Offset: 0x003656AE
		private void Chart_PrePaint(object sender, EventArgs args)
		{
			this.OnPrePaint(args);
		}

		// Token: 0x0600EF58 RID: 61272 RVA: 0x003674B7 File Offset: 0x003656B7
		private void DataManager_ItemDataBound(object sender, ChartItemDataBoundEventArgs args)
		{
			this.OnItemDataBound(args);
		}

		// Token: 0x0600EF59 RID: 61273 RVA: 0x003674C0 File Offset: 0x003656C0
		protected virtual void OnBeforeLayout(EventArgs args)
		{
			if (this.BeforeLayout != null)
			{
				this.BeforeLayout(this, args);
			}
		}

		// Token: 0x0600EF5A RID: 61274 RVA: 0x003674D7 File Offset: 0x003656D7
		protected virtual void OnPrePaint(EventArgs args)
		{
			if (this.PrePaint != null)
			{
				this.PrePaint(this, args);
			}
		}

		// Token: 0x0600EF5B RID: 61275 RVA: 0x003674EE File Offset: 0x003656EE
		protected virtual void OnItemDataBound(ChartItemDataBoundEventArgs args)
		{
			if (this.ItemDataBound != null)
			{
				this.ItemDataBound(this, args);
			}
		}

		// Token: 0x0600EF5C RID: 61276 RVA: 0x00367505 File Offset: 0x00365705
		protected virtual void OnClick(ChartClickEventArgs args)
		{
			if (this.Click != null)
			{
				this.Click(this, args);
			}
		}

		// Token: 0x0600EF5D RID: 61277 RVA: 0x0036751C File Offset: 0x0036571C
		protected virtual void OnZoom(ChartZoomEventArgs args)
		{
			if (this.Zoom != null)
			{
				this.Zoom(this, args);
			}
		}

		// Token: 0x1700486D RID: 18541
		// (get) Token: 0x0600EF5E RID: 61278 RVA: 0x00367533 File Offset: 0x00365733
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Div;
			}
		}

		// Token: 0x0600EF5F RID: 61279 RVA: 0x00367538 File Offset: 0x00365738
		private bool CheckHandler()
		{
			if (!this.EnableHandlerDetection || this.handlerChecked)
			{
				return true;
			}
			try
			{
				Configuration configuration = WebConfigurationManager.OpenWebConfiguration("~/Web.Config");
				ConfigurationSectionGroup configurationSectionGroup = configuration.SectionGroups["system.web"];
				HttpHandlersSection httpHandlersSection = (HttpHandlersSection)configurationSectionGroup.Sections["httpHandlers"];
				HttpHandlerAction action = new HttpHandlerAction(ChartHttpHandler.Path, typeof(ChartHttpHandler).AssemblyQualifiedName, "*", false);
				this.handlerChecked = (httpHandlersSection.Handlers.IndexOf(action) != -1);
			}
			catch
			{
				this.handlerChecked = true;
			}
			return this.handlerChecked;
		}

		// Token: 0x1700486E RID: 18542
		// (get) Token: 0x0600EF60 RID: 61280 RVA: 0x003675E4 File Offset: 0x003657E4
		private bool HasClient
		{
			get
			{
				return this.ClientSettings.ScrollMode != ChartClientScrollMode.None && this.CheckHandler();
			}
		}

		// Token: 0x0600EF61 RID: 61281 RVA: 0x003675FC File Offset: 0x003657FC
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			if (this.HasClient)
			{
				ScriptObjectBuilder.RegisterCssReferences(this);
			}
		}

		// Token: 0x0600EF62 RID: 61282 RVA: 0x00367614 File Offset: 0x00365814
		protected override void DescribeComponent(IScriptDescriptor descriptor)
		{
			base.DescribeComponent(descriptor);
			ChartClientSettings chartClientSettings = null;
			if (this.SeriesOrientation == ChartSeriesOrientation.Horizontal)
			{
				chartClientSettings = (this.ClientSettings.Clone() as ChartClientSettings);
				if (chartClientSettings.ScrollMode == ChartClientScrollMode.XOnly)
				{
					chartClientSettings.ScrollMode = ChartClientScrollMode.YOnly;
				}
				else if (chartClientSettings.ScrollMode == ChartClientScrollMode.YOnly)
				{
					chartClientSettings.ScrollMode = ChartClientScrollMode.XOnly;
				}
				descriptor.AddProperty("_axesSwapped", true);
				float xscrollOffset = chartClientSettings.XScrollOffset;
				chartClientSettings.XScrollOffset = chartClientSettings.YScrollOffset;
				chartClientSettings.YScrollOffset = xscrollOffset;
			}
			descriptor.AddProperty("_uniqueId", this.UniqueID);
			descriptor.AddProperty("_seriesOrientation", this.SeriesOrientation);
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			javaScriptSerializer.RegisterConverters(new JavaScriptConverter[]
			{
				new JavaScriptColorConverter()
			});
			string script = javaScriptSerializer.Serialize(chartClientSettings ?? this.ClientSettings);
			string script2 = javaScriptSerializer.Serialize(this.ZoomHistory);
			descriptor.AddScriptProperty("_clientSettings", script);
			descriptor.AddScriptProperty("_zoomHistory", script2);
			this.Page.ClientScript.GetCallbackEventReference(this, null, null, null);
			this.Page.ClientScript.GetPostBackEventReference(this, null);
		}

		// Token: 0x0600EF63 RID: 61283 RVA: 0x00367738 File Offset: 0x00365938
		protected override void RenderContents(HtmlTextWriter writer)
		{
			if (base.DesignMode && base.Site == null)
			{
				return;
			}
			if (!this.CheckHandler())
			{
				string arg = string.Format("The Chart http handler is not registered. Please, manually add the following line to your <b>Web.config</b> httpHandlers section: <br/><b>&lt;add path=\"{0}\" verb=\"*\" type=\"{1}\" validate=\"false\" /&gt</b>", ChartHttpHandler.Path, typeof(ChartHttpHandler).AssemblyQualifiedName);
				writer.Write(string.Format("<div style='border:1px solid red;width:{1};height:{2}'>{0}</div>", arg, this.Width, this.Height));
				return;
			}
			if (!this.HasClient)
			{
				this.RenderClassic(writer);
				return;
			}
			this.RenderWithClient(writer);
		}

		// Token: 0x0600EF64 RID: 61284 RVA: 0x003677BC File Offset: 0x003659BC
		private void RenderAxisWithClient(HtmlTextWriter writer, ChartAxisType axisType, RectangleF axisRect, SizeF wrapperSize, string cssClassName, string clientID)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, cssClassName);
			this.AddSizeStyleAttributes(writer, wrapperSize.Width, wrapperSize.Height);
			this.AddPositionStyleAttributes(writer, Telerik.Charting.Styles.Unit.Pixel(axisRect.Top), Telerik.Charting.Styles.Unit.Pixel(axisRect.Left));
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			writer.AddAttribute(HtmlTextWriterAttribute.Id, clientID);
			this.AddSizeStyleAttributes(writer, Telerik.Charting.Styles.Unit.Pixel(axisRect.Width), Telerik.Charting.Styles.Unit.Pixel(axisRect.Height));
			this.RenderBrowserSpecificBackgroundImage(writer, "url({0}) no-repeat", this.GetAxisImagePath(axisType));
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			writer.RenderEndTag();
			writer.RenderEndTag();
		}

		// Token: 0x0600EF65 RID: 61285 RVA: 0x0036786C File Offset: 0x00365A6C
		private void RenderWithClient(HtmlTextWriter writer)
		{
			bool flag = this.SeriesOrientation == ChartSeriesOrientation.Vertical;
			this.AddIE6BackgroundTransparency(writer);
			RectangleF visiblePlotAreaRect = default(RectangleF);
			if (this.AutoLayout)
			{
				this.PlotArea.Appearance.RestoreAutoLayoutMargins();
			}
			visiblePlotAreaRect.Width = (float)Math.Round((double)this.PlotArea.Appearance.Dimensions.Width.PixelValue);
			visiblePlotAreaRect.Height = (float)Math.Round((double)this.PlotArea.Appearance.Dimensions.Height.PixelValue);
			visiblePlotAreaRect.Y = (float)Math.Round((double)this.PlotArea.Appearance.Dimensions.Margins.Top.PixelValue);
			visiblePlotAreaRect.X = (float)Math.Round((double)this.PlotArea.Appearance.Dimensions.Margins.Left.PixelValue);
			SizeF virtualPlotAreaSize = default(SizeF);
			virtualPlotAreaSize.Width = (float)Math.Round((double)(this.PlotArea.Appearance.Dimensions.Width.PixelValue * this.GetEffectiveXScale()));
			virtualPlotAreaSize.Height = (float)Math.Round((double)(this.PlotArea.Appearance.Dimensions.Height.PixelValue * this.GetEffectiveYScale()));
			float num = 0f;
			float num2 = 0f;
			if (this.PlotArea.XAxis.LayoutMode == ChartAxisLayoutMode.Normal)
			{
				num = this.PlotArea.XAxis.GetFirstItemHalfDimension();
				num2 = this.PlotArea.XAxis.GetLastItemHalfDimension();
			}
			PointF startPoint;
			PointF endPoint;
			if (flag)
			{
				startPoint = new PointF(visiblePlotAreaRect.X, visiblePlotAreaRect.Y + visiblePlotAreaRect.Height);
				endPoint = new PointF(visiblePlotAreaRect.X + virtualPlotAreaSize.Width + this.PlotArea.YAxis.Appearance.Width, visiblePlotAreaRect.Y + visiblePlotAreaRect.Height);
			}
			else
			{
				startPoint = new PointF(visiblePlotAreaRect.X, visiblePlotAreaRect.Y + virtualPlotAreaSize.Height);
				endPoint = new PointF(visiblePlotAreaRect.X, visiblePlotAreaRect.Y);
			}
			RectangleF clientRectangle = this.PlotArea.XAxis.GetClientRectangle(startPoint, endPoint);
			string cssClassName = "rchAxisX";
			string format = "{0}_xAxis";
			SizeF wrapperSize;
			if (flag)
			{
				wrapperSize = new SizeF(visiblePlotAreaRect.Width + num + num2, clientRectangle.Height);
			}
			else
			{
				wrapperSize = new SizeF(clientRectangle.Width, visiblePlotAreaRect.Height + num + num2);
				cssClassName = "rchAxisY";
				format = "{0}_yAxis";
			}
			this.RenderAxisWithClient(writer, ChartAxisType.XAxis, clientRectangle, wrapperSize, cssClassName, string.Format(format, this.ClientID));
			num = this.PlotArea.YAxis.GetFirstItemHalfDimension();
			num2 = this.PlotArea.YAxis.GetLastItemHalfDimension();
			if (flag)
			{
				startPoint = new PointF(visiblePlotAreaRect.X, visiblePlotAreaRect.Y + virtualPlotAreaSize.Height);
				endPoint = new PointF(visiblePlotAreaRect.X, visiblePlotAreaRect.Y);
			}
			else
			{
				startPoint = new PointF(visiblePlotAreaRect.X, visiblePlotAreaRect.Y + visiblePlotAreaRect.Height);
				endPoint = new PointF(visiblePlotAreaRect.X + virtualPlotAreaSize.Width, visiblePlotAreaRect.Y + visiblePlotAreaRect.Height);
			}
			clientRectangle = this.PlotArea.YAxis.GetClientRectangle(startPoint, endPoint);
			if (flag)
			{
				wrapperSize = new SizeF(clientRectangle.Width, visiblePlotAreaRect.Height + num + num2);
				cssClassName = "rchAxisY";
				format = "{0}_yAxis";
			}
			else
			{
				wrapperSize = new SizeF(visiblePlotAreaRect.Width + num + num2, clientRectangle.Height);
				cssClassName = "rchAxisX";
				format = "{0}_xAxis";
			}
			this.RenderAxisWithClient(writer, ChartAxisType.YAxis, clientRectangle, wrapperSize, cssClassName, string.Format(format, this.ClientID));
			if (this.PlotArea.YAxis2.IsVisible())
			{
				num = this.PlotArea.YAxis2.GetFirstItemHalfDimension();
				num2 = this.PlotArea.YAxis2.GetLastItemHalfDimension();
				if (flag)
				{
					startPoint = new PointF(visiblePlotAreaRect.X + visiblePlotAreaRect.Width, visiblePlotAreaRect.Y + virtualPlotAreaSize.Height);
					endPoint = new PointF(visiblePlotAreaRect.X + visiblePlotAreaRect.Width, visiblePlotAreaRect.Y);
				}
				else
				{
					startPoint = new PointF(visiblePlotAreaRect.X, visiblePlotAreaRect.Y);
					endPoint = new PointF(visiblePlotAreaRect.X + virtualPlotAreaSize.Width, visiblePlotAreaRect.Y);
				}
				clientRectangle = this.PlotArea.YAxis2.GetClientRectangle(startPoint, endPoint);
				if (flag)
				{
					wrapperSize = new SizeF(clientRectangle.Width, visiblePlotAreaRect.Height + num + num2);
				}
				else
				{
					wrapperSize = new SizeF(visiblePlotAreaRect.Width + num + num2, clientRectangle.Height);
				}
				this.RenderAxisWithClient(writer, ChartAxisType.YAxis2, clientRectangle, wrapperSize, "rchAxisY2", string.Format("{0}_yAxis2", this.ClientID));
			}
			this.RenderPlotArea(writer, visiblePlotAreaRect, virtualPlotAreaSize);
			if (this.AutoLayout)
			{
				this.PlotArea.Appearance.RestoreDimensions(true);
			}
		}

		// Token: 0x0600EF66 RID: 61286 RVA: 0x00367D78 File Offset: 0x00365F78
		private void RenderPlotArea(HtmlTextWriter writer, RectangleF visiblePlotAreaRect, SizeF virtualPlotAreaSize)
		{
			bool flag = this.SeriesOrientation == ChartSeriesOrientation.Vertical;
			float num2;
			float num = num2 = 0f;
			float num3;
			if (flag)
			{
				num3 = (float)((int)Math.Ceiling((double)(this.PlotArea.YAxis.Appearance.Width / 2f)));
				if (this.PlotArea.YAxis2.IsVisible())
				{
					num2 = this.PlotArea.YAxis2.Appearance.Width;
				}
			}
			else
			{
				num3 = (float)((int)Math.Ceiling((double)(this.PlotArea.XAxis.Appearance.Width / 2f)));
				if (this.PlotArea.YAxis2.IsVisible())
				{
					num = this.PlotArea.YAxis2.Appearance.Width;
				}
			}
			visiblePlotAreaRect.Y += num;
			visiblePlotAreaRect.X += num3;
			if (this.GetEffectiveXScale() > 1f)
			{
				visiblePlotAreaRect.Width -= num3;
				virtualPlotAreaSize.Width -= num3 + num2;
			}
			if (this.GetEffectiveYScale() > 1f)
			{
				visiblePlotAreaRect.Height -= num;
				virtualPlotAreaSize.Height -= num;
			}
			if (!flag)
			{
				ChartClientScrollMode scrollMode = this.ClientSettings.ScrollMode;
				if (this.ClientSettings.ScrollMode == ChartClientScrollMode.XOnly)
				{
					this.ClientSettings.ScrollMode = ChartClientScrollMode.YOnly;
				}
				else if (this.ClientSettings.ScrollMode == ChartClientScrollMode.YOnly)
				{
					this.ClientSettings.ScrollMode = ChartClientScrollMode.XOnly;
				}
				writer.AddAttribute(HtmlTextWriterAttribute.Class, string.Format("rchPlotArea rch{0}", this.ClientSettings.ScrollMode.ToString()));
				this.ClientSettings.ScrollMode = scrollMode;
			}
			else
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Class, string.Format("rchPlotArea rch{0}", this.ClientSettings.ScrollMode.ToString()));
			}
			writer.AddAttribute(HtmlTextWriterAttribute.Id, string.Format("{0}_rcPlotArea", this.ClientID));
			this.AddSizeStyleAttributes(writer, Telerik.Charting.Styles.Unit.Pixel(visiblePlotAreaRect.Width), Telerik.Charting.Styles.Unit.Pixel(visiblePlotAreaRect.Height));
			this.AddPositionStyleAttributes(writer, Telerik.Charting.Styles.Unit.Pixel(visiblePlotAreaRect.Y), Telerik.Charting.Styles.Unit.Pixel(visiblePlotAreaRect.X));
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			writer.AddAttribute(HtmlTextWriterAttribute.Id, string.Format("{0}_rchImgWrap", this.ClientID));
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rchImgWrap");
			this.AddSizeStyleAttributes(writer, Telerik.Charting.Styles.Unit.Pixel(virtualPlotAreaSize.Width), Telerik.Charting.Styles.Unit.Pixel(virtualPlotAreaSize.Height));
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			this.RenderChunks(writer, visiblePlotAreaRect, virtualPlotAreaSize);
			writer.RenderEndTag();
			writer.RenderEndTag();
		}

		// Token: 0x0600EF67 RID: 61287 RVA: 0x0036800C File Offset: 0x0036620C
		private void AddIE6BackgroundTransparency(HtmlTextWriter writer)
		{
			if (this.Page.Request.Browser.Type == "IE6")
			{
				this.RenderBrowserSpecificBackgroundImage(writer, "url({0}) no-repeat", this.GetBackgroundImagePath());
				writer.AddStyleAttribute(HtmlTextWriterStyle.Top, "0px");
				writer.AddStyleAttribute(HtmlTextWriterStyle.Left, "0px");
				writer.AddStyleAttribute(HtmlTextWriterStyle.Width, this.Width.ToString());
				writer.AddStyleAttribute(HtmlTextWriterStyle.Height, this.Height.ToString());
				writer.AddAttribute(HtmlTextWriterAttribute.Class, "rchBackground");
				writer.RenderBeginTag(HtmlTextWriterTag.Div);
				writer.RenderEndTag();
			}
		}

		// Token: 0x0600EF68 RID: 61288 RVA: 0x003680B9 File Offset: 0x003662B9
		private void AddSizeStyleAttributes(HtmlTextWriter writer, Telerik.Charting.Styles.Unit width, Telerik.Charting.Styles.Unit height)
		{
			writer.AddStyleAttribute(HtmlTextWriterStyle.Width, width.ToString());
			writer.AddStyleAttribute(HtmlTextWriterStyle.Height, height.ToString());
		}

		// Token: 0x0600EF69 RID: 61289 RVA: 0x003680D7 File Offset: 0x003662D7
		private void AddPositionStyleAttributes(HtmlTextWriter writer, Telerik.Charting.Styles.Unit top, Telerik.Charting.Styles.Unit left)
		{
			writer.AddStyleAttribute(HtmlTextWriterStyle.Top, top.ToString());
			writer.AddStyleAttribute(HtmlTextWriterStyle.Left, left.ToString());
		}

		// Token: 0x0600EF6A RID: 61290 RVA: 0x003680F5 File Offset: 0x003662F5
		private float GetEffectiveXScale()
		{
			if (this.SeriesOrientation == ChartSeriesOrientation.Vertical)
			{
				return this.ClientSettings.XScale;
			}
			return this.ClientSettings.YScale;
		}

		// Token: 0x0600EF6B RID: 61291 RVA: 0x00368116 File Offset: 0x00366316
		private float GetEffectiveYScale()
		{
			if (this.SeriesOrientation == ChartSeriesOrientation.Vertical)
			{
				return this.ClientSettings.YScale;
			}
			return this.ClientSettings.XScale;
		}

		// Token: 0x0600EF6C RID: 61292 RVA: 0x00368138 File Offset: 0x00366338
		private void RenderChunks(HtmlTextWriter writer, RectangleF visiblePlotAreaRect, SizeF virtualPlotAreaSize)
		{
			if (this.ClientSettings.ScrollMode == ChartClientScrollMode.None)
			{
				return;
			}
			int num = (int)Math.Ceiling((double)(virtualPlotAreaSize.Width / visiblePlotAreaRect.Width));
			int num2 = (int)Math.Ceiling((double)(virtualPlotAreaSize.Height / visiblePlotAreaRect.Height));
			for (int i = 0; i < num; i++)
			{
				for (int j = 0; j < num2; j++)
				{
					this.RenderChunk(writer, visiblePlotAreaRect, virtualPlotAreaSize, i, j);
				}
			}
		}

		// Token: 0x0600EF6D RID: 61293 RVA: 0x003681A8 File Offset: 0x003663A8
		private void RenderChunk(HtmlTextWriter writer, RectangleF visiblePlotAreaRect, SizeF virtualPlotAreaSize, int positionX, int positionY)
		{
			float num = (float)positionX * visiblePlotAreaRect.Width;
			float num2 = (float)positionY * visiblePlotAreaRect.Height;
			float n = Math.Min(visiblePlotAreaRect.Width, virtualPlotAreaSize.Width - num);
			float n2 = Math.Min(visiblePlotAreaRect.Height, virtualPlotAreaSize.Height - num2);
			writer.AddAttribute(HtmlTextWriterAttribute.Id, string.Format("{0}_c_{1}_{2}", this.ClientID, positionY, positionX));
			this.AddSizeStyleAttributes(writer, Telerik.Charting.Styles.Unit.Pixel(n), Telerik.Charting.Styles.Unit.Pixel(n2));
			this.AddPositionStyleAttributes(writer, Telerik.Charting.Styles.Unit.Pixel(num2), Telerik.Charting.Styles.Unit.Pixel(num));
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			writer.RenderEndTag();
		}

		// Token: 0x0600EF6E RID: 61294 RVA: 0x00368254 File Offset: 0x00366454
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			base.AddAttributesToRender(writer);
			if (this.HasClient && this.Page.Request.Browser.Type != "IE6")
			{
				writer.AddStyleAttribute("background", string.Format("url({0}) no-repeat", this.GetBackgroundImagePath()));
			}
		}

		// Token: 0x0600EF6F RID: 61295 RVA: 0x003682AC File Offset: 0x003664AC
		private void RenderBrowserSpecificBackgroundImage(HtmlTextWriter writer, string value, string imageURL)
		{
			if (this.Page.Request.Browser.Type != "IE6")
			{
				writer.AddStyleAttribute("background", string.Format(value, imageURL));
				return;
			}
			writer.AddStyleAttribute("filter", string.Format("progid:DXImageTransform.Microsoft.AlphaImageLoader(src='{0}',sizingMethod='crop');", imageURL));
		}

		// Token: 0x0600EF70 RID: 61296 RVA: 0x00368304 File Offset: 0x00366504
		private string GetImagePath(System.Drawing.Image img)
		{
			if (img == null)
			{
				return string.Empty;
			}
			this._chart.ApplicationPath = this.applicationPath;
			string path = string.Empty;
			if (this.ChartImageFormat == ImageFormat.Emf)
			{
				this._useSession = false;
			}
			string text;
			if (this._useSession)
			{
				text = string.Format("{0}_chart_{1}", Guid.NewGuid(), this.UniqueID);
				this.Page.Session[text] = new Bitmap(img);
			}
			else
			{
				text = string.Format("chart_{0}", Guid.NewGuid());
				string text2 = this.TempImagesFolder.Replace("~/", this.applicationPath);
				path = string.Concat(new object[]
				{
					text2,
					"/",
					text,
					".",
					this._chart.ImageFormat
				});
				try
				{
					img.Save(HttpContext.Current.Server.MapPath(path), this.ChartImageFormat);
				}
				catch (ExternalException ex)
				{
					throw new ChartException(ex.Message + " Check the folder specified in the TempImagesFolder property of the control. Current value is \"" + text2 + "\". The folder should exist and must have been granted write permissions for the ASPNET user.");
				}
			}
			string result;
			if (this._useSession)
			{
				result = string.Format("{0}?UseSession=true&ChartID={1}&imageFormat={2}&random={3}", new object[]
				{
					this.HttpHandlerUrl,
					text,
					this._chart.ImageFormat,
					Guid.NewGuid().ToString()
				});
			}
			else
			{
				result = string.Format("{0}?UseSession=false&ImageName={1}&imageFormat={2}", this.HttpHandlerUrl, this.GarbleImagePath(path), this._chart.ImageFormat);
			}
			return result;
		}

		// Token: 0x0600EF71 RID: 61297 RVA: 0x003684BC File Offset: 0x003666BC
		private string GetBackgroundImagePath()
		{
			System.Drawing.Image image = null;
			switch (this.ClientSettings.ScrollMode)
			{
			case ChartClientScrollMode.XOnly:
				image = this._chart.GetStaticArea((int)this.Width.Value, (int)this.Height.Value, false, true, true);
				break;
			case ChartClientScrollMode.YOnly:
				image = this._chart.GetStaticArea((int)this.Width.Value, (int)this.Height.Value, true, false, false);
				break;
			case ChartClientScrollMode.Both:
				image = this._chart.GetStaticArea((int)this.Width.Value, (int)this.Height.Value, false, false, false);
				break;
			}
			string imagePath = this.GetImagePath(image);
			if (image != null)
			{
				image.Dispose();
			}
			return imagePath;
		}

		// Token: 0x0600EF72 RID: 61298 RVA: 0x00368590 File Offset: 0x00366790
		private string GetPlotAreaChunkImagePath(Telerik.Charting.Styles.Unit width, Telerik.Charting.Styles.Unit height, Telerik.Charting.Styles.Unit top, Telerik.Charting.Styles.Unit left)
		{
			System.Drawing.Image plotArea = this._chart.GetPlotArea((int)this.Width.Value, (int)this.Height.Value, this.ClientSettings.XScale, this.ClientSettings.YScale, width, height, top, left);
			string imagePath = this.GetImagePath(plotArea);
			if (plotArea != null)
			{
				plotArea.Dispose();
			}
			return imagePath;
		}

		// Token: 0x0600EF73 RID: 61299 RVA: 0x003685F4 File Offset: 0x003667F4
		private string GetAxisImagePath(ChartAxisType axisType)
		{
			ChartClientScrollMode chartClientScrollMode = (axisType == ChartAxisType.XAxis) ? ChartClientScrollMode.YOnly : ChartClientScrollMode.XOnly;
			if (this.ClientSettings.ScrollMode == chartClientScrollMode || this.ClientSettings.ScrollMode == ChartClientScrollMode.None)
			{
				return string.Empty;
			}
			System.Drawing.Image axis = this._chart.GetAxis((int)this.Width.Value, (int)this.Height.Value, this.ClientSettings.XScale, this.ClientSettings.YScale, axisType);
			string imagePath = this.GetImagePath(axis);
			if (axis != null)
			{
				axis.Dispose();
			}
			return imagePath;
		}

		// Token: 0x0600EF74 RID: 61300 RVA: 0x0036867F File Offset: 0x0036687F
		private string GarbleImagePath(string path)
		{
			return HttpUtility.UrlEncode(Convert.ToBase64String(Security.encryptStringToBytes_AES(path, Security.chartKey, Security.chartIV)));
		}

		// Token: 0x0600EF75 RID: 61301 RVA: 0x0036869C File Offset: 0x0036689C
		protected override void RenderClassic(HtmlTextWriter writer)
		{
			this._chart.ApplicationPath = this.applicationPath;
			System.Drawing.Image image = this._chart.GetImage((int)this.Width.Value, (int)this.Height.Value);
			string path = string.Empty;
			if (this.ChartImageFormat == ImageFormat.Emf)
			{
				this._useSession = false;
			}
			string text;
			if (this._useSession)
			{
				text = string.Format("{0}_chart_{1}", Guid.NewGuid(), this.UniqueID);
				this.Page.Session[text] = new Bitmap(image);
			}
			else
			{
				text = string.Format("chart_{0}", Guid.NewGuid());
				string text2 = this.TempImagesFolder.Replace("~/", this.applicationPath);
				path = string.Concat(new object[]
				{
					text2,
					"/",
					text,
					".",
					this._chart.ImageFormat
				});
				try
				{
					this.Save(HttpContext.Current.Server.MapPath(path), this.ChartImageFormat);
				}
				catch (ExternalException ex)
				{
					throw new ChartException(ex.Message + " Check the folder specified in the TempImagesFolder property of the control. Current value is \"" + text2 + "\". The folder should exist and must have been granted write permissions for the ASPNET user.");
				}
			}
			string text3 = "border-width: 0px;";
			string text4 = string.Empty;
			string text5 = string.Empty;
			if (this.MapAreaBuilder != null)
			{
				text4 = "usemap='#im" + this.ClientID + "'";
				text5 = this.MapAreaBuilder.GenerateImageMap();
			}
			if (this._useSession)
			{
				string value = string.Format("<img alt='{6}' style='{0}' {4} src='{1}?UseSession=true&amp;ChartID={2}&amp;imageFormat={3}&amp;random={5}' {7} />", new object[]
				{
					text3,
					this.HttpHandlerUrl,
					text,
					this._chart.ImageFormat,
					text4,
					Guid.NewGuid().ToString(),
					this.AlternateText,
					RadChart.ImageASPXDebug()
				});
				writer.Write(value);
			}
			else
			{
				writer.Write(string.Format("<img style='{0}' alt='{5}' {4} border='0' src='{1}?UseSession=false&amp;ImageName={2}&amp;imageFormat={3}' {6} />", new object[]
				{
					text3,
					this.HttpHandlerUrl,
					this.GarbleImagePath(path),
					this._chart.ImageFormat,
					text4,
					this.AlternateText,
					RadChart.ImageASPXDebug()
				}));
			}
			if (text5.Length > 0)
			{
				writer.Write(string.Format("<map id='{0}' name='{1}'>", "im" + this.ClientID, "im" + this.ClientID));
				writer.Write(text5);
				writer.Write("</map>");
			}
			if (image != null)
			{
				image.Dispose();
			}
		}

		// Token: 0x1700486F RID: 18543
		// (get) Token: 0x0600EF76 RID: 61302 RVA: 0x00368964 File Offset: 0x00366B64
		bool IStateManager.IsTrackingViewState
		{
			get
			{
				return this._tracking;
			}
		}

		// Token: 0x0600EF77 RID: 61303 RVA: 0x0036896C File Offset: 0x00366B6C
		void IStateManager.LoadViewState(object savedState)
		{
			this.LoadViewState(savedState);
		}

		// Token: 0x0600EF78 RID: 61304 RVA: 0x00368975 File Offset: 0x00366B75
		object IStateManager.SaveViewState()
		{
			return this.SaveViewState();
		}

		// Token: 0x0600EF79 RID: 61305 RVA: 0x0036897D File Offset: 0x00366B7D
		void IStateManager.TrackViewState()
		{
			this.TrackViewState();
		}

		// Token: 0x0600EF7A RID: 61306 RVA: 0x00368985 File Offset: 0x00366B85
		protected override void TrackViewState()
		{
			this._tracking = true;
			base.TrackViewState();
			((IChartingStateManager)this._chart).TrackViewState();
			((IChartingStateManager)this._chartClientSettings).TrackViewState();
		}

		// Token: 0x0600EF7B RID: 61307 RVA: 0x003689AC File Offset: 0x00366BAC
		protected override void LoadViewState(object savedState)
		{
			object[] array = (object[])savedState;
			if (array != null)
			{
				base.LoadViewState(array[0]);
				((IChartingStateManager)this._chart).LoadViewState(array[1]);
				((IChartingStateManager)this._chartClientSettings).LoadViewState(array[2]);
				this._chart.CalculateChart();
			}
		}

		// Token: 0x0600EF7C RID: 61308 RVA: 0x003689F4 File Offset: 0x00366BF4
		protected override object SaveViewState()
		{
			return new ArrayList
			{
				base.SaveViewState(),
				((IChartingStateManager)this._chart).SaveViewState(),
				((IChartingStateManager)this._chartClientSettings).SaveViewState()
			}.ToArray();
		}

		// Token: 0x0600EF7D RID: 61309 RVA: 0x00368A40 File Offset: 0x00366C40
		protected override void LoadClientState(Dictionary<string, object> clientState)
		{
			base.LoadClientState(clientState);
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			javaScriptSerializer.RegisterConverters(new JavaScriptConverter[]
			{
				new JavaScriptColorConverter()
			});
			Dictionary<string, object> dictionary = (Dictionary<string, object>)clientState["_clientSettings"];
			this.ClientSettings.EnableZoom = (bool)dictionary["EnableZoom"];
			this.ClientSettings.ScrollMode = (ChartClientScrollMode)dictionary["ScrollMode"];
			this.ClientSettings.XScale = float.Parse(dictionary["XScale"].ToString());
			this.ClientSettings.XScrollOffset = float.Parse(dictionary["XScrollOffset"].ToString());
			this.ClientSettings.YScale = float.Parse(dictionary["YScale"].ToString());
			this.ClientSettings.YScrollOffset = float.Parse(dictionary["YScrollOffset"].ToString());
			this.ClientSettings.EnableAxisMarkers = (bool)dictionary["EnableAxisMarkers"];
			this.ClientSettings.AxisMarkersSize = (int)dictionary["AxisMarkersSize"];
			this.ClientSettings.ZoomRectangleOpacity = float.Parse(dictionary["ZoomRectangleOpacity"].ToString());
			Dictionary<string, object> dictionary2 = (Dictionary<string, object>)dictionary["AxisMarkersColor"];
			string htmlColor = dictionary2["hex"].ToString();
			this.ClientSettings.AxisMarkersColor = ColorTranslator.FromHtml(htmlColor);
			Dictionary<string, object> dictionary3 = (Dictionary<string, object>)dictionary["ZoomRectangleColor"];
			string htmlColor2 = dictionary3["hex"].ToString();
			this.ClientSettings.ZoomRectangleColor = ColorTranslator.FromHtml(htmlColor2);
			Dictionary<string, object> dictionary4 = (Dictionary<string, object>)clientState["_zoomHistory"];
			Stack<ZoomInfo> stack = new Stack<ZoomInfo>();
			for (int i = dictionary4.Count - 1; i >= 0; i--)
			{
				Dictionary<string, object> dictionary5 = (Dictionary<string, object>)dictionary4[i.ToString()];
				stack.Push(new ZoomInfo
				{
					XScale = float.Parse(dictionary5["XScale"].ToString()),
					XScrollOffset = float.Parse(dictionary5["XScrollOffset"].ToString()),
					YScale = float.Parse(dictionary5["YScale"].ToString()),
					YScrollOffset = float.Parse(dictionary5["YScrollOffset"].ToString())
				});
				this.ZoomHistory = stack;
			}
		}

		// Token: 0x0600EF7E RID: 61310 RVA: 0x00368CD2 File Offset: 0x00366ED2
		public void ClearSkin()
		{
			this._chart.ClearSkin(this.Chart);
		}

		// Token: 0x0600EF7F RID: 61311 RVA: 0x00368CE5 File Offset: 0x00366EE5
		public void LoadSkin(TextWriter text)
		{
			this._chart.LoadSkin(this.Chart, text);
		}

		// Token: 0x0600EF80 RID: 61312 RVA: 0x00368CF9 File Offset: 0x00366EF9
		public TextWriter SaveToXml()
		{
			return this._chart.SaveChart(this);
		}

		// Token: 0x0600EF81 RID: 61313 RVA: 0x00368D08 File Offset: 0x00366F08
		[Description("Saves the chart contents into an Xml file")]
		public void SaveToXml(string fileName)
		{
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(this.SaveToXml().ToString());
			xmlDocument.Save(this.MapPath(fileName));
		}

		// Token: 0x0600EF82 RID: 61314 RVA: 0x00368D3C File Offset: 0x00366F3C
		[Description("Loads an Xml file and populates the chart's properties")]
		public void LoadFromXml(string relativeFileName)
		{
			CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;
			Thread.CurrentThread.CurrentCulture = new CultureInfo("En-us");
			this.Clear();
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.Load(this.MapPath(relativeFileName));
			TextReader reader = new StringReader(xmlDocument.OuterXml);
			this.LoadFromXml(reader);
			Thread.CurrentThread.CurrentCulture = currentCulture;
		}

		// Token: 0x0600EF83 RID: 61315 RVA: 0x00368D9F File Offset: 0x00366F9F
		public void LoadFromXml(TextReader reader)
		{
			this._chart.LoadChart(this, reader);
		}

		// Token: 0x0600EF84 RID: 61316 RVA: 0x00368DAE File Offset: 0x00366FAE
		public TextWriter SaveSkin()
		{
			return this._chart.SaveSkin(this.Chart);
		}

		// Token: 0x0600EF85 RID: 61317 RVA: 0x00368DC4 File Offset: 0x00366FC4
		public object Clone()
		{
			RadChart radChart = new RadChart();
			radChart.LoadViewState(this.SaveViewState());
			radChart.Chart.DesignTime = this.Chart.DesignTime;
			radChart.DataSourceID = this.DataSourceID;
			return radChart;
		}

		// Token: 0x0600EF86 RID: 61318 RVA: 0x00368E06 File Offset: 0x00367006
		[Description("Clears all chart collections: series collection and axis items.")]
		public void Clear()
		{
			this._chart.Series.RemoveSeries();
		}

		// Token: 0x0600EF87 RID: 61319 RVA: 0x00368E18 File Offset: 0x00367018
		[Description("Removes only the series from the series collection.")]
		public void RemoveAllSeries()
		{
			this._chart.RemoveAllSeries();
		}

		// Token: 0x0600EF88 RID: 61320 RVA: 0x00368E25 File Offset: 0x00367025
		[Description("Removes the series at the specified series index.")]
		public void RemoveSeriesAt(int seriesIndex)
		{
			this._chart.RemoveSeriesAt(seriesIndex, new int[0]);
		}

		// Token: 0x0600EF89 RID: 61321 RVA: 0x00368E39 File Offset: 0x00367039
		[Description("Gets the series from the series collection at the specified index.")]
		public ChartSeries GetSeries(int seriesIndex)
		{
			if (seriesIndex >= 0 && seriesIndex < this._chart.Series.Count)
			{
				return this._chart.Series[seriesIndex];
			}
			return null;
		}

		// Token: 0x0600EF8A RID: 61322 RVA: 0x00368E65 File Offset: 0x00367065
		[Description("Gets the series from the series collection with the specified name.")]
		public ChartSeries GetSeries(string seriesName)
		{
			return this.Series.GetByName(seriesName);
		}

		// Token: 0x0600EF8B RID: 61323 RVA: 0x00368E73 File Offset: 0x00367073
		[Description("Gets the series from the series collection with the specified color.")]
		public ChartSeries GetSeries(Color seriesColor)
		{
			return this._chart.GetSeries(seriesColor);
		}

		// Token: 0x0600EF8C RID: 61324 RVA: 0x00368E84 File Offset: 0x00367084
		[Description("Creates a new series, adds it to the series collection and returns a reference to it.")]
		public ChartSeries CreateSeries(string seriesName, Color mainColor, Color secondColor, ChartSeriesType chartSeriesType)
		{
			ChartSeries chartSeries = new ChartSeries(seriesName, chartSeriesType);
			chartSeries.Appearance.FillStyle.MainColor = mainColor;
			chartSeries.Appearance.FillStyle.SecondColor = secondColor;
			chartSeries.Appearance.LineSeriesAppearance.Color = mainColor;
			chartSeries.Appearance.PointMark.Chart = this._chart;
			chartSeries.Appearance.LabelAppearance.Chart = this._chart;
			this.Series.Add(chartSeries);
			return chartSeries;
		}

		// Token: 0x0600EF8D RID: 61325 RVA: 0x00368F06 File Offset: 0x00367106
		[Description("Saves the chart as an image in format specified by ChartImageFormat property.")]
		public void Save(string filename)
		{
			this.Save(filename, this.ChartImageFormat);
		}

		// Token: 0x0600EF8E RID: 61326 RVA: 0x00368F18 File Offset: 0x00367118
		public void Save(Stream stream, ImageFormat imageFormat)
		{
			if (base.Site != null && base.Site.DesignMode)
			{
				this._chart.ApplicationPath = this.applicationPath + "\\";
			}
			if (imageFormat == ImageFormat.Emf)
			{
				ImageFormat chartImageFormat = this.ChartImageFormat;
				this.ChartImageFormat = ImageFormat.Emf;
				this.GetBitmap();
				if (File.Exists(this._chart.TempImagePath))
				{
					new MemoryStream(File.ReadAllBytes(this._chart.TempImagePath)).WriteTo(stream);
					File.Delete(this._chart.TempImagePath);
				}
				this.ChartImageFormat = chartImageFormat;
				this.GetBitmap();
				return;
			}
			System.Drawing.Image bitmap = this.GetBitmap();
			bitmap.Save(stream, imageFormat);
			bitmap.Dispose();
		}

		// Token: 0x0600EF8F RID: 61327 RVA: 0x00368FD8 File Offset: 0x003671D8
		[Description("Saves the chart as an image with the specified ImageFormat.")]
		public void Save(string filename, ImageFormat imageFormat)
		{
			if (imageFormat == ImageFormat.Emf)
			{
				ImageFormat chartImageFormat = this.ChartImageFormat;
				this.ChartImageFormat = ImageFormat.Emf;
				this.GetBitmap();
				if (File.Exists(this._chart.TempImagePath))
				{
					File.Copy(this._chart.TempImagePath, filename, true);
					File.Delete(this._chart.TempImagePath);
				}
				this.ChartImageFormat = chartImageFormat;
				this.GetBitmap();
				return;
			}
			System.Drawing.Image bitmap = this.GetBitmap();
			bitmap.Save(filename, imageFormat);
			bitmap.Dispose();
		}

		// Token: 0x0600EF90 RID: 61328 RVA: 0x00369060 File Offset: 0x00367260
		public System.Drawing.Image GetBitmap()
		{
			if (base.Site != null && base.Site.DesignMode)
			{
				this._chart.ApplicationPath = this.applicationPath + "\\";
			}
			return this._chart.GetImage((int)this.Width.Value, (int)this.Height.Value);
		}

		// Token: 0x17004870 RID: 18544
		// (get) Token: 0x0600EF91 RID: 61329 RVA: 0x003690C8 File Offset: 0x003672C8
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		[Bindable(false)]
		public override Color BorderColor
		{
			get
			{
				return default(Color);
			}
		}

		// Token: 0x17004871 RID: 18545
		// (get) Token: 0x0600EF92 RID: 61330 RVA: 0x003690DE File Offset: 0x003672DE
		[Browsable(false)]
		[Bindable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override BorderStyle BorderStyle
		{
			get
			{
				return BorderStyle.NotSet;
			}
		}

		// Token: 0x17004872 RID: 18546
		// (get) Token: 0x0600EF93 RID: 61331 RVA: 0x003690E1 File Offset: 0x003672E1
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Bindable(false)]
		[Browsable(false)]
		public new Telerik.Charting.Styles.Unit BorderWidth
		{
			get
			{
				return new Telerik.Charting.Styles.Unit();
			}
		}

		// Token: 0x17004873 RID: 18547
		// (get) Token: 0x0600EF94 RID: 61332 RVA: 0x003690E8 File Offset: 0x003672E8
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Bindable(false)]
		[Browsable(false)]
		[DefaultValue("RadChart")]
		public override string CssClass
		{
			get
			{
				return "RadChart";
			}
		}

		// Token: 0x17004874 RID: 18548
		// (get) Token: 0x0600EF95 RID: 61333 RVA: 0x003690EF File Offset: 0x003672EF
		[Browsable(false)]
		[Bindable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override FontInfo Font
		{
			get
			{
				return base.Font;
			}
		}

		// Token: 0x17004875 RID: 18549
		// (get) Token: 0x0600EF96 RID: 61334 RVA: 0x003690F8 File Offset: 0x003672F8
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Bindable(false)]
		public override Color ForeColor
		{
			get
			{
				return default(Color);
			}
		}

		// Token: 0x17004876 RID: 18550
		// (get) Token: 0x0600EF97 RID: 61335 RVA: 0x0036910E File Offset: 0x0036730E
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		[Bindable(false)]
		public override string AccessKey
		{
			get
			{
				return "";
			}
		}

		// Token: 0x17004877 RID: 18551
		// (get) Token: 0x0600EF98 RID: 61336 RVA: 0x00369115 File Offset: 0x00367315
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		[Bindable(false)]
		public override bool Enabled
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17004878 RID: 18552
		// (get) Token: 0x0600EF99 RID: 61337 RVA: 0x00369118 File Offset: 0x00367318
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		[Bindable(false)]
		public override short TabIndex
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17004879 RID: 18553
		// (get) Token: 0x0600EF9A RID: 61338 RVA: 0x0036911B File Offset: 0x0036731B
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Bindable(false)]
		public override string ToolTip
		{
			get
			{
				return "";
			}
		}

		// Token: 0x1700487A RID: 18554
		// (get) Token: 0x0600EF9B RID: 61339 RVA: 0x00369122 File Offset: 0x00367322
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		[Bindable(false)]
		public override Color BackColor
		{
			get
			{
				return Color.Empty;
			}
		}

		// Token: 0x0600EF9C RID: 61340 RVA: 0x0036912C File Offset: 0x0036732C
		protected override void PerformSelect()
		{
			if (!base.IsBoundUsingDataSourceID)
			{
				this.OnDataBinding(EventArgs.Empty);
			}
			DataSourceView data = this.GetData();
			if (data.CanPage)
			{
				data.Select(new DataSourceSelectArguments(0, int.MaxValue), new DataSourceViewSelectCallback(this.OnDataSourceViewSelectCallback));
			}
			else
			{
				data.Select(DataSourceSelectArguments.Empty, new DataSourceViewSelectCallback(this.OnDataSourceViewSelectCallback));
			}
			base.RequiresDataBinding = false;
			base.MarkAsDataBound();
			this.OnDataBound(EventArgs.Empty);
		}

		// Token: 0x0600EF9D RID: 61341 RVA: 0x003691A9 File Offset: 0x003673A9
		private void OnDataSourceViewSelectCallback(IEnumerable retrievedData)
		{
			if (base.IsBoundUsingDataSourceID)
			{
				this.OnDataBinding(EventArgs.Empty);
			}
			this.PerformDataBinding(retrievedData);
		}

		// Token: 0x0600EF9E RID: 61342 RVA: 0x003691C5 File Offset: 0x003673C5
		protected override void PerformDataBinding(IEnumerable data)
		{
			if (data != null && !this.DataManager.IsDataBindCalled)
			{
				this._chart.DataManager.DataSource = data;
				this._chart.DataManager.DataBind();
			}
		}

		// Token: 0x0600EF9F RID: 61343 RVA: 0x003691F8 File Offset: 0x003673F8
		protected override void OnDataSourceViewChanged(object sender, EventArgs e)
		{
			this.Series.ClearItems();
			base.OnDataSourceViewChanged(sender, e);
		}

		// Token: 0x1700487B RID: 18555
		// (get) Token: 0x0600EFA0 RID: 61344 RVA: 0x0036920D File Offset: 0x0036740D
		// (set) Token: 0x0600EFA1 RID: 61345 RVA: 0x00369218 File Offset: 0x00367418
		[Category("Data")]
		[DefaultValue("")]
		[Description("Gets or sets the ID of the control from which the data-bound control retrieves its list of data items.")]
		public override string DataSourceID
		{
			get
			{
				return base.DataSourceID;
			}
			set
			{
				if (this.DataSource != null)
				{
					this.DataManager.ClearDataSource();
				}
				if (this._chart.DesignTime)
				{
					this.DataManager.UseAutoBind = true;
					if (string.Compare(base.DataSourceID, value, true) != 0 && !string.IsNullOrEmpty(base.DataSourceID))
					{
						this.PlotArea.XAxis.ClearDataBoundState();
						this.Series.ClearDataBoundState();
						this.DataManager.UseSeriesGrouping = true;
					}
				}
				base.DataSourceID = value;
				try
				{
					if (this._chart.DesignTime)
					{
						IDataSource dataSource = this.FindDataSource(value);
						AccessDataSource accessDataSource = dataSource as AccessDataSource;
						if (accessDataSource != null)
						{
							this.LocalDataFilePathToGlobal(accessDataSource);
						}
						else
						{
							XmlDataSource xmlDataSource = dataSource as XmlDataSource;
							if (xmlDataSource != null)
							{
								this.LocalDataFilePathToGlobal(xmlDataSource);
							}
						}
					}
					this.DataBind();
					this.DataManager.UseAutoBind = false;
				}
				catch
				{
				}
			}
		}

		// Token: 0x0600EFA2 RID: 61346 RVA: 0x00369300 File Offset: 0x00367500
		private IDataSource FindDataSource(string name)
		{
			foreach (object obj in base.Site.Container.Components)
			{
				try
				{
					if (string.Compare(TypeDescriptor.GetProperties(obj)["ID"].GetValue(obj).ToString(), name, true) == 0)
					{
						return (IDataSource)obj;
					}
				}
				catch
				{
				}
			}
			return null;
		}

		// Token: 0x0600EFA3 RID: 61347 RVA: 0x00369398 File Offset: 0x00367598
		private IDataSource LocalDataFilePathToGlobal(AccessDataSource ids)
		{
			return this._chart.LocalDataFilePathToGlobal(ids);
		}

		// Token: 0x0600EFA4 RID: 61348 RVA: 0x003693A6 File Offset: 0x003675A6
		private IDataSource LocalDataFilePathToGlobal(XmlDataSource ids)
		{
			return this._chart.LocalDataFilePathToGlobal(ids);
		}

		// Token: 0x0600EFA5 RID: 61349 RVA: 0x003693B4 File Offset: 0x003675B4
		protected void SetDataSourceID(string id)
		{
			if (base.DataSourceID != id && !string.IsNullOrEmpty(base.DataSourceID))
			{
				this.PlotArea.XAxis.ClearDataBoundState();
				this.DataManager.UseSeriesGrouping = true;
			}
			base.DataSourceID = id;
		}

		// Token: 0x1700487C RID: 18556
		// (get) Token: 0x0600EFA6 RID: 61350 RVA: 0x003693F4 File Offset: 0x003675F4
		// (set) Token: 0x0600EFA7 RID: 61351 RVA: 0x00369408 File Offset: 0x00367608
		[Browsable(false)]
		[Category("Data")]
		[DefaultValue("")]
		[AttributeProvider(typeof(IListSource))]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override object DataSource
		{
			get
			{
				return this._chart.DataManager.DataSource;
			}
			set
			{
				if (!string.IsNullOrEmpty(this.DataSourceID))
				{
					this.DataSourceID = string.Empty;
				}
				if (this._chart.DesignTime && this.DataManager.DataSource != value)
				{
					this.PlotArea.XAxis.ClearDataBoundState();
					this.Series.ClearDataBoundState();
					this.DataManager.UseSeriesGrouping = true;
				}
				this.DataManager.DataSource = value;
			}
		}

		// Token: 0x1700487D RID: 18557
		// (get) Token: 0x0600EFA8 RID: 61352 RVA: 0x0036947B File Offset: 0x0036767B
		// (set) Token: 0x0600EFA9 RID: 61353 RVA: 0x0036948D File Offset: 0x0036768D
		[Browsable(false)]
		[Category("Data")]
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override string DataMember
		{
			get
			{
				return this._chart.DataManager.DataMember;
			}
			set
			{
				this._chart.DataManager.DataMember = value;
			}
		}

		// Token: 0x0600EFAA RID: 61354 RVA: 0x003694A0 File Offset: 0x003676A0
		[Category("Data")]
		[Browsable(false)]
		public override void DataBind()
		{
			this.Controls.Clear();
			if (base.HasChildViewState)
			{
				base.ClearChildViewState();
			}
			if (!base.IsTrackingViewState)
			{
				this.TrackViewState();
			}
			if (base.IsBoundUsingDataSourceID)
			{
				this.PerformSelect();
			}
			else
			{
				this.OnDataBinding(EventArgs.Empty);
				this.DataManager.DataBind();
				base.MarkAsDataBound();
				this.OnDataBound(EventArgs.Empty);
			}
			base.ChildControlsCreated = true;
		}

		// Token: 0x1700487E RID: 18558
		// (get) Token: 0x0600EFAB RID: 61355 RVA: 0x00369512 File Offset: 0x00367712
		// (set) Token: 0x0600EFAC RID: 61356 RVA: 0x00369520 File Offset: 0x00367720
		[NotifyParentProperty(true)]
		[Category("Data")]
		[DefaultValue("")]
		[Description("Name of the DataSource column (member) that will be used to split Y values from one column into several chart Series")]
		[Editor(typeof(DataColumnEditor), typeof(UITypeEditor))]
		public string DataGroupColumn
		{
			get
			{
				return this._chart.DataGroupColumn;
			}
			set
			{
				if (value == "(None)")
				{
					this._chart.DataManager.UseSeriesGrouping = false;
					this._chart.DataGroupColumn = string.Empty;
				}
				else
				{
					this._chart.DataManager.UseSeriesGrouping = true;
					this._chart.DataGroupColumn = value;
				}
				if (base.Site != null)
				{
					try
					{
						if (!string.IsNullOrEmpty(this.DataSourceID))
						{
							this.DataManager.ClearDataSource();
							this.DataManager.UseAutoBind = true;
							this.DataBind();
							this.DataManager.UseAutoBind = false;
						}
					}
					catch
					{
					}
				}
			}
		}

		// Token: 0x1700487F RID: 18559
		// (get) Token: 0x0600EFAD RID: 61357 RVA: 0x003695D0 File Offset: 0x003677D0
		// (set) Token: 0x0600EFAE RID: 61358 RVA: 0x003695D8 File Offset: 0x003677D8
		private string LastRenderedChunkPath
		{
			get
			{
				return this.lastRenderedChunk;
			}
			set
			{
				this.lastRenderedChunk = value;
			}
		}

		// Token: 0x0600EFAF RID: 61359 RVA: 0x003695E1 File Offset: 0x003677E1
		private static string ImageASPXDebug()
		{
			if (HttpContext.Current.IsDebuggingEnabled)
			{
				return "onerror=\"if(confirm('Error loading RadChart image.\\nYou may also wish to check the ASP.NET Trace for further details.\\nDisplay stack trace?'))window.location.href=this.src;\"";
			}
			return string.Empty;
		}

		// Token: 0x0600EFB0 RID: 61360 RVA: 0x003695FA File Offset: 0x003677FA
		internal bool HasClickEvent()
		{
			return this.Click != null;
		}

		// Token: 0x17004880 RID: 18560
		// (get) Token: 0x0600EFB1 RID: 61361 RVA: 0x00369608 File Offset: 0x00367808
		public bool ScaleEnabled
		{
			get
			{
				return this.ClientSettings.ScrollMode != ChartClientScrollMode.None;
			}
		}

		// Token: 0x0600EFB2 RID: 61362 RVA: 0x0036961B File Offset: 0x0036781B
		protected override void RegisterScriptControl()
		{
			if (this.HasClient)
			{
				base.RegisterScriptControl();
			}
		}

		// Token: 0x0600EFB3 RID: 61363 RVA: 0x0036962B File Offset: 0x0036782B
		protected override void RegisterScriptDescriptors()
		{
			if (this.HasClient)
			{
				base.RegisterScriptDescriptors();
			}
		}

		// Token: 0x0600EFB4 RID: 61364 RVA: 0x0036963B File Offset: 0x0036783B
		protected override void RegisterCssReferences()
		{
		}

		// Token: 0x0600EFB5 RID: 61365 RVA: 0x00369640 File Offset: 0x00367840
		protected override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
		{
			IEnumerable<ScriptDescriptor> result = new List<ScriptDescriptor>();
			if (this.HasClient)
			{
				result = base.GetScriptDescriptors();
			}
			return result;
		}

		// Token: 0x0600EFB6 RID: 61366 RVA: 0x00369664 File Offset: 0x00367864
		protected override IEnumerable<ScriptReference> GetScriptReferences()
		{
			IEnumerable<ScriptReference> result = new List<ScriptReference>();
			if (this.HasClient)
			{
				result = base.GetScriptReferences();
			}
			return result;
		}

		// Token: 0x0600EFB7 RID: 61367 RVA: 0x00369688 File Offset: 0x00367888
		public string MapPath(string filePath)
		{
			string text = filePath.Replace('/', '\\');
			if (!Path.IsPathRooted(text))
			{
				if (base.Site != null)
				{
					text = this.ResolvePhysicalLocation(filePath);
					if (text.EndsWith(Path.DirectorySeparatorChar.ToString()))
					{
						text = text.Substring(0, text.Length - 1);
					}
				}
				else if (text.StartsWith("~\\"))
				{
					text = HttpContext.Current.Server.MapPath(this.applicationPath) + text.Remove(0, 2);
				}
				else
				{
					text = HttpContext.Current.Server.MapPath(text);
				}
			}
			return text.Replace("\\\\", "\\");
		}

		// Token: 0x0600EFB8 RID: 61368 RVA: 0x00369734 File Offset: 0x00367934
		public string GetCallbackResult()
		{
			return this.LastRenderedChunkPath;
		}

		// Token: 0x0600EFB9 RID: 61369 RVA: 0x0036973C File Offset: 0x0036793C
		public void RaiseCallbackEvent(string eventArgument)
		{
			string[] array = eventArgument.Split(new char[]
			{
				':'
			});
			if (array[0] == "LC")
			{
				Telerik.Charting.Styles.Unit left = Telerik.Charting.Styles.Unit.Pixel(float.Parse(array[1], CultureInfo.InvariantCulture));
				Telerik.Charting.Styles.Unit top = Telerik.Charting.Styles.Unit.Pixel(float.Parse(array[2], CultureInfo.InvariantCulture));
				Telerik.Charting.Styles.Unit width = Telerik.Charting.Styles.Unit.Pixel(float.Parse(array[3], CultureInfo.InvariantCulture));
				Telerik.Charting.Styles.Unit height = Telerik.Charting.Styles.Unit.Pixel(float.Parse(array[4], CultureInfo.InvariantCulture));
				this.ClientSettings.XScale = float.Parse(array[5], CultureInfo.InvariantCulture);
				this.ClientSettings.YScale = float.Parse(array[6], CultureInfo.InvariantCulture);
				this.LastRenderedChunkPath = this.GetPlotAreaChunkImagePath(width, height, top, left);
			}
		}

		// Token: 0x0600EFBA RID: 61370 RVA: 0x00369800 File Offset: 0x00367A00
		protected override void RaisePostDataChangedEvent()
		{
			base.RaisePostDataChangedEvent();
			if (this.isZoomingIn)
			{
				this.OnZoomIn(this.zoomArguments);
			}
		}

		// Token: 0x0600EFBB RID: 61371 RVA: 0x0036981C File Offset: 0x00367A1C
		protected override bool LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			base.LoadPostData(postDataKey, postCollection);
			string[] array = postCollection["__EVENTARGUMENT"].Split(new char[]
			{
				','
			});
			if (array[0] == "zoom")
			{
				this.isZoomingIn = true;
				this.zoomArguments = array;
			}
			return true;
		}

		// Token: 0x040044F0 RID: 17648
		private const int defHeight = 300;

		// Token: 0x040044F1 RID: 17649
		private const int defWidth = 400;

		// Token: 0x040044F2 RID: 17650
		private string _siteDomain;

		// Token: 0x040044F3 RID: 17651
		private bool _useSession;

		// Token: 0x040044F4 RID: 17652
		private string _tempImagesFolder;

		// Token: 0x040044F5 RID: 17653
		private string _applicationPath;

		// Token: 0x040044F6 RID: 17654
		private bool _createImageMap;

		// Token: 0x040044F7 RID: 17655
		private string _alternateText;

		// Token: 0x040044F8 RID: 17656
		private MapAreaBuilder _mapAreaBuilder;

		// Token: 0x040044F9 RID: 17657
		private readonly Chart _chart;

		// Token: 0x040044FA RID: 17658
		private ChartClientSettings _chartClientSettings;

		// Token: 0x040044FB RID: 17659
		private string[] zoomArguments;

		// Token: 0x040044FC RID: 17660
		private bool isZoomingIn;

		// Token: 0x04004502 RID: 17666
		private bool handlerChecked;

		// Token: 0x04004503 RID: 17667
		private bool _tracking;

		// Token: 0x04004504 RID: 17668
		private string lastRenderedChunk;

		// Token: 0x02001805 RID: 6149
		// (Invoke) Token: 0x0600EFBD RID: 61373
		public delegate void ChartClickEventHandler(object sender, ChartClickEventArgs e);
	}
}
