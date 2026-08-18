using System;
using System.Collections.Generic;

namespace Oracle.SqlAndPlsqlParser.LocalParsing
{
	// Token: 0x020001DD RID: 477
	internal interface IOracleLpColumnDescriptorContainer
	{
		// Token: 0x170002EE RID: 750
		// (get) Token: 0x060011BE RID: 4542
		List<OracleLpColumnDescriptor> ColumnDescriptors { get; }

		// Token: 0x060011BF RID: 4543
		void Resolve();
	}
}
