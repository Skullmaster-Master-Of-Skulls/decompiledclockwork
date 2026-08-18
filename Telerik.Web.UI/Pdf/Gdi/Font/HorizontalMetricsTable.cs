using System;
using System.Collections;

namespace Telerik.Pdf.Gdi.Font
{
	// Token: 0x02001621 RID: 5665
	internal class HorizontalMetricsTable : FontTable
	{
		// Token: 0x0600DC5F RID: 56415 RVA: 0x00302A9D File Offset: 0x00300C9D
		public HorizontalMetricsTable(DirectoryEntry entry) : base("hmtx", entry)
		{
		}

		// Token: 0x0600DC60 RID: 56416 RVA: 0x00302AAB File Offset: 0x00300CAB
		public HorizontalMetricsTable(DirectoryEntry entry, int numMetrics) : base("hmtx", entry)
		{
			this.metrics = new ArrayList(numMetrics);
		}

		// Token: 0x1700436F RID: 17263
		// (get) Token: 0x0600DC61 RID: 56417 RVA: 0x00302AC5 File Offset: 0x00300CC5
		public int Count
		{
			get
			{
				return this.metrics.Count;
			}
		}

		// Token: 0x17004370 RID: 17264
		public HorizontalMetric this[int index]
		{
			get
			{
				return (HorizontalMetric)this.metrics[index];
			}
			set
			{
				this.metrics.Insert(index, value);
			}
		}

		// Token: 0x0600DC64 RID: 56420 RVA: 0x00302AF4 File Offset: 0x00300CF4
		protected internal override void Read(FontFileReader reader)
		{
			FontFileStream stream = reader.Stream;
			int hmetricCount = reader.GetHorizontalHeaderTable().HMetricCount;
			int glyphCount = reader.GetMaximumProfileTable().GlyphCount;
			int num = (glyphCount > hmetricCount) ? glyphCount : hmetricCount;
			this.metrics = new ArrayList(num);
			for (int i = 0; i < hmetricCount; i++)
			{
				this.metrics.Add(new HorizontalMetric(stream.ReadUShort(), stream.ReadShort()));
			}
			if (hmetricCount < num)
			{
				HorizontalMetric horizontalMetric = (HorizontalMetric)this.metrics[this.metrics.Count - 1];
				for (int j = hmetricCount; j < glyphCount; j++)
				{
					this.metrics.Add(new HorizontalMetric(horizontalMetric.AdvanceWidth, stream.ReadShort()));
				}
			}
		}

		// Token: 0x0600DC65 RID: 56421 RVA: 0x00302BB4 File Offset: 0x00300DB4
		protected internal override void Write(FontFileWriter writer)
		{
			FontFileStream stream = writer.Stream;
			for (int i = 0; i < this.metrics.Count; i++)
			{
				HorizontalMetric horizontalMetric = (HorizontalMetric)this.metrics[i];
				stream.WriteUShort(horizontalMetric.AdvanceWidth);
				stream.WriteShort((int)horizontalMetric.LeftSideBearing);
			}
		}

		// Token: 0x04003DDE RID: 15838
		public IList metrics;
	}
}
