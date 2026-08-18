using System;
using System.ComponentModel;
using System.Drawing;
using System.Web.UI;
using Telerik.Charting;

namespace Telerik.Web.UI
{
	// Token: 0x02001802 RID: 6146
	[TypeConverter(typeof(ExpandableObjectConverter))]
	[ParseChildren(true)]
	[PersistChildren(false)]
	public class ChartClientSettings : StateManagedObject, ICloneable
	{
		// Token: 0x17004841 RID: 18497
		// (get) Token: 0x0600EEF0 RID: 61168 RVA: 0x003663A8 File Offset: 0x003645A8
		// (set) Token: 0x0600EEF1 RID: 61169 RVA: 0x003663C9 File Offset: 0x003645C9
		[Description("Enables or disables the zoom assist axis markers.")]
		[NotifyParentProperty(true)]
		[DefaultValue(true)]
		public bool EnableAxisMarkers
		{
			get
			{
				return (bool)(base.ViewState["EnableAxisMarkers"] ?? true);
			}
			set
			{
				base.ViewState["EnableAxisMarkers"] = value;
			}
		}

		// Token: 0x17004842 RID: 18498
		// (get) Token: 0x0600EEF2 RID: 61170 RVA: 0x003663E1 File Offset: 0x003645E1
		// (set) Token: 0x0600EEF3 RID: 61171 RVA: 0x00366406 File Offset: 0x00364606
		[Description("Specifies the color of the zoom assist axis markers.")]
		[NotifyParentProperty(true)]
		[DefaultValue(typeof(Color), "Red")]
		public Color AxisMarkersColor
		{
			get
			{
				return (Color)(base.ViewState["AxisMarkersColor"] ?? Color.Red);
			}
			set
			{
				base.ViewState["AxisMarkersColor"] = value;
			}
		}

		// Token: 0x17004843 RID: 18499
		// (get) Token: 0x0600EEF4 RID: 61172 RVA: 0x00366420 File Offset: 0x00364620
		// (set) Token: 0x0600EEF5 RID: 61173 RVA: 0x0036644F File Offset: 0x0036464F
		[Description("Specifies the size of the axis markers in pixels.")]
		[NotifyParentProperty(true)]
		[DefaultValue(20)]
		public int AxisMarkersSize
		{
			get
			{
				int num = 20;
				return (int)(base.ViewState["AxisMarkersSize"] ?? num);
			}
			set
			{
				base.ViewState["AxisMarkersSize"] = value;
			}
		}

		// Token: 0x17004844 RID: 18500
		// (get) Token: 0x0600EEF6 RID: 61174 RVA: 0x00366467 File Offset: 0x00364667
		// (set) Token: 0x0600EEF7 RID: 61175 RVA: 0x00366488 File Offset: 0x00364688
		[NotifyParentProperty(true)]
		[Description("Enables or disables client-side zoom functionality.")]
		[DefaultValue(true)]
		public bool EnableZoom
		{
			get
			{
				return (bool)(base.ViewState["EnableZoom"] ?? true);
			}
			set
			{
				base.ViewState["EnableZoom"] = value;
			}
		}

		// Token: 0x17004845 RID: 18501
		// (get) Token: 0x0600EEF8 RID: 61176 RVA: 0x003664A0 File Offset: 0x003646A0
		// (set) Token: 0x0600EEF9 RID: 61177 RVA: 0x003664CD File Offset: 0x003646CD
		[Description("Specifies the color of the zoom rectangle.")]
		[NotifyParentProperty(true)]
		[DefaultValue(typeof(Color), "51, 0, 153")]
		public Color ZoomRectangleColor
		{
			get
			{
				return (Color)(base.ViewState["ZoomRectangleColor"] ?? Color.FromArgb(51, 0, 153));
			}
			set
			{
				base.ViewState["ZoomRectangleColor"] = value;
			}
		}

		// Token: 0x17004846 RID: 18502
		// (get) Token: 0x0600EEFA RID: 61178 RVA: 0x003664E5 File Offset: 0x003646E5
		// (set) Token: 0x0600EEFB RID: 61179 RVA: 0x0036650A File Offset: 0x0036470A
		[DefaultValue(0.2f)]
		[NotifyParentProperty(true)]
		[Description("Specifies the opacity of the zoom rectangle.")]
		public float ZoomRectangleOpacity
		{
			get
			{
				return (float)(base.ViewState["ZoomRectangleOpacity"] ?? 0.2f);
			}
			set
			{
				base.ViewState["ZoomRectangleOpacity"] = value;
			}
		}

		// Token: 0x17004847 RID: 18503
		// (get) Token: 0x0600EEFC RID: 61180 RVA: 0x00366522 File Offset: 0x00364722
		// (set) Token: 0x0600EEFD RID: 61181 RVA: 0x00366543 File Offset: 0x00364743
		[NotifyParentProperty(true)]
		[DefaultValue(typeof(ChartClientScrollMode), "None")]
		[Description("PlotArea client scroll mode.")]
		public ChartClientScrollMode ScrollMode
		{
			get
			{
				return (ChartClientScrollMode)(base.ViewState["ClientScrollMode"] ?? ChartClientScrollMode.None);
			}
			set
			{
				base.ViewState["ClientScrollMode"] = value;
			}
		}

		// Token: 0x17004848 RID: 18504
		// (get) Token: 0x0600EEFE RID: 61182 RVA: 0x0036655B File Offset: 0x0036475B
		// (set) Token: 0x0600EEFF RID: 61183 RVA: 0x00366580 File Offset: 0x00364780
		[DefaultValue(0f)]
		[NotifyParentProperty(true)]
		[Description("YScroll offset ratio")]
		public float YScrollOffset
		{
			get
			{
				return (float)(base.ViewState["YScrollOffset"] ?? 0f);
			}
			set
			{
				if (value > 1f || value < 0f)
				{
					throw new ChartException("Scroll offset must be between 0 and 1");
				}
				base.ViewState["YScrollOffset"] = value;
			}
		}

		// Token: 0x17004849 RID: 18505
		// (get) Token: 0x0600EF00 RID: 61184 RVA: 0x003665B3 File Offset: 0x003647B3
		// (set) Token: 0x0600EF01 RID: 61185 RVA: 0x003665D8 File Offset: 0x003647D8
		[DefaultValue(0f)]
		[NotifyParentProperty(true)]
		[Description("XScroll offset ratio")]
		public float XScrollOffset
		{
			get
			{
				return (float)(base.ViewState["XScrollOffset"] ?? 0f);
			}
			set
			{
				if (value > 1f || value < 0f)
				{
					throw new ChartException("Scroll offset must be between 0 and 1");
				}
				base.ViewState["XScrollOffset"] = value;
			}
		}

		// Token: 0x1700484A RID: 18506
		// (get) Token: 0x0600EF02 RID: 61186 RVA: 0x0036660B File Offset: 0x0036480B
		// (set) Token: 0x0600EF03 RID: 61187 RVA: 0x0036663E File Offset: 0x0036483E
		[DefaultValue(1f)]
		[NotifyParentProperty(true)]
		[Description("PlotArea scale value by Y axis.")]
		public float YScale
		{
			get
			{
				if (this.ScrollMode == ChartClientScrollMode.XOnly)
				{
					return 1f;
				}
				return (float)(base.ViewState["ClientScrollYScale"] ?? 1f);
			}
			set
			{
				if (value < 1f)
				{
					throw new ChartException("Scale factor must be greater or equal to 1");
				}
				if (value > 20f)
				{
					value = 20f;
				}
				base.ViewState["ClientScrollYScale"] = value;
			}
		}

		// Token: 0x1700484B RID: 18507
		// (get) Token: 0x0600EF04 RID: 61188 RVA: 0x00366678 File Offset: 0x00364878
		// (set) Token: 0x0600EF05 RID: 61189 RVA: 0x003666AC File Offset: 0x003648AC
		[DefaultValue(1f)]
		[NotifyParentProperty(true)]
		[Description("PlotArea scale value by X axis.")]
		public float XScale
		{
			get
			{
				if (this.ScrollMode == ChartClientScrollMode.YOnly)
				{
					return 1f;
				}
				return (float)(base.ViewState["ClientScrollXScale"] ?? 1f);
			}
			set
			{
				if (value < 1f)
				{
					throw new ChartException("Scale factor must be greater or equal to 1");
				}
				if (value > 20f)
				{
					value = 20f;
				}
				base.ViewState["ClientScrollXScale"] = value;
			}
		}

		// Token: 0x0600EF06 RID: 61190 RVA: 0x003666E6 File Offset: 0x003648E6
		public override string ToString()
		{
			return string.Empty;
		}

		// Token: 0x0600EF07 RID: 61191 RVA: 0x003666F0 File Offset: 0x003648F0
		public object Clone()
		{
			ChartClientSettings chartClientSettings = (ChartClientSettings)base.MemberwiseClone();
			chartClientSettings.ViewState = base.CloneState();
			return chartClientSettings;
		}
	}
}
