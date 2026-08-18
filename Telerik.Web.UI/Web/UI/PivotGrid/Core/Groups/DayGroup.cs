using System;
using System.Globalization;

namespace Telerik.Web.UI.PivotGrid.Core.Groups
{
	// Token: 0x02000CC2 RID: 3266
	[Serializable]
	public struct DayGroup : IComparable, IComparable<DayGroup>, IEquatable<DayGroup>
	{
		// Token: 0x060079FC RID: 31228 RVA: 0x001BFEB0 File Offset: 0x001BE0B0
		public DayGroup(int month, int day)
		{
			this = default(DayGroup);
			this.Month = month;
			this.Day = day;
		}

		// Token: 0x060079FD RID: 31229 RVA: 0x001BFEC7 File Offset: 0x001BE0C7
		public DayGroup(int month, int day, CultureInfo culture)
		{
			this = new DayGroup(month, day);
			this.culture = culture;
		}

		// Token: 0x17002742 RID: 10050
		// (get) Token: 0x060079FE RID: 31230 RVA: 0x001BFED8 File Offset: 0x001BE0D8
		// (set) Token: 0x060079FF RID: 31231 RVA: 0x001BFEE0 File Offset: 0x001BE0E0
		public int Day { get; set; }

		// Token: 0x17002743 RID: 10051
		// (get) Token: 0x06007A00 RID: 31232 RVA: 0x001BFEE9 File Offset: 0x001BE0E9
		// (set) Token: 0x06007A01 RID: 31233 RVA: 0x001BFEF1 File Offset: 0x001BE0F1
		public int Month { get; set; }

		// Token: 0x06007A02 RID: 31234 RVA: 0x001BFEFC File Offset: 0x001BE0FC
		public override string ToString()
		{
			CultureInfo cultureInfo = this.culture ?? CultureInfo.InvariantCulture;
			return cultureInfo.DateTimeFormat.GetAbbreviatedMonthName(this.Month) + "-" + this.Day.ToString(cultureInfo.NumberFormat);
		}

		// Token: 0x06007A03 RID: 31235 RVA: 0x001BFF48 File Offset: 0x001BE148
		public override int GetHashCode()
		{
			return this.Day * 7951 + this.Month * 7993;
		}

		// Token: 0x06007A04 RID: 31236 RVA: 0x001BFF63 File Offset: 0x001BE163
		public override bool Equals(object obj)
		{
			return obj is DayGroup && this.Equals((DayGroup)obj);
		}

		// Token: 0x06007A05 RID: 31237 RVA: 0x001BFF7B File Offset: 0x001BE17B
		public bool Equals(DayGroup other)
		{
			return this.Month == other.Month && this.Day == other.Day;
		}

		// Token: 0x06007A06 RID: 31238 RVA: 0x001BFF9D File Offset: 0x001BE19D
		public int CompareTo(object obj)
		{
			if (obj is DayGroup)
			{
				return this.CompareTo((DayGroup)obj);
			}
			throw new ArgumentException("Can not compare.", "obj");
		}

		// Token: 0x06007A07 RID: 31239 RVA: 0x001BFFC4 File Offset: 0x001BE1C4
		public int CompareTo(DayGroup other)
		{
			int num = this.Month.CompareTo(other.Month);
			if (num == 0)
			{
				num = this.Day.CompareTo(other.Day);
			}
			return num;
		}

		// Token: 0x06007A08 RID: 31240 RVA: 0x001C0001 File Offset: 0x001BE201
		public static bool operator <(DayGroup left, DayGroup right)
		{
			return left.CompareTo(right) < 0;
		}

		// Token: 0x06007A09 RID: 31241 RVA: 0x001C000E File Offset: 0x001BE20E
		public static bool operator >(DayGroup left, DayGroup right)
		{
			return left.CompareTo(right) > 0;
		}

		// Token: 0x06007A0A RID: 31242 RVA: 0x001C001B File Offset: 0x001BE21B
		public static bool operator <=(DayGroup left, DayGroup right)
		{
			return left.CompareTo(right) <= 0;
		}

		// Token: 0x06007A0B RID: 31243 RVA: 0x001C002B File Offset: 0x001BE22B
		public static bool operator >=(DayGroup left, DayGroup right)
		{
			return left.CompareTo(right) >= 0;
		}

		// Token: 0x06007A0C RID: 31244 RVA: 0x001C003B File Offset: 0x001BE23B
		public static bool operator ==(DayGroup left, DayGroup right)
		{
			return left.Equals(right);
		}

		// Token: 0x06007A0D RID: 31245 RVA: 0x001C0045 File Offset: 0x001BE245
		public static bool operator !=(DayGroup left, DayGroup right)
		{
			return !left.Equals(right);
		}

		// Token: 0x04002174 RID: 8564
		private readonly CultureInfo culture;
	}
}
