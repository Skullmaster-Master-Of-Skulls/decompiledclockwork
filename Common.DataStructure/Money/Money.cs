using System;
using System.Globalization;

namespace TechnoPro.Common.DataStructure.Money
{
	// Token: 0x0200001B RID: 27
	public sealed class Money : IEquatable<Money>, IComparable, IComparable<Money>
	{
		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x00004A43 File Offset: 0x00002C43
		// (set) Token: 0x060000A1 RID: 161 RVA: 0x00004A4B File Offset: 0x00002C4B
		public CurrencyCodeKind CurrencyCode { get; private set; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x00004A54 File Offset: 0x00002C54
		// (set) Token: 0x060000A3 RID: 163 RVA: 0x00004A5C File Offset: 0x00002C5C
		public decimal Amount { get; private set; }

		// Token: 0x060000A4 RID: 164 RVA: 0x00004A65 File Offset: 0x00002C65
		public Money(CurrencyCodeKind currencyCode, decimal amount)
		{
			if (CurrencyRepository.Exists(currencyCode))
			{
				this.CurrencyCode = currencyCode;
				this.Amount = amount;
				this._currency = CurrencyRepository.Get(this.CurrencyCode);
				return;
			}
			throw new InvalidOperationException("Currency code unknown.");
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00004AA0 File Offset: 0x00002CA0
		public Money(CultureInfo cultureInfo, decimal amount)
		{
			CurrencyCodeKind currencyCode;
			try
			{
				currencyCode = (CurrencyCodeKind)Enum.Parse(typeof(CurrencyCodeKind), new RegionInfo(cultureInfo.LCID).ISOCurrencySymbol);
			}
			catch (Exception)
			{
				throw new InvalidOperationException("Currency code unknown.");
			}
			if (CurrencyRepository.Exists(currencyCode))
			{
				this.CurrencyCode = currencyCode;
				this.Amount = amount;
				this._currency = CurrencyRepository.Get(this.CurrencyCode);
				return;
			}
			throw new InvalidOperationException("Currency code unknown.");
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00004B28 File Offset: 0x00002D28
		public Money(decimal amount)
		{
			CurrencyCodeKind currencyCode;
			try
			{
				currencyCode = (CurrencyCodeKind)Enum.Parse(typeof(CurrencyCodeKind), RegionInfo.CurrentRegion.ISOCurrencySymbol);
			}
			catch (Exception)
			{
				throw new InvalidOperationException("Currency code unknown.");
			}
			if (CurrencyRepository.Exists(currencyCode))
			{
				this.CurrencyCode = currencyCode;
				this.Amount = amount;
				this._currency = CurrencyRepository.Get(this.CurrencyCode);
				return;
			}
			throw new InvalidOperationException("Currency code unknown.");
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00004BAC File Offset: 0x00002DAC
		private static void AssertSameCurrency(Money first, Money second)
		{
			if (first.CurrencyCode != second.CurrencyCode)
			{
				throw new InvalidOperationException("Money currency mismatch.");
			}
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00004BC8 File Offset: 0x00002DC8
		public override int GetHashCode()
		{
			return this.Amount.GetHashCode() ^ this.CurrencyCode.GetHashCode();
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00004BF8 File Offset: 0x00002DF8
		public Money Copy()
		{
			return new Money(this.CurrencyCode, this.Amount);
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00004BF8 File Offset: 0x00002DF8
		public Money Clone()
		{
			return new Money(this.CurrencyCode, this.Amount);
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00004C0B File Offset: 0x00002E0B
		public int CompareTo(object obj)
		{
			if (obj == null)
			{
				return 1;
			}
			if (!(obj is Money))
			{
				throw new ArgumentException("Argument must be money");
			}
			return this.CompareTo((Money)obj);
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00004C31 File Offset: 0x00002E31
		public int CompareTo(Money other)
		{
			if (this < other)
			{
				return -1;
			}
			if (this > other)
			{
				return 1;
			}
			return 0;
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00004C4A File Offset: 0x00002E4A
		internal int CentFactor()
		{
			return Money._cents[this._currency.SignificantDecimalDigits];
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00004C60 File Offset: 0x00002E60
		internal Money Truncated()
		{
			return new Money(this.CurrencyCode, (long)Math.Truncate(this.Amount * this.CentFactor()) / this.CentFactor());
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00004CB0 File Offset: 0x00002EB0
		public Money Rounded()
		{
			CurrencyRoundingKind roundingType = this._currency.RoundingType;
			if (roundingType != CurrencyRoundingKind.Swiss)
			{
				if (roundingType != CurrencyRoundingKind.Argentinian)
				{
					return new Money(this.CurrencyCode, decimal.Round(this.Amount, this._currency.SignificantDecimalDigits, MidpointRounding.AwayFromZero));
				}
				decimal d = this.Amount * this.CentFactor() - (long)Math.Truncate(this.Amount * this.CentFactor());
				if (d < 0.3m)
				{
					return this.Truncated();
				}
				if (d > 0.7m)
				{
					return new Money(this.CurrencyCode, decimal.Round(this.Amount, this._currency.SignificantDecimalDigits, MidpointRounding.AwayFromZero));
				}
				return new Money(this.CurrencyCode, ((long)Math.Truncate(this.Amount * this.CentFactor()) + 0.5m) / this.CentFactor());
			}
			else
			{
				decimal d2 = this.Amount * this.CentFactor() - (long)Math.Truncate(this.Amount * this.CentFactor());
				if (d2 < 0.26m)
				{
					return this.Truncated();
				}
				if (d2 > 0.75m)
				{
					return new Money(this.CurrencyCode, decimal.Round(this.Amount, this._currency.SignificantDecimalDigits, MidpointRounding.AwayFromZero));
				}
				return new Money(this.CurrencyCode, ((long)Math.Truncate(this.Amount * this.CentFactor()) + 0.5m) / this.CentFactor());
			}
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00004EB9 File Offset: 0x000030B9
		public static implicit operator Money(byte value)
		{
			return new Money(value);
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00004EC6 File Offset: 0x000030C6
		public static implicit operator Money(sbyte value)
		{
			return new Money(value);
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00004ED3 File Offset: 0x000030D3
		public static implicit operator Money(float value)
		{
			return new Money((decimal)value);
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00004EE0 File Offset: 0x000030E0
		public static implicit operator Money(double value)
		{
			return new Money((decimal)value);
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00004EED File Offset: 0x000030ED
		public static implicit operator Money(decimal value)
		{
			return new Money(value);
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00004EF5 File Offset: 0x000030F5
		public static implicit operator decimal(Money value)
		{
			return value.Amount;
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00004EFD File Offset: 0x000030FD
		public static implicit operator Money(short value)
		{
			return new Money(value);
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00004F0A File Offset: 0x0000310A
		public static implicit operator Money(int value)
		{
			return new Money(value);
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00004F17 File Offset: 0x00003117
		public static implicit operator Money(long value)
		{
			return new Money(value);
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00004F24 File Offset: 0x00003124
		public static implicit operator Money(ushort value)
		{
			return new Money(value);
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00004F31 File Offset: 0x00003131
		public static implicit operator Money(uint value)
		{
			return new Money(value);
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00004F3E File Offset: 0x0000313E
		public static implicit operator Money(ulong value)
		{
			return new Money(value);
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00004F4B File Offset: 0x0000314B
		public static bool operator >(Money first, Money second)
		{
			Money.AssertSameCurrency(first, second);
			return first.Amount > second.Amount;
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00004F65 File Offset: 0x00003165
		public static bool operator >=(Money first, Money second)
		{
			Money.AssertSameCurrency(first, second);
			return first.Amount >= second.Amount;
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00004F7F File Offset: 0x0000317F
		public static bool operator <=(Money first, Money second)
		{
			Money.AssertSameCurrency(first, second);
			return first.Amount <= second.Amount;
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00004F99 File Offset: 0x00003199
		public static bool operator <(Money first, Money second)
		{
			Money.AssertSameCurrency(first, second);
			return first.Amount < second.Amount;
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00004FB3 File Offset: 0x000031B3
		public static Money operator +(Money first, Money second)
		{
			Money.AssertSameCurrency(first, second);
			return new Money(first.CurrencyCode, first.Amount + second.Amount);
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00004FD8 File Offset: 0x000031D8
		public static Money operator -(Money first, Money second)
		{
			Money.AssertSameCurrency(first, second);
			return new Money(first.CurrencyCode, first.Amount - second.Amount);
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00004FFD File Offset: 0x000031FD
		public static bool operator ==(Money first, Money second)
		{
			return first == second || (first != null && second != null && first.CurrencyCode == second.CurrencyCode && first.Amount == second.Amount);
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x0000502E File Offset: 0x0000322E
		public static bool operator !=(Money first, Money second)
		{
			return !first.Equals(second);
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x0000503A File Offset: 0x0000323A
		public static Money operator *(Money money, decimal value)
		{
			if (money == null)
			{
				throw new ArgumentNullException("money");
			}
			return new Money(money.CurrencyCode, money.Amount * value);
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00005067 File Offset: 0x00003267
		public static Money operator /(Money money, decimal value)
		{
			if (money == null)
			{
				throw new ArgumentNullException("money");
			}
			return new Money(money.CurrencyCode, money.Amount / value);
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x00005094 File Offset: 0x00003294
		public static Money Empty
		{
			get
			{
				return new Money(0m);
			}
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x000050A0 File Offset: 0x000032A0
		public static Money Add(Money first, Money second)
		{
			Money.AssertSameCurrency(first, second);
			return first + second;
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x000050B0 File Offset: 0x000032B0
		public void Add(Money addAmount)
		{
			Money.AssertSameCurrency(this, addAmount);
			this.Amount += addAmount.Amount;
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x000050D0 File Offset: 0x000032D0
		public static Money Subtract(Money first, Money second)
		{
			Money.AssertSameCurrency(first, second);
			return first - second;
		}

		// Token: 0x060000CA RID: 202 RVA: 0x000050E0 File Offset: 0x000032E0
		public void Subtract(Money subtractAmount)
		{
			Money.AssertSameCurrency(this, subtractAmount);
			this.Amount -= subtractAmount.Amount;
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00005100 File Offset: 0x00003300
		public static Money Multiply(Money money, decimal value)
		{
			return money * value;
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00005109 File Offset: 0x00003309
		public void Multiply(decimal value)
		{
			this.Amount *= value;
		}

		// Token: 0x060000CD RID: 205 RVA: 0x0000511D File Offset: 0x0000331D
		public static Money Divide(Money first, decimal value)
		{
			return first / value;
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00005126 File Offset: 0x00003326
		public void Divide(decimal value)
		{
			this.Amount /= value;
		}

		// Token: 0x060000CF RID: 207 RVA: 0x0000513C File Offset: 0x0000333C
		public Money[] Allocate(int n)
		{
			Money money = new Money(this.CurrencyCode, this.Amount / n).Truncated();
			Money money2 = money.Amount + new Money(this.CurrencyCode, 1.0m / this.CentFactor());
			Money[] array = new Money[n];
			int num = (int)(this.Amount * this.CentFactor() % n);
			for (int i = 0; i < num; i++)
			{
				array[i] = money2;
			}
			for (int j = num; j < n; j++)
			{
				array[j] = money;
			}
			return array;
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x000051FC File Offset: 0x000033FC
		public Money[] Allocate(int[] ratios)
		{
			decimal num = 0m;
			for (int i = 0; i < ratios.Length; i++)
			{
				num += ratios[i];
			}
			Money money = this.Copy();
			Money[] array = new Money[ratios.Length];
			for (int j = 0; j < array.Length; j++)
			{
				array[j] = new Money(this.CurrencyCode, this.Amount * ratios[j] / num).Truncated();
				money.Subtract(array[j]);
			}
			long num2 = (long)(money.Amount / (1.0m / this.CentFactor()));
			int num3 = 0;
			while ((long)num3 < num2)
			{
				array[num3].Add(new Money(this.CurrencyCode, 1.0m / this.CentFactor()));
				num3++;
			}
			return array;
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x000052FA File Offset: 0x000034FA
		public override bool Equals(object obj)
		{
			return obj is Money && this.Equals((Money)obj);
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00005312 File Offset: 0x00003512
		public bool Equals(Money other)
		{
			return other != null && this.CurrencyCode == other.CurrencyCode && this.Amount == other.Amount;
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x0000533A File Offset: 0x0000353A
		private CultureInfo GetGlobalCultureInfo(CultureInfo cultureInfo)
		{
			if (!cultureInfo.UseUserOverride)
			{
				return cultureInfo;
			}
			return new CultureInfo(cultureInfo.Name, false);
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00005354 File Offset: 0x00003554
		private NumberFormatInfo GetCurrencyFormatter(CultureInfo cultureInfo, bool useSymbol)
		{
			NumberFormatInfo numberFormatInfo = (NumberFormatInfo)this.GetGlobalCultureInfo(cultureInfo).NumberFormat.Clone();
			Currency currency = CurrencyRepository.Get(this.CurrencyCode);
			numberFormatInfo.CurrencyDecimalDigits = currency.SignificantDecimalDigits;
			if (useSymbol)
			{
				numberFormatInfo.CurrencySymbol = currency.Symbol;
			}
			else
			{
				numberFormatInfo.CurrencySymbol = Enum.GetName(typeof(CurrencyCodeKind), this.CurrencyCode);
				if (numberFormatInfo.CurrencyPositivePattern <= 1)
				{
					numberFormatInfo.CurrencyPositivePattern += 2;
				}
				switch (numberFormatInfo.CurrencyNegativePattern)
				{
				case 0:
					numberFormatInfo.CurrencyNegativePattern = 14;
					break;
				case 1:
					numberFormatInfo.CurrencyNegativePattern = 9;
					break;
				case 2:
					numberFormatInfo.CurrencyNegativePattern = 12;
					break;
				case 3:
					numberFormatInfo.CurrencyNegativePattern = 11;
					break;
				case 4:
					numberFormatInfo.CurrencyNegativePattern = 15;
					break;
				case 5:
					numberFormatInfo.CurrencyNegativePattern = 8;
					break;
				case 6:
					numberFormatInfo.CurrencyNegativePattern = 13;
					break;
				case 7:
					numberFormatInfo.CurrencyNegativePattern = 10;
					break;
				}
			}
			return numberFormatInfo;
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00005458 File Offset: 0x00003658
		public new string ToString()
		{
			return this.Rounded().Amount.ToString("C", this.GetCurrencyFormatter(CultureInfo.CurrentCulture, false));
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x0000548C File Offset: 0x0000368C
		public string ToString(CultureInfo cultureInfo)
		{
			return this.Rounded().Amount.ToString("C", this.GetCurrencyFormatter(cultureInfo, false));
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x000054BC File Offset: 0x000036BC
		public string ToString(CultureInfo cultureInfo, bool useSymbol)
		{
			return this.Rounded().Amount.ToString("C", this.GetCurrencyFormatter(cultureInfo, useSymbol));
		}

		// Token: 0x040000D3 RID: 211
		private Currency _currency;

		// Token: 0x040000D4 RID: 212
		private static int[] _cents = new int[]
		{
			1,
			10,
			100,
			1000
		};
	}
}
