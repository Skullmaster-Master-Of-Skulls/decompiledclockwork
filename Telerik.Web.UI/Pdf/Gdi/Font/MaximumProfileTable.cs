using System;

namespace Telerik.Pdf.Gdi.Font
{
	// Token: 0x02001624 RID: 5668
	internal class MaximumProfileTable : FontTable
	{
		// Token: 0x0600DC74 RID: 56436 RVA: 0x00302E39 File Offset: 0x00301039
		public MaximumProfileTable(DirectoryEntry entry) : base("maxp", entry)
		{
		}

		// Token: 0x17004375 RID: 17269
		// (get) Token: 0x0600DC75 RID: 56437 RVA: 0x00302E47 File Offset: 0x00301047
		// (set) Token: 0x0600DC76 RID: 56438 RVA: 0x00302E4F File Offset: 0x0030104F
		public int GlyphCount
		{
			get
			{
				return this.numGlyphs;
			}
			set
			{
				this.numGlyphs = (int)Convert.ToUInt16(value);
			}
		}

		// Token: 0x0600DC77 RID: 56439 RVA: 0x00302E60 File Offset: 0x00301060
		protected internal override void Read(FontFileReader reader)
		{
			FontFileStream stream = reader.Stream;
			this.versionNo = stream.ReadFixed();
			this.numGlyphs = stream.ReadUShort();
			if (this.versionNo == 65536)
			{
				this.maxPoints = stream.ReadUShort();
				this.maxContours = stream.ReadUShort();
				this.maxCompositePoints = stream.ReadUShort();
				this.maxCompositeContours = stream.ReadUShort();
				this.maxZones = stream.ReadUShort();
				this.maxTwilightPoints = stream.ReadUShort();
				this.maxStorage = stream.ReadUShort();
				this.maxFunctionDefs = stream.ReadUShort();
				this.maxInstructionDefs = stream.ReadUShort();
				this.maxStackElements = stream.ReadUShort();
				this.maxSizeOfInstructions = stream.ReadUShort();
				this.maxComponentElements = stream.ReadUShort();
				this.maxComponentDepth = stream.ReadUShort();
			}
		}

		// Token: 0x0600DC78 RID: 56440 RVA: 0x00302F38 File Offset: 0x00301138
		protected internal override void Write(FontFileWriter writer)
		{
			FontFileStream stream = writer.Stream;
			stream.WriteFixed(this.versionNo);
			stream.WriteUShort(this.numGlyphs);
			if (this.versionNo == 65536)
			{
				stream.WriteUShort(this.maxPoints);
				stream.WriteUShort(this.maxContours);
				stream.WriteUShort(this.maxCompositePoints);
				stream.WriteUShort(this.maxCompositeContours);
				stream.WriteUShort(this.maxZones);
				stream.WriteUShort(this.maxTwilightPoints);
				stream.WriteUShort(this.maxStorage);
				stream.WriteUShort(this.maxFunctionDefs);
				stream.WriteUShort(this.maxInstructionDefs);
				stream.WriteUShort(this.maxStackElements);
				stream.WriteUShort(this.maxSizeOfInstructions);
				stream.WriteUShort(this.maxComponentElements);
				stream.WriteUShort(this.maxComponentDepth);
			}
		}

		// Token: 0x04003DE4 RID: 15844
		internal int versionNo;

		// Token: 0x04003DE5 RID: 15845
		internal int numGlyphs;

		// Token: 0x04003DE6 RID: 15846
		internal int maxPoints;

		// Token: 0x04003DE7 RID: 15847
		internal int maxContours;

		// Token: 0x04003DE8 RID: 15848
		internal int maxCompositePoints;

		// Token: 0x04003DE9 RID: 15849
		internal int maxCompositeContours;

		// Token: 0x04003DEA RID: 15850
		internal int maxZones;

		// Token: 0x04003DEB RID: 15851
		internal int maxTwilightPoints;

		// Token: 0x04003DEC RID: 15852
		internal int maxStorage;

		// Token: 0x04003DED RID: 15853
		internal int maxFunctionDefs;

		// Token: 0x04003DEE RID: 15854
		internal int maxInstructionDefs;

		// Token: 0x04003DEF RID: 15855
		internal int maxStackElements;

		// Token: 0x04003DF0 RID: 15856
		internal int maxSizeOfInstructions;

		// Token: 0x04003DF1 RID: 15857
		internal int maxComponentElements;

		// Token: 0x04003DF2 RID: 15858
		internal int maxComponentDepth;
	}
}
