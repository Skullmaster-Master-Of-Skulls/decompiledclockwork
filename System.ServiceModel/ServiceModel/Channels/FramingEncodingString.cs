using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x0200080C RID: 2060
	internal static class FramingEncodingString
	{
		// Token: 0x06004D29 RID: 19753 RVA: 0x00119E71 File Offset: 0x00118071
		public static bool TryGetFaultString(Exception exception, out string framingFault)
		{
			framingFault = null;
			if (exception.Data.Contains("FramingEncodingString"))
			{
				framingFault = (exception.Data["FramingEncodingString"] as string);
				if (framingFault != null)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06004D2A RID: 19754 RVA: 0x00119EA6 File Offset: 0x001180A6
		public static void AddFaultString(Exception exception, string framingFault)
		{
			exception.Data["FramingEncodingString"] = framingFault;
		}

		// Token: 0x0400302C RID: 12332
		public const string Soap11Utf8 = "text/xml; charset=utf-8";

		// Token: 0x0400302D RID: 12333
		public const string Soap11Utf16 = "text/xml; charset=utf16";

		// Token: 0x0400302E RID: 12334
		public const string Soap11Utf16FFFE = "text/xml; charset=unicodeFFFE";

		// Token: 0x0400302F RID: 12335
		public const string Soap12Utf8 = "application/soap+xml; charset=utf-8";

		// Token: 0x04003030 RID: 12336
		public const string Soap12Utf16 = "application/soap+xml; charset=utf16";

		// Token: 0x04003031 RID: 12337
		public const string Soap12Utf16FFFE = "application/soap+xml; charset=unicodeFFFE";

		// Token: 0x04003032 RID: 12338
		public const string MTOM = "multipart/related";

		// Token: 0x04003033 RID: 12339
		public const string Binary = "application/soap+msbin1";

		// Token: 0x04003034 RID: 12340
		public const string BinarySession = "application/soap+msbinsession1";

		// Token: 0x04003035 RID: 12341
		public const string ExtendedBinaryGZip = "application/soap+msbin1+gzip";

		// Token: 0x04003036 RID: 12342
		public const string ExtendedBinarySessionGZip = "application/soap+msbinsession1+gzip";

		// Token: 0x04003037 RID: 12343
		public const string ExtendedBinaryDeflate = "application/soap+msbin1+deflate";

		// Token: 0x04003038 RID: 12344
		public const string ExtendedBinarySessionDeflate = "application/soap+msbinsession1+deflate";

		// Token: 0x04003039 RID: 12345
		public const string NamespaceUri = "http://schemas.microsoft.com/ws/2006/05/framing";

		// Token: 0x0400303A RID: 12346
		private const string FaultBaseUri = "http://schemas.microsoft.com/ws/2006/05/framing/faults/";

		// Token: 0x0400303B RID: 12347
		public const string ContentTypeInvalidFault = "http://schemas.microsoft.com/ws/2006/05/framing/faults/ContentTypeInvalid";

		// Token: 0x0400303C RID: 12348
		public const string ContentTypeTooLongFault = "http://schemas.microsoft.com/ws/2006/05/framing/faults/ContentTypeTooLong";

		// Token: 0x0400303D RID: 12349
		public const string ConnectionDispatchFailedFault = "http://schemas.microsoft.com/ws/2006/05/framing/faults/ConnectionDispatchFailed";

		// Token: 0x0400303E RID: 12350
		public const string EndpointNotFoundFault = "http://schemas.microsoft.com/ws/2006/05/framing/faults/EndpointNotFound";

		// Token: 0x0400303F RID: 12351
		public const string EndpointUnavailableFault = "http://schemas.microsoft.com/ws/2006/05/framing/faults/EndpointUnavailable";

		// Token: 0x04003040 RID: 12352
		public const string MaxMessageSizeExceededFault = "http://schemas.microsoft.com/ws/2006/05/framing/faults/MaxMessageSizeExceededFault";

		// Token: 0x04003041 RID: 12353
		public const string ServerTooBusyFault = "http://schemas.microsoft.com/ws/2006/05/framing/faults/ServerTooBusy";

		// Token: 0x04003042 RID: 12354
		public const string ServiceActivationFailedFault = "http://schemas.microsoft.com/ws/2006/05/framing/faults/ServiceActivationFailed";

		// Token: 0x04003043 RID: 12355
		public const string UnsupportedModeFault = "http://schemas.microsoft.com/ws/2006/05/framing/faults/UnsupportedMode";

		// Token: 0x04003044 RID: 12356
		public const string UnsupportedVersionFault = "http://schemas.microsoft.com/ws/2006/05/framing/faults/UnsupportedVersion";

		// Token: 0x04003045 RID: 12357
		public const string UpgradeInvalidFault = "http://schemas.microsoft.com/ws/2006/05/framing/faults/UpgradeInvalid";

		// Token: 0x04003046 RID: 12358
		public const string ViaTooLongFault = "http://schemas.microsoft.com/ws/2006/05/framing/faults/ViaTooLong";

		// Token: 0x04003047 RID: 12359
		private const string ExceptionKey = "FramingEncodingString";
	}
}
