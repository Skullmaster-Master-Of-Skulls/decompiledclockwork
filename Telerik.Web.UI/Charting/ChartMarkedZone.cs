using System;
using System.Collections;
using System.ComponentModel;
using System.Web.UI;
using Telerik.Charting.Styles;

namespace Telerik.Charting
{
	// Token: 0x02001736 RID: 5942
	[TypeConverter(typeof(ExpandableObjectConverter))]
	[PersistChildren(false)]
	[ParseChildren(true)]
	public class ChartMarkedZone : RenderedObject
	{
		// Token: 0x0600E761 RID: 59233 RVA: 0x0033C547 File Offset: 0x0033A747
		protected override void TrackViewState()
		{
			base.TrackViewState();
			((IChartingStateManager)this.chartMarkedZoneAppearance).TrackViewState();
			((IChartingStateManager)this.chartMarkedZoneLabel).TrackViewState();
		}

		// Token: 0x0600E762 RID: 59234 RVA: 0x0033C568 File Offset: 0x0033A768
		protected override void LoadViewState(object savedState)
		{
			object[] array = (object[])savedState;
			if (array != null)
			{
				base.LoadViewState(array[0]);
				((IChartingStateManager)this.chartMarkedZoneAppearance).LoadViewState(array[1]);
				((IChartingStateManager)this.chartMarkedZoneLabel).LoadViewState(array[2]);
			}
		}

		// Token: 0x0600E763 RID: 59235 RVA: 0x0033C5A4 File Offset: 0x0033A7A4
		protected override object SaveViewState()
		{
			return new ArrayList
			{
				base.SaveViewState(),
				((IChartingStateManager)this.chartMarkedZoneAppearance).SaveViewState(),
				((IChartingStateManager)this.chartMarkedZoneLabel).SaveViewState()
			}.ToArray();
		}

		// Token: 0x1700466A RID: 18026
		// (get) Token: 0x0600E764 RID: 59236 RVA: 0x0033C5EE File Offset: 0x0033A7EE
		// (set) Token: 0x0600E765 RID: 59237 RVA: 0x0033C5FB File Offset: 0x0033A7FB
		[DefaultValue(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[NotifyParentProperty(true)]
		public bool Visible
		{
			get
			{
				return this.chartMarkedZoneAppearance.Visible;
			}
			set
			{
				this.chartMarkedZoneAppearance.Visible = value;
			}
		}

		// Token: 0x1700466B RID: 18027
		// (get) Token: 0x0600E766 RID: 59238 RVA: 0x0033C609 File Offset: 0x0033A809
		[SkinnableProperty]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TypeConverter(typeof(ExpandableObjectConverter))]
		[NotifyParentProperty(true)]
		public ChartLabel Label
		{
			get
			{
				return this.chartMarkedZoneLabel;
			}
		}

		// Token: 0x1700466C RID: 18028
		// (get) Token: 0x0600E767 RID: 59239 RVA: 0x0033C611 File Offset: 0x0033A811
		[SkinnableProperty]
		[TypeConverter(typeof(ExpandableObjectConverter))]
		[NotifyParentProperty(true)]
		[Browsable(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public StyleMarkedZone Appearance
		{
			get
			{
				return this.chartMarkedZoneAppearance;
			}
		}

		// Token: 0x1700466D RID: 18029
		// (get) Token: 0x0600E768 RID: 59240 RVA: 0x0033C619 File Offset: 0x0033A819
		// (set) Token: 0x0600E769 RID: 59241 RVA: 0x0033C639 File Offset: 0x0033A839
		[NotifyParentProperty(true)]
		[DefaultValue("Marked zone")]
		public string Name
		{
			get
			{
				return (string)(base.ViewState["Name"] ?? "Marked zone");
			}
			set
			{
				base.ViewState["Name"] = value;
			}
		}

		// Token: 0x1700466E RID: 18030
		// (get) Token: 0x0600E76A RID: 59242 RVA: 0x0033C64C File Offset: 0x0033A84C
		// (set) Token: 0x0600E76B RID: 59243 RVA: 0x0033C66D File Offset: 0x0033A86D
		[DefaultValue(ChartYAxisType.Primary)]
		[NotifyParentProperty(true)]
		public ChartYAxisType YAxisType
		{
			get
			{
				return (ChartYAxisType)(base.ViewState["YAxisType"] ?? ChartYAxisType.Primary);
			}
			set
			{
				base.ViewState["YAxisType"] = value;
			}
		}

		// Token: 0x1700466F RID: 18031
		// (get) Token: 0x0600E76C RID: 59244 RVA: 0x0033C685 File Offset: 0x0033A885
		// (set) Token: 0x0600E76D RID: 59245 RVA: 0x0033C6AE File Offset: 0x0033A8AE
		[DefaultValue(0.0)]
		[NotifyParentProperty(true)]
		public double ValueStartX
		{
			get
			{
				return (double)(base.ViewState["ValueStartX"] ?? 0.0);
			}
			set
			{
				base.ViewState["ValueStartX"] = value;
			}
		}

		// Token: 0x17004670 RID: 18032
		// (get) Token: 0x0600E76E RID: 59246 RVA: 0x0033C6C6 File Offset: 0x0033A8C6
		// (set) Token: 0x0600E76F RID: 59247 RVA: 0x0033C6EF File Offset: 0x0033A8EF
		[DefaultValue(0.0)]
		[NotifyParentProperty(true)]
		public double ValueEndX
		{
			get
			{
				return (double)(base.ViewState["ValueEndX"] ?? 0.0);
			}
			set
			{
				base.ViewState["ValueEndX"] = value;
			}
		}

		// Token: 0x17004671 RID: 18033
		// (get) Token: 0x0600E770 RID: 59248 RVA: 0x0033C707 File Offset: 0x0033A907
		// (set) Token: 0x0600E771 RID: 59249 RVA: 0x0033C730 File Offset: 0x0033A930
		[DefaultValue(0.0)]
		[NotifyParentProperty(true)]
		public double ValueStartY
		{
			get
			{
				return (double)(base.ViewState["ValueStartY"] ?? 0.0);
			}
			set
			{
				base.ViewState["ValueStartY"] = value;
			}
		}

		// Token: 0x17004672 RID: 18034
		// (get) Token: 0x0600E772 RID: 59250 RVA: 0x0033C748 File Offset: 0x0033A948
		// (set) Token: 0x0600E773 RID: 59251 RVA: 0x0033C771 File Offset: 0x0033A971
		[NotifyParentProperty(true)]
		[DefaultValue(0.0)]
		public double ValueEndY
		{
			get
			{
				return (double)(base.ViewState["ValueEndY"] ?? 0.0);
			}
			set
			{
				base.ViewState["ValueEndY"] = value;
			}
		}

		// Token: 0x0600E774 RID: 59252 RVA: 0x0033C789 File Offset: 0x0033A989
		public ChartMarkedZone(IContainer container) : base(container)
		{
		}

		// Token: 0x0600E775 RID: 59253 RVA: 0x0033C792 File Offset: 0x0033A992
		public ChartMarkedZone() : base(null)
		{
			this.chartMarkedZoneLabel = new MarkedZoneLabel();
			this.chartMarkedZoneAppearance = new StyleMarkedZone();
		}

		// Token: 0x0600E776 RID: 59254 RVA: 0x0033C7B1 File Offset: 0x0033A9B1
		public ChartMarkedZone(string name) : this()
		{
			this.Name = name;
		}

		// Token: 0x0600E777 RID: 59255 RVA: 0x0033C7C0 File Offset: 0x0033A9C0
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x0600E778 RID: 59256 RVA: 0x0033C7C8 File Offset: 0x0033A9C8
		internal MarkedZoneType GetZoneType()
		{
			if (this.ValueStartX == 0.0 && this.ValueEndX == 0.0)
			{
				return MarkedZoneType.Horizontal;
			}
			if (this.ValueStartY == 0.0 && this.ValueEndY == 0.0)
			{
				return MarkedZoneType.Vertical;
			}
			return MarkedZoneType.Rectangular;
		}

		// Token: 0x0600E779 RID: 59257 RVA: 0x0033C81E File Offset: 0x0033AA1E
		protected override void Dispose(bool disposing)
		{
			if (this.chartMarkedZoneAppearance != null)
			{
				this.chartMarkedZoneAppearance.Dispose();
				this.chartMarkedZoneAppearance = null;
			}
			if (this.chartMarkedZoneLabel != null)
			{
				this.chartMarkedZoneLabel.Dispose();
				this.chartMarkedZoneLabel = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x0400427F RID: 17023
		private StyleMarkedZone chartMarkedZoneAppearance;

		// Token: 0x04004280 RID: 17024
		private MarkedZoneLabel chartMarkedZoneLabel;
	}
}
