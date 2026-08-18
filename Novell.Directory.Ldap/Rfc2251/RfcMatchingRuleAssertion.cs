using System;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Rfc2251
{
	// Token: 0x020000DA RID: 218
	public class RfcMatchingRuleAssertion : Asn1Sequence
	{
		// Token: 0x06000586 RID: 1414 RVA: 0x0001A610 File Offset: 0x00019610
		public RfcMatchingRuleAssertion(RfcAssertionValue matchValue) : this(null, null, matchValue, null)
		{
		}

		// Token: 0x06000587 RID: 1415 RVA: 0x0001A628 File Offset: 0x00019628
		public RfcMatchingRuleAssertion(RfcMatchingRuleId matchingRule, RfcAttributeDescription type, RfcAssertionValue matchValue, Asn1Boolean dnAttributes) : base(4)
		{
			if (matchingRule != null)
			{
				base.add(new Asn1Tagged(new Asn1Identifier(2, false, 1), matchingRule, false));
			}
			if (type != null)
			{
				base.add(new Asn1Tagged(new Asn1Identifier(2, false, 2), type, false));
			}
			base.add(new Asn1Tagged(new Asn1Identifier(2, false, 3), matchValue, false));
			if (dnAttributes != null && dnAttributes.booleanValue())
			{
				base.add(new Asn1Tagged(new Asn1Identifier(2, false, 4), dnAttributes, false));
			}
		}
	}
}
