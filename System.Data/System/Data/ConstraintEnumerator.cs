using System;
using System.Collections;

namespace System.Data
{
	// Token: 0x02000064 RID: 100
	internal class ConstraintEnumerator
	{
		// Token: 0x060004A8 RID: 1192 RVA: 0x001E9078 File Offset: 0x001E8478
		public ConstraintEnumerator(DataSet dataSet)
		{
			this.tables = ((dataSet != null) ? dataSet.Tables.GetEnumerator() : null);
			this.currentObject = null;
		}

		// Token: 0x060004A9 RID: 1193 RVA: 0x001E90B8 File Offset: 0x001E84B8
		public bool GetNext()
		{
			this.currentObject = null;
			while (this.tables != null)
			{
				if (this.constraints == null)
				{
					if (!this.tables.MoveNext())
					{
						this.tables = null;
						return false;
					}
					this.constraints = ((DataTable)this.tables.Current).Constraints.GetEnumerator();
				}
				if (!this.constraints.MoveNext())
				{
					this.constraints = null;
				}
				else
				{
					Constraint constraint = (Constraint)this.constraints.Current;
					if (this.IsValidCandidate(constraint))
					{
						this.currentObject = constraint;
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060004AA RID: 1194 RVA: 0x001E9158 File Offset: 0x001E8558
		public Constraint GetConstraint()
		{
			return this.currentObject;
		}

		// Token: 0x060004AB RID: 1195 RVA: 0x001E9178 File Offset: 0x001E8578
		protected virtual bool IsValidCandidate(Constraint constraint)
		{
			return true;
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060004AC RID: 1196 RVA: 0x001E9188 File Offset: 0x001E8588
		protected Constraint CurrentObject
		{
			get
			{
				return this.currentObject;
			}
		}

		// Token: 0x040006DE RID: 1758
		private IEnumerator tables;

		// Token: 0x040006DF RID: 1759
		private IEnumerator constraints;

		// Token: 0x040006E0 RID: 1760
		private Constraint currentObject;
	}
}
