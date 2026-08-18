using System;

namespace System.ServiceModel
{
	// Token: 0x020000B7 RID: 183
	internal class WSReliableMessaging11Version : ReliableMessagingVersion
	{
		// Token: 0x06000310 RID: 784 RVA: 0x00011ED1 File Offset: 0x000100D1
		private WSReliableMessaging11Version() : base("http://docs.oasis-open.org/ws-rx/wsrm/200702", DXD.Wsrm11Dictionary.Namespace)
		{
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000311 RID: 785 RVA: 0x00011EE8 File Offset: 0x000100E8
		internal static ReliableMessagingVersion Instance
		{
			get
			{
				return WSReliableMessaging11Version.instance;
			}
		}

		// Token: 0x06000312 RID: 786 RVA: 0x00011EEF File Offset: 0x000100EF
		public override string ToString()
		{
			return "WSReliableMessaging11";
		}

		// Token: 0x04000963 RID: 2403
		private static ReliableMessagingVersion instance = new WSReliableMessaging11Version();
	}
}
