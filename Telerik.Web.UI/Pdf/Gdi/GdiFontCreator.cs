using System;
using System.IO;
using Telerik.Pdf.Gdi.Font;

namespace Telerik.Pdf.Gdi
{
	// Token: 0x0200162C RID: 5676
	public class GdiFontCreator : IDisposable
	{
		// Token: 0x0600DCAF RID: 56495 RVA: 0x00303876 File Offset: 0x00301A76
		public GdiFontCreator(GdiDeviceContent dc)
		{
			this.dc = dc;
			this.header = new TrueTypeHeader();
			this.ms = new MemoryStream();
			this.fs = new FontFileStream(this.ms);
		}

		// Token: 0x0600DCB0 RID: 56496 RVA: 0x003038AC File Offset: 0x00301AAC
		public byte[] Build()
		{
			byte[] array = this.ReadTableData("head");
			byte[] array2 = this.ReadTableData("maxp");
			byte[] array3 = this.ReadTableData("hhea");
			byte[] array4 = this.ReadTableData("hmtx");
			byte[] array5 = this.ReadTableData("cvt ");
			byte[] array6 = this.ReadTableData("prep");
			byte[] array7 = this.ReadTableData("fpgm");
			byte[] array8 = this.ReadTableData("glyf");
			byte[] array9 = this.ReadTableData("loca");
			byte[] array10 = this.ReadTableData("OS/2");
			byte[] array11 = this.ReadTableData("post");
			this.fs.WriteFixed(65536);
			this.fs.WriteUShort(11);
			this.fs.WriteUShort(0);
			this.fs.WriteUShort(0);
			this.fs.WriteUShort(0);
			this.offset = (int)this.fs.Position + 176;
			this.WriteDirectoryEntry("head", array);
			this.WriteDirectoryEntry("maxp", array2);
			this.WriteDirectoryEntry("hhea", array3);
			this.WriteDirectoryEntry("hmtx", array4);
			this.WriteDirectoryEntry("cvt ", array5);
			this.WriteDirectoryEntry("prep", array6);
			this.WriteDirectoryEntry("fpgm", array7);
			this.WriteDirectoryEntry("glyf", array8);
			this.WriteDirectoryEntry("loca", array9);
			this.WriteDirectoryEntry("OS/2", array10);
			this.WriteDirectoryEntry("post", array11);
			this.fs.Write(array, 0, array.Length);
			this.fs.Write(array2, 0, array2.Length);
			this.fs.Write(array3, 0, array3.Length);
			this.fs.Write(array4, 0, array4.Length);
			this.fs.Write(array5, 0, array5.Length);
			this.fs.Write(array6, 0, array6.Length);
			this.fs.Write(array7, 0, array7.Length);
			this.fs.Write(array8, 0, array8.Length);
			this.fs.Write(array9, 0, array9.Length);
			this.fs.Write(array10, 0, array10.Length);
			this.fs.Write(array11, 0, array11.Length);
			return this.ms.ToArray();
		}

		// Token: 0x0600DCB1 RID: 56497 RVA: 0x00303AF1 File Offset: 0x00301CF1
		private void WriteTable(byte[] data)
		{
			this.fs.Write(data, 0, data.Length);
			this.fs.Pad();
		}

		// Token: 0x0600DCB2 RID: 56498 RVA: 0x00303B10 File Offset: 0x00301D10
		private void WriteDirectoryEntry(string tableName, byte[] data)
		{
			this.fs.WriteByte((byte)tableName[0]);
			this.fs.WriteByte((byte)tableName[1]);
			this.fs.WriteByte((byte)tableName[2]);
			this.fs.WriteByte((byte)tableName[3]);
			this.fs.WriteULong(0L);
			this.fs.WriteULong((long)this.offset);
			this.fs.WriteULong((long)data.Length);
			this.offset += data.Length;
		}

		// Token: 0x0600DCB3 RID: 56499 RVA: 0x00303BA8 File Offset: 0x00301DA8
		private byte[] ReadTableData(string tableName)
		{
			int dwTable = TableNames.ToUint(tableName);
			int fontData = NativeMethods.GetFontData(this.dc.Handle, dwTable, 0, null, 0);
			if ((ulong)fontData == (ulong)-1 && !this.IsRequiredTable(tableName))
			{
				return new byte[0];
			}
			byte[] array = new byte[fontData];
			long num = (long)NativeMethods.GetFontData(this.dc.Handle, dwTable, 0, array, array.Length);
			if (num == (long)((ulong)-1))
			{
				throw new Exception("Failed to retrieve table " + tableName);
			}
			return array;
		}

		// Token: 0x0600DCB4 RID: 56500 RVA: 0x00303C20 File Offset: 0x00301E20
		private bool IsRequiredTable(string tableName)
		{
			return tableName == "cmap" || tableName == "glyf" || tableName == "head" || tableName == "hhea" || tableName == "hmtx" || tableName == "loca" || tableName == "maxp" || tableName == "name" || tableName == "post" || tableName == "OS/2";
		}

		// Token: 0x0600DCB5 RID: 56501 RVA: 0x00303CAF File Offset: 0x00301EAF
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.ms != null)
				{
					this.ms.Close();
				}
				if (this.fs != null)
				{
					this.fs.Dispose();
				}
			}
		}

		// Token: 0x0600DCB6 RID: 56502 RVA: 0x00303CDA File Offset: 0x00301EDA
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x04003E43 RID: 15939
		private const int NumTables = 11;

		// Token: 0x04003E44 RID: 15940
		private GdiDeviceContent dc;

		// Token: 0x04003E45 RID: 15941
		private TrueTypeHeader header;

		// Token: 0x04003E46 RID: 15942
		private MemoryStream ms;

		// Token: 0x04003E47 RID: 15943
		private FontFileStream fs;

		// Token: 0x04003E48 RID: 15944
		private int offset;
	}
}
