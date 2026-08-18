using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Telerik.Web.UI.PivotGrid.Core.Groups
{
	// Token: 0x020006CE RID: 1742
	public struct WeekGroup : IComparable, IComparable<WeekGroup>, IEquatable<WeekGroup>
	{
		// Token: 0x06003E8C RID: 16012 RVA: 0x000C7CEF File Offset: 0x000C5EEF
		public WeekGroup(int week)
		{
			this = default(WeekGroup);
			this.Week = week;
		}

		// Token: 0x06003E8D RID: 16013 RVA: 0x000C7CFF File Offset: 0x000C5EFF
		public WeekGroup(int week, CultureInfo culture)
		{
			this = new WeekGroup(week);
			this.culture = culture;
		}

		// Token: 0x17001471 RID: 5233
		// (get) Token: 0x06003E8E RID: 16014 RVA: 0x000C7D0F File Offset: 0x000C5F0F
		// (set) Token: 0x06003E8F RID: 16015 RVA: 0x000C7D17 File Offset: 0x000C5F17
		public int Week { get; set; }

		// Token: 0x06003E90 RID: 16016 RVA: 0x000C7D20 File Offset: 0x000C5F20
		public override string ToString()
		{
			CultureInfo cultureInfo = this.culture ?? CultureInfo.InvariantCulture;
			return this.Week.ToString(cultureInfo.NumberFormat);
		}

		// Token: 0x06003E91 RID: 16017 RVA: 0x000C7D51 File Offset: 0x000C5F51
		public override int GetHashCode()
		{
			return this.Week;
		}

		// Token: 0x06003E92 RID: 16018 RVA: 0x000C7D59 File Offset: 0x000C5F59
		[SuppressMessage("Microsoft.Usage", "CA2231:OverloadOperatorEqualsOnOverridingValueTypeEquals", Justification = "Design choice.")]
		public override bool Equals(object obj)
		{
			return obj is WeekGroup && this.Equals((WeekGroup)obj);
		}

		// Token: 0x06003E93 RID: 16019 RVA: 0x000C7D71 File Offset: 0x000C5F71
		public bool Equals(WeekGroup other)
		{
			return this.Week == other.Week;
		}

		// Token: 0x06003E94 RID: 16020 RVA: 0x000C7D82 File Offset: 0x000C5F82
		public int CompareTo(object obj)
		{
			if (obj is WeekGroup)
			{
				return this.CompareTo((WeekGroup)obj);
			}
			throw new ArgumentException("Can not compare.", "obj");
		}

		// Token: 0x06003E95 RID: 16021 RVA: 0x000C7DA8 File Offset: 0x000C5FA8
		public int CompareTo(WeekGroup other)
		{
			return this.Week.CompareTo(other.Week);
		}

		// Token: 0x06003E96 RID: 16022 RVA: 0x000C7DCA File Offset: 0x000C5FCA
		public static bool operator <(WeekGroup left, WeekGroup right)
		{
			return left.CompareTo(right) < 0;
		}

		// Token: 0x06003E97 RID: 16023 RVA: 0x000C7DD7 File Offset: 0x000C5FD7
		public static bool operator >(WeekGroup left, WeekGroup right)
		{
			return left.CompareTo(right) > 0;
		}

		// Token: 0x06003E98 RID: 16024 RVA: 0x000C7DE4 File Offset: 0x000C5FE4
		public static bool operator <=(WeekGroup left, WeekGroup right)
		{
			return left.CompareTo(right) <= 0;
		}

		// Token: 0x06003E99 RID: 16025 RVA: 0x000C7DF4 File Offset: 0x000C5FF4
		public static bool operator >=(WeekGroup left, WeekGroup right)
		{
			return left.CompareTo(right) >= 0;
		}

		// Token: 0x06003E9A RID: 16026 RVA: 0x000C7E04 File Offset: 0x000C6004
		public static bool operator ==(WeekGroup left, WeekGroup right)
		{
			return left.Equals(right);
		}

		// Token: 0x06003E9B RID: 16027 RVA: 0x000C7E0E File Offset: 0x000C600E
		public static bool operator !=(WeekGroup left, WeekGroup right)
		{
			return !left.Equals(right);
		}

		// Token: 0x040010A8 RID: 4264
		private readonly CultureInfo culture;
	}
}
