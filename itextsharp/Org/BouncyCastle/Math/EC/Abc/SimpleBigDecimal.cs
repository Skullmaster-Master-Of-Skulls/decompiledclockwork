using System;
using System.Text;

namespace Org.BouncyCastle.Math.EC.Abc
{
	// Token: 0x02000610 RID: 1552
	internal class SimpleBigDecimal
	{
		// Token: 0x060034D6 RID: 13526 RVA: 0x001484E2 File Offset: 0x001474E2
		public static SimpleBigDecimal GetInstance(BigInteger val, int scale)
		{
			return new SimpleBigDecimal(val.ShiftLeft(scale), scale);
		}

		// Token: 0x060034D7 RID: 13527 RVA: 0x001484F1 File Offset: 0x001474F1
		public SimpleBigDecimal(BigInteger bigInt, int scale)
		{
			if (scale < 0)
			{
				throw new ArgumentException("scale may not be negative");
			}
			this.bigInt = bigInt;
			this.scale = scale;
		}

		// Token: 0x060034D8 RID: 13528 RVA: 0x00148516 File Offset: 0x00147516
		private SimpleBigDecimal(SimpleBigDecimal limBigDec)
		{
			this.bigInt = limBigDec.bigInt;
			this.scale = limBigDec.scale;
		}

		// Token: 0x060034D9 RID: 13529 RVA: 0x00148536 File Offset: 0x00147536
		private void CheckScale(SimpleBigDecimal b)
		{
			if (this.scale != b.scale)
			{
				throw new ArgumentException("Only SimpleBigDecimal of same scale allowed in arithmetic operations");
			}
		}

		// Token: 0x060034DA RID: 13530 RVA: 0x00148551 File Offset: 0x00147551
		public SimpleBigDecimal AdjustScale(int newScale)
		{
			if (newScale < 0)
			{
				throw new ArgumentException("scale may not be negative");
			}
			if (newScale == this.scale)
			{
				return this;
			}
			return new SimpleBigDecimal(this.bigInt.ShiftLeft(newScale - this.scale), newScale);
		}

		// Token: 0x060034DB RID: 13531 RVA: 0x00148586 File Offset: 0x00147586
		public SimpleBigDecimal Add(SimpleBigDecimal b)
		{
			this.CheckScale(b);
			return new SimpleBigDecimal(this.bigInt.Add(b.bigInt), this.scale);
		}

		// Token: 0x060034DC RID: 13532 RVA: 0x001485AB File Offset: 0x001475AB
		public SimpleBigDecimal Add(BigInteger b)
		{
			return new SimpleBigDecimal(this.bigInt.Add(b.ShiftLeft(this.scale)), this.scale);
		}

		// Token: 0x060034DD RID: 13533 RVA: 0x001485CF File Offset: 0x001475CF
		public SimpleBigDecimal Negate()
		{
			return new SimpleBigDecimal(this.bigInt.Negate(), this.scale);
		}

		// Token: 0x060034DE RID: 13534 RVA: 0x001485E7 File Offset: 0x001475E7
		public SimpleBigDecimal Subtract(SimpleBigDecimal b)
		{
			return this.Add(b.Negate());
		}

		// Token: 0x060034DF RID: 13535 RVA: 0x001485F5 File Offset: 0x001475F5
		public SimpleBigDecimal Subtract(BigInteger b)
		{
			return new SimpleBigDecimal(this.bigInt.Subtract(b.ShiftLeft(this.scale)), this.scale);
		}

		// Token: 0x060034E0 RID: 13536 RVA: 0x00148619 File Offset: 0x00147619
		public SimpleBigDecimal Multiply(SimpleBigDecimal b)
		{
			this.CheckScale(b);
			return new SimpleBigDecimal(this.bigInt.Multiply(b.bigInt), this.scale + this.scale);
		}

		// Token: 0x060034E1 RID: 13537 RVA: 0x00148645 File Offset: 0x00147645
		public SimpleBigDecimal Multiply(BigInteger b)
		{
			return new SimpleBigDecimal(this.bigInt.Multiply(b), this.scale);
		}

		// Token: 0x060034E2 RID: 13538 RVA: 0x00148660 File Offset: 0x00147660
		public SimpleBigDecimal Divide(SimpleBigDecimal b)
		{
			this.CheckScale(b);
			BigInteger bigInteger = this.bigInt.ShiftLeft(this.scale);
			return new SimpleBigDecimal(bigInteger.Divide(b.bigInt), this.scale);
		}

		// Token: 0x060034E3 RID: 13539 RVA: 0x0014869D File Offset: 0x0014769D
		public SimpleBigDecimal Divide(BigInteger b)
		{
			return new SimpleBigDecimal(this.bigInt.Divide(b), this.scale);
		}

		// Token: 0x060034E4 RID: 13540 RVA: 0x001486B6 File Offset: 0x001476B6
		public SimpleBigDecimal ShiftLeft(int n)
		{
			return new SimpleBigDecimal(this.bigInt.ShiftLeft(n), this.scale);
		}

		// Token: 0x060034E5 RID: 13541 RVA: 0x001486CF File Offset: 0x001476CF
		public int CompareTo(SimpleBigDecimal val)
		{
			this.CheckScale(val);
			return this.bigInt.CompareTo(val.bigInt);
		}

		// Token: 0x060034E6 RID: 13542 RVA: 0x001486E9 File Offset: 0x001476E9
		public int CompareTo(BigInteger val)
		{
			return this.bigInt.CompareTo(val.ShiftLeft(this.scale));
		}

		// Token: 0x060034E7 RID: 13543 RVA: 0x00148702 File Offset: 0x00147702
		public BigInteger Floor()
		{
			return this.bigInt.ShiftRight(this.scale);
		}

		// Token: 0x060034E8 RID: 13544 RVA: 0x00148718 File Offset: 0x00147718
		public BigInteger Round()
		{
			SimpleBigDecimal simpleBigDecimal = new SimpleBigDecimal(BigInteger.One, 1);
			return this.Add(simpleBigDecimal.AdjustScale(this.scale)).Floor();
		}

		// Token: 0x17000923 RID: 2339
		// (get) Token: 0x060034E9 RID: 13545 RVA: 0x00148748 File Offset: 0x00147748
		public int IntValue
		{
			get
			{
				return this.Floor().IntValue;
			}
		}

		// Token: 0x17000924 RID: 2340
		// (get) Token: 0x060034EA RID: 13546 RVA: 0x00148755 File Offset: 0x00147755
		public long LongValue
		{
			get
			{
				return this.Floor().LongValue;
			}
		}

		// Token: 0x17000925 RID: 2341
		// (get) Token: 0x060034EB RID: 13547 RVA: 0x00148762 File Offset: 0x00147762
		public int Scale
		{
			get
			{
				return this.scale;
			}
		}

		// Token: 0x060034EC RID: 13548 RVA: 0x0014876C File Offset: 0x0014776C
		public override string ToString()
		{
			if (this.scale == 0)
			{
				return this.bigInt.ToString();
			}
			BigInteger bigInteger = this.Floor();
			BigInteger bigInteger2 = this.bigInt.Subtract(bigInteger.ShiftLeft(this.scale));
			if (this.bigInt.SignValue < 0)
			{
				bigInteger2 = BigInteger.One.ShiftLeft(this.scale).Subtract(bigInteger2);
			}
			if (bigInteger.SignValue == -1 && !bigInteger2.Equals(BigInteger.Zero))
			{
				bigInteger = bigInteger.Add(BigInteger.One);
			}
			string value = bigInteger.ToString();
			char[] array = new char[this.scale];
			string text = bigInteger2.ToString(2);
			int length = text.Length;
			int num = this.scale - length;
			for (int i = 0; i < num; i++)
			{
				array[i] = '0';
			}
			for (int j = 0; j < length; j++)
			{
				array[num + j] = text[j];
			}
			string value2 = new string(array);
			StringBuilder stringBuilder = new StringBuilder(value);
			stringBuilder.Append(".");
			stringBuilder.Append(value2);
			return stringBuilder.ToString();
		}

		// Token: 0x060034ED RID: 13549 RVA: 0x0014888C File Offset: 0x0014788C
		public override bool Equals(object obj)
		{
			if (this == obj)
			{
				return true;
			}
			SimpleBigDecimal simpleBigDecimal = obj as SimpleBigDecimal;
			return simpleBigDecimal != null && this.bigInt.Equals(simpleBigDecimal.bigInt) && this.scale == simpleBigDecimal.scale;
		}

		// Token: 0x060034EE RID: 13550 RVA: 0x001488CE File Offset: 0x001478CE
		public override int GetHashCode()
		{
			return this.bigInt.GetHashCode() ^ this.scale;
		}

		// Token: 0x04002370 RID: 9072
		private readonly BigInteger bigInt;

		// Token: 0x04002371 RID: 9073
		private readonly int scale;
	}
}
