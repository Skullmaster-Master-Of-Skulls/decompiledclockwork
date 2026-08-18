using System;
using System.IO;

namespace System.ServiceModel.Channels
{
	// Token: 0x020007F5 RID: 2037
	internal class ServerSingletonSizedDecoder : FramingDecoder
	{
		// Token: 0x06004CEF RID: 19695 RVA: 0x001190A4 File Offset: 0x001172A4
		public ServerSingletonSizedDecoder(long streamPosition, int maxViaLength, int maxContentTypeLength) : base(streamPosition)
		{
			this.viaDecoder = new ViaStringDecoder(maxViaLength);
			this.contentTypeDecoder = new ContentTypeStringDecoder(maxContentTypeLength);
			this.currentState = ServerSingletonSizedDecoder.State.ReadingViaRecord;
		}

		// Token: 0x06004CF0 RID: 19696 RVA: 0x001190CC File Offset: 0x001172CC
		public int Decode(byte[] bytes, int offset, int size)
		{
			DecoderHelper.ValidateSize(size);
			int result;
			try
			{
				int num;
				switch (this.currentState)
				{
				case ServerSingletonSizedDecoder.State.ReadingViaRecord:
				{
					FramingRecordType framingRecordType = (FramingRecordType)bytes[offset];
					base.ValidateRecordType(FramingRecordType.Via, framingRecordType);
					num = 1;
					this.viaDecoder.Reset();
					this.currentState = ServerSingletonSizedDecoder.State.ReadingViaString;
					break;
				}
				case ServerSingletonSizedDecoder.State.ReadingViaString:
					num = this.viaDecoder.Decode(bytes, offset, size);
					if (this.viaDecoder.IsValueDecoded)
					{
						this.currentState = ServerSingletonSizedDecoder.State.ReadingContentTypeRecord;
					}
					break;
				case ServerSingletonSizedDecoder.State.ReadingContentTypeRecord:
				{
					FramingRecordType framingRecordType = (FramingRecordType)bytes[offset];
					if (framingRecordType == FramingRecordType.KnownEncoding)
					{
						num = 1;
						this.currentState = ServerSingletonSizedDecoder.State.ReadingContentTypeByte;
					}
					else
					{
						base.ValidateRecordType(FramingRecordType.ExtensibleEncoding, framingRecordType);
						num = 1;
						this.contentTypeDecoder.Reset();
						this.currentState = ServerSingletonSizedDecoder.State.ReadingContentTypeString;
					}
					break;
				}
				case ServerSingletonSizedDecoder.State.ReadingContentTypeString:
					num = this.contentTypeDecoder.Decode(bytes, offset, size);
					if (this.contentTypeDecoder.IsValueDecoded)
					{
						this.currentState = ServerSingletonSizedDecoder.State.Start;
						this.contentType = this.contentTypeDecoder.Value;
					}
					break;
				case ServerSingletonSizedDecoder.State.ReadingContentTypeByte:
					this.contentType = ContentTypeStringDecoder.GetString((FramingEncodingType)bytes[offset]);
					num = 1;
					this.currentState = ServerSingletonSizedDecoder.State.Start;
					break;
				case ServerSingletonSizedDecoder.State.Start:
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

		// Token: 0x06004CF1 RID: 19697 RVA: 0x00119264 File Offset: 0x00117464
		public void Reset(long streamPosition)
		{
			base.StreamPosition = streamPosition;
			this.currentState = ServerSingletonSizedDecoder.State.ReadingViaRecord;
		}

		// Token: 0x17001353 RID: 4947
		// (get) Token: 0x06004CF2 RID: 19698 RVA: 0x00119274 File Offset: 0x00117474
		public ServerSingletonSizedDecoder.State CurrentState
		{
			get
			{
				return this.currentState;
			}
		}

		// Token: 0x17001354 RID: 4948
		// (get) Token: 0x06004CF3 RID: 19699 RVA: 0x0011927C File Offset: 0x0011747C
		protected override string CurrentStateAsString
		{
			get
			{
				return this.currentState.ToString();
			}
		}

		// Token: 0x17001355 RID: 4949
		// (get) Token: 0x06004CF4 RID: 19700 RVA: 0x0011928F File Offset: 0x0011748F
		public Uri Via
		{
			get
			{
				if (this.currentState < ServerSingletonSizedDecoder.State.ReadingContentTypeRecord)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("FramingValueNotAvailable")));
				}
				return this.viaDecoder.ValueAsUri;
			}
		}

		// Token: 0x17001356 RID: 4950
		// (get) Token: 0x06004CF5 RID: 19701 RVA: 0x001192BF File Offset: 0x001174BF
		public string ContentType
		{
			get
			{
				if (this.currentState < ServerSingletonSizedDecoder.State.Start)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("FramingValueNotAvailable")));
				}
				return this.contentType;
			}
		}

		// Token: 0x04002FE1 RID: 12257
		private ViaStringDecoder viaDecoder;

		// Token: 0x04002FE2 RID: 12258
		private ContentTypeStringDecoder contentTypeDecoder;

		// Token: 0x04002FE3 RID: 12259
		private ServerSingletonSizedDecoder.State currentState;

		// Token: 0x04002FE4 RID: 12260
		private string contentType;

		// Token: 0x02000D13 RID: 3347
		public enum State
		{
			// Token: 0x040046AD RID: 18093
			ReadingViaRecord,
			// Token: 0x040046AE RID: 18094
			ReadingViaString,
			// Token: 0x040046AF RID: 18095
			ReadingContentTypeRecord,
			// Token: 0x040046B0 RID: 18096
			ReadingContentTypeString,
			// Token: 0x040046B1 RID: 18097
			ReadingContentTypeByte,
			// Token: 0x040046B2 RID: 18098
			Start
		}
	}
}
