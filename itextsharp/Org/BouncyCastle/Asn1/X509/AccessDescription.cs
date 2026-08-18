using System;

namespace Org.BouncyCastle.Asn1.X509
{
	// Token: 0x02000517 RID: 1303
	public class AccessDescription : Asn1Encodable
	{
		// Token: 0x06002C8C RID: 11404 RVA: 0x0010F098 File Offset: 0x0010E098
		public static AccessDescription GetInstance(object obj)
		{
			if (obj is AccessDescription)
			{
				return (AccessDescription)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new AccessDescription((Asn1Sequence)obj);
			}
			throw new ArgumentException("unknown object in factory: " + obj.GetType().Name, "obj");
		}

		// Token: 0x06002C8D RID: 11405 RVA: 0x0010F0E7 File Offset: 0x0010E0E7
		private AccessDescription(Asn1Sequence seq)
		{
			if (seq.Count != 2)
			{
				throw new ArgumentException("wrong number of elements in sequence");
			}
			this.accessMethod = DerObjectIdentifier.GetInstance(seq[0]);
			this.accessLocation = GeneralName.GetInstance(seq[1]);
		}

		// Token: 0x06002C8E RID: 11406 RVA: 0x0010F127 File Offset: 0x0010E127
		public AccessDescription(DerObjectIdentifier oid, GeneralName location)
		{
			this.accessMethod = oid;
			this.accessLocation = location;
		}

		// Token: 0x170007A8 RID: 1960
		// (get) Token: 0x06002C8F RID: 11407 RVA: 0x0010F13D File Offset: 0x0010E13D
		public DerObjectIdentifier AccessMethod
		{
			get
			{
				return this.accessMethod;
			}
		}

		// Token: 0x170007A9 RID: 1961
		// (get) Token: 0x06002C90 RID: 11408 RVA: 0x0010F145 File Offset: 0x0010E145
		public GeneralName AccessLocation
		{
			get
			{
				return this.accessLocation;
			}
		}

		// Token: 0x06002C91 RID: 11409 RVA: 0x0010F150 File Offset: 0x0010E150
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.accessMethod,
				this.accessLocation
			});
		}

		// Token: 0x06002C92 RID: 11410 RVA: 0x0010F17C File Offset: 0x0010E17C
		public override string ToString()
		{
			return "AccessDescription: Oid(" + this.accessMethod.Id + ")";
		}

		// Token: 0x04001EA7 RID: 7847
		public static readonly DerObjectIdentifier IdADCAIssuers = new DerObjectIdentifier("1.3.6.1.5.5.7.48.2");

		// Token: 0x04001EA8 RID: 7848
		public static readonly DerObjectIdentifier IdADOcsp = new DerObjectIdentifier("1.3.6.1.5.5.7.48.1");

		// Token: 0x04001EA9 RID: 7849
		private readonly DerObjectIdentifier accessMethod;

		// Token: 0x04001EAA RID: 7850
		private readonly GeneralName accessLocation;
	}
}
