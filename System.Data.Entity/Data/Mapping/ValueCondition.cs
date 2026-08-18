using System;

namespace System.Data.Mapping
{
	// Token: 0x02000229 RID: 553
	internal class ValueCondition : IEquatable<ValueCondition>
	{
		// Token: 0x060023BD RID: 9149 RVA: 0x000814E7 File Offset: 0x0007F6E7
		private ValueCondition(string description, bool isSentinel)
		{
			this.Description = description;
			this.IsSentinel = isSentinel;
		}

		// Token: 0x060023BE RID: 9150 RVA: 0x000814FD File Offset: 0x0007F6FD
		internal ValueCondition(string description) : this(description, false)
		{
		}

		// Token: 0x1700070E RID: 1806
		// (get) Token: 0x060023BF RID: 9151 RVA: 0x00081507 File Offset: 0x0007F707
		internal bool IsNotNullCondition
		{
			get
			{
				return this == ValueCondition.IsNotNull;
			}
		}

		// Token: 0x060023C0 RID: 9152 RVA: 0x00081511 File Offset: 0x0007F711
		public bool Equals(ValueCondition other)
		{
			return other.IsSentinel == this.IsSentinel && other.Description == this.Description;
		}

		// Token: 0x060023C1 RID: 9153 RVA: 0x00081534 File Offset: 0x0007F734
		public override int GetHashCode()
		{
			return this.Description.GetHashCode();
		}

		// Token: 0x060023C2 RID: 9154 RVA: 0x00081541 File Offset: 0x0007F741
		public override string ToString()
		{
			return this.Description;
		}

		// Token: 0x04000FD7 RID: 4055
		internal readonly string Description;

		// Token: 0x04000FD8 RID: 4056
		internal readonly bool IsSentinel;

		// Token: 0x04000FD9 RID: 4057
		internal const string IsNullDescription = "NULL";

		// Token: 0x04000FDA RID: 4058
		internal const string IsNotNullDescription = "NOT NULL";

		// Token: 0x04000FDB RID: 4059
		internal const string IsOtherDescription = "OTHER";

		// Token: 0x04000FDC RID: 4060
		internal static readonly ValueCondition IsNull = new ValueCondition("NULL", true);

		// Token: 0x04000FDD RID: 4061
		internal static readonly ValueCondition IsNotNull = new ValueCondition("NOT NULL", true);

		// Token: 0x04000FDE RID: 4062
		internal static readonly ValueCondition IsOther = new ValueCondition("OTHER", true);
	}
}
