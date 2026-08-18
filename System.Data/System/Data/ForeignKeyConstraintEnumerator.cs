using System;

namespace System.Data
{
	// Token: 0x02000065 RID: 101
	internal class ForeignKeyConstraintEnumerator : ConstraintEnumerator
	{
		// Token: 0x060004AD RID: 1197 RVA: 0x001E91A8 File Offset: 0x001E85A8
		public ForeignKeyConstraintEnumerator(DataSet dataSet) : base(dataSet)
		{
		}

		// Token: 0x060004AE RID: 1198 RVA: 0x001E91C8 File Offset: 0x001E85C8
		protected override bool IsValidCandidate(Constraint constraint)
		{
			return constraint is ForeignKeyConstraint;
		}

		// Token: 0x060004AF RID: 1199 RVA: 0x001E91E8 File Offset: 0x001E85E8
		public ForeignKeyConstraint GetForeignKeyConstraint()
		{
			return (ForeignKeyConstraint)base.CurrentObject;
		}
	}
}
