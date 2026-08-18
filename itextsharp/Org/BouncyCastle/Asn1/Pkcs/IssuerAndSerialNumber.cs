using System;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Math;

namespace Org.BouncyCastle.Asn1.Pkcs
{
	// Token: 0x0200044B RID: 1099
	public class IssuerAndSerialNumber : Asn1Encodable
	{
		// Token: 0x06002528 RID: 9512 RVA: 0x000E18B8 File Offset: 0x000E08B8
		public static IssuerAndSerialNumber GetInstance(object obj)
		{
			if (obj is IssuerAndSerialNumber)
			{
				return (IssuerAndSerialNumber)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new IssuerAndSerialNumber((Asn1Sequence)obj);
			}
			throw new ArgumentException("Unknown object in factory: " + obj.GetType().FullName, "obj");
		}

		// Token: 0x06002529 RID: 9513 RVA: 0x000E1908 File Offset: 0x000E0908
		private IssuerAndSerialNumber(Asn1Sequence seq)
		{
			if (seq.Count != 2)
			{
				throw new ArgumentException("Wrong number of elements in sequence", "seq");
			}
			this.name = X509Name.GetInstance(seq[0]);
			this.certSerialNumber = DerInteger.GetInstance(seq[1]);
		}

		// Token: 0x0600252A RID: 9514 RVA: 0x000E1958 File Offset: 0x000E0958
		public IssuerAndSerialNumber(X509Name name, BigInteger certSerialNumber)
		{
			this.name = name;
			this.certSerialNumber = new DerInteger(certSerialNumber);
		}

		// Token: 0x0600252B RID: 9515 RVA: 0x000E1973 File Offset: 0x000E0973
		public IssuerAndSerialNumber(X509Name name, DerInteger certSerialNumber)
		{
			this.name = name;
			this.certSerialNumber = certSerialNumber;
		}

		// Token: 0x17000660 RID: 1632
		// (get) Token: 0x0600252C RID: 9516 RVA: 0x000E1989 File Offset: 0x000E0989
		public X509Name Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000661 RID: 1633
		// (get) Token: 0x0600252D RID: 9517 RVA: 0x000E1991 File Offset: 0x000E0991
		public DerInteger CertificateSerialNumber
		{
			get
			{
				return this.certSerialNumber;
			}
		}

		// Token: 0x0600252E RID: 9518 RVA: 0x000E199C File Offset: 0x000E099C
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.name,
				this.certSerialNumber
			});
		}

		// Token: 0x04001A12 RID: 6674
		private readonly X509Name name;

		// Token: 0x04001A13 RID: 6675
		private readonly DerInteger certSerialNumber;
	}
}
