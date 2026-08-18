using System;
using System.IO;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Rfc2251
{
	// Token: 0x020000C9 RID: 201
	public class RfcBindResponse : Asn1Sequence, RfcResponse
	{
		// Token: 0x17000144 RID: 324
		// (get) Token: 0x0600050F RID: 1295 RVA: 0x00017D54 File Offset: 0x00016D54
		public virtual Asn1OctetString ServerSaslCreds
		{
			get
			{
				Asn1OctetString result;
				if (base.size() == 5)
				{
					result = (Asn1OctetString)((Asn1Tagged)base.get_Renamed(4)).taggedValue();
				}
				else
				{
					if (base.size() == 4)
					{
						Asn1Object asn1Object = base.get_Renamed(3);
						if (asn1Object is Asn1Tagged)
						{
							return (Asn1OctetString)((Asn1Tagged)asn1Object).taggedValue();
						}
					}
					result = null;
				}
				return result;
			}
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x00017DB4 File Offset: 0x00016DB4
		[CLSCompliant(false)]
		public RfcBindResponse(Asn1Decoder dec, Stream in_Renamed, int len) : base(dec, in_Renamed, len)
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

		// Token: 0x06000511 RID: 1297 RVA: 0x00017E20 File Offset: 0x00016E20
		public Asn1Enumerated getResultCode()
		{
			return (Asn1Enumerated)base.get_Renamed(0);
		}

		// Token: 0x06000512 RID: 1298 RVA: 0x00017E40 File Offset: 0x00016E40
		public RfcLdapDN getMatchedDN()
		{
			return new RfcLdapDN(((Asn1OctetString)base.get_Renamed(1)).byteValue());
		}

		// Token: 0x06000513 RID: 1299 RVA: 0x00017E68 File Offset: 0x00016E68
		public RfcLdapString getErrorMessage()
		{
			return new RfcLdapString(((Asn1OctetString)base.get_Renamed(2)).byteValue());
		}

		// Token: 0x06000514 RID: 1300 RVA: 0x00017E90 File Offset: 0x00016E90
		public RfcReferral getReferral()
		{
			if (base.size() > 3)
			{
				Asn1Object asn1Object = base.get_Renamed(3);
				if (asn1Object is RfcReferral)
				{
					return (RfcReferral)asn1Object;
				}
			}
			return null;
		}

		// Token: 0x06000515 RID: 1301 RVA: 0x00017EC4 File Offset: 0x00016EC4
		public override Asn1Identifier getIdentifier()
		{
			return new Asn1Identifier(1, true, 1);
		}
	}
}
