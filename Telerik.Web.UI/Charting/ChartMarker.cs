using System;
using System.Collections;
using System.ComponentModel;
using System.Web.UI;
using Telerik.Charting.Styles;

namespace Telerik.Charting
{
	// Token: 0x0200170A RID: 5898
	[PersistChildren(false)]
	[TypeConverter(typeof(ExpandableObjectConverter))]
	[ParseChildren(true)]
	public class ChartMarker : LayoutElement, IActiveRegion
	{
		// Token: 0x170045E1 RID: 17889
		// (get) Token: 0x0600E54D RID: 58701 RVA: 0x0032EFE4 File Offset: 0x0032D1E4
		// (set) Token: 0x0600E54E RID: 58702 RVA: 0x0032EFF1 File Offset: 0x0032D1F1
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[DefaultValue(false)]
		public virtual bool Visible
		{
			get
			{
				return this.appearance.Visible;
			}
			set
			{
				this.appearance.Visible = value;
			}
		}

		// Token: 0x170045E2 RID: 17890
		// (get) Token: 0x0600E54F RID: 58703 RVA: 0x0032EFFF File Offset: 0x0032D1FF
		// (set) Token: 0x0600E550 RID: 58704 RVA: 0x0032F007 File Offset: 0x0032D207
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[NotifyParentProperty(true)]
		[Browsable(false)]
		public object Parent
		{
			get
			{
				return this.chartMarkerParent;
			}
			set
			{
				this.chartMarkerParent = value;
			}
		}

		// Token: 0x170045E3 RID: 17891
		// (get) Token: 0x0600E551 RID: 58705 RVA: 0x0032F010 File Offset: 0x0032D210
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[TypeConverter(typeof(ExpandableObjectConverter))]
		[SkinnableProperty]
		[Browsable(true)]
		public StyleMarker Appearance
		{
			get
			{
				return (StyleMarker)this.appearance;
			}
		}

		// Token: 0x0600E552 RID: 58706 RVA: 0x0032F01D File Offset: 0x0032D21D
		public ChartMarker() : this(null, null)
		{
		}

		// Token: 0x0600E553 RID: 58707 RVA: 0x0032F027 File Offset: 0x0032D227
		public ChartMarker(object parent) : this(parent, null)
		{
			this.chartMarkerParent = parent;
		}

		// Token: 0x0600E554 RID: 58708 RVA: 0x0032F038 File Offset: 0x0032D238
		public ChartMarker(IContainer container) : this(null, container)
		{
		}

		// Token: 0x0600E555 RID: 58709 RVA: 0x0032F042 File Offset: 0x0032D242
		public ChartMarker(object parent, IContainer container) : base(new StyleMarker(), container)
		{
			this.chartMarkerParent = parent;
			this.chartMarkerActiveRegion = new ActiveRegion(this);
		}

		// Token: 0x0600E556 RID: 58710 RVA: 0x0032F063 File Offset: 0x0032D263
		public void CopyFrom(ChartMarker marker)
		{
			this.appearance = (StyleMarker)marker.Appearance.Clone();
			this.objectContainer = marker.Container;
			this.chartMarkerParent = marker.Parent;
		}

		// Token: 0x170045E4 RID: 17892
		// (get) Token: 0x0600E557 RID: 58711 RVA: 0x0032F093 File Offset: 0x0032D293
		// (set) Token: 0x0600E558 RID: 58712 RVA: 0x0032F09B File Offset: 0x0032D29B
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Browsable(true)]
		public ActiveRegion ActiveRegion
		{
			get
			{
				return this.chartMarkerActiveRegion;
			}
			set
			{
				this.chartMarkerActiveRegion = value;
			}
		}

		// Token: 0x0600E559 RID: 58713 RVA: 0x0032F0A4 File Offset: 0x0032D2A4
		protected override void TrackViewState()
		{
			base.TrackViewState();
			((IChartingStateManager)this.chartMarkerActiveRegion).TrackViewState();
		}

		// Token: 0x0600E55A RID: 58714 RVA: 0x0032F0B8 File Offset: 0x0032D2B8
		protected override void LoadViewState(object savedState)
		{
			object[] array = (object[])savedState;
			if (array != null)
			{
				base.LoadViewState(array[0]);
				((IChartingStateManager)this.chartMarkerActiveRegion).LoadViewState(array[1]);
			}
		}

		// Token: 0x0600E55B RID: 58715 RVA: 0x0032F0E8 File Offset: 0x0032D2E8
		protected override object SaveViewState()
		{
			return new ArrayList
			{
				base.SaveViewState(),
				((IChartingStateManager)this.chartMarkerActiveRegion).SaveViewState()
			}.ToArray();
		}

		// Token: 0x0600E55C RID: 58716 RVA: 0x0032F120 File Offset: 0x0032D320
		protected override void Dispose(bool disposing)
		{
			if (this.chartMarkerActiveRegion != null)
			{
				this.chartMarkerActiveRegion.Dispose();
				this.chartMarkerActiveRegion = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x04004204 RID: 16900
		private object chartMarkerParent;

		// Token: 0x04004205 RID: 16901
		protected ActiveRegion chartMarkerActiveRegion;
	}
}
