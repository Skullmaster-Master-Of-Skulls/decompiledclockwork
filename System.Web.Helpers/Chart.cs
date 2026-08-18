using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Web.Helpers.Resources;
using System.Web.Hosting;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.WebControls;
using System.Web.WebPages;
using System.Xml;
using Microsoft.Internal.Web.Utils;

namespace System.Web.Helpers
{
	// Token: 0x02000002 RID: 2
	public class Chart
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D7 File Offset: 0x000002D7
		public Chart(int width, int height, string theme = null, string themePath = null) : this(Chart.GetDefaultContext(), () => HostingEnvironment.VirtualPathProvider, width, height, theme, themePath)
		{
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002118 File Offset: 0x00000318
		internal Chart(HttpContextBase httpContext, VirtualPathProvider virtualPathProvider, int width, int height, string theme = null, string themePath = null) : this(httpContext, () => virtualPathProvider, width, height, theme, themePath)
		{
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002154 File Offset: 0x00000354
		internal Chart(HttpContextBase httpContext, Func<VirtualPathProvider> virtualPathProviderFunc, int width, int height, string theme = null, string themePath = null)
		{
			if (width < 0)
			{
				throw new ArgumentOutOfRangeException("width", string.Format(CultureInfo.CurrentCulture, CommonResources.Argument_Must_Be_GreaterThanOrEqualTo, new object[]
				{
					0
				}));
			}
			if (height < 0)
			{
				throw new ArgumentOutOfRangeException("height", string.Format(CultureInfo.CurrentCulture, CommonResources.Argument_Must_Be_GreaterThanOrEqualTo, new object[]
				{
					0
				}));
			}
			this._httpContext = httpContext;
			this._virtualPathProviderFunc = virtualPathProviderFunc;
			this._width = width;
			this._height = height;
			this._theme = theme;
			if (!string.IsNullOrEmpty(themePath))
			{
				this._themePath = VirtualPathUtil.ResolvePath(TemplateStack.GetCurrentTemplate(httpContext), httpContext, themePath);
				if (!virtualPathProviderFunc().FileExists(this._themePath))
				{
					throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, HelpersResources.Chart_ThemeFileNotFound, new object[]
					{
						this._themePath
					}), "themePath");
				}
			}
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000004 RID: 4 RVA: 0x00002267 File Offset: 0x00000467
		public string FileName
		{
			get
			{
				return this._path;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000005 RID: 5 RVA: 0x0000226F File Offset: 0x0000046F
		public int Height
		{
			get
			{
				return this._height;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000006 RID: 6 RVA: 0x00002277 File Offset: 0x00000477
		public int Width
		{
			get
			{
				return this._width;
			}
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002280 File Offset: 0x00000480
		public Chart AddLegend(string title = null, string name = null)
		{
			this._legends.Add(new Chart.LegendData
			{
				Name = name,
				Title = title
			});
			return this;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000022B0 File Offset: 0x000004B0
		public Chart AddSeries(string name = null, string chartType = "Column", string chartArea = null, string axisLabel = null, string legend = null, int markerStep = 1, IEnumerable xValue = null, string xField = null, IEnumerable yValues = null, string yFields = null)
		{
			if (string.IsNullOrEmpty(chartType))
			{
				throw new ArgumentException(CommonResources.Argument_Cannot_Be_Null_Or_Empty, "chartType");
			}
			Chart.DataSourceData dataSource = null;
			if (yValues != null)
			{
				dataSource = new Chart.DataSourceData
				{
					XDataSource = xValue,
					XField = xField,
					DataSource = yValues,
					YFields = yFields
				};
			}
			this._series.Add(new Chart.SeriesData
			{
				Name = name,
				ChartType = Chart.ConvertStringArgument<SeriesChartType>("chartType", chartType),
				ChartArea = chartArea,
				AxisLabel = axisLabel,
				Legend = legend,
				MarkerStep = markerStep,
				DataSource = dataSource
			});
			return this;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002354 File Offset: 0x00000554
		public Chart AddTitle(string text = null, string name = null)
		{
			this._titles.Add(new Chart.TitleData
			{
				Name = name,
				Text = text
			});
			return this;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002384 File Offset: 0x00000584
		public Chart SetXAxis(string title = "", double min = 0.0, double max = double.NaN)
		{
			this._xAxis = new Chart.ChartAxisData
			{
				Title = title,
				Minimum = min,
				Maximum = max
			};
			return this;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000023B4 File Offset: 0x000005B4
		public Chart SetYAxis(string title = "", double min = 0.0, double max = double.NaN)
		{
			this._yAxis = new Chart.ChartAxisData
			{
				Title = title,
				Minimum = min,
				Maximum = max
			};
			return this;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000023E4 File Offset: 0x000005E4
		public Chart DataBindCrossTable(IEnumerable dataSource, string groupByField, string xField, string yFields, string otherFields = null, string pointSortOrder = "Ascending")
		{
			if (dataSource == null)
			{
				throw new ArgumentNullException("dataSource");
			}
			if (dataSource is string)
			{
				throw new ArgumentException(HelpersResources.Chart_ExceptionDataBindSeriesToString, "dataSource");
			}
			if (string.IsNullOrEmpty(groupByField))
			{
				throw new ArgumentException(CommonResources.Argument_Cannot_Be_Null_Or_Empty, "groupByField");
			}
			if (string.IsNullOrEmpty(yFields))
			{
				throw new ArgumentException(CommonResources.Argument_Cannot_Be_Null_Or_Empty, "yFields");
			}
			this._dataSource = new Chart.DataSourceData
			{
				DataSource = dataSource,
				GroupByField = groupByField,
				XField = xField,
				YFields = yFields,
				OtherFields = otherFields,
				PointSortOrder = Chart.ConvertStringArgument<PointSortOrder>("pointSortOrder", pointSortOrder)
			};
			return this;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002490 File Offset: 0x00000690
		public Chart DataBindTable(IEnumerable dataSource, string xField = null)
		{
			if (dataSource == null)
			{
				throw new ArgumentNullException("dataSource");
			}
			if (dataSource is string)
			{
				throw new ArgumentException(HelpersResources.Chart_ExceptionDataBindSeriesToString, "dataSource");
			}
			this._dataSource = new Chart.DataSourceData
			{
				DataBindTable = true,
				DataSource = dataSource,
				XField = xField
			};
			return this;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002510 File Offset: 0x00000710
		public byte[] GetBytes(string format = "jpeg")
		{
			Chart.<>c__DisplayClassf CS$<>8__locals1 = new Chart.<>c__DisplayClassf();
			CS$<>8__locals1.imageFormat = Chart.ConvertStringToChartImageFormat(format);
			byte[] result;
			using (MemoryStream stream = new MemoryStream())
			{
				this.ExecuteChartAction(delegate(Chart c)
				{
					c.SaveImage(stream, CS$<>8__locals1.imageFormat);
				});
				result = stream.ToArray();
			}
			return result;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002594 File Offset: 0x00000794
		public static Chart GetFromCache(string key)
		{
			return Chart.GetFromCache(Chart.GetDefaultContext(), key);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000025A1 File Offset: 0x000007A1
		public Chart Save(string path, string format = "jpeg")
		{
			return this.Save(Chart.GetDefaultContext(), path, format);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000025D8 File Offset: 0x000007D8
		internal Chart Save(HttpContextBase httpContext, string path, string format)
		{
			if (string.IsNullOrEmpty(path))
			{
				throw new ArgumentException(CommonResources.Argument_Cannot_Be_Null_Or_Empty, "path");
			}
			ChartImageFormat imageFormat = Chart.ConvertStringToChartImageFormat(format);
			this._path = VirtualPathUtil.MapPath(httpContext, path);
			this.ExecuteChartAction(delegate(Chart c)
			{
				c.RenderType = RenderType.ImageTag;
				c.SaveImage(this.FileName, imageFormat);
			});
			return this;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002636 File Offset: 0x00000836
		public string SaveToCache(string key = null, int minutesToCache = 20, bool slidingExpiration = true)
		{
			if (string.IsNullOrEmpty(key))
			{
				key = Chart.GetUniqueKey();
			}
			WebCache.Set(key, this, minutesToCache, slidingExpiration);
			return key;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002651 File Offset: 0x00000851
		public Chart SaveXml(string path)
		{
			return this.SaveXml(Chart.GetDefaultContext(), path);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002680 File Offset: 0x00000880
		internal Chart SaveXml(HttpContextBase httpContext, string path)
		{
			if (string.IsNullOrEmpty(path))
			{
				throw new ArgumentException(CommonResources.Argument_Cannot_Be_Null_Or_Empty, "path");
			}
			this.ExecuteChartAction(delegate(Chart c)
			{
				c.SaveXml(VirtualPathUtil.MapPath(httpContext, path));
			});
			return this;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000026D1 File Offset: 0x000008D1
		public WebImage ToWebImage(string format = "jpeg")
		{
			return new WebImage(this.GetBytes(format));
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000026E0 File Offset: 0x000008E0
		public Chart Write(string format = "jpeg")
		{
			HttpResponseBase response = this._httpContext.Response;
			response.Charset = string.Empty;
			response.ContentType = "image/" + Chart.NormalizeFormat(format);
			response.BinaryWrite(this.GetBytes(format));
			return this;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002728 File Offset: 0x00000928
		public static Chart WriteFromCache(string key, string format = "jpeg")
		{
			return Chart.WriteFromCache(Chart.GetDefaultContext(), key, format);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002738 File Offset: 0x00000938
		internal void ExecuteChartAction(Action<Chart> action)
		{
			using (Chart chart = new Chart())
			{
				chart.Width = new Unit(this._width);
				chart.Height = new Unit(this._height);
				this.ApplyChartArea(chart);
				this.ApplyLegends(chart);
				this.ApplySeries(chart);
				this.ApplyTitles(chart);
				this.DataBindChart(chart);
				this.LoadThemes(chart);
				action(chart);
			}
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000027BC File Offset: 0x000009BC
		private void LoadThemes(Chart chart)
		{
			if (!string.IsNullOrEmpty(this._theme))
			{
				using (MemoryStream memoryStream = new MemoryStream())
				{
					byte[] bytes = Encoding.UTF8.GetBytes(this._theme);
					memoryStream.Write(bytes, 0, bytes.Length);
					memoryStream.Seek(0L, SeekOrigin.Begin);
					Chart.LoadChartThemeFromFile(chart, memoryStream);
				}
			}
			if (!string.IsNullOrEmpty(this._themePath))
			{
				using (Stream stream = this._virtualPathProviderFunc().GetFile(this._themePath).Open())
				{
					Chart.LoadChartThemeFromFile(chart, stream);
				}
			}
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002870 File Offset: 0x00000A70
		private static void LoadChartThemeFromFile(Chart chart, Stream templateStream)
		{
			chart.Serializer.Content = SerializationContents.All;
			chart.Serializer.SerializableContent = string.Empty;
			chart.Serializer.IsTemplateMode = true;
			chart.Serializer.IsResetWhenLoading = false;
			XmlReader reader = XmlReader.Create(templateStream, new XmlReaderSettings
			{
				IgnoreComments = true
			});
			chart.Serializer.Load(reader);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000028D4 File Offset: 0x00000AD4
		internal static Chart GetFromCache(HttpContextBase context, string key)
		{
			if (string.IsNullOrEmpty(key))
			{
				throw new ArgumentException(CommonResources.Argument_Cannot_Be_Null_Or_Empty, "key");
			}
			Chart chart = WebCache.Get(key) as Chart;
			if (chart != null)
			{
				chart._httpContext = context;
			}
			return chart;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002910 File Offset: 0x00000B10
		internal static Chart WriteFromCache(HttpContextBase context, string key, string format = "jpeg")
		{
			Chart fromCache = Chart.GetFromCache(context, key);
			if (fromCache != null)
			{
				fromCache.Write(format);
			}
			return fromCache;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002934 File Offset: 0x00000B34
		private void ApplyChartArea(Chart chart)
		{
			ChartArea chartArea = new ChartArea("Default");
			try
			{
				Chart.ApplyAxis(chartArea.AxisX, this._xAxis);
				Chart.ApplyAxis(chartArea.AxisY, this._yAxis);
				chart.ChartAreas.Add(chartArea);
			}
			catch
			{
				chartArea.Dispose();
				throw;
			}
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002998 File Offset: 0x00000B98
		private static void ApplyAxis(Axis axis, Chart.ChartAxisData axisData)
		{
			if (axisData == null)
			{
				return;
			}
			if (!string.IsNullOrEmpty(axisData.Title))
			{
				axis.Title = axisData.Title;
			}
			axis.Minimum = axisData.Minimum;
			axis.Maximum = axisData.Maximum;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000029D0 File Offset: 0x00000BD0
		private void ApplyLegends(Chart chart)
		{
			foreach (Chart.LegendData legendData in this._legends)
			{
				Legend legend = new Legend();
				try
				{
					legend.Name = (legendData.Name ?? string.Empty);
					legend.Title = (legendData.Title ?? string.Empty);
				}
				catch (Exception)
				{
					legend.Dispose();
					throw;
				}
				chart.Legends.Add(legend);
			}
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002A70 File Offset: 0x00000C70
		private void ApplySeries(Chart chart)
		{
			foreach (Chart.SeriesData seriesData in this._series)
			{
				Series series = new Series();
				try
				{
					series.AxisLabel = (seriesData.AxisLabel ?? string.Empty);
					series.ChartArea = (seriesData.ChartArea ?? string.Empty);
					series.ChartType = seriesData.ChartType;
					series.Legend = (seriesData.Legend ?? string.Empty);
					series.MarkerStep = seriesData.MarkerStep;
					series.Name = (seriesData.Name ?? string.Empty);
					if (seriesData.DataSource != null)
					{
						if (string.IsNullOrEmpty(seriesData.DataSource.YFields))
						{
							IEnumerable dataSource = seriesData.DataSource.DataSource;
							IEnumerable[] array = dataSource as IEnumerable[];
							if (array != null && !(dataSource is string[]))
							{
								series.Points.DataBindXY(seriesData.DataSource.XDataSource, array);
							}
							else
							{
								series.Points.DataBindXY(seriesData.DataSource.XDataSource, new IEnumerable[]
								{
									dataSource
								});
							}
						}
						else
						{
							series.Points.DataBindXY(seriesData.DataSource.XDataSource, seriesData.DataSource.XField, seriesData.DataSource.DataSource, seriesData.DataSource.YFields);
						}
					}
				}
				catch (Exception)
				{
					series.Dispose();
					throw;
				}
				chart.Series.Add(series);
			}
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002C24 File Offset: 0x00000E24
		private void ApplyTitles(Chart chart)
		{
			foreach (Chart.TitleData titleData in this._titles)
			{
				Title title = new Title();
				try
				{
					title.Name = titleData.Name;
					title.Text = titleData.Text;
				}
				catch (Exception)
				{
					title.Dispose();
					throw;
				}
				chart.Titles.Add(title);
			}
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002CB4 File Offset: 0x00000EB4
		private static T ConvertStringArgument<T>(string paramName, string value)
		{
			object obj;
			if (!ConversionUtil.TryFromString(typeof(T), value, out obj))
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, HelpersResources.Chart_ArgumentConversionFailed, new object[]
				{
					typeof(T).FullName
				}), paramName);
			}
			return (T)((object)obj);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002D0C File Offset: 0x00000F0C
		private static ChartImageFormat ConvertStringToChartImageFormat(string format)
		{
			format = Chart.NormalizeFormat(format);
			object obj;
			if (!ConversionUtil.TryFromString(typeof(ChartImageFormat), format, out obj))
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, HelpersResources.Image_IncorrectImageFormat, new object[]
				{
					format
				}), "format");
			}
			return (ChartImageFormat)obj;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002D64 File Offset: 0x00000F64
		private void DataBindChart(Chart chart)
		{
			if (this._dataSource != null)
			{
				if (!string.IsNullOrEmpty(this._dataSource.GroupByField))
				{
					chart.DataBindCrossTable(this._dataSource.DataSource, this._dataSource.GroupByField, this._dataSource.XField ?? string.Empty, this._dataSource.YFields, this._dataSource.OtherFields ?? string.Empty, this._dataSource.PointSortOrder);
					return;
				}
				if (this._dataSource.DataBindTable)
				{
					chart.DataBindTable(this._dataSource.DataSource, this._dataSource.XField ?? string.Empty);
				}
			}
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002E1B File Offset: 0x0000101B
		private static HttpContextBase GetDefaultContext()
		{
			return new HttpContextWrapper(HttpContext.Current);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002E28 File Offset: 0x00001028
		private static string GetUniqueKey()
		{
			return Guid.NewGuid().ToString();
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002E48 File Offset: 0x00001048
		private static string NormalizeFormat(string format)
		{
			if (string.IsNullOrEmpty(format))
			{
				throw new ArgumentException(CommonResources.Argument_Cannot_Be_Null_Or_Empty, "format");
			}
			if (format.StartsWith("image/", StringComparison.OrdinalIgnoreCase))
			{
				format = format.Substring(6);
			}
			return ConversionUtil.NormalizeImageFormat(format);
		}

		// Token: 0x04000001 RID: 1
		private readonly int _height;

		// Token: 0x04000002 RID: 2
		private readonly int _width;

		// Token: 0x04000003 RID: 3
		private readonly string _themePath;

		// Token: 0x04000004 RID: 4
		private readonly string _theme;

		// Token: 0x04000005 RID: 5
		private readonly List<Chart.LegendData> _legends = new List<Chart.LegendData>();

		// Token: 0x04000006 RID: 6
		private readonly List<Chart.SeriesData> _series = new List<Chart.SeriesData>();

		// Token: 0x04000007 RID: 7
		private readonly List<Chart.TitleData> _titles = new List<Chart.TitleData>();

		// Token: 0x04000008 RID: 8
		private HttpContextBase _httpContext;

		// Token: 0x04000009 RID: 9
		private Func<VirtualPathProvider> _virtualPathProviderFunc;

		// Token: 0x0400000A RID: 10
		private string _path;

		// Token: 0x0400000B RID: 11
		private Chart.DataSourceData _dataSource;

		// Token: 0x0400000C RID: 12
		private Chart.ChartAxisData _xAxis;

		// Token: 0x0400000D RID: 13
		private Chart.ChartAxisData _yAxis;

		// Token: 0x02000003 RID: 3
		private class DataSourceData
		{
			// Token: 0x17000004 RID: 4
			// (get) Token: 0x06000029 RID: 41 RVA: 0x00002E7F File Offset: 0x0000107F
			// (set) Token: 0x0600002A RID: 42 RVA: 0x00002E87 File Offset: 0x00001087
			public bool DataBindTable { get; set; }

			// Token: 0x17000005 RID: 5
			// (get) Token: 0x0600002B RID: 43 RVA: 0x00002E90 File Offset: 0x00001090
			// (set) Token: 0x0600002C RID: 44 RVA: 0x00002E98 File Offset: 0x00001098
			public IEnumerable DataSource { get; set; }

			// Token: 0x17000006 RID: 6
			// (get) Token: 0x0600002D RID: 45 RVA: 0x00002EA1 File Offset: 0x000010A1
			// (set) Token: 0x0600002E RID: 46 RVA: 0x00002EA9 File Offset: 0x000010A9
			public string GroupByField { get; set; }

			// Token: 0x17000007 RID: 7
			// (get) Token: 0x0600002F RID: 47 RVA: 0x00002EB2 File Offset: 0x000010B2
			// (set) Token: 0x06000030 RID: 48 RVA: 0x00002EBA File Offset: 0x000010BA
			public string OtherFields { get; set; }

			// Token: 0x17000008 RID: 8
			// (get) Token: 0x06000031 RID: 49 RVA: 0x00002EC3 File Offset: 0x000010C3
			// (set) Token: 0x06000032 RID: 50 RVA: 0x00002ECB File Offset: 0x000010CB
			public string XField { get; set; }

			// Token: 0x17000009 RID: 9
			// (get) Token: 0x06000033 RID: 51 RVA: 0x00002ED4 File Offset: 0x000010D4
			// (set) Token: 0x06000034 RID: 52 RVA: 0x00002EDC File Offset: 0x000010DC
			public string YFields { get; set; }

			// Token: 0x1700000A RID: 10
			// (get) Token: 0x06000035 RID: 53 RVA: 0x00002EE5 File Offset: 0x000010E5
			// (set) Token: 0x06000036 RID: 54 RVA: 0x00002EED File Offset: 0x000010ED
			public PointSortOrder PointSortOrder { get; set; }

			// Token: 0x1700000B RID: 11
			// (get) Token: 0x06000037 RID: 55 RVA: 0x00002EF6 File Offset: 0x000010F6
			// (set) Token: 0x06000038 RID: 56 RVA: 0x00002EFE File Offset: 0x000010FE
			public IEnumerable XDataSource { get; set; }
		}

		// Token: 0x02000004 RID: 4
		private class LegendData
		{
			// Token: 0x1700000C RID: 12
			// (get) Token: 0x0600003A RID: 58 RVA: 0x00002F0F File Offset: 0x0000110F
			// (set) Token: 0x0600003B RID: 59 RVA: 0x00002F17 File Offset: 0x00001117
			public string Name { get; set; }

			// Token: 0x1700000D RID: 13
			// (get) Token: 0x0600003C RID: 60 RVA: 0x00002F20 File Offset: 0x00001120
			// (set) Token: 0x0600003D RID: 61 RVA: 0x00002F28 File Offset: 0x00001128
			public string Title { get; set; }
		}

		// Token: 0x02000005 RID: 5
		private class SeriesData
		{
			// Token: 0x1700000E RID: 14
			// (get) Token: 0x0600003F RID: 63 RVA: 0x00002F39 File Offset: 0x00001139
			// (set) Token: 0x06000040 RID: 64 RVA: 0x00002F41 File Offset: 0x00001141
			public string AxisLabel { get; set; }

			// Token: 0x1700000F RID: 15
			// (get) Token: 0x06000041 RID: 65 RVA: 0x00002F4A File Offset: 0x0000114A
			// (set) Token: 0x06000042 RID: 66 RVA: 0x00002F52 File Offset: 0x00001152
			public string ChartArea { get; set; }

			// Token: 0x17000010 RID: 16
			// (get) Token: 0x06000043 RID: 67 RVA: 0x00002F5B File Offset: 0x0000115B
			// (set) Token: 0x06000044 RID: 68 RVA: 0x00002F63 File Offset: 0x00001163
			public SeriesChartType ChartType { get; set; }

			// Token: 0x17000011 RID: 17
			// (get) Token: 0x06000045 RID: 69 RVA: 0x00002F6C File Offset: 0x0000116C
			// (set) Token: 0x06000046 RID: 70 RVA: 0x00002F74 File Offset: 0x00001174
			public string Legend { get; set; }

			// Token: 0x17000012 RID: 18
			// (get) Token: 0x06000047 RID: 71 RVA: 0x00002F7D File Offset: 0x0000117D
			// (set) Token: 0x06000048 RID: 72 RVA: 0x00002F85 File Offset: 0x00001185
			public int MarkerStep { get; set; }

			// Token: 0x17000013 RID: 19
			// (get) Token: 0x06000049 RID: 73 RVA: 0x00002F8E File Offset: 0x0000118E
			// (set) Token: 0x0600004A RID: 74 RVA: 0x00002F96 File Offset: 0x00001196
			public string Name { get; set; }

			// Token: 0x17000014 RID: 20
			// (get) Token: 0x0600004B RID: 75 RVA: 0x00002F9F File Offset: 0x0000119F
			// (set) Token: 0x0600004C RID: 76 RVA: 0x00002FA7 File Offset: 0x000011A7
			public Chart.DataSourceData DataSource { get; set; }
		}

		// Token: 0x02000006 RID: 6
		private class TitleData
		{
			// Token: 0x17000015 RID: 21
			// (get) Token: 0x0600004E RID: 78 RVA: 0x00002FB8 File Offset: 0x000011B8
			// (set) Token: 0x0600004F RID: 79 RVA: 0x00002FC0 File Offset: 0x000011C0
			public string Name { get; set; }

			// Token: 0x17000016 RID: 22
			// (get) Token: 0x06000050 RID: 80 RVA: 0x00002FC9 File Offset: 0x000011C9
			// (set) Token: 0x06000051 RID: 81 RVA: 0x00002FD1 File Offset: 0x000011D1
			public string Text { get; set; }
		}

		// Token: 0x02000007 RID: 7
		private class ChartAxisData
		{
			// Token: 0x17000017 RID: 23
			// (get) Token: 0x06000053 RID: 83 RVA: 0x00002FE2 File Offset: 0x000011E2
			// (set) Token: 0x06000054 RID: 84 RVA: 0x00002FEA File Offset: 0x000011EA
			public double Minimum { get; set; }

			// Token: 0x17000018 RID: 24
			// (get) Token: 0x06000055 RID: 85 RVA: 0x00002FF3 File Offset: 0x000011F3
			// (set) Token: 0x06000056 RID: 86 RVA: 0x00002FFB File Offset: 0x000011FB
			public double Maximum { get; set; }

			// Token: 0x17000019 RID: 25
			// (get) Token: 0x06000057 RID: 87 RVA: 0x00003004 File Offset: 0x00001204
			// (set) Token: 0x06000058 RID: 88 RVA: 0x0000300C File Offset: 0x0000120C
			public string Title { get; set; }
		}
	}
}
