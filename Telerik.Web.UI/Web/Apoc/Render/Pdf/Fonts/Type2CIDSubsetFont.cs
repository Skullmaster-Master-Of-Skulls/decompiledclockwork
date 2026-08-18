using System;
using System.IO;
using Telerik.Pdf;
using Telerik.Pdf.Gdi.Font;
using Telerik.Web.UI.Common;

namespace Telerik.Web.Apoc.Render.Pdf.Fonts
{
	// Token: 0x02001699 RID: 5785
	internal class Type2CIDSubsetFont : Type2CIDFont
	{
		// Token: 0x0600DF63 RID: 57187 RVA: 0x003192E8 File Offset: 0x003174E8
		public Type2CIDSubsetFont(FontProperties properties) : base(properties)
		{
			this.InsertNotdefGlyphs();
			this.namePrefix = new TelerikRandom().GetInt(1048576, 16777215).ToString("X").Substring(0, 6);
		}

		// Token: 0x0600DF64 RID: 57188 RVA: 0x00319330 File Offset: 0x00317530
		private void InsertNotdefGlyphs()
		{
			this.indexMappings = new IndexMappings();
			this.indexMappings.Add(new int[]
			{
				0,
				1,
				2
			});
		}

		// Token: 0x17004484 RID: 17540
		// (get) Token: 0x0600DF65 RID: 57189 RVA: 0x00319364 File Offset: 0x00317564
		public override PdfWArray WArray
		{
			get
			{
				int[] array = new int[this.indexMappings.Count];
				foreach (object obj in this.indexMappings.SubsetIndices)
				{
					int num = (int)obj;
					int glyphIndex = this.indexMappings.GetGlyphIndex(num);
					array[num] = this.Widths[glyphIndex];
				}
				PdfWArray pdfWArray = new PdfWArray(0);
				pdfWArray.AddEntry(array);
				return pdfWArray;
			}
		}

		// Token: 0x17004485 RID: 17541
		// (get) Token: 0x0600DF66 RID: 57190 RVA: 0x003193FC File Offset: 0x003175FC
		public override string FontName
		{
			get
			{
				return string.Format("{0}+{1}", this.namePrefix, this.baseFontName);
			}
		}

		// Token: 0x0600DF67 RID: 57191 RVA: 0x00319414 File Offset: 0x00317614
		public override int MapCharacter(char c)
		{
			return this.indexMappings.GetSubsetIndex(base.MapCharacter(c));
		}

		// Token: 0x0600DF68 RID: 57192 RVA: 0x00319428 File Offset: 0x00317628
		protected override void AddGlyphToCharMapping(int glyphIndex, char c)
		{
			int num;
			if (this.indexMappings.HasMapping(glyphIndex))
			{
				num = this.indexMappings.GetSubsetIndex(glyphIndex);
			}
			else
			{
				num = this.indexMappings.Map(glyphIndex);
			}
			if (!this.usedGlyphs.ContainsKey(num))
			{
				this.usedGlyphs.Add(num, c);
			}
		}

		// Token: 0x0600DF69 RID: 57193 RVA: 0x0031948C File Offset: 0x0031768C
		public override int GetWidth(int charIndex)
		{
			int glyphIndex = this.indexMappings.GetGlyphIndex(charIndex);
			return base.GetWidth(glyphIndex);
		}

		// Token: 0x17004486 RID: 17542
		// (get) Token: 0x0600DF6A RID: 57194 RVA: 0x003194B0 File Offset: 0x003176B0
		public override byte[] FontData
		{
			get
			{
				MemoryStream stream = new MemoryStream(this.metrics.GetFontData());
				FontSubset fontSubset = new FontSubset(new FontFileReader(stream, this.metrics.FaceName)
				{
					IndexMappings = this.indexMappings
				});
				MemoryStream memoryStream = new MemoryStream();
				fontSubset.Generate(memoryStream);
				return memoryStream.GetBuffer();
			}
		}

		// Token: 0x04004069 RID: 16489
		protected IndexMappings indexMappings;

		// Token: 0x0400406A RID: 16490
		protected string namePrefix;
	}
}
