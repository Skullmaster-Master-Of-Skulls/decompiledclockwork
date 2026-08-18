using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;
using System.Web.UI;
using System.Web.UI.Design;
using System.Web.UI.WebControls;
using Telerik.Charting.Styles;
using Telerik.Charting.Styles.Skins;

namespace Telerik.Charting
{
	// Token: 0x020016E1 RID: 5857
	[PersistChildren(false)]
	[ParseChildren(true)]
	public class Chart : LayoutElement, IContainer, IDisposable
	{
		// Token: 0x1700455C RID: 17756
		// (get) Token: 0x0600E31B RID: 58139 RVA: 0x00325313 File Offset: 0x00323513
		internal FiguresCollection Figures
		{
			get
			{
				return this.chartFigures;
			}
		}

		// Token: 0x1700455D RID: 17757
		// (get) Token: 0x0600E31C RID: 58140 RVA: 0x0032531B File Offset: 0x0032351B
		internal CustomFiguresCollection CustomFigures
		{
			get
			{
				return this.chartCustomFigures;
			}
		}

		// Token: 0x1700455E RID: 17758
		// (get) Token: 0x0600E31D RID: 58141 RVA: 0x00325323 File Offset: 0x00323523
		[NotifyParentProperty(true)]
		internal CustomPalettesCollection CustomPalettes
		{
			get
			{
				return this.chartCustomPalettes;
			}
		}

		// Token: 0x1700455F RID: 17759
		// (get) Token: 0x0600E31E RID: 58142 RVA: 0x0032532B File Offset: 0x0032352B
		// (set) Token: 0x0600E31F RID: 58143 RVA: 0x00325333 File Offset: 0x00323533
		internal bool DesignTime
		{
			get
			{
				return this.chartDesignTime;
			}
			set
			{
				this.chartDesignTime = value;
			}
		}

		// Token: 0x17004560 RID: 17760
		// (get) Token: 0x0600E320 RID: 58144 RVA: 0x0032533C File Offset: 0x0032353C
		[SkinnableProperty]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TypeConverter(typeof(ExpandableObjectConverter))]
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public ChartTitle ChartTitle
		{
			get
			{
				return this.chartTitle;
			}
		}

		// Token: 0x17004561 RID: 17761
		// (get) Token: 0x0600E321 RID: 58145 RVA: 0x00325344 File Offset: 0x00323544
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TypeConverter(typeof(ExpandableObjectConverter))]
		[SkinnableProperty]
		[NotifyParentProperty(true)]
		public ChartLegend Legend
		{
			get
			{
				return this.chartLegend;
			}
		}

		// Token: 0x17004562 RID: 17762
		// (get) Token: 0x0600E322 RID: 58146 RVA: 0x0032534C File Offset: 0x0032354C
		[TypeConverter(typeof(ExpandableObjectConverter))]
		[NotifyParentProperty(true)]
		[SkinnableProperty]
		[Browsable(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public StyleChart Appearance
		{
			get
			{
				return (StyleChart)this.appearance;
			}
		}

		// Token: 0x17004563 RID: 17763
		// (get) Token: 0x0600E323 RID: 58147 RVA: 0x00325359 File Offset: 0x00323559
		[SkinnableProperty]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[Browsable(true)]
		public ChartPlotArea PlotArea
		{
			get
			{
				return this.chartPlotArea;
			}
		}

		// Token: 0x17004564 RID: 17764
		// (get) Token: 0x0600E324 RID: 58148 RVA: 0x00325361 File Offset: 0x00323561
		// (set) Token: 0x0600E325 RID: 58149 RVA: 0x00325382 File Offset: 0x00323582
		[Browsable(true)]
		[DefaultValue("Bar")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public ChartSeriesType DefaultType
		{
			get
			{
				return (ChartSeriesType)(base.ViewState["DefaultType"] ?? ChartSeriesType.Bar);
			}
			set
			{
				base.ViewState["DefaultType"] = value;
				this.ChangeSeriesType();
			}
		}

		// Token: 0x17004565 RID: 17765
		// (get) Token: 0x0600E326 RID: 58150 RVA: 0x003253A0 File Offset: 0x003235A0
		[NotifyParentProperty(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		internal System.Drawing.Image Bitmap
		{
			get
			{
				return this.GetImage();
			}
		}

		// Token: 0x17004566 RID: 17766
		// (get) Token: 0x0600E327 RID: 58151 RVA: 0x003253A8 File Offset: 0x003235A8
		// (set) Token: 0x0600E328 RID: 58152 RVA: 0x003253C8 File Offset: 0x003235C8
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		internal string DataGroupColumn
		{
			get
			{
				return (string)(base.ViewState["DataGroupColumn"] ?? string.Empty);
			}
			set
			{
				if (string.Compare(value, "(None)", true) == 0)
				{
					base.ViewState["DataGroupColumn"] = string.Empty;
					return;
				}
				base.ViewState["DataGroupColumn"] = value;
			}
		}

		// Token: 0x17004567 RID: 17767
		// (get) Token: 0x0600E329 RID: 58153 RVA: 0x003253FF File Offset: 0x003235FF
		[SkinnableProperty]
		public ChartSeriesCollection Series
		{
			get
			{
				return this.chartSeriesCollection;
			}
		}

		// Token: 0x17004568 RID: 17768
		// (get) Token: 0x0600E32A RID: 58154 RVA: 0x00325407 File Offset: 0x00323607
		// (set) Token: 0x0600E32B RID: 58155 RVA: 0x0032540F File Offset: 0x0032360F
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		internal string SeriesPalette
		{
			get
			{
				return this.SeriesPaletteWrapper;
			}
			set
			{
				this.SeriesPaletteWrapper = value;
			}
		}

		// Token: 0x17004569 RID: 17769
		// (get) Token: 0x0600E32C RID: 58156 RVA: 0x00325418 File Offset: 0x00323618
		// (set) Token: 0x0600E32D RID: 58157 RVA: 0x00325438 File Offset: 0x00323638
		internal string SeriesPaletteWrapper
		{
			get
			{
				return (string)(base.ViewState["SeriesPalette"] ?? "");
			}
			set
			{
				if (string.Compare(value, "(None)", true) == 0)
				{
					base.ViewState["SeriesPalette"] = "";
				}
				else
				{
					base.ViewState["SeriesPalette"] = value;
				}
				this.ApplyPalette(value);
			}
		}

		// Token: 0x1700456A RID: 17770
		// (get) Token: 0x0600E32E RID: 58158 RVA: 0x00325477 File Offset: 0x00323677
		// (set) Token: 0x0600E32F RID: 58159 RVA: 0x0032547F File Offset: 0x0032367F
		[DefaultValue(false)]
		[NotifyParentProperty(true)]
		internal bool AutoLayout
		{
			get
			{
				return this.AutoLayoutWrapper;
			}
			set
			{
				this.AutoLayoutWrapper = value;
			}
		}

		// Token: 0x1700456B RID: 17771
		// (get) Token: 0x0600E330 RID: 58160 RVA: 0x00325488 File Offset: 0x00323688
		// (set) Token: 0x0600E331 RID: 58161 RVA: 0x003254A9 File Offset: 0x003236A9
		internal bool AutoLayoutWrapper
		{
			get
			{
				return (bool)(base.ViewState["AutoLayout"] ?? false);
			}
			set
			{
				base.ViewState["AutoLayout"] = value;
			}
		}

		// Token: 0x1700456C RID: 17772
		// (get) Token: 0x0600E332 RID: 58162 RVA: 0x003254C1 File Offset: 0x003236C1
		// (set) Token: 0x0600E333 RID: 58163 RVA: 0x003254C9 File Offset: 0x003236C9
		[DefaultValue(false)]
		[NotifyParentProperty(true)]
		internal bool AutoTextWrap
		{
			get
			{
				return this.AutoTextWrapWrapper;
			}
			set
			{
				this.AutoTextWrapWrapper = value;
			}
		}

		// Token: 0x1700456D RID: 17773
		// (get) Token: 0x0600E334 RID: 58164 RVA: 0x003254D2 File Offset: 0x003236D2
		// (set) Token: 0x0600E335 RID: 58165 RVA: 0x003254F3 File Offset: 0x003236F3
		internal bool AutoTextWrapWrapper
		{
			get
			{
				return (bool)(base.ViewState["AutoTextWrap"] ?? false);
			}
			set
			{
				base.ViewState["AutoTextWrap"] = value;
			}
		}

		// Token: 0x1700456E RID: 17774
		// (get) Token: 0x0600E336 RID: 58166 RVA: 0x0032550B File Offset: 0x0032370B
		// (set) Token: 0x0600E337 RID: 58167 RVA: 0x0032552C File Offset: 0x0032372C
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		public string Skin
		{
			get
			{
				return (string)(base.ViewState["Skin"] ?? "");
			}
			set
			{
				if (ChartSkin.IsEmpty(value))
				{
					base.ViewState["Skin"] = string.Empty;
					return;
				}
				bool flag = (string)base.ViewState["Skin"] != value;
				base.ViewState["Skin"] = value;
				this.ApplySkin(value);
				if (this.DesignTime && flag)
				{
					this.UpdateDesign();
				}
			}
		}

		// Token: 0x1700456F RID: 17775
		// (get) Token: 0x0600E338 RID: 58168 RVA: 0x0032559C File Offset: 0x0032379C
		// (set) Token: 0x0600E339 RID: 58169 RVA: 0x003255BD File Offset: 0x003237BD
		[DefaultValue(false)]
		[NotifyParentProperty(true)]
		internal bool SkinsOverrideStyles
		{
			get
			{
				return (bool)(base.ViewState["SkinsOverrideStyles"] ?? false);
			}
			set
			{
				base.ViewState["SkinsOverrideStyles"] = value;
			}
		}

		// Token: 0x17004570 RID: 17776
		// (get) Token: 0x0600E33A RID: 58170 RVA: 0x003255D5 File Offset: 0x003237D5
		internal DataManager DataManager
		{
			get
			{
				return this.chartDataManager;
			}
		}

		// Token: 0x17004571 RID: 17777
		// (get) Token: 0x0600E33B RID: 58171 RVA: 0x003255DD File Offset: 0x003237DD
		// (set) Token: 0x0600E33C RID: 58172 RVA: 0x003255FE File Offset: 0x003237FE
		[NotifyParentProperty(true)]
		[Browsable(true)]
		[DefaultValue(ChartSeriesOrientation.Vertical)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		internal ChartSeriesOrientation SeriesOrientation
		{
			get
			{
				return (ChartSeriesOrientation)(base.ViewState["SeriesOrientation"] ?? ChartSeriesOrientation.Vertical);
			}
			set
			{
				base.ViewState["SeriesOrientation"] = value;
				this.chartPlotArea.UpdateAxisOrientation();
			}
		}

		// Token: 0x17004572 RID: 17778
		// (get) Token: 0x0600E33D RID: 58173 RVA: 0x00325621 File Offset: 0x00323821
		// (set) Token: 0x0600E33E RID: 58174 RVA: 0x00325642 File Offset: 0x00323842
		[Browsable(true)]
		[NotifyParentProperty(true)]
		[DefaultValue(typeof(bool), "false")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		internal bool IntelligentLabelsEnabled
		{
			get
			{
				return (bool)(base.ViewState["IntelligentLabelsEnabled"] ?? false);
			}
			set
			{
				base.ViewState["IntelligentLabelsEnabled"] = value;
			}
		}

		// Token: 0x17004573 RID: 17779
		// (get) Token: 0x0600E33F RID: 58175 RVA: 0x0032565A File Offset: 0x0032385A
		// (set) Token: 0x0600E340 RID: 58176 RVA: 0x0032567A File Offset: 0x0032387A
		internal string ApplicationPath
		{
			get
			{
				return (string)(base.ViewState["ApplicationPath"] ?? string.Empty);
			}
			set
			{
				base.ViewState["ApplicationPath"] = value;
			}
		}

		// Token: 0x17004574 RID: 17780
		// (get) Token: 0x0600E341 RID: 58177 RVA: 0x0032568D File Offset: 0x0032388D
		// (set) Token: 0x0600E342 RID: 58178 RVA: 0x003256AD File Offset: 0x003238AD
		internal string TempImagePath
		{
			get
			{
				return (string)(base.ViewState["TempImagePath"] ?? string.Empty);
			}
			set
			{
				base.ViewState["TempImagePath"] = value;
			}
		}

		// Token: 0x17004575 RID: 17781
		// (get) Token: 0x0600E343 RID: 58179 RVA: 0x003256C0 File Offset: 0x003238C0
		// (set) Token: 0x0600E344 RID: 58180 RVA: 0x003256E0 File Offset: 0x003238E0
		internal ImageFormat ImageFormat
		{
			get
			{
				return (ImageFormat)(base.ViewState["ImageFormat"] ?? ImageFormat.Png);
			}
			set
			{
				base.ViewState["ImageFormat"] = value;
			}
		}

		// Token: 0x17004576 RID: 17782
		// (get) Token: 0x0600E345 RID: 58181 RVA: 0x003256F3 File Offset: 0x003238F3
		// (set) Token: 0x0600E346 RID: 58182 RVA: 0x00325718 File Offset: 0x00323918
		internal float BitmapResolution
		{
			get
			{
				return (float)(base.ViewState["BitmapResolution"] ?? 0f);
			}
			set
			{
				base.ViewState["BitmapResolution"] = value;
			}
		}

		// Token: 0x17004577 RID: 17783
		// (get) Token: 0x0600E347 RID: 58183 RVA: 0x00325730 File Offset: 0x00323930
		internal float TextWrapFactor
		{
			get
			{
				if (this.Appearance.Dimensions.Height.PixelValue == 0f)
				{
					return 2f;
				}
				return Math.Max(2f, this.Appearance.Dimensions.Width.PixelValue / this.Appearance.Dimensions.Height.PixelValue);
			}
		}

		// Token: 0x140001BF RID: 447
		// (add) Token: 0x0600E348 RID: 58184 RVA: 0x00325794 File Offset: 0x00323994
		// (remove) Token: 0x0600E349 RID: 58185 RVA: 0x003257CC File Offset: 0x003239CC
		internal event EventHandler<EventArgs> BeforeLayoutEventHandler;

		// Token: 0x140001C0 RID: 448
		// (add) Token: 0x0600E34A RID: 58186 RVA: 0x00325804 File Offset: 0x00323A04
		// (remove) Token: 0x0600E34B RID: 58187 RVA: 0x0032583C File Offset: 0x00323A3C
		internal event EventHandler<EventArgs> PrePaintEventHandler;

		// Token: 0x0600E34C RID: 58188 RVA: 0x00325871 File Offset: 0x00323A71
		internal Chart() : base(null)
		{
			this.Init();
			this.BeforeLayoutEventHandler += this.Chart_BeforeLayout;
			this.PrePaintEventHandler += this.Chart_PrePaint;
		}

		// Token: 0x0600E34D RID: 58189 RVA: 0x003258A4 File Offset: 0x00323AA4
		private void Chart_BeforeLayout(object sender, EventArgs e)
		{
		}

		// Token: 0x0600E34E RID: 58190 RVA: 0x003258A6 File Offset: 0x00323AA6
		private void Chart_PrePaint(object sender, EventArgs e)
		{
		}

		// Token: 0x0600E34F RID: 58191 RVA: 0x003258A8 File Offset: 0x00323AA8
		internal Chart(IChartComponent component) : this()
		{
			this.chartComponent = component;
		}

		// Token: 0x0600E350 RID: 58192 RVA: 0x003258B8 File Offset: 0x00323AB8
		private IActiveRegion CallRegionEvent(PointF point, IContainer container)
		{
			Chart chart = container as Chart;
			if (container != null)
			{
				foreach (IOrdering ordering in container.OrderList)
				{
					IContainer container2 = ordering as IContainer;
					if (container2 != null)
					{
						IActiveRegion activeRegion = this.CallRegionEvent(point, container2);
						if (activeRegion != null)
						{
							return activeRegion;
						}
					}
					IActiveRegion activeRegion2 = ordering as IActiveRegion;
					if (activeRegion2 != null && activeRegion2.ActiveRegion.CheckPoint(point, true))
					{
						return activeRegion2;
					}
				}
				if (chart != null)
				{
					ActiveRegion activeRegion3 = new ActiveRegion();
					foreach (ChartSeries chartSeries in chart.Series)
					{
						foreach (ChartSeriesItem chartSeriesItem in chartSeries.Items)
						{
							if (string.Compare(chartSeriesItem.ActiveRegion.Attributes, activeRegion3.Attributes, true) == 0 && string.Compare(chartSeriesItem.ActiveRegion.Tooltip, activeRegion3.Tooltip, true) == 0 && string.Compare(chartSeriesItem.ActiveRegion.Url, activeRegion3.Url, true) == 0 && chartSeries.IsActiveRegionSet)
							{
								chartSeriesItem.ActiveRegion.Url = chartSeries.ActiveRegionUrl;
								chartSeriesItem.ActiveRegion.Tooltip = chartSeries.ActiveRegionToolTip;
								chartSeriesItem.ActiveRegion.Attributes = chartSeries.ActiveRegionAttributes;
							}
							if (chartSeriesItem.ActiveRegion.CheckPoint(point, true))
							{
								return chartSeriesItem;
							}
						}
					}
				}
			}
			return null;
		}

		// Token: 0x0600E351 RID: 58193 RVA: 0x00325A94 File Offset: 0x00323C94
		private void ChangeSeriesType()
		{
			foreach (ChartSeries chartSeries in this.Series)
			{
				chartSeries.Type = this.DefaultType;
			}
		}

		// Token: 0x0600E352 RID: 58194 RVA: 0x00325AE8 File Offset: 0x00323CE8
		private void ApplyPalette(string paletteName)
		{
			this.chartPlotArea.Appearance.SeriesPalette = paletteName;
		}

		// Token: 0x0600E353 RID: 58195 RVA: 0x00325AFC File Offset: 0x00323CFC
		private void ApplySkin(string skinName)
		{
			if (!ChartSkin.IsEmpty(skinName))
			{
				ChartSkin skin = this.chartSkinsCollection.GetSkin(skinName.Trim());
				if (skin != null)
				{
					skin.ApplyTo(this);
				}
			}
		}

		// Token: 0x0600E354 RID: 58196 RVA: 0x00325B2D File Offset: 0x00323D2D
		internal bool ShouldApplyTextWrapping(AutoTextWrap textBlockAutoTextWrap)
		{
			if (this.AutoTextWrapWrapper)
			{
				if (textBlockAutoTextWrap == Telerik.Charting.Styles.AutoTextWrap.Auto || textBlockAutoTextWrap == Telerik.Charting.Styles.AutoTextWrap.True)
				{
					return true;
				}
			}
			else if (textBlockAutoTextWrap == Telerik.Charting.Styles.AutoTextWrap.True)
			{
				return true;
			}
			return false;
		}

		// Token: 0x0600E355 RID: 58197 RVA: 0x00325B48 File Offset: 0x00323D48
		internal Chart Clone()
		{
			Chart chart = new Chart();
			chart.LoadViewState(this.SaveViewState());
			return chart;
		}

		// Token: 0x17004578 RID: 17784
		// (get) Token: 0x0600E356 RID: 58198 RVA: 0x00325B68 File Offset: 0x00323D68
		internal ChartSeriesCollection DesignTimeSeriesCollection
		{
			get
			{
				if (this.chartDesignTimeSeriesCollection == null)
				{
					this.chartDesignTimeSeriesCollection = new ChartSeriesCollection(this);
				}
				return this.chartDesignTimeSeriesCollection;
			}
		}

		// Token: 0x17004579 RID: 17785
		// (get) Token: 0x0600E357 RID: 58199 RVA: 0x00325B84 File Offset: 0x00323D84
		internal ChartSeriesCollection OriginalSeriesCollection
		{
			get
			{
				if (this.chartOriginalSeriesCollection == null)
				{
					this.chartOriginalSeriesCollection = new ChartSeriesCollection(this);
				}
				return this.chartOriginalSeriesCollection;
			}
		}

		// Token: 0x1700457A RID: 17786
		// (get) Token: 0x0600E358 RID: 58200 RVA: 0x00325BA0 File Offset: 0x00323DA0
		// (set) Token: 0x0600E359 RID: 58201 RVA: 0x00325BAD File Offset: 0x00323DAD
		internal IComponent Parent
		{
			get
			{
				return this.chartComponent as IComponent;
			}
			set
			{
				this.chartComponent = (value as IChartComponent);
			}
		}

		// Token: 0x0600E35A RID: 58202 RVA: 0x00325BBC File Offset: 0x00323DBC
		internal void UpdateDesign()
		{
			if (this.DesignTime && this.Parent != null && this.Parent.Site != null)
			{
				IDesignerHost designerHost = (IDesignerHost)this.Parent.Site.GetService(typeof(IDesignerHost));
				((IChartDesigner)designerHost.GetDesigner(this.Parent)).Update();
			}
		}

		// Token: 0x0600E35B RID: 58203 RVA: 0x00325C1C File Offset: 0x00323E1C
		internal void SetDataGroupColumn(string columnName)
		{
			this.DataGroupColumn = columnName;
		}

		// Token: 0x0600E35C RID: 58204 RVA: 0x00325C28 File Offset: 0x00323E28
		internal TextRenderingHint GetTextQuality()
		{
			switch (this.Appearance.TextQuality)
			{
			case TextQuality.SystemDefault:
				return TextRenderingHint.SystemDefault;
			case TextQuality.SingleBitPerPixel:
				return TextRenderingHint.SingleBitPerPixel;
			case TextQuality.SingleBitPerPixelGridFit:
				return TextRenderingHint.SingleBitPerPixelGridFit;
			case TextQuality.AntiAlias:
				return TextRenderingHint.AntiAlias;
			case TextQuality.AntiAliasGridFit:
				return TextRenderingHint.AntiAliasGridFit;
			case TextQuality.ClearTypeGridFit:
				return TextRenderingHint.ClearTypeGridFit;
			default:
				return TextRenderingHint.SystemDefault;
			}
		}

		// Token: 0x0600E35D RID: 58205 RVA: 0x00325C70 File Offset: 0x00323E70
		internal SmoothingMode GetImageQuality()
		{
			switch (this.Appearance.ImageQuality)
			{
			case ImageQuality.Default:
				return SmoothingMode.Default;
			case ImageQuality.AntiAlias:
				return SmoothingMode.AntiAlias;
			case ImageQuality.HighQuality:
				return SmoothingMode.HighQuality;
			case ImageQuality.None:
				return SmoothingMode.None;
			default:
				return SmoothingMode.Default;
			}
		}

		// Token: 0x0600E35E RID: 58206 RVA: 0x00325CAA File Offset: 0x00323EAA
		internal bool OnlyPieSeries()
		{
			return this.Series.OnlyPieSeries();
		}

		// Token: 0x0600E35F RID: 58207 RVA: 0x00325CB7 File Offset: 0x00323EB7
		internal string MapPath(string filePath)
		{
			return this.chartComponent.MapPath(filePath);
		}

		// Token: 0x0600E360 RID: 58208 RVA: 0x00325CC8 File Offset: 0x00323EC8
		internal void InitDesignTime()
		{
			if (!this.chartDesignTime)
			{
				return;
			}
			this.OriginalSeriesCollection.Clear();
			foreach (ChartSeries item in this.chartSeriesCollection)
			{
				this.chartOriginalSeriesCollection.Add(item);
			}
			int num = this.chartSeriesCollection.GetMaxItemsCount();
			if (num < 7)
			{
				num = 7;
			}
			for (int i = 0; i < this.chartSeriesCollection.Count; i++)
			{
				ChartSeries chartSeries = this.chartSeriesCollection[i];
				ChartSeries chartSeries2;
				if (i == this.DesignTimeSeriesCollection.Count)
				{
					chartSeries2 = (ChartSeries)chartSeries.Clone();
					if (chartSeries2.Name == "__def__")
					{
						chartSeries2.Name = "Series 1";
					}
					chartSeries2.Parent.Parent = this;
					this.DesignTimeSeriesCollection.Add(chartSeries2);
				}
				else
				{
					chartSeries2 = this.DesignTimeSeriesCollection[i];
					chartSeries2.CopyFrom(chartSeries);
					chartSeries2.Parent.Parent = this;
					foreach (ChartSeriesItem chartSeriesItem in chartSeries2.Items)
					{
						chartSeriesItem.chartSeriesItemAppearance = new StyleSeriesItem();
					}
				}
				if (chartSeries.IsDataBound && chartSeries.Items.Count != 0)
				{
					chartSeries2.CopyItems(chartSeries);
					chartSeries2.Parent.Parent = this;
				}
				else if (chartSeries.Items.Count == 0)
				{
					if (chartSeries2.Items.Count != num)
					{
						chartSeries2.Items.Clear();
						int num2 = 0;
						while (chartSeries2.Items.Count < num)
						{
							ChartDesignTimeSeriesItem item2 = new ChartDesignTimeSeriesItem("Item " + ++num2, chartSeries2);
							chartSeries2.Items.Add(item2);
						}
					}
					foreach (ChartSeriesItem chartSeriesItem2 in chartSeries2.Items)
					{
						ChartDesignTimeSeriesItem chartDesignTimeSeriesItem = chartSeriesItem2 as ChartDesignTimeSeriesItem;
						if (chartDesignTimeSeriesItem != null)
						{
							chartDesignTimeSeriesItem.SetCorrectValues();
						}
					}
					this.chartSeriesCollection[i] = chartSeries2;
				}
			}
		}

		// Token: 0x0600E361 RID: 58209 RVA: 0x00325F28 File Offset: 0x00324128
		internal void FinalizeDesignTime()
		{
			if (!this.chartDesignTime)
			{
				return;
			}
			this.chartPlotArea.ClearAutoPropertiesForAxisItems();
			this.chartSeriesCollection.Clear();
			if (this.chartOriginalSeriesCollection != null)
			{
				foreach (ChartSeries chartSeries in this.chartOriginalSeriesCollection)
				{
					if (chartSeries.IsDataBound)
					{
						ChartSeries chartSeries2 = new ChartSeries();
						chartSeries2.CopyFrom(chartSeries);
						this.chartSeriesCollection.Add(chartSeries2);
					}
					else
					{
						this.chartSeriesCollection.Add(chartSeries);
					}
				}
			}
		}

		// Token: 0x0600E362 RID: 58210 RVA: 0x00325FC4 File Offset: 0x003241C4
		internal void ClearSkin(object skinContainer)
		{
			PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(skinContainer);
			foreach (object obj in properties)
			{
				PropertyDescriptor propertyDescriptor = (PropertyDescriptor)obj;
				if (propertyDescriptor.Attributes[typeof(SkinnablePropertyAttribute)] != null && !this.IsDefaultValue(propertyDescriptor, skinContainer))
				{
					if (null != propertyDescriptor.PropertyType.GetInterface("ICollection"))
					{
						IEnumerable enumerable = this.GetPropertyValue(propertyDescriptor, skinContainer) as IEnumerable;
						if (enumerable == null)
						{
							continue;
						}
						ColorBlend colorBlend = enumerable as ColorBlend;
						if (colorBlend != null)
						{
							colorBlend.Clear();
							continue;
						}
						using (IEnumerator enumerator2 = enumerable.GetEnumerator())
						{
							while (enumerator2.MoveNext())
							{
								object skinContainer2 = enumerator2.Current;
								this.ClearSkin(skinContainer2);
							}
							continue;
						}
					}
					if (propertyDescriptor.Converter.GetType() != typeof(TypeConverter) && propertyDescriptor.Converter.GetType() != typeof(ExpandableObjectConverter))
					{
						propertyDescriptor.SetValue(skinContainer, this.GetDefaultPropertyValue(propertyDescriptor));
					}
					else
					{
						this.ClearSkin(this.GetPropertyValue(propertyDescriptor, skinContainer));
					}
				}
			}
		}

		// Token: 0x0600E363 RID: 58211 RVA: 0x00326144 File Offset: 0x00324344
		private bool IsDefaultValue(PropertyDescriptor propDescriptor, object styleContainer)
		{
			object defaultPropertyValue = this.GetDefaultPropertyValue(propDescriptor);
			return defaultPropertyValue != null && defaultPropertyValue.Equals(this.GetPropertyValue(propDescriptor, styleContainer));
		}

		// Token: 0x0600E364 RID: 58212 RVA: 0x0032616C File Offset: 0x0032436C
		private object GetDefaultPropertyValue(PropertyDescriptor propDescriptor)
		{
			DefaultValueAttribute defaultValueAttribute = propDescriptor.Attributes[typeof(DefaultValueAttribute)] as DefaultValueAttribute;
			if (defaultValueAttribute != null)
			{
				return defaultValueAttribute.Value;
			}
			return null;
		}

		// Token: 0x0600E365 RID: 58213 RVA: 0x0032619F File Offset: 0x0032439F
		private object GetPropertyValue(PropertyDescriptor propDescriptor, object styleContainer)
		{
			return propDescriptor.GetValue(styleContainer);
		}

		// Token: 0x0600E366 RID: 58214 RVA: 0x003261A8 File Offset: 0x003243A8
		internal void LoadSkin(object skinContainer, TextWriter text)
		{
			if (this.SkinsOverrideStyles)
			{
				this.ClearSkin(this);
			}
			TextReader textReader = new StringReader(text.ToString());
			StyleSerializer styleSerializer = new StyleSerializer();
			styleSerializer.LoadXMLString(textReader.ReadToEnd(), skinContainer);
		}

		// Token: 0x0600E367 RID: 58215 RVA: 0x003261E4 File Offset: 0x003243E4
		internal TextWriter SaveSkin(object skinContainer)
		{
			TextWriter textWriter = new StringWriter();
			StyleSerializer styleSerializer = new StyleSerializer();
			textWriter.Write(styleSerializer.SaveXMLString(skinContainer));
			return textWriter;
		}

		// Token: 0x0600E368 RID: 58216 RVA: 0x0032620C File Offset: 0x0032440C
		internal void LoadChart(object skinContainer, TextReader reader)
		{
			this.Init();
			new StyleSerializer
			{
				ProcessAllProperties = true
			}.LoadXMLString(reader.ReadToEnd(), skinContainer);
		}

		// Token: 0x0600E369 RID: 58217 RVA: 0x0032623C File Offset: 0x0032443C
		internal TextWriter SaveChart(object skinContainer)
		{
			TextWriter textWriter = new StringWriter();
			textWriter.Write(new StyleSerializer
			{
				ProcessAllProperties = true
			}.SaveXMLString(skinContainer));
			return textWriter;
		}

		// Token: 0x0600E36A RID: 58218 RVA: 0x0032626C File Offset: 0x0032446C
		[SuppressMessage("Microsoft.Security", "CA2116:AptcaMethodsShouldOnlyCallAptcaMethods")]
		internal string ResolvePhysicalLocation(string path)
		{
			ISite site = this.chartComponent.Site;
			if (site == null)
			{
				return path;
			}
			IWebApplication webApplication = (IWebApplication)site.GetService(typeof(IWebApplication));
			IProjectItem projectItemFromUrl = webApplication.GetProjectItemFromUrl(path);
			if (projectItemFromUrl == null)
			{
				return path.Replace("~/", webApplication.RootProjectItem.PhysicalPath);
			}
			if (!projectItemFromUrl.PhysicalPath.EndsWith(Path.DirectorySeparatorChar.ToString()))
			{
				return projectItemFromUrl.PhysicalPath + Path.DirectorySeparatorChar;
			}
			return projectItemFromUrl.PhysicalPath;
		}

		// Token: 0x0600E36B RID: 58219 RVA: 0x003262F8 File Offset: 0x003244F8
		[SuppressMessage("Microsoft.Security", "CA2116:AptcaMethodsShouldOnlyCallAptcaMethods")]
		internal IDataSource LocalDataFilePathToGlobal(AccessDataSource ids)
		{
			IWebApplication webApplication = (IWebApplication)this.chartComponent.Site.GetService(typeof(IWebApplication));
			if (webApplication != null)
			{
				IProjectItem projectItemFromUrl = webApplication.GetProjectItemFromUrl(ids.DataFile);
				if (projectItemFromUrl != null)
				{
					ids.DataFile = projectItemFromUrl.PhysicalPath;
				}
			}
			return ids;
		}

		// Token: 0x0600E36C RID: 58220 RVA: 0x00326348 File Offset: 0x00324548
		[SuppressMessage("Microsoft.Security", "CA2116:AptcaMethodsShouldOnlyCallAptcaMethods")]
		internal IDataSource LocalDataFilePathToGlobal(XmlDataSource ids)
		{
			IWebApplication webApplication = (IWebApplication)this.chartComponent.Site.GetService(typeof(IWebApplication));
			if (webApplication != null)
			{
				IProjectItem projectItemFromUrl = webApplication.GetProjectItemFromUrl(ids.DataFile);
				if (projectItemFromUrl != null)
				{
					ids.DataFile = projectItemFromUrl.PhysicalPath;
				}
			}
			return ids;
		}

		// Token: 0x0600E36D RID: 58221 RVA: 0x00326398 File Offset: 0x00324598
		internal void Init()
		{
			this.chartOrderList = new List<IOrdering>();
			this.chartPlotArea = new ChartPlotArea(this);
			this.Add(this.chartPlotArea);
			this.chartSeriesCollection = new ChartSeriesCollection(this);
			this.chartLegend = new ChartLegend(this, this);
			this.Add(this.chartLegend);
			this.chartTitle = new ChartTitle(this, this);
			this.Add(this.chartTitle);
			this.appearance = new StyleChart(this);
			this.chartCustomPalettes = new CustomPalettesCollection();
			this.chartDataManager = new DataManager(this);
			this.chartCustomFigures = new CustomFiguresCollection();
			this.chartFigures = new FiguresCollection(this);
			this.chartSkinsCollection = new ChartSkinsCollection();
		}

		// Token: 0x0600E36E RID: 58222 RVA: 0x0032644C File Offset: 0x0032464C
		internal void CalculateChart()
		{
			int num = (int)this.Appearance.Dimensions.Width.PixelValue;
			int num2 = (int)this.Appearance.Dimensions.Height.PixelValue;
			if (num > 0 && num2 > 0)
			{
				using (RenderEngine renderEngine = new RenderEngine(this, (float)num, (float)num2))
				{
					this.Legend.BindSeriesToLegend(renderEngine);
				}
			}
		}

		// Token: 0x0600E36F RID: 58223 RVA: 0x003264C4 File Offset: 0x003246C4
		internal void ReCalculateChart()
		{
			int num = (int)this.Appearance.Dimensions.Width.PixelValue;
			int num2 = (int)this.Appearance.Dimensions.Height.PixelValue;
			if (num > 0 && num2 > 0)
			{
				using (RenderEngine renderEngine = new RenderEngine(this, (float)num, (float)num2))
				{
					renderEngine.InitializeChartElements();
					renderEngine.CalculateElementsForRender();
				}
			}
		}

		// Token: 0x0600E370 RID: 58224 RVA: 0x0032653C File Offset: 0x0032473C
		internal void OnBeforeLayout(object chart, EventArgs args)
		{
			if (this.BeforeLayoutEventHandler != null)
			{
				this.BeforeLayoutEventHandler(chart, args);
			}
		}

		// Token: 0x0600E371 RID: 58225 RVA: 0x00326553 File Offset: 0x00324753
		internal void OnPrePaint(object chart, EventArgs args)
		{
			if (this.PrePaintEventHandler != null)
			{
				this.PrePaintEventHandler(chart, args);
			}
		}

		// Token: 0x0600E372 RID: 58226 RVA: 0x0032656A File Offset: 0x0032476A
		internal System.Drawing.Image GetImage()
		{
			return this.GetImage((int)this.Appearance.Dimensions.Width.PixelValue, (int)this.Appearance.Dimensions.Height.PixelValue);
		}

		// Token: 0x0600E373 RID: 58227 RVA: 0x0032659E File Offset: 0x0032479E
		internal System.Drawing.Image GetImage(Telerik.Charting.Styles.Unit width, Telerik.Charting.Styles.Unit height)
		{
			return this.GetImage((int)width.PixelValue, (int)height.PixelValue);
		}

		// Token: 0x0600E374 RID: 58228 RVA: 0x003265B4 File Offset: 0x003247B4
		internal System.Drawing.Image GetImage(int width, int height)
		{
			this.Skin = this.Skin;
			if (width > 0 && height > 0)
			{
				RenderEngine renderEngine = null;
				if (this.BitmapResolution > 0f)
				{
					renderEngine = new RenderEngine(this, (float)width, (float)height, this.BitmapResolution);
				}
				else
				{
					renderEngine = new RenderEngine(this, (float)width, (float)height);
				}
				try
				{
					if (renderEngine.image != null)
					{
						this.CheckLimitations();
						renderEngine.InitializeChartElements();
						this.OnBeforeLayout(this, EventArgs.Empty);
						renderEngine.CalculateElementsForRender();
						this.OnPrePaint(this, null);
						return renderEngine.Render(true);
					}
					renderEngine = new RenderEngine(this, DimensionsChart.defWidth.PixelValue, DimensionsChart.defHeight.PixelValue);
					throw new ChartException("Image could not be created");
				}
				catch (ChartException ex)
				{
					return this.GetException(renderEngine, ex);
				}
				catch (Exception inner)
				{
					return this.GetException(renderEngine, new ChartException("An Unexpected error has occurred. Please review the InnerException for more information how to resolve the problem.", inner));
				}
				finally
				{
					renderEngine.Dispose();
					renderEngine = null;
				}
			}
			return null;
		}

		// Token: 0x0600E375 RID: 58229 RVA: 0x003266BC File Offset: 0x003248BC
		internal System.Drawing.Image GetStaticArea(int width, int height, bool withXAxis, bool withYAxis, bool withYAxis2)
		{
			this.Skin = this.Skin;
			if (width > 0 && height > 0)
			{
				RenderEngine renderEngine = new RenderEngine(this, (float)width, (float)height);
				try
				{
					this.CheckLimitations();
					renderEngine.InitializeChartElements();
					this.OnBeforeLayout(this, EventArgs.Empty);
					this.chartPlotArea.PrepareForScale();
					renderEngine.CalculateElementsForRender();
					this.OnPrePaint(this, null);
					return renderEngine.RenderChartArea(true, true, true, true, false, withXAxis, withYAxis, withYAxis2);
				}
				catch (ChartException ex)
				{
					return this.GetException(renderEngine, ex);
				}
				catch (Exception inner)
				{
					return this.GetException(renderEngine, new ChartException("An Unexpected error has occurred. Please review the InnerException for more information how to resolve the problem.", inner));
				}
				finally
				{
					renderEngine.Dispose();
					renderEngine = null;
				}
			}
			return null;
		}

		// Token: 0x0600E376 RID: 58230 RVA: 0x00326784 File Offset: 0x00324984
		internal System.Drawing.Image GetPlotArea(int width, int height, float xScale, float yScale, Telerik.Charting.Styles.Unit clientWidth, Telerik.Charting.Styles.Unit clientHeight, Telerik.Charting.Styles.Unit top, Telerik.Charting.Styles.Unit left)
		{
			this.Skin = this.Skin;
			if (width > 0 && height > 0)
			{
				this.ReCalculateChart();
				float scaledImageWidth = this.GetScaledImageWidth(xScale, yScale);
				float scaledImageHeight = this.GetScaledImageHeight(xScale, yScale);
				RenderEngine renderEngine = new RenderEngine(this, (float)((int)Math.Round((double)scaledImageWidth)), (float)((int)Math.Round((double)scaledImageHeight)), false);
				renderEngine.InitGraphics(width, height);
				try
				{
					ChartPlotArea chartPlotArea = this.chartPlotArea;
					this.PrepareForScale(xScale, yScale);
					renderEngine.InitializeChartElements();
					renderEngine.ScalePlotArea(xScale, yScale);
					float num = (float)Math.Round((double)chartPlotArea.Appearance.Position.X);
					float num2 = (float)Math.Round((double)chartPlotArea.Appearance.Position.Y);
					int width2 = (int)Math.Round((double)clientWidth.PixelValue);
					int height2 = (int)Math.Round((double)clientHeight.PixelValue);
					if (this.SeriesOrientation == ChartSeriesOrientation.Vertical)
					{
						num += chartPlotArea.YAxis.Appearance.Width;
					}
					else
					{
						num += chartPlotArea.XAxis.Appearance.Width;
						if (chartPlotArea.YAxis2.IsVisible())
						{
							num2 += chartPlotArea.YAxis2.Appearance.Width;
						}
					}
					renderEngine.InitGraphics(width2, height2);
					renderEngine.graphics.TranslateTransformDefault(-num - left.PixelValue, -num2 - top.PixelValue);
					System.Drawing.Image result = renderEngine.RenderPlotArea(true);
					this.RestoreAfterScale(width, height);
					return result;
				}
				catch (ChartException ex)
				{
					return this.GetException(renderEngine, ex);
				}
				catch (Exception inner)
				{
					return this.GetException(renderEngine, new ChartException("An Unexpected error has occurred. Please review the InnerException for more information how to resolve the problem.", inner));
				}
				finally
				{
					renderEngine.Dispose();
					renderEngine = null;
				}
			}
			return null;
		}

		// Token: 0x0600E377 RID: 58231 RVA: 0x00326974 File Offset: 0x00324B74
		private float GetScaledImageWidth(float xScale, float yScale)
		{
			float num = (this.SeriesOrientation == ChartSeriesOrientation.Vertical) ? xScale : yScale;
			return (this.chartPlotArea.Appearance.Dimensions.Width.PixelValue + this.chartPlotArea.Appearance.Dimensions.Margins.Left.PixelValue + this.chartPlotArea.Appearance.Dimensions.Margins.Right.PixelValue) * num;
		}

		// Token: 0x0600E378 RID: 58232 RVA: 0x003269EC File Offset: 0x00324BEC
		private float GetScaledImageHeight(float xScale, float yScale)
		{
			float num = (this.SeriesOrientation == ChartSeriesOrientation.Vertical) ? yScale : xScale;
			return (this.chartPlotArea.Appearance.Dimensions.Height.PixelValue + this.chartPlotArea.Appearance.Dimensions.Margins.Top.PixelValue + this.chartPlotArea.Appearance.Dimensions.Margins.Bottom.PixelValue) * num;
		}

		// Token: 0x0600E379 RID: 58233 RVA: 0x00326A62 File Offset: 0x00324C62
		private void PrepareForScale(float xScale, float yScale)
		{
			this.chartPlotArea.PrepareForScale(xScale, yScale);
			if (xScale > 1f)
			{
				this.Series.PrepareForScale();
			}
		}

		// Token: 0x0600E37A RID: 58234 RVA: 0x00326A84 File Offset: 0x00324C84
		private void RestoreAfterScale(int width, int height)
		{
			this.Appearance.Dimensions.SetDimensions((float)width, (float)height);
			this.chartPlotArea.RestoreAfterScale();
			this.Series.RestoreAfterScale();
		}

		// Token: 0x1700457B RID: 17787
		// (get) Token: 0x0600E37B RID: 58235 RVA: 0x00326AB0 File Offset: 0x00324CB0
		internal bool ScaleEnabled
		{
			get
			{
				return this.Parent != null && this.Parent is IChartSupportsScaling && ((IChartSupportsScaling)this.Parent).ScaleEnabled;
			}
		}

		// Token: 0x0600E37C RID: 58236 RVA: 0x00326ADC File Offset: 0x00324CDC
		internal void CheckLimitations()
		{
			if (this.ScaleEnabled)
			{
				if (this.chartPlotArea.DataTable.Visible)
				{
					throw new ChartException("DataTable feature is not supported with client-side scrolling and zooming enabled");
				}
				if (this.Series.Count == 1 || this.Series.OnlyPieSeries() || this.Series.IsUnScalable)
				{
					foreach (ChartSeries chartSeries in this.Series)
					{
						if (!chartSeries.IsScalable)
						{
							throw new ChartException(string.Format("{0} series is not supported with client-side scrolling and zooming enabled. Series: {1}", chartSeries.Type.ToString(), chartSeries.Name));
						}
					}
				}
			}
			if (this.AutoLayoutWrapper)
			{
				if (!this.chartPlotArea.Appearance.Position.Auto)
				{
					throw new ChartException("AutoLayout feature is not supported with PlotArea.Appearance.Position.Auto = false");
				}
				if (this.chartPlotArea.DataTable.Appearance.RenderType != TableRenderType.PlotAreaRelative && !this.chartPlotArea.DataTable.Appearance.Position.Auto)
				{
					throw new ChartException("AutoLayout feature is not supported with PlotArea.DataTable.Appearance.Position.Auto = false");
				}
			}
		}

		// Token: 0x0600E37D RID: 58237 RVA: 0x00326C0C File Offset: 0x00324E0C
		internal void PrepareForAutoLayout()
		{
			if (this.AutoLayoutWrapper)
			{
				if (this.chartPlotArea.DataTable.IsVisible)
				{
					this.chartPlotArea.DataTable.Appearance.SetAutoLayoutDefaults();
				}
				this.chartPlotArea.Appearance.SetAutoLayoutDefaults();
				this.Legend.Appearance.SetAutoLayoutDefaults();
				if (this.Legend.Appearance.Location == LabelLocation.OutsidePlotArea)
				{
					this.Legend.Appearance.Position.SetPositionForAutoLayout();
				}
				this.ChartTitle.Appearance.SetAutoLayoutDefaults();
				this.ChartTitle.Appearance.Position.SetPositionForAutoLayout();
			}
		}

		// Token: 0x0600E37E RID: 58238 RVA: 0x00326CB8 File Offset: 0x00324EB8
		internal void RestoreAutoLayoutChanges()
		{
			if (this.AutoLayoutWrapper)
			{
				this.chartPlotArea.DataTable.Appearance.RestoreInitialValues();
				this.chartPlotArea.Appearance.RestoreDimensions(true);
				this.ChartTitle.Appearance.RestoreInitialValues();
				this.Legend.Appearance.RestoreInitialValues();
			}
			this.chartPlotArea.XAxis.RestoreLabelPosition();
			this.chartPlotArea.YAxis.RestoreLabelPosition();
			this.chartPlotArea.YAxis2.RestoreLabelPosition();
		}

		// Token: 0x0600E37F RID: 58239 RVA: 0x00326D44 File Offset: 0x00324F44
		internal System.Drawing.Image GetAxis(int width, int height, float xScale, float yScale, ChartAxisType axisType)
		{
			this.Skin = this.Skin;
			if (width > 0 && height > 0)
			{
				float num = (float)width;
				float num2 = (float)height;
				if (axisType == ChartAxisType.XAxis)
				{
					yScale = 1f;
				}
				else
				{
					xScale = 1f;
				}
				bool flag = this.SeriesOrientation == ChartSeriesOrientation.Vertical;
				if ((axisType == ChartAxisType.XAxis && flag) || (axisType != ChartAxisType.XAxis && !flag))
				{
					num = this.GetScaledImageWidth(xScale, yScale);
				}
				else
				{
					num2 = this.GetScaledImageHeight(xScale, yScale);
				}
				if (num > 0f && num2 > 0f)
				{
					RenderEngine renderEngine = new RenderEngine(this, num, num2, true);
					ChartPlotArea chartPlotArea = this.chartPlotArea;
					try
					{
						this.PrepareForScale(xScale, yScale);
						renderEngine.InitializeChartElements();
						renderEngine.ScalePlotArea(xScale, yScale);
						ChartAxis chartAxis = null;
						switch (axisType)
						{
						case ChartAxisType.XAxis:
							chartAxis = chartPlotArea.XAxis;
							break;
						case ChartAxisType.YAxis:
							chartAxis = chartPlotArea.YAxis;
							break;
						case ChartAxisType.YAxis2:
							chartAxis = chartPlotArea.YAxis2;
							break;
						}
						System.Drawing.Image result = null;
						if (chartAxis != null)
						{
							RectangleF clientRectangle = chartAxis.GetClientRectangle();
							if (clientRectangle.Width > 0f && clientRectangle.Height > 0f)
							{
								renderEngine.InitGraphics((int)Math.Round((double)clientRectangle.Width), (int)Math.Round((double)clientRectangle.Height));
								renderEngine.graphics.TranslateTransformDefault((float)(-(float)((int)Math.Round((double)clientRectangle.X))), (float)(-(float)((int)Math.Round((double)clientRectangle.Y))));
								result = renderEngine.RenderAxis(true, axisType);
							}
						}
						this.RestoreAfterScale(width, height);
						return result;
					}
					catch (ChartException ex)
					{
						return this.GetException(renderEngine, ex);
					}
					catch (Exception inner)
					{
						return this.GetException(renderEngine, new ChartException("An Unexpected error has occurred. Please review the InnerException for more information how to resolve the problem.", inner));
					}
					finally
					{
						renderEngine.Dispose();
						renderEngine = null;
					}
				}
			}
			return null;
		}

		// Token: 0x0600E380 RID: 58240 RVA: 0x00326F44 File Offset: 0x00325144
		internal System.Drawing.Image GetException(RenderEngine renderEngine, Exception ex)
		{
			if (this.DesignTime)
			{
				this.chartLegend.ClearBoundItems(false);
				this.FinalizeDesignTime();
				renderEngine.graphics.FillRectangle(new SolidBrush(Color.White), 0f, 0f, this.Appearance.Dimensions.Width.PixelValue, this.Appearance.Dimensions.Height.PixelValue);
				string s = ChartException.WrappedByWidth(renderEngine.graphics, ex.Message, DefaultValues.DEFAULT_TEXT_FONT, this.Appearance.Dimensions.Width.PixelValue);
				renderEngine.graphics.ResetClip();
				renderEngine.graphics.Clear(Color.White);
				renderEngine.graphics.DrawString(s, DefaultValues.DEFAULT_TEXT_FONT, new SolidBrush(Color.Red), 0f, 0f);
				return renderEngine.image;
			}
			throw ex;
		}

		// Token: 0x0600E381 RID: 58241 RVA: 0x0032702C File Offset: 0x0032522C
		internal IActiveRegion CallRegionEvent(int x, int y)
		{
			PointF point = new PointF((float)x, (float)y);
			return this.CallRegionEvent(point);
		}

		// Token: 0x0600E382 RID: 58242 RVA: 0x0032704C File Offset: 0x0032524C
		internal IActiveRegion CallRegionEvent(float x, float y)
		{
			PointF point = new PointF(x, y);
			return this.CallRegionEvent(point);
		}

		// Token: 0x0600E383 RID: 58243 RVA: 0x00327069 File Offset: 0x00325269
		internal IActiveRegion CallRegionEvent(Point point)
		{
			return this.CallRegionEvent(point);
		}

		// Token: 0x0600E384 RID: 58244 RVA: 0x00327077 File Offset: 0x00325277
		internal IActiveRegion CallRegionEvent(PointF point)
		{
			return this.CallRegionEvent(point, this);
		}

		// Token: 0x0600E385 RID: 58245 RVA: 0x00327081 File Offset: 0x00325281
		internal ChartSeries GetSeries(string name)
		{
			return this.chartSeriesCollection.GetByName(name);
		}

		// Token: 0x0600E386 RID: 58246 RVA: 0x0032708F File Offset: 0x0032528F
		internal ChartSeries GetSeries(int index)
		{
			return this.chartSeriesCollection[index];
		}

		// Token: 0x0600E387 RID: 58247 RVA: 0x003270A0 File Offset: 0x003252A0
		internal ChartSeries GetSeries(Color seriesColor)
		{
			foreach (ChartSeries chartSeries in this.chartSeriesCollection)
			{
				if (chartSeries.Appearance.FillStyle.MainColor == seriesColor)
				{
					return chartSeries;
				}
			}
			return null;
		}

		// Token: 0x0600E388 RID: 58248 RVA: 0x00327108 File Offset: 0x00325308
		internal void AddChartSeries(ChartSeries series)
		{
			if (this.chartPlotArea != null)
			{
				series.chartSeriesPlotArea = this.chartPlotArea;
			}
			this.chartSeriesCollection.Add(series);
		}

		// Token: 0x0600E389 RID: 58249 RVA: 0x0032712A File Offset: 0x0032532A
		internal void AddSeries(ChartSeries series)
		{
			if (this.chartPlotArea != null)
			{
				series.chartSeriesPlotArea = this.chartPlotArea;
			}
			this.chartSeriesCollection.Add(series);
		}

		// Token: 0x0600E38A RID: 58250 RVA: 0x0032714C File Offset: 0x0032534C
		internal void AddSeries(ChartSeriesCollection chartSeries)
		{
			foreach (ChartSeries chartSeries2 in chartSeries)
			{
				if (this.chartPlotArea != null)
				{
					chartSeries2.chartSeriesPlotArea = this.chartPlotArea;
				}
				this.chartSeriesCollection.Add(chartSeries2);
			}
		}

		// Token: 0x0600E38B RID: 58251 RVA: 0x003271B0 File Offset: 0x003253B0
		internal void AddSeries(ChartSeries[] chartSeries)
		{
			foreach (ChartSeries chartSeries2 in chartSeries)
			{
				if (this.chartPlotArea != null)
				{
					chartSeries2.chartSeriesPlotArea = this.chartPlotArea;
				}
				this.chartSeriesCollection.Add(chartSeries2);
			}
		}

		// Token: 0x0600E38C RID: 58252 RVA: 0x003271F4 File Offset: 0x003253F4
		internal void AddSeries(List<ChartSeries> seriesList)
		{
			foreach (ChartSeries chartSeries in seriesList)
			{
				if (this.chartPlotArea != null)
				{
					chartSeries.chartSeriesPlotArea = this.chartPlotArea;
				}
				this.chartSeriesCollection.Add(chartSeries);
			}
		}

		// Token: 0x0600E38D RID: 58253 RVA: 0x0032725C File Offset: 0x0032545C
		internal void AddSeries(ChartSeries chartSeries, params ChartSeries[] chartSeriesArray)
		{
			if (this.chartPlotArea != null)
			{
				chartSeries.chartSeriesPlotArea = this.chartPlotArea;
			}
			this.chartSeriesCollection.Add(chartSeries);
			foreach (ChartSeries chartSeries2 in chartSeriesArray)
			{
				if (this.chartPlotArea != null)
				{
					chartSeries2.chartSeriesPlotArea = this.chartPlotArea;
				}
				this.chartSeriesCollection.Add(chartSeries2);
			}
		}

		// Token: 0x0600E38E RID: 58254 RVA: 0x003272BD File Offset: 0x003254BD
		internal void RemoveAllSeries()
		{
			this.chartSeriesCollection.Clear();
		}

		// Token: 0x0600E38F RID: 58255 RVA: 0x003272CC File Offset: 0x003254CC
		internal void RemoveSeries(ChartSeries chartSeries, params ChartSeries[] chartSeriesArray)
		{
			this.chartSeriesCollection.Remove(chartSeries);
			foreach (ChartSeries item in chartSeriesArray)
			{
				this.chartSeriesCollection.Remove(item);
			}
		}

		// Token: 0x0600E390 RID: 58256 RVA: 0x00327308 File Offset: 0x00325508
		internal void RemoveSeries(string seriesName, params string[] seriesNames)
		{
			this.chartSeriesCollection.Remove(this.chartSeriesCollection.GetByName(seriesName));
			foreach (string name in seriesNames)
			{
				this.chartSeriesCollection.Remove(this.chartSeriesCollection.GetByName(name));
			}
		}

		// Token: 0x0600E391 RID: 58257 RVA: 0x0032735C File Offset: 0x0032555C
		internal void RemoveSeriesAt(int index, params int[] indexes)
		{
			this.chartSeriesCollection.RemoveAt(index);
			foreach (int index2 in indexes)
			{
				this.chartSeriesCollection.RemoveAt(index2);
			}
		}

		// Token: 0x1700457C RID: 17788
		// (get) Token: 0x0600E392 RID: 58258 RVA: 0x00327395 File Offset: 0x00325595
		[Browsable(false)]
		public List<IOrdering> OrderList
		{
			get
			{
				return this.chartOrderList;
			}
		}

		// Token: 0x1700457D RID: 17789
		// (get) Token: 0x0600E393 RID: 58259 RVA: 0x003273A0 File Offset: 0x003255A0
		[Browsable(false)]
		public int NextPosition
		{
			get
			{
				IOrdering item = null;
				foreach (IOrdering ordering in this.chartOrderList)
				{
					item = ordering;
				}
				return this.chartOrderList.IndexOf(item) + 1;
			}
		}

		// Token: 0x0600E394 RID: 58260 RVA: 0x00327400 File Offset: 0x00325600
		public int GetOrder(IOrdering element)
		{
			return this.chartOrderList.IndexOf(element);
		}

		// Token: 0x0600E395 RID: 58261 RVA: 0x0032740E File Offset: 0x0032560E
		public void Add(IOrdering element)
		{
			element.Container = this;
			this.chartOrderList.Add(element);
		}

		// Token: 0x0600E396 RID: 58262 RVA: 0x00327423 File Offset: 0x00325623
		public void Insert(int order, IOrdering element)
		{
			element.Container = this;
			this.chartOrderList.Insert(order, element);
		}

		// Token: 0x0600E397 RID: 58263 RVA: 0x00327439 File Offset: 0x00325639
		public void Remove(IOrdering element)
		{
			this.chartOrderList.Remove(element);
		}

		// Token: 0x0600E398 RID: 58264 RVA: 0x00327448 File Offset: 0x00325648
		public void RemoveAt(int index)
		{
			this.chartOrderList.RemoveAt(index);
		}

		// Token: 0x0600E399 RID: 58265 RVA: 0x00327458 File Offset: 0x00325658
		public void ReIndex()
		{
			List<IOrdering> list = new List<IOrdering>();
			int count = this.chartOrderList.Count;
			for (int i = 0; i < count; i++)
			{
				IOrdering ordering = this.chartOrderList[i];
				if (ordering != null)
				{
					list.Insert(i++, ordering);
				}
			}
			this.chartOrderList = list;
		}

		// Token: 0x0600E39A RID: 58266 RVA: 0x003274A8 File Offset: 0x003256A8
		protected override void Dispose(bool disposing)
		{
			if (this.chartCustomFigures != null)
			{
				this.chartCustomFigures = null;
			}
			if (this.chartCustomPalettes != null)
			{
				this.chartCustomPalettes = null;
			}
			if (this.chartDataManager != null)
			{
				this.chartDataManager = null;
			}
			if (this.chartDesignTimeSeriesCollection != null)
			{
				this.chartDesignTimeSeriesCollection = null;
			}
			if (this.chartFigures != null)
			{
				this.chartFigures = null;
			}
			if (this.chartLegend != null)
			{
				this.chartLegend.Dispose();
				this.chartLegend = null;
			}
			if (this.chartOrderList != null)
			{
				this.chartOrderList = null;
			}
			if (this.chartOriginalSeriesCollection != null)
			{
				this.chartOriginalSeriesCollection = null;
			}
			if (this.chartPlotArea != null)
			{
				this.chartPlotArea.Dispose();
				this.chartPlotArea = null;
			}
			if (this.chartSeriesCollection != null)
			{
				this.chartSeriesCollection = null;
			}
			if (this.chartSkinsCollection != null)
			{
				this.chartSkinsCollection = null;
			}
			if (this.chartTitle != null)
			{
				this.chartTitle.Dispose();
				this.chartTitle = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600E39B RID: 58267 RVA: 0x00327594 File Offset: 0x00325794
		protected override void TrackViewState()
		{
			base.TrackViewState();
			((IChartingStateManager)this.Appearance).TrackViewState();
			((IChartingStateManager)this.chartCustomFigures).TrackViewState();
			((IChartingStateManager)this.chartCustomPalettes).TrackViewState();
			((IChartingStateManager)this.chartPlotArea).TrackViewState();
			((IChartingStateManager)this.chartLegend).TrackViewState();
			((IChartingStateManager)this.chartTitle).TrackViewState();
			((IChartingStateManager)this.chartSeriesCollection).TrackViewState();
		}

		// Token: 0x0600E39C RID: 58268 RVA: 0x003275F4 File Offset: 0x003257F4
		protected override void LoadViewState(object savedState)
		{
			object[] array = (object[])savedState;
			if (array != null)
			{
				base.LoadViewState(array[0]);
				((IChartingStateManager)this.Appearance).LoadViewState(array[1]);
				((IChartingStateManager)this.chartCustomFigures).LoadViewState(array[2]);
				((IChartingStateManager)this.chartCustomPalettes).LoadViewState(array[3]);
				((IChartingStateManager)this.chartPlotArea).LoadViewState(array[4]);
				((IChartingStateManager)this.chartLegend).LoadViewState(array[5]);
				if (this.chartLegend.Appearance.Location == LabelLocation.InsidePlotArea)
				{
					this.OrderList.Remove(this.chartLegend);
					this.chartPlotArea.OrderList.Add(this.chartLegend);
					this.chartLegend.Container = this.chartPlotArea;
				}
				((IChartingStateManager)this.chartTitle).LoadViewState(array[6]);
				((IChartingStateManager)this.chartSeriesCollection).LoadViewState(array[7]);
			}
		}

		// Token: 0x0600E39D RID: 58269 RVA: 0x003276C4 File Offset: 0x003258C4
		protected override object SaveViewState()
		{
			return new ArrayList
			{
				base.SaveViewState(),
				((IChartingStateManager)this.Appearance).SaveViewState(),
				((IChartingStateManager)this.chartCustomFigures).SaveViewState(),
				((IChartingStateManager)this.chartCustomPalettes).SaveViewState(),
				((IChartingStateManager)this.chartPlotArea).SaveViewState(),
				((IChartingStateManager)this.chartLegend).SaveViewState(),
				((IChartingStateManager)this.chartTitle).SaveViewState(),
				((IChartingStateManager)this.chartSeriesCollection).SaveViewState()
			}.ToArray();
		}

		// Token: 0x0600E39E RID: 58270 RVA: 0x00327768 File Offset: 0x00325968
		internal void CopyFrom(Chart baseChart)
		{
			this.Init();
			this.LoadViewState(baseChart.SaveViewState());
			this.DataManager.CopyFrom(baseChart.DataManager);
		}

		// Token: 0x04004182 RID: 16770
		private ChartTitle chartTitle;

		// Token: 0x04004183 RID: 16771
		private ChartLegend chartLegend;

		// Token: 0x04004184 RID: 16772
		private ChartPlotArea chartPlotArea;

		// Token: 0x04004185 RID: 16773
		private IChartComponent chartComponent;

		// Token: 0x04004186 RID: 16774
		private List<IOrdering> chartOrderList;

		// Token: 0x04004187 RID: 16775
		private bool chartDesignTime;

		// Token: 0x04004188 RID: 16776
		private DataManager chartDataManager;

		// Token: 0x04004189 RID: 16777
		internal ChartSeriesCollection chartSeriesCollection;

		// Token: 0x0400418A RID: 16778
		private ChartSeriesCollection chartDesignTimeSeriesCollection;

		// Token: 0x0400418B RID: 16779
		private ChartSeriesCollection chartOriginalSeriesCollection;

		// Token: 0x0400418C RID: 16780
		private CustomPalettesCollection chartCustomPalettes;

		// Token: 0x0400418D RID: 16781
		private CustomFiguresCollection chartCustomFigures;

		// Token: 0x0400418E RID: 16782
		private FiguresCollection chartFigures;

		// Token: 0x0400418F RID: 16783
		internal ChartSkinsCollection chartSkinsCollection;
	}
}
