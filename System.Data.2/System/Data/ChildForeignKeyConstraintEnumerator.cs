using System;

namespace System.Data
{
	// Token: 0x020000A2 RID: 162
	internal sealed class ChildForeignKeyConstraintEnumerator : ForeignKeyConstraintEnumerator
	{
		// Token: 0x0600082E RID: 2094 RVA: 0x00057F54 File Offset: 0x00057354
		public ChildForeignKeyConstraintEnumerator(DataSet dataSet, DataTable inTable) : base(dataSet)
		{
			this.table = inTable;
		}

		// Token: 0x0600082F RID: 2095 RVA: 0x00057F70 File Offset: 0x00057370
		protected override bool IsValidCandidate(Constraint constraint)
		{
			return constraint is ForeignKeyConstraint && ((ForeignKeyConstraint)constraint).Table == this.table;
		}

		// Token: 0x040002EB RID: 747
		private DataTable table;
	}
}
