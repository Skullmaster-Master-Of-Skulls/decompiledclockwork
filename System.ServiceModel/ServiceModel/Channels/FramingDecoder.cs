using System;
using System.IO;

namespace System.ServiceModel.Channels
{
	// Token: 0x020007F0 RID: 2032
	internal abstract class FramingDecoder
	{
		// Token: 0x06004CC3 RID: 19651 RVA: 0x00118347 File Offset: 0x00116547
		protected FramingDecoder()
		{
		}

		// Token: 0x06004CC4 RID: 19652 RVA: 0x0011834F File Offset: 0x0011654F
		protected FramingDecoder(long streamPosition)
		{
			this.streamPosition = streamPosition;
		}

		// Token: 0x1700133E RID: 4926
		// (get) Token: 0x06004CC5 RID: 19653
		protected abstract string CurrentStateAsString { get; }

		// Token: 0x1700133F RID: 4927
		// (get) Token: 0x06004CC6 RID: 19654 RVA: 0x0011835E File Offset: 0x0011655E
		// (set) Token: 0x06004CC7 RID: 19655 RVA: 0x00118366 File Offset: 0x00116566
		public long StreamPosition
		{
			get
			{
				return this.streamPosition;
			}
			set
			{
				this.streamPosition = value;
			}
		}

		// Token: 0x06004CC8 RID: 19656 RVA: 0x00118370 File Offset: 0x00116570
		protected void ValidateFramingMode(FramingMode mode)
		{
			if (mode - FramingMode.Singleton > 3)
			{
				Exception exception = this.CreateException(new InvalidDataException(SR.GetString("FramingModeNotSupported", new object[]
				{
					mode.ToString()
				})), "http://schemas.microsoft.com/ws/2006/05/framing/faults/UnsupportedMode");
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(exception);
			}
		}

		// Token: 0x06004CC9 RID: 19657 RVA: 0x001183C0 File Offset: 0x001165C0
		protected void ValidateRecordType(FramingRecordType expectedType, FramingRecordType foundType)
		{
			if (foundType != expectedType)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(this.CreateInvalidRecordTypeException(expectedType, foundType));
			}
		}

		// Token: 0x06004CCA RID: 19658 RVA: 0x001183DC File Offset: 0x001165DC
		protected void ValidatePreambleAck(FramingRecordType foundType)
		{
			if (foundType != FramingRecordType.PreambleAck)
			{
				Exception innerException = this.CreateInvalidRecordTypeException(FramingRecordType.PreambleAck, foundType);
				string @string;
				if ((byte)foundType == 104 || (byte)foundType == 72)
				{
					@string = SR.GetString("PreambleAckIncorrectMaybeHttp");
				}
				else
				{
					@string = SR.GetString("PreambleAckIncorrect");
				}
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ProtocolException(@string, innerException));
			}
		}

		// Token: 0x06004CCB RID: 19659 RVA: 0x0011842E File Offset: 0x0011662E
		private Exception CreateInvalidRecordTypeException(FramingRecordType expectedType, FramingRecordType foundType)
		{
			return new InvalidDataException(SR.GetString("FramingRecordTypeMismatch", new object[]
			{
				expectedType.ToString(),
				foundType.ToString()
			}));
		}

		// Token: 0x06004CCC RID: 19660 RVA: 0x00118468 File Offset: 0x00116668
		protected void ValidateMajorVersion(int majorVersion)
		{
			if (majorVersion != 1)
			{
				Exception exception = this.CreateException(new InvalidDataException(SR.GetString("FramingVersionNotSupported", new object[]
				{
					majorVersion
				})), "http://schemas.microsoft.com/ws/2006/05/framing/faults/UnsupportedVersion");
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(exception);
			}
		}

		// Token: 0x06004CCD RID: 19661 RVA: 0x001184AF File Offset: 0x001166AF
		public Exception CreatePrematureEOFException()
		{
			return this.CreateException(new InvalidDataException(SR.GetString("FramingPrematureEOF")));
		}

		// Token: 0x06004CCE RID: 19662 RVA: 0x001184C8 File Offset: 0x001166C8
		protected Exception CreateException(InvalidDataException innerException, string framingFault)
		{
			Exception ex = this.CreateException(innerException);
			FramingEncodingString.AddFaultString(ex, framingFault);
			return ex;
		}

		// Token: 0x06004CCF RID: 19663 RVA: 0x001184E5 File Offset: 0x001166E5
		protected Exception CreateException(InvalidDataException innerException)
		{
			return new ProtocolException(SR.GetString("FramingError", new object[]
			{
				this.StreamPosition,
				this.CurrentStateAsString
			}), innerException);
		}

		// Token: 0x04002FCB RID: 12235
		private long streamPosition;
	}
}
