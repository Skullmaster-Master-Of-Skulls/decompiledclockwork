using System;
using System.Collections.Generic;

namespace Oracle.SqlAndPlsqlParser.LocalParsing
{
	// Token: 0x020001D4 RID: 468
	internal abstract class OracleLpFromClauseBase : OracleLpStatementElement
	{
		// Token: 0x170002EA RID: 746
		// (get) Token: 0x060011AF RID: 4527 RVA: 0x000C3F0C File Offset: 0x000C210C
		internal override OracleLpStatementElementType ElementType
		{
			get
			{
				return OracleLpStatementElementType.FromClause;
			}
		}

		// Token: 0x170002EB RID: 747
		// (get) Token: 0x060011B0 RID: 4528
		public abstract List<OracleLpStatementDataContainer> Terms { get; }

		// Token: 0x060011B1 RID: 4529 RVA: 0x000C3F10 File Offset: 0x000C2110
		public OracleLpFromClauseBase(OracleLpStatementElement parent) : base(parent)
		{
		}

		// Token: 0x060011B2 RID: 4530
		public abstract OracleLpColumnDescriptor FindColumn(OracleLpName schema, OracleLpName parent, OracleLpName column);

		// Token: 0x060011B3 RID: 4531
		public abstract IOracleLpColumnDescriptorContainer FindColumnContainer(OracleLpName schema, OracleLpName parent);

		// Token: 0x060011B4 RID: 4532
		public abstract OracleLpQteNamedObject FindNamedObject(OracleLpName schema, OracleLpName parent);
	}
}
