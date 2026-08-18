using System;

namespace System.ServiceModel
{
	// Token: 0x020000B8 RID: 184
	internal class WSReliableMessagingFebruary2005Version : ReliableMessagingVersion
	{
		// Token: 0x06000314 RID: 788 RVA: 0x00011F02 File Offset: 0x00010102
		private WSReliableMessagingFebruary2005Version() : base("http://schemas.xmlsoap.org/ws/2005/02/rm", XD.WsrmFeb2005Dictionary.Namespace)
		{
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000315 RID: 789 RVA: 0x00011F19 File Offset: 0x00010119
		internal static ReliableMessagingVersion Instance
		{
			get
			{
				return WSReliableMessagingFebruary2005Version.instance;
			}
		}

		// Token: 0x06000316 RID: 790 RVA: 0x00011F20 File Offset: 0x00010120
		public override string ToString()
		{
			return "WSReliableMessagingFebruary2005";
		}

		// Token: 0x04000964 RID: 2404
		private static ReliableMessagingVersion instance = new WSReliableMessagingFebruary2005Version();
	}
}
