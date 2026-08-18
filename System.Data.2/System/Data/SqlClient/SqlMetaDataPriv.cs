using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.SqlServer.Server;

namespace System.Data.SqlClient
{
	// Token: 0x02000223 RID: 547
	internal class SqlMetaDataPriv
	{
		// Token: 0x06002220 RID: 8736 RVA: 0x000ECC6C File Offset: 0x000EC06C
		internal SqlMetaDataPriv()
		{
		}

		// Token: 0x06002221 RID: 8737 RVA: 0x000ECC98 File Offset: 0x000EC098
		internal virtual void CopyFrom(SqlMetaDataPriv original)
		{
			this.type = original.type;
			this.tdsType = original.tdsType;
			this.precision = original.precision;
			this.scale = original.scale;
			this.length = original.length;
			this.collation = original.collation;
			this.codePage = original.codePage;
			this.encoding = original.encoding;
			this.isNullable = original.isNullable;
			this.isMultiValued = original.isMultiValued;
			this.udtDatabaseName = original.udtDatabaseName;
			this.udtSchemaName = original.udtSchemaName;
			this.udtTypeName = original.udtTypeName;
			this.udtAssemblyQualifiedName = original.udtAssemblyQualifiedName;
			this.udtType = original.udtType;
			this.xmlSchemaCollectionDatabase = original.xmlSchemaCollectionDatabase;
			this.xmlSchemaCollectionOwningSchema = original.xmlSchemaCollectionOwningSchema;
			this.xmlSchemaCollectionName = original.xmlSchemaCollectionName;
			this.metaType = original.metaType;
			this.structuredTypeDatabaseName = original.structuredTypeDatabaseName;
			this.structuredTypeSchemaName = original.structuredTypeSchemaName;
			this.structuredTypeName = original.structuredTypeName;
			this.structuredFields = original.structuredFields;
		}

		// Token: 0x06002222 RID: 8738 RVA: 0x000ECDBC File Offset: 0x000EC1BC
		internal bool IsAlgorithmInitialized()
		{
			return this.cipherMD != null && this.cipherMD.IsAlgorithmInitialized();
		}

		// Token: 0x1700057A RID: 1402
		// (get) Token: 0x06002223 RID: 8739 RVA: 0x000ECDE0 File Offset: 0x000EC1E0
		internal byte NormalizationRuleVersion
		{
			get
			{
				if (this.cipherMD != null)
				{
					return this.cipherMD.NormalizationRuleVersion;
				}
				return 0;
			}
		}

		// Token: 0x04001479 RID: 5241
		internal SqlDbType type;

		// Token: 0x0400147A RID: 5242
		internal byte tdsType;

		// Token: 0x0400147B RID: 5243
		internal byte precision = byte.MaxValue;

		// Token: 0x0400147C RID: 5244
		internal byte scale = byte.MaxValue;

		// Token: 0x0400147D RID: 5245
		internal int length;

		// Token: 0x0400147E RID: 5246
		internal SqlCollation collation;

		// Token: 0x0400147F RID: 5247
		internal int codePage;

		// Token: 0x04001480 RID: 5248
		internal Encoding encoding;

		// Token: 0x04001481 RID: 5249
		internal bool isNullable;

		// Token: 0x04001482 RID: 5250
		internal bool isMultiValued;

		// Token: 0x04001483 RID: 5251
		internal string udtDatabaseName;

		// Token: 0x04001484 RID: 5252
		internal string udtSchemaName;

		// Token: 0x04001485 RID: 5253
		internal string udtTypeName;

		// Token: 0x04001486 RID: 5254
		internal string udtAssemblyQualifiedName;

		// Token: 0x04001487 RID: 5255
		internal Type udtType;

		// Token: 0x04001488 RID: 5256
		internal string xmlSchemaCollectionDatabase;

		// Token: 0x04001489 RID: 5257
		internal string xmlSchemaCollectionOwningSchema;

		// Token: 0x0400148A RID: 5258
		internal string xmlSchemaCollectionName;

		// Token: 0x0400148B RID: 5259
		internal MetaType metaType;

		// Token: 0x0400148C RID: 5260
		internal string structuredTypeDatabaseName;

		// Token: 0x0400148D RID: 5261
		internal string structuredTypeSchemaName;

		// Token: 0x0400148E RID: 5262
		internal string structuredTypeName;

		// Token: 0x0400148F RID: 5263
		internal IList<SmiMetaData> structuredFields;

		// Token: 0x04001490 RID: 5264
		internal bool isEncrypted;

		// Token: 0x04001491 RID: 5265
		internal SqlMetaDataPriv baseTI;

		// Token: 0x04001492 RID: 5266
		internal SqlCipherMetadata cipherMD;
	}
}
