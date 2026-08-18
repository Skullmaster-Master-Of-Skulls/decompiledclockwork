using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Licensing;
using Telerik.Web.UI.HtmlChart;
using Telerik.Web.UI.HtmlChart.Enums;
using Telerik.Web.UI.HtmlChart.Navigator;
using Telerik.Web.UI.HtmlChart.PlotArea;

namespace Telerik.Web.UI
{
	// Token: 0x020003E9 RID: 1001
	[ToolboxData("<{0}:RadHtmlChart runat=\"server\"></{0}:RadHtmlChart>")]
	[RequiredCss("Telerik.Web.UI.Skins.HTML5UI.dataviz.css", RenderMode.Lightweight, typeof(RadHtmlChart))]
	[EmbeddedSkin("HtmlChart", "Default", typeof(RadHtmlChart))]
	[RequiredScript(typeof(Html5DataVizCore))]
	[RequiredScript(typeof(Html5DataVizChart))]
	[Description("Telerik HtmlChart component")]
	[ClientScriptResource("Telerik.Web.UI.RadHtmlChart", "Telerik.Web.UI.HtmlChart.RadHtmlChart.js")]
	[TelerikToolboxCategory("Visualization")]
	[ToolboxBitmap(typeof(RadHtmlChart), "Telerik.Web.UI.HtmlChart.png")]
	[Designer("Telerik.Web.Design.RadHtmlChartDesigner, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4")]
	[EmbeddedSkin("HtmlChart", "Default")]
	[ParseChildren(ChildrenAsProperties = true)]
	[EmbeddedSkin("HtmlChart")]
	[LicenseProvider(typeof(TelerikLicenseProvider))]
	[EmbeddedSkin("HtmlChart")]
	[RequiredCss("Telerik.Web.UI.Skins.HTML5UI.dataviz.css", RenderMode.Classic, typeof(RadHtmlChart))]
	public class RadHtmlChart : RadDataBoundControl, ICallbackEventHandler
	{
		// Token: 0x06002491 RID: 9361 RVA: 0x00079628 File Offset: 0x00077828
		protected internal override void DescribeClientProperties(IScriptDescriptor descriptor)
		{
			base.DescribeProperty<string>(descriptor, "height", this.Height.ToString(CultureInfo.InvariantCulture), "");
			base.DescribeProperty<LoadDataInvocation>(descriptor, "invokeLoadData", this.InvokeLoadData, LoadDataInvocation.OnPageLoad);
			base.DescribeProperty<bool>(descriptor, "transitions", this.Transitions, true);
			base.DescribeProperty<string>(descriptor, "width", this.Width.ToString(CultureInfo.InvariantCulture), "");
			base.DescribeClientProperties(descriptor);
		}

		// Token: 0x06002492 RID: 9362 RVA: 0x000796AC File Offset: 0x000778AC
		protected internal override void DescribeClientEvents(IScriptDescriptor descriptor)
		{
			RadDataBoundControl.DescribeEvent(descriptor, "seriesClicked", this.OnClientSeriesClicked);
			RadDataBoundControl.DescribeEvent(descriptor, "seriesHovered", this.OnClientSeriesHovered);
			RadDataBoundControl.DescribeEvent(descriptor, "load", this.ClientEvents.OnLoad);
			RadDataBoundControl.DescribeEvent(descriptor, "kendoWidgetInitializing", this.ClientEvents.OnKendoWidgetInitializing);
			base.DescribeClientEvents(descriptor);
		}

		// Token: 0x06002493 RID: 9363 RVA: 0x00079710 File Offset: 0x00077910
		protected override void DescribeComponent(IScriptDescriptor descriptor)
		{
			base.DescribeComponent(descriptor);
			descriptor.AddScriptProperty("_clientEvents", this.ClientEvents.Serialize());
			this.Page.ClientScript.GetCallbackEventReference(this, null, null, null);
			descriptor.AddProperty("_uniqueId", this.UniqueID);
			descriptor.AddProperty("_layout", this.Layout.ToString().ToLower());
			descriptor.AddProperty("skin", base.RuntimeSkin);
			this.ConfigureDefaultVisibilitySettings();
			this.ConfigureStockChartSettings();
			this.DescribeStockChart(descriptor);
			string value = this.Appearance.Serialize();
			string value2 = this.ChartTitle.Serialize();
			string value3 = this.Legend.Serialize();
			string value4 = this.PlotArea.Serialize();
			string serializedSeries = this.GetSerializedSeries();
			if (!string.IsNullOrEmpty(value))
			{
				descriptor.AddProperty("_chartArea", value);
			}
			if (!string.IsNullOrEmpty(value2))
			{
				descriptor.AddProperty("_chartTitle", value2);
			}
			if (!string.IsNullOrEmpty(value3))
			{
				descriptor.AddProperty("_legend", value3);
			}
			if (!string.IsNullOrEmpty(value4))
			{
				descriptor.AddProperty("_plotArea", value4);
			}
			if (!string.IsNullOrEmpty(serializedSeries))
			{
				descriptor.AddProperty("_series", serializedSeries);
			}
			this.SerializeZoom(descriptor);
			this.SerializePan(descriptor);
			if (this.InvokeLoadData == LoadDataInvocation.OnPageLoad && this.PlotArea.Series.Count > 0)
			{
				this.PlotArea.Series.UpdateIsDataBound();
				if (this.PlotArea.Series.IsDataBound || this.PlotArea.XAxis.IsDataBound)
				{
					if (this.SerializedDataSource == null)
					{
						this.DataBind();
					}
					if (!string.IsNullOrEmpty(this.SerializedDataSource))
					{
						descriptor.AddProperty("_dataSource", this.SerializedDataSource);
					}
				}
			}
			if (this.RenderAs != ChartRenderingEngine.Auto)
			{
				descriptor.AddProperty("renderAs", this.RenderAs.ToString().ToLower());
			}
			if (!string.IsNullOrEmpty(this.ClientDataSourceID))
			{
				try
				{
					Control control = DataSourceControlHelper.FindControl(this, this.ClientDataSourceID);
					descriptor.AddProperty("clientDataSourceID", control.ClientID);
				}
				catch (Exception)
				{
					descriptor.AddProperty("clientDataSourceID", this.ClientDataSourceID);
				}
			}
		}

		// Token: 0x06002494 RID: 9364 RVA: 0x00079948 File Offset: 0x00077B48
		protected override IEnumerable<ScriptReference> GetScriptReferences()
		{
			if (this.EnableEmbeddedScripts)
			{
				IEnumerable<ScriptReference> scriptReferences = base.GetScriptReferences();
				List<ScriptReference> list = new List<ScriptReference>(scriptReferences);
				string fullName = Assembly.GetExecutingAssembly().FullName;
				SeriesCollection series = this.PlotArea.Series;
				list.Add(new ScriptReference(this.LayoutScript, fullName));
				return list;
			}
			return new List<ScriptReference>();
		}

		// Token: 0x17000BE6 RID: 3046
		// (get) Token: 0x06002495 RID: 9365 RVA: 0x0007999B File Offset: 0x00077B9B
		protected string LayoutScript
		{
			get
			{
				if (this.Layout == ChartLayout.Stock)
				{
					return "Telerik.Web.UI.Common.HTML5UI.DataViz.html5.dataviz.stock.js";
				}
				if (this.Layout == ChartLayout.Sparkline)
				{
					return "Telerik.Web.UI.Common.HTML5UI.DataViz.html5.dataviz.sparkline.js";
				}
				return "Telerik.Web.UI.Common.HTML5UI.DataViz.html5.dataviz.chart.js";
			}
		}

		// Token: 0x06002496 RID: 9366 RVA: 0x000799C0 File Offset: 0x00077BC0
		private void SerializeZoom(IScriptDescriptor descriptor)
		{
			if (this.Zoom.Enabled)
			{
				descriptor.AddScriptProperty("_zoomable", this.Zoom.Serialize());
				return;
			}
			descriptor.AddScriptProperty("_zoomable", bool.FalseString.ToLowerInvariant());
		}

		// Token: 0x06002497 RID: 9367 RVA: 0x000799FB File Offset: 0x00077BFB
		private void SerializePan(IScriptDescriptor descriptor)
		{
			if (this.Pan.Enabled)
			{
				descriptor.AddScriptProperty("_pannable", this.Pan.Serialize());
				return;
			}
			descriptor.AddScriptProperty("_pannable", bool.FalseString.ToLowerInvariant());
		}

		// Token: 0x06002498 RID: 9368 RVA: 0x00079A38 File Offset: 0x00077C38
		internal void ConfigureStockChartSettings()
		{
			if (this.Layout == ChartLayout.Stock)
			{
				this.CheckForValidSeriesTypesInNavigator();
				this.CheckForValidSeriesTypesInStockChart();
				this.CheckForValidNavigatorXAxisConfiguration();
				this.CheckForXAxisItemsInNavigator();
				this.SetAxisVisibility(this.Navigator.XAxis, true);
				this.SetAxisLabelsVisibility(this.Navigator.XAxis, true);
				return;
			}
			this.CheckForNavigatorUsage();
		}

		// Token: 0x06002499 RID: 9369 RVA: 0x00079A94 File Offset: 0x00077C94
		internal void CheckForNavigatorUsage()
		{
			if (this.Navigator.Series.Count > 0 || this.Navigator.RangeSelector.From != null || this.Navigator.RangeSelector.To != null)
			{
				throw new Exception("The Navigator element is applicable for stock charts only. You need to set the Layout property of RadHtmlChart to 'Stock' to allow this feature.");
			}
		}

		// Token: 0x0600249A RID: 9370 RVA: 0x00079AF4 File Offset: 0x00077CF4
		internal void CheckForValidSeriesTypesInNavigator()
		{
			for (int i = 0; i < this.Navigator.Series.Count; i++)
			{
				SeriesType type = this.Navigator.Series[i].Type;
				if (type != SeriesType.Area && type != SeriesType.Line && type != SeriesType.Column && type != SeriesType.Candlestick)
				{
					throw new Exception("The allowed series types in a Navigator are: AreaSeries, LineSeries, ColumnSeries and CandlestickSeries.");
				}
			}
		}

		// Token: 0x0600249B RID: 9371 RVA: 0x00079B50 File Offset: 0x00077D50
		internal void CheckForValidSeriesTypesInStockChart()
		{
			for (int i = 0; i < this.PlotArea.Series.Count; i++)
			{
				SeriesType type = this.PlotArea.Series[i].Type;
				if (type != SeriesType.Area && type != SeriesType.Line && type != SeriesType.Column && type != SeriesType.Candlestick)
				{
					throw new Exception("The allowed series types in a stock chart are: AreaSeries, LineSeries, ColumnSeries and CandlestickSeries.");
				}
			}
		}

		// Token: 0x0600249C RID: 9372 RVA: 0x00079BAB File Offset: 0x00077DAB
		internal void CheckForValidNavigatorXAxisConfiguration()
		{
			if (!string.IsNullOrEmpty(this.Navigator.XAxis.DataLabelsField))
			{
				throw new Exception("The DataLabelsField property is inapplicable for the X axis of the Navigator.The data for the Navigator is taken from the chart's datasource automatically.");
			}
		}

		// Token: 0x0600249D RID: 9373 RVA: 0x00079BCF File Offset: 0x00077DCF
		internal void CheckForXAxisItemsInNavigator()
		{
			if (this.Navigator.XAxis.Items.Count > 0)
			{
				throw new Exception("X axis' items cannot be set manually. They are automatically generated from stock chart's datasource.");
			}
		}

		// Token: 0x0600249E RID: 9374 RVA: 0x00079BF4 File Offset: 0x00077DF4
		private void DescribeStockChart(IScriptDescriptor descriptor)
		{
			if (this.Layout == ChartLayout.Stock)
			{
				this.DescribeStockChartDateTimeField(descriptor);
				this.DescribeNavigator(descriptor);
			}
		}

		// Token: 0x0600249F RID: 9375 RVA: 0x00079C10 File Offset: 0x00077E10
		private void DescribeStockChartDateTimeField(IScriptDescriptor descriptor)
		{
			if (this.PlotArea.XAxis != null && !string.IsNullOrEmpty(this.PlotArea.XAxis.DataLabelsField))
			{
				descriptor.AddProperty("_dateField", this.PlotArea.XAxis.DataLabelsField);
			}
		}

		// Token: 0x060024A0 RID: 9376 RVA: 0x00079C5C File Offset: 0x00077E5C
		private void DescribeNavigator(IScriptDescriptor descriptor)
		{
			string value = this.Navigator.Serialize();
			if (!string.IsNullOrEmpty(value))
			{
				descriptor.AddProperty("_navigator", value);
			}
		}

		// Token: 0x060024A1 RID: 9377 RVA: 0x00079C8C File Offset: 0x00077E8C
		private void ConfigureDefaultVisibilitySettings()
		{
			if (this.Layout != ChartLayout.Sparkline)
			{
				this.SetChartTitleVisibility(true);
				this.ConfigureAxesVisibility();
				this.ConfigureAxesGridLinesVisibility();
				this.ConfigureAxesTitlesVisibility();
				this.ConfigureAxesLabelsVisibility();
				this.ConfigureSeriesLabelsAndMarkersVisibility();
				return;
			}
			this.CheckForValidSeriesTypesInSparkline();
			this.ConfigureSparklineAxesLabelsVisibility();
			this.SetChartLegendVisibility(false);
			this.SetChartTitleVisibility(false);
		}

		// Token: 0x060024A2 RID: 9378 RVA: 0x00079CE4 File Offset: 0x00077EE4
		internal void CheckForValidSeriesTypesInSparkline()
		{
			int i = 0;
			while (i < this.PlotArea.Series.Count)
			{
				SeriesType type = this.PlotArea.Series[i].Type;
				SeriesType seriesType = type;
				switch (seriesType)
				{
				case SeriesType.Bar:
				case SeriesType.Column:
				case SeriesType.Pie:
				case SeriesType.Line:
				case SeriesType.Area:
					break;
				case SeriesType.Scatter:
				case SeriesType.ScatterLine:
					goto IL_50;
				default:
					switch (seriesType)
					{
					case SeriesType.Bullet:
					case SeriesType.VerticalBullet:
						goto IL_5B;
					}
					goto IL_50;
				}
				IL_5B:
				i++;
				continue;
				IL_50:
				throw new Exception("The allowed series types in a sparkline chart are AreaSeries, BarSeries, ColumnSeries, LineSeries, BulletSeries, VerticalBulletSeries and PieSeries.");
			}
		}

		// Token: 0x060024A3 RID: 9379 RVA: 0x00079D64 File Offset: 0x00077F64
		private void SetChartLegendVisibility(bool isVisible)
		{
			if (this.Legend.Appearance.Visible == null)
			{
				this.Legend.Appearance.Visible = new bool?(isVisible);
			}
		}

		// Token: 0x060024A4 RID: 9380 RVA: 0x00079DA4 File Offset: 0x00077FA4
		private void SetChartTitleVisibility(bool isVisible)
		{
			if (this.ChartTitle.Appearance.Visible == null)
			{
				this.ChartTitle.Appearance.Visible = new bool?(isVisible);
			}
		}

		// Token: 0x060024A5 RID: 9381 RVA: 0x00079DE4 File Offset: 0x00077FE4
		private void ConfigureAxesVisibility()
		{
			this.SetAxisVisibility(this.PlotArea.XAxis, true);
			this.SetAxisVisibility(this.PlotArea.YAxis, true);
			for (int i = 0; i < this.PlotArea.AdditionalYAxes.Count; i++)
			{
				this.SetAxisVisibility(this.PlotArea.AdditionalYAxes[i], true);
			}
		}

		// Token: 0x060024A6 RID: 9382 RVA: 0x00079E48 File Offset: 0x00078048
		private void SetAxisVisibility(AxisBase axis, bool isVisible)
		{
			if (axis.Visible == null)
			{
				axis.Visible = new bool?(isVisible);
			}
		}

		// Token: 0x060024A7 RID: 9383 RVA: 0x00079E74 File Offset: 0x00078074
		private void ConfigureSparklineAxesLabelsVisibility()
		{
			this.SetSparklineAxisLabelsVisibility(this.PlotArea.XAxis);
			this.SetSparklineAxisLabelsVisibility(this.PlotArea.YAxis);
			for (int i = 0; i < this.PlotArea.AdditionalYAxes.Count; i++)
			{
				this.SetSparklineAxisLabelsVisibility(this.PlotArea.AdditionalYAxes[i]);
			}
		}

		// Token: 0x060024A8 RID: 9384 RVA: 0x00079ED8 File Offset: 0x000780D8
		private void SetSparklineAxisLabelsVisibility(AxisBase axis)
		{
			if (axis.Visible == true && axis.LabelsAppearance.Visible == null)
			{
				axis.LabelsAppearance.Visible = new bool?(true);
			}
		}

		// Token: 0x060024A9 RID: 9385 RVA: 0x00079F27 File Offset: 0x00078127
		private void ConfigureAxesGridLinesVisibility()
		{
			this.ConfigureMajorGridLinesVisibility();
			this.ConfigureMinorGridLinesVisibility();
		}

		// Token: 0x060024AA RID: 9386 RVA: 0x00079F35 File Offset: 0x00078135
		private void ConfigureMinorGridLinesVisibility()
		{
			this.SetGridLinesVisibility(this.PlotArea.YAxis.MajorGridLines);
			this.SetGridLinesVisibility(this.PlotArea.YAxis.MinorGridLines);
		}

		// Token: 0x060024AB RID: 9387 RVA: 0x00079F63 File Offset: 0x00078163
		private void ConfigureMajorGridLinesVisibility()
		{
			this.SetGridLinesVisibility(this.PlotArea.XAxis.MajorGridLines);
			this.SetGridLinesVisibility(this.PlotArea.XAxis.MinorGridLines);
		}

		// Token: 0x060024AC RID: 9388 RVA: 0x00079F94 File Offset: 0x00078194
		private void SetGridLinesVisibility(GridLinesBase gridLines)
		{
			if (gridLines.Visible == null)
			{
				gridLines.Visible = new bool?(true);
			}
		}

		// Token: 0x060024AD RID: 9389 RVA: 0x00079FC0 File Offset: 0x000781C0
		private void ConfigureAxesTitlesVisibility()
		{
			this.SetAxisTitleVisibility(this.PlotArea.XAxis);
			this.SetAxisTitleVisibility(this.PlotArea.YAxis);
			for (int i = 0; i < this.PlotArea.AdditionalYAxes.Count; i++)
			{
				this.SetAxisTitleVisibility(this.PlotArea.AdditionalYAxes[i]);
			}
		}

		// Token: 0x060024AE RID: 9390 RVA: 0x0007A024 File Offset: 0x00078224
		private void SetAxisTitleVisibility(AxisBase axis)
		{
			if (axis.TitleAppearance.Visible == null)
			{
				axis.TitleAppearance.Visible = new bool?(true);
			}
		}

		// Token: 0x060024AF RID: 9391 RVA: 0x0007A058 File Offset: 0x00078258
		private void ConfigureAxesLabelsVisibility()
		{
			this.SetAxisLabelsVisibility(this.PlotArea.XAxis, true);
			this.SetAxisLabelsVisibility(this.PlotArea.YAxis, true);
			for (int i = 0; i < this.PlotArea.AdditionalYAxes.Count; i++)
			{
				this.SetAxisLabelsVisibility(this.PlotArea.AdditionalYAxes[i], true);
			}
		}

		// Token: 0x060024B0 RID: 9392 RVA: 0x0007A0BC File Offset: 0x000782BC
		private void SetAxisLabelsVisibility(AxisBase axis, bool isVisible)
		{
			if (axis.LabelsAppearance.Visible == null)
			{
				axis.LabelsAppearance.Visible = new bool?(isVisible);
			}
		}

		// Token: 0x060024B1 RID: 9393 RVA: 0x0007A0F0 File Offset: 0x000782F0
		private void ConfigureSeriesLabelsAndMarkersVisibility()
		{
			for (int i = 0; i < this.PlotArea.Series.Count; i++)
			{
				SeriesBase seriesBase = this.PlotArea.Series[i];
				BarSeries barSeries = seriesBase as BarSeries;
				if (barSeries != null)
				{
					if (barSeries.LabelsAppearance.Visible == null)
					{
						barSeries.LabelsAppearance.Visible = new bool?(true);
					}
				}
				else
				{
					ColumnSeries columnSeries = seriesBase as ColumnSeries;
					if (columnSeries != null)
					{
						if (columnSeries.LabelsAppearance.Visible == null)
						{
							columnSeries.LabelsAppearance.Visible = new bool?(true);
						}
					}
					else
					{
						PieSeries pieSeries = seriesBase as PieSeries;
						if (pieSeries != null)
						{
							if (pieSeries.LabelsAppearance.Visible == null)
							{
								pieSeries.LabelsAppearance.Visible = new bool?(true);
							}
						}
						else
						{
							DonutSeries donutSeries = seriesBase as DonutSeries;
							if (donutSeries != null)
							{
								if (donutSeries.LabelsAppearance.Visible == null)
								{
									donutSeries.LabelsAppearance.Visible = new bool?(true);
								}
							}
							else
							{
								AreaSeries areaSeries = seriesBase as AreaSeries;
								if (areaSeries != null)
								{
									this.SetMarkersVisibility(areaSeries, true);
									if (areaSeries.LabelsAppearance.Visible == null)
									{
										areaSeries.LabelsAppearance.Visible = new bool?(true);
									}
								}
								else
								{
									LineSeries lineSeries = seriesBase as LineSeries;
									if (lineSeries != null)
									{
										this.SetMarkersVisibility(lineSeries, true);
										if (lineSeries.LabelsAppearance.Visible == null)
										{
											lineSeries.LabelsAppearance.Visible = new bool?(true);
										}
									}
									else
									{
										ScatterLineSeries scatterLineSeries = seriesBase as ScatterLineSeries;
										if (scatterLineSeries != null)
										{
											this.SetMarkersVisibility(scatterLineSeries, true);
											if (scatterLineSeries.LabelsAppearance.Visible == null)
											{
												scatterLineSeries.LabelsAppearance.Visible = new bool?(true);
											}
										}
										else
										{
											ScatterSeries scatterSeries = seriesBase as ScatterSeries;
											if (scatterSeries != null)
											{
												this.SetMarkersVisibility(scatterSeries, true);
												if (scatterSeries.LabelsAppearance.Visible == null)
												{
													scatterSeries.LabelsAppearance.Visible = new bool?(true);
												}
											}
											else
											{
												BubbleSeries bubbleSeries = seriesBase as BubbleSeries;
												if (bubbleSeries != null)
												{
													this.SetMarkersVisibility(bubbleSeries, true);
												}
												else
												{
													FunnelSeries funnelSeries = seriesBase as FunnelSeries;
													if (funnelSeries != null && funnelSeries.LabelsAppearance.Visible == null)
													{
														funnelSeries.LabelsAppearance.Visible = new bool?(true);
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x060024B2 RID: 9394 RVA: 0x0007A37C File Offset: 0x0007857C
		private void SetMarkersVisibility(MarkersSeries markerSeries, bool isVisible)
		{
			if (markerSeries.MarkersAppearance.Visible == null)
			{
				markerSeries.MarkersAppearance.Visible = new bool?(isVisible);
			}
		}

		// Token: 0x060024B3 RID: 9395 RVA: 0x0007A3B0 File Offset: 0x000785B0
		protected override void LoadViewState(object state)
		{
			object[] array = (object[])state;
			base.LoadViewState(array[0]);
			((IStateManager)this.PlotArea.YAxis).LoadViewState(array[1]);
			((IStateManager)this.PlotArea.XAxis).LoadViewState(array[2]);
			((IStateManager)this.PlotArea.Series).LoadViewState(array[3]);
			((IStateManager)this.PlotArea.AdditionalYAxes).LoadViewState(array[4]);
		}

		// Token: 0x060024B4 RID: 9396 RVA: 0x0007A41C File Offset: 0x0007861C
		protected override object SaveViewState()
		{
			return new object[]
			{
				base.SaveViewState(),
				((IStateManager)this.PlotArea.YAxis).SaveViewState(),
				((IStateManager)this.PlotArea.XAxis).SaveViewState(),
				((IStateManager)this.PlotArea.Series).SaveViewState(),
				((IStateManager)this.PlotArea.AdditionalYAxes).SaveViewState()
			};
		}

		// Token: 0x060024B5 RID: 9397 RVA: 0x0007A488 File Offset: 0x00078688
		protected override void TrackViewState()
		{
			base.TrackViewState();
			((IStateManager)this.PlotArea.YAxis).TrackViewState();
			((IStateManager)this.PlotArea.XAxis).TrackViewState();
			((IStateManager)this.PlotArea.Series).TrackViewState();
			((IStateManager)this.PlotArea.AdditionalYAxes).TrackViewState();
		}

		// Token: 0x17000BE7 RID: 3047
		// (get) Token: 0x060024B6 RID: 9398 RVA: 0x0007A4DB File Offset: 0x000786DB
		// (set) Token: 0x060024B7 RID: 9399 RVA: 0x0007A4F2 File Offset: 0x000786F2
		internal string SerializedDataSource
		{
			get
			{
				return (string)this.ViewState["SerializedDataSource"];
			}
			set
			{
				this.ViewState["SerializedDataSource"] = value;
			}
		}

		// Token: 0x060024B8 RID: 9400 RVA: 0x0007A508 File Offset: 0x00078708
		protected override void PerformDataBinding(IEnumerable data)
		{
			if (data != null)
			{
				PropertyDescriptorCollection propertyDescriptorCollection = null;
				if (this.dataBindData == null)
				{
					this.dataBindData = new StringBuilder();
				}
				this.dataBindData.Clear();
				bool isXmlDataSource = false;
				if (!string.IsNullOrEmpty(this.DataSourceID))
				{
					try
					{
						Control control = DataSourceControlHelper.FindControl(this, this.DataSourceID);
						isXmlDataSource = (control is XmlDataSource);
						goto IL_61;
					}
					catch (Exception)
					{
						goto IL_61;
					}
				}
				isXmlDataSource = (this.DataSource is XmlDataSource);
				IL_61:
				foreach (object obj in data)
				{
					if (propertyDescriptorCollection == null)
					{
						propertyDescriptorCollection = TypeDescriptor.GetProperties(obj);
					}
					if (propertyDescriptorCollection.Count == 0)
					{
						this.BindToListData(data);
						this.SerializedDataSource = string.Empty;
						return;
					}
					this.dataBindData.Append("{");
					this.AddSerializedData(this.dataBindData, propertyDescriptorCollection, obj, isXmlDataSource);
					this.dataBindData.Append("},");
				}
				if (this.dataBindData.Length > 1)
				{
					this.dataBindData.Remove(this.dataBindData.Length - 1, 1);
				}
				this.dataBindData.Insert(0, "[");
				this.dataBindData.Append("]");
				this.RemoveSeriesDataSerialization();
				if (this.SerializedDataSource != this.dataBindData.ToString())
				{
					this.SerializedDataSource = this.dataBindData.ToString();
				}
			}
		}

		// Token: 0x060024B9 RID: 9401 RVA: 0x0007A698 File Offset: 0x00078898
		private void RemoveSeriesDataSerialization()
		{
			for (int i = 0; i < this.PlotArea.Series.Count; i++)
			{
				if (this.PlotArea.Series[i].Data != string.Empty)
				{
					this.PlotArea.Series[i].Data = string.Empty;
				}
			}
		}

		// Token: 0x060024BA RID: 9402 RVA: 0x0007A700 File Offset: 0x00078900
		protected void AddSerializedData(StringBuilder sb, PropertyDescriptorCollection props, object dataItem, bool isXmlDataSource)
		{
			foreach (object obj in props)
			{
				PropertyDescriptor propertyDescriptor = (PropertyDescriptor)obj;
				string name = propertyDescriptor.Name;
				sb.Append('"');
				sb.Append(name);
				sb.Append("\":");
				object obj2 = DataBinder.Eval(dataItem, name);
				if (obj2 != null && !obj2.GetType().IsArray)
				{
					object propertyValue = DataBinder.GetPropertyValue(dataItem, name);
					if (propertyValue is string && !isXmlDataSource)
					{
						sb.AppendFormat("\"{0}\",", propertyValue);
					}
					else
					{
						string value = string.Format(CultureInfo.InvariantCulture, "{0}", new object[]
						{
							propertyValue
						});
						if (obj2 is DateTime || obj2 is DateTime?)
						{
							sb.Append(HtmlChartHelper.GetSerializedValueField(value, true)).Append(",");
						}
						else
						{
							sb.Append(HtmlChartHelper.GetSerializedValueField(value, false)).Append(",");
						}
					}
				}
				else
				{
					sb.AppendFormat("{0},", this.serializer.Serialize(obj2));
				}
			}
			HtmlChartHelper.RemoveEndingComma(sb);
		}

		// Token: 0x060024BB RID: 9403 RVA: 0x0007A850 File Offset: 0x00078A50
		protected void BindToListData(object data)
		{
			foreach (object obj in this.PlotArea.Series)
			{
				SeriesBase seriesBase = (SeriesBase)obj;
				if (seriesBase.Items.Count == 0)
				{
					this.PlotArea.Series.IsDataBound = true;
					seriesBase.Data = this.serializer.Serialize(data);
				}
			}
		}

		// Token: 0x060024BC RID: 9404 RVA: 0x0007A8D8 File Offset: 0x00078AD8
		private string GetSerializedSeries()
		{
			return this.PlotArea.Series.Serialize();
		}

		// Token: 0x060024BD RID: 9405 RVA: 0x0007A8EC File Offset: 0x00078AEC
		string ICallbackEventHandler.GetCallbackResult()
		{
			string a = HttpContext.Current.Request["__CALLBACKPARAM"];
			if (a == "loadChartData")
			{
				if (this.dataBindData == null)
				{
					this.DataBind();
				}
				StringBuilder stringBuilder = new StringBuilder();
				if ((this.PlotArea.Series.IsDataBound || this.PlotArea.XAxis.IsDataBound) && !string.IsNullOrEmpty(this.SerializedDataSource))
				{
					stringBuilder.AppendFormat("dataSource: {0},", this.SerializedDataSource);
				}
				if (stringBuilder.Length > 0 && this.PlotArea.Series.Count > 0)
				{
					stringBuilder.AppendFormat("series: {0},", this.GetSerializedSeries());
				}
				HtmlChartHelper.RemoveEndingComma(stringBuilder);
				return stringBuilder.ToString();
			}
			return "";
		}

		// Token: 0x060024BE RID: 9406 RVA: 0x0007A9B6 File Offset: 0x00078BB6
		void ICallbackEventHandler.RaiseCallbackEvent(string eventArgument)
		{
		}

		// Token: 0x060024BF RID: 9407 RVA: 0x0007A9B8 File Offset: 0x00078BB8
		internal RadHtmlChartType GetHtmlChartTypeByFirstSeries()
		{
			RadHtmlChartType result = RadHtmlChartType.Bar;
			string name;
			if (this.PlotArea.Series.Count > 0 && (name = this.PlotArea.Series[0].GetType().Name) != null)
			{
				if (<PrivateImplementationDetails>{FD978F7E-3DA5-4815-803F-07E58A83CEFA}.$$method0x6002442-1 == null)
				{
					<PrivateImplementationDetails>{FD978F7E-3DA5-4815-803F-07E58A83CEFA}.$$method0x6002442-1 = new Dictionary<string, int>(25)
					{
						{
							"BarSeries",
							0
						},
						{
							"ColumnSeries",
							1
						},
						{
							"LineSeries",
							2
						},
						{
							"PieSeries",
							3
						},
						{
							"ScatterLineSeries",
							4
						},
						{
							"ScatterSeries",
							5
						},
						{
							"AreaSeries",
							6
						},
						{
							"BubbleSeries",
							7
						},
						{
							"DonutSeries",
							8
						},
						{
							"CandlestickSeries",
							9
						},
						{
							"FunnelSeries",
							10
						},
						{
							"PolarAreaSeries",
							11
						},
						{
							"PolarLineSeries",
							12
						},
						{
							"PolarScatterSeries",
							13
						},
						{
							"RadarAreaSeries",
							14
						},
						{
							"RadarColumnSeries",
							15
						},
						{
							"RadarLineSeries",
							16
						},
						{
							"BoxPlotSeries",
							17
						},
						{
							"VerticalBoxPlotSeries",
							18
						},
						{
							"RangeBarSeries",
							19
						},
						{
							"RangeColumnSeries",
							20
						},
						{
							"WaterfallSeries",
							21
						},
						{
							"HorizontalWaterfallSeries",
							22
						},
						{
							"BulletSeries",
							23
						},
						{
							"VerticalBulletSeries",
							24
						}
					};
				}
				int num;
				if (<PrivateImplementationDetails>{FD978F7E-3DA5-4815-803F-07E58A83CEFA}.$$method0x6002442-1.TryGetValue(name, out num))
				{
					switch (num)
					{
					case 0:
						result = RadHtmlChartType.Bar;
						break;
					case 1:
						result = RadHtmlChartType.Column;
						break;
					case 2:
						result = RadHtmlChartType.Line;
						break;
					case 3:
						result = RadHtmlChartType.Pie;
						break;
					case 4:
						result = RadHtmlChartType.ScatterLine;
						break;
					case 5:
						result = RadHtmlChartType.Scatter;
						break;
					case 6:
						result = RadHtmlChartType.Area;
						break;
					case 7:
						result = RadHtmlChartType.Bubble;
						break;
					case 8:
						result = RadHtmlChartType.Donut;
						break;
					case 9:
						result = RadHtmlChartType.Candlestick;
						break;
					case 10:
						result = RadHtmlChartType.Funnel;
						break;
					case 11:
						result = RadHtmlChartType.PolarArea;
						break;
					case 12:
						result = RadHtmlChartType.PolarLine;
						break;
					case 13:
						result = RadHtmlChartType.PolarScatter;
						break;
					case 14:
						result = RadHtmlChartType.RadarArea;
						break;
					case 15:
						result = RadHtmlChartType.RadarColumn;
						break;
					case 16:
						result = RadHtmlChartType.RadarLine;
						break;
					case 17:
						result = RadHtmlChartType.BoxPlot;
						break;
					case 18:
						result = RadHtmlChartType.VerticalBoxPlot;
						break;
					case 19:
						result = RadHtmlChartType.RangeBar;
						break;
					case 20:
						result = RadHtmlChartType.RangeColumn;
						break;
					case 21:
						result = RadHtmlChartType.Waterfall;
						break;
					case 22:
						result = RadHtmlChartType.HorizontalWaterfall;
						break;
					case 23:
						result = RadHtmlChartType.Bullet;
						break;
					case 24:
						result = RadHtmlChartType.VerticalBullet;
						break;
					}
				}
			}
			return result;
		}

		// Token: 0x060024C0 RID: 9408 RVA: 0x0007AC4C File Offset: 0x00078E4C
		internal string GetDefaultSeriesTypeName()
		{
			switch (this.HtmlChartType ?? this.GetHtmlChartTypeByFirstSeries())
			{
			case RadHtmlChartType.Column:
				return "ColumnSeries";
			case RadHtmlChartType.Line:
				return "LineSeries";
			case RadHtmlChartType.Pie:
				return "PieSeries";
			case RadHtmlChartType.Scatter:
				return "ScatterSeries";
			case RadHtmlChartType.ScatterLine:
				return "ScatterLineSeries";
			case RadHtmlChartType.Area:
				return "AreaSeries";
			case RadHtmlChartType.Bubble:
				return "BubbleSeries";
			case RadHtmlChartType.Donut:
				return "DonutSeries";
			case RadHtmlChartType.Candlestick:
				return "CandlestickSeries";
			case RadHtmlChartType.Funnel:
				return "FunnelSeries";
			case RadHtmlChartType.BoxPlot:
				return "BoxPlotSeries";
			}
			return "BarSeries";
		}

		// Token: 0x17000BE8 RID: 3048
		// (get) Token: 0x060024C1 RID: 9409 RVA: 0x0007AD28 File Offset: 0x00078F28
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				if (this.Layout == ChartLayout.Sparkline)
				{
					return HtmlTextWriterTag.Span;
				}
				return HtmlTextWriterTag.Div;
			}
		}

		// Token: 0x17000BE9 RID: 3049
		// (get) Token: 0x060024C2 RID: 9410 RVA: 0x0007AD38 File Offset: 0x00078F38
		protected override string CssClassFormatString
		{
			get
			{
				return "RadHtmlChart RadHtmlChart_{0}";
			}
		}

		// Token: 0x17000BEA RID: 3050
		// (get) Token: 0x060024C3 RID: 9411 RVA: 0x0007AD3F File Offset: 0x00078F3F
		[DefaultValue(true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public override bool EnableEmbeddedBaseStylesheet
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000BEB RID: 3051
		// (get) Token: 0x060024C4 RID: 9412 RVA: 0x0007AD42 File Offset: 0x00078F42
		// (set) Token: 0x060024C5 RID: 9413 RVA: 0x0007AD67 File Offset: 0x00078F67
		[DefaultValue(typeof(Unit), "")]
		[Description("Get/Set the Width of the chart")]
		[ClientControlProperty]
		[TypeConverter(typeof(UnitConverter))]
		[Category("Behavior")]
		public override Unit Width
		{
			get
			{
				return (Unit)(this.ViewState["Width"] ?? Unit.Empty);
			}
			set
			{
				this.ViewState["Width"] = value;
			}
		}

		// Token: 0x17000BEC RID: 3052
		// (get) Token: 0x060024C6 RID: 9414 RVA: 0x0007AD7F File Offset: 0x00078F7F
		// (set) Token: 0x060024C7 RID: 9415 RVA: 0x0007ADA4 File Offset: 0x00078FA4
		[ClientControlProperty]
		[Category("Behavior")]
		[Description("Get/Set the Height of the chart")]
		[DefaultValue(typeof(Unit), "")]
		[TypeConverter(typeof(UnitConverter))]
		public override Unit Height
		{
			get
			{
				return (Unit)(this.ViewState["Height"] ?? Unit.Empty);
			}
			set
			{
				this.ViewState["Height"] = value;
			}
		}

		// Token: 0x17000BED RID: 3053
		// (get) Token: 0x060024C8 RID: 9416 RVA: 0x0007ADBC File Offset: 0x00078FBC
		// (set) Token: 0x060024C9 RID: 9417 RVA: 0x0007AE11 File Offset: 0x00079011
		[Category("Behavior")]
		[Description("Get/Set whether transition animations should be played")]
		[ClientControlProperty]
		[DefaultValue(true)]
		public bool Transitions
		{
			get
			{
				if (this.Layout == ChartLayout.Sparkline)
				{
					return (bool)(this.ViewState["Transitions"] ?? false);
				}
				return (bool)(this.ViewState["Transitions"] ?? true);
			}
			set
			{
				this.ViewState["Transitions"] = value;
			}
		}

		// Token: 0x17000BEE RID: 3054
		// (get) Token: 0x060024CA RID: 9418 RVA: 0x0007AE29 File Offset: 0x00079029
		// (set) Token: 0x060024CB RID: 9419 RVA: 0x0007AE4A File Offset: 0x0007904A
		[Description("Get/Set when actual data will be loaded")]
		[DefaultValue(LoadDataInvocation.OnPageLoad)]
		[ClientControlProperty]
		[Category("Behavior")]
		public LoadDataInvocation InvokeLoadData
		{
			get
			{
				return (LoadDataInvocation)(this.ViewState["InvokeLoadData"] ?? LoadDataInvocation.OnPageLoad);
			}
			set
			{
				this.ViewState["InvokeLoadData"] = value;
			}
		}

		// Token: 0x17000BEF RID: 3055
		// (get) Token: 0x060024CC RID: 9420 RVA: 0x0007AE62 File Offset: 0x00079062
		// (set) Token: 0x060024CD RID: 9421 RVA: 0x0007AE83 File Offset: 0x00079083
		[Category("Behavior")]
		[Description("Gets/Sets the rendering engine.")]
		[DefaultValue(ChartRenderingEngine.Auto)]
		public ChartRenderingEngine RenderAs
		{
			get
			{
				return (ChartRenderingEngine)(this.ViewState["RenderAs"] ?? ChartRenderingEngine.Auto);
			}
			set
			{
				this.ViewState["RenderAs"] = value;
			}
		}

		// Token: 0x17000BF0 RID: 3056
		// (get) Token: 0x060024CE RID: 9422 RVA: 0x0007AE9B File Offset: 0x0007909B
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[DefaultValue("Appearance")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Description("Chart visual settings")]
		[TypeConverter(typeof(ExpandableObjectConverter))]
		[NotifyParentProperty(true)]
		[Browsable(true)]
		[Category("Appearance")]
		public HtmlChartAppearance Appearance
		{
			get
			{
				if (this._appearance == null)
				{
					this._appearance = new HtmlChartAppearance("ca", this.ViewState);
				}
				return this._appearance;
			}
		}

		// Token: 0x17000BF1 RID: 3057
		// (get) Token: 0x060024CF RID: 9423 RVA: 0x0007AEC1 File Offset: 0x000790C1
		[TypeConverter(typeof(ExpandableObjectConverter))]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Category("Appearance")]
		[Description("Chart title settings")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[NotifyParentProperty(true)]
		[Browsable(true)]
		[DefaultValue("ChartTitle")]
		public HtmlChartTitle ChartTitle
		{
			get
			{
				if (this._chartTitle == null)
				{
					this._chartTitle = new HtmlChartTitle(this.ViewState);
				}
				return this._chartTitle;
			}
		}

		// Token: 0x17000BF2 RID: 3058
		// (get) Token: 0x060024D0 RID: 9424 RVA: 0x0007AEE2 File Offset: 0x000790E2
		[Category("Appearance")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DefaultValue("ChartLegend")]
		[TypeConverter(typeof(ExpandableObjectConverter))]
		[Description("Chart legend settings")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[Browsable(true)]
		public HtmlChartLegend Legend
		{
			get
			{
				if (this._legend == null)
				{
					this._legend = new HtmlChartLegend(this.ViewState);
				}
				return this._legend;
			}
		}

		// Token: 0x17000BF3 RID: 3059
		// (get) Token: 0x060024D1 RID: 9425 RVA: 0x0007AF03 File Offset: 0x00079103
		[TypeConverter(typeof(ExpandableObjectConverter))]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Category("Appearance")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Browsable(true)]
		[NotifyParentProperty(true)]
		[Description("Chart plot area settings")]
		[DefaultValue("ChartPlotArea")]
		public HtmlChartPlotArea PlotArea
		{
			get
			{
				if (this._plotArea == null)
				{
					this._plotArea = new HtmlChartPlotArea(this.ViewState);
				}
				return this._plotArea;
			}
		}

		// Token: 0x17000BF4 RID: 3060
		// (get) Token: 0x060024D2 RID: 9426 RVA: 0x0007AF24 File Offset: 0x00079124
		[Description("Chart's navigator settings")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Category("Appearance")]
		public Navigator Navigator
		{
			get
			{
				if (this._navigator == null)
				{
					this._navigator = new Navigator(this.ViewState);
				}
				return this._navigator;
			}
		}

		// Token: 0x17000BF5 RID: 3061
		// (get) Token: 0x060024D3 RID: 9427 RVA: 0x0007AF45 File Offset: 0x00079145
		// (set) Token: 0x060024D4 RID: 9428 RVA: 0x0007AF4D File Offset: 0x0007914D
		internal RadHtmlChartType? HtmlChartType
		{
			get
			{
				return this._htmlChartType;
			}
			set
			{
				this._htmlChartType = value;
			}
		}

		// Token: 0x17000BF6 RID: 3062
		// (get) Token: 0x060024D5 RID: 9429 RVA: 0x0007AF56 File Offset: 0x00079156
		// (set) Token: 0x060024D6 RID: 9430 RVA: 0x0007AF76 File Offset: 0x00079176
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientPropertyName("seriesClicked")]
		[DefaultValue("")]
		[ClientControlEvent]
		[Category("Client-side events")]
		[Description("Gets or sets the name of the JavaScript function that will be called when a series is clicked.")]
		public string OnClientSeriesClicked
		{
			get
			{
				return ((string)this.ViewState["OnClientSeriesClicked"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["OnClientSeriesClicked"] = value;
			}
		}

		// Token: 0x17000BF7 RID: 3063
		// (get) Token: 0x060024D7 RID: 9431 RVA: 0x0007AF89 File Offset: 0x00079189
		// (set) Token: 0x060024D8 RID: 9432 RVA: 0x0007AFA9 File Offset: 0x000791A9
		[Category("Client-side events")]
		[Description("Gets or sets the name of the JavaScript function that will be called when a series is hovered.")]
		[ClientPropertyName("seriesHovered")]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		public string OnClientSeriesHovered
		{
			get
			{
				return ((string)this.ViewState["OnClientSeriesHovered"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["OnClientSeriesHovered"] = value;
			}
		}

		// Token: 0x17000BF8 RID: 3064
		// (get) Token: 0x060024D9 RID: 9433 RVA: 0x0007AFBC File Offset: 0x000791BC
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public HtmlChartClientEvents ClientEvents
		{
			get
			{
				if (this._clientEvents == null)
				{
					this._clientEvents = new HtmlChartClientEvents();
				}
				return this._clientEvents;
			}
		}

		// Token: 0x17000BF9 RID: 3065
		// (get) Token: 0x060024DA RID: 9434 RVA: 0x0007AFD7 File Offset: 0x000791D7
		// (set) Token: 0x060024DB RID: 9435 RVA: 0x0007AFF8 File Offset: 0x000791F8
		[Description("Get/Set the layout of the chart")]
		[DefaultValue(ChartLayout.Default)]
		[Category("Layout")]
		public ChartLayout Layout
		{
			get
			{
				return (ChartLayout)(this.ViewState["Layout"] ?? ChartLayout.Default);
			}
			set
			{
				this.ViewState["Layout"] = value;
			}
		}

		// Token: 0x17000BFA RID: 3066
		// (get) Token: 0x060024DC RID: 9436 RVA: 0x0007B010 File Offset: 0x00079210
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Description("Specifies the zooming configuration of the chart")]
		[Category("Behavior")]
		public Zoom Zoom
		{
			get
			{
				if (this._zoom == null)
				{
					this._zoom = new Zoom();
				}
				return this._zoom;
			}
		}

		// Token: 0x17000BFB RID: 3067
		// (get) Token: 0x060024DD RID: 9437 RVA: 0x0007B02B File Offset: 0x0007922B
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Description("Specifies the panning configuration of the chart")]
		[Category("Behavior")]
		public Pan Pan
		{
			get
			{
				if (this._pan == null)
				{
					this._pan = new Pan();
				}
				return this._pan;
			}
		}

		// Token: 0x04000966 RID: 2406
		protected StringBuilder dataBindData;

		// Token: 0x04000967 RID: 2407
		private JavaScriptSerializer serializer = new JavaScriptSerializer();

		// Token: 0x04000968 RID: 2408
		private HtmlChartAppearance _appearance;

		// Token: 0x04000969 RID: 2409
		private HtmlChartTitle _chartTitle;

		// Token: 0x0400096A RID: 2410
		private HtmlChartLegend _legend;

		// Token: 0x0400096B RID: 2411
		private HtmlChartPlotArea _plotArea;

		// Token: 0x0400096C RID: 2412
		private Navigator _navigator;

		// Token: 0x0400096D RID: 2413
		private RadHtmlChartType? _htmlChartType;

		// Token: 0x0400096E RID: 2414
		private HtmlChartClientEvents _clientEvents;

		// Token: 0x0400096F RID: 2415
		private Zoom _zoom;

		// Token: 0x04000970 RID: 2416
		private Pan _pan;
	}
}
