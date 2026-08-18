using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Telerik.Web.UI.PivotGrid.Core.Groups
{
	// Token: 0x02000CC4 RID: 3268
	[Serializable]
	public struct QuarterGroup : IComparable, IComparable<QuarterGroup>, IEquatable<QuarterGroup>
	{
		// Token: 0x06007A1E RID: 31262 RVA: 0x001C017F File Offset: 0x001BE37F
		public QuarterGroup(int quarter)
		{
			this = default(QuarterGroup);
			this.Quarter = quarter;
		}

		// Token: 0x06007A1F RID: 31263 RVA: 0x001C018F File Offset: 0x001BE38F
		public QuarterGroup(int quarter, CultureInfo culture)
		{
			this = new QuarterGroup(quarter);
			this.culture = culture;
		}

		// Token: 0x17002745 RID: 10053
		// (get) Token: 0x06007A20 RID: 31264 RVA: 0x001C019F File Offset: 0x001BE39F
		// (set) Token: 0x06007A21 RID: 31265 RVA: 0x001C01A7 File Offset: 0x001BE3A7
		public int Quarter { get; set; }

		// Token: 0x06007A22 RID: 31266 RVA: 0x001C01B0 File Offset: 0x001BE3B0
		public override string ToString()
		{
			CultureInfo cultureInfo = this.culture ?? CultureInfo.InvariantCulture;
			return string.Format(cultureInfo.NumberFormat, "Q{0}", new object[]
			{
				this.Quarter
			});
		}

		// Token: 0x06007A23 RID: 31267 RVA: 0x001C01F3 File Offset: 0x001BE3F3
		public override int GetHashCode()
		{
			return this.Quarter;
		}

		// Token: 0x06007A24 RID: 31268 RVA: 0x001C01FB File Offset: 0x001BE3FB
		[SuppressMessage("Microsoft.Usage", "CA2231:OverloadOperatorEqualsOnOverridingValueTypeEquals", Justification = "Design choice.")]
		public override bool Equals(object obj)
		{
			return obj is QuarterGroup && this.Equals((QuarterGroup)obj);
		}

		// Token: 0x06007A25 RID: 31269 RVA: 0x001C0213 File Offset: 0x001BE413
		public bool Equals(QuarterGroup other)
		{
			return this.Quarter == other.Quarter;
		}

		// Token: 0x06007A26 RID: 31270 RVA: 0x001C0224 File Offset: 0x001BE424
		public int CompareTo(object obj)
		{
			if (obj is QuarterGroup)
			{
				return this.CompareTo((QuarterGroup)obj);
			}
			throw new ArgumentException("Can not compare.", "obj");
		}

		// Token: 0x06007A27 RID: 31271 RVA: 0x001C024C File Offset: 0x001BE44C
		public int CompareTo(QuarterGroup other)
		{
			return this.Quarter.CompareTo(other.Quarter);
		}

		// Token: 0x06007A28 RID: 31272 RVA: 0x001C026E File Offset: 0x001BE46E
		public static bool operator <(QuarterGroup left, QuarterGroup right)
		{
			return left.CompareTo(right) < 0;
		}

		// Token: 0x06007A29 RID: 31273 RVA: 0x001C027B File Offset: 0x001BE47B
		public static bool operator >(QuarterGroup left, QuarterGroup right)
		{
			return left.CompareTo(right) > 0;
		}

		// Token: 0x06007A2A RID: 31274 RVA: 0x001C0288 File Offset: 0x001BE488
		public static bool operator <=(QuarterGroup left, QuarterGroup right)
		{
			return left.CompareTo(right) <= 0;
		}

		// Token: 0x06007A2B RID: 31275 RVA: 0x001C0298 File Offset: 0x001BE498
		public static bool operator >=(QuarterGroup left, QuarterGroup right)
		{
			return left.CompareTo(right) >= 0;
		}

		// Token: 0x06007A2C RID: 31276 RVA: 0x001C02A8 File Offset: 0x001BE4A8
		public static bool operator ==(QuarterGroup left, QuarterGroup right)
		{
			return left.Equals(right);
		}

		// Token: 0x06007A2D RID: 31277 RVA: 0x001C02B2 File Offset: 0x001BE4B2
		public static bool operator !=(QuarterGroup left, QuarterGroup right)
		{
			return !left.Equals(right);
		}

		// Token: 0x04002179 RID: 8569
		private readonly CultureInfo culture;
	}
}
