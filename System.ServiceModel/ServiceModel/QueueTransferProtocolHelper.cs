using System;

namespace System.ServiceModel
{
	// Token: 0x020000B5 RID: 181
	internal static class QueueTransferProtocolHelper
	{
		// Token: 0x06000308 RID: 776 RVA: 0x00011E73 File Offset: 0x00010073
		public static bool IsDefined(QueueTransferProtocol mode)
		{
			return mode >= QueueTransferProtocol.Native && mode <= QueueTransferProtocol.SrmpSecure;
		}
	}
}
