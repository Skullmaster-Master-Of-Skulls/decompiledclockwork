using System;
using System.IO;

namespace System.ServiceModel.Channels
{
	// Token: 0x020007F4 RID: 2036
	internal class ServerSingletonDecoder : FramingDecoder
	{
		// Token: 0x06004CE7 RID: 19687 RVA: 0x00118D50 File Offset: 0x00116F50
		public ServerSingletonDecoder(long streamPosition, int maxViaLength, int maxContentTypeLength) : base(streamPosition)
		{
			this.viaDecoder = new ViaStringDecoder(maxViaLength);
			this.contentTypeDecoder = new ContentTypeStringDecoder(maxContentTypeLength);
			this.currentState = ServerSingletonDecoder.State.ReadingViaRecord;
		}

		// Token: 0x06004CE8 RID: 19688 RVA: 0x00118D78 File Offset: 0x00116F78
		public void Reset()
		{
			this.currentState = ServerSingletonDecoder.State.ReadingViaRecord;
		}

		// Token: 0x1700134E RID: 4942
		// (get) Token: 0x06004CE9 RID: 19689 RVA: 0x00118D81 File Offset: 0x00116F81
		public ServerSingletonDecoder.State CurrentState
		{
			get
			{
				return this.currentState;
			}
		}

		// Token: 0x1700134F RID: 4943
		// (get) Token: 0x06004CEA RID: 19690 RVA: 0x00118D89 File Offset: 0x00116F89
		protected override string CurrentStateAsString
		{
			get
			{
				return this.currentState.ToString();
			}
		}

		// Token: 0x17001350 RID: 4944
		// (get) Token: 0x06004CEB RID: 19691 RVA: 0x00118D9C File Offset: 0x00116F9C
		public Uri Via
		{
			get
			{
				if (this.currentState < ServerSingletonDecoder.State.ReadingContentTypeRecord)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("FramingValueNotAvailable")));
				}
				return this.viaDecoder.ValueAsUri;
			}
		}

		// Token: 0x17001351 RID: 4945
		// (get) Token: 0x06004CEC RID: 19692 RVA: 0x00118DCC File Offset: 0x00116FCC
		public string ContentType
		{
			get
			{
				if (this.currentState < ServerSingletonDecoder.State.PreUpgradeStart)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("FramingValueNotAvailable")));
				}
				return this.contentType;
			}
		}

		// Token: 0x17001352 RID: 4946
		// (get) Token: 0x06004CED RID: 19693 RVA: 0x00118DF7 File Offset: 0x00116FF7
		public string Upgrade
		{
			get
			{
				if (this.currentState != ServerSingletonDecoder.State.UpgradeRequest)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("FramingValueNotAvailable")));
				}
				return this.upgrade;
			}
		}

		// Token: 0x06004CEE RID: 19694 RVA: 0x00118E24 File Offset: 0x00117024
		public int Decode(byte[] bytes, int offset, int size)
		{
			DecoderHelper.ValidateSize(size);
			int result;
			try
			{
				int num;
				switch (this.currentState)
				{
				case ServerSingletonDecoder.State.ReadingViaRecord:
				{
					FramingRecordType framingRecordType = (FramingRecordType)bytes[offset];
					base.ValidateRecordType(FramingRecordType.Via, framingRecordType);
					num = 1;
					this.viaDecoder.Reset();
					this.currentState = ServerSingletonDecoder.State.ReadingViaString;
					break;
				}
				case ServerSingletonDecoder.State.ReadingViaString:
					num = this.viaDecoder.Decode(bytes, offset, size);
					if (this.viaDecoder.IsValueDecoded)
					{
						this.currentState = ServerSingletonDecoder.State.ReadingContentTypeRecord;
					}
					break;
				case ServerSingletonDecoder.State.ReadingContentTypeRecord:
				{
					FramingRecordType framingRecordType = (FramingRecordType)bytes[offset];
					if (framingRecordType == FramingRecordType.KnownEncoding)
					{
						num = 1;
						this.currentState = ServerSingletonDecoder.State.ReadingContentTypeByte;
					}
					else
					{
						base.ValidateRecordType(FramingRecordType.ExtensibleEncoding, framingRecordType);
						num = 1;
						this.contentTypeDecoder.Reset();
						this.currentState = ServerSingletonDecoder.State.ReadingContentTypeString;
					}
					break;
				}
				case ServerSingletonDecoder.State.ReadingContentTypeString:
					num = this.contentTypeDecoder.Decode(bytes, offset, size);
					if (this.contentTypeDecoder.IsValueDecoded)
					{
						this.currentState = ServerSingletonDecoder.State.PreUpgradeStart;
						this.contentType = this.contentTypeDecoder.Value;
					}
					break;
				case ServerSingletonDecoder.State.ReadingContentTypeByte:
					this.contentType = ContentTypeStringDecoder.GetString((FramingEncodingType)bytes[offset]);
					num = 1;
					this.currentState = ServerSingletonDecoder.State.PreUpgradeStart;
					break;
				case ServerSingletonDecoder.State.PreUpgradeStart:
					num = 0;
					this.currentState = ServerSingletonDecoder.State.ReadingUpgradeRecord;
					break;
				case ServerSingletonDecoder.State.ReadingUpgradeRecord:
				{
					FramingRecordType framingRecordType = (FramingRecordType)bytes[offset];
					if (framingRecordType == FramingRecordType.UpgradeRequest)
					{
						num = 1;
						this.contentTypeDecoder.Reset();
						this.currentState = ServerSingletonDecoder.State.ReadingUpgradeString;
					}
					else
					{
						num = 0;
						this.currentState = ServerSingletonDecoder.State.ReadingPreambleEndRecord;
					}
					break;
				}
				case ServerSingletonDecoder.State.ReadingUpgradeString:
					num = this.contentTypeDecoder.Decode(bytes, offset, size);
					if (this.contentTypeDecoder.IsValueDecoded)
					{
						this.currentState = ServerSingletonDecoder.State.UpgradeRequest;
						this.upgrade = this.contentTypeDecoder.Value;
					}
					break;
				case ServerSingletonDecoder.State.UpgradeRequest:
					num = 0;
					this.currentState = ServerSingletonDecoder.State.ReadingUpgradeRecord;
					break;
				case ServerSingletonDecoder.State.ReadingPreambleEndRecord:
				{
					FramingRecordType framingRecordType = (FramingRecordType)bytes[offset];
					base.ValidateRecordType(FramingRecordType.PreambleEnd, framingRecordType);
					num = 1;
					this.currentState = ServerSingletonDecoder.State.Start;
					break;
				}
				case ServerSingletonDecoder.State.Start:
					num = 0;
					this.currentState = ServerSingletonDecoder.State.ReadingEnvelopeRecord;
					break;
				case ServerSingletonDecoder.State.ReadingEnvelopeRecord:
					base.ValidateRecordType(FramingRecordType.UnsizedEnvelope, (FramingRecordType)bytes[offset]);
					num = 1;
					this.currentState = ServerSingletonDecoder.State.EnvelopeStart;
					break;
				case ServerSingletonDecoder.State.EnvelopeStart:
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

		// Token: 0x04002FDC RID: 12252
		private ViaStringDecoder viaDecoder;

		// Token: 0x04002FDD RID: 12253
		private ContentTypeStringDecoder contentTypeDecoder;

		// Token: 0x04002FDE RID: 12254
		private ServerSingletonDecoder.State currentState;

		// Token: 0x04002FDF RID: 12255
		private string contentType;

		// Token: 0x04002FE0 RID: 12256
		private string upgrade;

		// Token: 0x02000D12 RID: 3346
		public enum State
		{
			// Token: 0x0400469A RID: 18074
			ReadingViaRecord,
			// Token: 0x0400469B RID: 18075
			ReadingViaString,
			// Token: 0x0400469C RID: 18076
			ReadingContentTypeRecord,
			// Token: 0x0400469D RID: 18077
			ReadingContentTypeString,
			// Token: 0x0400469E RID: 18078
			ReadingContentTypeByte,
			// Token: 0x0400469F RID: 18079
			PreUpgradeStart,
			// Token: 0x040046A0 RID: 18080
			ReadingUpgradeRecord,
			// Token: 0x040046A1 RID: 18081
			ReadingUpgradeString,
			// Token: 0x040046A2 RID: 18082
			UpgradeRequest,
			// Token: 0x040046A3 RID: 18083
			ReadingPreambleEndRecord,
			// Token: 0x040046A4 RID: 18084
			Start,
			// Token: 0x040046A5 RID: 18085
			ReadingEnvelopeRecord,
			// Token: 0x040046A6 RID: 18086
			EnvelopeStart,
			// Token: 0x040046A7 RID: 18087
			ReadingEnvelopeChunkSize,
			// Token: 0x040046A8 RID: 18088
			ChunkStart,
			// Token: 0x040046A9 RID: 18089
			ReadingEnvelopeChunk,
			// Token: 0x040046AA RID: 18090
			ChunkEnd,
			// Token: 0x040046AB RID: 18091
			End
		}
	}
}
