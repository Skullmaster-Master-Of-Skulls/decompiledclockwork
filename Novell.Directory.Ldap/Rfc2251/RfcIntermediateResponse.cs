using System;
using System.IO;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Rfc2251
{
	// Token: 0x020000D6 RID: 214
	public class RfcIntermediateResponse : Asn1Sequence, RfcResponse
	{
		// Token: 0x0600056B RID: 1387 RVA: 0x00019F64 File Offset: 0x00018F64
		[CLSCompliant(false)]
		public RfcIntermediateResponse(Asn1Decoder dec, Stream in_Renamed, int len) : base(dec, in_Renamed, len)
		{
			this.m_responseNameIndex = (this.m_responseValueIndex = 0);
			int i;
			if (base.size() >= 3)
			{
				i = 3;
			}
			else
			{
				i = 0;
			}
			while (i < base.size())
			{
				Asn1Tagged asn1Tagged = (Asn1Tagged)base.get_Renamed(i);
				Asn1Identifier identifier = asn1Tagged.getIdentifier();
				switch (identifier.Tag)
				{
				case 0:
					base.set_Renamed(i, new RfcLdapOID(((Asn1OctetString)asn1Tagged.taggedValue()).byteValue()));
					this.m_responseNameIndex = i;
					break;
				case 1:
					base.set_Renamed(i, asn1Tagged.taggedValue());
					this.m_responseValueIndex = i;
					break;
				}
				i++;
			}
		}

		// Token: 0x0600056C RID: 1388 RVA: 0x0001A010 File Offset: 0x00019010
		public Asn1Enumerated getResultCode()
		{
			Asn1Enumerated result;
			if (base.size() > 3)
			{
				result = (Asn1Enumerated)base.get_Renamed(0);
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x0600056D RID: 1389 RVA: 0x0001A03C File Offset: 0x0001903C
		public RfcLdapDN getMatchedDN()
		{
			RfcLdapDN result;
			if (base.size() > 3)
			{
				result = new RfcLdapDN(((Asn1OctetString)base.get_Renamed(1)).byteValue());
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x0600056E RID: 1390 RVA: 0x0001A070 File Offset: 0x00019070
		public RfcLdapString getErrorMessage()
		{
			RfcLdapString result;
			if (base.size() > 3)
			{
				result = new RfcLdapString(((Asn1OctetString)base.get_Renamed(2)).byteValue());
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x0600056F RID: 1391 RVA: 0x0001A0A4 File Offset: 0x000190A4
		public RfcReferral getReferral()
		{
			return (base.size() > 3) ? ((RfcReferral)base.get_Renamed(3)) : null;
		}

		// Token: 0x06000570 RID: 1392 RVA: 0x0001A0D0 File Offset: 0x000190D0
		public RfcLdapOID getResponseName()
		{
			return (this.m_responseNameIndex >= 0) ? ((RfcLdapOID)base.get_Renamed(this.m_responseNameIndex)) : null;
		}

		// Token: 0x06000571 RID: 1393 RVA: 0x0001A100 File Offset: 0x00019100
		public Asn1OctetString getResponse()
		{
			return (this.m_responseValueIndex != 0) ? ((Asn1OctetString)base.get_Renamed(this.m_responseValueIndex)) : null;
		}

		// Token: 0x06000572 RID: 1394 RVA: 0x0001A130 File Offset: 0x00019130
		public override Asn1Identifier getIdentifier()
		{
			return new Asn1Identifier(1, true, 25);
		}

		// Token: 0x0400041E RID: 1054
		public const int TAG_RESPONSE_NAME = 0;

		// Token: 0x0400041F RID: 1055
		public const int TAG_RESPONSE = 1;

		// Token: 0x04000420 RID: 1056
		private int m_referralIndex;

		// Token: 0x04000421 RID: 1057
		private int m_responseNameIndex;

		// Token: 0x04000422 RID: 1058
		private int m_responseValueIndex;
	}
}
