using System;
using System.Collections;

namespace System.Xml.Schema
{
	// Token: 0x0200018B RID: 395
	internal sealed class ConstraintStruct
	{
		// Token: 0x17000505 RID: 1285
		// (get) Token: 0x06001510 RID: 5392 RVA: 0x0005E068 File Offset: 0x0005D068
		internal int TableDim
		{
			get
			{
				return this.tableDim;
			}
		}

		// Token: 0x06001511 RID: 5393 RVA: 0x0005E070 File Offset: 0x0005D070
		internal ConstraintStruct(CompiledIdentityConstraint constraint)
		{
			this.constraint = constraint;
			this.tableDim = constraint.Fields.Length;
			this.axisFields = new ArrayList();
			this.axisSelector = new SelectorActiveAxis(constraint.Selector, this);
			if (this.constraint.Role != CompiledIdentityConstraint.ConstraintRole.Keyref)
			{
				this.qualifiedTable = new Hashtable();
			}
		}

		// Token: 0x04000C9E RID: 3230
		internal CompiledIdentityConstraint constraint;

		// Token: 0x04000C9F RID: 3231
		internal SelectorActiveAxis axisSelector;

		// Token: 0x04000CA0 RID: 3232
		internal ArrayList axisFields;

		// Token: 0x04000CA1 RID: 3233
		internal Hashtable qualifiedTable;

		// Token: 0x04000CA2 RID: 3234
		internal Hashtable keyrefTable;

		// Token: 0x04000CA3 RID: 3235
		private int tableDim;
	}
}
