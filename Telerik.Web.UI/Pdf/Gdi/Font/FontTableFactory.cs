using System;

namespace Telerik.Pdf.Gdi.Font
{
	// Token: 0x0200161B RID: 5659
	internal sealed class FontTableFactory
	{
		// Token: 0x0600DC40 RID: 56384 RVA: 0x00302245 File Offset: 0x00300445
		private FontTableFactory()
		{
		}

		// Token: 0x0600DC41 RID: 56385 RVA: 0x00302250 File Offset: 0x00300450
		public static FontTable Make(string tableName, FontFileReader reader)
		{
			DirectoryEntry dictionaryEntry = reader.GetDictionaryEntry(tableName);
			switch (tableName)
			{
			case "head":
				return new HeaderTable(dictionaryEntry);
			case "hhea":
				return new HorizontalHeaderTable(dictionaryEntry);
			case "hmtx":
				return new HorizontalMetricsTable(dictionaryEntry);
			case "maxp":
				return new MaximumProfileTable(dictionaryEntry);
			case "loca":
				return new IndexToLocationTable(dictionaryEntry);
			case "glyf":
				return new GlyfDataTable(dictionaryEntry);
			case "cvt ":
				return new ControlValueTable(dictionaryEntry);
			case "prep":
				return new ControlValueProgramTable(dictionaryEntry);
			case "fpgm":
				return new FontProgramTable(dictionaryEntry);
			case "post":
				return new PostTable(dictionaryEntry);
			case "OS/2":
				return new OS2Table(dictionaryEntry);
			case "name":
				return new NameTable(dictionaryEntry);
			case "kern":
				return new KerningTable(dictionaryEntry);
			}
			throw new ArgumentException("Unrecognised table name '" + tableName + "'", "tableName");
		}
	}
}
