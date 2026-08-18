using System;
using System.ComponentModel;

namespace Telerik.Charting
{
	// Token: 0x02001737 RID: 5943
	public class ChartMarkedZonesCollection : ChartingStateManagedCollection<ChartMarkedZone>
	{
		// Token: 0x17004673 RID: 18035
		// (get) Token: 0x0600E77A RID: 59258 RVA: 0x0033C85B File Offset: 0x0033AA5B
		// (set) Token: 0x0600E77B RID: 59259 RVA: 0x0033C863 File Offset: 0x0033AA63
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[NotifyParentProperty(true)]
		public ChartPlotArea Parent
		{
			get
			{
				return this.chartMarkedZonesCollectionParent;
			}
			set
			{
				this.chartMarkedZonesCollectionParent = value;
			}
		}

		// Token: 0x17004674 RID: 18036
		[NotifyParentProperty(false)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override ChartMarkedZone this[int index]
		{
			get
			{
				return base.List[index];
			}
			set
			{
				base.List[index] = value;
				this.Parent.Add(base.List[index]);
			}
		}

		// Token: 0x0600E77E RID: 59262 RVA: 0x0033C8A0 File Offset: 0x0033AAA0
		public ChartMarkedZonesCollection()
		{
		}

		// Token: 0x0600E77F RID: 59263 RVA: 0x0033C8A8 File Offset: 0x0033AAA8
		public ChartMarkedZonesCollection(ChartPlotArea parent)
		{
			this.chartMarkedZonesCollectionParent = parent;
		}

		// Token: 0x0600E780 RID: 59264 RVA: 0x0033C8B7 File Offset: 0x0033AAB7
		public override void Add(ChartMarkedZone item)
		{
			base.Add(item);
		}

		// Token: 0x0600E781 RID: 59265 RVA: 0x0033C8C0 File Offset: 0x0033AAC0
		public new void Clear()
		{
			foreach (ChartMarkedZone element in base.List)
			{
				this.chartMarkedZonesCollectionParent.Remove(element);
			}
			base.Clear();
		}

		// Token: 0x0600E782 RID: 59266 RVA: 0x0033C918 File Offset: 0x0033AB18
		public override void Insert(int index, ChartMarkedZone item)
		{
			this.chartMarkedZonesCollectionParent.Add(item);
			base.Insert(index, item);
		}

		// Token: 0x0600E783 RID: 59267 RVA: 0x0033C92E File Offset: 0x0033AB2E
		public override bool Remove(ChartMarkedZone item)
		{
			this.chartMarkedZonesCollectionParent.Remove(item);
			return base.Remove(item);
		}

		// Token: 0x0600E784 RID: 59268 RVA: 0x0033C943 File Offset: 0x0033AB43
		public override void RemoveAt(int index)
		{
			this.chartMarkedZonesCollectionParent.Remove(this[index]);
			base.RemoveAt(index);
		}

		// Token: 0x04004281 RID: 17025
		private ChartPlotArea chartMarkedZonesCollectionParent;
	}
}
