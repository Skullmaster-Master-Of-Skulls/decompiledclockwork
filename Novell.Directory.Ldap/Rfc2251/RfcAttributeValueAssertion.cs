using System;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Rfc2251
{
	// Token: 0x020000C6 RID: 198
	public class RfcAttributeValueAssertion : Asn1Sequence
	{
		// Token: 0x1700013F RID: 319
		// (get) Token: 0x060004FD RID: 1277 RVA: 0x00017AFC File Offset: 0x00016AFC
		public virtual string AttributeDescription
		{
			get
			{
				return ((RfcAttributeDescription)base.get_Renamed(0)).stringValue();
			}
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x060004FE RID: 1278 RVA: 0x00017B20 File Offset: 0x00016B20
		[CLSCompliant(false)]
		public virtual sbyte[] AssertionValue
		{
			get
			{
				return ((RfcAssertionValue)base.get_Renamed(1)).byteValue();
			}
		}

		// Token: 0x060004FF RID: 1279 RVA: 0x00017B44 File Offset: 0x00016B44
		public RfcAttributeValueAssertion(RfcAttributeDescription ad, RfcAssertionValue av) : base(2)
		{
			base.add(ad);
			base.add(av);
		}
	}
}
