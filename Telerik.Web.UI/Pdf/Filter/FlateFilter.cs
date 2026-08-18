using System;
using System.IO;
using System.IO.Compression;

namespace Telerik.Pdf.Filter
{
	// Token: 0x02001605 RID: 5637
	public class FlateFilter : IFilter
	{
		// Token: 0x17004344 RID: 17220
		// (get) Token: 0x0600DBA5 RID: 56229 RVA: 0x0030096B File Offset: 0x002FEB6B
		public PdfObject Name
		{
			get
			{
				return PdfName.Names.FlateDecode;
			}
		}

		// Token: 0x17004345 RID: 17221
		// (get) Token: 0x0600DBA6 RID: 56230 RVA: 0x00300972 File Offset: 0x002FEB72
		public PdfObject DecodeParms
		{
			get
			{
				return PdfNull.Null;
			}
		}

		// Token: 0x17004346 RID: 17222
		// (get) Token: 0x0600DBA7 RID: 56231 RVA: 0x00300979 File Offset: 0x002FEB79
		public bool HasDecodeParams
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600DBA8 RID: 56232 RVA: 0x0030097C File Offset: 0x002FEB7C
		public byte[] Encode(byte[] data)
		{
			MemoryStream memoryStream = new MemoryStream();
			memoryStream.WriteByte(88);
			memoryStream.WriteByte(9);
			DeflateStream deflateStream = new DeflateStream(memoryStream, CompressionMode.Compress, true);
			deflateStream.Write(data, 0, data.Length);
			deflateStream.Close();
			uint num = FlateFilter.Adler32.CalculateChecksum(data);
			memoryStream.WriteByte((byte)(num >> 24));
			memoryStream.WriteByte((byte)((num & 16711680U) >> 16));
			memoryStream.WriteByte((byte)((num & 65280U) >> 8));
			memoryStream.WriteByte((byte)num);
			return memoryStream.ToArray();
		}

		// Token: 0x02001606 RID: 5638
		public static class Adler32
		{
			// Token: 0x0600DBAA RID: 56234 RVA: 0x00300A02 File Offset: 0x002FEC02
			[CLSCompliant(false)]
			public static uint CalculateChecksum(byte[] buffer)
			{
				return FlateFilter.Adler32.CalculateChecksum(buffer, 0, buffer.Length);
			}

			// Token: 0x0600DBAB RID: 56235 RVA: 0x00300A10 File Offset: 0x002FEC10
			[CLSCompliant(false)]
			public static uint CalculateChecksum(byte[] buffer, int offset, int length)
			{
				if (buffer == null)
				{
					return 1U;
				}
				uint num = 1U;
				uint num2 = 0U;
				if (length > -2147483648)
				{
					while (--length >= 0)
					{
						num = (num + (uint)buffer[offset++]) % 65521U;
						num2 = (num2 + num) % 65521U;
					}
				}
				return num2 * 65536U + num;
			}
		}
	}
}
