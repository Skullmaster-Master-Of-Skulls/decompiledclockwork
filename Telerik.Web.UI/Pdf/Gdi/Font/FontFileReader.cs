using System;
using System.Collections;
using System.IO;
using System.Text;

namespace Telerik.Pdf.Gdi.Font
{
	// Token: 0x0200160D RID: 5645
	public class FontFileReader : IDisposable
	{
		// Token: 0x0600DBC8 RID: 56264 RVA: 0x00300BFE File Offset: 0x002FEDFE
		public FontFileReader(MemoryStream stream) : this(stream, string.Empty)
		{
		}

		// Token: 0x0600DBC9 RID: 56265 RVA: 0x00300C0C File Offset: 0x002FEE0C
		public FontFileReader(MemoryStream stream, string fontName)
		{
			this.stream = new FontFileStream(stream);
			this.fontName = fontName;
			this.ReadTableHeaders();
			this.ReadRequiredTables();
		}

		// Token: 0x17004355 RID: 17237
		// (get) Token: 0x0600DBCA RID: 56266 RVA: 0x00300C3E File Offset: 0x002FEE3E
		// (set) Token: 0x0600DBCB RID: 56267 RVA: 0x00300C59 File Offset: 0x002FEE59
		public IndexMappings IndexMappings
		{
			get
			{
				if (this.mappings == null)
				{
					this.mappings = new IndexMappings();
				}
				return this.mappings;
			}
			set
			{
				this.mappings = value;
			}
		}

		// Token: 0x17004356 RID: 17238
		// (get) Token: 0x0600DBCC RID: 56268 RVA: 0x00300C62 File Offset: 0x002FEE62
		internal FontFileStream Stream
		{
			get
			{
				return this.stream;
			}
		}

		// Token: 0x17004357 RID: 17239
		// (get) Token: 0x0600DBCD RID: 56269 RVA: 0x00300C6A File Offset: 0x002FEE6A
		public int TableCount
		{
			get
			{
				return this.header.Count;
			}
		}

		// Token: 0x0600DBCE RID: 56270 RVA: 0x00300C77 File Offset: 0x002FEE77
		public bool ContainsTable(string tableName)
		{
			return this.header.Contains(tableName);
		}

		// Token: 0x0600DBCF RID: 56271 RVA: 0x00300C88 File Offset: 0x002FEE88
		internal FontTable GetTable(string tableName)
		{
			if (!this.ContainsTable(tableName))
			{
				throw new ArgumentException("Cannot locate table '" + tableName + "'", "tableName");
			}
			if (this.tableCache.Contains(tableName))
			{
				return (FontTable)this.tableCache[tableName];
			}
			DirectoryEntry dictionaryEntry = this.GetDictionaryEntry(tableName);
			FontTable fontTable = dictionaryEntry.MakeTable(this);
			if (fontTable != null)
			{
				this.OffsetStream(dictionaryEntry);
				fontTable.Read(this);
			}
			return fontTable;
		}

		// Token: 0x0600DBD0 RID: 56272 RVA: 0x00300CFB File Offset: 0x002FEEFB
		internal HeaderTable GetHeaderTable()
		{
			return (HeaderTable)this.GetTable("head");
		}

		// Token: 0x0600DBD1 RID: 56273 RVA: 0x00300D0D File Offset: 0x002FEF0D
		internal MaximumProfileTable GetMaximumProfileTable()
		{
			return (MaximumProfileTable)this.GetTable("maxp");
		}

		// Token: 0x0600DBD2 RID: 56274 RVA: 0x00300D1F File Offset: 0x002FEF1F
		internal HorizontalHeaderTable GetHorizontalHeaderTable()
		{
			return (HorizontalHeaderTable)this.GetTable("hhea");
		}

		// Token: 0x0600DBD3 RID: 56275 RVA: 0x00300D31 File Offset: 0x002FEF31
		internal HorizontalMetricsTable GetHorizontalMetricsTable()
		{
			return (HorizontalMetricsTable)this.GetTable("hmtx");
		}

		// Token: 0x0600DBD4 RID: 56276 RVA: 0x00300D43 File Offset: 0x002FEF43
		internal ControlValueTable GetControlValueTable()
		{
			return (ControlValueTable)this.GetTable("cvt ");
		}

		// Token: 0x0600DBD5 RID: 56277 RVA: 0x00300D55 File Offset: 0x002FEF55
		internal ControlValueProgramTable GetControlValueProgramTable()
		{
			return (ControlValueProgramTable)this.GetTable("prep");
		}

		// Token: 0x0600DBD6 RID: 56278 RVA: 0x00300D67 File Offset: 0x002FEF67
		internal FontProgramTable GetFontProgramTable()
		{
			return (FontProgramTable)this.GetTable("fpgm");
		}

		// Token: 0x0600DBD7 RID: 56279 RVA: 0x00300D79 File Offset: 0x002FEF79
		internal GlyfDataTable GetGlyfDataTable()
		{
			return (GlyfDataTable)this.GetTable("glyf");
		}

		// Token: 0x0600DBD8 RID: 56280 RVA: 0x00300D8B File Offset: 0x002FEF8B
		internal IndexToLocationTable GetIndexToLocationTable()
		{
			return (IndexToLocationTable)this.GetTable("loca");
		}

		// Token: 0x0600DBD9 RID: 56281 RVA: 0x00300D9D File Offset: 0x002FEF9D
		internal OS2Table GetOS2Table()
		{
			return (OS2Table)this.GetTable("OS/2");
		}

		// Token: 0x0600DBDA RID: 56282 RVA: 0x00300DAF File Offset: 0x002FEFAF
		internal PostTable GetPostTable()
		{
			return (PostTable)this.GetTable("post");
		}

		// Token: 0x0600DBDB RID: 56283 RVA: 0x00300DC1 File Offset: 0x002FEFC1
		internal DirectoryEntry GetDictionaryEntry(string tableName)
		{
			if (!this.ContainsTable(tableName))
			{
				throw new ArgumentException("Cannot locate table named " + tableName, "tableName");
			}
			return this.header[tableName];
		}

		// Token: 0x0600DBDC RID: 56284 RVA: 0x00300DF0 File Offset: 0x002FEFF0
		protected void ReadTableHeaders()
		{
			string @string = Encoding.ASCII.GetString(this.stream.ReadTag());
			if (@string == "ttcf")
			{
				this.stream.Skip(4L);
				int num = this.stream.ReadULong();
				bool flag = false;
				int num2 = 0;
				while (num2 < num && !flag)
				{
					int num3 = this.stream.ReadULong();
					this.stream.SetRestorePoint();
					this.stream.Position = (long)num3;
					this.header = new TrueTypeHeader();
					this.header.Read(this.stream);
					if (!this.header.Contains("name"))
					{
						throw new Exception("Unable to parse TrueType collection - missing 'head' table.");
					}
					NameTable nameTable = (NameTable)this.GetTable("name");
					if (string.IsNullOrEmpty(this.fontName) || string.IsNullOrEmpty(nameTable.FullName))
					{
						flag = true;
					}
					this.stream.Restore();
					num2++;
				}
				if (!flag)
				{
					throw new Exception("Unable to locate font '" + this.fontName + "' in TrueType collection");
				}
			}
			else
			{
				this.stream.Position = 0L;
				this.header = new TrueTypeHeader();
				this.header.Read(this.stream);
			}
		}

		// Token: 0x0600DBDD RID: 56285 RVA: 0x00300F38 File Offset: 0x002FF138
		protected void ReadRequiredTables()
		{
			this.tableCache["head"] = this.GetTable("head");
			this.tableCache["hhea"] = this.GetTable("hhea");
			this.tableCache["maxp"] = this.GetTable("maxp");
			this.tableCache["loca"] = this.GetTable("loca");
		}

		// Token: 0x0600DBDE RID: 56286 RVA: 0x00300FB4 File Offset: 0x002FF1B4
		private void OffsetStream(DirectoryEntry entry)
		{
			this.stream.Position = (long)entry.Offset;
			if (this.stream.Position + (long)entry.Length > this.stream.Length)
			{
				string message = string.Format("Error reading table '{0}'.  Expected {1} bytes, current position {2}, stream length {3}", new object[]
				{
					entry.TableName,
					entry.Length,
					this.stream.Position,
					this.stream.Length
				});
				throw new ArgumentException(message);
			}
		}

		// Token: 0x0600DBDF RID: 56287 RVA: 0x0030104A File Offset: 0x002FF24A
		protected virtual void Dispose(bool disposing)
		{
			if (disposing && this.stream != null)
			{
				this.stream.Dispose();
			}
		}

		// Token: 0x0600DBE0 RID: 56288 RVA: 0x00301062 File Offset: 0x002FF262
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x04003D6C RID: 15724
		private FontFileStream stream;

		// Token: 0x04003D6D RID: 15725
		private string fontName;

		// Token: 0x04003D6E RID: 15726
		private TrueTypeHeader header;

		// Token: 0x04003D6F RID: 15727
		private IDictionary tableCache = new Hashtable();

		// Token: 0x04003D70 RID: 15728
		private IndexMappings mappings;
	}
}
