using System;

namespace System.Xml.Schema
{
	// Token: 0x02000207 RID: 519
	internal class RedefineEntry
	{
		// Token: 0x0600187F RID: 6271 RVA: 0x0006E11C File Offset: 0x0006D11C
		public RedefineEntry(XmlSchemaRedefine external, XmlSchema schema)
		{
			this.redefine = external;
			this.schemaToUpdate = schema;
		}

		// Token: 0x04000E71 RID: 3697
		internal XmlSchemaRedefine redefine;

		// Token: 0x04000E72 RID: 3698
		internal XmlSchema schemaToUpdate;
	}
}
