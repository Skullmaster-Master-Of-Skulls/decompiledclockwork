using System;

namespace System.IO.Compression
{
	// Token: 0x0200042D RID: 1069
	internal class GZipDecoder : IFileFormatReader
	{
		// Token: 0x0600281C RID: 10268 RVA: 0x000B8360 File Offset: 0x000B6560
		public GZipDecoder()
		{
			this.Reset();
		}

		// Token: 0x0600281D RID: 10269 RVA: 0x000B836E File Offset: 0x000B656E
		public void Reset()
		{
			this.gzipHeaderSubstate = GZipDecoder.GzipHeaderState.ReadingID1;
			this.gzipFooterSubstate = GZipDecoder.GzipHeaderState.ReadingCRC;
			this.expectedCrc32 = 0U;
			this.expectedOutputStreamSizeModulo = 0U;
		}

		// Token: 0x0600281E RID: 10270 RVA: 0x000B8390 File Offset: 0x000B6590
		public bool ReadHeader(InputBuffer input)
		{
			int bits;
			switch (this.gzipHeaderSubstate)
			{
			case GZipDecoder.GzipHeaderState.ReadingID1:
				bits = input.GetBits(8);
				if (bits < 0)
				{
					return false;
				}
				if (bits != 31)
				{
					throw new InvalidDataException(SR.GetString("CorruptedGZipHeader"));
				}
				this.gzipHeaderSubstate = GZipDecoder.GzipHeaderState.ReadingID2;
				break;
			case GZipDecoder.GzipHeaderState.ReadingID2:
				break;
			case GZipDecoder.GzipHeaderState.ReadingCM:
				goto IL_A5;
			case GZipDecoder.GzipHeaderState.ReadingFLG:
				goto IL_CE;
			case GZipDecoder.GzipHeaderState.ReadingMMTime:
				goto IL_F1;
			case GZipDecoder.GzipHeaderState.ReadingXFL:
				goto IL_128;
			case GZipDecoder.GzipHeaderState.ReadingOS:
				goto IL_13D;
			case GZipDecoder.GzipHeaderState.ReadingXLen1:
				goto IL_152;
			case GZipDecoder.GzipHeaderState.ReadingXLen2:
				goto IL_17B;
			case GZipDecoder.GzipHeaderState.ReadingXLenData:
				goto IL_1A8;
			case GZipDecoder.GzipHeaderState.ReadingFileName:
				goto IL_1E5;
			case GZipDecoder.GzipHeaderState.ReadingComment:
				goto IL_212;
			case GZipDecoder.GzipHeaderState.ReadingCRC16Part1:
				goto IL_240;
			case GZipDecoder.GzipHeaderState.ReadingCRC16Part2:
				goto IL_26A;
			case GZipDecoder.GzipHeaderState.Done:
				return true;
			default:
				throw new InvalidDataException(SR.GetString("UnknownState"));
			}
			bits = input.GetBits(8);
			if (bits < 0)
			{
				return false;
			}
			if (bits != 139)
			{
				throw new InvalidDataException(SR.GetString("CorruptedGZipHeader"));
			}
			this.gzipHeaderSubstate = GZipDecoder.GzipHeaderState.ReadingCM;
			IL_A5:
			bits = input.GetBits(8);
			if (bits < 0)
			{
				return false;
			}
			if (bits != 8)
			{
				throw new InvalidDataException(SR.GetString("UnknownCompressionMode"));
			}
			this.gzipHeaderSubstate = GZipDecoder.GzipHeaderState.ReadingFLG;
			IL_CE:
			bits = input.GetBits(8);
			if (bits < 0)
			{
				return false;
			}
			this.gzip_header_flag = bits;
			this.gzipHeaderSubstate = GZipDecoder.GzipHeaderState.ReadingMMTime;
			this.loopCounter = 0;
			IL_F1:
			while (this.loopCounter < 4)
			{
				bits = input.GetBits(8);
				if (bits < 0)
				{
					return false;
				}
				this.loopCounter++;
			}
			this.gzipHeaderSubstate = GZipDecoder.GzipHeaderState.ReadingXFL;
			this.loopCounter = 0;
			IL_128:
			bits = input.GetBits(8);
			if (bits < 0)
			{
				return false;
			}
			this.gzipHeaderSubstate = GZipDecoder.GzipHeaderState.ReadingOS;
			IL_13D:
			bits = input.GetBits(8);
			if (bits < 0)
			{
				return false;
			}
			this.gzipHeaderSubstate = GZipDecoder.GzipHeaderState.ReadingXLen1;
			IL_152:
			if ((this.gzip_header_flag & 4) == 0)
			{
				goto IL_1E5;
			}
			bits = input.GetBits(8);
			if (bits < 0)
			{
				return false;
			}
			this.gzip_header_xlen = bits;
			this.gzipHeaderSubstate = GZipDecoder.GzipHeaderState.ReadingXLen2;
			IL_17B:
			bits = input.GetBits(8);
			if (bits < 0)
			{
				return false;
			}
			this.gzip_header_xlen |= bits << 8;
			this.gzipHeaderSubstate = GZipDecoder.GzipHeaderState.ReadingXLenData;
			this.loopCounter = 0;
			IL_1A8:
			while (this.loopCounter < this.gzip_header_xlen)
			{
				bits = input.GetBits(8);
				if (bits < 0)
				{
					return false;
				}
				this.loopCounter++;
			}
			this.gzipHeaderSubstate = GZipDecoder.GzipHeaderState.ReadingFileName;
			this.loopCounter = 0;
			IL_1E5:
			if ((this.gzip_header_flag & 8) == 0)
			{
				this.gzipHeaderSubstate = GZipDecoder.GzipHeaderState.ReadingComment;
			}
			else
			{
				for (;;)
				{
					bits = input.GetBits(8);
					if (bits < 0)
					{
						break;
					}
					if (bits == 0)
					{
						goto Block_20;
					}
				}
				return false;
				Block_20:
				this.gzipHeaderSubstate = GZipDecoder.GzipHeaderState.ReadingComment;
			}
			IL_212:
			if ((this.gzip_header_flag & 16) == 0)
			{
				this.gzipHeaderSubstate = GZipDecoder.GzipHeaderState.ReadingCRC16Part1;
			}
			else
			{
				for (;;)
				{
					bits = input.GetBits(8);
					if (bits < 0)
					{
						break;
					}
					if (bits == 0)
					{
						goto Block_23;
					}
				}
				return false;
				Block_23:
				this.gzipHeaderSubstate = GZipDecoder.GzipHeaderState.ReadingCRC16Part1;
			}
			IL_240:
			if ((this.gzip_header_flag & 2) == 0)
			{
				this.gzipHeaderSubstate = GZipDecoder.GzipHeaderState.Done;
				return true;
			}
			bits = input.GetBits(8);
			if (bits < 0)
			{
				return false;
			}
			this.gzipHeaderSubstate = GZipDecoder.GzipHeaderState.ReadingCRC16Part2;
			IL_26A:
			bits = input.GetBits(8);
			if (bits < 0)
			{
				return false;
			}
			this.gzipHeaderSubstate = GZipDecoder.GzipHeaderState.Done;
			return true;
		}

		// Token: 0x0600281F RID: 10271 RVA: 0x000B8630 File Offset: 0x000B6830
		public bool ReadFooter(InputBuffer input)
		{
			input.SkipToByteBoundary();
			if (this.gzipFooterSubstate == GZipDecoder.GzipHeaderState.ReadingCRC)
			{
				while (this.loopCounter < 4)
				{
					int bits = input.GetBits(8);
					if (bits < 0)
					{
						return false;
					}
					this.expectedCrc32 |= (uint)((uint)bits << 8 * this.loopCounter);
					this.loopCounter++;
				}
				this.gzipFooterSubstate = GZipDecoder.GzipHeaderState.ReadingFileSize;
				this.loopCounter = 0;
			}
			if (this.gzipFooterSubstate == GZipDecoder.GzipHeaderState.ReadingFileSize)
			{
				if (this.loopCounter == 0)
				{
					this.expectedOutputStreamSizeModulo = 0U;
				}
				while (this.loopCounter < 4)
				{
					int bits2 = input.GetBits(8);
					if (bits2 < 0)
					{
						return false;
					}
					this.expectedOutputStreamSizeModulo |= (uint)((uint)bits2 << 8 * this.loopCounter);
					this.loopCounter++;
				}
			}
			return true;
		}

		// Token: 0x06002820 RID: 10272 RVA: 0x000B86F8 File Offset: 0x000B68F8
		public void UpdateWithBytesRead(byte[] buffer, int offset, int copied)
		{
			this.actualCrc32 = Crc32Helper.UpdateCrc32(this.actualCrc32, buffer, offset, copied);
			long num = this.actualStreamSizeModulo + (long)((ulong)copied);
			if (num >= 4294967296L)
			{
				num %= 4294967296L;
			}
			this.actualStreamSizeModulo = num;
		}

		// Token: 0x06002821 RID: 10273 RVA: 0x000B8742 File Offset: 0x000B6942
		public void Validate()
		{
			if (this.expectedCrc32 != this.actualCrc32)
			{
				throw new InvalidDataException(SR.GetString("InvalidCRC"));
			}
			if (this.actualStreamSizeModulo != (long)((ulong)this.expectedOutputStreamSizeModulo))
			{
				throw new InvalidDataException(SR.GetString("InvalidStreamSize"));
			}
		}

		// Token: 0x040021D7 RID: 8663
		private GZipDecoder.GzipHeaderState gzipHeaderSubstate;

		// Token: 0x040021D8 RID: 8664
		private GZipDecoder.GzipHeaderState gzipFooterSubstate;

		// Token: 0x040021D9 RID: 8665
		private int gzip_header_flag;

		// Token: 0x040021DA RID: 8666
		private int gzip_header_xlen;

		// Token: 0x040021DB RID: 8667
		private uint expectedCrc32;

		// Token: 0x040021DC RID: 8668
		private uint expectedOutputStreamSizeModulo;

		// Token: 0x040021DD RID: 8669
		private int loopCounter;

		// Token: 0x040021DE RID: 8670
		private uint actualCrc32;

		// Token: 0x040021DF RID: 8671
		private long actualStreamSizeModulo;

		// Token: 0x0200082A RID: 2090
		internal enum GzipHeaderState
		{
			// Token: 0x040035D8 RID: 13784
			ReadingID1,
			// Token: 0x040035D9 RID: 13785
			ReadingID2,
			// Token: 0x040035DA RID: 13786
			ReadingCM,
			// Token: 0x040035DB RID: 13787
			ReadingFLG,
			// Token: 0x040035DC RID: 13788
			ReadingMMTime,
			// Token: 0x040035DD RID: 13789
			ReadingXFL,
			// Token: 0x040035DE RID: 13790
			ReadingOS,
			// Token: 0x040035DF RID: 13791
			ReadingXLen1,
			// Token: 0x040035E0 RID: 13792
			ReadingXLen2,
			// Token: 0x040035E1 RID: 13793
			ReadingXLenData,
			// Token: 0x040035E2 RID: 13794
			ReadingFileName,
			// Token: 0x040035E3 RID: 13795
			ReadingComment,
			// Token: 0x040035E4 RID: 13796
			ReadingCRC16Part1,
			// Token: 0x040035E5 RID: 13797
			ReadingCRC16Part2,
			// Token: 0x040035E6 RID: 13798
			Done,
			// Token: 0x040035E7 RID: 13799
			ReadingCRC,
			// Token: 0x040035E8 RID: 13800
			ReadingFileSize
		}

		// Token: 0x0200082B RID: 2091
		[Flags]
		internal enum GZipOptionalHeaderFlags
		{
			// Token: 0x040035EA RID: 13802
			CRCFlag = 2,
			// Token: 0x040035EB RID: 13803
			ExtraFieldsFlag = 4,
			// Token: 0x040035EC RID: 13804
			FileNameFlag = 8,
			// Token: 0x040035ED RID: 13805
			CommentFlag = 16
		}
	}
}
