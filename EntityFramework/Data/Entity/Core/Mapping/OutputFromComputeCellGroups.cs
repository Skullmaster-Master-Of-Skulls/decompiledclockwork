using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Mapping.ViewGeneration.Structures;
using System.Data.Entity.Core.Mapping.ViewGeneration.Validation;

namespace System.Data.Entity.Core.Mapping
{
	// Token: 0x020003DA RID: 986
	internal struct OutputFromComputeCellGroups
	{
		// Token: 0x04000CA8 RID: 3240
		internal List<Cell> Cells;

		// Token: 0x04000CA9 RID: 3241
		internal CqlIdentifiers Identifiers;

		// Token: 0x04000CAA RID: 3242
		internal List<Set<Cell>> CellGroups;

		// Token: 0x04000CAB RID: 3243
		internal List<ForeignConstraint> ForeignKeyConstraints;

		// Token: 0x04000CAC RID: 3244
		internal bool Success;
	}
}
