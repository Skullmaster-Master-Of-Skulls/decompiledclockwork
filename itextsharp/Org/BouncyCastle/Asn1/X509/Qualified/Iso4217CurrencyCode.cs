using System;

namespace Org.BouncyCastle.Asn1.X509.Qualified
{
	// Token: 0x020001A7 RID: 423
	public class Iso4217CurrencyCode : Asn1Encodable, IAsn1Choice
	{
		// Token: 0x06001029 RID: 4137 RVA: 0x0005D82C File Offset: 0x0005C82C
		public static Iso4217CurrencyCode GetInstance(object obj)
		{
			if (obj == null || obj is Iso4217CurrencyCode)
			{
				return (Iso4217CurrencyCode)obj;
			}
			if (obj is DerInteger)
			{
				DerInteger instance = DerInteger.GetInstance(obj);
				int intValue = instance.Value.IntValue;
				return new Iso4217CurrencyCode(intValue);
			}
			if (obj is DerPrintableString)
			{
				DerPrintableString instance2 = DerPrintableString.GetInstance(obj);
				return new Iso4217CurrencyCode(instance2.GetString());
			}
			throw new ArgumentException("unknown object in GetInstance: " + obj.GetType().FullName, "obj");
		}

		// Token: 0x0600102A RID: 4138 RVA: 0x0005D8A8 File Offset: 0x0005C8A8
		public Iso4217CurrencyCode(int numeric)
		{
			if (numeric > 999 || numeric < 1)
			{
				throw new ArgumentException(string.Concat(new object[]
				{
					"wrong size in numeric code : not in (",
					1,
					"..",
					999,
					")"
				}));
			}
			this.obj = new DerInteger(numeric);
		}

		// Token: 0x0600102B RID: 4139 RVA: 0x0005D914 File Offset: 0x0005C914
		public Iso4217CurrencyCode(string alphabetic)
		{
			if (alphabetic.Length > 3)
			{
				throw new ArgumentException("wrong size in alphabetic code : max size is " + 3);
			}
			this.obj = new DerPrintableString(alphabetic);
		}

		// Token: 0x17000300 RID: 768
		// (get) Token: 0x0600102C RID: 4140 RVA: 0x0005D947 File Offset: 0x0005C947
		public bool IsAlphabetic
		{
			get
			{
				return this.obj is DerPrintableString;
			}
		}

		// Token: 0x17000301 RID: 769
		// (get) Token: 0x0600102D RID: 4141 RVA: 0x0005D957 File Offset: 0x0005C957
		public string Alphabetic
		{
			get
			{
				return ((DerPrintableString)this.obj).GetString();
			}
		}

		// Token: 0x17000302 RID: 770
		// (get) Token: 0x0600102E RID: 4142 RVA: 0x0005D969 File Offset: 0x0005C969
		public int Numeric
		{
			get
			{
				return ((DerInteger)this.obj).Value.IntValue;
			}
		}

		// Token: 0x0600102F RID: 4143 RVA: 0x0005D980 File Offset: 0x0005C980
		public override Asn1Object ToAsn1Object()
		{
			return this.obj.ToAsn1Object();
		}

		// Token: 0x04000BE2 RID: 3042
		internal const int AlphabeticMaxSize = 3;

		// Token: 0x04000BE3 RID: 3043
		internal const int NumericMinSize = 1;

		// Token: 0x04000BE4 RID: 3044
		internal const int NumericMaxSize = 999;

		// Token: 0x04000BE5 RID: 3045
		internal Asn1Encodable obj;
	}
}
