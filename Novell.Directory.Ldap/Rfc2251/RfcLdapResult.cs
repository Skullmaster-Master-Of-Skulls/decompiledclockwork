using System;
using System.IO;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Rfc2251
{
	// Token: 0x020000BD RID: 189
	public class RfcLdapResult : Asn1Sequence, RfcResponse
	{
		// Token: 0x060004E7 RID: 1255 RVA: 0x00017814 File Offset: 0x00016814
		public RfcLdapResult(Asn1Enumerated resultCode, RfcLdapDN matchedDN, RfcLdapString errorMessage) : this(resultCode, matchedDN, errorMessage, null)
		{
		}

		// Token: 0x060004E8 RID: 1256 RVA: 0x00017830 File Offset: 0x00016830
		public RfcLdapResult(Asn1Enumerated resultCode, RfcLdapDN matchedDN, RfcLdapString errorMessage, RfcReferral referral) : base(4)
		{
			base.add(resultCode);
			base.add(matchedDN);
			base.add(errorMessage);
			if (referral != null)
			{
				base.add(referral);
			}
		}

		// Token: 0x060004E9 RID: 1257 RVA: 0x00017868 File Offset: 0x00016868
		[CLSCompliant(false)]
		public RfcLdapResult(Asn1Decoder dec, Stream in_Renamed, int len) : base(dec, in_Renamed, len)
		{
			if (base.size() > 3)
			{
				Asn1Tagged asn1Tagged = (Asn1Tagged)base.get_Renamed(3);
				Asn1Identifier identifier = asn1Tagged.getIdentifier();
				if (identifier.Tag == 3)
				{
					sbyte[] array = ((Asn1OctetString)asn1Tagged.taggedValue()).byteValue();
					MemoryStream in_Renamed2 = new MemoryStream(SupportClass.ToByteArray(array));
					base.set_Renamed(3, new RfcReferral(dec, in_Renamed2, array.Length));
				}
			}
		}

		// Token: 0x060004EA RID: 1258 RVA: 0x000178D4 File Offset: 0x000168D4
		public Asn1Enumerated getResultCode()
		{
			return (Asn1Enumerated)base.get_Renamed(0);
		}

		// Token: 0x060004EB RID: 1259 RVA: 0x000178F4 File Offset: 0x000168F4
		public RfcLdapDN getMatchedDN()
		{
			return new RfcLdapDN(((Asn1OctetString)base.get_Renamed(1)).byteValue());
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x0001791C File Offset: 0x0001691C
		public RfcLdapString getErrorMessage()
		{
			return new RfcLdapString(((Asn1OctetString)base.get_Renamed(2)).byteValue());
		}

		// Token: 0x060004ED RID: 1261 RVA: 0x00017944 File Offset: 0x00016944
		public RfcReferral getReferral()
		{
			return (base.size() > 3) ? ((RfcReferral)base.get_Renamed(3)) : null;
		}

		// Token: 0x040003FA RID: 1018
		public const int REFERRAL = 3;
	}
}
