using System;
using System.Collections;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Charting.Styles
{
	// Token: 0x020017DF RID: 6111
	[TypeConverter(typeof(ExpandableObjectConverter))]
	[ParseChildren(true)]
	[PersistChildren(false)]
	public class StyleMarkedZone : Style
	{
		// Token: 0x170047F1 RID: 18417
		// (get) Token: 0x0600EDB5 RID: 60853 RVA: 0x00362E90 File Offset: 0x00361090
		[Browsable(true)]
		[SkinnableProperty]
		[NotifyParentProperty(true)]
		[TypeConverter(typeof(ExpandableObjectConverter))]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public FillStyleMarkedZones FillStyle
		{
			get
			{
				return this.styleMarkedZoneFillStyle;
			}
		}

		// Token: 0x170047F2 RID: 18418
		internal override object this[StyleProperties name]
		{
			get
			{
				if (name == StyleProperties.FillStyle)
				{
					return this.FillStyle;
				}
				return base[name];
			}
		}

		// Token: 0x0600EDB7 RID: 60855 RVA: 0x00362EBA File Offset: 0x003610BA
		public StyleMarkedZone()
		{
			this.styleMarkedZoneFillStyle = new FillStyleMarkedZones();
		}

		// Token: 0x0600EDB8 RID: 60856 RVA: 0x00362ECD File Offset: 0x003610CD
		public StyleMarkedZone(FillStyleMarkedZones fillStyle, StyleBorder border, ShadowStyle shadowStyle, bool visible) : base(border, visible, shadowStyle)
		{
			this.styleMarkedZoneFillStyle = fillStyle;
		}

		// Token: 0x0600EDB9 RID: 60857 RVA: 0x00362EE0 File Offset: 0x003610E0
		internal override void Reset()
		{
			base.Reset();
			this.styleMarkedZoneFillStyle = new FillStyleMarkedZones();
		}

		// Token: 0x0600EDBA RID: 60858 RVA: 0x00362EF3 File Offset: 0x003610F3
		protected override void TrackViewState()
		{
			base.TrackViewState();
			((IChartingStateManager)this.styleMarkedZoneFillStyle).TrackViewState();
		}

		// Token: 0x0600EDBB RID: 60859 RVA: 0x00362F08 File Offset: 0x00361108
		protected override void LoadViewState(object savedState)
		{
			object[] array = (object[])savedState;
			if (array != null)
			{
				base.LoadViewState(array[0]);
				((IChartingStateManager)this.styleMarkedZoneFillStyle).LoadViewState(array[1]);
			}
		}

		// Token: 0x0600EDBC RID: 60860 RVA: 0x00362F38 File Offset: 0x00361138
		protected override object SaveViewState()
		{
			return new ArrayList
			{
				base.SaveViewState(),
				((IChartingStateManager)this.styleMarkedZoneFillStyle).SaveViewState()
			}.ToArray();
		}

		// Token: 0x0600EDBD RID: 60861 RVA: 0x00362F70 File Offset: 0x00361170
		protected override void Dispose(bool disposing)
		{
			if (this.styleMarkedZoneFillStyle != null)
			{
				this.styleMarkedZoneFillStyle.Dispose();
				this.styleMarkedZoneFillStyle = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x040044BC RID: 17596
		internal FillStyleMarkedZones styleMarkedZoneFillStyle;
	}
}
