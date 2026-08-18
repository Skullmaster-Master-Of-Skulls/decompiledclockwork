using System;

namespace System.IO.Compression
{
	// Token: 0x02000428 RID: 1064
	internal class FastEncoder
	{
		// Token: 0x060027F2 RID: 10226 RVA: 0x000B793B File Offset: 0x000B5B3B
		public FastEncoder()
		{
			this.inputWindow = new FastEncoderWindow();
			this.currentMatch = new Match();
		}

		// Token: 0x170009DC RID: 2524
		// (get) Token: 0x060027F3 RID: 10227 RVA: 0x000B7959 File Offset: 0x000B5B59
		internal int BytesInHistory
		{
			get
			{
				return this.inputWindow.BytesAvailable;
			}
		}

		// Token: 0x170009DD RID: 2525
		// (get) Token: 0x060027F4 RID: 10228 RVA: 0x000B7966 File Offset: 0x000B5B66
		internal DeflateInput UnprocessedInput
		{
			get
			{
				return this.inputWindow.UnprocessedInput;
			}
		}

		// Token: 0x060027F5 RID: 10229 RVA: 0x000B7973 File Offset: 0x000B5B73
		internal void FlushInput()
		{
			this.inputWindow.FlushWindow();
		}

		// Token: 0x170009DE RID: 2526
		// (get) Token: 0x060027F6 RID: 10230 RVA: 0x000B7980 File Offset: 0x000B5B80
		internal double LastCompressionRatio
		{
			get
			{
				return this.lastCompressionRatio;
			}
		}

		// Token: 0x060027F7 RID: 10231 RVA: 0x000B7988 File Offset: 0x000B5B88
		internal void GetBlock(DeflateInput input, OutputBuffer output, int maxBytesToCopy)
		{
			FastEncoder.WriteDeflatePreamble(output);
			this.GetCompressedOutput(input, output, maxBytesToCopy);
			this.WriteEndOfBlock(output);
		}

		// Token: 0x060027F8 RID: 10232 RVA: 0x000B79A0 File Offset: 0x000B5BA0
		internal void GetCompressedData(DeflateInput input, OutputBuffer output)
		{
			this.GetCompressedOutput(input, output, -1);
		}

		// Token: 0x060027F9 RID: 10233 RVA: 0x000B79AB File Offset: 0x000B5BAB
		internal void GetBlockHeader(OutputBuffer output)
		{
			FastEncoder.WriteDeflatePreamble(output);
		}

		// Token: 0x060027FA RID: 10234 RVA: 0x000B79B3 File Offset: 0x000B5BB3
		internal void GetBlockFooter(OutputBuffer output)
		{
			this.WriteEndOfBlock(output);
		}

		// Token: 0x060027FB RID: 10235 RVA: 0x000B79BC File Offset: 0x000B5BBC
		private void GetCompressedOutput(DeflateInput input, OutputBuffer output, int maxBytesToCopy)
		{
			int bytesWritten = output.BytesWritten;
			int num = 0;
			int num2 = this.BytesInHistory + input.Count;
			do
			{
				int num3 = (input.Count < this.inputWindow.FreeWindowSpace) ? input.Count : this.inputWindow.FreeWindowSpace;
				if (maxBytesToCopy >= 1)
				{
					num3 = Math.Min(num3, maxBytesToCopy - num);
				}
				if (num3 > 0)
				{
					this.inputWindow.CopyBytes(input.Buffer, input.StartIndex, num3);
					input.ConsumeBytes(num3);
					num += num3;
				}
				this.GetCompressedOutput(output);
			}
			while (this.SafeToWriteTo(output) && this.InputAvailable(input) && (maxBytesToCopy < 1 || num < maxBytesToCopy));
			int bytesWritten2 = output.BytesWritten;
			int num4 = bytesWritten2 - bytesWritten;
			int num5 = this.BytesInHistory + input.Count;
			int num6 = num2 - num5;
			if (num4 != 0)
			{
				this.lastCompressionRatio = (double)num4 / (double)num6;
			}
		}

		// Token: 0x060027FC RID: 10236 RVA: 0x000B7A9C File Offset: 0x000B5C9C
		private void GetCompressedOutput(OutputBuffer output)
		{
			while (this.inputWindow.BytesAvailable > 0 && this.SafeToWriteTo(output))
			{
				this.inputWindow.GetNextSymbolOrMatch(this.currentMatch);
				if (this.currentMatch.State == MatchState.HasSymbol)
				{
					FastEncoder.WriteChar(this.currentMatch.Symbol, output);
				}
				else if (this.currentMatch.State == MatchState.HasMatch)
				{
					FastEncoder.WriteMatch(this.currentMatch.Length, this.currentMatch.Position, output);
				}
				else
				{
					FastEncoder.WriteChar(this.currentMatch.Symbol, output);
					FastEncoder.WriteMatch(this.currentMatch.Length, this.currentMatch.Position, output);
				}
			}
		}

		// Token: 0x060027FD RID: 10237 RVA: 0x000B7B54 File Offset: 0x000B5D54
		private bool InputAvailable(DeflateInput input)
		{
			return input.Count > 0 || this.BytesInHistory > 0;
		}

		// Token: 0x060027FE RID: 10238 RVA: 0x000B7B6A File Offset: 0x000B5D6A
		private bool SafeToWriteTo(OutputBuffer output)
		{
			return output.FreeBytes > 16;
		}

		// Token: 0x060027FF RID: 10239 RVA: 0x000B7B78 File Offset: 0x000B5D78
		private void WriteEndOfBlock(OutputBuffer output)
		{
			uint num = FastEncoderStatics.FastEncoderLiteralCodeInfo[256];
			int n = (int)(num & 31U);
			output.WriteBits(n, num >> 5);
		}

		// Token: 0x06002800 RID: 10240 RVA: 0x000B7BA0 File Offset: 0x000B5DA0
		internal static void WriteMatch(int matchLen, int matchPos, OutputBuffer output)
		{
			uint num = FastEncoderStatics.FastEncoderLiteralCodeInfo[254 + matchLen];
			int num2 = (int)(num & 31U);
			if (num2 <= 16)
			{
				output.WriteBits(num2, num >> 5);
			}
			else
			{
				output.WriteBits(16, num >> 5 & 65535U);
				output.WriteBits(num2 - 16, num >> 21);
			}
			num = FastEncoderStatics.FastEncoderDistanceCodeInfo[FastEncoderStatics.GetSlot(matchPos)];
			output.WriteBits((int)(num & 15U), num >> 8);
			int num3 = (int)(num >> 4 & 15U);
			if (num3 != 0)
			{
				output.WriteBits(num3, (uint)(matchPos & (int)FastEncoderStatics.BitMask[num3]));
			}
		}

		// Token: 0x06002801 RID: 10241 RVA: 0x000B7C24 File Offset: 0x000B5E24
		internal static void WriteChar(byte b, OutputBuffer output)
		{
			uint num = FastEncoderStatics.FastEncoderLiteralCodeInfo[(int)b];
			output.WriteBits((int)(num & 31U), num >> 5);
		}

		// Token: 0x06002802 RID: 10242 RVA: 0x000B7C46 File Offset: 0x000B5E46
		internal static void WriteDeflatePreamble(OutputBuffer output)
		{
			output.WriteBytes(FastEncoderStatics.FastEncoderTreeStructureData, 0, FastEncoderStatics.FastEncoderTreeStructureData.Length);
			output.WriteBits(9, 34U);
		}

		// Token: 0x040021B1 RID: 8625
		private FastEncoderWindow inputWindow;

		// Token: 0x040021B2 RID: 8626
		private Match currentMatch;

		// Token: 0x040021B3 RID: 8627
		private double lastCompressionRatio;
	}
}
