using System;
using Novell.Directory.Ldap.Rfc2251;

namespace Novell.Directory.Ldap
{
	// Token: 0x02000027 RID: 39
	public class LdapCompareRequest : LdapMessage
	{
		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000168 RID: 360 RVA: 0x00007D04 File Offset: 0x00006D04
		public virtual string AttributeDescription
		{
			get
			{
				RfcCompareRequest rfcCompareRequest = (RfcCompareRequest)this.Asn1Object.getRequest();
				return rfcCompareRequest.AttributeValueAssertion.AttributeDescription;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000169 RID: 361 RVA: 0x00007D34 File Offset: 0x00006D34
		[CLSCompliant(false)]
		public virtual sbyte[] AssertionValue
		{
			get
			{
				RfcCompareRequest rfcCompareRequest = (RfcCompareRequest)this.Asn1Object.getRequest();
				return rfcCompareRequest.AttributeValueAssertion.AssertionValue;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x0600016A RID: 362 RVA: 0x00007D64 File Offset: 0x00006D64
		public virtual string DN
		{
			get
			{
				return this.Asn1Object.RequestDN;
			}
		}

		// Token: 0x0600016B RID: 363 RVA: 0x00007D80 File Offset: 0x00006D80
		[CLSCompliant(false)]
		public LdapCompareRequest(string dn, string name, sbyte[] value_Renamed, LdapControl[] cont) : base(14, new RfcCompareRequest(new RfcLdapDN(dn), new RfcAttributeValueAssertion(new RfcAttributeDescription(name), new RfcAssertionValue(value_Renamed))), cont)
		{
		}
	}
}
