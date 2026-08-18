using System;

namespace System.IO.Compression
{
	// Token: 0x02000421 RID: 1057
	internal class DeflaterManaged : IDeflater, IDisposable
	{
		// Token: 0x06002787 RID: 10119 RVA: 0x000B5E46 File Offset: 0x000B4046
		internal DeflaterManaged()
		{
			this.deflateEncoder = new FastEncoder();
			this.copyEncoder = new CopyEncoder();
			this.input = new DeflateInput();
			this.output = new OutputBuffer();
			this.processingState = DeflaterManaged.DeflaterState.NotStarted;
		}

		// Token: 0x06002788 RID: 10120 RVA: 0x000B5E81 File Offset: 0x000B4081
		private bool NeedsInput()
		{
			return ((IDeflater)this).NeedsInput();
		}

		// Token: 0x06002789 RID: 10121 RVA: 0x000B5E89 File Offset: 0x000B4089
		bool IDeflater.NeedsInput()
		{
			return this.input.Count == 0 && this.deflateEncoder.BytesInHistory == 0;
		}

		// Token: 0x0600278A RID: 10122 RVA: 0x000B5EA8 File Offset: 0x000B40A8
		void IDeflater.SetInput(byte[] inputBuffer, int startIndex, int count)
		{
			this.input.Buffer = inputBuffer;
			this.input.Count = count;
			this.input.StartIndex = startIndex;
			if (count > 0 && count < 256)
			{
				DeflaterManaged.DeflaterState deflaterState = this.processingState;
				if (deflaterState != DeflaterManaged.DeflaterState.NotStarted)
				{
					if (deflaterState == DeflaterManaged.DeflaterState.CompressThenCheck)
					{
						this.processingState = DeflaterManaged.DeflaterState.HandlingSmallData;
						return;
					}
					if (deflaterState != DeflaterManaged.DeflaterState.CheckingForIncompressible)
					{
						return;
					}
				}
				this.processingState = DeflaterManaged.DeflaterState.StartingSmallData;
				return;
			}
		}

		// Token: 0x0600278B RID: 10123 RVA: 0x000B5F08 File Offset: 0x000B4108
		int IDeflater.GetDeflateOutput(byte[] outputBuffer)
		{
			this.output.UpdateBuffer(outputBuffer);
			switch (this.processingState)
			{
			case DeflaterManaged.DeflaterState.NotStarted:
			{
				DeflateInput.InputState state = this.input.DumpState();
				OutputBuffer.BufferState state2 = this.output.DumpState();
				this.deflateEncoder.GetBlockHeader(this.output);
				this.deflateEncoder.GetCompressedData(this.input, this.output);
				if (!this.UseCompressed(this.deflateEncoder.LastCompressionRatio))
				{
					this.input.RestoreState(state);
					this.output.RestoreState(state2);
					this.copyEncoder.GetBlock(this.input, this.output, false);
					this.FlushInputWindows();
					this.processingState = DeflaterManaged.DeflaterState.CheckingForIncompressible;
					goto IL_23A;
				}
				this.processingState = DeflaterManaged.DeflaterState.CompressThenCheck;
				goto IL_23A;
			}
			case DeflaterManaged.DeflaterState.SlowDownForIncompressible1:
				this.deflateEncoder.GetBlockFooter(this.output);
				this.processingState = DeflaterManaged.DeflaterState.SlowDownForIncompressible2;
				break;
			case DeflaterManaged.DeflaterState.SlowDownForIncompressible2:
				break;
			case DeflaterManaged.DeflaterState.StartingSmallData:
				this.deflateEncoder.GetBlockHeader(this.output);
				this.processingState = DeflaterManaged.DeflaterState.HandlingSmallData;
				goto IL_223;
			case DeflaterManaged.DeflaterState.CompressThenCheck:
				this.deflateEncoder.GetCompressedData(this.input, this.output);
				if (!this.UseCompressed(this.deflateEncoder.LastCompressionRatio))
				{
					this.processingState = DeflaterManaged.DeflaterState.SlowDownForIncompressible1;
					this.inputFromHistory = this.deflateEncoder.UnprocessedInput;
					goto IL_23A;
				}
				goto IL_23A;
			case DeflaterManaged.DeflaterState.CheckingForIncompressible:
			{
				DeflateInput.InputState state3 = this.input.DumpState();
				OutputBuffer.BufferState state4 = this.output.DumpState();
				this.deflateEncoder.GetBlock(this.input, this.output, 8072);
				if (!this.UseCompressed(this.deflateEncoder.LastCompressionRatio))
				{
					this.input.RestoreState(state3);
					this.output.RestoreState(state4);
					this.copyEncoder.GetBlock(this.input, this.output, false);
					this.FlushInputWindows();
					goto IL_23A;
				}
				goto IL_23A;
			}
			case DeflaterManaged.DeflaterState.HandlingSmallData:
				goto IL_223;
			default:
				goto IL_23A;
			}
			if (this.inputFromHistory.Count > 0)
			{
				this.copyEncoder.GetBlock(this.inputFromHistory, this.output, false);
			}
			if (this.inputFromHistory.Count == 0)
			{
				this.deflateEncoder.FlushInput();
				this.processingState = DeflaterManaged.DeflaterState.CheckingForIncompressible;
				goto IL_23A;
			}
			goto IL_23A;
			IL_223:
			this.deflateEncoder.GetCompressedData(this.input, this.output);
			IL_23A:
			return this.output.BytesWritten;
		}

		// Token: 0x0600278C RID: 10124 RVA: 0x000B615C File Offset: 0x000B435C
		bool IDeflater.Finish(byte[] outputBuffer, out int bytesRead)
		{
			if (this.processingState == DeflaterManaged.DeflaterState.NotStarted)
			{
				bytesRead = 0;
				return true;
			}
			this.output.UpdateBuffer(outputBuffer);
			if (this.processingState == DeflaterManaged.DeflaterState.CompressThenCheck || this.processingState == DeflaterManaged.DeflaterState.HandlingSmallData || this.processingState == DeflaterManaged.DeflaterState.SlowDownForIncompressible1)
			{
				this.deflateEncoder.GetBlockFooter(this.output);
			}
			this.WriteFinal();
			bytesRead = this.output.BytesWritten;
			return true;
		}

		// Token: 0x0600278D RID: 10125 RVA: 0x000B61C2 File Offset: 0x000B43C2
		void IDisposable.Dispose()
		{
		}

		// Token: 0x0600278E RID: 10126 RVA: 0x000B61C4 File Offset: 0x000B43C4
		protected void Dispose(bool disposing)
		{
		}

		// Token: 0x0600278F RID: 10127 RVA: 0x000B61C6 File Offset: 0x000B43C6
		private bool UseCompressed(double ratio)
		{
			return ratio <= 1.0;
		}

		// Token: 0x06002790 RID: 10128 RVA: 0x000B61D7 File Offset: 0x000B43D7
		private void FlushInputWindows()
		{
			this.deflateEncoder.FlushInput();
		}

		// Token: 0x06002791 RID: 10129 RVA: 0x000B61E4 File Offset: 0x000B43E4
		private void WriteFinal()
		{
			this.copyEncoder.GetBlock(null, this.output, true);
		}

		// Token: 0x04002177 RID: 8567
		private const int MinBlockSize = 256;

		// Token: 0x04002178 RID: 8568
		private const int MaxHeaderFooterGoo = 120;

		// Token: 0x04002179 RID: 8569
		private const int CleanCopySize = 8072;

		// Token: 0x0400217A RID: 8570
		private const double BadCompressionThreshold = 1.0;

		// Token: 0x0400217B RID: 8571
		private FastEncoder deflateEncoder;

		// Token: 0x0400217C RID: 8572
		private CopyEncoder copyEncoder;

		// Token: 0x0400217D RID: 8573
		private DeflateInput input;

		// Token: 0x0400217E RID: 8574
		private OutputBuffer output;

		// Token: 0x0400217F RID: 8575
		private DeflaterManaged.DeflaterState processingState;

		// Token: 0x04002180 RID: 8576
		private DeflateInput inputFromHistory;

		// Token: 0x02000817 RID: 2071
		private enum DeflaterState
		{
			// Token: 0x0400359D RID: 13725
			NotStarted,
			// Token: 0x0400359E RID: 13726
			SlowDownForIncompressible1,
			// Token: 0x0400359F RID: 13727
			SlowDownForIncompressible2,
			// Token: 0x040035A0 RID: 13728
			StartingSmallData,
			// Token: 0x040035A1 RID: 13729
			CompressThenCheck,
			// Token: 0x040035A2 RID: 13730
			CheckingForIncompressible,
			// Token: 0x040035A3 RID: 13731
			HandlingSmallData
		}
	}
}
