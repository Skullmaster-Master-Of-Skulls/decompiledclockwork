using System;
using System.Collections;

namespace Telerik.Pdf.Gdi.Font
{
	// Token: 0x0200161C RID: 5660
	internal class GlyfDataTable : FontTable
	{
		// Token: 0x0600DC42 RID: 56386 RVA: 0x003023EC File Offset: 0x003005EC
		public GlyfDataTable(DirectoryEntry entry) : base("glyf", entry)
		{
			this.glyphDescriptions = new SortedList();
		}

		// Token: 0x17004365 RID: 17253
		public Glyph this[int glyphIndex]
		{
			get
			{
				return (Glyph)this.glyphDescriptions[glyphIndex];
			}
			set
			{
				this.glyphDescriptions[glyphIndex] = value;
			}
		}

		// Token: 0x17004366 RID: 17254
		// (get) Token: 0x0600DC45 RID: 56389 RVA: 0x00302431 File Offset: 0x00300631
		public int Count
		{
			get
			{
				return this.glyphDescriptions.Count;
			}
		}

		// Token: 0x0600DC46 RID: 56390 RVA: 0x00302440 File Offset: 0x00300640
		protected internal override void Read(FontFileReader reader)
		{
			GlyphReader glyphReader = new GlyphReader(reader);
			foreach (object obj in reader.IndexMappings.GlyphIndices)
			{
				int glyphIndex = (int)obj;
				Glyph glyph = glyphReader.ReadGlyph(glyphIndex);
				this.glyphDescriptions[glyph.Index] = glyph;
				if (glyph.IsComposite)
				{
					foreach (object obj2 in glyph.Children)
					{
						int num = (int)obj2;
						if (this[num] == null)
						{
							int glyphIndex2 = reader.IndexMappings.GetGlyphIndex(num);
							this[num] = glyphReader.ReadGlyph(glyphIndex2);
						}
					}
				}
			}
		}

		// Token: 0x0600DC47 RID: 56391 RVA: 0x00302540 File Offset: 0x00300740
		protected internal override void Write(FontFileWriter writer)
		{
			FontFileStream stream = writer.Stream;
			foreach (object obj in this.glyphDescriptions.Keys)
			{
				int glyphIndex = (int)obj;
				this[glyphIndex].Write(stream);
			}
		}

		// Token: 0x04003DB9 RID: 15801
		private IDictionary glyphDescriptions;
	}
}
