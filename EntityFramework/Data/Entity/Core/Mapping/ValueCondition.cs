using System;

namespace System.Data.Entity.Core.Mapping
{
	// Token: 0x0200041C RID: 1052
	internal class ValueCondition : IEquatable<ValueCondition>
	{
		// Token: 0x060026BC RID: 9916 RVA: 0x000BAFE1 File Offset: 0x000B91E1
		private ValueCondition(string description, bool isSentinel)
		{
			this.Description = description;
			this.IsSentinel = isSentinel;
		}

		// Token: 0x060026BD RID: 9917 RVA: 0x000BAFF7 File Offset: 0x000B91F7
		internal ValueCondition(string description) : this(description, false)
		{
		}

		// Token: 0x1700054E RID: 1358
		// (get) Token: 0x060026BE RID: 9918 RVA: 0x000BB001 File Offset: 0x000B9201
		internal bool IsNotNullCondition
		{
			get
			{
				return object.ReferenceEquals(this, ValueCondition.IsNotNull);
			}
		}

		// Token: 0x060026BF RID: 9919 RVA: 0x000BB00E File Offset: 0x000B920E
		public bool Equals(ValueCondition other)
		{
			return other.IsSentinel == this.IsSentinel && other.Description == this.Description;
		}

		// Token: 0x060026C0 RID: 9920 RVA: 0x000BB031 File Offset: 0x000B9231
		public override int GetHashCode()
		{
			return this.Description.GetHashCode();
		}

		// Token: 0x060026C1 RID: 9921 RVA: 0x000BB03E File Offset: 0x000B923E
		public override string ToString()
		{
			return this.Description;
		}

		// Token: 0x04000E8B RID: 3723
		internal const string IsNullDescription = "NULL";

		// Token: 0x04000E8C RID: 3724
		internal const string IsNotNullDescription = "NOT NULL";

		// Token: 0x04000E8D RID: 3725
		internal const string IsOtherDescription = "OTHER";

		// Token: 0x04000E8E RID: 3726
		internal readonly string Description;

		// Token: 0x04000E8F RID: 3727
		internal readonly bool IsSentinel;

		// Token: 0x04000E90 RID: 3728
		internal static readonly ValueCondition IsNull = new ValueCondition("NULL", true);

		// Token: 0x04000E91 RID: 3729
		internal static readonly ValueCondition IsNotNull = new ValueCondition("NOT NULL", true);

		// Token: 0x04000E92 RID: 3730
		internal static readonly ValueCondition IsOther = new ValueCondition("OTHER", true);
	}
}
