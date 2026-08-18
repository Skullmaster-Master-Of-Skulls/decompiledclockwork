using System;
using System.Xml.Schema;

namespace System.Data
{
	// Token: 0x02000142 RID: 322
	internal sealed class ConstraintTable
	{
		// Token: 0x06001300 RID: 4864 RVA: 0x00094950 File Offset: 0x00093D50
		public ConstraintTable(DataTable t, XmlSchemaIdentityConstraint c)
		{
			this.table = t;
			this.constraint = c;
		}

		// Token: 0x0400076C RID: 1900
		public DataTable table;

		// Token: 0x0400076D RID: 1901
		public XmlSchemaIdentityConstraint constraint;
	}
}
