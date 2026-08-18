using System;
using System.IO;

namespace System.ServiceModel.Channels
{
	// Token: 0x020007EE RID: 2030
	internal class FaultStringDecoder : StringDecoder
	{
		// Token: 0x06004CBD RID: 19645 RVA: 0x00118036 File Offset: 0x00116236
		public FaultStringDecoder() : base(256)
		{
		}

		// Token: 0x06004CBE RID: 19646 RVA: 0x00118043 File Offset: 0x00116243
		protected override Exception OnSizeQuotaExceeded(int size)
		{
			return new InvalidDataException(SR.GetString("FramingFaultTooLong", new object[]
			{
				size
			}));
		}

		// Token: 0x06004CBF RID: 19647 RVA: 0x00118064 File Offset: 0x00116264
		public static Exception GetFaultException(string faultString, string via, string contentType)
		{
			if (faultString == "http://schemas.microsoft.com/ws/2006/05/framing/faults/EndpointNotFound")
			{
				return new EndpointNotFoundException(SR.GetString("EndpointNotFound", new object[]
				{
					via
				}));
			}
			if (faultString == "http://schemas.microsoft.com/ws/2006/05/framing/faults/ContentTypeInvalid")
			{
				return new ProtocolException(SR.GetString("FramingContentTypeMismatch", new object[]
				{
					contentType,
					via
				}));
			}
			if (faultString == "http://schemas.microsoft.com/ws/2006/05/framing/faults/ServiceActivationFailed")
			{
				return new ServiceActivationException(SR.GetString("Hosting_ServiceActivationFailed", new object[]
				{
					via
				}));
			}
			if (faultString == "http://schemas.microsoft.com/ws/2006/05/framing/faults/ConnectionDispatchFailed")
			{
				return new CommunicationException(SR.GetString("Sharing_ConnectionDispatchFailed", new object[]
				{
					via
				}));
			}
			if (faultString == "http://schemas.microsoft.com/ws/2006/05/framing/faults/EndpointUnavailable")
			{
				return new EndpointNotFoundException(SR.GetString("Sharing_EndpointUnavailable", new object[]
				{
					via
				}));
			}
			if (faultString == "http://schemas.microsoft.com/ws/2006/05/framing/faults/MaxMessageSizeExceededFault")
			{
				Exception ex = new QuotaExceededException(SR.GetString("FramingMaxMessageSizeExceeded"));
				return new CommunicationException(ex.Message, ex);
			}
			if (faultString == "http://schemas.microsoft.com/ws/2006/05/framing/faults/UnsupportedMode")
			{
				return new ProtocolException(SR.GetString("FramingModeNotSupportedFault", new object[]
				{
					via
				}));
			}
			if (faultString == "http://schemas.microsoft.com/ws/2006/05/framing/faults/UnsupportedVersion")
			{
				return new ProtocolException(SR.GetString("FramingVersionNotSupportedFault", new object[]
				{
					via
				}));
			}
			if (faultString == "http://schemas.microsoft.com/ws/2006/05/framing/faults/ContentTypeTooLong")
			{
				Exception ex2 = new QuotaExceededException(SR.GetString("FramingContentTypeTooLongFault", new object[]
				{
					contentType
				}));
				return new CommunicationException(ex2.Message, ex2);
			}
			if (faultString == "http://schemas.microsoft.com/ws/2006/05/framing/faults/ViaTooLong")
			{
				Exception ex3 = new QuotaExceededException(SR.GetString("FramingViaTooLongFault", new object[]
				{
					via
				}));
				return new CommunicationException(ex3.Message, ex3);
			}
			if (faultString == "http://schemas.microsoft.com/ws/2006/05/framing/faults/ServerTooBusy")
			{
				return new ServerTooBusyException(SR.GetString("ServerTooBusy", new object[]
				{
					via
				}));
			}
			if (faultString == "http://schemas.microsoft.com/ws/2006/05/framing/faults/UpgradeInvalid")
			{
				return new ProtocolException(SR.GetString("FramingUpgradeInvalid", new object[]
				{
					via
				}));
			}
			return new ProtocolException(SR.GetString("FramingFaultUnrecognized", new object[]
			{
				faultString
			}));
		}

		// Token: 0x04002FCA RID: 12234
		internal const int FaultSizeQuota = 256;
	}
}
