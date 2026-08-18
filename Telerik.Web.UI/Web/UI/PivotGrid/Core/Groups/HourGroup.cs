using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Telerik.Web.UI.PivotGrid.Core.Groups
{
	// Token: 0x020006CB RID: 1739
	public struct HourGroup : IComparable, IComparable<HourGroup>, IEquatable<HourGroup>
	{
		// Token: 0x06003E5C RID: 15964 RVA: 0x000C7968 File Offset: 0x000C5B68
		public HourGroup(int hour)
		{
			this = default(HourGroup);
			this.Hour = hour;
		}

		// Token: 0x06003E5D RID: 15965 RVA: 0x000C7978 File Offset: 0x000C5B78
		public HourGroup(int hour, CultureInfo culture)
		{
			this = new HourGroup(hour);
			this.culture = culture;
		}

		// Token: 0x1700146E RID: 5230
		// (get) Token: 0x06003E5E RID: 15966 RVA: 0x000C7988 File Offset: 0x000C5B88
		// (set) Token: 0x06003E5F RID: 15967 RVA: 0x000C7990 File Offset: 0x000C5B90
		public int Hour { get; set; }

		// Token: 0x06003E60 RID: 15968 RVA: 0x000C799C File Offset: 0x000C5B9C
		public override string ToString()
		{
			CultureInfo cultureInfo = this.culture ?? CultureInfo.InvariantCulture;
			return this.Hour.ToString(cultureInfo.NumberFormat);
		}

		// Token: 0x06003E61 RID: 15969 RVA: 0x000C79CD File Offset: 0x000C5BCD
		public override int GetHashCode()
		{
			return this.Hour;
		}

		// Token: 0x06003E62 RID: 15970 RVA: 0x000C79D5 File Offset: 0x000C5BD5
		[SuppressMessage("Microsoft.Usage", "CA2231:OverloadOperatorEqualsOnOverridingValueTypeEquals", Justification = "Design choice.")]
		public override bool Equals(object obj)
		{
			return obj is HourGroup && this.Equals((HourGroup)obj);
		}

		// Token: 0x06003E63 RID: 15971 RVA: 0x000C79ED File Offset: 0x000C5BED
		public bool Equals(HourGroup other)
		{
			return this.Hour == other.Hour;
		}

		// Token: 0x06003E64 RID: 15972 RVA: 0x000C79FE File Offset: 0x000C5BFE
		public int CompareTo(object obj)
		{
			if (obj is HourGroup)
			{
				return this.CompareTo((HourGroup)obj);
			}
			throw new ArgumentException("Can not compare.", "obj");
		}

		// Token: 0x06003E65 RID: 15973 RVA: 0x000C7A24 File Offset: 0x000C5C24
		public int CompareTo(HourGroup other)
		{
			return this.Hour.CompareTo(other.Hour);
		}

		// Token: 0x06003E66 RID: 15974 RVA: 0x000C7A46 File Offset: 0x000C5C46
		public static bool operator <(HourGroup left, HourGroup right)
		{
			return left.CompareTo(right) < 0;
		}

		// Token: 0x06003E67 RID: 15975 RVA: 0x000C7A53 File Offset: 0x000C5C53
		public static bool operator >(HourGroup left, HourGroup right)
		{
			return left.CompareTo(right) > 0;
		}

		// Token: 0x06003E68 RID: 15976 RVA: 0x000C7A60 File Offset: 0x000C5C60
		public static bool operator <=(HourGroup left, HourGroup right)
		{
			return left.CompareTo(right) <= 0;
		}

		// Token: 0x06003E69 RID: 15977 RVA: 0x000C7A70 File Offset: 0x000C5C70
		public static bool operator >=(HourGroup left, HourGroup right)
		{
			return left.CompareTo(right) >= 0;
		}

		// Token: 0x06003E6A RID: 15978 RVA: 0x000C7A80 File Offset: 0x000C5C80
		public static bool operator ==(HourGroup left, HourGroup right)
		{
			return left.Equals(right);
		}

		// Token: 0x06003E6B RID: 15979 RVA: 0x000C7A8A File Offset: 0x000C5C8A
		public static bool operator !=(HourGroup left, HourGroup right)
		{
			return !left.Equals(right);
		}

		// Token: 0x040010A2 RID: 4258
		private readonly CultureInfo culture;
	}
}
