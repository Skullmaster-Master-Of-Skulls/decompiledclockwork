using System;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Events.Edir.EventData
{
	// Token: 0x02000077 RID: 119
	public class ReferralAddress
	{
		// Token: 0x17000119 RID: 281
		// (get) Token: 0x06000422 RID: 1058 RVA: 0x0001418C File Offset: 0x0001318C
		public int AddressType
		{
			get
			{
				return this.address_type;
			}
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x06000423 RID: 1059 RVA: 0x000141A4 File Offset: 0x000131A4
		public string Address
		{
			get
			{
				return this.strAddress;
			}
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x000141BC File Offset: 0x000131BC
		public ReferralAddress(Asn1Sequence dseObject)
		{
			this.address_type = ((Asn1Integer)dseObject.get_Renamed(0)).intValue();
			this.strAddress = ((Asn1OctetString)dseObject.get_Renamed(1)).stringValue();
		}

		// Token: 0x04000205 RID: 517
		protected int address_type;

		// Token: 0x04000206 RID: 518
		protected string strAddress;
	}
}
