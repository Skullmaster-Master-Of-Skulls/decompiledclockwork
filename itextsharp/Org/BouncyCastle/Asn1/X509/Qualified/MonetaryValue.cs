using System;
using Org.BouncyCastle.Math;

namespace Org.BouncyCastle.Asn1.X509.Qualified
{
	// Token: 0x02000400 RID: 1024
	public class MonetaryValue : Asn1Encodable
	{
		// Token: 0x060022FD RID: 8957 RVA: 0x000D7BB8 File Offset: 0x000D6BB8
		public static MonetaryValue GetInstance(object obj)
		{
			if (obj == null || obj is MonetaryValue)
			{
				return (MonetaryValue)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new MonetaryValue(Asn1Sequence.GetInstance(obj));
			}
			throw new ArgumentException("unknown object in GetInstance: " + obj.GetType().FullName, "obj");
		}

		// Token: 0x060022FE RID: 8958 RVA: 0x000D7C0C File Offset: 0x000D6C0C
		private MonetaryValue(Asn1Sequence seq)
		{
			if (seq.Count != 3)
			{
				throw new ArgumentException("Bad sequence size: " + seq.Count, "seq");
			}
			this.currency = Iso4217CurrencyCode.GetInstance(seq[0]);
			this.amount = DerInteger.GetInstance(seq[1]);
			this.exponent = DerInteger.GetInstance(seq[2]);
		}

		// Token: 0x060022FF RID: 8959 RVA: 0x000D7C7E File Offset: 0x000D6C7E
		public MonetaryValue(Iso4217CurrencyCode currency, int amount, int exponent)
		{
			this.currency = currency;
			this.amount = new DerInteger(amount);
			this.exponent = new DerInteger(exponent);
		}

		// Token: 0x170005F3 RID: 1523
		// (get) Token: 0x06002300 RID: 8960 RVA: 0x000D7CA5 File Offset: 0x000D6CA5
		public Iso4217CurrencyCode Currency
		{
			get
			{
				return this.currency;
			}
		}

		// Token: 0x170005F4 RID: 1524
		// (get) Token: 0x06002301 RID: 8961 RVA: 0x000D7CAD File Offset: 0x000D6CAD
		public BigInteger Amount
		{
			get
			{
				return this.amount.Value;
			}
		}

		// Token: 0x170005F5 RID: 1525
		// (get) Token: 0x06002302 RID: 8962 RVA: 0x000D7CBA File Offset: 0x000D6CBA
		public BigInteger Exponent
		{
			get
			{
				return this.exponent.Value;
			}
		}

		// Token: 0x06002303 RID: 8963 RVA: 0x000D7CC8 File Offset: 0x000D6CC8
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.currency,
				this.amount,
				this.exponent
			});
		}

		// Token: 0x040017D6 RID: 6102
		internal Iso4217CurrencyCode currency;

		// Token: 0x040017D7 RID: 6103
		internal DerInteger amount;

		// Token: 0x040017D8 RID: 6104
		internal DerInteger exponent;
	}
}
