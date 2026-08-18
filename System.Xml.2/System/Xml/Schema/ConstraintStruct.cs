using System;
using System.Collections;

namespace System.Xml.Schema
{
	// Token: 0x020001E4 RID: 484
	internal sealed class ConstraintStruct
	{
		// Token: 0x170006A9 RID: 1705
		// (get) Token: 0x06002060 RID: 8288 RVA: 0x000B1D84 File Offset: 0x000AFF84
		internal int TableDim
		{
			get
			{
				return this.tableDim;
			}
		}

		// Token: 0x06002061 RID: 8289 RVA: 0x000B1D8C File Offset: 0x000AFF8C
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

		// Token: 0x04000D95 RID: 3477
		internal CompiledIdentityConstraint constraint;

		// Token: 0x04000D96 RID: 3478
		internal SelectorActiveAxis axisSelector;

		// Token: 0x04000D97 RID: 3479
		internal ArrayList axisFields;

		// Token: 0x04000D98 RID: 3480
		internal Hashtable qualifiedTable;

		// Token: 0x04000D99 RID: 3481
		internal Hashtable keyrefTable;

		// Token: 0x04000D9A RID: 3482
		private int tableDim;
	}
}
