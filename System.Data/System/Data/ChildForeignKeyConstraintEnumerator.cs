using System;

namespace System.Data
{
	// Token: 0x02000066 RID: 102
	internal sealed class ChildForeignKeyConstraintEnumerator : ForeignKeyConstraintEnumerator
	{
		// Token: 0x060004B0 RID: 1200 RVA: 0x001E9208 File Offset: 0x001E8608
		public ChildForeignKeyConstraintEnumerator(DataSet dataSet, DataTable inTable) : base(dataSet)
		{
			this.table = inTable;
		}

		// Token: 0x060004B1 RID: 1201 RVA: 0x001E9228 File Offset: 0x001E8628
		protected override bool IsValidCandidate(Constraint constraint)
		{
			return constraint is ForeignKeyConstraint && ((ForeignKeyConstraint)constraint).Table == this.table;
		}

		// Token: 0x040006E1 RID: 1761
		private DataTable table;
	}
}
