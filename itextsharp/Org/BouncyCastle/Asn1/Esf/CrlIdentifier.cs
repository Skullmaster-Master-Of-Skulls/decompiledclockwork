using System;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Math;

namespace Org.BouncyCastle.Asn1.Esf
{
	// Token: 0x0200040B RID: 1035
	public class CrlIdentifier : Asn1Encodable
	{
		// Token: 0x06002339 RID: 9017 RVA: 0x000D8E90 File Offset: 0x000D7E90
		public static CrlIdentifier GetInstance(object obj)
		{
			if (obj == null || obj is CrlIdentifier)
			{
				return (CrlIdentifier)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new CrlIdentifier((Asn1Sequence)obj);
			}
			throw new ArgumentException("Unknown object in 'CrlIdentifier' factory: " + obj.GetType().Name, "obj");
		}

		// Token: 0x0600233A RID: 9018 RVA: 0x000D8EE4 File Offset: 0x000D7EE4
		private CrlIdentifier(Asn1Sequence seq)
		{
			if (seq == null)
			{
				throw new ArgumentNullException("seq");
			}
			if (seq.Count < 2 || seq.Count > 3)
			{
				throw new ArgumentException("Bad sequence size: " + seq.Count, "seq");
			}
			this.crlIssuer = X509Name.GetInstance(seq[0]);
			this.crlIssuedTime = DerUtcTime.GetInstance(seq[1]);
			if (seq.Count > 2)
			{
				this.crlNumber = DerInteger.GetInstance(seq[2]);
			}
		}

		// Token: 0x0600233B RID: 9019 RVA: 0x000D8F76 File Offset: 0x000D7F76
		public CrlIdentifier(X509Name crlIssuer, DateTime crlIssuedTime) : this(crlIssuer, crlIssuedTime, null)
		{
		}

		// Token: 0x0600233C RID: 9020 RVA: 0x000D8F81 File Offset: 0x000D7F81
		public CrlIdentifier(X509Name crlIssuer, DateTime crlIssuedTime, BigInteger crlNumber)
		{
			if (crlIssuer == null)
			{
				throw new ArgumentNullException("crlIssuer");
			}
			this.crlIssuer = crlIssuer;
			this.crlIssuedTime = new DerUtcTime(crlIssuedTime);
			if (crlNumber != null)
			{
				this.crlNumber = new DerInteger(crlNumber);
			}
		}

		// Token: 0x17000603 RID: 1539
		// (get) Token: 0x0600233D RID: 9021 RVA: 0x000D8FB9 File Offset: 0x000D7FB9
		public X509Name CrlIssuer
		{
			get
			{
				return this.crlIssuer;
			}
		}

		// Token: 0x17000604 RID: 1540
		// (get) Token: 0x0600233E RID: 9022 RVA: 0x000D8FC1 File Offset: 0x000D7FC1
		public DateTime CrlIssuedTime
		{
			get
			{
				return this.crlIssuedTime.ToAdjustedDateTime();
			}
		}

		// Token: 0x17000605 RID: 1541
		// (get) Token: 0x0600233F RID: 9023 RVA: 0x000D8FCE File Offset: 0x000D7FCE
		public BigInteger CrlNumber
		{
			get
			{
				if (this.crlNumber != null)
				{
					return this.crlNumber.Value;
				}
				return null;
			}
		}

		// Token: 0x06002340 RID: 9024 RVA: 0x000D8FE8 File Offset: 0x000D7FE8
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.crlIssuer.ToAsn1Object(),
				this.crlIssuedTime
			});
			if (this.crlNumber != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					this.crlNumber
				});
			}
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x0400186E RID: 6254
		private readonly X509Name crlIssuer;

		// Token: 0x0400186F RID: 6255
		private readonly DerUtcTime crlIssuedTime;

		// Token: 0x04001870 RID: 6256
		private readonly DerInteger crlNumber;
	}
}
