using System;

namespace System.Data
{
	// Token: 0x02000067 RID: 103
	internal sealed class ParentForeignKeyConstraintEnumerator : ForeignKeyConstraintEnumerator
	{
		// Token: 0x060004B2 RID: 1202 RVA: 0x001E9258 File Offset: 0x001E8658
		public ParentForeignKeyConstraintEnumerator(DataSet dataSet, DataTable inTable) : base(dataSet)
		{
			this.table = inTable;
		}

		// Token: 0x060004B3 RID: 1203 RVA: 0x001E9278 File Offset: 0x001E8678
		protected override bool IsValidCandidate(Constraint constraint)
		{
			return constraint is ForeignKeyConstraint && ((ForeignKeyConstraint)constraint).RelatedTable == this.table;
		}

		// Token: 0x040006E2 RID: 1762
		private DataTable table;
	}
}
