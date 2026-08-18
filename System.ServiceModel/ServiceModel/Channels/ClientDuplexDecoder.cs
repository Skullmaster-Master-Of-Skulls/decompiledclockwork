using System;
using System.IO;

namespace System.ServiceModel.Channels
{
	// Token: 0x020007F8 RID: 2040
	internal class ClientDuplexDecoder : ClientFramingDecoder
	{
		// Token: 0x06004CFC RID: 19708 RVA: 0x0011931E File Offset: 0x0011751E
		public ClientDuplexDecoder(long streamPosition) : base(streamPosition)
		{
			this.sizeDecoder = default(IntDecoder);
		}

		// Token: 0x1700135A RID: 4954
		// (get) Token: 0x06004CFD RID: 19709 RVA: 0x00119333 File Offset: 0x00117533
		public int EnvelopeSize
		{
			get
			{
				if (base.CurrentState < ClientFramingDecoderState.EnvelopeStart)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("FramingValueNotAvailable")));
				}
				return this.envelopeSize;
			}
		}

		// Token: 0x1700135B RID: 4955
		// (get) Token: 0x06004CFE RID: 19710 RVA: 0x0011935F File Offset: 0x0011755F
		public override string Fault
		{
			get
			{
				if (base.CurrentState < ClientFramingDecoderState.Fault)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("FramingValueNotAvailable")));
				}
				return this.faultDecoder.Value;
			}
		}

		// Token: 0x06004CFF RID: 19711 RVA: 0x00119390 File Offset: 0x00117590
		public override int Decode(byte[] bytes, int offset, int size)
		{
			DecoderHelper.ValidateSize(size);
			int result;
			try
			{
				int num;
				switch (base.CurrentState)
				{
				case ClientFramingDecoderState.ReadingUpgradeRecord:
				{
					FramingRecordType framingRecordType = (FramingRecordType)bytes[offset];
					if (framingRecordType == FramingRecordType.UpgradeResponse)
					{
						num = 1;
						base.CurrentState = ClientFramingDecoderState.UpgradeResponse;
						goto IL_246;
					}
					num = 0;
					base.CurrentState = ClientFramingDecoderState.ReadingAckRecord;
					goto IL_246;
				}
				case ClientFramingDecoderState.UpgradeResponse:
					num = 0;
					base.CurrentState = ClientFramingDecoderState.ReadingUpgradeRecord;
					goto IL_246;
				case ClientFramingDecoderState.ReadingAckRecord:
				{
					FramingRecordType framingRecordType = (FramingRecordType)bytes[offset];
					if (framingRecordType == FramingRecordType.Fault)
					{
						num = 1;
						this.faultDecoder = new FaultStringDecoder();
						base.CurrentState = ClientFramingDecoderState.ReadingFaultString;
						goto IL_246;
					}
					base.ValidatePreambleAck(framingRecordType);
					num = 1;
					base.CurrentState = ClientFramingDecoderState.Start;
					goto IL_246;
				}
				case ClientFramingDecoderState.Start:
					num = 0;
					base.CurrentState = ClientFramingDecoderState.ReadingEnvelopeRecord;
					goto IL_246;
				case ClientFramingDecoderState.ReadingFaultString:
					num = this.faultDecoder.Decode(bytes, offset, size);
					if (this.faultDecoder.IsValueDecoded)
					{
						base.CurrentState = ClientFramingDecoderState.Fault;
						goto IL_246;
					}
					goto IL_246;
				case ClientFramingDecoderState.Fault:
					num = 0;
					base.CurrentState = ClientFramingDecoderState.ReadingEndRecord;
					goto IL_246;
				case ClientFramingDecoderState.ReadingEnvelopeRecord:
				{
					FramingRecordType framingRecordType = (FramingRecordType)bytes[offset];
					if (framingRecordType == FramingRecordType.End)
					{
						num = 1;
						base.CurrentState = ClientFramingDecoderState.End;
						goto IL_246;
					}
					if (framingRecordType == FramingRecordType.Fault)
					{
						num = 1;
						this.faultDecoder = new FaultStringDecoder();
						base.CurrentState = ClientFramingDecoderState.ReadingFaultString;
						goto IL_246;
					}
					base.ValidateRecordType(FramingRecordType.SizedEnvelope, framingRecordType);
					num = 1;
					base.CurrentState = ClientFramingDecoderState.ReadingEnvelopeSize;
					this.sizeDecoder.Reset();
					goto IL_246;
				}
				case ClientFramingDecoderState.ReadingEnvelopeSize:
					num = this.sizeDecoder.Decode(bytes, offset, size);
					if (this.sizeDecoder.IsValueDecoded)
					{
						base.CurrentState = ClientFramingDecoderState.EnvelopeStart;
						this.envelopeSize = this.sizeDecoder.Value;
						this.envelopeBytesNeeded = this.envelopeSize;
						goto IL_246;
					}
					goto IL_246;
				case ClientFramingDecoderState.EnvelopeStart:
					num = 0;
					base.CurrentState = ClientFramingDecoderState.ReadingEnvelopeBytes;
					goto IL_246;
				case ClientFramingDecoderState.ReadingEnvelopeBytes:
					num = size;
					if (num > this.envelopeBytesNeeded)
					{
						num = this.envelopeBytesNeeded;
					}
					this.envelopeBytesNeeded -= num;
					if (this.envelopeBytesNeeded == 0)
					{
						base.CurrentState = ClientFramingDecoderState.EnvelopeEnd;
						goto IL_246;
					}
					goto IL_246;
				case ClientFramingDecoderState.EnvelopeEnd:
					num = 0;
					base.CurrentState = ClientFramingDecoderState.ReadingEnvelopeRecord;
					goto IL_246;
				case ClientFramingDecoderState.ReadingEndRecord:
					base.ValidateRecordType(FramingRecordType.End, (FramingRecordType)bytes[offset]);
					num = 1;
					base.CurrentState = ClientFramingDecoderState.End;
					goto IL_246;
				case ClientFramingDecoderState.End:
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(base.CreateException(new InvalidDataException(SR.GetString("FramingAtEnd"))));
				}
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(base.CreateException(new InvalidDataException(SR.GetString("InvalidDecoderStateMachine"))));
				IL_246:
				base.StreamPosition += (long)num;
				result = num;
			}
			catch (InvalidDataException innerException)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(base.CreateException(innerException));
			}
			return result;
		}

		// Token: 0x04002FF6 RID: 12278
		private IntDecoder sizeDecoder;

		// Token: 0x04002FF7 RID: 12279
		private FaultStringDecoder faultDecoder;

		// Token: 0x04002FF8 RID: 12280
		private int envelopeBytesNeeded;

		// Token: 0x04002FF9 RID: 12281
		private int envelopeSize;
	}
}
