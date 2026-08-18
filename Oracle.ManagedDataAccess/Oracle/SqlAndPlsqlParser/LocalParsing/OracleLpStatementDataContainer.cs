using System;
using System.Collections.Generic;

namespace Oracle.SqlAndPlsqlParser.LocalParsing
{
	// Token: 0x020001DF RID: 479
	internal abstract class OracleLpStatementDataContainer : OracleLpStatementElement, IOracleLpColumnDescriptorContainer, IOracleLpNamedObjectContainer
	{
		// Token: 0x170002EF RID: 751
		// (get) Token: 0x060011C1 RID: 4545 RVA: 0x000C4374 File Offset: 0x000C2574
		public virtual List<OracleLpColumnDescriptor> ColumnDescriptors
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060011C2 RID: 4546 RVA: 0x000C4378 File Offset: 0x000C2578
		public OracleLpStatementDataContainer(OracleLpStatementElement parent) : base(parent)
		{
		}

		// Token: 0x060011C3 RID: 4547 RVA: 0x000C4384 File Offset: 0x000C2584
		public virtual void Resolve()
		{
		}

		// Token: 0x060011C4 RID: 4548 RVA: 0x000C4388 File Offset: 0x000C2588
		public virtual void RetrieveNamedObjectReferences(OracleLpStatement statement)
		{
		}
	}
}
