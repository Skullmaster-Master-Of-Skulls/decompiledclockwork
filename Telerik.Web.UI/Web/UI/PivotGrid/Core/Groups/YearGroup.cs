using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Telerik.Web.UI.PivotGrid.Core.Groups
{
	// Token: 0x02000CC5 RID: 3269
	[Serializable]
	public struct YearGroup : IComparable, IComparable<YearGroup>, IEquatable<YearGroup>
	{
		// Token: 0x06007A2E RID: 31278 RVA: 0x001C02BF File Offset: 0x001BE4BF
		public YearGroup(int year)
		{
			this = default(YearGroup);
			this.Year = year;
		}

		// Token: 0x06007A2F RID: 31279 RVA: 0x001C02CF File Offset: 0x001BE4CF
		public YearGroup(int year, CultureInfo culture)
		{
			this = new YearGroup(year);
			this.culture = culture;
		}

		// Token: 0x17002746 RID: 10054
		// (get) Token: 0x06007A30 RID: 31280 RVA: 0x001C02DF File Offset: 0x001BE4DF
		// (set) Token: 0x06007A31 RID: 31281 RVA: 0x001C02E7 File Offset: 0x001BE4E7
		public int Year { get; set; }

		// Token: 0x06007A32 RID: 31282 RVA: 0x001C02F0 File Offset: 0x001BE4F0
		public override string ToString()
		{
			CultureInfo cultureInfo = this.culture ?? CultureInfo.InvariantCulture;
			return this.Year.ToString(cultureInfo.NumberFormat);
		}

		// Token: 0x06007A33 RID: 31283 RVA: 0x001C0321 File Offset: 0x001BE521
		public override int GetHashCode()
		{
			return this.Year;
		}

		// Token: 0x06007A34 RID: 31284 RVA: 0x001C0329 File Offset: 0x001BE529
		[SuppressMessage("Microsoft.Usage", "CA2231:OverloadOperatorEqualsOnOverridingValueTypeEquals", Justification = "Design choice.")]
		public override bool Equals(object obj)
		{
			return obj is YearGroup && this.Equals((YearGroup)obj);
		}

		// Token: 0x06007A35 RID: 31285 RVA: 0x001C0341 File Offset: 0x001BE541
		public bool Equals(YearGroup other)
		{
			return this.Year == other.Year;
		}

		// Token: 0x06007A36 RID: 31286 RVA: 0x001C0352 File Offset: 0x001BE552
		public int CompareTo(object obj)
		{
			if (obj is YearGroup)
			{
				return this.CompareTo((YearGroup)obj);
			}
			throw new ArgumentException("Can not compare.", "obj");
		}

		// Token: 0x06007A37 RID: 31287 RVA: 0x001C0378 File Offset: 0x001BE578
		public int CompareTo(YearGroup other)
		{
			return this.Year.CompareTo(other.Year);
		}

		// Token: 0x06007A38 RID: 31288 RVA: 0x001C039A File Offset: 0x001BE59A
		public static bool operator <(YearGroup left, YearGroup right)
		{
			return left.CompareTo(right) < 0;
		}

		// Token: 0x06007A39 RID: 31289 RVA: 0x001C03A7 File Offset: 0x001BE5A7
		public static bool operator >(YearGroup left, YearGroup right)
		{
			return left.CompareTo(right) > 0;
		}

		// Token: 0x06007A3A RID: 31290 RVA: 0x001C03B4 File Offset: 0x001BE5B4
		public static bool operator <=(YearGroup left, YearGroup right)
		{
			return left.CompareTo(right) <= 0;
		}

		// Token: 0x06007A3B RID: 31291 RVA: 0x001C03C4 File Offset: 0x001BE5C4
		public static bool operator >=(YearGroup left, YearGroup right)
		{
			return left.CompareTo(right) >= 0;
		}

		// Token: 0x06007A3C RID: 31292 RVA: 0x001C03D4 File Offset: 0x001BE5D4
		public static bool operator ==(YearGroup left, YearGroup right)
		{
			return left.Equals(right);
		}

		// Token: 0x06007A3D RID: 31293 RVA: 0x001C03DE File Offset: 0x001BE5DE
		public static bool operator !=(YearGroup left, YearGroup right)
		{
			return !left.Equals(right);
		}

		// Token: 0x0400217B RID: 8571
		private readonly CultureInfo culture;
	}
}
