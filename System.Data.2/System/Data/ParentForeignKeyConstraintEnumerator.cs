using System;

namespace System.Data
{
	// Token: 0x020000A3 RID: 163
	internal sealed class ParentForeignKeyConstraintEnumerator : ForeignKeyConstraintEnumerator
	{
		// Token: 0x06000830 RID: 2096 RVA: 0x00057F9C File Offset: 0x0005739C
		public ParentForeignKeyConstraintEnumerator(DataSet dataSet, DataTable inTable) : base(dataSet)
		{
			this.table = inTable;
		}

		// Token: 0x06000831 RID: 2097 RVA: 0x00057FB8 File Offset: 0x000573B8
		protected override bool IsValidCandidate(Constraint constraint)
		{
			return constraint is ForeignKeyConstraint && ((ForeignKeyConstraint)constraint).RelatedTable == this.table;
		}

		// Token: 0x040002EC RID: 748
		private DataTable table;
	}
}
