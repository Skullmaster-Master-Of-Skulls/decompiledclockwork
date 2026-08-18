using System;
using System.Collections;
using System.IO;

namespace Telerik.Pdf.Gdi.Font
{
	// Token: 0x0200160F RID: 5647
	internal class FontFileWriter : IDisposable
	{
		// Token: 0x0600DC04 RID: 56324 RVA: 0x003015D4 File Offset: 0x002FF7D4
		public FontFileWriter(Stream stream)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream", "Supplied stream cannot be a null reference");
			}
			if (!stream.CanWrite)
			{
				throw new ArgumentException("The supplied stream is not writable", "stream");
			}
			this.stream = new FontFileStream(stream);
			this.tables = new Hashtable();
		}

		// Token: 0x1700435A RID: 17242
		// (get) Token: 0x0600DC05 RID: 56325 RVA: 0x00301629 File Offset: 0x002FF829
		public FontFileStream Stream
		{
			get
			{
				return this.stream;
			}
		}

		// Token: 0x0600DC06 RID: 56326 RVA: 0x00301634 File Offset: 0x002FF834
		public void Write(FontTable table)
		{
			if (this.tables.Contains(table.Name))
			{
				throw new ArgumentException("Already written table '" + table.Name + "'");
			}
			this.tables.Add(table.Name, table);
		}

		// Token: 0x0600DC07 RID: 56327 RVA: 0x00301681 File Offset: 0x002FF881
		public void Close()
		{
			this.WriteOffsetTable();
			this.SkipTableDirectory();
			this.WriteTables();
			this.WriteTableDirectory();
			this.WriteChecksumAdjustment();
		}

		// Token: 0x0600DC08 RID: 56328 RVA: 0x003016A4 File Offset: 0x002FF8A4
		private void WriteChecksumAdjustment()
		{
			HeaderTable headerTable = (HeaderTable)this.tables["head"];
			this.stream.Position = (long)(headerTable.Entry.Offset + 8);
			this.stream.WriteULong((long)this.CalculateCheckSumAdjustment());
		}

		// Token: 0x0600DC09 RID: 56329 RVA: 0x003016F4 File Offset: 0x002FF8F4
		private void WriteTables()
		{
			foreach (object obj in this.tables.Values)
			{
				FontTable table = (FontTable)obj;
				this.WriteFontTable(table);
			}
		}

		// Token: 0x0600DC0A RID: 56330 RVA: 0x00301754 File Offset: 0x002FF954
		private void WriteFontTable(FontTable table)
		{
			long num = this.stream.SetRestorePoint();
			table.Write(this);
			int num2 = this.stream.Pad();
			long num3 = this.stream.Restore();
			table.Entry.Length = (int)(num3 - num - (long)num2);
			table.Entry.Offset = (int)num;
			table.Entry.CheckSum = this.CalculateCheckSum((long)table.Entry.Length);
		}

		// Token: 0x0600DC0B RID: 56331 RVA: 0x003017C8 File Offset: 0x002FF9C8
		private void WriteOffsetTable()
		{
			this.stream.WriteFixed(65536);
			int count = this.tables.Count;
			this.stream.WriteUShort(count);
			int num = this.MaxPow2(count);
			int num2 = num * 16;
			this.stream.WriteUShort(num2);
			int value = (int)Math.Log((double)num, 2.0);
			this.stream.WriteUShort(value);
			int value2 = count * 16 - num2;
			this.stream.WriteUShort(value2);
		}

		// Token: 0x0600DC0C RID: 56332 RVA: 0x0030184C File Offset: 0x002FFA4C
		private void WriteTableDirectory()
		{
			this.stream.SetRestorePoint();
			this.stream.Position = 0L;
			this.stream.Skip(12L);
			foreach (object obj in this.tables.Values)
			{
				FontTable fontTable = (FontTable)obj;
				this.stream.WriteULong((long)fontTable.Tag);
				this.stream.WriteULong(fontTable.Entry.CheckSum);
				this.stream.WriteULong((long)fontTable.Entry.Offset);
				this.stream.WriteULong((long)fontTable.Entry.Length);
			}
			this.stream.Restore();
		}

		// Token: 0x0600DC0D RID: 56333 RVA: 0x0030192C File Offset: 0x002FFB2C
		private void SkipTableDirectory()
		{
			this.stream.Skip((long)(this.tables.Count * 16));
		}

		// Token: 0x0600DC0E RID: 56334 RVA: 0x00301948 File Offset: 0x002FFB48
		private int MaxPow2(int max)
		{
			int num = 0;
			while (Math.Pow(2.0, (double)num) < (double)max)
			{
				num++;
			}
			if (num != 0)
			{
				return num - 1;
			}
			return 0;
		}

		// Token: 0x0600DC0F RID: 56335 RVA: 0x0030197C File Offset: 0x002FFB7C
		private int CalculateCheckSumAdjustment()
		{
			long length = this.stream.SetRestorePoint();
			this.stream.Position = 0L;
			int result = (int)((ulong)-1313820742 - (ulong)this.CalculateCheckSum(length));
			this.stream.Restore();
			return result;
		}

		// Token: 0x0600DC10 RID: 56336 RVA: 0x003019C0 File Offset: 0x002FFBC0
		private long CalculateCheckSum(long length)
		{
			long num = length + length % 4L;
			long num2 = 0L;
			for (long num3 = 0L; num3 < num; num3 += 4L)
			{
				num2 += (long)this.stream.ReadULong();
				if (num2 > (long)((ulong)-1))
				{
					num2 -= (long)((ulong)-1);
				}
			}
			return num2;
		}

		// Token: 0x0600DC11 RID: 56337 RVA: 0x003019FF File Offset: 0x002FFBFF
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600DC12 RID: 56338 RVA: 0x00301A0E File Offset: 0x002FFC0E
		protected virtual void Dispose(bool disposing)
		{
			if (disposing && this.stream != null)
			{
				this.stream.Dispose();
			}
		}

		// Token: 0x04003D73 RID: 15731
		private const int OffsetTableSize = 12;

		// Token: 0x04003D74 RID: 15732
		private FontFileStream stream;

		// Token: 0x04003D75 RID: 15733
		private IDictionary tables;
	}
}
