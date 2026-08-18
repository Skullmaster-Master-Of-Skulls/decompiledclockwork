using System;
using System.IO;
using System.util.zlib;

namespace iTextSharp.text.pdf.codec
{
	// Token: 0x0200005D RID: 93
	public class PngWriter
	{
		// Token: 0x060002B2 RID: 690 RVA: 0x0000CF2A File Offset: 0x0000BF2A
		public PngWriter(Stream outp)
		{
			this.outp = outp;
			outp.Write(PngWriter.PNG_SIGNTURE, 0, PngWriter.PNG_SIGNTURE.Length);
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x0000CF4C File Offset: 0x0000BF4C
		public void WriteHeader(int width, int height, int bitDepth, int colorType)
		{
			MemoryStream memoryStream = new MemoryStream();
			PngWriter.OutputInt(width, memoryStream);
			PngWriter.OutputInt(height, memoryStream);
			memoryStream.WriteByte((byte)bitDepth);
			memoryStream.WriteByte((byte)colorType);
			memoryStream.WriteByte(0);
			memoryStream.WriteByte(0);
			memoryStream.WriteByte(0);
			this.WriteChunk(PngWriter.IHDR, memoryStream.ToArray());
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x0000CFA4 File Offset: 0x0000BFA4
		public void WriteEnd()
		{
			this.WriteChunk(PngWriter.IEND, new byte[0]);
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x0000CFB8 File Offset: 0x0000BFB8
		public void WriteData(byte[] data, int stride)
		{
			MemoryStream memoryStream = new MemoryStream();
			ZDeflaterOutputStream zdeflaterOutputStream = new ZDeflaterOutputStream(memoryStream, 5);
			for (int i = 0; i < data.Length; i += stride)
			{
				zdeflaterOutputStream.WriteByte(0);
				zdeflaterOutputStream.Write(data, i, stride);
			}
			zdeflaterOutputStream.Finish();
			this.WriteChunk(PngWriter.IDAT, memoryStream.ToArray());
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x0000D008 File Offset: 0x0000C008
		public void WritePalette(byte[] data)
		{
			this.WriteChunk(PngWriter.PLTE, data);
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x0000D018 File Offset: 0x0000C018
		public void WriteIccProfile(byte[] data)
		{
			MemoryStream memoryStream = new MemoryStream();
			memoryStream.WriteByte(73);
			memoryStream.WriteByte(67);
			memoryStream.WriteByte(67);
			memoryStream.WriteByte(0);
			memoryStream.WriteByte(0);
			ZDeflaterOutputStream zdeflaterOutputStream = new ZDeflaterOutputStream(memoryStream, 5);
			zdeflaterOutputStream.Write(data, 0, data.Length);
			zdeflaterOutputStream.Finish();
			this.WriteChunk(PngWriter.iCCP, memoryStream.ToArray());
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x0000D07C File Offset: 0x0000C07C
		private static void make_crc_table()
		{
			if (PngWriter.crc_table != null)
			{
				return;
			}
			uint[] array = new uint[256];
			for (uint num = 0U; num < 256U; num += 1U)
			{
				uint num2 = num;
				for (int i = 0; i < 8; i++)
				{
					if ((num2 & 1U) != 0U)
					{
						num2 = (3988292384U ^ num2 >> 1);
					}
					else
					{
						num2 >>= 1;
					}
				}
				array[(int)((UIntPtr)num)] = num2;
			}
			PngWriter.crc_table = array;
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x0000D0DC File Offset: 0x0000C0DC
		private static uint update_crc(uint crc, byte[] buf, int offset, int len)
		{
			uint num = crc;
			if (PngWriter.crc_table == null)
			{
				PngWriter.make_crc_table();
			}
			for (int i = 0; i < len; i++)
			{
				num = (PngWriter.crc_table[(int)((UIntPtr)((num ^ (uint)buf[i + offset]) & 255U))] ^ num >> 8);
			}
			return num;
		}

		// Token: 0x060002BA RID: 698 RVA: 0x0000D11D File Offset: 0x0000C11D
		private static uint crc(byte[] buf, int offset, int len)
		{
			return PngWriter.update_crc(uint.MaxValue, buf, offset, len) ^ uint.MaxValue;
		}

		// Token: 0x060002BB RID: 699 RVA: 0x0000D12A File Offset: 0x0000C12A
		private static uint crc(byte[] buf)
		{
			return PngWriter.update_crc(uint.MaxValue, buf, 0, buf.Length) ^ uint.MaxValue;
		}

		// Token: 0x060002BC RID: 700 RVA: 0x0000D139 File Offset: 0x0000C139
		public void OutputInt(int n)
		{
			PngWriter.OutputInt(n, this.outp);
		}

		// Token: 0x060002BD RID: 701 RVA: 0x0000D147 File Offset: 0x0000C147
		public static void OutputInt(int n, Stream s)
		{
			s.WriteByte((byte)(n >> 24));
			s.WriteByte((byte)(n >> 16));
			s.WriteByte((byte)(n >> 8));
			s.WriteByte((byte)n);
		}

		// Token: 0x060002BE RID: 702 RVA: 0x0000D174 File Offset: 0x0000C174
		public void WriteChunk(byte[] chunkType, byte[] data)
		{
			this.OutputInt(data.Length);
			this.outp.Write(chunkType, 0, 4);
			this.outp.Write(data, 0, data.Length);
			uint num = PngWriter.update_crc(uint.MaxValue, chunkType, 0, chunkType.Length);
			num = (PngWriter.update_crc(num, data, 0, data.Length) ^ uint.MaxValue);
			this.OutputInt((int)num);
		}

		// Token: 0x04000146 RID: 326
		private static readonly byte[] PNG_SIGNTURE = new byte[]
		{
			137,
			80,
			78,
			71,
			13,
			10,
			26,
			10
		};

		// Token: 0x04000147 RID: 327
		private static readonly byte[] IHDR = DocWriter.GetISOBytes("IHDR");

		// Token: 0x04000148 RID: 328
		private static readonly byte[] PLTE = DocWriter.GetISOBytes("PLTE");

		// Token: 0x04000149 RID: 329
		private static readonly byte[] IDAT = DocWriter.GetISOBytes("IDAT");

		// Token: 0x0400014A RID: 330
		private static readonly byte[] IEND = DocWriter.GetISOBytes("IEND");

		// Token: 0x0400014B RID: 331
		private static readonly byte[] iCCP = DocWriter.GetISOBytes("iCCP");

		// Token: 0x0400014C RID: 332
		private static uint[] crc_table;

		// Token: 0x0400014D RID: 333
		private Stream outp;
	}
}
