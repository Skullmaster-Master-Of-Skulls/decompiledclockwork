using System;
using Org.BouncyCastle.Math;

namespace Org.BouncyCastle.Asn1.IsisMtt.X509
{
	// Token: 0x0200048C RID: 1164
	public class MonetaryLimit : Asn1Encodable
	{
		// Token: 0x06002768 RID: 10088 RVA: 0x000EDC38 File Offset: 0x000ECC38
		public static MonetaryLimit GetInstance(object obj)
		{
			if (obj == null || obj is MonetaryLimit)
			{
				return (MonetaryLimit)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new MonetaryLimit(Asn1Sequence.GetInstance(obj));
			}
			throw new ArgumentException("unknown object in factory: " + obj.GetType().Name, "obj");
		}

		// Token: 0x06002769 RID: 10089 RVA: 0x000EDC8C File Offset: 0x000ECC8C
		private MonetaryLimit(Asn1Sequence seq)
		{
			if (seq.Count != 3)
			{
				throw new ArgumentException("Bad sequence size: " + seq.Count);
			}
			this.currency = DerPrintableString.GetInstance(seq[0]);
			this.amount = DerInteger.GetInstance(seq[1]);
			this.exponent = DerInteger.GetInstance(seq[2]);
		}

		// Token: 0x0600276A RID: 10090 RVA: 0x000EDCF9 File Offset: 0x000ECCF9
		public MonetaryLimit(string currency, int amount, int exponent)
		{
			this.currency = new DerPrintableString(currency, true);
			this.amount = new DerInteger(amount);
			this.exponent = new DerInteger(exponent);
		}

		// Token: 0x170006CA RID: 1738
		// (get) Token: 0x0600276B RID: 10091 RVA: 0x000EDD26 File Offset: 0x000ECD26
		public virtual string Currency
		{
			get
			{
				return this.currency.GetString();
			}
		}

		// Token: 0x170006CB RID: 1739
		// (get) Token: 0x0600276C RID: 10092 RVA: 0x000EDD33 File Offset: 0x000ECD33
		public virtual BigInteger Amount
		{
			get
			{
				return this.amount.Value;
			}
		}

		// Token: 0x170006CC RID: 1740
		// (get) Token: 0x0600276D RID: 10093 RVA: 0x000EDD40 File Offset: 0x000ECD40
		public virtual BigInteger Exponent
		{
			get
			{
				return this.exponent.Value;
			}
		}

		// Token: 0x0600276E RID: 10094 RVA: 0x000EDD50 File Offset: 0x000ECD50
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.currency,
				this.amount,
				this.exponent
			});
		}

		// Token: 0x04001B25 RID: 6949
		private readonly DerPrintableString currency;

		// Token: 0x04001B26 RID: 6950
		private readonly DerInteger amount;

		// Token: 0x04001B27 RID: 6951
		private readonly DerInteger exponent;
	}
}
