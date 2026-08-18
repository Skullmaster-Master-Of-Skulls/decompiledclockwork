using System;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Rfc2251
{
	// Token: 0x020000C5 RID: 197
	public class RfcAttributeValue : Asn1OctetString
	{
		// Token: 0x060004FB RID: 1275 RVA: 0x00017AD4 File Offset: 0x00016AD4
		public RfcAttributeValue(string value_Renamed) : base(value_Renamed)
		{
		}

		// Token: 0x060004FC RID: 1276 RVA: 0x00017AE8 File Offset: 0x00016AE8
		[CLSCompliant(false)]
		public RfcAttributeValue(sbyte[] value_Renamed) : base(value_Renamed)
		{
		}
	}
}
