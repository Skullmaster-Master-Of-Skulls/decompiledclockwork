using System;
using System.Diagnostics.CodeAnalysis;

namespace Telerik.Pdf.Gdi.Font
{
	// Token: 0x02001611 RID: 5649
	internal class GlyphReader
	{
		// Token: 0x0600DC17 RID: 56343 RVA: 0x00301C2A File Offset: 0x002FFE2A
		public GlyphReader(FontFileReader reader)
		{
			this.reader = reader;
			this.glyfEntry = reader.GetDictionaryEntry("glyf");
			this.loca = reader.GetIndexToLocationTable();
		}

		// Token: 0x0600DC18 RID: 56344 RVA: 0x00301C58 File Offset: 0x002FFE58
		public Glyph ReadGlyph(int glyphIndex)
		{
			FontFileStream stream = this.reader.Stream;
			int num = this.glyfEntry.Offset + this.loca[glyphIndex];
			long glyphLength = this.GetGlyphLength(glyphIndex);
			Glyph glyph = new Glyph(this.reader.IndexMappings.Map(glyphIndex));
			if (glyphLength != 0L)
			{
				byte[] array = new byte[glyphLength];
				stream.Position = (long)num;
				stream.Read(array, 0, array.Length);
				glyph.SetGlyphData(array);
				FontFileStream fontFileStream = new FontFileStream(array);
				bool flag = fontFileStream.ReadShort() < 0;
				fontFileStream.Skip(8L);
				if (flag)
				{
					this.ReadCompositeGlyph(fontFileStream, glyph);
				}
			}
			return glyph;
		}

		// Token: 0x0600DC19 RID: 56345 RVA: 0x00301D04 File Offset: 0x002FFF04
		private void ReadCompositeGlyph(FontFileStream stream, Glyph glyph)
		{
			bool flag = true;
			while (flag)
			{
				int num = stream.ReadUShort();
				long position = stream.Position;
				int num2 = this.reader.IndexMappings.Map(stream.ReadUShort());
				glyph.AddChild(num2);
				stream.Position -= 2L;
				stream.WriteUShort(num2);
				int num3;
				if ((num & 1) > 0)
				{
					num3 = 4;
				}
				else
				{
					num3 = 2;
				}
				if ((num & 8) > 0)
				{
					num3 = 2;
				}
				else if ((num & 64) > 0)
				{
					num3 = 4;
				}
				else if ((num & 128) > 0)
				{
					num3 = 8;
				}
				if ((num & 256) > 0)
				{
					num3 = stream.ReadUShort();
				}
				flag = ((num & 32) > 0);
				stream.Skip((long)num3);
			}
		}

		// Token: 0x0600DC1A RID: 56346 RVA: 0x00301DB8 File Offset: 0x002FFFB8
		[SuppressMessage("Microsoft.Usage", "CA2233:OperationsShouldNotOverflow")]
		private long GetGlyphLength(int index)
		{
			if (index == this.loca.Count - 1)
			{
				return (long)(this.glyfEntry.Length - this.loca[index]);
			}
			return (long)(this.loca[index + 1] - this.loca[index]);
		}

		// Token: 0x04003D77 RID: 15735
		private IndexToLocationTable loca;

		// Token: 0x04003D78 RID: 15736
		private FontFileReader reader;

		// Token: 0x04003D79 RID: 15737
		private DirectoryEntry glyfEntry;
	}
}
