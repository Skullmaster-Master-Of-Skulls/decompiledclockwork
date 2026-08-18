using System;
using System.IO;

namespace Telerik.Pdf.Gdi.Font
{
	// Token: 0x02001610 RID: 5648
	public class FontSubset
	{
		// Token: 0x0600DC13 RID: 56339 RVA: 0x00301A26 File Offset: 0x002FFC26
		public FontSubset(FontFileReader reader)
		{
			this.reader = reader;
		}

		// Token: 0x0600DC14 RID: 56340 RVA: 0x00301A38 File Offset: 0x002FFC38
		public void Generate(MemoryStream output)
		{
			HeaderTable headerTable = this.reader.GetHeaderTable();
			MaximumProfileTable maximumProfileTable = this.reader.GetMaximumProfileTable();
			HorizontalHeaderTable horizontalHeaderTable = this.reader.GetHorizontalHeaderTable();
			ControlValueTable controlValueTable = this.reader.GetControlValueTable();
			FontProgramTable fontProgramTable = this.reader.GetFontProgramTable();
			GlyfDataTable glyfDataTable = this.reader.GetGlyfDataTable();
			ControlValueProgramTable controlValueProgramTable = this.reader.GetControlValueProgramTable();
			IndexToLocationTable table = this.CreateLocaTable(glyfDataTable);
			HorizontalMetricsTable table2 = this.CreateHmtxTable(glyfDataTable);
			maximumProfileTable.GlyphCount = glyfDataTable.Count;
			horizontalHeaderTable.HMetricCount = glyfDataTable.Count;
			FontFileWriter fontFileWriter = new FontFileWriter(output);
			fontFileWriter.Write(headerTable);
			fontFileWriter.Write(maximumProfileTable);
			fontFileWriter.Write(horizontalHeaderTable);
			fontFileWriter.Write(table2);
			fontFileWriter.Write(controlValueTable);
			fontFileWriter.Write(controlValueProgramTable);
			fontFileWriter.Write(fontProgramTable);
			fontFileWriter.Write(table);
			fontFileWriter.Write(glyfDataTable);
			fontFileWriter.Close();
		}

		// Token: 0x0600DC15 RID: 56341 RVA: 0x00301B28 File Offset: 0x002FFD28
		private HorizontalMetricsTable CreateHmtxTable(GlyfDataTable glyfTable)
		{
			HorizontalMetricsTable horizontalMetricsTable = this.reader.GetHorizontalMetricsTable();
			DirectoryEntry entry = new DirectoryEntry("hmtx");
			HorizontalMetricsTable horizontalMetricsTable2 = new HorizontalMetricsTable(entry, glyfTable.Count);
			IndexMappings indexMappings = this.reader.IndexMappings;
			foreach (object obj in indexMappings.SubsetIndices)
			{
				int num = (int)obj;
				int glyphIndex = indexMappings.GetGlyphIndex(num);
				horizontalMetricsTable2[num] = horizontalMetricsTable[glyphIndex].Clone();
			}
			return horizontalMetricsTable2;
		}

		// Token: 0x0600DC16 RID: 56342 RVA: 0x00301BD4 File Offset: 0x002FFDD4
		private IndexToLocationTable CreateLocaTable(GlyfDataTable glyfTable)
		{
			DirectoryEntry entry = new DirectoryEntry("loca");
			IndexToLocationTable indexToLocationTable = new IndexToLocationTable(entry, glyfTable.Count);
			int num = 0;
			for (int i = 0; i < glyfTable.Count; i++)
			{
				indexToLocationTable.AddOffset(num);
				num += glyfTable[i].Length;
			}
			indexToLocationTable.AddOffset(num);
			return indexToLocationTable;
		}

		// Token: 0x04003D76 RID: 15734
		private FontFileReader reader;
	}
}
