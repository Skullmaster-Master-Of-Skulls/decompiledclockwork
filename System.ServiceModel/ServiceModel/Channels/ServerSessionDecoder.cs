using System;
using System.IO;

namespace System.ServiceModel.Channels
{
	// Token: 0x020007F2 RID: 2034
	internal class ServerSessionDecoder : FramingDecoder
	{
		// Token: 0x06004CD8 RID: 19672 RVA: 0x001186E5 File Offset: 0x001168E5
		public ServerSessionDecoder(long streamPosition, int maxViaLength, int maxContentTypeLength) : base(streamPosition)
		{
			this.viaDecoder = new ViaStringDecoder(maxViaLength);
			this.contentTypeDecoder = new ContentTypeStringDecoder(maxContentTypeLength);
			this.sizeDecoder = default(IntDecoder);
			this.currentState = ServerSessionDecoder.State.ReadingViaRecord;
		}

		// Token: 0x17001345 RID: 4933
		// (get) Token: 0x06004CD9 RID: 19673 RVA: 0x00118719 File Offset: 0x00116919
		public ServerSessionDecoder.State CurrentState
		{
			get
			{
				return this.currentState;
			}
		}

		// Token: 0x17001346 RID: 4934
		// (get) Token: 0x06004CDA RID: 19674 RVA: 0x00118721 File Offset: 0x00116921
		protected override string CurrentStateAsString
		{
			get
			{
				return this.currentState.ToString();
			}
		}

		// Token: 0x17001347 RID: 4935
		// (get) Token: 0x06004CDB RID: 19675 RVA: 0x00118734 File Offset: 0x00116934
		public string ContentType
		{
			get
			{
				if (this.currentState < ServerSessionDecoder.State.PreUpgradeStart)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("FramingValueNotAvailable")));
				}
				return this.contentType;
			}
		}

		// Token: 0x17001348 RID: 4936
		// (get) Token: 0x06004CDC RID: 19676 RVA: 0x0011875F File Offset: 0x0011695F
		public Uri Via
		{
			get
			{
				if (this.currentState < ServerSessionDecoder.State.ReadingContentTypeRecord)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("FramingValueNotAvailable")));
				}
				return this.viaDecoder.ValueAsUri;
			}
		}

		// Token: 0x06004CDD RID: 19677 RVA: 0x0011878F File Offset: 0x0011698F
		public void Reset(long streamPosition)
		{
			base.StreamPosition = streamPosition;
			this.currentState = ServerSessionDecoder.State.ReadingViaRecord;
		}

		// Token: 0x17001349 RID: 4937
		// (get) Token: 0x06004CDE RID: 19678 RVA: 0x0011879F File Offset: 0x0011699F
		public string Upgrade
		{
			get
			{
				if (this.currentState != ServerSessionDecoder.State.UpgradeRequest)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("FramingValueNotAvailable")));
				}
				return this.upgrade;
			}
		}

		// Token: 0x1700134A RID: 4938
		// (get) Token: 0x06004CDF RID: 19679 RVA: 0x001187CA File Offset: 0x001169CA
		public int EnvelopeSize
		{
			get
			{
				if (this.currentState < ServerSessionDecoder.State.EnvelopeStart)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("FramingValueNotAvailable")));
				}
				return this.envelopeSize;
			}
		}

		// Token: 0x06004CE0 RID: 19680 RVA: 0x001187F8 File Offset: 0x001169F8
		public int Decode(byte[] bytes, int offset, int size)
		{
			DecoderHelper.ValidateSize(size);
			int result;
			try
			{
				int num;
				switch (this.currentState)
				{
				case ServerSessionDecoder.State.ReadingViaRecord:
				{
					FramingRecordType framingRecordType = (FramingRecordType)bytes[offset];
					base.ValidateRecordType(FramingRecordType.Via, framingRecordType);
					num = 1;
					this.viaDecoder.Reset();
					this.currentState = ServerSessionDecoder.State.ReadingViaString;
					break;
				}
				case ServerSessionDecoder.State.ReadingViaString:
					num = this.viaDecoder.Decode(bytes, offset, size);
					if (this.viaDecoder.IsValueDecoded)
					{
						this.currentState = ServerSessionDecoder.State.ReadingContentTypeRecord;
					}
					break;
				case ServerSessionDecoder.State.ReadingContentTypeRecord:
				{
					FramingRecordType framingRecordType = (FramingRecordType)bytes[offset];
					if (framingRecordType == FramingRecordType.KnownEncoding)
					{
						num = 1;
						this.currentState = ServerSessionDecoder.State.ReadingContentTypeByte;
					}
					else
					{
						base.ValidateRecordType(FramingRecordType.ExtensibleEncoding, framingRecordType);
						num = 1;
						this.contentTypeDecoder.Reset();
						this.currentState = ServerSessionDecoder.State.ReadingContentTypeString;
					}
					break;
				}
				case ServerSessionDecoder.State.ReadingContentTypeString:
					num = this.contentTypeDecoder.Decode(bytes, offset, size);
					if (this.contentTypeDecoder.IsValueDecoded)
					{
						this.currentState = ServerSessionDecoder.State.PreUpgradeStart;
						this.contentType = this.contentTypeDecoder.Value;
					}
					break;
				case ServerSessionDecoder.State.ReadingContentTypeByte:
					this.contentType = ContentTypeStringDecoder.GetString((FramingEncodingType)bytes[offset]);
					num = 1;
					this.currentState = ServerSessionDecoder.State.PreUpgradeStart;
					break;
				case ServerSessionDecoder.State.PreUpgradeStart:
					num = 0;
					this.currentState = ServerSessionDecoder.State.ReadingUpgradeRecord;
					break;
				case ServerSessionDecoder.State.ReadingUpgradeRecord:
				{
					FramingRecordType framingRecordType = (FramingRecordType)bytes[offset];
					if (framingRecordType == FramingRecordType.UpgradeRequest)
					{
						num = 1;
						this.contentTypeDecoder.Reset();
						this.currentState = ServerSessionDecoder.State.ReadingUpgradeString;
					}
					else
					{
						num = 0;
						this.currentState = ServerSessionDecoder.State.ReadingPreambleEndRecord;
					}
					break;
				}
				case ServerSessionDecoder.State.ReadingUpgradeString:
					num = this.contentTypeDecoder.Decode(bytes, offset, size);
					if (this.contentTypeDecoder.IsValueDecoded)
					{
						this.currentState = ServerSessionDecoder.State.UpgradeRequest;
						this.upgrade = this.contentTypeDecoder.Value;
					}
					break;
				case ServerSessionDecoder.State.UpgradeRequest:
					num = 0;
					this.currentState = ServerSessionDecoder.State.ReadingUpgradeRecord;
					break;
				case ServerSessionDecoder.State.ReadingPreambleEndRecord:
				{
					FramingRecordType framingRecordType = (FramingRecordType)bytes[offset];
					base.ValidateRecordType(FramingRecordType.PreambleEnd, framingRecordType);
					num = 1;
					this.currentState = ServerSessionDecoder.State.Start;
					break;
				}
				case ServerSessionDecoder.State.Start:
					num = 0;
					this.currentState = ServerSessionDecoder.State.ReadingEndRecord;
					break;
				case ServerSessionDecoder.State.ReadingEnvelopeRecord:
					base.ValidateRecordType(FramingRecordType.SizedEnvelope, (FramingRecordType)bytes[offset]);
					num = 1;
					this.currentState = ServerSessionDecoder.State.ReadingEnvelopeSize;
					this.sizeDecoder.Reset();
					break;
				case ServerSessionDecoder.State.ReadingEnvelopeSize:
					num = this.sizeDecoder.Decode(bytes, offset, size);
					if (this.sizeDecoder.IsValueDecoded)
					{
						this.currentState = ServerSessionDecoder.State.EnvelopeStart;
						this.envelopeSize = this.sizeDecoder.Value;
						this.envelopeBytesNeeded = this.envelopeSize;
					}
					break;
				case ServerSessionDecoder.State.EnvelopeStart:
					num = 0;
					this.currentState = ServerSessionDecoder.State.ReadingEnvelopeBytes;
					break;
				case ServerSessionDecoder.State.ReadingEnvelopeBytes:
					num = size;
					if (num > this.envelopeBytesNeeded)
					{
						num = this.envelopeBytesNeeded;
					}
					this.envelopeBytesNeeded -= num;
					if (this.envelopeBytesNeeded == 0)
					{
						this.currentState = ServerSessionDecoder.State.EnvelopeEnd;
					}
					break;
				case ServerSessionDecoder.State.EnvelopeEnd:
					num = 0;
					this.currentState = ServerSessionDecoder.State.ReadingEndRecord;
					break;
				case ServerSessionDecoder.State.ReadingEndRecord:
				{
					FramingRecordType framingRecordType = (FramingRecordType)bytes[offset];
					if (framingRecordType == FramingRecordType.End)
					{
						num = 1;
						this.currentState = ServerSessionDecoder.State.End;
					}
					else
					{
						num = 0;
						this.currentState = ServerSessionDecoder.State.ReadingEnvelopeRecord;
					}
					break;
				}
				case ServerSessionDecoder.State.End:
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

		// Token: 0x04002FD0 RID: 12240
		private ViaStringDecoder viaDecoder;

		// Token: 0x04002FD1 RID: 12241
		private StringDecoder contentTypeDecoder;

		// Token: 0x04002FD2 RID: 12242
		private IntDecoder sizeDecoder;

		// Token: 0x04002FD3 RID: 12243
		private ServerSessionDecoder.State currentState;

		// Token: 0x04002FD4 RID: 12244
		private string contentType;

		// Token: 0x04002FD5 RID: 12245
		private int envelopeBytesNeeded;

		// Token: 0x04002FD6 RID: 12246
		private int envelopeSize;

		// Token: 0x04002FD7 RID: 12247
		private string upgrade;

		// Token: 0x02000D10 RID: 3344
		public enum State
		{
			// Token: 0x04004680 RID: 18048
			ReadingViaRecord,
			// Token: 0x04004681 RID: 18049
			ReadingViaString,
			// Token: 0x04004682 RID: 18050
			ReadingContentTypeRecord,
			// Token: 0x04004683 RID: 18051
			ReadingContentTypeString,
			// Token: 0x04004684 RID: 18052
			ReadingContentTypeByte,
			// Token: 0x04004685 RID: 18053
			PreUpgradeStart,
			// Token: 0x04004686 RID: 18054
			ReadingUpgradeRecord,
			// Token: 0x04004687 RID: 18055
			ReadingUpgradeString,
			// Token: 0x04004688 RID: 18056
			UpgradeRequest,
			// Token: 0x04004689 RID: 18057
			ReadingPreambleEndRecord,
			// Token: 0x0400468A RID: 18058
			Start,
			// Token: 0x0400468B RID: 18059
			ReadingEnvelopeRecord,
			// Token: 0x0400468C RID: 18060
			ReadingEnvelopeSize,
			// Token: 0x0400468D RID: 18061
			EnvelopeStart,
			// Token: 0x0400468E RID: 18062
			ReadingEnvelopeBytes,
			// Token: 0x0400468F RID: 18063
			EnvelopeEnd,
			// Token: 0x04004690 RID: 18064
			ReadingEndRecord,
			// Token: 0x04004691 RID: 18065
			End
		}
	}
}
