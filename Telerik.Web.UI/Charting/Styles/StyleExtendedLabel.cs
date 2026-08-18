using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Web.UI;

namespace Telerik.Charting.Styles
{
	// Token: 0x020017D6 RID: 6102
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class StyleExtendedLabel : StyleLabel
	{
		// Token: 0x170047E1 RID: 18401
		// (get) Token: 0x0600ED7B RID: 60795 RVA: 0x003625AF File Offset: 0x003607AF
		// (set) Token: 0x0600ED7C RID: 60796 RVA: 0x003625D0 File Offset: 0x003607D0
		[DefaultValue(typeof(LabelLocation), "OutsidePlotArea")]
		[NotifyParentProperty(true)]
		[SkinnableProperty]
		public LabelLocation Location
		{
			get
			{
				return (LabelLocation)(base.ViewState["Location"] ?? LabelLocation.OutsidePlotArea);
			}
			set
			{
				base.ViewState["Location"] = value;
				ExtendedLabel extendedLabel = this.styleContainerObject as ExtendedLabel;
				if (extendedLabel != null)
				{
					extendedLabel.Container.OrderList.Remove(extendedLabel);
					if (extendedLabel.Appearance != null)
					{
						switch (extendedLabel.Appearance.Location)
						{
						case LabelLocation.InsidePlotArea:
							base.Chart.PlotArea.Add(extendedLabel);
							return;
						}
						base.Chart.OrderList.Insert(1, extendedLabel);
						extendedLabel.Container = base.Chart;
					}
				}
			}
		}

		// Token: 0x170047E2 RID: 18402
		// (get) Token: 0x0600ED7D RID: 60797 RVA: 0x00362668 File Offset: 0x00360868
		[TypeConverter(typeof(ExpandableObjectConverter))]
		[SkinnableProperty]
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public virtual StyleLabel ItemAppearance
		{
			get
			{
				return this.styleExtendedLabelItemAppearance;
			}
		}

		// Token: 0x170047E3 RID: 18403
		// (get) Token: 0x0600ED7E RID: 60798 RVA: 0x00362670 File Offset: 0x00360870
		[SkinnableProperty]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TypeConverter(typeof(ExpandableObjectConverter))]
		[NotifyParentProperty(true)]
		public virtual StyleTextBlock ItemTextAppearance
		{
			get
			{
				return this.styleExtendedLabelItemTextAppearance;
			}
		}

		// Token: 0x170047E4 RID: 18404
		// (get) Token: 0x0600ED7F RID: 60799 RVA: 0x00362678 File Offset: 0x00360878
		[NotifyParentProperty(true)]
		[TypeConverter(typeof(ExpandableObjectConverter))]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[SkinnableProperty]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public virtual StyleMarker ItemMarkerAppearance
		{
			get
			{
				return this.styleExtendedLabelItemMarkerAppearance;
			}
		}

		// Token: 0x170047E5 RID: 18405
		// (get) Token: 0x0600ED80 RID: 60800 RVA: 0x00362680 File Offset: 0x00360880
		// (set) Token: 0x0600ED81 RID: 60801 RVA: 0x003626A1 File Offset: 0x003608A1
		[DefaultValue(typeof(Overflow), "Column")]
		[SkinnableProperty]
		[NotifyParentProperty(true)]
		public virtual Overflow Overflow
		{
			get
			{
				return (Overflow)(base.ViewState["Overflow"] ?? Overflow.Column);
			}
			set
			{
				base.ViewState["Overflow"] = value;
			}
		}

		// Token: 0x170047E6 RID: 18406
		internal override object this[StyleProperties name]
		{
			get
			{
				switch (name)
				{
				case StyleProperties.SubItemAppearance:
					return this.styleExtendedLabelItemAppearance;
				case StyleProperties.SubItemTextBlockAppearance:
					return this.styleExtendedLabelItemTextAppearance;
				case StyleProperties.SubItemMarkerAppearance:
					return this.styleExtendedLabelItemMarkerAppearance;
				default:
					if (name == StyleProperties.Overflow)
					{
						return this.Overflow;
					}
					if (name != StyleProperties.Location)
					{
						return base[name];
					}
					return this.Location;
				}
			}
		}

		// Token: 0x170047E7 RID: 18407
		// (get) Token: 0x0600ED83 RID: 60803 RVA: 0x00362720 File Offset: 0x00360920
		// (set) Token: 0x0600ED84 RID: 60804 RVA: 0x00362740 File Offset: 0x00360940
		[NotifyParentProperty(true)]
		[Description("Specifies the series names format shown in Legend when data grouping being used and names are digits.")]
		[DefaultValue("")]
		public string GroupNameFormat
		{
			get
			{
				return (string)(base.ViewState["GroupNameFormat"] ?? string.Empty);
			}
			set
			{
				base.ViewState["GroupNameFormat"] = value;
			}
		}

		// Token: 0x0600ED85 RID: 60805 RVA: 0x00362753 File Offset: 0x00360953
		public StyleExtendedLabel(ChartSeries series) : base(series)
		{
			this.styleExtendedLabelItemAppearance = new StyleLabel();
			this.styleExtendedLabelItemMarkerAppearance = new StyleMarker();
			this.styleExtendedLabelItemTextAppearance = new StyleTextBlock();
		}

		// Token: 0x0600ED86 RID: 60806 RVA: 0x0036277D File Offset: 0x0036097D
		public StyleExtendedLabel()
		{
		}

		// Token: 0x0600ED87 RID: 60807 RVA: 0x00362785 File Offset: 0x00360985
		public StyleExtendedLabel(FillStyle fillStyle) : this(fillStyle, null)
		{
		}

		// Token: 0x0600ED88 RID: 60808 RVA: 0x0036278F File Offset: 0x0036098F
		public StyleExtendedLabel(Position position) : this(null, position)
		{
		}

		// Token: 0x0600ED89 RID: 60809 RVA: 0x00362799 File Offset: 0x00360999
		public StyleExtendedLabel(FillStyle fillStyle, Position position) : this(fillStyle, position, null)
		{
		}

		// Token: 0x0600ED8A RID: 60810 RVA: 0x003627A4 File Offset: 0x003609A4
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public StyleExtendedLabel(FillStyle fillStyle, Position position, Dimensions dimensions) : base(fillStyle, position, dimensions)
		{
			this.Overflow = Overflow.Column;
			this.styleExtendedLabelItemAppearance = new StyleLabel();
			this.styleExtendedLabelItemMarkerAppearance = new StyleMarker();
			this.styleExtendedLabelItemTextAppearance = new StyleTextBlock();
		}

		// Token: 0x0600ED8B RID: 60811 RVA: 0x003627D8 File Offset: 0x003609D8
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public StyleExtendedLabel(LabelItemsCompositionTypes compositionType, Dimensions dimensions, string figure, FillStyle fillStyle, Overflow overflow, Position position, float rotationAngle, Corners corners, StyleBorder border, ShadowStyle shadowStyle, bool visible) : base(compositionType, dimensions, figure, fillStyle, position, rotationAngle, corners, border, shadowStyle, visible)
		{
			this.Overflow = overflow;
		}

		// Token: 0x0600ED8C RID: 60812 RVA: 0x00362804 File Offset: 0x00360A04
		internal override void Reset()
		{
			base.Reset();
			this.styleExtendedLabelItemAppearance = new StyleLabel();
			this.styleExtendedLabelItemMarkerAppearance = new StyleMarker();
			this.styleExtendedLabelItemTextAppearance = new StyleTextBlock();
			this.Overflow = Overflow.Column;
			this.Location = LabelLocation.OutsidePlotArea;
			this.styleExtendedLabelItemTextAppearance.Position.Reset();
		}

		// Token: 0x0600ED8D RID: 60813 RVA: 0x00362858 File Offset: 0x00360A58
		public override object Clone()
		{
			StyleExtendedLabel styleExtendedLabel = (StyleExtendedLabel)base.Clone();
			styleExtendedLabel.styleExtendedLabelItemAppearance = (StyleLabel)this.styleExtendedLabelItemAppearance.Clone();
			styleExtendedLabel.styleExtendedLabelItemMarkerAppearance = (StyleMarker)this.styleExtendedLabelItemMarkerAppearance.Clone();
			styleExtendedLabel.styleExtendedLabelItemTextAppearance = (StyleTextBlock)this.styleExtendedLabelItemTextAppearance.Clone();
			return styleExtendedLabel;
		}

		// Token: 0x0600ED8E RID: 60814 RVA: 0x003628B4 File Offset: 0x00360AB4
		protected override void Dispose(bool disposing)
		{
			if (this.styleExtendedLabelItemAppearance != null)
			{
				this.styleExtendedLabelItemAppearance.Dispose();
				this.styleExtendedLabelItemAppearance = null;
			}
			if (this.styleExtendedLabelItemMarkerAppearance != null)
			{
				this.styleExtendedLabelItemMarkerAppearance.Dispose();
				this.styleExtendedLabelItemMarkerAppearance = null;
			}
			if (this.styleExtendedLabelItemTextAppearance != null)
			{
				this.styleExtendedLabelItemTextAppearance.Dispose();
				this.styleExtendedLabelItemTextAppearance = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600ED8F RID: 60815 RVA: 0x00362916 File Offset: 0x00360B16
		protected override void TrackViewState()
		{
			base.TrackViewState();
			((IChartingStateManager)this.styleExtendedLabelItemAppearance).TrackViewState();
			((IChartingStateManager)this.styleExtendedLabelItemMarkerAppearance).TrackViewState();
			((IChartingStateManager)this.styleExtendedLabelItemTextAppearance).TrackViewState();
		}

		// Token: 0x0600ED90 RID: 60816 RVA: 0x00362940 File Offset: 0x00360B40
		protected override void LoadViewState(object savedState)
		{
			object[] array = (object[])savedState;
			if (array != null)
			{
				base.LoadViewState(array[0]);
				((IChartingStateManager)this.styleExtendedLabelItemAppearance).LoadViewState(array[1]);
				((IChartingStateManager)this.styleExtendedLabelItemMarkerAppearance).LoadViewState(array[2]);
				((IChartingStateManager)this.styleExtendedLabelItemTextAppearance).LoadViewState(array[3]);
			}
		}

		// Token: 0x0600ED91 RID: 60817 RVA: 0x0036298C File Offset: 0x00360B8C
		protected override object SaveViewState()
		{
			return new ArrayList
			{
				base.SaveViewState(),
				((IChartingStateManager)this.styleExtendedLabelItemAppearance).SaveViewState(),
				((IChartingStateManager)this.styleExtendedLabelItemMarkerAppearance).SaveViewState(),
				((IChartingStateManager)this.styleExtendedLabelItemTextAppearance).SaveViewState()
			}.ToArray();
		}

		// Token: 0x040044B0 RID: 17584
		protected StyleLabel styleExtendedLabelItemAppearance;

		// Token: 0x040044B1 RID: 17585
		protected StyleMarker styleExtendedLabelItemMarkerAppearance;

		// Token: 0x040044B2 RID: 17586
		protected StyleTextBlock styleExtendedLabelItemTextAppearance;
	}
}
