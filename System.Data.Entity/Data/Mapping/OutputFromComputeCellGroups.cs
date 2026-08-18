using System;
using System.Collections.Generic;
using System.Data.Common.Utils;
using System.Data.Mapping.ViewGeneration.Structures;
using System.Data.Mapping.ViewGeneration.Validation;

namespace System.Data.Mapping
{
	// Token: 0x02000241 RID: 577
	internal struct OutputFromComputeCellGroups
	{
		// Token: 0x0400101B RID: 4123
		internal List<Cell> Cells;

		// Token: 0x0400101C RID: 4124
		internal CqlIdentifiers Identifiers;

		// Token: 0x0400101D RID: 4125
		internal List<Set<Cell>> CellGroups;

		// Token: 0x0400101E RID: 4126
		internal List<ForeignConstraint> ForeignKeyConstraints;

		// Token: 0x0400101F RID: 4127
		internal bool Success;
	}
}
