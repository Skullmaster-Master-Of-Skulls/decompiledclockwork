using System;

namespace Org.BouncyCastle.Asn1.Pkcs
{
	// Token: 0x020001B0 RID: 432
	public class PbeS2Parameters : Asn1Encodable
	{
		// Token: 0x06001064 RID: 4196 RVA: 0x0005E4A4 File Offset: 0x0005D4A4
		public static PbeS2Parameters GetInstance(object obj)
		{
			if (obj == null || obj is PbeS2Parameters)
			{
				return (PbeS2Parameters)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new PbeS2Parameters((Asn1Sequence)obj);
			}
			throw new ArgumentException("Unknown object in factory: " + obj.GetType().FullName, "obj");
		}

		// Token: 0x06001065 RID: 4197 RVA: 0x0005E4F8 File Offset: 0x0005D4F8
		public PbeS2Parameters(Asn1Sequence seq)
		{
			if (seq.Count != 2)
			{
				throw new ArgumentException("Wrong number of elements in sequence", "seq");
			}
			Asn1Sequence asn1Sequence = (Asn1Sequence)seq[0];
			if (asn1Sequence[0].Equals(PkcsObjectIdentifiers.IdPbkdf2))
			{
				this.func = new KeyDerivationFunc(PkcsObjectIdentifiers.IdPbkdf2, Pbkdf2Params.GetInstance(asn1Sequence[1]));
			}
			else
			{
				this.func = new KeyDerivationFunc(asn1Sequence);
			}
			this.scheme = new EncryptionScheme((Asn1Sequence)seq[1]);
		}

		// Token: 0x17000314 RID: 788
		// (get) Token: 0x06001066 RID: 4198 RVA: 0x0005E585 File Offset: 0x0005D585
		public KeyDerivationFunc KeyDerivationFunc
		{
			get
			{
				return this.func;
			}
		}

		// Token: 0x17000315 RID: 789
		// (get) Token: 0x06001067 RID: 4199 RVA: 0x0005E58D File Offset: 0x0005D58D
		public EncryptionScheme EncryptionScheme
		{
			get
			{
				return this.scheme;
			}
		}

		// Token: 0x06001068 RID: 4200 RVA: 0x0005E598 File Offset: 0x0005D598
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.func,
				this.scheme
			});
		}

		// Token: 0x04000C0F RID: 3087
		private readonly KeyDerivationFunc func;

		// Token: 0x04000C10 RID: 3088
		private readonly EncryptionScheme scheme;
	}
}
