using System;

namespace System.Xml.Schema
{
	// Token: 0x02000254 RID: 596
	internal class RedefineEntry
	{
		// Token: 0x06002330 RID: 9008 RVA: 0x000BB273 File Offset: 0x000B9473
		public RedefineEntry(XmlSchemaRedefine external, XmlSchema schema)
		{
			this.redefine = external;
			this.schemaToUpdate = schema;
		}

		// Token: 0x04000ECE RID: 3790
		internal XmlSchemaRedefine redefine;

		// Token: 0x04000ECF RID: 3791
		internal XmlSchema schemaToUpdate;
	}
}
