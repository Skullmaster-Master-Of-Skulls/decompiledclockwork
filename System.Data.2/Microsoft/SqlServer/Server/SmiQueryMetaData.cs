using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Globalization;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x02000047 RID: 71
	internal class SmiQueryMetaData : SmiStorageMetaData
	{
		// Token: 0x0600024F RID: 591 RVA: 0x0003BAD8 File Offset: 0x0003AED8
		[Obsolete("Not supported as of SMI v2.  Will be removed when v1 support dropped. Use ctor without columns param.")]
		internal SmiQueryMetaData(SqlDbType dbType, long maxLength, byte precision, byte scale, long localeId, SqlCompareOptions compareOptions, Type userDefinedType, SmiMetaData[] columns, string name, string typeSpecificNamePart1, string typeSpecificNamePart2, string typeSpecificNamePart3, bool allowsDBNull, string serverName, string catalogName, string schemaName, string tableName, string columnName, SqlBoolean isKey, bool isIdentity, bool isReadOnly, SqlBoolean isExpression, SqlBoolean isAliased, SqlBoolean isHidden) : this(dbType, maxLength, precision, scale, localeId, compareOptions, userDefinedType, name, typeSpecificNamePart1, typeSpecificNamePart2, typeSpecificNamePart3, allowsDBNull, serverName, catalogName, schemaName, tableName, columnName, isKey, isIdentity, isReadOnly, isExpression, isAliased, isHidden)
		{
		}

		// Token: 0x06000250 RID: 592 RVA: 0x0003BB18 File Offset: 0x0003AF18
		internal SmiQueryMetaData(SqlDbType dbType, long maxLength, byte precision, byte scale, long localeId, SqlCompareOptions compareOptions, Type userDefinedType, string name, string typeSpecificNamePart1, string typeSpecificNamePart2, string typeSpecificNamePart3, bool allowsDBNull, string serverName, string catalogName, string schemaName, string tableName, string columnName, SqlBoolean isKey, bool isIdentity, bool isReadOnly, SqlBoolean isExpression, SqlBoolean isAliased, SqlBoolean isHidden) : this(dbType, maxLength, precision, scale, localeId, compareOptions, userDefinedType, false, null, null, name, typeSpecificNamePart1, typeSpecificNamePart2, typeSpecificNamePart3, allowsDBNull, serverName, catalogName, schemaName, tableName, columnName, isKey, isIdentity, isReadOnly, isExpression, isAliased, isHidden)
		{
		}

		// Token: 0x06000251 RID: 593 RVA: 0x0003BB5C File Offset: 0x0003AF5C
		internal SmiQueryMetaData(SqlDbType dbType, long maxLength, byte precision, byte scale, long localeId, SqlCompareOptions compareOptions, Type userDefinedType, bool isMultiValued, IList<SmiExtendedMetaData> fieldMetaData, SmiMetaDataPropertyCollection extendedProperties, string name, string typeSpecificNamePart1, string typeSpecificNamePart2, string typeSpecificNamePart3, bool allowsDBNull, string serverName, string catalogName, string schemaName, string tableName, string columnName, SqlBoolean isKey, bool isIdentity, bool isReadOnly, SqlBoolean isExpression, SqlBoolean isAliased, SqlBoolean isHidden) : this(dbType, maxLength, precision, scale, localeId, compareOptions, userDefinedType, null, isMultiValued, fieldMetaData, extendedProperties, name, typeSpecificNamePart1, typeSpecificNamePart2, typeSpecificNamePart3, allowsDBNull, serverName, catalogName, schemaName, tableName, columnName, isKey, isIdentity, false, isReadOnly, isExpression, isAliased, isHidden)
		{
		}

		// Token: 0x06000252 RID: 594 RVA: 0x0003BBA4 File Offset: 0x0003AFA4
		internal SmiQueryMetaData(SqlDbType dbType, long maxLength, byte precision, byte scale, long localeId, SqlCompareOptions compareOptions, Type userDefinedType, string udtAssemblyQualifiedName, bool isMultiValued, IList<SmiExtendedMetaData> fieldMetaData, SmiMetaDataPropertyCollection extendedProperties, string name, string typeSpecificNamePart1, string typeSpecificNamePart2, string typeSpecificNamePart3, bool allowsDBNull, string serverName, string catalogName, string schemaName, string tableName, string columnName, SqlBoolean isKey, bool isIdentity, bool isColumnSet, bool isReadOnly, SqlBoolean isExpression, SqlBoolean isAliased, SqlBoolean isHidden) : base(dbType, maxLength, precision, scale, localeId, compareOptions, userDefinedType, udtAssemblyQualifiedName, isMultiValued, fieldMetaData, extendedProperties, name, typeSpecificNamePart1, typeSpecificNamePart2, typeSpecificNamePart3, allowsDBNull, serverName, catalogName, schemaName, tableName, columnName, isKey, isIdentity, isColumnSet)
		{
			this._isReadOnly = isReadOnly;
			this._isExpression = isExpression;
			this._isAliased = isAliased;
			this._isHidden = isHidden;
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000253 RID: 595 RVA: 0x0003BC04 File Offset: 0x0003B004
		internal bool IsReadOnly
		{
			get
			{
				return this._isReadOnly;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000254 RID: 596 RVA: 0x0003BC18 File Offset: 0x0003B018
		internal SqlBoolean IsExpression
		{
			get
			{
				return this._isExpression;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000255 RID: 597 RVA: 0x0003BC2C File Offset: 0x0003B02C
		internal SqlBoolean IsAliased
		{
			get
			{
				return this._isAliased;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000256 RID: 598 RVA: 0x0003BC40 File Offset: 0x0003B040
		internal SqlBoolean IsHidden
		{
			get
			{
				return this._isHidden;
			}
		}

		// Token: 0x06000257 RID: 599 RVA: 0x0003BC54 File Offset: 0x0003B054
		internal override string TraceString(int indent)
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}{1}           IsReadOnly={2}\n\t{1}         IsExpression={3}\n\t{1}            IsAliased={4}\n\t{1}             IsHidden={5}", new object[]
			{
				base.TraceString(indent),
				new string(' ', indent),
				base.AllowsDBNull,
				this.IsExpression,
				this.IsAliased,
				this.IsHidden
			});
		}

		// Token: 0x0400015C RID: 348
		private bool _isReadOnly;

		// Token: 0x0400015D RID: 349
		private SqlBoolean _isExpression;

		// Token: 0x0400015E RID: 350
		private SqlBoolean _isAliased;

		// Token: 0x0400015F RID: 351
		private SqlBoolean _isHidden;
	}
}
