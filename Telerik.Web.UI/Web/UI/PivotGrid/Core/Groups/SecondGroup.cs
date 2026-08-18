using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Telerik.Web.UI.PivotGrid.Core.Groups
{
	// Token: 0x020006CD RID: 1741
	public struct SecondGroup : IComparable, IComparable<SecondGroup>, IEquatable<SecondGroup>
	{
		// Token: 0x06003E7C RID: 15996 RVA: 0x000C7BC3 File Offset: 0x000C5DC3
		public SecondGroup(int second)
		{
			this = default(SecondGroup);
			this.Second = second;
		}

		// Token: 0x06003E7D RID: 15997 RVA: 0x000C7BD3 File Offset: 0x000C5DD3
		public SecondGroup(int second, CultureInfo culture)
		{
			this = new SecondGroup(second);
			this.culture = culture;
		}

		// Token: 0x17001470 RID: 5232
		// (get) Token: 0x06003E7E RID: 15998 RVA: 0x000C7BE3 File Offset: 0x000C5DE3
		// (set) Token: 0x06003E7F RID: 15999 RVA: 0x000C7BEB File Offset: 0x000C5DEB
		public int Second { get; set; }

		// Token: 0x06003E80 RID: 16000 RVA: 0x000C7BF4 File Offset: 0x000C5DF4
		public override string ToString()
		{
			CultureInfo cultureInfo = this.culture ?? CultureInfo.InvariantCulture;
			return this.Second.ToString(cultureInfo.NumberFormat);
		}

		// Token: 0x06003E81 RID: 16001 RVA: 0x000C7C25 File Offset: 0x000C5E25
		public override int GetHashCode()
		{
			return this.Second;
		}

		// Token: 0x06003E82 RID: 16002 RVA: 0x000C7C2D File Offset: 0x000C5E2D
		[SuppressMessage("Microsoft.Usage", "CA2231:OverloadOperatorEqualsOnOverridingValueTypeEquals", Justification = "Design choice.")]
		public override bool Equals(object obj)
		{
			return obj is SecondGroup && this.Equals((SecondGroup)obj);
		}

		// Token: 0x06003E83 RID: 16003 RVA: 0x000C7C45 File Offset: 0x000C5E45
		public bool Equals(SecondGroup other)
		{
			return this.Second == other.Second;
		}

		// Token: 0x06003E84 RID: 16004 RVA: 0x000C7C56 File Offset: 0x000C5E56
		public int CompareTo(object obj)
		{
			if (obj is SecondGroup)
			{
				return this.CompareTo((SecondGroup)obj);
			}
			throw new ArgumentException("Can not compare.", "obj");
		}

		// Token: 0x06003E85 RID: 16005 RVA: 0x000C7C7C File Offset: 0x000C5E7C
		public int CompareTo(SecondGroup other)
		{
			return this.Second.CompareTo(other.Second);
		}

		// Token: 0x06003E86 RID: 16006 RVA: 0x000C7C9E File Offset: 0x000C5E9E
		public static bool operator <(SecondGroup left, SecondGroup right)
		{
			return left.CompareTo(right) < 0;
		}

		// Token: 0x06003E87 RID: 16007 RVA: 0x000C7CAB File Offset: 0x000C5EAB
		public static bool operator >(SecondGroup left, SecondGroup right)
		{
			return left.CompareTo(right) > 0;
		}

		// Token: 0x06003E88 RID: 16008 RVA: 0x000C7CB8 File Offset: 0x000C5EB8
		public static bool operator <=(SecondGroup left, SecondGroup right)
		{
			return left.CompareTo(right) <= 0;
		}

		// Token: 0x06003E89 RID: 16009 RVA: 0x000C7CC8 File Offset: 0x000C5EC8
		public static bool operator >=(SecondGroup left, SecondGroup right)
		{
			return left.CompareTo(right) >= 0;
		}

		// Token: 0x06003E8A RID: 16010 RVA: 0x000C7CD8 File Offset: 0x000C5ED8
		public static bool operator ==(SecondGroup left, SecondGroup right)
		{
			return left.Equals(right);
		}

		// Token: 0x06003E8B RID: 16011 RVA: 0x000C7CE2 File Offset: 0x000C5EE2
		public static bool operator !=(SecondGroup left, SecondGroup right)
		{
			return !left.Equals(right);
		}

		// Token: 0x040010A6 RID: 4262
		private readonly CultureInfo culture;
	}
}
