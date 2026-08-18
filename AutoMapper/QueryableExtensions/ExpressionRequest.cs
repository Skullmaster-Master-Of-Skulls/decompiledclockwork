using System;
using System.Linq;
using System.Reflection;

namespace AutoMapper.QueryableExtensions
{
	// Token: 0x02000056 RID: 86
	public class ExpressionRequest : IEquatable<ExpressionRequest>
	{
		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x0600033F RID: 831 RVA: 0x00008499 File Offset: 0x00006699
		public Type SourceType { get; }

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x06000340 RID: 832 RVA: 0x000084A1 File Offset: 0x000066A1
		public Type DestinationType { get; }

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x06000341 RID: 833 RVA: 0x000084A9 File Offset: 0x000066A9
		public MemberInfo[] MembersToExpand { get; }

		// Token: 0x06000342 RID: 834 RVA: 0x000084B4 File Offset: 0x000066B4
		public ExpressionRequest(Type sourceType, Type destinationType, params MemberInfo[] membersToExpand)
		{
			this.SourceType = sourceType;
			this.DestinationType = destinationType;
			this.MembersToExpand = (from p in membersToExpand
			orderby p.Name
			select p).ToArray<MemberInfo>();
		}

		// Token: 0x06000343 RID: 835 RVA: 0x00008508 File Offset: 0x00006708
		public bool Equals(ExpressionRequest other)
		{
			return other != null && (this == other || (this.MembersToExpand.SequenceEqual(other.MembersToExpand) && this.SourceType == other.SourceType && this.DestinationType == other.DestinationType));
		}

		// Token: 0x06000344 RID: 836 RVA: 0x00008559 File Offset: 0x00006759
		public override bool Equals(object obj)
		{
			return obj != null && (this == obj || (!(obj.GetType() != base.GetType()) && this.Equals((ExpressionRequest)obj)));
		}

		// Token: 0x06000345 RID: 837 RVA: 0x00008588 File Offset: 0x00006788
		public override int GetHashCode()
		{
			int num = this.SourceType.GetHashCode();
			num = (num * 397 ^ this.DestinationType.GetHashCode());
			return this.MembersToExpand.Aggregate(num, (int currentHash, MemberInfo p) => currentHash * 397 ^ p.GetHashCode());
		}

		// Token: 0x06000346 RID: 838 RVA: 0x000085E0 File Offset: 0x000067E0
		public static bool operator ==(ExpressionRequest left, ExpressionRequest right)
		{
			return object.Equals(left, right);
		}

		// Token: 0x06000347 RID: 839 RVA: 0x000085E9 File Offset: 0x000067E9
		public static bool operator !=(ExpressionRequest left, ExpressionRequest right)
		{
			return !object.Equals(left, right);
		}
	}
}
