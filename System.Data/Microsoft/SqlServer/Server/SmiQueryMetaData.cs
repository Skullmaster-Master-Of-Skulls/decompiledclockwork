using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Globalization;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x02000041 RID: 65
	internal class SmiQueryMetaData : SmiStorageMetaData
	{
		// Token: 0x06000252 RID: 594 RVA: 0x001DEC18 File Offset: 0x001DE018
		[Obsolete("Not supported as of SMI v2.  Will be removed when v1 support dropped. Use ctor without columns param.")]
		internal SmiQueryMetaData(SqlDbType dbType, long maxLength, byte precision, byte scale, long localeId, SqlCompareOptions compareOptions, Type userDefinedType, SmiMetaData[] columns, string name, string typeSpecificNamePart1, string typeSpecificNamePart2, string typeSpecificNamePart3, bool allowsDBNull, string serverName, string catalogName, string schemaName, string tableName, string columnName, SqlBoolean isKey, bool isIdentity, bool isReadOnly, SqlBoolean isExpression, SqlBoolean isAliased, SqlBoolean isHidden) : this(dbType, maxLength, precision, scale, localeId, compareOptions, userDefinedType, name, typeSpecificNamePart1, typeSpecificNamePart2, typeSpecificNamePart3, allowsDBNull, serverName, catalogName, schemaName, tableName, columnName, isKey, isIdentity, isReadOnly, isExpression, isAliased, isHidden)
		{
		}

		// Token: 0x06000253 RID: 595 RVA: 0x001DEC58 File Offset: 0x001DE058
		internal SmiQueryMetaData(SqlDbType dbType, long maxLength, byte precision, byte scale, long localeId, SqlCompareOptions compareOptions, Type userDefinedType, string name, string typeSpecificNamePart1, string typeSpecificNamePart2, string typeSpecificNamePart3, bool allowsDBNull, string serverName, string catalogName, string schemaName, string tableName, string columnName, SqlBoolean isKey, bool isIdentity, bool isReadOnly, SqlBoolean isExpression, SqlBoolean isAliased, SqlBoolean isHidden) : this(dbType, maxLength, precision, scale, localeId, compareOptions, userDefinedType, false, null, null, name, typeSpecificNamePart1, typeSpecificNamePart2, typeSpecificNamePart3, allowsDBNull, serverName, catalogName, schemaName, tableName, columnName, isKey, isIdentity, isReadOnly, isExpression, isAliased, isHidden)
		{
		}

		// Token: 0x06000254 RID: 596 RVA: 0x001DECA8 File Offset: 0x001DE0A8
		internal SmiQueryMetaData(SqlDbType dbType, long maxLength, byte precision, byte scale, long localeId, SqlCompareOptions compareOptions, Type userDefinedType, bool isMultiValued, IList<SmiExtendedMetaData> fieldMetaData, SmiMetaDataPropertyCollection extendedProperties, string name, string typeSpecificNamePart1, string typeSpecificNamePart2, string typeSpecificNamePart3, bool allowsDBNull, string serverName, string catalogName, string schemaName, string tableName, string columnName, SqlBoolean isKey, bool isIdentity, bool isReadOnly, SqlBoolean isExpression, SqlBoolean isAliased, SqlBoolean isHidden) : this(dbType, maxLength, precision, scale, localeId, compareOptions, userDefinedType, null, isMultiValued, fieldMetaData, extendedProperties, name, typeSpecificNamePart1, typeSpecificNamePart2, typeSpecificNamePart3, allowsDBNull, serverName, catalogName, schemaName, tableName, columnName, isKey, isIdentity, false, isReadOnly, isExpression, isAliased, isHidden)
		{
		}

		// Token: 0x06000255 RID: 597 RVA: 0x001DECF8 File Offset: 0x001DE0F8
		internal SmiQueryMetaData(SqlDbType dbType, long maxLength, byte precision, byte scale, long localeId, SqlCompareOptions compareOptions, Type userDefinedType, string udtAssemblyQualifiedName, bool isMultiValued, IList<SmiExtendedMetaData> fieldMetaData, SmiMetaDataPropertyCollection extendedProperties, string name, string typeSpecificNamePart1, string typeSpecificNamePart2, string typeSpecificNamePart3, bool allowsDBNull, string serverName, string catalogName, string schemaName, string tableName, string columnName, SqlBoolean isKey, bool isIdentity, bool isColumnSet, bool isReadOnly, SqlBoolean isExpression, SqlBoolean isAliased, SqlBoolean isHidden) : base(dbType, maxLength, precision, scale, localeId, compareOptions, userDefinedType, udtAssemblyQualifiedName, isMultiValued, fieldMetaData, extendedProperties, name, typeSpecificNamePart1, typeSpecificNamePart2, typeSpecificNamePart3, allowsDBNull, serverName, catalogName, schemaName, tableName, columnName, isKey, isIdentity, isColumnSet)
		{
			this._isReadOnly = isReadOnly;
			this._isExpression = isExpression;
			this._isAliased = isAliased;
			this._isHidden = isHidden;
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000256 RID: 598 RVA: 0x001DED58 File Offset: 0x001DE158
		internal bool IsReadOnly
		{
			get
			{
				return this._isReadOnly;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000257 RID: 599 RVA: 0x001DED78 File Offset: 0x001DE178
		internal SqlBoolean IsExpression
		{
			get
			{
				return this._isExpression;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000258 RID: 600 RVA: 0x001DED98 File Offset: 0x001DE198
		internal SqlBoolean IsAliased
		{
			get
			{
				return this._isAliased;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000259 RID: 601 RVA: 0x001DEDB8 File Offset: 0x001DE1B8
		internal SqlBoolean IsHidden
		{
			get
			{
				return this._isHidden;
			}
		}

		// Token: 0x0600025A RID: 602 RVA: 0x001DEDD8 File Offset: 0x001DE1D8
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

		// Token: 0x040005EA RID: 1514
		private bool _isReadOnly;

		// Token: 0x040005EB RID: 1515
		private SqlBoolean _isExpression;

		// Token: 0x040005EC RID: 1516
		private SqlBoolean _isAliased;

		// Token: 0x040005ED RID: 1517
		private SqlBoolean _isHidden;
	}
}
