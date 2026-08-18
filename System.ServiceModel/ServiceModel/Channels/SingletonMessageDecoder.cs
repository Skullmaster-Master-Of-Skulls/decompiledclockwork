using System;
using System.IO;

namespace System.ServiceModel.Channels
{
	// Token: 0x020007F3 RID: 2035
	internal class SingletonMessageDecoder : FramingDecoder
	{
		// Token: 0x06004CE1 RID: 19681 RVA: 0x00118B5C File Offset: 0x00116D5C
		public SingletonMessageDecoder(long streamPosition) : base(streamPosition)
		{
			this.sizeDecoder = default(IntDecoder);
			this.currentState = SingletonMessageDecoder.State.ChunkStart;
		}

		// Token: 0x06004CE2 RID: 19682 RVA: 0x00118B78 File Offset: 0x00116D78
		public void Reset()
		{
			this.currentState = SingletonMessageDecoder.State.ChunkStart;
		}

		// Token: 0x1700134B RID: 4939
		// (get) Token: 0x06004CE3 RID: 19683 RVA: 0x00118B81 File Offset: 0x00116D81
		public SingletonMessageDecoder.State CurrentState
		{
			get
			{
				return this.currentState;
			}
		}

		// Token: 0x1700134C RID: 4940
		// (get) Token: 0x06004CE4 RID: 19684 RVA: 0x00118B89 File Offset: 0x00116D89
		protected override string CurrentStateAsString
		{
			get
			{
				return this.currentState.ToString();
			}
		}

		// Token: 0x1700134D RID: 4941
		// (get) Token: 0x06004CE5 RID: 19685 RVA: 0x00118B9C File Offset: 0x00116D9C
		public int ChunkSize
		{
			get
			{
				if (this.currentState < SingletonMessageDecoder.State.ChunkStart)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("FramingValueNotAvailable")));
				}
				return this.chunkSize;
			}
		}

		// Token: 0x06004CE6 RID: 19686 RVA: 0x00118BC8 File Offset: 0x00116DC8
		public int Decode(byte[] bytes, int offset, int size)
		{
			DecoderHelper.ValidateSize(size);
			int result;
			try
			{
				int num;
				switch (this.currentState)
				{
				case SingletonMessageDecoder.State.ReadingEnvelopeChunkSize:
					num = this.sizeDecoder.Decode(bytes, offset, size);
					if (this.sizeDecoder.IsValueDecoded)
					{
						this.chunkSize = this.sizeDecoder.Value;
						this.sizeDecoder.Reset();
						if (this.chunkSize == 0)
						{
							this.currentState = SingletonMessageDecoder.State.EnvelopeEnd;
						}
						else
						{
							this.currentState = SingletonMessageDecoder.State.ChunkStart;
							this.chunkBytesNeeded = this.chunkSize;
						}
					}
					break;
				case SingletonMessageDecoder.State.ChunkStart:
					num = 0;
					this.currentState = SingletonMessageDecoder.State.ReadingEnvelopeBytes;
					break;
				case SingletonMessageDecoder.State.ReadingEnvelopeBytes:
					num = size;
					if (num > this.chunkBytesNeeded)
					{
						num = this.chunkBytesNeeded;
					}
					this.chunkBytesNeeded -= num;
					if (this.chunkBytesNeeded == 0)
					{
						this.currentState = SingletonMessageDecoder.State.ChunkEnd;
					}
					break;
				case SingletonMessageDecoder.State.ChunkEnd:
					num = 0;
					this.currentState = SingletonMessageDecoder.State.ReadingEnvelopeChunkSize;
					break;
				case SingletonMessageDecoder.State.EnvelopeEnd:
					base.ValidateRecordType(FramingRecordType.End, (FramingRecordType)bytes[offset]);
					num = 1;
					this.currentState = SingletonMessageDecoder.State.End;
					break;
				case SingletonMessageDecoder.State.End:
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(base.CreateException(new InvalidDataException(SR.GetString("FramingAtEnd"))));
				default:
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(base.CreateException(new InvalidDataException(SR.GetString("InvalidDecoderStateMachine"))));
				}
				base.StreamPosition += (long)num;
				result = num;
			}
			catch (InvalidDataException innerException)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(base.CreateException(innerException));
			}
			return result;
		}

		// Token: 0x04002FD8 RID: 12248
		private IntDecoder sizeDecoder;

		// Token: 0x04002FD9 RID: 12249
		private int chunkBytesNeeded;

		// Token: 0x04002FDA RID: 12250
		private int chunkSize;

		// Token: 0x04002FDB RID: 12251
		private SingletonMessageDecoder.State currentState;

		// Token: 0x02000D11 RID: 3345
		public enum State
		{
			// Token: 0x04004693 RID: 18067
			ReadingEnvelopeChunkSize,
			// Token: 0x04004694 RID: 18068
			ChunkStart,
			// Token: 0x04004695 RID: 18069
			ReadingEnvelopeBytes,
			// Token: 0x04004696 RID: 18070
			ChunkEnd,
			// Token: 0x04004697 RID: 18071
			EnvelopeEnd,
			// Token: 0x04004698 RID: 18072
			End
		}
	}
}
