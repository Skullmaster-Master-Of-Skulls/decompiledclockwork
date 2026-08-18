using System;

namespace Telerik.Pdf.Gdi.Font
{
	// Token: 0x0200161F RID: 5663
	internal class HorizontalHeaderTable : FontTable
	{
		// Token: 0x0600DC56 RID: 56406 RVA: 0x003028A7 File Offset: 0x00300AA7
		public HorizontalHeaderTable(DirectoryEntry entry) : base("hhea", entry)
		{
		}

		// Token: 0x1700436C RID: 17260
		// (get) Token: 0x0600DC57 RID: 56407 RVA: 0x003028B5 File Offset: 0x00300AB5
		// (set) Token: 0x0600DC58 RID: 56408 RVA: 0x003028BD File Offset: 0x00300ABD
		public int HMetricCount
		{
			get
			{
				return this.numberOfHMetrics;
			}
			set
			{
				this.numberOfHMetrics = (int)Convert.ToUInt16(value);
			}
		}

		// Token: 0x0600DC59 RID: 56409 RVA: 0x003028CC File Offset: 0x00300ACC
		protected internal override void Read(FontFileReader reader)
		{
			FontFileStream stream = reader.Stream;
			this.versionNo = stream.ReadFixed();
			this.ascender = stream.ReadFWord();
			this.decender = stream.ReadFWord();
			this.lineGap = stream.ReadFWord();
			this.advanceWidthMax = stream.ReadUFWord();
			this.minLeftSideBearing = stream.ReadFWord();
			this.minRightSideBearing = stream.ReadFWord();
			this.xMaxExtent = stream.ReadFWord();
			this.caretSlopeRise = stream.ReadShort();
			this.caretSlopeRun = stream.ReadShort();
			this.caretOffset = stream.ReadShort();
			stream.ReadShort();
			stream.ReadShort();
			stream.ReadShort();
			stream.ReadShort();
			this.metricDataFormat = stream.ReadShort();
			this.numberOfHMetrics = stream.ReadUShort();
		}

		// Token: 0x0600DC5A RID: 56410 RVA: 0x00302998 File Offset: 0x00300B98
		protected internal override void Write(FontFileWriter writer)
		{
			FontFileStream stream = writer.Stream;
			stream.WriteFixed(this.versionNo);
			stream.WriteFWord((int)this.ascender);
			stream.WriteFWord((int)this.decender);
			stream.WriteFWord((int)this.lineGap);
			stream.WriteUFWord(this.advanceWidthMax);
			stream.WriteFWord((int)this.minLeftSideBearing);
			stream.WriteFWord((int)this.minRightSideBearing);
			stream.WriteFWord((int)this.xMaxExtent);
			stream.WriteShort((int)this.caretSlopeRise);
			stream.WriteShort((int)this.caretSlopeRun);
			stream.WriteShort((int)this.caretOffset);
			stream.WriteShort(0);
			stream.WriteShort(0);
			stream.WriteShort(0);
			stream.WriteShort(0);
			stream.WriteShort((int)this.metricDataFormat);
			stream.WriteUShort(this.numberOfHMetrics);
		}

		// Token: 0x04003DCF RID: 15823
		internal int versionNo;

		// Token: 0x04003DD0 RID: 15824
		internal short ascender;

		// Token: 0x04003DD1 RID: 15825
		internal short decender;

		// Token: 0x04003DD2 RID: 15826
		internal short lineGap;

		// Token: 0x04003DD3 RID: 15827
		internal int advanceWidthMax;

		// Token: 0x04003DD4 RID: 15828
		internal short minLeftSideBearing;

		// Token: 0x04003DD5 RID: 15829
		internal short minRightSideBearing;

		// Token: 0x04003DD6 RID: 15830
		internal short xMaxExtent;

		// Token: 0x04003DD7 RID: 15831
		internal short caretSlopeRise;

		// Token: 0x04003DD8 RID: 15832
		internal short caretSlopeRun;

		// Token: 0x04003DD9 RID: 15833
		internal short caretOffset;

		// Token: 0x04003DDA RID: 15834
		internal short metricDataFormat;

		// Token: 0x04003DDB RID: 15835
		internal int numberOfHMetrics;
	}
}
