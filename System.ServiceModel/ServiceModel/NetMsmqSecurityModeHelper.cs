using System;

namespace System.ServiceModel
{
	// Token: 0x02000148 RID: 328
	internal static class NetMsmqSecurityModeHelper
	{
		// Token: 0x06000938 RID: 2360 RVA: 0x00024C71 File Offset: 0x00022E71
		internal static bool IsDefined(NetMsmqSecurityMode value)
		{
			return value == NetMsmqSecurityMode.Transport || value == NetMsmqSecurityMode.Message || value == NetMsmqSecurityMode.Both || value == NetMsmqSecurityMode.None;
		}

		// Token: 0x06000939 RID: 2361 RVA: 0x00024C85 File Offset: 0x00022E85
		internal static NetMsmqSecurityMode ToSecurityMode(UnifiedSecurityMode value)
		{
			if (value <= UnifiedSecurityMode.Transport)
			{
				if (value == UnifiedSecurityMode.None)
				{
					return NetMsmqSecurityMode.None;
				}
				if (value == UnifiedSecurityMode.Transport)
				{
					return NetMsmqSecurityMode.Transport;
				}
			}
			else
			{
				if (value == UnifiedSecurityMode.Message)
				{
					return NetMsmqSecurityMode.Message;
				}
				if (value == UnifiedSecurityMode.Both)
				{
					return NetMsmqSecurityMode.Both;
				}
			}
			return (NetMsmqSecurityMode)value;
		}
	}
}
