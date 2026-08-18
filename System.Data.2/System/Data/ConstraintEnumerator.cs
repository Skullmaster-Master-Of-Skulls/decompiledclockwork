using System;
using System.Collections;

namespace System.Data
{
	// Token: 0x020000A0 RID: 160
	internal class ConstraintEnumerator
	{
		// Token: 0x06000826 RID: 2086 RVA: 0x00057E08 File Offset: 0x00057208
		public ConstraintEnumerator(DataSet dataSet)
		{
			this.tables = ((dataSet != null) ? dataSet.Tables.GetEnumerator() : null);
			this.currentObject = null;
		}

		// Token: 0x06000827 RID: 2087 RVA: 0x00057E3C File Offset: 0x0005723C
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

		// Token: 0x06000828 RID: 2088 RVA: 0x00057ED8 File Offset: 0x000572D8
		public Constraint GetConstraint()
		{
			return this.currentObject;
		}

		// Token: 0x06000829 RID: 2089 RVA: 0x00057EEC File Offset: 0x000572EC
		protected virtual bool IsValidCandidate(Constraint constraint)
		{
			return true;
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x0600082A RID: 2090 RVA: 0x00057EFC File Offset: 0x000572FC
		protected Constraint CurrentObject
		{
			get
			{
				return this.currentObject;
			}
		}

		// Token: 0x040002E8 RID: 744
		private IEnumerator tables;

		// Token: 0x040002E9 RID: 745
		private IEnumerator constraints;

		// Token: 0x040002EA RID: 746
		private Constraint currentObject;
	}
}
