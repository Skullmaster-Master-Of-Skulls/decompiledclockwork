using System;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Math;

namespace Org.BouncyCastle.Asn1.Pkcs
{
	// Token: 0x020004D1 RID: 1233
	public class MacData : Asn1Encodable
	{
		// Token: 0x06002A11 RID: 10769 RVA: 0x000FFF70 File Offset: 0x000FEF70
		public static MacData GetInstance(object obj)
		{
			if (obj is MacData)
			{
				return (MacData)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new MacData((Asn1Sequence)obj);
			}
			throw new ArgumentException("Unknown object in factory: " + obj.GetType().FullName, "obj");
		}

		// Token: 0x06002A12 RID: 10770 RVA: 0x000FFFC0 File Offset: 0x000FEFC0
		private MacData(Asn1Sequence seq)
		{
			this.digInfo = DigestInfo.GetInstance(seq[0]);
			this.salt = ((Asn1OctetString)seq[1]).GetOctets();
			if (seq.Count == 3)
			{
				this.iterationCount = ((DerInteger)seq[2]).Value;
				return;
			}
			this.iterationCount = BigInteger.One;
		}

		// Token: 0x06002A13 RID: 10771 RVA: 0x00100028 File Offset: 0x000FF028
		public MacData(DigestInfo digInfo, byte[] salt, int iterationCount)
		{
			this.digInfo = digInfo;
			this.salt = (byte[])salt.Clone();
			this.iterationCount = BigInteger.ValueOf((long)iterationCount);
		}

		// Token: 0x1700074E RID: 1870
		// (get) Token: 0x06002A14 RID: 10772 RVA: 0x00100055 File Offset: 0x000FF055
		public DigestInfo Mac
		{
			get
			{
				return this.digInfo;
			}
		}

		// Token: 0x06002A15 RID: 10773 RVA: 0x0010005D File Offset: 0x000FF05D
		public byte[] GetSalt()
		{
			return (byte[])this.salt.Clone();
		}

		// Token: 0x1700074F RID: 1871
		// (get) Token: 0x06002A16 RID: 10774 RVA: 0x0010006F File Offset: 0x000FF06F
		public BigInteger IterationCount
		{
			get
			{
				return this.iterationCount;
			}
		}

		// Token: 0x06002A17 RID: 10775 RVA: 0x00100078 File Offset: 0x000FF078
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.digInfo,
				new DerOctetString(this.salt)
			});
			if (!this.iterationCount.Equals(BigInteger.One))
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerInteger(this.iterationCount)
				});
			}
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04001D4B RID: 7499
		internal DigestInfo digInfo;

		// Token: 0x04001D4C RID: 7500
		internal byte[] salt;

		// Token: 0x04001D4D RID: 7501
		internal BigInteger iterationCount;
	}
}
