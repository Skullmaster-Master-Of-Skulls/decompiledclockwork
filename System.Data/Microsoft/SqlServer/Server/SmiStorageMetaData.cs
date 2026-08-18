using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Globalization;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x02000040 RID: 64
	internal class SmiStorageMetaData : SmiExtendedMetaData
	{
		// Token: 0x06000244 RID: 580 RVA: 0x001DE8D8 File Offset: 0x001DDCD8
		[Obsolete("Not supported as of SMI v2.  Will be removed when v1 support dropped. Use ctor without columns param.")]
		internal SmiStorageMetaData(SqlDbType dbType, long maxLength, byte precision, byte scale, long localeId, SqlCompareOptions compareOptions, Type userDefinedType, SmiMetaData[] columns, string name, string typeSpecificNamePart1, string typeSpecificNamePart2, string typeSpecificNamePart3, bool allowsDBNull, string serverName, string catalogName, string schemaName, string tableName, string columnName, SqlBoolean isKey, bool isIdentity) : this(dbType, maxLength, precision, scale, localeId, compareOptions, userDefinedType, name, typeSpecificNamePart1, typeSpecificNamePart2, typeSpecificNamePart3, allowsDBNull, serverName, catalogName, schemaName, tableName, columnName, isKey, isIdentity)
		{
		}

		// Token: 0x06000245 RID: 581 RVA: 0x001DE918 File Offset: 0x001DDD18
		internal SmiStorageMetaData(SqlDbType dbType, long maxLength, byte precision, byte scale, long localeId, SqlCompareOptions compareOptions, Type userDefinedType, string name, string typeSpecificNamePart1, string typeSpecificNamePart2, string typeSpecificNamePart3, bool allowsDBNull, string serverName, string catalogName, string schemaName, string tableName, string columnName, SqlBoolean isKey, bool isIdentity) : this(dbType, maxLength, precision, scale, localeId, compareOptions, userDefinedType, false, null, null, name, typeSpecificNamePart1, typeSpecificNamePart2, typeSpecificNamePart3, allowsDBNull, serverName, catalogName, schemaName, tableName, columnName, isKey, isIdentity)
		{
		}

		// Token: 0x06000246 RID: 582 RVA: 0x001DE958 File Offset: 0x001DDD58
		internal SmiStorageMetaData(SqlDbType dbType, long maxLength, byte precision, byte scale, long localeId, SqlCompareOptions compareOptions, Type userDefinedType, bool isMultiValued, IList<SmiExtendedMetaData> fieldMetaData, SmiMetaDataPropertyCollection extendedProperties, string name, string typeSpecificNamePart1, string typeSpecificNamePart2, string typeSpecificNamePart3, bool allowsDBNull, string serverName, string catalogName, string schemaName, string tableName, string columnName, SqlBoolean isKey, bool isIdentity) : this(dbType, maxLength, precision, scale, localeId, compareOptions, userDefinedType, null, isMultiValued, fieldMetaData, extendedProperties, name, typeSpecificNamePart1, typeSpecificNamePart2, typeSpecificNamePart3, allowsDBNull, serverName, catalogName, schemaName, tableName, columnName, isKey, isIdentity, false)
		{
		}

		// Token: 0x06000247 RID: 583 RVA: 0x001DE998 File Offset: 0x001DDD98
		internal SmiStorageMetaData(SqlDbType dbType, long maxLength, byte precision, byte scale, long localeId, SqlCompareOptions compareOptions, Type userDefinedType, string udtAssemblyQualifiedName, bool isMultiValued, IList<SmiExtendedMetaData> fieldMetaData, SmiMetaDataPropertyCollection extendedProperties, string name, string typeSpecificNamePart1, string typeSpecificNamePart2, string typeSpecificNamePart3, bool allowsDBNull, string serverName, string catalogName, string schemaName, string tableName, string columnName, SqlBoolean isKey, bool isIdentity, bool isColumnSet) : base(dbType, maxLength, precision, scale, localeId, compareOptions, userDefinedType, udtAssemblyQualifiedName, isMultiValued, fieldMetaData, extendedProperties, name, typeSpecificNamePart1, typeSpecificNamePart2, typeSpecificNamePart3)
		{
			this._allowsDBNull = allowsDBNull;
			this._serverName = serverName;
			this._catalogName = catalogName;
			this._schemaName = schemaName;
			this._tableName = tableName;
			this._columnName = columnName;
			this._isKey = isKey;
			this._isIdentity = isIdentity;
			this._isColumnSet = isColumnSet;
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000248 RID: 584 RVA: 0x001DEA18 File Offset: 0x001DDE18
		internal bool AllowsDBNull
		{
			get
			{
				return this._allowsDBNull;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000249 RID: 585 RVA: 0x001DEA38 File Offset: 0x001DDE38
		internal string ServerName
		{
			get
			{
				return this._serverName;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600024A RID: 586 RVA: 0x001DEA58 File Offset: 0x001DDE58
		internal string CatalogName
		{
			get
			{
				return this._catalogName;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x0600024B RID: 587 RVA: 0x001DEA78 File Offset: 0x001DDE78
		internal string SchemaName
		{
			get
			{
				return this._schemaName;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x0600024C RID: 588 RVA: 0x001DEA98 File Offset: 0x001DDE98
		internal string TableName
		{
			get
			{
				return this._tableName;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x0600024D RID: 589 RVA: 0x001DEAB8 File Offset: 0x001DDEB8
		internal string ColumnName
		{
			get
			{
				return this._columnName;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x0600024E RID: 590 RVA: 0x001DEAD8 File Offset: 0x001DDED8
		internal SqlBoolean IsKey
		{
			get
			{
				return this._isKey;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x0600024F RID: 591 RVA: 0x001DEAF8 File Offset: 0x001DDEF8
		internal bool IsIdentity
		{
			get
			{
				return this._isIdentity;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000250 RID: 592 RVA: 0x001DEB18 File Offset: 0x001DDF18
		internal bool IsColumnSet
		{
			get
			{
				return this._isColumnSet;
			}
		}

		// Token: 0x06000251 RID: 593 RVA: 0x001DEB38 File Offset: 0x001DDF38
		internal override string TraceString(int indent)
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}{1}         AllowsDBNull={2}\n\t{1}           ServerName='{3}'\n\t{1}          CatalogName='{4}'\n\t{1}           SchemaName='{5}'\n\t{1}            TableName='{6}'\n\t{1}           ColumnName='{7}'\n\t{1}                IsKey={8}\n\t{1}           IsIdentity={9}\n\t", new object[]
			{
				base.TraceString(indent),
				new string(' ', indent),
				this.AllowsDBNull,
				(this.ServerName != null) ? this.ServerName : "<null>",
				(this.CatalogName != null) ? this.CatalogName : "<null>",
				(this.SchemaName != null) ? this.SchemaName : "<null>",
				(this.TableName != null) ? this.TableName : "<null>",
				(this.ColumnName != null) ? this.ColumnName : "<null>",
				this.IsKey,
				this.IsIdentity
			});
		}

		// Token: 0x040005E1 RID: 1505
		private bool _allowsDBNull;

		// Token: 0x040005E2 RID: 1506
		private string _serverName;

		// Token: 0x040005E3 RID: 1507
		private string _catalogName;

		// Token: 0x040005E4 RID: 1508
		private string _schemaName;

		// Token: 0x040005E5 RID: 1509
		private string _tableName;

		// Token: 0x040005E6 RID: 1510
		private string _columnName;

		// Token: 0x040005E7 RID: 1511
		private SqlBoolean _isKey;

		// Token: 0x040005E8 RID: 1512
		private bool _isIdentity;

		// Token: 0x040005E9 RID: 1513
		private bool _isColumnSet;
	}
}
