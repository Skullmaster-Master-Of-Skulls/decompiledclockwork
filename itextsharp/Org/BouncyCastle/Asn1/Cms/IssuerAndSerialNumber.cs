using System;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Math;

namespace Org.BouncyCastle.Asn1.Cms
{
	// Token: 0x020002BE RID: 702
	public class IssuerAndSerialNumber : Asn1Encodable
	{
		// Token: 0x06001A6C RID: 6764 RVA: 0x0009BFD4 File Offset: 0x0009AFD4
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
			throw new ArgumentException("Illegal object in IssuerAndSerialNumber: " + obj.GetType().Name);
		}

		// Token: 0x06001A6D RID: 6765 RVA: 0x0009C013 File Offset: 0x0009B013
		public IssuerAndSerialNumber(Asn1Sequence seq)
		{
			this.name = X509Name.GetInstance(seq[0]);
			this.serialNumber = (DerInteger)seq[1];
		}

		// Token: 0x06001A6E RID: 6766 RVA: 0x0009C03F File Offset: 0x0009B03F
		public IssuerAndSerialNumber(X509Name name, BigInteger serialNumber)
		{
			this.name = name;
			this.serialNumber = new DerInteger(serialNumber);
		}

		// Token: 0x06001A6F RID: 6767 RVA: 0x0009C05A File Offset: 0x0009B05A
		public IssuerAndSerialNumber(X509Name name, DerInteger serialNumber)
		{
			this.name = name;
			this.serialNumber = serialNumber;
		}

		// Token: 0x170004C7 RID: 1223
		// (get) Token: 0x06001A70 RID: 6768 RVA: 0x0009C070 File Offset: 0x0009B070
		public X509Name Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170004C8 RID: 1224
		// (get) Token: 0x06001A71 RID: 6769 RVA: 0x0009C078 File Offset: 0x0009B078
		public DerInteger SerialNumber
		{
			get
			{
				return this.serialNumber;
			}
		}

		// Token: 0x06001A72 RID: 6770 RVA: 0x0009C080 File Offset: 0x0009B080
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.name,
				this.serialNumber
			});
		}

		// Token: 0x040011A4 RID: 4516
		private X509Name name;

		// Token: 0x040011A5 RID: 4517
		private DerInteger serialNumber;
	}
}
