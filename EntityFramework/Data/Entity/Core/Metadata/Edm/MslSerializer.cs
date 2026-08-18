using System;
using System.Data.Entity.Utilities;
using System.Xml;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x0200002C RID: 44
	internal class MslSerializer
	{
		// Token: 0x060001D2 RID: 466 RVA: 0x0000A6D0 File Offset: 0x000088D0
		public virtual bool Serialize(DbDatabaseMapping databaseMapping, XmlWriter xmlWriter)
		{
			Check.NotNull<DbDatabaseMapping>(databaseMapping, "databaseMapping");
			Check.NotNull<XmlWriter>(xmlWriter, "xmlWriter");
			MslXmlSchemaWriter mslXmlSchemaWriter = new MslXmlSchemaWriter(xmlWriter, databaseMapping.Model.SchemaVersion);
			mslXmlSchemaWriter.WriteSchema(databaseMapping);
			return true;
		}
	}
}
