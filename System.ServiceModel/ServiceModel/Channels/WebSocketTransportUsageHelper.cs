using System;
using System.ComponentModel;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000887 RID: 2183
	internal static class WebSocketTransportUsageHelper
	{
		// Token: 0x060052D9 RID: 21209 RVA: 0x001314A8 File Offset: 0x0012F6A8
		internal static bool IsDefined(WebSocketTransportUsage value)
		{
			return value == WebSocketTransportUsage.WhenDuplex || value == WebSocketTransportUsage.Never || value == WebSocketTransportUsage.Always;
		}

		// Token: 0x060052DA RID: 21210 RVA: 0x001314B7 File Offset: 0x0012F6B7
		internal static void Validate(WebSocketTransportUsage value)
		{
			if (!WebSocketTransportUsageHelper.IsDefined(value))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidEnumArgumentException("value", (int)value, typeof(WebSocketTransportUsage)));
			}
		}
	}
}
