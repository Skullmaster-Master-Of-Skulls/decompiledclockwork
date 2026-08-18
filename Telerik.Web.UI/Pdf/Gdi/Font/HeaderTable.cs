using System;

namespace Telerik.Pdf.Gdi.Font
{
	// Token: 0x0200161E RID: 5662
	internal class HeaderTable : FontTable
	{
		// Token: 0x0600DC50 RID: 56400 RVA: 0x00302644 File Offset: 0x00300844
		public HeaderTable(DirectoryEntry entry) : base("head", entry)
		{
		}

		// Token: 0x1700436B RID: 17259
		// (get) Token: 0x0600DC51 RID: 56401 RVA: 0x00302652 File Offset: 0x00300852
		public bool IsShortFormat
		{
			get
			{
				return this.indexToLocFormat == 0;
			}
		}

		// Token: 0x0600DC52 RID: 56402 RVA: 0x00302660 File Offset: 0x00300860
		protected internal override void Read(FontFileReader reader)
		{
			FontFileStream stream = reader.Stream;
			this.versionNo = stream.ReadFixed();
			this.fontRevision = stream.ReadFixed();
			this.checkSumAdjustment = stream.ReadULong();
			this.magicNumber = stream.ReadULong();
			this.flags = stream.ReadUShort();
			this.unitsPermEm = stream.ReadUShort();
			this.createDate = this.GetDate(stream.ReadLongDateTime());
			this.updateDate = this.GetDate(stream.ReadLongDateTime());
			this.xMin = stream.ReadShort();
			this.yMin = stream.ReadShort();
			this.xMax = stream.ReadShort();
			this.yMax = stream.ReadShort();
			this.macStyle = stream.ReadUShort();
			this.lowestRecPPEM = stream.ReadUShort();
			this.fontDirectionHint = stream.ReadShort();
			this.indexToLocFormat = stream.ReadShort();
			this.glyphDataFormat = stream.ReadShort();
		}

		// Token: 0x0600DC53 RID: 56403 RVA: 0x0030274C File Offset: 0x0030094C
		private DateTime GetDate(long seconds)
		{
			DateTime result;
			try
			{
				result = new DateTime(HeaderTable.BaseDate.Ticks).AddSeconds((double)seconds);
			}
			catch
			{
				result = HeaderTable.BaseDate;
			}
			return result;
		}

		// Token: 0x0600DC54 RID: 56404 RVA: 0x00302794 File Offset: 0x00300994
		protected internal override void Write(FontFileWriter writer)
		{
			FontFileStream stream = writer.Stream;
			stream.WriteFixed(this.versionNo);
			stream.WriteFixed(this.fontRevision);
			stream.WriteULong(0L);
			stream.WriteULong(1594834165L);
			stream.WriteUShort(this.flags);
			stream.WriteUShort(this.unitsPermEm);
			stream.WriteDateTime((long)(this.createDate - HeaderTable.BaseDate).TotalSeconds);
			stream.WriteDateTime((long)(this.updateDate - HeaderTable.BaseDate).TotalSeconds);
			stream.WriteShort((int)this.xMin);
			stream.WriteShort((int)this.yMin);
			stream.WriteShort((int)this.xMax);
			stream.WriteShort((int)this.yMax);
			stream.WriteUShort(this.macStyle);
			stream.WriteUShort(this.lowestRecPPEM);
			stream.WriteShort((int)this.fontDirectionHint);
			stream.WriteShort(1);
			stream.WriteShort((int)this.glyphDataFormat);
		}

		// Token: 0x04003DBD RID: 15805
		internal int versionNo;

		// Token: 0x04003DBE RID: 15806
		internal int fontRevision;

		// Token: 0x04003DBF RID: 15807
		internal int checkSumAdjustment;

		// Token: 0x04003DC0 RID: 15808
		internal int magicNumber;

		// Token: 0x04003DC1 RID: 15809
		internal int flags;

		// Token: 0x04003DC2 RID: 15810
		internal int unitsPermEm;

		// Token: 0x04003DC3 RID: 15811
		internal DateTime createDate;

		// Token: 0x04003DC4 RID: 15812
		internal DateTime updateDate;

		// Token: 0x04003DC5 RID: 15813
		internal short xMin;

		// Token: 0x04003DC6 RID: 15814
		internal short yMin;

		// Token: 0x04003DC7 RID: 15815
		internal short xMax;

		// Token: 0x04003DC8 RID: 15816
		internal short yMax;

		// Token: 0x04003DC9 RID: 15817
		internal int macStyle;

		// Token: 0x04003DCA RID: 15818
		internal int lowestRecPPEM;

		// Token: 0x04003DCB RID: 15819
		internal short fontDirectionHint;

		// Token: 0x04003DCC RID: 15820
		internal short indexToLocFormat;

		// Token: 0x04003DCD RID: 15821
		internal short glyphDataFormat;

		// Token: 0x04003DCE RID: 15822
		private static readonly DateTime BaseDate = new DateTime(1904, 1, 1, 0, 0, 0);
	}
}
