using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Web.UI;

namespace Telerik.Charting.Styles
{
	// Token: 0x02001798 RID: 6040
	[PersistChildren(false)]
	[ParseChildren(true)]
	public abstract class Style : StateManagedObject, ICloneable
	{
		// Token: 0x17004745 RID: 18245
		// (get) Token: 0x0600EB57 RID: 60247 RVA: 0x003596C0 File Offset: 0x003578C0
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[SkinnableProperty]
		[Browsable(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[NotifyParentProperty(true)]
		public virtual StyleBorder Border
		{
			get
			{
				return this.styleBorder;
			}
		}

		// Token: 0x17004746 RID: 18246
		// (get) Token: 0x0600EB58 RID: 60248 RVA: 0x003596C8 File Offset: 0x003578C8
		[NotifyParentProperty(true)]
		[Browsable(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TypeConverter(typeof(ExpandableObjectConverter))]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[SkinnableProperty]
		public virtual ShadowStyle Shadow
		{
			get
			{
				return this.styleShadow;
			}
		}

		// Token: 0x17004747 RID: 18247
		// (get) Token: 0x0600EB59 RID: 60249 RVA: 0x003596D0 File Offset: 0x003578D0
		// (set) Token: 0x0600EB5A RID: 60250 RVA: 0x003596F1 File Offset: 0x003578F1
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(true)]
		[NotifyParentProperty(true)]
		[DefaultValue(true)]
		public virtual bool Visible
		{
			get
			{
				return (bool)(base.ViewState["Visible"] ?? true);
			}
			set
			{
				base.ViewState["Visible"] = value;
			}
		}

		// Token: 0x17004748 RID: 18248
		internal virtual object this[StyleProperties name]
		{
			get
			{
				switch (name)
				{
				case StyleProperties.Visible:
					return this.Visible;
				case StyleProperties.Border:
					return this.styleBorder;
				case StyleProperties.Shadow:
					return this.styleShadow;
				default:
					return null;
				}
			}
		}

		// Token: 0x17004749 RID: 18249
		// (get) Token: 0x0600EB5C RID: 60252 RVA: 0x0035974C File Offset: 0x0035794C
		// (set) Token: 0x0600EB5D RID: 60253 RVA: 0x00359754 File Offset: 0x00357954
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		internal Chart Chart
		{
			get
			{
				return this.styleChart;
			}
			set
			{
				this.styleChart = value;
			}
		}

		// Token: 0x0600EB5E RID: 60254 RVA: 0x0035975D File Offset: 0x0035795D
		public Style(object containerObject) : this()
		{
			this.styleContainerObject = containerObject;
			this.styleBorder = new StyleBorder(containerObject);
		}

		// Token: 0x0600EB5F RID: 60255 RVA: 0x00359778 File Offset: 0x00357978
		public Style() : this(null)
		{
		}

		// Token: 0x0600EB60 RID: 60256 RVA: 0x00359781 File Offset: 0x00357981
		public Style(StyleBorder border)
		{
			this.styleBorder = (border ?? new StyleBorder());
			this.styleShadow = new ShadowStyle();
		}

		// Token: 0x0600EB61 RID: 60257 RVA: 0x003597A4 File Offset: 0x003579A4
		public Style(StyleBorder border, bool visible) : this(border, visible, null)
		{
		}

		// Token: 0x0600EB62 RID: 60258 RVA: 0x003597B0 File Offset: 0x003579B0
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public Style(StyleBorder border, bool visible, ShadowStyle shadowStyle)
		{
			this.styleBorder = (border ?? new StyleBorder());
			this.styleShadow = (shadowStyle ?? new ShadowStyle());
			if (!visible || base.ViewState["Visible"] != null)
			{
				this.Visible = visible;
			}
		}

		// Token: 0x0600EB63 RID: 60259 RVA: 0x00359800 File Offset: 0x00357A00
		internal static bool IsVisible(object element)
		{
			bool flag = true;
			IOrdering ordering = element as IOrdering;
			if (ordering != null)
			{
				ChartAxis chartAxis = ordering as ChartAxis;
				if (chartAxis != null)
				{
					flag = chartAxis.IsVisible();
				}
				else
				{
					flag = (bool)Style.GetStyleProperty(ordering, StyleProperties.Visible);
					if (flag)
					{
						ExtendedLabel extendedLabel = ordering as ExtendedLabel;
						if (extendedLabel != null)
						{
							flag = extendedLabel.IsVisible();
						}
						else
						{
							ChartBaseLabel chartBaseLabel = ordering as ChartBaseLabel;
							if (chartBaseLabel != null)
							{
								flag = chartBaseLabel.IsVisible();
							}
						}
					}
				}
			}
			else if (element.GetType() == typeof(Chart))
			{
				flag = ((Chart)element).Appearance.Visible;
			}
			return flag;
		}

		// Token: 0x0600EB64 RID: 60260 RVA: 0x0035988E File Offset: 0x00357A8E
		internal virtual void Reset()
		{
			this.styleBorder = new StyleBorder();
			this.styleShadow = new ShadowStyle();
			base.ViewState.Remove("Visible");
		}

		// Token: 0x0600EB65 RID: 60261 RVA: 0x003598B8 File Offset: 0x00357AB8
		internal static void SetPixelValues(IOrdering elem, IContainer container)
		{
			if (elem != null && container != null)
			{
				Dimensions dimensions = (Dimensions)Style.GetStyleProperty(container, StyleProperties.Dimensions);
				if (dimensions != null)
				{
					Dimensions dimensions2 = (Dimensions)Style.GetStyleProperty(elem, StyleProperties.Dimensions);
					if (dimensions2 != null)
					{
						float pixelValue = dimensions.Width.PixelValue;
						float pixelValue2 = dimensions.Height.PixelValue;
						Style.SetPixelValues(dimensions2, pixelValue, pixelValue2);
					}
				}
			}
		}

		// Token: 0x0600EB66 RID: 60262 RVA: 0x0035990C File Offset: 0x00357B0C
		internal static void SetPixelValues(IOrdering elem, float contWidth, float contHeight)
		{
			if (elem != null)
			{
				Dimensions objDims = (Dimensions)Style.GetStyleProperty(elem, StyleProperties.Dimensions);
				Style.SetPixelValues(objDims, contWidth, contHeight);
			}
		}

		// Token: 0x0600EB67 RID: 60263 RVA: 0x00359934 File Offset: 0x00357B34
		private static void SetPixelValues(Dimensions objDims, float contWidth, float contHeight)
		{
			if (objDims != null)
			{
				objDims.Width.CalculatePixelValue(contWidth);
				objDims.Height.CalculatePixelValue(contHeight);
				objDims.Margins.Left.CalculatePixelValue(contWidth);
				objDims.Margins.Top.CalculatePixelValue(contHeight);
				objDims.Margins.Right.CalculatePixelValue(contWidth);
				objDims.Margins.Bottom.CalculatePixelValue(contHeight);
			}
		}

		// Token: 0x0600EB68 RID: 60264 RVA: 0x003599A0 File Offset: 0x00357BA0
		internal static RectangleF GetRealBounds(Dimensions dimensions, float? rotation)
		{
			RectangleF rectangleF = RectangleF.Empty;
			if (dimensions != null)
			{
				rectangleF = new RectangleF(0f, 0f, dimensions.Width.PixelValue, dimensions.Height.PixelValue);
				if (rotation != null)
				{
					float num = rotation.Value;
					if (num % 360f != 0f)
					{
						using (GraphicsPath graphicsPath = new GraphicsPath())
						{
							graphicsPath.AddRectangle(rectangleF);
							Matrix matrix = new Matrix();
							matrix.Rotate(num);
							graphicsPath.Transform(matrix);
							rectangleF = graphicsPath.GetBounds();
						}
					}
				}
			}
			return rectangleF;
		}

		// Token: 0x0600EB69 RID: 60265 RVA: 0x00359A44 File Offset: 0x00357C44
		public virtual object Clone()
		{
			Style style = (Style)base.MemberwiseClone();
			style.ViewState = base.CloneState();
			style.styleBorder = (StyleBorder)this.styleBorder.Clone();
			style.styleShadow = (ShadowStyle)this.styleShadow.Clone();
			style.styleContainerObject = null;
			return style;
		}

		// Token: 0x0600EB6A RID: 60266 RVA: 0x00359AA0 File Offset: 0x00357CA0
		internal static object GetStyleProperty(object element, StyleProperties propertyName)
		{
			ChartLabel chartLabel = element as ChartLabel;
			if (chartLabel != null)
			{
				return chartLabel.Appearance[propertyName];
			}
			TextBlock textBlock = element as TextBlock;
			if (textBlock != null)
			{
				return textBlock.Appearance[propertyName];
			}
			ChartMarker chartMarker = element as ChartMarker;
			if (chartMarker != null)
			{
				return chartMarker.Appearance[propertyName];
			}
			SeriesItemLabel seriesItemLabel = element as SeriesItemLabel;
			if (seriesItemLabel != null)
			{
				return seriesItemLabel.Appearance[propertyName];
			}
			ExtendedLabel extendedLabel = element as ExtendedLabel;
			if (extendedLabel != null)
			{
				ChartLegend chartLegend = extendedLabel as ChartLegend;
				if (chartLegend != null)
				{
					return chartLegend.Appearance[propertyName];
				}
				return extendedLabel.Appearance[propertyName];
			}
			else
			{
				Chart chart = element as Chart;
				if (chart != null)
				{
					return chart.Appearance[propertyName];
				}
				ChartAxis chartAxis = element as ChartAxis;
				if (chartAxis != null)
				{
					return chartAxis.Appearance[propertyName];
				}
				ChartPlotArea chartPlotArea = element as ChartPlotArea;
				if (chartPlotArea != null)
				{
					return chartPlotArea.Appearance[propertyName];
				}
				ChartMarkedZone chartMarkedZone = element as ChartMarkedZone;
				if (chartMarkedZone != null)
				{
					return chartMarkedZone.Appearance[propertyName];
				}
				ChartDataTable chartDataTable = element as ChartDataTable;
				if (chartDataTable != null)
				{
					return chartDataTable.Appearance[propertyName];
				}
				return null;
			}
		}

		// Token: 0x0600EB6B RID: 60267 RVA: 0x00359BC1 File Offset: 0x00357DC1
		protected override void Dispose(bool disposing)
		{
			if (this.styleBorder != null)
			{
				this.styleBorder.Dispose();
				this.styleBorder = null;
			}
			if (this.styleShadow != null)
			{
				this.styleShadow.Dispose();
				this.styleShadow = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600EB6C RID: 60268 RVA: 0x00359BFE File Offset: 0x00357DFE
		protected override void TrackViewState()
		{
			base.TrackViewState();
			((IChartingStateManager)this.styleBorder).TrackViewState();
			((IChartingStateManager)this.styleShadow).TrackViewState();
		}

		// Token: 0x0600EB6D RID: 60269 RVA: 0x00359C1C File Offset: 0x00357E1C
		protected override void LoadViewState(object savedState)
		{
			object[] array = (object[])savedState;
			if (array != null)
			{
				base.LoadViewState(array[0]);
				((IChartingStateManager)this.styleBorder).LoadViewState(array[1]);
				((IChartingStateManager)this.styleShadow).LoadViewState(array[2]);
			}
		}

		// Token: 0x0600EB6E RID: 60270 RVA: 0x00359C58 File Offset: 0x00357E58
		protected override object SaveViewState()
		{
			return new ArrayList
			{
				base.SaveViewState(),
				((IChartingStateManager)this.styleBorder).SaveViewState(),
				((IChartingStateManager)this.styleShadow).SaveViewState()
			}.ToArray();
		}

		// Token: 0x04004413 RID: 17427
		internal ShadowStyle styleShadow;

		// Token: 0x04004414 RID: 17428
		internal StyleBorder styleBorder;

		// Token: 0x04004415 RID: 17429
		internal object styleContainerObject;

		// Token: 0x04004416 RID: 17430
		internal Chart styleChart;
	}
}
