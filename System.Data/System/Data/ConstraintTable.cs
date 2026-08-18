using System;
using System.Xml.Schema;

namespace System.Data
{
	// Token: 0x020000FA RID: 250
	internal sealed class ConstraintTable
	{
		// Token: 0x06000E93 RID: 3731 RVA: 0x002221A8 File Offset: 0x002215A8
		public ConstraintTable(DataTable t, XmlSchemaIdentityConstraint c)
		{
			this.table = t;
			this.constraint = c;
		}

		// Token: 0x04000A91 RID: 2705
		public DataTable table;

		// Token: 0x04000A92 RID: 2706
		public XmlSchemaIdentityConstraint constraint;
	}
}
