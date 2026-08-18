using System;
using System.IO;

namespace System.ServiceModel.Channels
{
	// Token: 0x020007F9 RID: 2041
	internal class ClientSingletonDecoder : ClientFramingDecoder
	{
		// Token: 0x06004D00 RID: 19712 RVA: 0x00119628 File Offset: 0x00117828
		public ClientSingletonDecoder(long streamPosition) : base(streamPosition)
		{
		}

		// Token: 0x1700135C RID: 4956
		// (get) Token: 0x06004D01 RID: 19713 RVA: 0x00119631 File Offset: 0x00117831
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

		// Token: 0x06004D02 RID: 19714 RVA: 0x00119664 File Offset: 0x00117864
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
						goto IL_1A2;
					}
					num = 0;
					base.CurrentState = ClientFramingDecoderState.ReadingAckRecord;
					goto IL_1A2;
				}
				case ClientFramingDecoderState.UpgradeResponse:
					num = 0;
					base.CurrentState = ClientFramingDecoderState.ReadingUpgradeRecord;
					goto IL_1A2;
				case ClientFramingDecoderState.ReadingAckRecord:
				{
					FramingRecordType framingRecordType = (FramingRecordType)bytes[offset];
					if (framingRecordType == FramingRecordType.Fault)
					{
						num = 1;
						this.faultDecoder = new FaultStringDecoder();
						base.CurrentState = ClientFramingDecoderState.ReadingFaultString;
						goto IL_1A2;
					}
					base.ValidatePreambleAck(framingRecordType);
					num = 1;
					base.CurrentState = ClientFramingDecoderState.Start;
					goto IL_1A2;
				}
				case ClientFramingDecoderState.Start:
					num = 0;
					base.CurrentState = ClientFramingDecoderState.ReadingEnvelopeRecord;
					goto IL_1A2;
				case ClientFramingDecoderState.ReadingFault:
				{
					FramingRecordType framingRecordType = (FramingRecordType)bytes[offset];
					base.ValidateRecordType(FramingRecordType.Fault, framingRecordType);
					num = 1;
					this.faultDecoder = new FaultStringDecoder();
					base.CurrentState = ClientFramingDecoderState.ReadingFaultString;
					goto IL_1A2;
				}
				case ClientFramingDecoderState.ReadingFaultString:
					num = this.faultDecoder.Decode(bytes, offset, size);
					if (this.faultDecoder.IsValueDecoded)
					{
						base.CurrentState = ClientFramingDecoderState.Fault;
						goto IL_1A2;
					}
					goto IL_1A2;
				case ClientFramingDecoderState.Fault:
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(base.CreateException(new InvalidDataException(SR.GetString("FramingAtEnd"))));
				case ClientFramingDecoderState.ReadingEnvelopeRecord:
				{
					FramingRecordType framingRecordType = (FramingRecordType)bytes[offset];
					if (framingRecordType == FramingRecordType.End)
					{
						num = 1;
						base.CurrentState = ClientFramingDecoderState.End;
						goto IL_1A2;
					}
					if (framingRecordType == FramingRecordType.Fault)
					{
						num = 0;
						base.CurrentState = ClientFramingDecoderState.ReadingFault;
						goto IL_1A2;
					}
					base.ValidateRecordType(FramingRecordType.UnsizedEnvelope, framingRecordType);
					num = 1;
					base.CurrentState = ClientFramingDecoderState.EnvelopeStart;
					goto IL_1A2;
				}
				case ClientFramingDecoderState.EnvelopeStart:
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(base.CreateException(new InvalidDataException(SR.GetString("FramingAtEnd"))));
				}
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(base.CreateException(new InvalidDataException(SR.GetString("InvalidDecoderStateMachine"))));
				IL_1A2:
				base.StreamPosition += (long)num;
				result = num;
			}
			catch (InvalidDataException innerException)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(base.CreateException(innerException));
			}
			return result;
		}

		// Token: 0x04002FFA RID: 12282
		private FaultStringDecoder faultDecoder;
	}
}
