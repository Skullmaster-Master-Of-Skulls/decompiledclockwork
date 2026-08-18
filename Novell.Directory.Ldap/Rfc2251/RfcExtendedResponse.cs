using System;
using System.IO;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Rfc2251
{
	// Token: 0x020000D2 RID: 210
	public class RfcExtendedResponse : Asn1Sequence, RfcResponse
	{
		// Token: 0x17000149 RID: 329
		// (get) Token: 0x0600053D RID: 1341 RVA: 0x00018424 File Offset: 0x00017424
		public virtual RfcLdapOID ResponseName
		{
			get
			{
				return (this.responseNameIndex != 0) ? ((RfcLdapOID)base.get_Renamed(this.responseNameIndex)) : null;
			}
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x0600053E RID: 1342 RVA: 0x00018454 File Offset: 0x00017454
		[CLSCompliant(false)]
		public virtual Asn1OctetString Response
		{
			get
			{
				return (this.responseIndex != 0) ? ((Asn1OctetString)base.get_Renamed(this.responseIndex)) : null;
			}
		}

		// Token: 0x0600053F RID: 1343 RVA: 0x00018484 File Offset: 0x00017484
		[CLSCompliant(false)]
		public RfcExtendedResponse(Asn1Decoder dec, Stream in_Renamed, int len) : base(dec, in_Renamed, len)
		{
			if (base.size() > 3)
			{
				for (int i = 3; i < base.size(); i++)
				{
					Asn1Tagged asn1Tagged = (Asn1Tagged)base.get_Renamed(i);
					Asn1Identifier identifier = asn1Tagged.getIdentifier();
					int tag = identifier.Tag;
					if (tag != 3)
					{
						switch (tag)
						{
						case 10:
							base.set_Renamed(i, new RfcLdapOID(((Asn1OctetString)asn1Tagged.taggedValue()).byteValue()));
							this.responseNameIndex = i;
							break;
						case 11:
							base.set_Renamed(i, asn1Tagged.taggedValue());
							this.responseIndex = i;
							break;
						}
					}
					else
					{
						sbyte[] array = ((Asn1OctetString)asn1Tagged.taggedValue()).byteValue();
						MemoryStream in_Renamed2 = new MemoryStream(SupportClass.ToByteArray(array));
						base.set_Renamed(i, new RfcReferral(dec, in_Renamed2, array.Length));
						this.referralIndex = i;
					}
				}
			}
		}

		// Token: 0x06000540 RID: 1344 RVA: 0x00018568 File Offset: 0x00017568
		public Asn1Enumerated getResultCode()
		{
			return (Asn1Enumerated)base.get_Renamed(0);
		}

		// Token: 0x06000541 RID: 1345 RVA: 0x00018588 File Offset: 0x00017588
		public RfcLdapDN getMatchedDN()
		{
			return new RfcLdapDN(((Asn1OctetString)base.get_Renamed(1)).byteValue());
		}

		// Token: 0x06000542 RID: 1346 RVA: 0x000185B0 File Offset: 0x000175B0
		public RfcLdapString getErrorMessage()
		{
			return new RfcLdapString(((Asn1OctetString)base.get_Renamed(2)).byteValue());
		}

		// Token: 0x06000543 RID: 1347 RVA: 0x000185D8 File Offset: 0x000175D8
		public RfcReferral getReferral()
		{
			return (this.referralIndex != 0) ? ((RfcReferral)base.get_Renamed(this.referralIndex)) : null;
		}

		// Token: 0x06000544 RID: 1348 RVA: 0x00018608 File Offset: 0x00017608
		public override Asn1Identifier getIdentifier()
		{
			return new Asn1Identifier(1, true, 24);
		}

		// Token: 0x040003FF RID: 1023
		public const int RESPONSE_NAME = 10;

		// Token: 0x04000400 RID: 1024
		public const int RESPONSE = 11;

		// Token: 0x04000401 RID: 1025
		private int referralIndex;

		// Token: 0x04000402 RID: 1026
		private int responseNameIndex;

		// Token: 0x04000403 RID: 1027
		private int responseIndex;
	}
}
