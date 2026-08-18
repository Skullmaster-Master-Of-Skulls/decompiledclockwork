using System;
using System.IO;
using System.Text;

namespace System.ServiceModel.Channels
{
	// Token: 0x020007EC RID: 2028
	internal abstract class StringDecoder
	{
		// Token: 0x06004CB1 RID: 19633 RVA: 0x00117D15 File Offset: 0x00115F15
		public StringDecoder(int sizeQuota)
		{
			this.sizeQuota = sizeQuota;
			this.sizeDecoder = default(IntDecoder);
			this.currentState = StringDecoder.State.ReadingSize;
			this.Reset();
		}

		// Token: 0x1700133B RID: 4923
		// (get) Token: 0x06004CB2 RID: 19634 RVA: 0x00117D3D File Offset: 0x00115F3D
		public bool IsValueDecoded
		{
			get
			{
				return this.currentState == StringDecoder.State.Done;
			}
		}

		// Token: 0x1700133C RID: 4924
		// (get) Token: 0x06004CB3 RID: 19635 RVA: 0x00117D48 File Offset: 0x00115F48
		public string Value
		{
			get
			{
				if (this.currentState != StringDecoder.State.Done)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("FramingValueNotAvailable")));
				}
				return this.value;
			}
		}

		// Token: 0x06004CB4 RID: 19636 RVA: 0x00117D74 File Offset: 0x00115F74
		public int Decode(byte[] buffer, int offset, int size)
		{
			DecoderHelper.ValidateSize(size);
			StringDecoder.State state = this.currentState;
			int num;
			if (state != StringDecoder.State.ReadingSize)
			{
				if (state != StringDecoder.State.ReadingBytes)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidDataException(SR.GetString("InvalidDecoderStateMachine")));
				}
				if (this.value != null && this.valueLengthInBytes == this.encodedSize && this.bytesNeeded == this.encodedSize && size >= this.encodedSize && StringDecoder.CompareBuffers(this.encodedBytes, buffer, offset))
				{
					num = this.bytesNeeded;
					this.OnComplete(this.value);
				}
				else
				{
					num = this.bytesNeeded;
					if (size < this.bytesNeeded)
					{
						num = size;
					}
					Buffer.BlockCopy(buffer, offset, this.encodedBytes, this.encodedSize - this.bytesNeeded, num);
					this.bytesNeeded -= num;
					if (this.bytesNeeded == 0)
					{
						this.value = Encoding.UTF8.GetString(this.encodedBytes, 0, this.encodedSize);
						this.valueLengthInBytes = this.encodedSize;
						this.OnComplete(this.value);
					}
				}
			}
			else
			{
				num = this.sizeDecoder.Decode(buffer, offset, size);
				if (this.sizeDecoder.IsValueDecoded)
				{
					this.encodedSize = this.sizeDecoder.Value;
					if (this.encodedSize > this.sizeQuota)
					{
						Exception exception = this.OnSizeQuotaExceeded(this.encodedSize);
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(exception);
					}
					if (this.encodedBytes == null || this.encodedBytes.Length < this.encodedSize)
					{
						this.encodedBytes = DiagnosticUtility.Utility.AllocateByteArray(this.encodedSize);
						this.value = null;
					}
					this.currentState = StringDecoder.State.ReadingBytes;
					this.bytesNeeded = this.encodedSize;
				}
			}
			return num;
		}

		// Token: 0x06004CB5 RID: 19637 RVA: 0x00117F2A File Offset: 0x0011612A
		protected virtual void OnComplete(string value)
		{
			this.currentState = StringDecoder.State.Done;
		}

		// Token: 0x06004CB6 RID: 19638 RVA: 0x00117F34 File Offset: 0x00116134
		private static bool CompareBuffers(byte[] buffer1, byte[] buffer2, int offset)
		{
			for (int i = 0; i < buffer1.Length; i++)
			{
				if (buffer1[i] != buffer2[i + offset])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06004CB7 RID: 19639
		protected abstract Exception OnSizeQuotaExceeded(int size);

		// Token: 0x06004CB8 RID: 19640 RVA: 0x00117F5C File Offset: 0x0011615C
		public void Reset()
		{
			this.currentState = StringDecoder.State.ReadingSize;
			this.sizeDecoder.Reset();
		}

		// Token: 0x04002FC1 RID: 12225
		private int encodedSize;

		// Token: 0x04002FC2 RID: 12226
		private byte[] encodedBytes;

		// Token: 0x04002FC3 RID: 12227
		private int bytesNeeded;

		// Token: 0x04002FC4 RID: 12228
		private string value;

		// Token: 0x04002FC5 RID: 12229
		private StringDecoder.State currentState;

		// Token: 0x04002FC6 RID: 12230
		private IntDecoder sizeDecoder;

		// Token: 0x04002FC7 RID: 12231
		private int sizeQuota;

		// Token: 0x04002FC8 RID: 12232
		private int valueLengthInBytes;

		// Token: 0x02000D0E RID: 3342
		private enum State
		{
			// Token: 0x04004675 RID: 18037
			ReadingSize,
			// Token: 0x04004676 RID: 18038
			ReadingBytes,
			// Token: 0x04004677 RID: 18039
			Done
		}
	}
}
