using System;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Rfc2251
{
	// Token: 0x020000C4 RID: 196
	public class RfcAttributeTypeAndValues : Asn1Sequence
	{
		// Token: 0x060004FA RID: 1274 RVA: 0x00017AB0 File Offset: 0x00016AB0
		public RfcAttributeTypeAndValues(RfcAttributeDescription type, Asn1SetOf vals) : base(2)
		{
			base.add(type);
			base.add(vals);
		}
	}
}
