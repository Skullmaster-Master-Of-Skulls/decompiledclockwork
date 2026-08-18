using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Telerik.Web.UI.PivotGrid.Core.Groups
{
	// Token: 0x020006CC RID: 1740
	public struct MinuteGroup : IComparable, IComparable<MinuteGroup>, IEquatable<MinuteGroup>
	{
		// Token: 0x06003E6C RID: 15980 RVA: 0x000C7A97 File Offset: 0x000C5C97
		public MinuteGroup(int minute)
		{
			this = default(MinuteGroup);
			this.Minute = minute;
		}

		// Token: 0x06003E6D RID: 15981 RVA: 0x000C7AA7 File Offset: 0x000C5CA7
		public MinuteGroup(int minute, CultureInfo culture)
		{
			this = new MinuteGroup(minute);
			this.culture = culture;
		}

		// Token: 0x1700146F RID: 5231
		// (get) Token: 0x06003E6E RID: 15982 RVA: 0x000C7AB7 File Offset: 0x000C5CB7
		// (set) Token: 0x06003E6F RID: 15983 RVA: 0x000C7ABF File Offset: 0x000C5CBF
		public int Minute { get; set; }

		// Token: 0x06003E70 RID: 15984 RVA: 0x000C7AC8 File Offset: 0x000C5CC8
		public override string ToString()
		{
			CultureInfo cultureInfo = this.culture ?? CultureInfo.InvariantCulture;
			return this.Minute.ToString(cultureInfo.NumberFormat);
		}

		// Token: 0x06003E71 RID: 15985 RVA: 0x000C7AF9 File Offset: 0x000C5CF9
		public override int GetHashCode()
		{
			return this.Minute;
		}

		// Token: 0x06003E72 RID: 15986 RVA: 0x000C7B01 File Offset: 0x000C5D01
		[SuppressMessage("Microsoft.Usage", "CA2231:OverloadOperatorEqualsOnOverridingValueTypeEquals", Justification = "Design choice.")]
		public override bool Equals(object obj)
		{
			return obj is MinuteGroup && this.Equals((MinuteGroup)obj);
		}

		// Token: 0x06003E73 RID: 15987 RVA: 0x000C7B19 File Offset: 0x000C5D19
		public bool Equals(MinuteGroup other)
		{
			return this.Minute == other.Minute;
		}

		// Token: 0x06003E74 RID: 15988 RVA: 0x000C7B2A File Offset: 0x000C5D2A
		public int CompareTo(object obj)
		{
			if (obj is MinuteGroup)
			{
				return this.CompareTo((MinuteGroup)obj);
			}
			throw new ArgumentException("Can not compare.", "obj");
		}

		// Token: 0x06003E75 RID: 15989 RVA: 0x000C7B50 File Offset: 0x000C5D50
		public int CompareTo(MinuteGroup other)
		{
			return this.Minute.CompareTo(other.Minute);
		}

		// Token: 0x06003E76 RID: 15990 RVA: 0x000C7B72 File Offset: 0x000C5D72
		public static bool operator <(MinuteGroup left, MinuteGroup right)
		{
			return left.CompareTo(right) < 0;
		}

		// Token: 0x06003E77 RID: 15991 RVA: 0x000C7B7F File Offset: 0x000C5D7F
		public static bool operator >(MinuteGroup left, MinuteGroup right)
		{
			return left.CompareTo(right) > 0;
		}

		// Token: 0x06003E78 RID: 15992 RVA: 0x000C7B8C File Offset: 0x000C5D8C
		public static bool operator <=(MinuteGroup left, MinuteGroup right)
		{
			return left.CompareTo(right) <= 0;
		}

		// Token: 0x06003E79 RID: 15993 RVA: 0x000C7B9C File Offset: 0x000C5D9C
		public static bool operator >=(MinuteGroup left, MinuteGroup right)
		{
			return left.CompareTo(right) >= 0;
		}

		// Token: 0x06003E7A RID: 15994 RVA: 0x000C7BAC File Offset: 0x000C5DAC
		public static bool operator ==(MinuteGroup left, MinuteGroup right)
		{
			return left.Equals(right);
		}

		// Token: 0x06003E7B RID: 15995 RVA: 0x000C7BB6 File Offset: 0x000C5DB6
		public static bool operator !=(MinuteGroup left, MinuteGroup right)
		{
			return !left.Equals(right);
		}

		// Token: 0x040010A4 RID: 4260
		private readonly CultureInfo culture;
	}
}
