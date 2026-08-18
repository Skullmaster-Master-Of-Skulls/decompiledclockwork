using System;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Rfc2251
{
	// Token: 0x020000E7 RID: 231
	public class RfcSubstringFilter : Asn1Sequence
	{
		// Token: 0x060005AD RID: 1453 RVA: 0x0001AB88 File Offset: 0x00019B88
		public RfcSubstringFilter(RfcAttributeDescription type, Asn1SequenceOf substrings) : base(2)
		{
			base.add(type);
			base.add(substrings);
		}
	}
}
