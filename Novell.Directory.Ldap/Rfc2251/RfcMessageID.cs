using System;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Rfc2251
{
	// Token: 0x020000B8 RID: 184
	internal class RfcMessageID : Asn1Integer
	{
		// Token: 0x1700013D RID: 317
		// (get) Token: 0x060004D3 RID: 1235 RVA: 0x00017620 File Offset: 0x00016620
		private static int MessageID
		{
			get
			{
				int result;
				lock (RfcMessageID.lock_Renamed)
				{
					result = ((RfcMessageID.messageID < int.MaxValue) ? (++RfcMessageID.messageID) : (RfcMessageID.messageID = 1));
				}
				return result;
			}
		}

		// Token: 0x060004D4 RID: 1236 RVA: 0x00017684 File Offset: 0x00016684
		protected internal RfcMessageID() : base(RfcMessageID.MessageID)
		{
		}

		// Token: 0x060004D5 RID: 1237 RVA: 0x0001769C File Offset: 0x0001669C
		protected internal RfcMessageID(int i) : base(i)
		{
		}

		// Token: 0x040003F8 RID: 1016
		private static int messageID = 0;

		// Token: 0x040003F9 RID: 1017
		private static object lock_Renamed = new object();
	}
}
