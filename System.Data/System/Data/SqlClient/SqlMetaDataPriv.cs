using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.SqlServer.Server;

namespace System.Data.SqlClient
{
	// Token: 0x02000328 RID: 808
	internal class SqlMetaDataPriv
	{
		// Token: 0x06002A5D RID: 10845 RVA: 0x002BE798 File Offset: 0x002BDB98
		internal SqlMetaDataPriv()
		{
		}

		// Token: 0x04001BBE RID: 7102
		internal SqlDbType type;

		// Token: 0x04001BBF RID: 7103
		internal byte tdsType;

		// Token: 0x04001BC0 RID: 7104
		internal byte precision = byte.MaxValue;

		// Token: 0x04001BC1 RID: 7105
		internal byte scale = byte.MaxValue;

		// Token: 0x04001BC2 RID: 7106
		internal int length;

		// Token: 0x04001BC3 RID: 7107
		internal SqlCollation collation;

		// Token: 0x04001BC4 RID: 7108
		internal int codePage;

		// Token: 0x04001BC5 RID: 7109
		internal Encoding encoding;

		// Token: 0x04001BC6 RID: 7110
		internal bool isNullable;

		// Token: 0x04001BC7 RID: 7111
		internal bool isMultiValued;

		// Token: 0x04001BC8 RID: 7112
		internal string udtDatabaseName;

		// Token: 0x04001BC9 RID: 7113
		internal string udtSchemaName;

		// Token: 0x04001BCA RID: 7114
		internal string udtTypeName;

		// Token: 0x04001BCB RID: 7115
		internal string udtAssemblyQualifiedName;

		// Token: 0x04001BCC RID: 7116
		internal Type udtType;

		// Token: 0x04001BCD RID: 7117
		internal string xmlSchemaCollectionDatabase;

		// Token: 0x04001BCE RID: 7118
		internal string xmlSchemaCollectionOwningSchema;

		// Token: 0x04001BCF RID: 7119
		internal string xmlSchemaCollectionName;

		// Token: 0x04001BD0 RID: 7120
		internal MetaType metaType;

		// Token: 0x04001BD1 RID: 7121
		internal string structuredTypeDatabaseName;

		// Token: 0x04001BD2 RID: 7122
		internal string structuredTypeSchemaName;

		// Token: 0x04001BD3 RID: 7123
		internal string structuredTypeName;

		// Token: 0x04001BD4 RID: 7124
		internal IList<SmiMetaData> structuredFields;
	}
}
