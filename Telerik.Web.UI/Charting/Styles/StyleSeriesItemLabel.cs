using System;
using System.Collections;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Charting.Styles
{
	// Token: 0x020017D7 RID: 6103
	[TypeConverter(typeof(ExpandableObjectConverter))]
	[ParseChildren(true)]
	[PersistChildren(false)]
	public class StyleSeriesItemLabel : StyleLabel
	{
		// Token: 0x0600ED92 RID: 60818 RVA: 0x003629E8 File Offset: 0x00360BE8
		public StyleSeriesItemLabel() : this(null)
		{
		}

		// Token: 0x0600ED93 RID: 60819 RVA: 0x003629F4 File Offset: 0x00360BF4
		public StyleSeriesItemLabel(ChartSeries series) : base(new PositionTop())
		{
			Position position = this.position;
			this.dimensions.containerObject = series;
			position.positionContainerObject = series;
			this.styleSeriesItemLabelLabelConnectorStyle = new StyleItemLabelConnector();
			if (series != null && series.Parent != null)
			{
				this.styleChart = series.Parent.Parent;
			}
		}

		// Token: 0x170047E8 RID: 18408
		// (get) Token: 0x0600ED94 RID: 60820 RVA: 0x00362A4D File Offset: 0x00360C4D
		// (set) Token: 0x0600ED95 RID: 60821 RVA: 0x00362A6E File Offset: 0x00360C6E
		[RefreshProperties(RefreshProperties.Repaint)]
		[DefaultValue(0)]
		[SkinnableProperty]
		public int Distance
		{
			get
			{
				return (int)(base.ViewState["Distance"] ?? 0);
			}
			set
			{
				base.ViewState["Distance"] = value;
			}
		}

		// Token: 0x170047E9 RID: 18409
		// (get) Token: 0x0600ED96 RID: 60822 RVA: 0x00362A86 File Offset: 0x00360C86
		// (set) Token: 0x0600ED97 RID: 60823 RVA: 0x00362AA7 File Offset: 0x00360CA7
		[DefaultValue(true)]
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[RefreshProperties(RefreshProperties.Repaint)]
		public override bool Visible
		{
			get
			{
				return (bool)(base.ViewState["Visible"] ?? true);
			}
			set
			{
				base.Visible = value;
			}
		}

		// Token: 0x170047EA RID: 18410
		// (get) Token: 0x0600ED98 RID: 60824 RVA: 0x00362AB0 File Offset: 0x00360CB0
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[SkinnableProperty]
		[Browsable(true)]
		public LineStyle LabelConnectorStyle
		{
			get
			{
				return this.styleSeriesItemLabelLabelConnectorStyle;
			}
		}

		// Token: 0x170047EB RID: 18411
		// (get) Token: 0x0600ED99 RID: 60825 RVA: 0x00362AB8 File Offset: 0x00360CB8
		// (set) Token: 0x0600ED9A RID: 60826 RVA: 0x00362AD9 File Offset: 0x00360CD9
		[SkinnableProperty]
		[RefreshProperties(RefreshProperties.Repaint)]
		[DefaultValue(typeof(StyleSeriesItemLabel.ItemLabelLocation), "Auto")]
		[NotifyParentProperty(true)]
		public StyleSeriesItemLabel.ItemLabelLocation LabelLocation
		{
			get
			{
				return (StyleSeriesItemLabel.ItemLabelLocation)(base.ViewState["LabelLocation"] ?? StyleSeriesItemLabel.ItemLabelLocation.Auto);
			}
			set
			{
				base.ViewState["LabelLocation"] = value;
			}
		}

		// Token: 0x0600ED9B RID: 60827 RVA: 0x00362AF1 File Offset: 0x00360CF1
		internal override void Reset()
		{
			base.Reset();
			this.LabelLocation = StyleSeriesItemLabel.ItemLabelLocation.Auto;
			this.position = new PositionTop();
			this.styleSeriesItemLabelLabelConnectorStyle = new StyleItemLabelConnector();
			this.Distance = 0;
		}

		// Token: 0x170047EC RID: 18412
		internal override object this[StyleProperties name]
		{
			get
			{
				switch (name)
				{
				case StyleProperties.LabelLocation:
					return this.LabelLocation;
				case StyleProperties.ShowLabelConnector:
					return this.LabelConnectorStyle.Visible;
				case StyleProperties.LabelConnectorStyle:
					return this.LabelConnectorStyle;
				default:
					if (name != StyleProperties.Distance)
					{
						return base[name];
					}
					return this.Distance;
				}
			}
		}

		// Token: 0x0600ED9D RID: 60829 RVA: 0x00362B84 File Offset: 0x00360D84
		public override object Clone()
		{
			StyleSeriesItemLabel styleSeriesItemLabel = (StyleSeriesItemLabel)base.Clone();
			styleSeriesItemLabel.styleSeriesItemLabelLabelConnectorStyle = (LineStyle)this.styleSeriesItemLabelLabelConnectorStyle.Clone();
			return styleSeriesItemLabel;
		}

		// Token: 0x0600ED9E RID: 60830 RVA: 0x00362BB4 File Offset: 0x00360DB4
		protected override void TrackViewState()
		{
			base.TrackViewState();
			((IChartingStateManager)this.styleSeriesItemLabelLabelConnectorStyle).TrackViewState();
		}

		// Token: 0x0600ED9F RID: 60831 RVA: 0x00362BC8 File Offset: 0x00360DC8
		protected override void LoadViewState(object savedState)
		{
			object[] array = (object[])savedState;
			if (array != null)
			{
				base.LoadViewState(array[0]);
				((IChartingStateManager)this.styleSeriesItemLabelLabelConnectorStyle).LoadViewState(array[1]);
			}
		}

		// Token: 0x0600EDA0 RID: 60832 RVA: 0x00362BF8 File Offset: 0x00360DF8
		protected override object SaveViewState()
		{
			return new ArrayList
			{
				base.SaveViewState(),
				((IChartingStateManager)this.styleSeriesItemLabelLabelConnectorStyle).SaveViewState()
			}.ToArray();
		}

		// Token: 0x040044B3 RID: 17587
		internal LineStyle styleSeriesItemLabelLabelConnectorStyle;

		// Token: 0x040044B4 RID: 17588
		internal bool styleSeriesItemLabelIsSet;

		// Token: 0x020017D8 RID: 6104
		public enum ItemLabelLocation
		{
			// Token: 0x040044B6 RID: 17590
			Inside,
			// Token: 0x040044B7 RID: 17591
			Outside,
			// Token: 0x040044B8 RID: 17592
			Auto
		}
	}
}
