using System;
using System.Globalization;

namespace Telerik.Web.UI.PivotGrid.Core.Groups
{
	// Token: 0x02000CC3 RID: 3267
	[Serializable]
	public struct MonthGroup : IComparable, IComparable<MonthGroup>, IEquatable<MonthGroup>
	{
		// Token: 0x06007A0E RID: 31246 RVA: 0x001C0052 File Offset: 0x001BE252
		public MonthGroup(int month)
		{
			this = default(MonthGroup);
			this.Month = month;
		}

		// Token: 0x06007A0F RID: 31247 RVA: 0x001C0062 File Offset: 0x001BE262
		public MonthGroup(int month, CultureInfo culture)
		{
			this = new MonthGroup(month);
			this.culture = culture;
		}

		// Token: 0x17002744 RID: 10052
		// (get) Token: 0x06007A10 RID: 31248 RVA: 0x001C0072 File Offset: 0x001BE272
		// (set) Token: 0x06007A11 RID: 31249 RVA: 0x001C007A File Offset: 0x001BE27A
		public int Month { get; set; }

		// Token: 0x06007A12 RID: 31250 RVA: 0x001C0084 File Offset: 0x001BE284
		public override string ToString()
		{
			CultureInfo cultureInfo = this.culture ?? CultureInfo.InvariantCulture;
			return cultureInfo.DateTimeFormat.GetMonthName(this.Month);
		}

		// Token: 0x06007A13 RID: 31251 RVA: 0x001C00B2 File Offset: 0x001BE2B2
		public override int GetHashCode()
		{
			return this.Month;
		}

		// Token: 0x06007A14 RID: 31252 RVA: 0x001C00BA File Offset: 0x001BE2BA
		public override bool Equals(object obj)
		{
			return obj is MonthGroup && this.Equals((MonthGroup)obj);
		}

		// Token: 0x06007A15 RID: 31253 RVA: 0x001C00D2 File Offset: 0x001BE2D2
		public bool Equals(MonthGroup other)
		{
			return this.Month == other.Month;
		}

		// Token: 0x06007A16 RID: 31254 RVA: 0x001C00E3 File Offset: 0x001BE2E3
		public int CompareTo(object obj)
		{
			if (obj is MonthGroup)
			{
				return this.CompareTo((MonthGroup)obj);
			}
			throw new ArgumentException("Can not compare.", "obj");
		}

		// Token: 0x06007A17 RID: 31255 RVA: 0x001C010C File Offset: 0x001BE30C
		public int CompareTo(MonthGroup other)
		{
			return this.Month.CompareTo(other.Month);
		}

		// Token: 0x06007A18 RID: 31256 RVA: 0x001C012E File Offset: 0x001BE32E
		public static bool operator <(MonthGroup left, MonthGroup right)
		{
			return left.CompareTo(right) < 0;
		}

		// Token: 0x06007A19 RID: 31257 RVA: 0x001C013B File Offset: 0x001BE33B
		public static bool operator >(MonthGroup left, MonthGroup right)
		{
			return left.CompareTo(right) > 0;
		}

		// Token: 0x06007A1A RID: 31258 RVA: 0x001C0148 File Offset: 0x001BE348
		public static bool operator <=(MonthGroup left, MonthGroup right)
		{
			return left.CompareTo(right) <= 0;
		}

		// Token: 0x06007A1B RID: 31259 RVA: 0x001C0158 File Offset: 0x001BE358
		public static bool operator >=(MonthGroup left, MonthGroup right)
		{
			return left.CompareTo(right) >= 0;
		}

		// Token: 0x06007A1C RID: 31260 RVA: 0x001C0168 File Offset: 0x001BE368
		public static bool operator ==(MonthGroup left, MonthGroup right)
		{
			return left.Equals(right);
		}

		// Token: 0x06007A1D RID: 31261 RVA: 0x001C0172 File Offset: 0x001BE372
		public static bool operator !=(MonthGroup left, MonthGroup right)
		{
			return !left.Equals(right);
		}

		// Token: 0x04002177 RID: 8567
		private readonly CultureInfo culture;
	}
}
