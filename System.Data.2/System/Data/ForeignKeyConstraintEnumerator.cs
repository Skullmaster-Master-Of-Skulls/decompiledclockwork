using System;

namespace System.Data
{
	// Token: 0x020000A1 RID: 161
	internal class ForeignKeyConstraintEnumerator : ConstraintEnumerator
	{
		// Token: 0x0600082B RID: 2091 RVA: 0x00057F10 File Offset: 0x00057310
		public ForeignKeyConstraintEnumerator(DataSet dataSet) : base(dataSet)
		{
		}

		// Token: 0x0600082C RID: 2092 RVA: 0x00057F24 File Offset: 0x00057324
		protected override bool IsValidCandidate(Constraint constraint)
		{
			return constraint is ForeignKeyConstraint;
		}

		// Token: 0x0600082D RID: 2093 RVA: 0x00057F3C File Offset: 0x0005733C
		public ForeignKeyConstraint GetForeignKeyConstraint()
		{
			return (ForeignKeyConstraint)base.CurrentObject;
		}
	}
}
