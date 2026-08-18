using System;
using System.Globalization;

namespace Telerik.Web.UI.PivotGrid.Core.Groups
{
	// Token: 0x02000CD4 RID: 3284
	[Serializable]
	public struct DoubleGroup : IEquatable<DoubleGroup>, IComparable<DoubleGroup>, IComparable
	{
		// Token: 0x06007AB3 RID: 31411 RVA: 0x001C28E7 File Offset: 0x001C0AE7
		public DoubleGroup(double start, double end)
		{
			this = default(DoubleGroup);
			this.Start = start;
			this.End = end;
		}

		// Token: 0x1700275C RID: 10076
		// (get) Token: 0x06007AB4 RID: 31412 RVA: 0x001C28FE File Offset: 0x001C0AFE
		// (set) Token: 0x06007AB5 RID: 31413 RVA: 0x001C2906 File Offset: 0x001C0B06
		public double Start { get; set; }

		// Token: 0x1700275D RID: 10077
		// (get) Token: 0x06007AB6 RID: 31414 RVA: 0x001C290F File Offset: 0x001C0B0F
		// (set) Token: 0x06007AB7 RID: 31415 RVA: 0x001C2917 File Offset: 0x001C0B17
		public double End { get; set; }

		// Token: 0x06007AB8 RID: 31416 RVA: 0x001C2920 File Offset: 0x001C0B20
		public override string ToString()
		{
			NumberFormatInfo numberFormat = CultureInfo.InvariantCulture.NumberFormat;
			if (this.Start == double.NegativeInfinity)
			{
				return string.Format(numberFormat, "<{0:0}", new object[]
				{
					this.End
				});
			}
			if (this.End == double.PositiveInfinity)
			{
				return string.Format(numberFormat, ">{0:0}", new object[]
				{
					this.Start
				});
			}
			return string.Format(numberFormat, "{0:0} - {1:0}", new object[]
			{
				this.Start,
				this.End
			});
		}

		// Token: 0x06007AB9 RID: 31417 RVA: 0x001C29CD File Offset: 0x001C0BCD
		public override bool Equals(object obj)
		{
			return obj is DoubleGroup && this.Equals((DoubleGroup)obj);
		}

		// Token: 0x06007ABA RID: 31418 RVA: 0x001C29E5 File Offset: 0x001C0BE5
		public bool Equals(DoubleGroup other)
		{
			return this.Start == other.Start && this.End == other.End;
		}

		// Token: 0x06007ABB RID: 31419 RVA: 0x001C2A08 File Offset: 0x001C0C08
		public int CompareTo(DoubleGroup other)
		{
			int num = this.Start.CompareTo(other.Start);
			if (num == 0)
			{
				return this.End.CompareTo(other.End);
			}
			return num;
		}

		// Token: 0x06007ABC RID: 31420 RVA: 0x001C2A45 File Offset: 0x001C0C45
		public int CompareTo(object obj)
		{
			if (obj is DoubleGroup)
			{
				return this.CompareTo((DoubleGroup)obj);
			}
			throw new ArgumentException("Can not compare.", "obj");
		}

		// Token: 0x06007ABD RID: 31421 RVA: 0x001C2A6C File Offset: 0x001C0C6C
		public override int GetHashCode()
		{
			return this.Start.GetHashCode() + 105943 * this.End.GetHashCode();
		}

		// Token: 0x06007ABE RID: 31422 RVA: 0x001C2A9C File Offset: 0x001C0C9C
		public static bool operator <(DoubleGroup left, DoubleGroup right)
		{
			return left.CompareTo(right) < 0;
		}

		// Token: 0x06007ABF RID: 31423 RVA: 0x001C2AA9 File Offset: 0x001C0CA9
		public static bool operator >(DoubleGroup left, DoubleGroup right)
		{
			return left.CompareTo(right) > 0;
		}

		// Token: 0x06007AC0 RID: 31424 RVA: 0x001C2AB6 File Offset: 0x001C0CB6
		public static bool operator <=(DoubleGroup left, DoubleGroup right)
		{
			return left.CompareTo(right) <= 0;
		}

		// Token: 0x06007AC1 RID: 31425 RVA: 0x001C2AC6 File Offset: 0x001C0CC6
		public static bool operator >=(DoubleGroup left, DoubleGroup right)
		{
			return left.CompareTo(right) >= 0;
		}

		// Token: 0x06007AC2 RID: 31426 RVA: 0x001C2AD6 File Offset: 0x001C0CD6
		public static bool operator ==(DoubleGroup left, DoubleGroup right)
		{
			return left.Equals(right);
		}

		// Token: 0x06007AC3 RID: 31427 RVA: 0x001C2AE0 File Offset: 0x001C0CE0
		public static bool operator !=(DoubleGroup left, DoubleGroup right)
		{
			return !left.Equals(right);
		}
	}
}
